using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Microsoft.AspNetCore.Authentication;
using System.Data;
using System.ComponentModel.DataAnnotations;


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
        private readonly string secretKey;
        /// <summary>
        /// EN: The logger functions as a register of the exception that happen in the runtime. <br/>
        /// ES: El logger funciona como el registro de excpciones que pasan en tiempo de ejecuccion <br/>
        /// </summary>
        private readonly Logger _logger = new();


        private readonly IConfiguration _config;
        /// <summary>
        /// Recupera el contexto desde el archivo API_WEB_SAEContext <br/>
        /// </summary>
        /// <param name="config">
        /// Es el acceso a la base de datos con las credenciales de owner <br/>
        /// </param>
        public UsuariosController( IConfiguration config)
        {

            _config = config;
            //Funciona asi y no recuperando la clave correcta, desconozco el origen del problema
            secretKey = config.GetSection("Settings").GetSection("secretkey").ToString() ?? "ERROR";
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
        public async Task<ActionResult<IEnumerable<string>>> MockResponse()
        {
            return Ok
                ("Hello World! V3");
        }
        /// <summary>
        /// Genera un JWT.
        /// </summary>
        /// <param name="legajo">Es un legajo tanto de alumno, docente o no docente en la UTN FRC </param>
        /// <returns> Devuelve un Jason Web Token para utilizar en la sesion.</returns>
        /// <remarks>
        /// Para utilizarlo legajo no debe ser nulo y ademas tenemos que tener un usuario en A4 y esta aplicacion. 
        ///
        /// NOTA: Una vez generado el JWT tiene vigencia por 30 minutos y debe ser creado nuevamente
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Usuarios/ObtenerTokenJWT/{legajo}
        ///     
        ///     RESPONSE:
        ///     {
        ///         "token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI4MzQ2NCw1LDAiLCJuYmYiOjE3MTY5MDMzODU"
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve un JWT que permite usar la aplicacion </response>
        /// <response code="204" >El usuario no esta registrado en la aplicacion </response>
        /// <response code="409" >Ocurre un error en la creacion de la sesión o en la lectura del AppSettings </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{legajo}")]
        [ActionName("ObtenerTokenJWT")]
        [ProducesResponseType(typeof(JWToken), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<IEnumerable<JWToken>>> ObtenerTokenJWT(string legajo)
        {
            // A desarrollar: Mediante SP recupera el Usuario que esta logueado y su perfil de usuario, eso lo usamos para dejarlo guardado en cada consulta
            // Ademas renovamos su sesion todas las veces que utiliza un EndPoint (Cuando averigue como).

            //string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
            if (secretKey == "ERROR") Conflict();

            Usuarios usr = await BuscarUsuarioActivoXLegajo(legajo);
            if (usr != null && usr.legajo != null && usr.id_perfil != null && usr.id != null)
            {
                string ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";
                if (ip != null && ip != "" && await CrearSesionUsuario(usr.id))
                {
                    _logger.RegistrarIP(legajo, ip);
                    byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
                    ClaimsIdentity claims = new();
                    //Guardamos todos los datos necesarios para operar mas adelante
                    string claimValue = legajo + "," + usr.id_perfil + "," + usr.id;
                    claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, claimValue));

                    SecurityTokenDescriptor tokenDescriptor = new()
                    {
                        Subject = claims,
                        Expires = DateTime.UtcNow.AddMinutes(30),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature),
                    };
                    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                    SecurityToken TokenConfig = tokenHandler.CreateToken(tokenDescriptor);
                    string ActualToken = tokenHandler.WriteToken(TokenConfig);
                    JWToken token = new(ActualToken);
                    return Created("/api/Usuarios/ObtenerTokenJWT/", token);
                }
                else
                {
                    _logger.RegistrarERROR(new("ERROR AL CREAR LA SESION EN LA BASE DE DATOS. ACCESO NO AUTORIZADO"), "No se pudo crear Sesion");
                    return Conflict();
                }
            }
            else return NoContent();
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
        public async Task<ActionResult<IEnumerable<Usuarios>>> ObtenerUsuarios()
        {
            try
            {
                //El numero de funcion es: 9
                if (await TienePermiso(9))
                {
                    DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_USUARIOS_Vista_Usuarios");
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                    List<Usuarios> usuarios = new();
                    foreach (DataRow row in respuesta.Rows)
                    {
                        Usuarios user = new(row);
                        usuarios.Add(user);
                    }
                    return Ok(usuarios);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "PRUEBA");
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
        public async Task<ActionResult<Usuarios>> ObtenerUsuarioXId(int id)
        {
            try
            {
                if (await TienePermiso(10))
                {
                    Dictionary<string, string> parametros = new()
                    {
                        { "@id_usuario", id.ToString() }
                    };
                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_USUARIOS_Buscar_Usuarios_Id", parametros);
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                    else return Ok(new Usuarios(respuesta.Rows[0]));
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR BUSCANDO USUARIO");
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
        public async Task<ActionResult<Usuarios>> ObtenerUsuarioXlegajo(string legajo)
        {
            try
            {
                //El numero de funcion es: 11
                if (await TienePermiso(11))
                {
                    Dictionary<string, string> parametros = new()
                    {
                        {"@legajo",legajo }
                    };
                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_USUARIOS_Buscar_Usuarios_Legajo", parametros);
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                    else return Ok(new Usuarios(respuesta.Rows[0]));
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR BUSCANDO USUARIO");
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
        public async Task<ActionResult<Usuarios>> ModificarUsuario(int id, [FromBody, Required] Usuarios user)
        {
            try
            {
                if (id != user.id) return BadRequest();
                //El numero de funcion es: 12
                if (await TienePermiso(12))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    string usuarioActual = userData.Split(',')[2];
                    Dictionary<string, string> parametros = new() {
                        { "@id_usuario",user.id.ToString() },
                        { "@legajo", user.legajo },
                        { "@nombre_usuario", user.nombre_usuario },
                        { "@id_perfil", user.id_perfil.ToString() },
                        { "@activo",user.activo.ToString() },
                        { "@id_usuario_mod",usuarioActual.ToString() }
                    };

                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_USUARIOS_Modificar_Usuario", parametros);

                    //En este caso sino modifica nada es un conflicto en la BD
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                    return Ok(user);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR MODIFICANDO USUARIO");
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
        public async Task<ActionResult<Usuarios>> CrearUsuario([FromBody] Usuarios user)
        {
            try
            {
                if (await TienePermiso(13))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        string usuarioActual = userData.Split(',')[2];
                        Dictionary<string, string> parametros = new() {
                            { "@legajo", user.legajo },
                            { "@nombre_usuario", user.nombre_usuario },
                            { "@id_perfil", user.id_perfil.ToString() },
                            {"@id_usuario_alta",usuarioActual.ToString() }
                        };

                        DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_USUARIOS_Crear_Usuario", parametros);
                        //En este caso sino crea es un error en la BD
                        if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                        return Created("Usuario Creado", user);
                    }

                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR CREANDO USUARIO");
                return BadRequest();
            }
        }
        /// <summary>
        /// Este metodo crea una nueva sesion de Usuario para tener registro de las actividades de cada uno
        /// </summary>
        /// <param name="id_usuario">Es el IF de usuario que tiene en la aplicacion</param>
        /// <returns> True = Crea la sesion || False = Ocurre un error en la creacion de la sesion</returns>
        private async Task<bool> CrearSesionUsuario(int id_usuario)
        {
            try
            {
                Dictionary<string, string> parametros = new()
                {
                    {"@id_usuario", id_usuario.ToString()},
                    {"@fecha_ini", DateTime.UtcNow.ToString()},
                    {"@fecha_fin",DateTime.UtcNow.AddMinutes(30).ToString()}
                };

                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_USUARIOS_Crear_Sesion", parametros);

                if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return false;
                else return true;
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR CREANDO LA SESION");
                return false;
            }

        }
        /// <summary>
        /// Es un endpoint oculto que sirve para validar la existencia de ese legajo en nuestra BD
        /// </summary>
        /// <param name="legajo">Legajo activo en la base y la aplicacion</param>
        /// <returns>Un usuario existente en nuestra base o error</returns>
        private async Task<Usuarios> BuscarUsuarioActivoXLegajo(string legajo)
        {
            try
            {
                //MODULO_USUARIOS_Buscar_Usuario_Activo_Legajo
                Dictionary<string, string> parametros = new()
                {
                    {"@legajo",legajo }
                };
                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_USUARIOS_Buscar_Usuario_Activo_Legajo", parametros);
                if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                else return new(respuesta.Rows[0]);
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR BUSCANDO USUARIO");
                return new();
            }
        }
        /// <summary>
        /// Permite validar si el perfil tiene permiso en la BD para ejecutar este endpoint
        /// </summary>
        /// <param name="id_funcion">Es la funcion que queremos validar </param>
        /// <returns> True = Tiene permisos || False = No tiene permisos </returns>
        private async Task<bool> TienePermiso(int id_funcion)
        {
            string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
            if (userData == null || userData == "NO DATA") return false;

            int id_perfil;
            try { id_perfil = int.Parse(userData.Split(',')[1]); }
            catch (Exception) { return false; }

            PerfilesController p = new();
            return await p.TienePermiso(_config, id_perfil, id_funcion);
        }
    }
}
