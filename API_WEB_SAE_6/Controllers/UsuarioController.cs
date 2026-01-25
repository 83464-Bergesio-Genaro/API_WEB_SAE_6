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
using API_WEB_SAE_6.Tools;
using API_WEB_SAE_6.Adapters;


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

        public ActionResult<IEnumerable<JWToken>> ObtenerTokenJWT(string legajo)
        {
            // A desarrollar: Mediante SP recupera el Usuario que esta logueado y su perfil de usuario, eso lo usamos para dejarlo guardado en cada consulta
            // Ademas renovamos su sesion todas las veces que utiliza un EndPoint (Cuando averigue como).

            //string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
            if (SecretKey == "ERROR") Conflict();
            string method = "ObtenerTokenJWT";
            Usuarios? usr = UserAdapter.BuscarUsuarioActivoXLegajo(legajo);
            if (usr != null && usr.legajo != "" && usr.id_perfil != -1 && usr.id != -1)
            {
                string ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "";
                if (ip != null && ip != "" && UserAdapter.CrearSesionUsuario(usr.id))
                {
                    Logger.RegistrarDatos(Logger.LogOptions.IP, method,"IP: "+ip, ControllerName);
                    byte[] keyBytes = Encoding.UTF8.GetBytes(SecretKey);
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
                    JwtSecurityTokenHandler tokenHandler = new();
                    SecurityToken TokenConfig = tokenHandler.CreateToken(tokenDescriptor);
                    string ActualToken = tokenHandler.WriteToken(TokenConfig);
                    JWToken token = new(ActualToken);
                    return Created("/api/Usuarios/ObtenerTokenJWT/", token);
                }
                else
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, method, "ERROR AL CREAR LA SESION EN LA BASE DE DATOS. ACCESO NO AUTORIZADO", ControllerName);
                    return Conflict();
                }
            }
            //Usuario no encontrado
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
        public ActionResult<IEnumerable<Usuarios>> ObtenerUsuarios()
        {
            string method = "ObtenerUsuarios";
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
                Logger.RegistrarDatos(Logger.LogOptions.Error,method, ex.Message, ControllerName);
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
            string method = "ObtenerUsuarioXId";
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
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, ControllerName);
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
            string method = "ObtenerUsuarioXlegajo";
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
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, ControllerName);
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
            string method = "ModificarUsuario";
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
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, ControllerName);
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
            string method = "CrearUsuario";
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
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, ControllerName);
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
