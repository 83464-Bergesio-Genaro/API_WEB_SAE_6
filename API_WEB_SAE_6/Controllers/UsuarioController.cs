using API_WEB_SAE_6.Adapters;
using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models.Usuario;
using API_WEB_SAE_6.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace API_WEB_SAE_6.Controllers
{
    /// <summary>
    /// Es el controlador para los Usuarios de la aplicacion como tambien quien provee el Token JWT para el funcionamiento de la aplicacion.
    /// </summary>
    [EnableCors("CorsRules")]
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class UsuariosController : Controller
    {
        /// <summary>
        /// Es el adaptador con respecto a la base de datos para realizar llamadas
        /// </summary>
        private UsuarioAdapter UserAdapter = new();
        /// <summary>
        /// 
        /// </summary>
        private readonly string SecretKey;
        /// <summary>
        /// 
        /// </summary>
        private readonly string ControllerName = "UsuariosController";

        /// <summary>
        /// Recupera el contexto desde el archivo API_WEB_SAEContext <br/>
        /// </summary>

        public UsuariosController()
        {
            //Funciona asi y no recuperando la clave correcta, desconozco el origen del problema
            SecretKey = SettingsReader.GetAppSettings().SecretKey;
        }
        /// <summary>
        /// Lo utilizo para probar si funciona la API en diferentes entornos
        /// </summary>
        /// <returns> Devuelve un OK.</returns>
        /// <remarks>
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Usuarios/MockResponse
        ///     
        ///     RESPONSE:
        ///     {
        ///         "Hello World."
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve un Hello World de Prueba </response>
        /// <response code="409" >La API no esta funcionando </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("MockResponse")]
        public ActionResult<string> MockResponse()
        {
            return Ok("Hello World! V3");
        }
        /// <summary>
        /// Es el metodo para realizar consultas a la web de frc.utn.edu.ar para validar las credenciales de los estudiantes.
        /// </summary>
        /// <param name="legajo">Un valor provisto por el centro de computos unico e irrepetible</param>
        /// <param name="password">La credencial de la persona</param>
        /// <param name="dominio">Es adonde el usuario esta cargado</param>
        /// <returns>Retorna un string con el nombre de la persona</returns>
        private async Task<string> CheckSessionA4(string legajo,string password,string dominio)
        {
            try
            {
                HttpClientHandler handler = new()
                {
                    CookieContainer = new CookieContainer(),
                    AllowAutoRedirect = false
                };

                HttpClient client = new HttpClient(handler);

                // abrir la página de login primero
                await client.GetAsync("https://www.frc.utn.edu.ar/logon.frc");
                client.DefaultRequestHeaders.Add("Origin", "https://www.frc.utn.edu.ar");
                client.DefaultRequestHeaders.Add("Referer", "https://www.frc.utn.edu.ar/logon.frc");

                FormUrlEncodedContent content = new(new Dictionary<string, string>()
                {
                    {"userid", "userid"},
                    {"t", "79845687"},
                    {"page", "login"},
                    {"redir", "/logon.frc"},
                    {"txtUsuario", legajo},
                    {"txtDominios", dominio},
                    {"pwdClave", password}
                });
                //BORRAR CUANDO DESCUBRA COMO CAMBIAR MIS CREDENCIALES EN A4
                if (legajo == "gbergesio" && dominio == "frc" && password == "SAEGestion>A4") return "Genaro Rafael Bergesio";

                HttpResponseMessage response = await client.PostAsync(
                    "https://www.frc.utn.edu.ar/funciones/sesion/iniciarSesion.frc",
                    content
                );
                
                //Revisamos si con las credenciales que nos dio la persona es valido su sesion.
                //Cuando funcion en teoria devuelve el codigo 302 y nos redirige a "/"
                if(response.Headers.Location != null && response.StatusCode == HttpStatusCode.Found && response.Headers.Location.ToString() == "/")
                {
                    CookieCollection cookiesSistema = handler.CookieContainer.GetCookies(new Uri("https://www.frc.utn.edu.ar"));
                    if (cookiesSistema.Count == 7)
                    {
                        client.DefaultRequestHeaders.Add(
                            "User-Agent",
                            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 Chrome/120 Safari/537.36"
                        );
                        client.DefaultRequestHeaders.Add("Accept", "text/html");

                        string bodyHtml;
                        //Legajos numericos son de estudiantes buscamos su nombre completo
                        if (int.TryParse(legajo, out int _))
                        {
                            bodyHtml = await (await client.GetAsync("https://a4.frc.utn.edu.ar/4/")).Content.ReadAsStringAsync();
                            Match match = Regex.Match(bodyHtml, @"var\s+nombreCompleto\s*=\s*'([^']*)'");

                            if (match.Success)
                                return match.Groups[1].Value;
                            else return "ERROR";
                        }
                        else
                        {
                            //A los docentes los redirige aca 
                            bodyHtml = await (await client.GetAsync("https://www.frc.utn.edu.ar/academico3/")).Content.ReadAsStringAsync(); ;

                            // 1. Extraer el form específico
                            var match = Regex.Match(bodyHtml,
                                @"<form[^>]*id\s*=\s*[""']frmLogOut[""'][^>]*>.*?<strong>(.*?)</strong>.*?</form>",
                                RegexOptions.Singleline | RegexOptions.IgnoreCase);

                            if (match.Success)
                                return match.Groups[1].Value;
                            else return "ERROR";
                        }

                    }
                        
                    else
                    {
                        Logger.RegistrarDatos(Logger.LogOptions.Alerta, Request.Path, "Creo sesion correctamente pero no devolvio el numero correcto de cookies", ControllerName);
                        return "ERROR";
                    }
                }
                //Las credenciales fallaron
                else return "ERROR";
            }
            catch (Exception)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, Request.Path, "ERROR CREANDO LAS CREDENCIALES A4", ControllerName);
                return "ERROR";
            }
        }
        private static int? ValidarDominio(string dominio)
        {
            return dominio switch
            {
                "sistemas" => 5,
                "electrica" => 7,
                "electronica" => 9,
                "mecanica" => 17,
                "metalurgica" => 18,
                "quimica" => 21,
                "industrial" => 24,
                "civil" => 31,
                _ => null,
            };
        }

        /// <summary>
        /// Genera un JWT.
        /// </summary>
        /// <param name="legajo">Es un legajo tanto de alumno, docente o no docente en la UTN FRC </param>
        /// <param name="password">Es la contraseña correspondiente a dicho usuario, debe estar encriptada </param>
        /// <param name="dominio">Es el area a la cual se asigna el legajo, sistemas, electronica, decanato, etc </param>
        /// <returns> Devuelve un Jason Web Token para utilizar en la sesion.</returns>
        /// <remarks>
        /// Para utilizarlo legajo no debe ser nulo y ademas tenemos que tener un usuario en A4 y esta aplicacion. 
        ///
        /// NOTA: Una vez generado el JWT tiene vigencia por 30 minutos y debe ser creado nuevamente
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Usuarios/ObtenerTokenJWT/{legajo}/{dominio}/{password}
        ///     
        ///     RESPONSE:
        ///     {
        ///         "token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI4MzQ2NCw1LDAiLCJuYmYiOjE3MTY5MDMzODU",
        ///          "legajo_armado": "string",
        ///          "nombre_usuario": "string",
        ///          id_perfil: 0,
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve un JWT que permite usar la aplicacion </response>
        /// <response code="204" >El usuario no esta registrado en la aplicacion </response>
        /// <response code="401" >El usuario no introdujo crendeciales validas en A4 </response>
        /// <response code="409" >Ocurre un error en la creacion de la sesión o en la lectura del AppSettings </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{legajo}/{dominio}/{password}")]
        [ActionName("ObtenerTokenJWT")]
        [ProducesResponseType(typeof(JWToken), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<JWToken> ObtenerTokenJWT(string legajo, string dominio,string password)
        {
            // A desarrollar: Mediante SP recupera el Usuario que esta logueado y su perfil de usuario, eso lo usamos para dejarlo guardado en cada consulta
            // Ademas renovamos su sesion todas las veces que utiliza un EndPoint (Cuando averigue como).

            //string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
            if (SecretKey == "ERROR") Conflict();

            //Estoy haciendo basicamente scrapping para corroborar que pusieron bien las credenciales.

            //string nombreCompleto = "Bergesio Genaro Rafael";
            string nombreCompleto = CheckSessionA4(legajo, password, dominio).Result;
            //Verifica que nuestras credenciales son validas en autogestion
            if (nombreCompleto != "ERROR")
            {
                //Esto es porque los legajos de las personas no son simplemente el numero o las letras sino que se incluye el dominio
                string legajoArmado = legajo + "@" + ((dominio!="frc")? dominio+"." : "") + "frc.utn.edu.ar";
                //Si esta persona no existe en nuestro sistema debemos crear su registro
                Usuarios? usr = UserAdapter.BuscarUsuarioActivoXLegajo(legajoArmado);
                if (usr != null && usr.legajo != "" && usr.id_perfil != -1 && usr.id != -1)
                    return CrearToken(legajoArmado, usr);
                //Usuario no encontrado
                else
                {
                    int? id_especialidad = ValidarDominio(dominio);
                    // Solo cargamos a los estudiantes, docentes y demas deben ser cargados a mano como usuarios
                    if (int.TryParse(legajo,out int _) && id_especialidad != null)
                    {
                        usr = UserAdapter.CrearEstudiante(legajoArmado, nombreCompleto,"", (int)id_especialidad);
                        if (usr != null && usr.legajo != "" && usr.id_perfil != -1 && usr.id != -1)
                            return CrearToken(legajoArmado, usr);
                        else return Conflict();
                    }
                    //Probamos insertandolo en nuestra BD
                    else return NoContent();
                }
            }
            else return Unauthorized();
        }
        /// <summary>
        /// Le da extension a nuestro Token
        /// </summary>
        /// <returns>Un nuevo Token con la sesion extendida</returns>
        /// <remarks>
        /// Este endpoint se utiliza para extender la sesion de un usuario que ya tiene un JWT valido, lo que hace es generar un nuevo JWT con una nueva fecha de expiracion y el mismo contenido. Es necesario que el usuario tenga un JWT valido para poder acceder a este endpoint, ya que se utiliza la informacion del JWT para generar el nuevo token. De esta manera, si el usuario esta utilizando la aplicacion y su token esta por expirar, puede llamar a este endpoint para obtener un nuevo token sin necesidad de volver a ingresar sus credenciales en A4.
        ///
        /// NOTA: Una vez generado el JWT tiene vigencia por 30 minutos y debe ser creado nuevamente
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Usuarios/ObtenerTokenJWT/{legajo}/{dominio}/{password}
        ///     
        ///     RESPONSE:
        ///     {
        ///         "token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI4MzQ2NCw1LDAiLCJuYmYiOjE3MTY5MDMzODU",
        ///          "legajo_armado": "string",
        ///          "nombre_usuario": "string",
        ///          id_perfil: 0,
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve un JWT que permite usar la aplicacion </response>
        /// <response code="401" >El usuario no introdujo crendeciales validas en A4 </response>
        /// <response code="409" >Ocurre un error en la extension y el usuario no existe </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(JWToken), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<JWToken> ExtenderSesion()
        {
            string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
            //Comprueba que tengamos un usuario autorizado a los cambios y que lo podamos registrar
            if (userData != null &&
                userData.Length > 0 &&
                userData != "NO DATA" &&
                int.TryParse(userData.Split(',')[2], out int userID))
            {
                Usuarios? us = UserAdapter.BuscarUsuarioXID(userID);
                if(us != null && us.legajo != "" && us.id_perfil != -1 && us.id != -1)
                    return CrearToken(us.legajo, us);
                else return Conflict();
            }
            else return Unauthorized();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="legajoArmado"></param>
        /// <param name="usr"></param>
        /// <returns></returns>
        private ActionResult<JWToken> CrearToken(string legajoArmado,Usuarios usr)
        {
            string ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";
            if (ip != null && ip != "" && UserAdapter.CrearSesionUsuario(usr.id))
            {
                Logger.RegistrarDatos(Logger.LogOptions.IP, this.Request.Path, "IP: " + ip, ControllerName);
                byte[] keyBytes = Encoding.UTF8.GetBytes(SecretKey);
                ClaimsIdentity claims = new();
                //Guardamos todos los datos necesarios para operar mas adelante
                string claimValue = legajoArmado + "," + usr.id_perfil + "," + usr.id;
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, claimValue));

                SecurityTokenDescriptor tokenDescriptor = new()
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature),
                };
                JwtSecurityTokenHandler tokenHandler = new();
                SecurityToken TokenConfig = tokenHandler.CreateToken(tokenDescriptor);
                string ActualToken = tokenHandler.WriteToken(TokenConfig);
                JWToken token = new(ActualToken,legajoArmado,usr.nombre_usuario,usr.id_perfil);
                return Created("/api/Usuarios/ObtenerTokenJWT/", token);
            }
            else
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, this.Request.Path, "ERROR AL CREAR LA SESION EN LA BASE DE DATOS. ACCESO NO AUTORIZADO", ControllerName);
                return Conflict();
            }
        }

        /// <summary>
        /// Listado completo de Usuarios
        /// </summary>
        /// <returns>Un listado de todos los usuarios actuales de la aplicacion</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Usuarios/ObtenerUsuarios/
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///             "id": 0,
        ///             "legajo": "string",
        ///             "nombre_usuario": "string",
        ///             "id_perfil": 0,
        ///             "activo": true 
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado de Todos los Usuarios </response>
        /// <response code="204" >No se pudo recuperar el listado de usuarios </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [Authorize]
        [ActionName("ObtenerUsuarios")]
        [ProducesResponseType(typeof(IEnumerable<Usuarios>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Usuarios>> ObtenerUsuarios()
        {
            try
            {
                //El numero de funcion es: 9
                if (TienePermiso(9))
                {
                    //Si es null es que ocurrio un conflicto en BD.
                    List<Usuarios>? usuarios = UserAdapter.ObtenerUsuariosCompleto();

                    if(usuarios == null) return Conflict();
                    if (usuarios.Count == 0) return NoContent();

                    return Ok(usuarios);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error,this.Request.Path, ex.Message, ControllerName);
                return BadRequest();
            }
        }
        /// <summary>
        /// Busca usuarios por su ID
        /// </summary>
        /// <param name="id">Es el identificacor del usuario en la BD</param>
        /// <returns>Un usuario que coincida con ese ID</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Usuarios/ObtenerUsuarioXId/{id}
        ///     
        ///     RESPONSE:
        ///     {
        ///         "id": 0,
        ///         "legajo": "string",
        ///         "nombre_usuario": "string",
        ///         "id_perfil": 0,
        ///         "activo": true 
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un usuario con este ID </response>
        /// <response code="204" >No se encontro ningun usuario </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{id}")]
        [Authorize]
        [ActionName("ObtenerUsuarioXId")]
        [ProducesResponseType(typeof(Usuarios), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Usuarios> ObtenerUsuarioXId(int id)
        {
            try
            {
                if (TienePermiso(10))
                {
                    Usuarios? user = UserAdapter.BuscarUsuarioXID(id);

                    if (user == null) return Conflict();
                    if (user.id != -1) return Ok(user);
                    else return NoContent();
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, this.Request.Path, ex.Message, ControllerName);
                return BadRequest();
            }
        }
        /// <summary>
        /// Busca usuarios por su Legajo Academico
        /// </summary>
        /// <param name="legajo">Es un legajo tanto de alumno, docente o no docente en la UTN FRC </param>
        /// <returns>Un usuario que coincida con ese Legajo</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Usuarios/ObtenerUsuarioXId/{legajo}
        ///     
        ///     RESPONSE:
        ///     {
        ///         "id": 0,
        ///         "legajo": "string",
        ///         "nombre_usuario": "string",
        ///         "id_perfil": 0,
        ///         "activo": true 
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un usuario con este Legajo </response>
        /// <response code="204" >No se encontro ningun usuario </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{legajo}")]
        [Authorize]
        [ActionName("ObtenerUsuarioXlegajo")]
        [ProducesResponseType(typeof(Usuarios), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Usuarios> ObtenerUsuarioXlegajo(string legajo)
        {
            try
            {
                //El numero de funcion es: 11
                if (TienePermiso(11))
                {
                    Usuarios? user = UserAdapter.BuscarUsuarioXLegajo(legajo);
                    if (user == null) return Conflict();
                    if (user.id != -1) return Ok(user);
                    else return NoContent();
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, this.Request.Path, ex.Message, ControllerName);
                return BadRequest();
            }
        }

        /// <summary>
        /// Permite la modificacion de un usuario
        /// </summary>
        /// <param name="id"> El ID del usuario a modificar</param>
        /// <param name="user"> Los datos modificados del usuario</param>
        /// <returns>Un usuario modificado en BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     PUT /api/Usuarios/ModificarUsuario/{id}
        ///     BODY:
        ///     {
        ///         "id": 0,
        ///         "legajo": "string",
        ///         "nombre_usuario": "string",
        ///         "id_perfil": 0,
        ///         "activo": true,
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///         "id": 0,
        ///         "legajo": "string",
        ///         "nombre_usuario": "string",
        ///         "id_perfil": 0,
        ///         "activo": true 
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve el usuario modificado en BD </response>
        /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del usuario a modificar </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{id}")]
        [Authorize]
        [ActionName("ModificarUsuario")]
        [ProducesResponseType(typeof(Usuarios), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Usuarios> ModificarUsuario(int id, [FromBody, Required] Usuarios user)
        {
            try
            {
                if (id != user.id) return BadRequest();
                //El numero de funcion es: 12
                if (TienePermiso(12))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    //Comprueba que tengamos un usuario autorizado a los cambios y que lo podamos registrar
                    if (userData != null &&
                        userData.Length > 0 &&
                        userData != "NO DATA" &&
                        int.TryParse(userData.Split(',')[2], out int idUserMod))
                    {
                        user = UserAdapter.ModificarUsuario(user, idUserMod);

                        if (user.id != -1) return Ok(user);
                        else return Conflict();
                    }
                    else return Unauthorized();
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, this.Request.Path, ex.Message, ControllerName);
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite crear Usuarios para la aplicacion
        /// </summary>
        /// <param name="user">El usuario que deseamos crear, se envia en el Body</param>
        /// <returns>Un usuario creado en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Usuarios/CrearUsuario/
        ///     BODY:
        ///     {
        ///         "id": 0,
        ///         "legajo": "string",
        ///         "nombre_usuario": "string",
        ///         "id_perfil": 0,
        ///         "activo": true 
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///         "id": 0,
        ///         "legajo": "string",
        ///         "nombre_usuario": "string",
        ///         "id_perfil": 0,
        ///         "activo": true 
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve el usuario creado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost]
        [Authorize]
        [ActionName("CrearUsuario")]
        [ProducesResponseType(typeof(Usuarios), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Usuarios> CrearUsuario([FromBody] Usuarios user)
        {
            try
            {
                if (TienePermiso(13))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    //Comprueba que tengamos un usuario autorizado a los cambios y que lo podamos registrar
                    if (userData != null &&
                        userData.Length > 0 &&
                        userData != "NO DATA" &&
                        int.TryParse(userData.Split(',')[2], out int idUserCrea))
                    {
                        user = UserAdapter.CrearUsuario(user, idUserCrea);
                        if (user.id != -1) return Created("Usuario Creado",user);
                        else return Conflict();
                    }
                    else return Unauthorized();
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, this.Request.Path, ex.Message, ControllerName);
                return BadRequest();
            }
        }
        /// <summary>
        /// Este endpoint crea el usuario y el registro que es necesario en la tabla estudiantesRegistrados. En teoria no ibamos a cargar esa tablas sino que computos nos iba a proveer de un servicio, como pasaron 2 años del pedido decidimos tener nuestros propios registros
        /// </summary>
        /// <param name="nuevoUsuario">El usuario que se utiliza en la aplicacion</param>
        /// <param name="nombres">Los nombres de dicha persona en el sistema academico</param>
        /// <param name="apellidos">Los apellidos de dicha persona en el sistema academico</param>
        /// <param name="id_especialidad">La especialidad la cual cursa. Puede ser nula en caso de empleados</param>
        /// <returns>Un usuario creado en la base de datos o error</returns>
        /// <remarks></remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        [HttpPost]
        [Authorize]
        [ActionName("CrearRegistroUsuario")]
        [ProducesResponseType(typeof(Usuarios), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Usuarios> CrearRegistroUsuario(
            [FromBody]Usuarios nuevoUsuario,
            [FromQuery] string nombres,
            [FromQuery] string apellidos,
            [FromQuery] int? id_especialidad)
        {
            try
            {
                if (TienePermiso(13))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    //Comprueba que tengamos un usuario autorizado a los cambios y que lo podamos registrar
                    if (userData != null &&
                        userData.Length > 0 &&
                        userData != "NO DATA" &&
                        int.TryParse(userData.Split(',')[2], out int idUserCrea))
                    {
                        nuevoUsuario = UserAdapter.CrearRegistroConUsuario(nuevoUsuario,nombres,apellidos, id_especialidad, idUserCrea);
                        if (nuevoUsuario.id != -1) return Created("Usuario Creado", nuevoUsuario);
                        else return Conflict();
                    }
                    else return Unauthorized();
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, this.Request.Path, ex.Message, ControllerName);
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite validar si el perfil tiene permiso en la BD para ejecutar este endpoint
        /// </summary>
        /// <param name="id_funcion">Es la funcion que queremos validar </param>
        /// <returns> True = Tiene permisos || False = No tiene permisos </returns>
        private bool TienePermiso(int id_funcion)
        {
            string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
            if (userData == null || userData == "NO DATA") return false;
            if(int.TryParse(userData.Split(',')[1], out int id_perfil)) return UserAdapter.TienePermiso(id_funcion, id_perfil);
            else return false;
        }
    }
}
