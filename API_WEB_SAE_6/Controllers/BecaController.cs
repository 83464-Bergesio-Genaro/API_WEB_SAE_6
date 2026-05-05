using API_WEB_SAE_6.Adapters;
using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace API_WEB_SAE_6.Controllers
{
    /// <summary>
    /// Es el controlador para los becarios del area y nacionales
    /// </summary>
    [EnableCors("CorsRules")]
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class BecaController : Controller
    {
        /// <summary>
        /// Es el adaptador de usuarios para consultar los permisos
        /// </summary>
        private UsuarioAdapter UserAdapter = new();
        /// <summary>
        /// Es el adaptador con respecto a la base de datos para realizar llamadas
        /// </summary>
        private BecaAdapter BecAdapter = new();
        /// <summary>
        /// 
        /// </summary>
        private readonly string ControllerName = "BecaController";
        /// <summary>
        /// 
        /// </summary>
        public BecaController() { }
        
        /// <summary>
        /// Recupera todos los becarios
        /// </summary>
        /// <returns>Un listado de todos los becarios</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Beca/ObtenerBecariosCompleto/
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "id": 0,
        ///         "legajo": "string",
        ///         "nombre_becario": "string",
        ///         "alquila": true,
        ///         "fecha_solicitud": "2024-07-19T19:48:53.374Z",
        ///         "aceptado_inicio": true,
        ///         "puede_pagarle": true,
        ///         "activo": true,
        ///         "anio_beca": 0,
        ///         "id_becario_previo": 0
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado completo de los becarios </response>
        /// <response code="204" >No se encontro ningun becario </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerBecariosCompleto")]
        [ProducesResponseType(typeof(IEnumerable<BecariosSAE>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<BecariosSAE>> ObtenerBecariosCompleto()
        {
            try
            {
                //El numero de funcion es: 53
                if (TienePermiso(53))
                {
                    List<BecariosSAE>? listBec = BecAdapter.ObtenerBecariosCompleto();

                    if (listBec == null) return Conflict();
                    if (listBec.Count == 0) return NoContent();

                    return Ok(listBec);
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
        /// Recupera todos los becarios activos
        /// </summary>
        /// <returns>Un listado de todos los becarios activos</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Beca/ObtenerBecariosActivos/
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "id": 0,
        ///         "legajo": "string",
        ///         "nombre_becario": "string",
        ///         "alquila": true,
        ///         "fecha_solicitud": "2024-07-19T19:48:53.374Z",
        ///         "aceptado_inicio": true,
        ///         "puede_pagarle": true,
        ///         "activo": true,
        ///         "anio_beca": 0,
        ///         "id_becario_previo": 0
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado completo de los becarios </response>
        /// <response code="204" >No se encontro ningun becario </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerBecariosActivos")]
        [ProducesResponseType(typeof(IEnumerable<BecariosSAE>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<BecariosSAE>> ObtenerBecariosActivos()
        {
            try
            {
                //El numero de funcion es: 53
                if (TienePermiso(53))
                {
                    List<BecariosSAE>? listBec = BecAdapter.ObtenerBecariosActivo();

                    if (listBec == null) return Conflict();
                    if (listBec.Count == 0) return NoContent();

                    return Ok(listBec);
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
        /// Busca becarios por ID
        /// </summary>
        /// <param name="id_becario">Es el identificador del becario en la BD</param>
        /// <returns>Un becario encontrado por ID</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Beca/ObtenerBecariosXId/{id_becario}
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "legajo": "string",
        ///       "nombre_becario": "string",
        ///       "alquila": true,
        ///       "fecha_solicitud": "2024-07-19T19:48:53.374Z",
        ///       "aceptado_inicio": true,
        ///       "puede_pagarle": true,
        ///       "activo": true,
        ///       "anio_beca": 0,
        ///       "id_becario_previo": 0
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un becario </response>
        /// <response code="204" >No se encontro ningun becario </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{id_becario}")]
        [ActionName("ObtenerBecariosXId")]
        [ProducesResponseType(typeof(BecariosSAE), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BecariosSAE> ObtenerBecariosXId(int id_becario)
        {
            try
            {
                //Este lo puede visualizar los becarios
                if (TienePermiso(53))
                {
                    BecariosSAE? becar = BecAdapter.BuscarBecarioXID(id_becario);
                    if (becar == null) return Conflict();
                    if (becar.id == -1) return NoContent();
                    else return Ok(becar);
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
        /// Busca becarios por legajo
        /// </summary>
        /// <param name="legajo">Es el identificador del becario en la FRC</param>
        /// <returns>Un listado de becarios por su legajo</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Beca/ObtenerBecariosXLegajo/{legajo}
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "legajo": "string",
        ///       "nombre_becario": "string",
        ///       "alquila": true,
        ///       "fecha_solicitud": "2024-07-19T19:48:53.374Z",
        ///       "aceptado_inicio": true,
        ///       "puede_pagarle": true,
        ///       "activo": true,
        ///       "anio_beca": 0,
        ///       "id_becario_previo": 0
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un becario </response>
        /// <response code="204" >No se encontro ningun becario </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{legajo}")]
        [ActionName("ObtenerBecariosXLegajo")]
        [ProducesResponseType(typeof(IEnumerable<BecariosSAE>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<BecariosSAE>> ObtenerBecariosXLegajo(string legajo)
        {
            try
            {
                //Este lo puede visualizar los becarios
                if (TienePermiso(53))
                {
                    BecariosSAE? becar = BecAdapter.BuscarBecarioXLegajo(legajo);
                    if (becar == null) return Conflict();
                    if (becar.id == -1) return NoContent();
                    else return Ok(becar);
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
        /// Permite modificar becarios SAE
        /// </summary>
        /// <param name="id">El ID del becario que deseamos modificar, se envia en la URL</param>
        /// <param name="becario">El becario que deseamos modificar, se envia en el Body</param>
        /// <returns>Un becario modificado en BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Beca/ModificarBecarioSAE/{id}
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "legajo": "string",
        ///       "alquila": true,
        ///       "fecha_solicitud": "2024-07-21T17:56:49.671Z",
        ///       "aceptado_inicio": true,
        ///       "puede_pagarle": true,
        ///       "activo": true,
        ///       "anio_beca": 0,
        ///       "id_becario_previo": 0
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "legajo": "string",
        ///       "nombre_becario": "string",
        ///       "alquila": true,
        ///       "fecha_solicitud": "2024-07-21T17:56:49.671Z",
        ///       "aceptado_inicio": true,
        ///       "puede_pagarle": true,
        ///       "activo": true,
        ///       "anio_beca": 0,
        ///       "id_becario_previo": 0
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve el becario modificado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del becario a modificar </response>
        /// <response code="401" >El usuario no genero su JWT</response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{id}")]
        [ActionName("ModificarBecarioSAE")]
        [ProducesResponseType(typeof(BecariosSAE), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BecariosSAE> ModificarBecarioSAE(int id, BecariosSAE becario)
        {
            try
            {
                if (id != becario.id) return BadRequest();

                if (TienePermiso(52))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    //Comprueba que tengamos un usuario autorizado a los cambios y que lo podamos registrar
                    if (userData != null &&
                        userData.Length > 0 &&
                        userData != "NO DATA" &&
                        int.TryParse(userData.Split(',')[2], out int idUserMod))
                    {
                        becario = BecAdapter.ModificarBecario(becario, idUserMod);

                        if (becario.id != -1) return Ok(becario);
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
        /// Permite crear becario SAE
        /// </summary>
        /// <param name="becario">El becario que deseamos crear, se envia en el Body</param>
        /// <returns>Un becario creado en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Beca/CrearBecarioSAE/
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "legajo": "string",
        ///       "alquila": true,
        ///       "fecha_solicitud": "2024-07-21T17:56:49.671Z",
        ///       "activo": true,
        ///       "anio_beca": 0,
        ///       "id_becario_previo": 0
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "legajo": "string",
        ///       "nombre_becario": "string",
        ///       "alquila": true,
        ///       "fecha_solicitud": "2024-07-21T17:56:49.671Z",
        ///       "aceptado_inicio": true,
        ///       "puede_pagarle": true,
        ///       "activo": true,
        ///       "anio_beca": 0,
        ///       "id_becario_previo": 0
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve el becario creado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT</response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost]
        [ActionName("CrearBecarioSAE")]
        [ProducesResponseType(typeof(BecariosSAE), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BecariosSAE> CrearBecarioSAE(BecariosSAE becario)
        {
            try
            {
                if (TienePermiso(51))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    //Comprueba que tengamos un usuario autorizado a los cambios y que lo podamos registrar
                    if (userData != null &&
                        userData.Length > 0 &&
                        userData != "NO DATA" &&
                        int.TryParse(userData.Split(',')[2], out int idUserCrea))
                    {
                        becario = BecAdapter.CrearBecario(becario, idUserCrea);
                        if (becario.id != -1) return Created("Becario Creado", becario);
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
        /// Recupera todos los becarios con modulos economicos
        /// </summary>
        /// <returns>Un listado de todos los becarios</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Beca/ObtenerBecariosEconomica/
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///             "id": 0,
        ///             "entrevista_realizada": true,
        ///             "modulo_asignados": 0,
        ///             "becario": 
        ///             {
        ///                 "id": 0,
        ///                 "legajo": "string",
        ///                 "nombre_becario": "string",
        ///                 "alquila": true,
        ///                 "fecha_solicitud": "2024-07-21T14:50:52.102Z",
        ///                 "aceptado_inicio": true,
        ///                 "puede_pagarle": true,
        ///                 "activo": true,
        ///                 "anio_beca": 0,
        ///                 "id_becario_previo": 0
        ///             }
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado completo de los becarios de economica </response>
        /// <response code="204" >No se encontro ningun becario con modulos en economicas </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerBecariosEconomica")]
        [ProducesResponseType(typeof(IEnumerable<BecariosSAEEconomica>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<BecariosSAEEconomica>> ObtenerBecariosEconomica()
        {
            try
            {
                //El numero de funcion es: 53
                if ( TienePermiso(53))
                {
                    List<BecariosSAEEconomica>? listBec = BecAdapter.ObtenerBecariosEconomica();

                    if (listBec == null) return Conflict();
                    if (listBec.Count == 0) return NoContent();

                    return Ok(listBec);
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
        /// Recupera todos los becarios con modulos economicos activos
        /// </summary>
        /// <returns>Un listado de todos los becarios activos</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Beca/ObtenerBecariosEconomicaActivos/
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///             "id": 0,
        ///             "entrevista_realizada": true,
        ///             "modulo_asignados": 0,
        ///             "becario": 
        ///             {
        ///                 "id": 0,
        ///                 "legajo": "string",
        ///                 "nombre_becario": "string",
        ///                 "alquila": true,
        ///                 "fecha_solicitud": "2024-07-21T14:50:52.102Z",
        ///                 "aceptado_inicio": true,
        ///                 "puede_pagarle": true,
        ///                 "activo": true,
        ///                 "anio_beca": 0,
        ///                 "id_becario_previo": 0
        ///             }
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado completo de los becarios de economica activos </response>
        /// <response code="204" >No se encontro ningun becario con modulos en economicas activos </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerBecariosEconomicaActivos")]
        [ProducesResponseType(typeof(IEnumerable<BecariosSAEEconomica>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<BecariosSAEEconomica>> ObtenerBecariosEconomicaActivos()
        {
            try
            {
                //El numero de funcion es: 53
                if ( TienePermiso(53))
                {
                    List<BecariosSAEEconomica>? listBec = BecAdapter.ObtenerBecariosEconomicaActivos();

                    if (listBec == null) return Conflict();
                    if (listBec.Count == 0) return NoContent();

                    return Ok(listBec);
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
        /// Busca becarios por ID becario
        /// </summary>
        /// <param name="id_becario">Es el identificador del becario en la BD</param>
        /// <returns>Un becario encontrado por ID</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Beca/ObtenerBecariosEconomicaXIdBecario/{id_becario}
        ///     
        ///     RESPONSE:
        ///     {
        ///         "id": 0,
        ///         "entrevista_realizada": true,
        ///         "modulo_asignados": 0,
        ///         "becario": 
        ///         {
        ///             "id": 0,
        ///             "legajo": "string",
        ///             "nombre_becario": "string",
        ///             "alquila": true,
        ///             "fecha_solicitud": "2024-07-21T14:50:52.102Z",
        ///             "aceptado_inicio": true,
        ///             "puede_pagarle": true,
        ///             "activo": true,
        ///             "anio_beca": 0,
        ///             "id_becario_previo": 0
        ///         }
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un becario </response>
        /// <response code="204" >No se encontro ningun becario </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{id_becario}")]
        [ActionName("ObtenerBecariosEconomicaXIdBecario")]
        [ProducesResponseType(typeof(BecariosSAE), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BecariosSAEEconomica> ObtenerBecariosEconomicaXIdBecario(int id_becario)
        {
            try
            {
                //Este lo puede visualizar los becarios
                if ( TienePermiso(53))
                {
                    BecariosSAEEconomica? becar = BecAdapter.BuscarBecarioEconomicaXID(id_becario);
                    if (becar == null) return Conflict();
                    if (becar.id == -1) return NoContent();
                    else return Ok(becar);
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
        /// Busca becarios por ID becario
        /// </summary>
        /// <param name="legajo_estudiante">Es el legajo del estudiante en FRC</param>
        /// <returns>Un becario encontrado por legajo</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Beca/ObtenerBecariosEconomicaXLegajo/{legajo_estudiante}
        ///     
        ///     RESPONSE:
        ///     {
        ///         "id": 0,
        ///         "entrevista_realizada": true,
        ///         "modulo_asignados": 0,
        ///         "becario": 
        ///         {
        ///             "id": 0,
        ///             "legajo": "string",
        ///             "nombre_becario": "string",
        ///             "alquila": true,
        ///             "fecha_solicitud": "2024-07-21T14:50:52.102Z",
        ///             "aceptado_inicio": true,
        ///             "puede_pagarle": true,
        ///             "activo": true,
        ///             "anio_beca": 0,
        ///             "id_becario_previo": 0
        ///         }
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un becario </response>
        /// <response code="204" >No se encontro ningun becario </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{legajo_estudiante}")]
        [ActionName("ObtenerBecariosEconomicaXLegajo")]
        [ProducesResponseType(typeof(BecariosSAE), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BecariosSAEEconomica> ObtenerBecariosEconomicaXLegajo(string legajo_estudiante)
        {
            try
            {
                //Este lo puede visualizar los becarios
                if (TienePermiso(53))
                {
                    BecariosSAEEconomica? becar = BecAdapter.BuscarBecarioEconomicaXLegajo(legajo_estudiante);
                    if (becar == null) return Conflict();
                    if (becar.id == -1) return NoContent();
                    else return Ok(becar);
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
        /// Permite modificar becario de beca economica
        /// </summary>
        /// <param name="id">El ID del becario de beca economica</param>
        /// <param name="economica">El becario que deseamos modificar, se envia en el Body</param>
        /// <returns>Un becario modificado en BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Beca/ModificarBecarioEconomica/{id}
        ///     BODY:
        ///     {
        ///         "id": 0,
        ///         "entrevista_realizada": true,
        ///         "modulo_asignados": 0,
        ///         "becario": 
        ///         {
        ///             "id": 0
        ///         }
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///         "id": 0,
        ///         "entrevista_realizada": true,
        ///         "modulo_asignados": 0,
        ///         "becario": 
        ///         {
        ///             "id": 0,
        ///             "legajo": "string",
        ///             "nombre_becario": "string",
        ///             "alquila": true,
        ///             "fecha_solicitud": "2024-07-21T14:50:52.102Z",
        ///             "aceptado_inicio": true,
        ///             "puede_pagarle": true,
        ///             "activo": true,
        ///             "anio_beca": 0,
        ///             "id_becario_previo": 0
        ///         }
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve el becario modificado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del becario a modificar </response>
        /// <response code="401" >El usuario no genero su JWT</response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{id}")]
        [ActionName("ModificarBecarioEconomica")]
        [ProducesResponseType(typeof(BecariosSAEEconomica), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BecariosSAEEconomica> ModificarBecarioEconomica(int id, BecariosSAEEconomica economica)
        {
            try
            {
                if (id != economica.id) return BadRequest();

                if (TienePermiso(52))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    //Comprueba que tengamos un usuario autorizado a los cambios y que lo podamos registrar
                    if (userData != null &&
                        userData.Length > 0 &&
                        userData != "NO DATA" &&
                        int.TryParse(userData.Split(',')[2], out int idUserMod))
                    {
                        economica = BecAdapter.ModificarBecarioEconomica(economica, idUserMod);

                        if (economica.id != -1) return Ok(economica);
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
        /// Permite crear un becario de beca economica
        /// </summary>
        /// <param name="id_becario">El ID del becario  que deseamos crearle una beca economica, se envia en el Body</param>
        /// <returns>Un becario de beca economica creado en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization y debe haber creado el registro del becario antes
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Beca/CrearBecarioEconomica/{"id_becario"}
        ///     
        ///     RESPONSE:
        ///     {
        ///         "id": 0,
        ///         "entrevista_realizada": true,
        ///         "modulo_asignados": 0,
        ///         "becario": 
        ///         {
        ///             "id": 0,
        ///             "legajo": "string",
        ///             "nombre_becario": "string",
        ///             "alquila": true,
        ///             "fecha_solicitud": "2024-07-21T14:50:52.102Z",
        ///             "aceptado_inicio": true,
        ///             "puede_pagarle": true,
        ///             "activo": true,
        ///             "anio_beca": 0,
        ///             "id_becario_previo": 0
        ///         }
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve el becario de beca economica creado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT</response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost("{id_becario}")]
        [ActionName("CrearBecarioEconomica")]
        [ProducesResponseType(typeof(BecariosSAEEconomica), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BecariosSAEEconomica> CrearBecarioEconomica(int id_becario)
        {
            try
            {
                if (TienePermiso(51))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    //Comprueba que tengamos un usuario autorizado a los cambios y que lo podamos registrar
                    if (userData != null &&
                        userData.Length > 0 &&
                        userData != "NO DATA" &&
                        int.TryParse(userData.Split(',')[2], out int idUserCrea))
                    {
                        BecariosSAEEconomica becario = BecAdapter.CrearBecarioEconomica(id_becario, idUserCrea);
                        if (becario.id != -1) return Created("Becario Creado", becario);
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
        /// Recupera todos los becarios con modulos de investigacion
        /// </summary>
        /// <returns>Un listado de todos los becarios</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Beca/ObtenerBecariosInvestigacion/
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///             "id": 0,
        ///             "proyecto_investigacion": 
        ///             {
        ///                 "id": 0,
        ///                 "nombre_proyecto_investigacion": "string",
        ///                 "activo": true,
        ///                 "centro_investigacion": "string"
        ///             },
        ///             "modulos_asignados": 0,
        ///             "becario": 
        ///             {
        ///                 "id": 0,
        ///                 "legajo": "string",
        ///                 "nombre_becario": "string",
        ///                 "alquila": true,
        ///                 "fecha_solicitud": "2024-07-21T16:40:09.230Z",
        ///                 "aceptado_inicio": true,
        ///                 "puede_pagarle": true,
        ///                 "activo": true,
        ///                 "anio_beca": 0,
        ///                 "id_becario_previo": 0
        ///             }
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado completo de los becarios de investigacion </response>
        /// <response code="204" >No se encontro ningun becario con modulos en investigacion </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerBecariosInvestigacion")]
        [ProducesResponseType(typeof(IEnumerable<BecariosSAEInvestigacion>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<BecariosSAEInvestigacion>> ObtenerBecariosInvestigacion()
        {
            try
            {
                //El numero de funcion es: 53
                if ( TienePermiso(53))
                {
                    List<BecariosSAEInvestigacion>? listBec = BecAdapter.ObtenerBecariosInvestigacion();

                    if (listBec == null) return Conflict();
                    if (listBec.Count == 0) return NoContent();

                    return Ok(listBec);
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
        /// Recupera todos los becarios con modulos de investigacion
        /// </summary>
        /// <returns>Un listado de todos los becarios activos</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Beca/ObtenerBecariosInvestigacionActivos/
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///             "id": 0,
        ///             "proyecto_investigacion": 
        ///             {
        ///                 "id": 0,
        ///                 "nombre_proyecto_investigacion": "string",
        ///                 "activo": true,
        ///                 "centro_investigacion": "string"
        ///             },
        ///             "modulos_asignados": 0,
        ///             "becario": 
        ///             {
        ///                 "id": 0,
        ///                 "legajo": "string",
        ///                 "nombre_becario": "string",
        ///                 "alquila": true,
        ///                 "fecha_solicitud": "2024-07-21T16:40:09.230Z",
        ///                 "aceptado_inicio": true,
        ///                 "puede_pagarle": true,
        ///                 "activo": true,
        ///                 "anio_beca": 0,
        ///                 "id_becario_previo": 0
        ///             }
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado completo de los becarios de investigacion activos </response>
        /// <response code="204" >No se encontro ningun becario con modulos en investigacion activos </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerBecariosInvestigacionActivos")]
        [ProducesResponseType(typeof(IEnumerable<BecariosSAEInvestigacion>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<BecariosSAEInvestigacion>> ObtenerBecariosInvestigacionActivos()
        {
            try
            {
                //El numero de funcion es: 53
                if ( TienePermiso(53))
                {
                    List<BecariosSAEInvestigacion>? listBec = BecAdapter.ObtenerBecariosInvestigacionActivos();

                    if (listBec == null) return Conflict();
                    if (listBec.Count == 0) return NoContent();

                    return Ok(listBec);
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
        /// Busca becarios por ID becario
        /// </summary>
        /// <param name="id_becario">Es el identificador del becario en la BD</param>
        /// <returns>Un becario encontrado por ID</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Beca/ObtenerBecariosInvestigacionXIdBecario/{id_becario}
        ///     
        ///     RESPONSE:
        ///     {
        ///         "id": 0,
        ///         "proyecto_investigacion": 
        ///         {
        ///             "id": 0,
        ///             "nombre_proyecto_investigacion": "string",
        ///             "activo": true,
        ///             "centro_investigacion": "string"
        ///         },
        ///         "modulos_asignados": 0,
        ///         "becario": 
        ///         {
        ///             "id": 0,
        ///             "legajo": "string",
        ///             "nombre_becario": "string",
        ///             "alquila": true,
        ///             "fecha_solicitud": "2024-07-21T16:40:09.230Z",
        ///             "aceptado_inicio": true,
        ///             "puede_pagarle": true,
        ///             "activo": true,
        ///             "anio_beca": 0,
        ///             "id_becario_previo": 0
        ///         }
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un becario </response>
        /// <response code="204" >No se encontro ningun becario </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{id_becario}")]
        [ActionName("ObtenerBecariosInvestigacionXIdBecario")]
        [ProducesResponseType(typeof(BecariosSAEInvestigacion), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BecariosSAEInvestigacion> ObtenerBecariosInvestigacionXIdBecario(int id_becario)
        {
            try
            {
                //Este lo puede visualizar los becarios
                if ( TienePermiso(53))
                {
                    BecariosSAEInvestigacion? becar = BecAdapter.BuscarBecarioInvestigacionXID(id_becario);
                    if (becar == null) return Conflict();
                    if (becar.id == -1) return NoContent();
                    else return Ok(becar);
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
        /// Busca becarios por legajo
        /// </summary>
        /// <param name="legajo_estudiante">Es el legajo del estudiante en FRC</param>
        /// <returns>Un becario encontrado por ID</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Beca/ObtenerBecariosInvestigacionXLegajo/{legajo_estudiante}
        ///     
        ///     RESPONSE:
        ///     {
        ///         "id": 0,
        ///         "proyecto_investigacion": 
        ///         {
        ///             "id": 0,
        ///             "nombre_proyecto_investigacion": "string",
        ///             "activo": true,
        ///             "centro_investigacion": "string"
        ///         },
        ///         "modulos_asignados": 0,
        ///         "becario": 
        ///         {
        ///             "id": 0,
        ///             "legajo": "string",
        ///             "nombre_becario": "string",
        ///             "alquila": true,
        ///             "fecha_solicitud": "2024-07-21T16:40:09.230Z",
        ///             "aceptado_inicio": true,
        ///             "puede_pagarle": true,
        ///             "activo": true,
        ///             "anio_beca": 0,
        ///             "id_becario_previo": 0
        ///         }
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un becario </response>
        /// <response code="204" >No se encontro ningun becario </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{legajo_estudiante}")]
        [ActionName("ObtenerBecariosInvestigacionXLegajo")]
        [ProducesResponseType(typeof(BecariosSAEInvestigacion), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BecariosSAEInvestigacion> ObtenerBecariosInvestigacionXLegajo(string legajo_estudiante)
        {
            try
            {
                //Este lo puede visualizar los becarios
                if (TienePermiso(53))
                {
                    BecariosSAEInvestigacion? becar = BecAdapter.BuscarBecarioInvestigacionXLegajo(legajo_estudiante);
                    if (becar == null) return Conflict();
                    if (becar.id == -1) return NoContent();
                    else return Ok(becar);
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
        /// Permite modificar becario investigacion
        /// </summary>
        /// <param name="id">El ID del becario investigacion</param>
        /// <param name="investiga">El becario que deseamos modificar, se envia en el Body</param>
        /// <returns>Un becario modificado en BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Beca/ModificarBecarioInvestigacion/{id}
        ///     BODY:
        ///     {
        ///         "id": 0,
        ///         "proyecto_investigacion": 
        ///         {
        ///             "id": 0,
        ///         },
        ///         "modulos_asignados": 0,
        ///         "becario": 
        ///         {
        ///             "id": 0,
        ///         }
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///         "id": 0,
        ///         "proyecto_investigacion": 
        ///         {
        ///             "id": 0,
        ///             "nombre_proyecto_investigacion": "string",
        ///             "activo": true,
        ///             "centro_investigacion": "string"
        ///         },
        ///         "modulos_asignados": 0,
        ///         "becario": 
        ///         {
        ///             "id": 0,
        ///             "legajo": "string",
        ///             "nombre_becario": "string",
        ///             "alquila": true,
        ///             "fecha_solicitud": "2024-07-21T16:40:09.230Z",
        ///             "aceptado_inicio": true,
        ///             "puede_pagarle": true,
        ///             "activo": true,
        ///             "anio_beca": 0,
        ///             "id_becario_previo": 0
        ///         }
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve el becario modificado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del becario a modificar </response>
        /// <response code="401" >El usuario no genero su JWT</response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{id}")]
        [ActionName("ModificarBecarioInvestigacion")]
        [ProducesResponseType(typeof(BecariosSAEInvestigacion), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BecariosSAEInvestigacion> ModificarBecarioInvestigacion(int id, [FromBody] BecariosSAEInvestigacion investiga)
        {
            try
            {
                if (id != investiga.id) return BadRequest();

                if (TienePermiso(52))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    //Comprueba que tengamos un usuario autorizado a los cambios y que lo podamos registrar
                    if (userData != null &&
                        userData.Length > 0 &&
                        userData != "NO DATA" &&
                        int.TryParse(userData.Split(',')[2], out int idUserMod))
                    {
                        investiga = BecAdapter.ModificarBecarioInvestigacion(investiga, idUserMod);

                        if (investiga.id != -1) return Ok(investiga);
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
        /// Permite crear un becario de beca de investigacion
        /// </summary>
        /// <param name="id_becario">El ID del becario  que deseamos crearle una beca de investigacion, se envia en el Body</param>
        /// <returns>Un becario de beca de investigacion creado en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization y debe haber creado el registro del becario antes
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Beca/CrearBecarioInvestigacion/{"id_becario"}
        ///     
        ///     RESPONSE:
        ///     {
        ///         "id": 0,
        ///         "proyecto_investigacion": 
        ///         {
        ///             "id": 0,
        ///             "nombre_proyecto_investigacion": "string",
        ///             "activo": true,
        ///             "centro_investigacion": "string"
        ///         },
        ///         "modulos_asignados": 0,
        ///         "becario": 
        ///         {
        ///             "id": 0,
        ///             "legajo": "string",
        ///             "nombre_becario": "string",
        ///             "alquila": true,
        ///             "fecha_solicitud": "2024-07-21T16:40:09.230Z",
        ///             "aceptado_inicio": true,
        ///             "puede_pagarle": true,
        ///             "activo": true,
        ///             "anio_beca": 0,
        ///             "id_becario_previo": 0
        ///         }
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve el becario de beca de investigacion creado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT</response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost("{id_becario}")]
        [ActionName("CrearBecarioInvestigacion")]
        [ProducesResponseType(typeof(BecariosSAEInvestigacion), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BecariosSAEInvestigacion> CrearBecarioInvestigacion(int id_becario)
        {
            try
            {
                if (TienePermiso(51))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    //Comprueba que tengamos un usuario autorizado a los cambios y que lo podamos registrar
                    if (userData != null &&
                        userData.Length > 0 &&
                        userData != "NO DATA" &&
                        int.TryParse(userData.Split(',')[2], out int idUserCrea))
                    {
                        BecariosSAEInvestigacion becario = BecAdapter.CrearBecarioInvestigacion(id_becario, idUserCrea);
                        if (becario.id != -1) return Created("Becario Creado", becario);
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
        /// Recupera todos los becarios con modulos de servicios
        /// </summary>
        /// <returns>Un listado de todos los becarios de servicios</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Beca/ObtenerBecariosServicio/
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "id": 0,
        ///         "servicio": 
        ///         {
        ///             "id": 0,
        ///             "nombre": "string",
        ///             "nro_telefono": 0,
        ///             "nro_interno_telefono": 0,
        ///             "horario_atencion": "string",
        ///             "horario_atencion_real": "string",
        ///             "email_institucional": "string"
        ///         },
        ///         "modulos_asignados": 0,
        ///         "becario": 
        ///         {
        ///           "id": 0,
        ///           "legajo": "string",
        ///           "nombre_becario": "string",
        ///           "alquila": true,
        ///           "fecha_solicitud": "2024-07-21T17:56:49.671Z",
        ///           "aceptado_inicio": true,
        ///           "puede_pagarle": true,
        ///           "activo": true,
        ///           "anio_beca": 0,
        ///           "id_becario_previo": 0
        ///         }
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado completo de los becarios de investigacion </response>
        /// <response code="204" >No se encontro ningun becario con modulos en investigacion </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerBecariosServicio")]
        [ProducesResponseType(typeof(IEnumerable<BecariosSAEServicio>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<BecariosSAEServicio>> ObtenerBecariosServicio()
        {
            try
            {
                //El numero de funcion es: 53
                if ( TienePermiso(53))
                {
                    List<BecariosSAEServicio>? listBec = BecAdapter.ObtenerBecariosServicio();

                    if (listBec == null) return Conflict();
                    if (listBec.Count == 0) return NoContent();

                    return Ok(listBec);
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
        /// Recupera todos los becarios con modulos de servicios activos
        /// </summary>
        /// <returns>Un listado de todos los becarios activos</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Beca/ObtenerBecariosServiciosActivos/
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "id": 0,
        ///         "servicio": 
        ///         {
        ///             "id": 0,
        ///             "nombre": "string",
        ///             "nro_telefono": 0,
        ///             "nro_interno_telefono": 0,
        ///             "horario_atencion": "string",
        ///             "horario_atencion_real": "string",
        ///             "email_institucional": "string"
        ///         },
        ///         "modulos_asignados": 0,
        ///         "becario": 
        ///         {
        ///           "id": 0,
        ///           "legajo": "string",
        ///           "nombre_becario": "string",
        ///           "alquila": true,
        ///           "fecha_solicitud": "2024-07-21T17:56:49.671Z",
        ///           "aceptado_inicio": true,
        ///           "puede_pagarle": true,
        ///           "activo": true,
        ///           "anio_beca": 0,
        ///           "id_becario_previo": 0
        ///         }
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado completo de los becarios de servicios activos </response>
        /// <response code="204" >No se encontro ningun becario con modulos en servicios activos </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerBecariosServiciosActivos")]
        [ProducesResponseType(typeof(IEnumerable<BecariosSAEServicio>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<BecariosSAEServicio>> ObtenerBecariosServiciosActivos()
        {
            try
            {
                //El numero de funcion es: 53
                if ( TienePermiso(53))
                {
                    List<BecariosSAEServicio>? listBec = BecAdapter.ObtenerBecariosServicioActivos();

                    if (listBec == null) return Conflict();
                    if (listBec.Count == 0) return NoContent();

                    return Ok(listBec);
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
        /// Busca becarios por ID becario
        /// </summary>
        /// <param name="id_becario">Es el identificador del becario en la BD</param>
        /// <returns>Un becario encontrado por ID</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Beca/ObtenerBecariosServiciosXIdBecario/{id_becario}
        ///     
        ///     RESPONSE:
        ///     {
        ///         "id": 0,
        ///         "servicio": 
        ///         {
        ///             "id": 0,
        ///             "nombre": "string",
        ///             "nro_telefono": 0,
        ///             "nro_interno_telefono": 0,
        ///             "horario_atencion": "string",
        ///             "horario_atencion_real": "string",
        ///             "email_institucional": "string"
        ///         },
        ///         "modulos_asignados": 0,
        ///         "becario": 
        ///         {
        ///           "id": 0,
        ///           "legajo": "string",
        ///           "nombre_becario": "string",
        ///           "alquila": true,
        ///           "fecha_solicitud": "2024-07-21T17:56:49.671Z",
        ///           "aceptado_inicio": true,
        ///           "puede_pagarle": true,
        ///           "activo": true,
        ///           "anio_beca": 0,
        ///           "id_becario_previo": 0
        ///         }
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un becario </response>
        /// <response code="204" >No se encontro ningun becario </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{id_becario}")]
        [ActionName("ObtenerBecariosServiciosXIdBecario")]
        [ProducesResponseType(typeof(BecariosSAEServicio), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BecariosSAEServicio> ObtenerBecariosServiciosXIdBecario(int id_becario)
        {
            try
            {
                //Este lo puede visualizar los becarios
                if ( TienePermiso(53))
                {
                    BecariosSAEServicio? becar = BecAdapter.BuscarBecarioServicioXID(id_becario);
                    if (becar == null) return Conflict();
                    if (becar.id == -1) return NoContent();
                    else return Ok(becar);
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
        /// Busca becarios por legajo
        /// </summary>
        /// <param name="legajo_estudiante">Es el legajo provisto por FRC</param>
        /// <returns>Un becario encontrado por ID</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Beca/ObtenerBecariosServiciosXLegajo/{legajo_estudiante}
        ///     
        ///     RESPONSE:
        ///     {
        ///         "id": 0,
        ///         "servicio": 
        ///         {
        ///             "id": 0,
        ///             "nombre": "string",
        ///             "nro_telefono": 0,
        ///             "nro_interno_telefono": 0,
        ///             "horario_atencion": "string",
        ///             "horario_atencion_real": "string",
        ///             "email_institucional": "string"
        ///         },
        ///         "modulos_asignados": 0,
        ///         "becario": 
        ///         {
        ///           "id": 0,
        ///           "legajo": "string",
        ///           "nombre_becario": "string",
        ///           "alquila": true,
        ///           "fecha_solicitud": "2024-07-21T17:56:49.671Z",
        ///           "aceptado_inicio": true,
        ///           "puede_pagarle": true,
        ///           "activo": true,
        ///           "anio_beca": 0,
        ///           "id_becario_previo": 0
        ///         }
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un becario </response>
        /// <response code="204" >No se encontro ningun becario </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{legajo_estudiante}")]
        [ActionName("ObtenerBecariosServiciosXLegajo")]
        [ProducesResponseType(typeof(BecariosSAEServicio), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BecariosSAEServicio> ObtenerBecariosServiciosXLegajo(string legajo_estudiante)
        {
            try
            {
                //Este lo puede visualizar los becarios
                if (TienePermiso(53))
                {
                    BecariosSAEServicio? becar = BecAdapter.BuscarBecarioServicioXLegajo(legajo_estudiante);
                    if (becar == null) return Conflict();
                    if (becar.id == -1) return NoContent();
                    else return Ok(becar);
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
        /// Permite modificar becario de servicio
        /// </summary>
        /// <param name="id">El ID del becario de servicio</param>
        /// <param name="servicio">El becario que deseamos modificar, se envia en el Body</param>
        /// <returns>Un becario modificado en BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Beca/ModificarBecarioServicio/{id}
        ///     BODY:
        ///     {
        ///         "id": 0,
        ///         "servicio": 
        ///         {
        ///             "id": 0,
        ///         },
        ///         "modulos_asignados": 0,
        ///         "becario": 
        ///         {
        ///           "id": 0,
        ///         }
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///         "id": 0,
        ///         "servicio": 
        ///         {
        ///             "id": 0,
        ///             "nombre": "string",
        ///             "nro_telefono": 0,
        ///             "nro_interno_telefono": 0,
        ///             "horario_atencion": "string",
        ///             "horario_atencion_real": "string",
        ///             "email_institucional": "string"
        ///         },
        ///         "modulos_asignados": 0,
        ///         "becario": 
        ///         {
        ///           "id": 0,
        ///           "legajo": "string",
        ///           "nombre_becario": "string",
        ///           "alquila": true,
        ///           "fecha_solicitud": "2024-07-21T17:56:49.671Z",
        ///           "aceptado_inicio": true,
        ///           "puede_pagarle": true,
        ///           "activo": true,
        ///           "anio_beca": 0,
        ///           "id_becario_previo": 0
        ///         }
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve el becario modificado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del becario a modificar </response>
        /// <response code="401" >El usuario no genero su JWT</response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{id}")]
        [ActionName("ModificarBecarioServicio")]
        [ProducesResponseType(typeof(BecariosSAEServicio), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BecariosSAEServicio> ModificarBecarioServicio(int id, BecariosSAEServicio servicio)
        {
            try
            {
                if (id != servicio.id) return BadRequest();

                if (TienePermiso(52))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    //Comprueba que tengamos un usuario autorizado a los cambios y que lo podamos registrar
                    if (userData != null &&
                        userData.Length > 0 &&
                        userData != "NO DATA" &&
                        int.TryParse(userData.Split(',')[2], out int idUserMod))
                    {
                        servicio = BecAdapter.ModificarBecarioServicio(servicio, idUserMod);

                        if (servicio.id != -1) return Ok(servicio);
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
        /// Permite crear un becario de beca de servicio
        /// </summary>
        /// <param name="id_becario">El ID del becario que deseamos crearle una beca de servicio, se envia en el Body</param>
        /// <returns>Un becario de beca de servicio creado en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization y debe haber creado el registro del becario antes
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Beca/CrearBecarioServicio/{"id_becario"}
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "servicio": 
        ///       {
        ///           "id": 0,
        ///           "nombre": "string",
        ///           "nro_telefono": 0,
        ///           "nro_interno_telefono": 0,
        ///           "horario_atencion": "string",
        ///           "horario_atencion_real": "string",
        ///           "email_institucional": "string"
        ///       },
        ///       "modulos_asignados": 0,
        ///       "becario": 
        ///       {
        ///         "id": 0,
        ///         "legajo": "string",
        ///         "nombre_becario": "string",
        ///         "alquila": true,
        ///         "fecha_solicitud": "2024-07-21T17:56:49.671Z",
        ///         "aceptado_inicio": true,
        ///         "puede_pagarle": true,
        ///         "activo": true,
        ///         "anio_beca": 0,
        ///         "id_becario_previo": 0
        ///       }
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve el becario de beca de servicio creado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT</response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost("{id_becario}")]
        [ActionName("CrearBecarioServicio")]
        [ProducesResponseType(typeof(BecariosSAEServicio), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BecariosSAEServicio> CrearBecarioServicio(int id_becario)
        {
            try
            {
                if (TienePermiso(51))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    //Comprueba que tengamos un usuario autorizado a los cambios y que lo podamos registrar
                    if (userData != null &&
                        userData.Length > 0 &&
                        userData != "NO DATA" &&
                        int.TryParse(userData.Split(',')[2], out int idUserCrea))
                    {
                        BecariosSAEServicio becario = BecAdapter.CrearBecarioServicio(id_becario, idUserCrea);
                        if (becario.id != -1) return Created("Becario Creado", becario);
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
        /// Listar becarios nacionales
        /// </summary>
        /// <returns>Un listado de todos los becarios nacionales</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Beca/ObtenerBecariosNacionales/
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "id": 0,
        ///         "legajo": "string",
        ///         "nombre_becario": "string",
        ///         "tipo_plan": 0,
        ///         "regularizacion": true,
        ///         "cumplimiento_servicio": true,
        ///         "activo": true
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado de becarios </response>
        /// <response code="204" >No se encontro ningun becario </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerBecariosNacionales")]
        [ProducesResponseType(typeof(IEnumerable<BecariosNacionales>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<BecariosNacionales>> ObtenerBecariosNacionales()
        {
            try
            {
                //Este lo puede visualizar los becarios nacionales
                if ( TienePermiso(145))
                {
                    List<BecariosNacionales>? listBec = BecAdapter.ObtenerBecariosNacionales();

                    if (listBec == null) return Conflict();
                    if (listBec.Count == 0) return NoContent();

                    return Ok(listBec);
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
        /// Busca becarios por ID becario
        /// </summary>
        /// <param name="id_becario">Es el identificador del becario en la BD</param>
        /// <returns>Un becario encontrado por ID</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Beca/ObtenerBecariosServiciosXIdBecario/{id_becario}
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "legajo": "string",
        ///       "nombre_becario": "string",
        ///       "tipo_plan": 0,
        ///       "regularizacion": true,
        ///       "cumplimiento_servicio": true,
        ///       "activo": true
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un becario nacional </response>
        /// <response code="204" >No se encontro ningun becario </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{id_becario}")]
        [ActionName("ObtenerBecariosNacionalesXId")]
        [ProducesResponseType(typeof(BecariosNacionales), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BecariosNacionales> ObtenerBecariosNacionalesXId(int id_becario)
        {
            try
            {
                //Este lo puede visualizar los becarios
                if ( TienePermiso(145))
                {
                    BecariosNacionales? becar = BecAdapter.BuscarBecarioNacionalXID(id_becario);
                    if (becar == null) return Conflict();
                    if (becar.id == -1) return NoContent();
                    else return Ok(becar);
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
        /// Busca becarios por legajo
        /// </summary>
        /// <param name="legajo">Es el identificador del legajo en la BD</param>
        /// <returns>Un becario encontrado por legajo</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Beca/ObtenerBecariosNacionalesXLegajo/{legajo}
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "id": 0,
        ///         "legajo": "string",
        ///         "nombre_becario": "string",
        ///         "tipo_plan": 0,
        ///         "regularizacion": true,
        ///         "cumplimiento_servicio": true,
        ///         "activo": true
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un becario nacional </response>
        /// <response code="204" >No se encontro ningun becario </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{legajo}")]
        [ActionName("ObtenerBecariosNacionalesXLegajo")]
        [ProducesResponseType(typeof(IEnumerable<BecariosNacionales>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<BecariosNacionales>> ObtenerBecariosNacionalesXLegajo(string legajo)
        {
            try
            {
                //Este lo puede visualizar los becarios
                if ( TienePermiso(145))
                {
                    BecariosNacionales? becar = BecAdapter.BuscarBecarioNacionalXLegajo(legajo);
                    if (becar == null) return Conflict();
                    if (becar.id == -1) return NoContent();
                    else return Ok(becar);
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
        /// Permite modificar becario nacional
        /// </summary>
        /// <param name="id">El ID del becario nacional</param>
        /// <param name="nacional">El becario que deseamos modificar, se envia en el Body</param>
        /// <returns>Un becario modificado en BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Beca/ModificarBecarioNacional/{id}
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "legajo": "string",
        ///       "nombre_becario": "string",
        ///       "tipo_plan": 0,
        ///       "regularizacion": true,
        ///       "cumplimiento_servicio": true,
        ///       "activo": true
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "legajo": "string",
        ///       "nombre_becario": "string",
        ///       "tipo_plan": 0,
        ///       "regularizacion": true,
        ///       "cumplimiento_servicio": true,
        ///       "activo": true
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve el becario modificado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del becario a modificar </response>
        /// <response code="401" >El usuario no genero su JWT</response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{id}")]
        [ActionName("ModificarBecarioNacional")]
        [ProducesResponseType(typeof(BecariosNacionales), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BecariosNacionales> ModificarBecarioNacional(int id, BecariosNacionales nacional)
        {
            try
            {
                if (id != nacional.id) return BadRequest();

                if (TienePermiso(146))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    //Comprueba que tengamos un usuario autorizado a los cambios y que lo podamos registrar
                    if (userData != null &&
                        userData.Length > 0 &&
                        userData != "NO DATA" &&
                        int.TryParse(userData.Split(',')[2], out int idUserMod))
                    {
                        nacional = BecAdapter.ModificarBecarioNacional(nacional, idUserMod);

                        if (nacional.id != -1) return Ok(nacional);
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
        /// Permite crear un becario nacional
        /// </summary>
        /// <param name="becario">Es el becario que queremos crear, se envia en el Body</param>
        /// <returns>Un becario nacional creado en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Beca/CrearBecarioNacional/
        ///     
        ///     BODY:
        ///     {
        ///         "id": 0,
        ///         "legajo": "string",
        ///         "tipo_plan": 0,
        ///         "regularizacion": true,
        ///         "cumplimiento_servicio": true,
        ///         "activo": true
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///         "id": 0,
        ///         "legajo": "string",
        ///         "nombre_becario": "string",
        ///         "tipo_plan": 0,
        ///         "regularizacion": true,
        ///         "cumplimiento_servicio": true,
        ///         "activo": true
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve el becario nacional creado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT</response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost]
        [ActionName("CrearBecarioNacional")]
        [ProducesResponseType(typeof(BecariosNacionales), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<BecariosNacionales> CrearBecarioNacional(BecariosNacionales becario)
        {
            try
            {
                if (TienePermiso(56))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    //Comprueba que tengamos un usuario autorizado a los cambios y que lo podamos registrar
                    if (userData != null &&
                        userData.Length > 0 &&
                        userData != "NO DATA" &&
                        int.TryParse(userData.Split(',')[2], out int idUserCrea))
                    {
                        becario = BecAdapter.CrearBecarioNacional(becario, idUserCrea);
                        if (becario.id != -1) return Created("Becario Creado", becario);
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
        /// Listar proyectos de investigacion
        /// </summary>
        /// <returns>Un listado de todos los proyectos</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Beca/ObtenerProyectosInvestigacion/
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "id": 0,
        ///         "nombre_proyecto_investigacion": "string",
        ///         "activo": true,
        ///         "centro_investigacion": "string"
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado de proyectos </response>
        /// <response code="204" >No se encontro ningun proyecto </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerProyectosInvestigacion")]
        [ProducesResponseType(typeof(IEnumerable<ProyectosInvestigacion>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ProyectosInvestigacion>> ObtenerProyectosInvestigacion()
        {
            try
            {
                //Este lo puede visualizar los proyectos
                if ( TienePermiso(143))
                {
                    List<ProyectosInvestigacion>? listProyec = BecAdapter.ObtenerProyectos();

                    if (listProyec == null) return Conflict();
                    if (listProyec.Count == 0) return NoContent();

                    return Ok(listProyec);
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
        /// Permite modificar un proyecto de investigacion
        /// </summary>
        /// <param name="id">El ID del proyecto</param>
        /// <param name="servicio">El proyecto que deseamos modificar, se envia en el Body</param>
        /// <returns>Un proyecto modificado en BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Beca/ModificarProyecto/{id}
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "nombre_proyecto_investigacion": "string",
        ///       "activo": true,
        ///       "centro_investigacion": "string"
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "nombre_proyecto_investigacion": "string",
        ///       "activo": true,
        ///       "centro_investigacion": "string"
        ///     }
        ///       
        /// </remarks>
        /// <response code="200" >Devuelve el proyecto modificado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del proyecto a modificar </response>
        /// <response code="401" >El usuario no genero su JWT</response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{id}")]
        [ActionName("ModificarProyecto")]
        [ProducesResponseType(typeof(ProyectosInvestigacion), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ProyectosInvestigacion> ModificarProyecto(int id, ProyectosInvestigacion servicio)
        {
            try
            {
                if (id != servicio.id) return BadRequest();

                if (TienePermiso(142))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    //Comprueba que tengamos un usuario autorizado a los cambios y que lo podamos registrar
                    if (userData != null &&
                        userData.Length > 0 &&
                        userData != "NO DATA" &&
                        int.TryParse(userData.Split(',')[2], out int idUserMod))
                    {
                        servicio = BecAdapter.ModificarProyecto(servicio);

                        if (servicio.id != -1) return Ok(servicio);
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
        /// Permite crear un proyecto de investigacion
        /// </summary>
        /// <param name="servicioInt">El proyecto de investigacion que deseamos crear, se envia en el Body</param>
        /// <returns>Un proyecto de investigacion creado en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization.
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Beca/CrearProyectoInvestigacion/
        ///     
        ///     BODY:
        ///     {
        ///         "id": 0,
        ///         "nombre_proyecto_investigacion": "string",
        ///         "activo": true,
        ///         "centro_investigacion": "string"
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///         "id": 0,
        ///         "nombre_proyecto_investigacion": "string",
        ///         "activo": true,
        ///         "centro_investigacion": "string"
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve el proyecto de investigacion creado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT</response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost]
        [ActionName("CrearProyectoInvestigacion")]
        [ProducesResponseType(typeof(ProyectosInvestigacion), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ProyectosInvestigacion> CrearProyectoInvestigacion(ProyectosInvestigacion servicioInt)
        {
            try
            {
                if (TienePermiso(141))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    //Comprueba que tengamos un usuario autorizado a los cambios y que lo podamos registrar
                    if (userData != null &&
                        userData.Length > 0 &&
                        userData != "NO DATA" &&
                        int.TryParse(userData.Split(',')[2], out int idUserCrea))
                    {
                        servicioInt = BecAdapter.CrearProyecto(servicioInt);
                        if (servicioInt.id != -1) return Created("Proyecto Creado", servicioInt);
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
        /// Listar Servicios Internos
        /// </summary>
        /// <returns>Un listado de servicios de la facultad</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Beca/ObtenerServiciosInternos/
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "id": 0,
        ///         "nombre": "string",
        ///         "nro_telefono": 0,
        ///         "nro_interno_telefono": 0,
        ///         "horario_atencion": "string",
        ///         "horario_atencion_real": "string",
        ///         "email_institucional": "string"
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado de servicios internos </response>
        /// <response code="204" >No se encontro ningun servucio </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerServiciosInternos")]
        [ProducesResponseType(typeof(IEnumerable<ServiciosInternosFacultad>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ServiciosInternosFacultad>> ObtenerServiciosInternos()
        {
            try
            {
                //Este lo puede visualizar los servicios 
                if ( TienePermiso(144))
                {
                    List<ServiciosInternosFacultad>? listProyec = BecAdapter.ObtenerServiciosDisponibles();

                    if (listProyec == null) return Conflict();
                    if (listProyec.Count == 0) return NoContent();

                    return Ok(listProyec);
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
        /// Permite modificar un servicio de la facultad
        /// </summary>
        /// <param name="id">El ID del servicio</param>
        /// <param name="servicio">El servicio que deseamos modificar, se envia en el Body</param>
        /// <returns>Un servicio modificado en BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Beca/ModificarServicio/{id}
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "nombre": "string",
        ///       "nro_telefono": 0,
        ///       "nro_interno_telefono": 0,
        ///       "horario_atencion": "string",
        ///       "horario_atencion_real": "string",
        ///       "email_institucional": "string"
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "nombre": "string",
        ///       "nro_telefono": 0,
        ///       "nro_interno_telefono": 0,
        ///       "horario_atencion": "string",
        ///       "horario_atencion_real": "string",
        ///       "email_institucional": "string"
        ///     }
        ///       
        /// </remarks>
        /// <response code="200" >Devuelve el servicio modificado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del proyecto a modificar </response>
        /// <response code="401" >El usuario no genero su JWT</response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{id}")]
        [ActionName("ModificarServicio")]
        [ProducesResponseType(typeof(ServiciosInternosFacultad), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ServiciosInternosFacultad> ModificarProyecto(int id, ServiciosInternosFacultad servicio)
        {
            try
            {
                if (id != servicio.id) return BadRequest();

                if (TienePermiso(142))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    //Comprueba que tengamos un usuario autorizado a los cambios y que lo podamos registrar
                    if (userData != null &&
                        userData.Length > 0 &&
                        userData != "NO DATA" &&
                        int.TryParse(userData.Split(',')[2], out int idUserMod))
                    {
                        servicio = BecAdapter.ModificarServicio(servicio);

                        if (servicio.id != -1) return Ok(servicio);
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
        /// Permite crear un servicio de la facultad
        /// </summary>
        /// <param name="servicioInt">El servicio de investigacion que deseamos crear, se envia en el Body</param>
        /// <returns>Un servicio para contactar o asociar a un becario creado en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization.
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Beca/CrearServicioInterno/
        ///     
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "nombre": "string",
        ///       "nro_telefono": 0,
        ///       "nro_interno_telefono": 0,
        ///       "horario_atencion": "string",
        ///       "horario_atencion_real": "string",
        ///       "email_institucional": "string"
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "nombre": "string",
        ///       "nro_telefono": 0,
        ///       "nro_interno_telefono": 0,
        ///       "horario_atencion": "string",
        ///       "horario_atencion_real": "string",
        ///       "email_institucional": "string"
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve el servicio creado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT</response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost]
        [ActionName("CrearServicioInterno")]
        [ProducesResponseType(typeof(ServiciosInternosFacultad), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ServiciosInternosFacultad> CrearServicioInterno(ServiciosInternosFacultad servicioInt)
        {
            try
            {
                if (TienePermiso(141))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    //Comprueba que tengamos un usuario autorizado a los cambios y que lo podamos registrar
                    if (userData != null &&
                        userData.Length > 0 &&
                        userData != "NO DATA" &&
                        int.TryParse(userData.Split(',')[2], out int idUserCrea))
                    {
                        servicioInt = BecAdapter.CrearServicioInterno(servicioInt);
                        if (servicioInt.id != -1) return Created("Proyecto Creado", servicioInt);
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
        /// Busca becarios por legajo
        /// </summary>
        /// <param name="legajo">Es el identificador del legajo en la BD</param>
        /// <returns>Un becario encontrado por legajo</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Beca/ObtenerSituacionesAcademicasXLegajo/{legajo}
        ///     
        ///     RESPONSE:
        ///     [
        ///        {
        ///             "id": 0,
        ///             "legajo": "string",
        ///             "cursando": true,
        ///             "anio_situacion": 0,
        ///             "cant_materias_cursadas_anterior": 0,
        ///             "cant_materias_aprobadas_periodo_anterior": 0,
        ///             "cant_materias_cursando": 0,
        ///             "cant_materias_aprobadas_total": 0,
        ///             "prom_gral_con_aplazos": 0,
        ///             "prom_gral_sin_aplazos": 0,
        ///             "ingreso": 0
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un becario nacional </response>
        /// <response code="204" >No se encontro ningun becario </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{legajo}")]
        [ActionName("ObtenerSituacionesAcademicasXLegajo")]
        [ProducesResponseType(typeof(IEnumerable<SituacionesAcademicas>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<SituacionesAcademicas>> ObtenerSituacionesAcademicasXLegajo(string legajo)
        {
            try
            {
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                //Este lo puede visualizar los becarios
                if (userData != null && userData != "NO DATA" && TienePermiso(125))
                {
                    string legajoRegistrado = userData.Split(',')[0];
                    string idPerfil = userData.Split(',')[1];
                    if (idPerfil == "2" || idPerfil == "5" || legajoRegistrado == legajo)
                    {
                        List<SituacionesAcademicas>? listProyec = BecAdapter.BuscarSituacionesAcademicasXLegajo(legajo);

                        if (listProyec == null) return Conflict();
                        if (listProyec.Count == 0) return NoContent();

                        return Ok(listProyec);
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
        /// Permite modificar una situacion academica
        /// </summary>
        /// <param name="id">El ID de la situacion</param>
        /// <param name="situacion">El proyecto que deseamos modificar, se envia en el Body</param>
        /// <returns>Un proyecto modificado en BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Beca/ModificarSituacionAcademica/{id}
        ///     BODY:
        ///     {
        ///         "id": 0,
        ///         "legajo": "string",
        ///         "cursando": true,
        ///         "anio_situacion": 0,
        ///         "cant_materias_cursadas_anterior": 0,
        ///         "cant_materias_aprobadas_periodo_anterior": 0,
        ///         "cant_materias_cursando": 0,
        ///         "cant_materias_aprobadas_total": 0,
        ///         "prom_gral_con_aplazos": 0.00,
        ///         "prom_gral_sin_aplazos": 0.00,
        ///         "ingreso": 0
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///         "id": 0,
        ///         "legajo": "string",
        ///         "cursando": true,
        ///         "anio_situacion": 0,
        ///         "cant_materias_cursadas_anterior": 0,
        ///         "cant_materias_aprobadas_periodo_anterior": 0,
        ///         "cant_materias_cursando": 0,
        ///         "cant_materias_aprobadas_total": 0,
        ///         "prom_gral_con_aplazos": 0.00,
        ///         "prom_gral_sin_aplazos": 0.00,
        ///         "ingreso": 0
        ///     }
        ///       
        /// </remarks>
        /// <response code="200" >Devuelve la situacion academica modificado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que la situacion a modificar </response>
        /// <response code="401" >El usuario no genero su JWT</response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{id}")]
        [ActionName("ModificarSituacionAcademica")]
        [ProducesResponseType(typeof(SituacionesAcademicas), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<SituacionesAcademicas> ModificarSituacionAcademica(int id, SituacionesAcademicas situacion)
        {
            try
            {
                if (id != situacion.id) return BadRequest();

                if ( TienePermiso(124))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    //Comprueba que tengamos un usuario autorizado a los cambios y que lo podamos registrar
                    if (userData != null &&
                        userData.Length > 0 &&
                        userData != "NO DATA" &&
                        int.TryParse(userData.Split(',')[2], out int idUserMod))
                    {
                        situacion = BecAdapter.ModificarSituacionAcademica(situacion,idUserMod);

                        if (situacion.id != -1) return Ok(situacion);
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
        /// Permite crear una situacion academica
        /// </summary>
        /// <param name="situacion">La situacion academica que deseamos crear, se envia en el Body</param>
        /// <returns>Una situacion academica creado en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization.
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Beca/CrearSituacionAcademica/
        ///     
        ///     BODY:
        ///     {
        ///         "id": 0,
        ///         "legajo": "string",
        ///         "cursando": true,
        ///         "anio_situacion": 0,
        ///         "cant_materias_cursadas_anterior": 0,
        ///         "cant_materias_aprobadas_periodo_anterior": 0,
        ///         "cant_materias_cursando": 0,
        ///         "cant_materias_aprobadas_total": 0,
        ///         "prom_gral_con_aplazos": 0.00,
        ///         "prom_gral_sin_aplazos": 0.00,
        ///         "ingreso": 0
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///         "id": 0,
        ///         "legajo": "string",
        ///         "cursando": true,
        ///         "anio_situacion": 0,
        ///         "cant_materias_cursadas_anterior": 0,
        ///         "cant_materias_aprobadas_periodo_anterior": 0,
        ///         "cant_materias_cursando": 0,
        ///         "cant_materias_aprobadas_total": 0,
        ///         "prom_gral_con_aplazos": 0.00,
        ///         "prom_gral_sin_aplazos": 0.00,
        ///         "ingreso": 0
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve la situacion academica creado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT</response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost]
        [ActionName("CrearSituacionAcademica")]
        [ProducesResponseType(typeof(SituacionesAcademicas), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<SituacionesAcademicas> CrearSituacionAcademica(SituacionesAcademicas situacion)
        {
            try
            {
                if ( TienePermiso(123))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    //Comprueba que tengamos un usuario autorizado a los cambios y que lo podamos registrar
                    if (userData != null &&
                        userData.Length > 0 &&
                        userData != "NO DATA" &&
                        int.TryParse(userData.Split(',')[2], out int idUserCrea))
                    {
                        situacion = BecAdapter.CrearSituacionAcademica(situacion,idUserCrea);
                        if (situacion.id != -1) return Created("Situacion Creada", situacion);
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
            if (int.TryParse(userData.Split(',')[1], out int id_perfil)) return UserAdapter.TienePermiso(id_funcion, id_perfil);
            else return false;
        }
    }
}
