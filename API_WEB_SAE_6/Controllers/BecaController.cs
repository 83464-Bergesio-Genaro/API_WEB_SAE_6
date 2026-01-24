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
    ///// <summary>
    ///// Es el controlador para los becarios del area y nacionales
    ///// </summary>
    //[EnableCors("CorsRules")]
    //[Route("api/[controller]/[action]")]
    //[Authorize]
    //[ApiController]
    //public class BecaController : Controller
    //{


    //    private readonly IConfiguration _config;
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="config"></param>
    //    public BecaController(IConfiguration config)
    //    {
    //        _config = config;
    //    }
    //    /// <summary>
    //    /// EN: The logger functions as a register of the exception that happen in the runtime. <br/>
    //    /// ES: El logger funciona como el registro de excpciones que pasan en tiempo de ejecuccion <br/>
    //    /// </summary>
    //    private readonly Logger _logger = new();
    //    /// <summary>
    //    /// Recupera todos los becarios
    //    /// </summary>
    //    /// <returns>Un listado de todos los becarios</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Beca/ObtenerBecariosCompleto/
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///       {
    //    ///         "id": 0,
    //    ///         "legajo": "string",
    //    ///         "nombre_becario": "string",
    //    ///         "alquila": true,
    //    ///         "fecha_solicitud": "2024-07-19T19:48:53.374Z",
    //    ///         "aceptado_inicio": true,
    //    ///         "puede_pagarle": true,
    //    ///         "activo": true,
    //    ///         "anio_beca": 0,
    //    ///         "id_becario_previo": 0
    //    ///       }
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un listado completo de los becarios </response>
    //    /// <response code="204" >No se encontro ningun becario </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet]
    //    [ActionName("ObtenerBecariosCompleto")]
    //    [ProducesResponseType(typeof(IEnumerable<BecariosSAE>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<BecariosSAE>>> ObtenerBecariosCompleto()
    //    {
    //        try
    //        {
    //            //El numero de funcion es: 53
    //            if (await TienePermiso(53))
    //            {
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_BECAS_Listar_Becarios_SAE");
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<BecariosSAE> listadoBecarios = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    BecariosSAE becario = new(row);
    //                    listadoBecarios.Add(becario);
    //                }
    //                return Ok(listadoBecarios);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR LISTANDO BECARIOS");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Recupera todos los becarios activos
    //    /// </summary>
    //    /// <returns>Un listado de todos los becarios activos</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Beca/ObtenerBecariosActivos/
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///       {
    //    ///         "id": 0,
    //    ///         "legajo": "string",
    //    ///         "nombre_becario": "string",
    //    ///         "alquila": true,
    //    ///         "fecha_solicitud": "2024-07-19T19:48:53.374Z",
    //    ///         "aceptado_inicio": true,
    //    ///         "puede_pagarle": true,
    //    ///         "activo": true,
    //    ///         "anio_beca": 0,
    //    ///         "id_becario_previo": 0
    //    ///       }
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un listado completo de los becarios </response>
    //    /// <response code="204" >No se encontro ningun becario </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet]
    //    [ActionName("ObtenerBecariosActivos")]
    //    [ProducesResponseType(typeof(IEnumerable<BecariosSAE>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<BecariosSAE>>> ObtenerBecariosActivos()
    //    {
    //        try
    //        {
    //            //El numero de funcion es: 53
    //            if (await TienePermiso(53))
    //            {
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_BECAS_Listar_Becarios_SAE_Activos");
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<BecariosSAE> listadoBecarios = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    BecariosSAE becarios = new(row);
    //                    listadoBecarios.Add(becarios);
    //                }
    //                return Ok(listadoBecarios);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR LISTANDO BECARIOS ACTIVOS");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Busca becarios por ID
    //    /// </summary>
    //    /// <param name="id_becario">Es el identificador del becario en la BD</param>
    //    /// <returns>Un becario encontrado por ID</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Beca/ObtenerBecariosXId/{id_becario}
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "legajo": "string",
    //    ///       "nombre_becario": "string",
    //    ///       "alquila": true,
    //    ///       "fecha_solicitud": "2024-07-19T19:48:53.374Z",
    //    ///       "aceptado_inicio": true,
    //    ///       "puede_pagarle": true,
    //    ///       "activo": true,
    //    ///       "anio_beca": 0,
    //    ///       "id_becario_previo": 0
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un becario </response>
    //    /// <response code="204" >No se encontro ningun becario </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet("{id_becario}")]
    //    [ActionName("ObtenerBecariosXId")]
    //    [ProducesResponseType(typeof(BecariosSAE), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<BecariosSAE>> ObtenerBecariosXId(int id_becario)
    //    {
    //        try
    //        {
    //            //Este lo puede visualizar los becarios
    //            if (await TienePermiso(53))
    //            {
    //                Dictionary<string, string> parametros = new()
    //                {
    //                    { "@id_becario", id_becario.ToString() }
    //                };
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_BECAS_Buscar_Becarios_SAE_Id", parametros);
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                BecariosSAE becario = new(respuesta.Rows[0]);
    //                return Ok(becario);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR BUSCANDO BECARIOS SAE");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Busca becarios por legajo
    //    /// </summary>
    //    /// <param name="legajo">Es el identificador del becario en la FRC</param>
    //    /// <returns>Un listado de becarios por su legajo</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Beca/ObtenerBecariosXLegajo/{legajo}
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "legajo": "string",
    //    ///       "nombre_becario": "string",
    //    ///       "alquila": true,
    //    ///       "fecha_solicitud": "2024-07-19T19:48:53.374Z",
    //    ///       "aceptado_inicio": true,
    //    ///       "puede_pagarle": true,
    //    ///       "activo": true,
    //    ///       "anio_beca": 0,
    //    ///       "id_becario_previo": 0
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un becario </response>
    //    /// <response code="204" >No se encontro ningun becario </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet("{legajo}")]
    //    [ActionName("ObtenerBecariosXLegajo")]
    //    [ProducesResponseType(typeof(IEnumerable<BecariosSAE>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<BecariosSAE>>> ObtenerBecariosXLegajo(string legajo)
    //    {
    //        try
    //        {
    //            //Este lo puede visualizar los becarios
    //            if (await TienePermiso(53))
    //            {
    //                Dictionary<string, string> parametros = new()
    //                {
    //                    { "@legajo", legajo.ToString() }
    //                };
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_BECAS_Buscar_Becarios_SAE_Legajo", parametros);
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<BecariosSAE> listadoBecarios = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    BecariosSAE becarios = new(row);
    //                    listadoBecarios.Add(becarios);
    //                }
    //                return Ok(listadoBecarios);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR BUSCANDO BECARIOS SAE POR LEGAJO");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Recupera todos los becarios con modulos economicos
    //    /// </summary>
    //    /// <returns>Un listado de todos los becarios</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Beca/ObtenerBecariosEconomica/
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///         {
    //    ///             "id": 0,
    //    ///             "entrevista_realizada": true,
    //    ///             "modulo_asignados": 0,
    //    ///             "becario": 
    //    ///             {
    //    ///                 "id": 0,
    //    ///                 "legajo": "string",
    //    ///                 "nombre_becario": "string",
    //    ///                 "alquila": true,
    //    ///                 "fecha_solicitud": "2024-07-21T14:50:52.102Z",
    //    ///                 "aceptado_inicio": true,
    //    ///                 "puede_pagarle": true,
    //    ///                 "activo": true,
    //    ///                 "anio_beca": 0,
    //    ///                 "id_becario_previo": 0
    //    ///             }
    //    ///         }
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un listado completo de los becarios de economica </response>
    //    /// <response code="204" >No se encontro ningun becario con modulos en economicas </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet]
    //    [ActionName("ObtenerBecariosEconomica")]
    //    [ProducesResponseType(typeof(IEnumerable<BecariosSAEEconomica>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<BecariosSAEEconomica>>> ObtenerBecariosEconomica()
    //    {
    //        try
    //        {
    //            //El numero de funcion es: 53
    //            if (await TienePermiso(53))
    //            {
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_BECAS_Listar_Becarios_Economica");
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<BecariosSAEEconomica> listadoEconomicas = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    BecariosSAEEconomica becario = new(row);
    //                    listadoEconomicas.Add(becario);
    //                }
    //                return Ok(listadoEconomicas);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR LISTANDO BECARIOS ECONOMICAS");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Recupera todos los becarios con modulos economicos activos
    //    /// </summary>
    //    /// <returns>Un listado de todos los becarios activos</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Beca/ObtenerBecariosEconomicaActivos/
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///         {
    //    ///             "id": 0,
    //    ///             "entrevista_realizada": true,
    //    ///             "modulo_asignados": 0,
    //    ///             "becario": 
    //    ///             {
    //    ///                 "id": 0,
    //    ///                 "legajo": "string",
    //    ///                 "nombre_becario": "string",
    //    ///                 "alquila": true,
    //    ///                 "fecha_solicitud": "2024-07-21T14:50:52.102Z",
    //    ///                 "aceptado_inicio": true,
    //    ///                 "puede_pagarle": true,
    //    ///                 "activo": true,
    //    ///                 "anio_beca": 0,
    //    ///                 "id_becario_previo": 0
    //    ///             }
    //    ///         }
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un listado completo de los becarios de economica activos </response>
    //    /// <response code="204" >No se encontro ningun becario con modulos en economicas activos </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet]
    //    [ActionName("ObtenerBecariosEconomicaActivos")]
    //    [ProducesResponseType(typeof(IEnumerable<BecariosSAEEconomica>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<BecariosSAEEconomica>>> ObtenerBecariosEconomicaActivos()
    //    {
    //        try
    //        {
    //            //El numero de funcion es: 53
    //            if (await TienePermiso(53))
    //            {
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_BECAS_Listar_Becarios_Economica_Activo");
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<BecariosSAEEconomica> listadoEconomicas = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    BecariosSAEEconomica becario = new(row);
    //                    listadoEconomicas.Add(becario);
    //                }
    //                return Ok(listadoEconomicas);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR LISTANDO BECARIOS ECONOMICAS ACTIVOS");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Busca becarios por ID becario
    //    /// </summary>
    //    /// <param name="id_becario">Es el identificador del becario en la BD</param>
    //    /// <returns>Un becario encontrado por ID</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Beca/ObtenerBecariosEconomicaXIdBecario/{id_becario}
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///         "id": 0,
    //    ///         "entrevista_realizada": true,
    //    ///         "modulo_asignados": 0,
    //    ///         "becario": 
    //    ///         {
    //    ///             "id": 0,
    //    ///             "legajo": "string",
    //    ///             "nombre_becario": "string",
    //    ///             "alquila": true,
    //    ///             "fecha_solicitud": "2024-07-21T14:50:52.102Z",
    //    ///             "aceptado_inicio": true,
    //    ///             "puede_pagarle": true,
    //    ///             "activo": true,
    //    ///             "anio_beca": 0,
    //    ///             "id_becario_previo": 0
    //    ///         }
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un becario </response>
    //    /// <response code="204" >No se encontro ningun becario </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet("{id_becario}")]
    //    [ActionName("ObtenerBecariosEconomicaXIdBecario")]
    //    [ProducesResponseType(typeof(BecariosSAE), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<BecariosSAE>> ObtenerBecariosEconomicaXIdBecario(int id_becario)
    //    {
    //        try
    //        {
    //            //Este lo puede visualizar los becarios
    //            if (await TienePermiso(53))
    //            {
    //                Dictionary<string, string> parametros = new()
    //                {
    //                    { "@id_becario", id_becario.ToString() }
    //                };
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_BECAS_Buscar_Becarios_Economica_Id_becario", parametros);
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                BecariosSAE becario = new(respuesta.Rows[0]);
    //                return Ok(becario);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR BUSCANDO BECARIOS ECONOMICA POR ID");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Recupera todos los becarios con modulos de investigacion
    //    /// </summary>
    //    /// <returns>Un listado de todos los becarios</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Beca/ObtenerBecariosInvestigacion/
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///         {
    //    ///             "id": 0,
    //    ///             "proyecto_investigacion": 
    //    ///             {
    //    ///                 "id": 0,
    //    ///                 "nombre_proyecto_investigacion": "string",
    //    ///                 "activo": true,
    //    ///                 "centro_investigacion": "string"
    //    ///             },
    //    ///             "modulos_asignados": 0,
    //    ///             "becario": 
    //    ///             {
    //    ///                 "id": 0,
    //    ///                 "legajo": "string",
    //    ///                 "nombre_becario": "string",
    //    ///                 "alquila": true,
    //    ///                 "fecha_solicitud": "2024-07-21T16:40:09.230Z",
    //    ///                 "aceptado_inicio": true,
    //    ///                 "puede_pagarle": true,
    //    ///                 "activo": true,
    //    ///                 "anio_beca": 0,
    //    ///                 "id_becario_previo": 0
    //    ///             }
    //    ///         }
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un listado completo de los becarios de investigacion </response>
    //    /// <response code="204" >No se encontro ningun becario con modulos en investigacion </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet]
    //    [ActionName("ObtenerBecariosInvestigacion")]
    //    [ProducesResponseType(typeof(IEnumerable<BecariosSAEInvestigacion>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<BecariosSAEInvestigacion>>> ObtenerBecariosInvestigacion()
    //    {
    //        try
    //        {
    //            //El numero de funcion es: 53
    //            if (await TienePermiso(53))
    //            {
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_BECAS_Listar_Becarios_Investigacion");
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<BecariosSAEInvestigacion> listadoInvestigacion = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    BecariosSAEInvestigacion becario = new(row);
    //                    listadoInvestigacion.Add(becario);
    //                }
    //                return Ok(listadoInvestigacion);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR LISTANDO BECARIOS INVESTIGACION");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Recupera todos los becarios con modulos de investigacion
    //    /// </summary>
    //    /// <returns>Un listado de todos los becarios activos</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Beca/ObtenerBecariosInvestigacionActivos/
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///         {
    //    ///             "id": 0,
    //    ///             "proyecto_investigacion": 
    //    ///             {
    //    ///                 "id": 0,
    //    ///                 "nombre_proyecto_investigacion": "string",
    //    ///                 "activo": true,
    //    ///                 "centro_investigacion": "string"
    //    ///             },
    //    ///             "modulos_asignados": 0,
    //    ///             "becario": 
    //    ///             {
    //    ///                 "id": 0,
    //    ///                 "legajo": "string",
    //    ///                 "nombre_becario": "string",
    //    ///                 "alquila": true,
    //    ///                 "fecha_solicitud": "2024-07-21T16:40:09.230Z",
    //    ///                 "aceptado_inicio": true,
    //    ///                 "puede_pagarle": true,
    //    ///                 "activo": true,
    //    ///                 "anio_beca": 0,
    //    ///                 "id_becario_previo": 0
    //    ///             }
    //    ///         }
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un listado completo de los becarios de investigacion activos </response>
    //    /// <response code="204" >No se encontro ningun becario con modulos en investigacion activos </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet]
    //    [ActionName("ObtenerBecariosInvestigacionActivos")]
    //    [ProducesResponseType(typeof(IEnumerable<BecariosSAEInvestigacion>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<BecariosSAEInvestigacion>>> ObtenerBecariosInvestigacionActivos()
    //    {
    //        try
    //        {
    //            //El numero de funcion es: 53
    //            if (await TienePermiso(53))
    //            {
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_BECAS_Listar_Becarios_Investigacion_Activos");
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<BecariosSAEInvestigacion> listadoInvestigacion = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    BecariosSAEInvestigacion becario = new(row);
    //                    listadoInvestigacion.Add(becario);
    //                }
    //                return Ok(listadoInvestigacion);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR LISTANDO BECARIOS INVESTIGACION ACTIVOS");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Busca becarios por ID becario
    //    /// </summary>
    //    /// <param name="id_becario">Es el identificador del becario en la BD</param>
    //    /// <returns>Un becario encontrado por ID</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Beca/ObtenerBecariosInvestigacionXIdBecario/{id_becario}
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///         "id": 0,
    //    ///         "proyecto_investigacion": 
    //    ///         {
    //    ///             "id": 0,
    //    ///             "nombre_proyecto_investigacion": "string",
    //    ///             "activo": true,
    //    ///             "centro_investigacion": "string"
    //    ///         },
    //    ///         "modulos_asignados": 0,
    //    ///         "becario": 
    //    ///         {
    //    ///             "id": 0,
    //    ///             "legajo": "string",
    //    ///             "nombre_becario": "string",
    //    ///             "alquila": true,
    //    ///             "fecha_solicitud": "2024-07-21T16:40:09.230Z",
    //    ///             "aceptado_inicio": true,
    //    ///             "puede_pagarle": true,
    //    ///             "activo": true,
    //    ///             "anio_beca": 0,
    //    ///             "id_becario_previo": 0
    //    ///         }
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un becario </response>
    //    /// <response code="204" >No se encontro ningun becario </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet("{id_becario}")]
    //    [ActionName("ObtenerBecariosInvestigacionXIdBecario")]
    //    [ProducesResponseType(typeof(BecariosSAEInvestigacion), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<BecariosSAEInvestigacion>> ObtenerBecariosInvestigacionXIdBecario(int id_becario)
    //    {
    //        try
    //        {
    //            //Este lo puede visualizar los becarios
    //            if (await TienePermiso(53))
    //            {
    //                Dictionary<string, string> parametros = new()
    //                {
    //                    { "@id_becario", id_becario.ToString() }
    //                };
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_BECAS_Buscar_Becarios_Investigacion_Id_becario", parametros);
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                BecariosSAEInvestigacion becario = new(respuesta.Rows[0]);
    //                return Ok(becario);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR BUSCANDO BECARIOS INVESTIGACION POR ID");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Recupera todos los becarios con modulos de servicios
    //    /// </summary>
    //    /// <returns>Un listado de todos los becarios de servicios</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Beca/ObtenerBecariosServicio/
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///       {
    //    ///         "id": 0,
    //    ///         "servicio": 
    //    ///         {
    //    ///             "id": 0,
    //    ///             "nombre": "string",
    //    ///             "nro_telefono": 0,
    //    ///             "nro_interno_telefono": 0,
    //    ///             "horario_atencion": "string",
    //    ///             "horario_atencion_real": "string",
    //    ///             "email_institucional": "string"
    //    ///         },
    //    ///         "modulos_asignados": 0,
    //    ///         "becario": 
    //    ///         {
    //    ///           "id": 0,
    //    ///           "legajo": "string",
    //    ///           "nombre_becario": "string",
    //    ///           "alquila": true,
    //    ///           "fecha_solicitud": "2024-07-21T17:56:49.671Z",
    //    ///           "aceptado_inicio": true,
    //    ///           "puede_pagarle": true,
    //    ///           "activo": true,
    //    ///           "anio_beca": 0,
    //    ///           "id_becario_previo": 0
    //    ///         }
    //    ///       }
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un listado completo de los becarios de investigacion </response>
    //    /// <response code="204" >No se encontro ningun becario con modulos en investigacion </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet]
    //    [ActionName("ObtenerBecariosServicio")]
    //    [ProducesResponseType(typeof(IEnumerable<BecariosSAEServicio>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<BecariosSAEServicio>>> ObtenerBecariosServicio()
    //    {
    //        try
    //        {
    //            //El numero de funcion es: 53
    //            if (await TienePermiso(53))
    //            {
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_BECAS_Listar_Becarios_Servicios");
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<BecariosSAEServicio> listadoServicio = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    BecariosSAEServicio becario = new(row);
    //                    listadoServicio.Add(becario);
    //                }
    //                return Ok(listadoServicio);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR LISTANDO BECARIOS SERVICIOS");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Recupera todos los becarios con modulos de servicios activos
    //    /// </summary>
    //    /// <returns>Un listado de todos los becarios activos</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Beca/ObtenerBecariosServiciosActivos/
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///       {
    //    ///         "id": 0,
    //    ///         "servicio": 
    //    ///         {
    //    ///             "id": 0,
    //    ///             "nombre": "string",
    //    ///             "nro_telefono": 0,
    //    ///             "nro_interno_telefono": 0,
    //    ///             "horario_atencion": "string",
    //    ///             "horario_atencion_real": "string",
    //    ///             "email_institucional": "string"
    //    ///         },
    //    ///         "modulos_asignados": 0,
    //    ///         "becario": 
    //    ///         {
    //    ///           "id": 0,
    //    ///           "legajo": "string",
    //    ///           "nombre_becario": "string",
    //    ///           "alquila": true,
    //    ///           "fecha_solicitud": "2024-07-21T17:56:49.671Z",
    //    ///           "aceptado_inicio": true,
    //    ///           "puede_pagarle": true,
    //    ///           "activo": true,
    //    ///           "anio_beca": 0,
    //    ///           "id_becario_previo": 0
    //    ///         }
    //    ///       }
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un listado completo de los becarios de servicios activos </response>
    //    /// <response code="204" >No se encontro ningun becario con modulos en servicios activos </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet]
    //    [ActionName("ObtenerBecariosServiciosActivos")]
    //    [ProducesResponseType(typeof(IEnumerable<BecariosSAEServicio>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<BecariosSAEServicio>>> ObtenerBecariosServiciosActivos()
    //    {
    //        try
    //        {
    //            //El numero de funcion es: 53
    //            if (await TienePermiso(53))
    //            {
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_BECAS_Listar_Becarios_Servicios_Activos");
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<BecariosSAEServicio> listadoServicios = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    BecariosSAEServicio becario = new(row);
    //                    listadoServicios.Add(becario);
    //                }
    //                return Ok(listadoServicios);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR LISTANDO BECARIOS SERVICIOS ACTIVOS");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Busca becarios por ID becario
    //    /// </summary>
    //    /// <param name="id_becario">Es el identificador del becario en la BD</param>
    //    /// <returns>Un becario encontrado por ID</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Beca/ObtenerBecariosServiciosXIdBecario/{id_becario}
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///         "id": 0,
    //    ///         "servicio": 
    //    ///         {
    //    ///             "id": 0,
    //    ///             "nombre": "string",
    //    ///             "nro_telefono": 0,
    //    ///             "nro_interno_telefono": 0,
    //    ///             "horario_atencion": "string",
    //    ///             "horario_atencion_real": "string",
    //    ///             "email_institucional": "string"
    //    ///         },
    //    ///         "modulos_asignados": 0,
    //    ///         "becario": 
    //    ///         {
    //    ///           "id": 0,
    //    ///           "legajo": "string",
    //    ///           "nombre_becario": "string",
    //    ///           "alquila": true,
    //    ///           "fecha_solicitud": "2024-07-21T17:56:49.671Z",
    //    ///           "aceptado_inicio": true,
    //    ///           "puede_pagarle": true,
    //    ///           "activo": true,
    //    ///           "anio_beca": 0,
    //    ///           "id_becario_previo": 0
    //    ///         }
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un becario </response>
    //    /// <response code="204" >No se encontro ningun becario </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet("{id_becario}")]
    //    [ActionName("ObtenerBecariosServiciosXIdBecario")]
    //    [ProducesResponseType(typeof(BecariosSAEServicio), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<BecariosSAEServicio>> ObtenerBecariosServiciosXIdBecario(int id_becario)
    //    {
    //        try
    //        {
    //            //Este lo puede visualizar los becarios
    //            if (await TienePermiso(53))
    //            {
    //                Dictionary<string, string> parametros = new()
    //                {
    //                    { "@id_becario", id_becario.ToString() }
    //                };
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_BECAS_Buscar_Becarios_Servicios_Id_becario", parametros);
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                BecariosSAEServicio becario = new(respuesta.Rows[0]);
    //                return Ok(becario);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR BUSCANDO BECARIOS SERVICIOS POR ID");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Listar becarios nacionales
    //    /// </summary>
    //    /// <returns>Un listado de todos los becarios nacionales</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Beca/ObtenerBecariosNacionales/
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///       {
    //    ///         "id": 0,
    //    ///         "legajo": "string",
    //    ///         "nombre_becario": "string",
    //    ///         "tipo_plan": 0,
    //    ///         "regularizacion": true,
    //    ///         "cumplimiento_servicio": true,
    //    ///         "activo": true
    //    ///       }
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un listado de becarios </response>
    //    /// <response code="204" >No se encontro ningun becario </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet]
    //    [ActionName("ObtenerBecariosNacionales")]
    //    [ProducesResponseType(typeof(IEnumerable<BecariosNacionales>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<BecariosNacionales>>> ObtenerBecariosNacionales()
    //    {
    //        try
    //        {
    //            //Este lo puede visualizar los becarios nacionales
    //            if (await TienePermiso(145))
    //            {
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_BECAS_Listar_Becarios_Nacional");
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<BecariosNacionales> listadoBecariosNacionales = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    BecariosNacionales becarios = new(row);
    //                    listadoBecariosNacionales.Add(becarios);
    //                }
    //                return Ok(listadoBecariosNacionales);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR LISTANDO BECARIOS NACIONALES");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Busca becarios por ID becario
    //    /// </summary>
    //    /// <param name="id_becario">Es el identificador del becario en la BD</param>
    //    /// <returns>Un becario encontrado por ID</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Beca/ObtenerBecariosServiciosXIdBecario/{id_becario}
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "legajo": "string",
    //    ///       "nombre_becario": "string",
    //    ///       "tipo_plan": 0,
    //    ///       "regularizacion": true,
    //    ///       "cumplimiento_servicio": true,
    //    ///       "activo": true
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un becario nacional </response>
    //    /// <response code="204" >No se encontro ningun becario </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet("{id_becario}")]
    //    [ActionName("ObtenerBecariosNacionalesXId")]
    //    [ProducesResponseType(typeof(BecariosNacionales), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<BecariosNacionales>> ObtenerBecariosNacionalesXId(int id_becario)
    //    {
    //        try
    //        {
    //            //Este lo puede visualizar los becarios
    //            if (await TienePermiso(145))
    //            {
    //                Dictionary<string, string> parametros = new()
    //                {
    //                    { "@id_becario", id_becario.ToString() }
    //                };
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_BECAS_Buscar_Becario_Nacional_Id", parametros);
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                BecariosNacionales becario = new(respuesta.Rows[0]);
    //                return Ok(becario);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR BUSCANDO BECARIOS NACIONALES POR ID");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Busca becarios por legajo
    //    /// </summary>
    //    /// <param name="legajo">Es el identificador del legajo en la BD</param>
    //    /// <returns>Un becario encontrado por legajo</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Beca/ObtenerBecariosNacionalesXLegajo/{legajo}
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///       {
    //    ///         "id": 0,
    //    ///         "legajo": "string",
    //    ///         "nombre_becario": "string",
    //    ///         "tipo_plan": 0,
    //    ///         "regularizacion": true,
    //    ///         "cumplimiento_servicio": true,
    //    ///         "activo": true
    //    ///       }
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un becario nacional </response>
    //    /// <response code="204" >No se encontro ningun becario </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet("{legajo}")]
    //    [ActionName("ObtenerBecariosNacionalesXLegajo")]
    //    [ProducesResponseType(typeof(IEnumerable<BecariosNacionales>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<BecariosNacionales>>> ObtenerBecariosNacionalesXLegajo(string legajo)
    //    {
    //        try
    //        {
    //            //Este lo puede visualizar los becarios
    //            if (await TienePermiso(145))
    //            {
    //                Dictionary<string, string> parametros = new()
    //                {
    //                    { "@legajo", legajo }
    //                };
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_BECAS_Buscar_Becario_Nacional_Legajo", parametros);
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<BecariosNacionales> listadoBecariosNacionales = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    BecariosNacionales becarios = new(row);
    //                    listadoBecariosNacionales.Add(becarios);
    //                }
    //                return Ok(listadoBecariosNacionales);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR BUSCANDO BECARIOS NACIONALES POR LEGAJO");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Listar proyectos de investigacion
    //    /// </summary>
    //    /// <returns>Un listado de todos los proyectos</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Beca/ObtenerProyectosInvestigacion/
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///       {
    //    ///         "id": 0,
    //    ///         "nombre_proyecto_investigacion": "string",
    //    ///         "activo": true,
    //    ///         "centro_investigacion": "string"
    //    ///       }
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un listado de proyectos </response>
    //    /// <response code="204" >No se encontro ningun proyecto </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet]
    //    [ActionName("ObtenerProyectosInvestigacion")]
    //    [ProducesResponseType(typeof(IEnumerable<ProyectosInvestigacion>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<ProyectosInvestigacion>>> ObtenerProyectosInvestigacion()
    //    {
    //        try
    //        {
    //            //Este lo puede visualizar los proyectos
    //            if (await TienePermiso(143))
    //            {
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_BECAS_Listar_Proyectos_Investigacion");
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<ProyectosInvestigacion> listadoProyectos = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    ProyectosInvestigacion proyecto = new(row);
    //                    listadoProyectos.Add(proyecto);
    //                }
    //                return Ok(listadoProyectos);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR LISTANDO PROYECTOS");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Listar Servicios Internos
    //    /// </summary>
    //    /// <returns>Un listado de servicios de la facultad</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Beca/ObtenerServiciosInternos/
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///       {
    //    ///         "id": 0,
    //    ///         "nombre": "string",
    //    ///         "nro_telefono": 0,
    //    ///         "nro_interno_telefono": 0,
    //    ///         "horario_atencion": "string",
    //    ///         "horario_atencion_real": "string",
    //    ///         "email_institucional": "string"
    //    ///       }
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un listado de servicios internos </response>
    //    /// <response code="204" >No se encontro ningun servucio </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet]
    //    [ActionName("ObtenerServiciosInternos")]
    //    [ProducesResponseType(typeof(IEnumerable<ServiciosInternosFacultad>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<ServiciosInternosFacultad>>> ObtenerServiciosInternos()
    //    {
    //        try
    //        {
    //            //Este lo puede visualizar los servicios 
    //            if (await TienePermiso(144))
    //            {
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_BECAS_Listar_Servicios");
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<ServiciosInternosFacultad> listadoProyectos = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    ServiciosInternosFacultad proyecto = new(row);
    //                    listadoProyectos.Add(proyecto);
    //                }
    //                return Ok(listadoProyectos);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR LISTANDO PROYECTOS");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Busca becarios por legajo
    //    /// </summary>
    //    /// <param name="legajo">Es el identificador del legajo en la BD</param>
    //    /// <returns>Un becario encontrado por legajo</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Beca/ObtenerSituacionesAcademicasXLegajo/{legajo}
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///        {
    //    ///             "id": 0,
    //    ///             "legajo": "string",
    //    ///             "cursando": true,
    //    ///             "anio_situacion": 0,
    //    ///             "cant_materias_cursadas_anterior": 0,
    //    ///             "cant_materias_aprobadas_periodo_anterior": 0,
    //    ///             "cant_materias_cursando": 0,
    //    ///             "cant_materias_aprobadas_total": 0,
    //    ///             "prom_gral_con_aplazos": 0,
    //    ///             "prom_gral_sin_aplazos": 0,
    //    ///             "ingreso": 0
    //    ///       }
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un becario nacional </response>
    //    /// <response code="204" >No se encontro ningun becario </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet("{legajo}")]
    //    [ActionName("ObtenerSituacionesAcademicasXLegajo")]
    //    [ProducesResponseType(typeof(IEnumerable<SituacionesAcademicas>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<SituacionesAcademicas>>> ObtenerSituacionesAcademicasXLegajo(string legajo)
    //    {
    //        try
    //        {
    //            //Este lo puede visualizar los becarios
    //            if (await TienePermiso(125))
    //            {
    //                Dictionary<string, string> parametros = new()
    //                {
    //                    { "@legajo", legajo }
    //                };
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_BECAS_Buscar_Situaciones_Academicas_Legajo", parametros);
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<SituacionesAcademicas> ListadoSituaciones = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    SituacionesAcademicas situacionAcademica = new(row);
    //                    ListadoSituaciones.Add(situacionAcademica);
    //                }
    //                return Ok(ListadoSituaciones);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR BUSCANDO SITUACIONES POR LEGAJO");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite modificar becarios SAE
    //    /// </summary>
    //    /// <param name="id">El ID del becario que deseamos modificar, se envia en la URL</param>
    //    /// <param name="becario">El becario que deseamos modificar, se envia en el Body</param>
    //    /// <returns>Un becario modificado en BD</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Beca/ModificarBecarioSAE/{id}
    //    ///     BODY:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "legajo": "string",
    //    ///       "alquila": true,
    //    ///       "fecha_solicitud": "2024-07-21T17:56:49.671Z",
    //    ///       "aceptado_inicio": true,
    //    ///       "puede_pagarle": true,
    //    ///       "activo": true,
    //    ///       "anio_beca": 0,
    //    ///       "id_becario_previo": 0
    //    ///     }
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "legajo": "string",
    //    ///       "nombre_becario": "string",
    //    ///       "alquila": true,
    //    ///       "fecha_solicitud": "2024-07-21T17:56:49.671Z",
    //    ///       "aceptado_inicio": true,
    //    ///       "puede_pagarle": true,
    //    ///       "activo": true,
    //    ///       "anio_beca": 0,
    //    ///       "id_becario_previo": 0
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve el becario modificado en la BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del becario a modificar </response>
    //    /// <response code="401" >El usuario no genero su JWT</response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPut("{id}")]
    //    [ActionName("ModificarBecarioSAE")]
    //    [ProducesResponseType(typeof(BecariosSAE), StatusCodes.Status201Created)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<BecariosSAE>> ModificarBecarioSAE(int id, BecariosSAE becario)
    //    {
    //        try
    //        {
    //            if (id != becario.id) return BadRequest();

    //            if (await TienePermiso(52))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {

    //                    string usuarioActual = userData.Split(',')[2];
    //                    Dictionary<string, string> parametros = new() {
    //                        { "@id_becario", becario.id.ToString()},
    //                        { "@legajo", becario.legajo},
    //                        { "@alquila", becario.alquila.ToString() },
    //                        { "@fecha_solicitud", becario.fecha_solicitud.ToShortDateString()},
    //                        { "@aceptado", becario.aceptado_inicio?.ToString()??"NULL"},
    //                        { "@puede_pagarle", becario.puede_pagarle?.ToString()??"NULL"},
    //                        { "@activo", becario.activo.ToString()},
    //                        { "@anio_beca", becario.anio_beca.ToString()},
    //                        { "@id_becario_previo", becario.id_becario_previo?.ToString()??"NULL" },
    //                        { "@id_usuario_mod", usuarioActual }
    //                    };
    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_BECAS_Modificar_Becario_SAE", parametros);
    //                    //En este caso sino crea es un error en la BD
    //                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                    return Ok(new BecariosSAE(respuesta.Rows[0]));
    //                }
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR MODIFICANDO BECARIO SAE");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite modificar becario de beca economica
    //    /// </summary>
    //    /// <param name="id">El ID del becario de beca economica</param>
    //    /// <param name="economica">El becario que deseamos modificar, se envia en el Body</param>
    //    /// <returns>Un becario modificado en BD</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Beca/ModificarBecarioEconomica/{id}
    //    ///     BODY:
    //    ///     {
    //    ///         "id": 0,
    //    ///         "entrevista_realizada": true,
    //    ///         "modulo_asignados": 0,
    //    ///         "becario": 
    //    ///         {
    //    ///             "id": 0
    //    ///         }
    //    ///     }
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///         "id": 0,
    //    ///         "entrevista_realizada": true,
    //    ///         "modulo_asignados": 0,
    //    ///         "becario": 
    //    ///         {
    //    ///             "id": 0,
    //    ///             "legajo": "string",
    //    ///             "nombre_becario": "string",
    //    ///             "alquila": true,
    //    ///             "fecha_solicitud": "2024-07-21T14:50:52.102Z",
    //    ///             "aceptado_inicio": true,
    //    ///             "puede_pagarle": true,
    //    ///             "activo": true,
    //    ///             "anio_beca": 0,
    //    ///             "id_becario_previo": 0
    //    ///         }
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve el becario modificado en la BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del becario a modificar </response>
    //    /// <response code="401" >El usuario no genero su JWT</response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPut("{id}")]
    //    [ActionName("ModificarBecarioEconomica")]
    //    [ProducesResponseType(typeof(BecariosSAEEconomica), StatusCodes.Status201Created)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<BecariosSAEEconomica>> ModificarBecarioEconomica(int id, BecariosSAEEconomica economica)
    //    {
    //        try
    //        {
    //            if (id != economica.id) return BadRequest();

    //            if (await TienePermiso(52))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {
    //                    string usuarioActual = userData.Split(',')[2];
    //                    Dictionary<string, string> parametros = new() {
    //                        { "@id_economica", economica.id.ToString()},
    //                        { "@id_becario", economica.becario.id.ToString()},
    //                        { "@entrevista", economica.entrevista_realizada.ToString() },
    //                        { "@modulos", economica.modulos_asignados.ToString() },
    //                        { "@id_usuario_mod", usuarioActual }
    //                    };
    //                    //@id_economica int, @entrevista bit,@id_becario int, @modulos int,@id_usuario_mod int
    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_BECAS_Modificar_Becario_Economica", parametros);
    //                    //En este caso sino crea es un error en la BD
    //                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                    return Ok(new BecariosSAEEconomica(respuesta.Rows[0]));
    //                }
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR MODIFICANDO BECARIO ECONOMICA");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite modificar becario investigacion
    //    /// </summary>
    //    /// <param name="id">El ID del becario investigacion</param>
    //    /// <param name="investiga">El becario que deseamos modificar, se envia en el Body</param>
    //    /// <returns>Un becario modificado en BD</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Beca/ModificarBecarioInvestigacion/{id}
    //    ///     BODY:
    //    ///     {
    //    ///         "id": 0,
    //    ///         "proyecto_investigacion": 
    //    ///         {
    //    ///             "id": 0,
    //    ///         },
    //    ///         "modulos_asignados": 0,
    //    ///         "becario": 
    //    ///         {
    //    ///             "id": 0,
    //    ///         }
    //    ///     }
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///         "id": 0,
    //    ///         "proyecto_investigacion": 
    //    ///         {
    //    ///             "id": 0,
    //    ///             "nombre_proyecto_investigacion": "string",
    //    ///             "activo": true,
    //    ///             "centro_investigacion": "string"
    //    ///         },
    //    ///         "modulos_asignados": 0,
    //    ///         "becario": 
    //    ///         {
    //    ///             "id": 0,
    //    ///             "legajo": "string",
    //    ///             "nombre_becario": "string",
    //    ///             "alquila": true,
    //    ///             "fecha_solicitud": "2024-07-21T16:40:09.230Z",
    //    ///             "aceptado_inicio": true,
    //    ///             "puede_pagarle": true,
    //    ///             "activo": true,
    //    ///             "anio_beca": 0,
    //    ///             "id_becario_previo": 0
    //    ///         }
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve el becario modificado en la BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del becario a modificar </response>
    //    /// <response code="401" >El usuario no genero su JWT</response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPut("{id}")]
    //    [ActionName("ModificarBecarioInvestigacion")]
    //    [ProducesResponseType(typeof(BecariosSAEInvestigacion), StatusCodes.Status201Created)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<BecariosSAEInvestigacion>> ModificarBecarioInvestigacion(int id, BecariosSAEInvestigacion investiga)
    //    {
    //        try
    //        {
    //            if (id != investiga.id) return BadRequest();

    //            if (await TienePermiso(52))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {
    //                    string usuarioActual = userData.Split(',')[2];
    //                    Dictionary<string, string> parametros = new() {
    //                        { "@id_investigacion", investiga.id.ToString()},
    //                        { "@id_becario", investiga.becario.id.ToString()},
    //                        { "@id_proyecto", investiga.proyecto_investigacion?.id.ToString()??"NULL" },
    //                        { "@modulos", investiga.modulos_asignados.ToString() },
    //                        { "@id_usuario_mod", usuarioActual }
    //                    };
    //                    //@id_economica int, @entrevista bit,@id_becario int, @modulos int,@id_usuario_mod int
    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_BECAS_Modificar_Becario_Investigacion", parametros);
    //                    //En este caso sino crea es un error en la BD
    //                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                    return Ok(new BecariosSAEInvestigacion(respuesta.Rows[0]));
    //                }
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR MODIFICANDO BECARIO INVESTIGACION");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite modificar becario de servicio
    //    /// </summary>
    //    /// <param name="id">El ID del becario de servicio</param>
    //    /// <param name="servicio">El becario que deseamos modificar, se envia en el Body</param>
    //    /// <returns>Un becario modificado en BD</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Beca/ModificarBecarioServicio/{id}
    //    ///     BODY:
    //    ///     {
    //    ///         "id": 0,
    //    ///         "servicio": 
    //    ///         {
    //    ///             "id": 0,
    //    ///         },
    //    ///         "modulos_asignados": 0,
    //    ///         "becario": 
    //    ///         {
    //    ///           "id": 0,
    //    ///         }
    //    ///     }
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///         "id": 0,
    //    ///         "servicio": 
    //    ///         {
    //    ///             "id": 0,
    //    ///             "nombre": "string",
    //    ///             "nro_telefono": 0,
    //    ///             "nro_interno_telefono": 0,
    //    ///             "horario_atencion": "string",
    //    ///             "horario_atencion_real": "string",
    //    ///             "email_institucional": "string"
    //    ///         },
    //    ///         "modulos_asignados": 0,
    //    ///         "becario": 
    //    ///         {
    //    ///           "id": 0,
    //    ///           "legajo": "string",
    //    ///           "nombre_becario": "string",
    //    ///           "alquila": true,
    //    ///           "fecha_solicitud": "2024-07-21T17:56:49.671Z",
    //    ///           "aceptado_inicio": true,
    //    ///           "puede_pagarle": true,
    //    ///           "activo": true,
    //    ///           "anio_beca": 0,
    //    ///           "id_becario_previo": 0
    //    ///         }
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve el becario modificado en la BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del becario a modificar </response>
    //    /// <response code="401" >El usuario no genero su JWT</response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPut("{id}")]
    //    [ActionName("ModificarBecarioServicio")]
    //    [ProducesResponseType(typeof(BecariosSAEServicio), StatusCodes.Status201Created)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<BecariosSAEServicio>> ModificarBecarioServicio(int id, BecariosSAEServicio servicio)
    //    {
    //        try
    //        {
    //            if (id != servicio.id) return BadRequest();

    //            if (await TienePermiso(52))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {
    //                    string usuarioActual = userData.Split(',')[2];
    //                    Dictionary<string, string> parametros = new() {
    //                        { "@id_beca_servicio", servicio.id.ToString()},
    //                        { "@id_becario", servicio.becario.id.ToString()},
    //                        { "@id_servicio", servicio.servicio?.id.ToString()??"NULL" },
    //                        { "@modulos", servicio.modulos_asignados.ToString() },
    //                        { "@id_usuario_mod", usuarioActual }
    //                    };
    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_BECAS_Modificar_Becario_Servicio", parametros);
    //                    //En este caso sino crea es un error en la BD
    //                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                    return Ok(new BecariosSAEServicio(respuesta.Rows[0]));
    //                }
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR MODIFICANDO BECARIO SERVICIO");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite modificar becario nacional
    //    /// </summary>
    //    /// <param name="id">El ID del becario nacional</param>
    //    /// <param name="nacional">El becario que deseamos modificar, se envia en el Body</param>
    //    /// <returns>Un becario modificado en BD</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Beca/ModificarBecarioNacional/{id}
    //    ///     BODY:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "legajo": "string",
    //    ///       "nombre_becario": "string",
    //    ///       "tipo_plan": 0,
    //    ///       "regularizacion": true,
    //    ///       "cumplimiento_servicio": true,
    //    ///       "activo": true
    //    ///     }
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "legajo": "string",
    //    ///       "nombre_becario": "string",
    //    ///       "tipo_plan": 0,
    //    ///       "regularizacion": true,
    //    ///       "cumplimiento_servicio": true,
    //    ///       "activo": true
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve el becario modificado en la BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del becario a modificar </response>
    //    /// <response code="401" >El usuario no genero su JWT</response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPut("{id}")]
    //    [ActionName("ModificarBecarioNacional")]
    //    [ProducesResponseType(typeof(BecariosNacionales), StatusCodes.Status201Created)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<BecariosNacionales>> ModificarBecarioNacional(int id, BecariosNacionales nacional)
    //    {
    //        try
    //        {
    //            if (id != nacional.id) return BadRequest();

    //            if (await TienePermiso(146))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {
    //                    string usuarioActual = userData.Split(',')[2];
    //                    Dictionary<string, string> parametros = new() {
    //                        { "@id_becario", nacional.id.ToString()},
    //                        { "@legajo", nacional.legajo},
    //                        { "@tipo_plan", nacional.tipo_plan.ToString() },
    //                        { "@regular", nacional.regularizacion.ToString() },
    //                        { "@cumplio", nacional.cumplimiento_servicio.ToString() },
    //                        { "@anio_beca", nacional.anio_beca.ToString() },
    //                        { "@activo", nacional.activo.ToString() },
    //                        { "@id_usuario_mod", usuarioActual }
    //                    };

    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_BECAS_Modificar_Becario_Nacional", parametros);
    //                    //En este caso sino crea es un error en la BD
    //                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                    return Ok(new BecariosNacionales(respuesta.Rows[0]));
    //                }
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR MODIFICANDO BECARIO NACIONAL");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite modificar un proyecto de investigacion
    //    /// </summary>
    //    /// <param name="id">El ID del proyecto</param>
    //    /// <param name="proyecto">El proyecto que deseamos modificar, se envia en el Body</param>
    //    /// <returns>Un proyecto modificado en BD</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Beca/ModificarProyecto/{id}
    //    ///     BODY:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "nombre_proyecto_investigacion": "string",
    //    ///       "activo": true,
    //    ///       "centro_investigacion": "string"
    //    ///     }
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "nombre_proyecto_investigacion": "string",
    //    ///       "activo": true,
    //    ///       "centro_investigacion": "string"
    //    ///     }
    //    ///       
    //    /// </remarks>
    //    /// <response code="200" >Devuelve el proyecto modificado en la BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del proyecto a modificar </response>
    //    /// <response code="401" >El usuario no genero su JWT</response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPut("{id}")]
    //    [ActionName("ModificarProyecto")]
    //    [ProducesResponseType(typeof(ProyectosInvestigacion), StatusCodes.Status201Created)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<ProyectosInvestigacion>> ModificarProyecto(int id, ProyectosInvestigacion proyecto)
    //    {
    //        try
    //        {
    //            if (id != proyecto.id) return BadRequest();

    //            if (await TienePermiso(142))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {
    //                    string usuarioActual = userData.Split(',')[2];
    //                    Dictionary<string, string> parametros = new() {
    //                        { "@id_proyecto", proyecto.id.ToString()},
    //                        { "@nombre", proyecto.nombre_proyecto_investigacion},
    //                        { "@centro", proyecto.centro_investigacion.ToString() },
    //                        { "@activo", proyecto.activo.ToString() },
    //                    };

    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_BECAS_Modificar_Proyecto_Investigacion", parametros);
    //                    //En este caso sino crea es un error en la BD
    //                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                    return Ok(new ProyectosInvestigacion(respuesta.Rows[0]));
    //                }
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR MODIFICANDO BECARIO NACIONAL");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite modificar una situacion academica
    //    /// </summary>
    //    /// <param name="id">El ID de la situacion</param>
    //    /// <param name="situacion">El proyecto que deseamos modificar, se envia en el Body</param>
    //    /// <returns>Un proyecto modificado en BD</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Beca/ModificarSituacionAcademica/{id}
    //    ///     BODY:
    //    ///     {
    //    ///         "id": 0,
    //    ///         "legajo": "string",
    //    ///         "cursando": true,
    //    ///         "anio_situacion": 0,
    //    ///         "cant_materias_cursadas_anterior": 0,
    //    ///         "cant_materias_aprobadas_periodo_anterior": 0,
    //    ///         "cant_materias_cursando": 0,
    //    ///         "cant_materias_aprobadas_total": 0,
    //    ///         "prom_gral_con_aplazos": 0.00,
    //    ///         "prom_gral_sin_aplazos": 0.00,
    //    ///         "ingreso": 0
    //    ///     }
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///         "id": 0,
    //    ///         "legajo": "string",
    //    ///         "cursando": true,
    //    ///         "anio_situacion": 0,
    //    ///         "cant_materias_cursadas_anterior": 0,
    //    ///         "cant_materias_aprobadas_periodo_anterior": 0,
    //    ///         "cant_materias_cursando": 0,
    //    ///         "cant_materias_aprobadas_total": 0,
    //    ///         "prom_gral_con_aplazos": 0.00,
    //    ///         "prom_gral_sin_aplazos": 0.00,
    //    ///         "ingreso": 0
    //    ///     }
    //    ///       
    //    /// </remarks>
    //    /// <response code="200" >Devuelve la situacion academica modificado en la BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que la situacion a modificar </response>
    //    /// <response code="401" >El usuario no genero su JWT</response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPut("{id}")]
    //    [ActionName("ModificarSituacionAcademica")]
    //    [ProducesResponseType(typeof(SituacionesAcademicas), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<SituacionesAcademicas>> ModificarSituacionAcademica(int id, SituacionesAcademicas situacion)
    //    {
    //        try
    //        {
    //            if (id != situacion.id) return BadRequest();

    //            if (await TienePermiso(124))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {
    //                    string usuarioActual = userData.Split(',')[2];
    //                    Dictionary<string, string> parametros = new() {
    //                        { "@id_situacion", situacion.id.ToString() },
    //                        { "@legajo", situacion.legajo },
    //                        { "@cursando", situacion.cursando.ToString() },
    //                        { "@anio_situacion", situacion.anio_situacion.ToString() },
    //                        { "@cant_cur_anterior", situacion.cant_materias_cursadas_anterior.ToString() },
    //                        { "@cant_aprob_anterior", situacion.cant_materias_aprobadas_periodo_anterior.ToString() },
    //                        { "@cant_cur", situacion.cant_materias_cursando.ToString() },
    //                        { "@cant_aprob", situacion.cant_materias_aprobadas_total.ToString() },
    //                        { "@prom_con_apla", GetDecimalForBase(situacion.prom_gral_con_aplazos) },
    //                        { "@anio_ingre", situacion.ingreso.ToString() },
    //                        { "@prom_sin_apla", GetDecimalForBase(situacion.prom_gral_sin_aplazos) },
    //                        { "@id_usuario_mod", usuarioActual }
    //                    };

    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_BECAS_Modificar_Situacion_Academica", parametros);
    //                    //En este caso sino crea es un error en la BD
    //                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                    return Ok(new SituacionesAcademicas(respuesta.Rows[0]));
    //                }
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR MODIFICANDO SITUACION ACADEMICA");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite crear becario SAE
    //    /// </summary>
    //    /// <param name="becario">El becario que deseamos crear, se envia en el Body</param>
    //    /// <returns>Un becario creado en la base de datos o error</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Beca/CrearBecarioSAE/
    //    ///     BODY:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "legajo": "string",
    //    ///       "alquila": true,
    //    ///       "fecha_solicitud": "2024-07-21T17:56:49.671Z",
    //    ///       "activo": true,
    //    ///       "anio_beca": 0,
    //    ///       "id_becario_previo": 0
    //    ///     }
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "legajo": "string",
    //    ///       "nombre_becario": "string",
    //    ///       "alquila": true,
    //    ///       "fecha_solicitud": "2024-07-21T17:56:49.671Z",
    //    ///       "aceptado_inicio": true,
    //    ///       "puede_pagarle": true,
    //    ///       "activo": true,
    //    ///       "anio_beca": 0,
    //    ///       "id_becario_previo": 0
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="201" >Devuelve el becario creado en la BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT</response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPost]
    //    [ActionName("CrearBecarioSAE")]
    //    [ProducesResponseType(typeof(HorariosSAE), StatusCodes.Status201Created)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<HorariosSAE>> CrearBecarioSAE(BecariosSAE becario)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(51))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {
    //                    string usuarioActual = userData.Split(',')[2];
    //                    Dictionary<string, string> parametros = new() {
    //                        { "@legajo", becario.legajo},
    //                        { "@alquila", becario.alquila.ToString() },
    //                        { "@fecha_solicitud", becario.fecha_solicitud.ToShortDateString()},
    //                        { "@anio_beca", becario.anio_beca.ToString()},
    //                        { "@id_becario_previo", becario.id_becario_previo?.ToString()??"NULL" },
    //                        { "@id_usuario_alta", usuarioActual }
    //                    };
    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_BECAS_Crear_Becario_SAE", parametros);
    //                    //En este caso sino crea es un error en la BD
    //                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                    return Created("Becario SAE Creado", new HorariosSAE(respuesta.Rows[0]));
    //                }
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR CREANDO BECARIO");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite crear un becario de beca economica
    //    /// </summary>
    //    /// <param name="id_becario">El ID del becario  que deseamos crearle una beca economica, se envia en el Body</param>
    //    /// <returns>Un becario de beca economica creado en la base de datos o error</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization y debe haber creado el registro del becario antes
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Beca/CrearBecarioEconomica/{"id_becario"}
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///         "id": 0,
    //    ///         "entrevista_realizada": true,
    //    ///         "modulo_asignados": 0,
    //    ///         "becario": 
    //    ///         {
    //    ///             "id": 0,
    //    ///             "legajo": "string",
    //    ///             "nombre_becario": "string",
    //    ///             "alquila": true,
    //    ///             "fecha_solicitud": "2024-07-21T14:50:52.102Z",
    //    ///             "aceptado_inicio": true,
    //    ///             "puede_pagarle": true,
    //    ///             "activo": true,
    //    ///             "anio_beca": 0,
    //    ///             "id_becario_previo": 0
    //    ///         }
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="201" >Devuelve el becario de beca economica creado en la BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT</response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPost("{id_becario}")]
    //    [ActionName("CrearBecarioEconomica")]
    //    [ProducesResponseType(typeof(BecariosSAEEconomica), StatusCodes.Status201Created)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<BecariosSAEEconomica>> CrearBecarioEconomica(int id_becario)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(51))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {
    //                    string usuarioActual = userData.Split(',')[2];
    //                    Dictionary<string, string> parametros = new() {
    //                        { "@id_becario", id_becario.ToString() },
    //                        { "@id_usuario_alta", usuarioActual }
    //                    };
    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_BECAS_Crear_Becario_Economica", parametros);
    //                    //En este caso sino crea es un error en la BD
    //                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                    return Created("Becario Economica Creado", new BecariosSAEEconomica(respuesta.Rows[0]));
    //                }
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR CREANDO BECARIO ECONOMICA");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite crear un becario de beca de investigacion
    //    /// </summary>
    //    /// <param name="id_becario">El ID del becario  que deseamos crearle una beca de investigacion, se envia en el Body</param>
    //    /// <returns>Un becario de beca de investigacion creado en la base de datos o error</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization y debe haber creado el registro del becario antes
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Beca/CrearBecarioInvestigacion/{"id_becario"}
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///         "id": 0,
    //    ///         "proyecto_investigacion": 
    //    ///         {
    //    ///             "id": 0,
    //    ///             "nombre_proyecto_investigacion": "string",
    //    ///             "activo": true,
    //    ///             "centro_investigacion": "string"
    //    ///         },
    //    ///         "modulos_asignados": 0,
    //    ///         "becario": 
    //    ///         {
    //    ///             "id": 0,
    //    ///             "legajo": "string",
    //    ///             "nombre_becario": "string",
    //    ///             "alquila": true,
    //    ///             "fecha_solicitud": "2024-07-21T16:40:09.230Z",
    //    ///             "aceptado_inicio": true,
    //    ///             "puede_pagarle": true,
    //    ///             "activo": true,
    //    ///             "anio_beca": 0,
    //    ///             "id_becario_previo": 0
    //    ///         }
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="201" >Devuelve el becario de beca de investigacion creado en la BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT</response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPost("{id_becario}")]
    //    [ActionName("CrearBecarioInvestigacion")]
    //    [ProducesResponseType(typeof(BecariosSAEInvestigacion), StatusCodes.Status201Created)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<BecariosSAEInvestigacion>> CrearBecarioInvestigacion(int id_becario)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(51))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {
    //                    string usuarioActual = userData.Split(',')[2];
    //                    Dictionary<string, string> parametros = new() {
    //                        { "@id_becario", id_becario.ToString() },
    //                        { "@id_usuario_alta", usuarioActual }
    //                    };
    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_BECAS_Crear_Becario_Investigacion", parametros);
    //                    //En este caso sino crea es un error en la BD
    //                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                    return Created("Becario de Investigacion Creado", new BecariosSAEInvestigacion(respuesta.Rows[0]));
    //                }
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR CREANDO BECARIO INVESTIGACION");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite crear un becario de beca de servicio
    //    /// </summary>
    //    /// <param name="id_becario">El ID del becario que deseamos crearle una beca de servicio, se envia en el Body</param>
    //    /// <returns>Un becario de beca de servicio creado en la base de datos o error</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization y debe haber creado el registro del becario antes
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Beca/CrearBecarioServicio/{"id_becario"}
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "servicio": 
    //    ///       {
    //    ///           "id": 0,
    //    ///           "nombre": "string",
    //    ///           "nro_telefono": 0,
    //    ///           "nro_interno_telefono": 0,
    //    ///           "horario_atencion": "string",
    //    ///           "horario_atencion_real": "string",
    //    ///           "email_institucional": "string"
    //    ///       },
    //    ///       "modulos_asignados": 0,
    //    ///       "becario": 
    //    ///       {
    //    ///         "id": 0,
    //    ///         "legajo": "string",
    //    ///         "nombre_becario": "string",
    //    ///         "alquila": true,
    //    ///         "fecha_solicitud": "2024-07-21T17:56:49.671Z",
    //    ///         "aceptado_inicio": true,
    //    ///         "puede_pagarle": true,
    //    ///         "activo": true,
    //    ///         "anio_beca": 0,
    //    ///         "id_becario_previo": 0
    //    ///       }
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="201" >Devuelve el becario de beca de servicio creado en la BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT</response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPost("{id_becario}")]
    //    [ActionName("CrearBecarioServicio")]
    //    [ProducesResponseType(typeof(BecariosSAEServicio), StatusCodes.Status201Created)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<BecariosSAEServicio>> CrearBecarioServicio(int id_becario)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(51))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {
    //                    string usuarioActual = userData.Split(',')[2];
    //                    Dictionary<string, string> parametros = new() {
    //                        { "@id_becario", id_becario.ToString() },
    //                        { "@id_usuario_alta", usuarioActual }
    //                    };
    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_BECAS_Crear_Becario_Servicio", parametros);
    //                    //En este caso sino crea es un error en la BD
    //                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                    return Created("Becario de Servicio Creado", new BecariosSAEServicio(respuesta.Rows[0]));
    //                }
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR CREANDO BECARIO SERVICIO");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite crear un becario nacional
    //    /// </summary>
    //    /// <param name="becarios">Es el becario que queremos crear, se envia en el Body</param>
    //    /// <returns>Un becario nacional creado en la base de datos o error</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Beca/CrearBecarioNacional/
    //    ///     
    //    ///     BODY:
    //    ///     {
    //    ///         "id": 0,
    //    ///         "legajo": "string",
    //    ///         "tipo_plan": 0,
    //    ///         "regularizacion": true,
    //    ///         "cumplimiento_servicio": true,
    //    ///         "activo": true
    //    ///     }
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///         "id": 0,
    //    ///         "legajo": "string",
    //    ///         "nombre_becario": "string",
    //    ///         "tipo_plan": 0,
    //    ///         "regularizacion": true,
    //    ///         "cumplimiento_servicio": true,
    //    ///         "activo": true
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="201" >Devuelve el becario nacional creado en la BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT</response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPost]
    //    [ActionName("CrearBecarioNacional")]
    //    [ProducesResponseType(typeof(BecariosNacionales), StatusCodes.Status201Created)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<BecariosNacionales>> CrearBecarioNacional(BecariosNacionales becarios)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(56))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {
    //                    string usuarioActual = userData.Split(',')[2];
    //                    Dictionary<string, string> parametros = new() {
    //                        { "@legajo", becarios.legajo },
    //                        { "@tipo_plan", becarios.tipo_plan.ToString() },
    //                        { "@regular", becarios.regularizacion.ToString() },
    //                        { "@cumplio", becarios.cumplimiento_servicio.ToString() },
    //                        { "@anio_beca", becarios.anio_beca.ToString() },
    //                        { "@activo", becarios.activo.ToString() },
    //                        { "@id_usuario_alta", usuarioActual }
    //                    };
    //                    //@tipo_plan int,@regular bit, @cumplio bit, @anio_beca int, @activo bit,@id_usuario_alta int
    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_BECAS_Crear_Becario_Nacional", parametros);
    //                    //En este caso sino crea es un error en la BD
    //                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                    return Created("Becario Nacional Creado", new BecariosNacionales(respuesta.Rows[0]));
    //                }
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR CREANDO BECARIO NACIONAL");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite crear un proyecto de investigacion
    //    /// </summary>
    //    /// <param name="proyect">El proyecto de investigacion que deseamos crear, se envia en el Body</param>
    //    /// <returns>Un proyecto de investigacion creado en la base de datos o error</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization.
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Beca/CrearProyectoInvestigacion/
    //    ///     
    //    ///     BODY:
    //    ///     {
    //    ///         "id": 0,
    //    ///         "nombre_proyecto_investigacion": "string",
    //    ///         "activo": true,
    //    ///         "centro_investigacion": "string"
    //    ///     }
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///         "id": 0,
    //    ///         "nombre_proyecto_investigacion": "string",
    //    ///         "activo": true,
    //    ///         "centro_investigacion": "string"
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="201" >Devuelve el proyecto de investigacion creado en la BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT</response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPost]
    //    [ActionName("CrearProyectoInvestigacion")]
    //    [ProducesResponseType(typeof(ProyectosInvestigacion), StatusCodes.Status201Created)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<ProyectosInvestigacion>> CrearProyectoInvestigacion(ProyectosInvestigacion proyect)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(141))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {
    //                    string usuarioActual = userData.Split(',')[2];
    //                    Dictionary<string, string> parametros = new() {
    //                        { "@nombre", proyect.nombre_proyecto_investigacion },
    //                        { "@activo", proyect.activo.ToString() },
    //                        { "@centro_inv", proyect.centro_investigacion }
    //                    };
    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_BECAS_Crear_Proyecto_Investigacion", parametros);
    //                    //En este caso sino crea es un error en la BD
    //                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                    return Created("Proyecto de Investigacion Creado", new ProyectosInvestigacion(respuesta.Rows[0]));
    //                }
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR CREANDO PROYECTO DE INVESTIGACION");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite crear una situacion academica
    //    /// </summary>
    //    /// <param name="situacion">La situacion academica que deseamos crear, se envia en el Body</param>
    //    /// <returns>Una situacion academica creado en la base de datos o error</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization.
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Beca/CrearSituacionAcademica/
    //    ///     
    //    ///     BODY:
    //    ///     {
    //    ///         "id": 0,
    //    ///         "legajo": "string",
    //    ///         "cursando": true,
    //    ///         "anio_situacion": 0,
    //    ///         "cant_materias_cursadas_anterior": 0,
    //    ///         "cant_materias_aprobadas_periodo_anterior": 0,
    //    ///         "cant_materias_cursando": 0,
    //    ///         "cant_materias_aprobadas_total": 0,
    //    ///         "prom_gral_con_aplazos": 0.00,
    //    ///         "prom_gral_sin_aplazos": 0.00,
    //    ///         "ingreso": 0
    //    ///     }
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///         "id": 0,
    //    ///         "legajo": "string",
    //    ///         "cursando": true,
    //    ///         "anio_situacion": 0,
    //    ///         "cant_materias_cursadas_anterior": 0,
    //    ///         "cant_materias_aprobadas_periodo_anterior": 0,
    //    ///         "cant_materias_cursando": 0,
    //    ///         "cant_materias_aprobadas_total": 0,
    //    ///         "prom_gral_con_aplazos": 0.00,
    //    ///         "prom_gral_sin_aplazos": 0.00,
    //    ///         "ingreso": 0
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="201" >Devuelve la situacion academica creado en la BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT</response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPost]
    //    [ActionName("CrearSituacionAcademica")]
    //    [ProducesResponseType(typeof(SituacionesAcademicas), StatusCodes.Status201Created)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<SituacionesAcademicas>> CrearSituacionAcademica(SituacionesAcademicas situacion)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(123))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {
    //                    string usuarioActual = userData.Split(',')[2];

    //                    Dictionary<string, string> parametros = new() {
    //                        { "@legajo", situacion.legajo },
    //                        { "@cursando", situacion.cursando.ToString() },
    //                        { "@anio_situacion", situacion.anio_situacion.ToString() },
    //                        { "@cant_cur_anterior", situacion.cant_materias_cursadas_anterior.ToString() },
    //                        { "@cant_aprob_anterior", situacion.cant_materias_aprobadas_periodo_anterior.ToString() },
    //                        { "@cant_cur", situacion.cant_materias_cursando.ToString() },
    //                        { "@cant_aprob", situacion.cant_materias_aprobadas_total.ToString() },
    //                        { "@prom_con_apla", GetDecimalForBase(situacion.prom_gral_con_aplazos) },
    //                        { "@anio_ingre", situacion.ingreso.ToString() },
    //                        { "@prom_sin_apla", GetDecimalForBase(situacion.prom_gral_sin_aplazos) },
    //                        { "@id_usuario_alta", usuarioActual }
    //                    };
    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_BECAS_Crear_Situacion_Academica", parametros);
    //                    //En este caso sino crea es un error en la BD
    //                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                    return Created("Situacion Academica Creada", new SituacionesAcademicas(respuesta.Rows[0]));
    //                }
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR CREANDO SITUACION ACEDEMICA");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite validar si el perfil tiene permiso en la BD para ejecutar este endpoint
    //    /// </summary>
    //    /// <param name="id_funcion">Es la funcion que queremos validar </param>
    //    /// <returns> True = Tiene permisos || False = No tiene permisos </returns>
    //    private async Task<bool> TienePermiso(int id_funcion)
    //    {
    //        string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //        if (userData == null || userData == "NO DATA") return false;

    //        int id_perfil;
    //        try { id_perfil = int.Parse(userData.Split(',')[1]); }
    //        catch (Exception) { return false; }

    //        PerfilesController p = new();
    //        return await p.TienePermiso(_config, id_perfil, id_funcion);
    //    }
    //    private string GetDecimalForBase(decimal valor)
    //    {
    //        valor = Math.Round(valor * 100, 0);
    //        return int.Parse(valor.ToString()).ToString();
    //    }
    //}
}
