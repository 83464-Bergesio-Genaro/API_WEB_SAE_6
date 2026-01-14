using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Security.Claims;
using System.Security.Cryptography;

namespace API_WEB_SAE_6.Controllers
{
    /// <summary>
    /// Es el controlador para los becarios del area y nacionales
    /// </summary>
    [EnableCors("CorsRules")]
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class DeporteController : Controller
    {
        /// <summary>
        /// EN: The logger functions as a register of the exception that happen in the runtime. <br/>
        /// ES: El logger funciona como el registro de excpciones que pasan en tiempo de ejecuccion <br/>
        /// </summary>
        private readonly Logger _logger = new();

        private readonly IConfiguration _config;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public DeporteController(IConfiguration config)
        {
            _config = config;
        }
        /// <summary>
        /// Recupera todos los deportes
        /// </summary>
        /// <returns>Un listado de todos los deportes</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Deporte/ObtenerDeportesCompleto/
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "id": 0,
        ///         "nombre": "string",
        ///         "activo": true
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado completo de los deportes </response>
        /// <response code="204" >No se encontro ningun deporte </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerDeportesCompleto")]
        [ProducesResponseType(typeof(IEnumerable<Deportes>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Deportes>>> ObtenerDeportesCompleto()
        {
            try
            {
                if (await TienePermiso(20))
                {
                    DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_DEPORTES_Listar_Deportes_Completo");
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                    List<Deportes> listadoDeportes = new();
                    foreach (DataRow row in respuesta.Rows)
                    {
                        Deportes becario = new(row);
                        listadoDeportes.Add(becario);
                    }
                    return Ok(listadoDeportes);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR LISTANDO DEPORTES");
                return BadRequest();
            }
        }
        /// <summary>
        /// Recupera todos los deportes activos
        /// </summary>
        /// <returns>Un listado de todos los deportes activos</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Deporte/ObtenerDeportesActivos/
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "id": 0,
        ///         "nombre": "string",
        ///         "activo": true
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado de los deportes activos </response>
        /// <response code="204" >No se encontro ningun deporte </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerDeportesActivos")]
        [ProducesResponseType(typeof(IEnumerable<Deportes>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Deportes>>> ObtenerDeportesActivos()
        {
            try
            {
                //El numero de funcion es: 20
                if (await TienePermiso(20))
                {
                    DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_DEPORTES_Listar_Deportes_Activo");
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                    List<Deportes> listadoDeportes = new();
                    foreach (DataRow row in respuesta.Rows)
                    {
                        Deportes becarios = new(row);
                        listadoDeportes.Add(becarios);
                    }
                    return Ok(listadoDeportes);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR LISTANDO DEPORTES ACTIVOS");
                return BadRequest();
            }
        }
        /// <summary>
        /// Busca deportes por ID
        /// </summary>
        /// <param name="id_deporte">Es el identificador del deporte en la BD</param>
        /// <returns>Un deporte encontrado por ID</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Deporte/ObtenerDeportesXId/{id_deporte}
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "nombre": "string",
        ///       "activo": true
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un deporte </response>
        /// <response code="204" >No se encontro ningun deporte </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{id_deporte}")]
        [ActionName("ObtenerDeportesXId")]
        [ProducesResponseType(typeof(Deportes), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Deportes>> ObtenerDeportesXId(int id_deporte)
        {
            try
            {
                if (await TienePermiso(20))
                {
                    Dictionary<string, string> parametros = new()
                    {
                        { "@id_deporte", id_deporte.ToString() }
                    };
                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Buscar_Deporte_Id", parametros);
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                    Deportes becario = new(respuesta.Rows[0]);
                    return Ok(becario);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR BUSCANDO DEPORTES");
                return BadRequest();
            }
        }
        /// <summary>
        /// Recupera todos los espacios deportivos
        /// </summary>
        /// <returns>Un listado de todos los espacios deportivos</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Deporte/ObtenerEspDeportivoCompleto/
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "id": 0,
        ///         "nombre": "string",
        ///         "domicilio": "string",
        ///         "activo": true,
        ///         "url_maps": "string"
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado completo de los espacios deportivos </response>
        /// <response code="204" >No se encontro ningun espacio deportivo </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerEspDeportivoCompleto")]
        [ProducesResponseType(typeof(IEnumerable<EspaciosDeportivos>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EspaciosDeportivos>>> ObtenerEspDeportivoCompleto()
        {
            try
            {
                if (await TienePermiso(42))
                {
                    DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_DEPORTES_Listar_Espacios_Deportivos_Completo");
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                    List<EspaciosDeportivos> listadoEspacios = new();
                    foreach (DataRow row in respuesta.Rows)
                    {
                        EspaciosDeportivos espacio = new(row);
                        listadoEspacios.Add(espacio);
                    }
                    return Ok(listadoEspacios);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR LISTANDO ESPACIOS DEPORTIVOS");
                return BadRequest();
            }
        }
        /// <summary>
        /// Recupera todos los espacios deportivos activos
        /// </summary>
        /// <returns>Un listado de todos los espacios deportivos activos</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Deporte/ObtenerEspDeportivoActivos/
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "id": 0,
        ///         "nombre": "string",
        ///         "domicilio": "string",
        ///         "activo": true,
        ///         "url_maps": "string"
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado de los deportes activos </response>
        /// <response code="204" >No se encontro ningun deporte </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerEspDeportivoActivos")]
        [ProducesResponseType(typeof(IEnumerable<EspaciosDeportivos>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EspaciosDeportivos>>> ObtenerEspDeportivoActivos()
        {
            try
            {
                if (await TienePermiso(42))
                {
                    DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_DEPORTES_Listar_Espacios_Deportivos_Activo");
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                    List<EspaciosDeportivos> listadoEspacio = new();
                    foreach (DataRow row in respuesta.Rows)
                    {
                        EspaciosDeportivos espacios = new(row);
                        listadoEspacio.Add(espacios);
                    }
                    return Ok(listadoEspacio);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR LISTANDO ESPACIOS DEPORTIVOS ACTIVOS");
                return BadRequest();
            }
        }
        /// <summary>
        /// Busca espacios por ID
        /// </summary>
        /// <param name="id_espacio">Es el identificador del espacio deportivo en la BD</param>
        /// <returns>Un espacio deportivo encontrado por ID</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Deporte/ObtenerEspDeportivoXId/{id_espacio}
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "nombre": "string",
        ///       "domicilio": "string",
        ///       "activo": true,
        ///       "url_maps": "string"
        ///     }
        ///     
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un espacio deportivo </response>
        /// <response code="204" >No se encontro ningun espacio deportivo </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{id_espacio}")]
        [ActionName("ObtenerEspDeportivoXId")]
        [ProducesResponseType(typeof(EspaciosDeportivos), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EspaciosDeportivos>> ObtenerEspDeportivoXId(int id_espacio)
        {
            try
            {
                if (await TienePermiso(42))
                {
                    Dictionary<string, string> parametros = new()
                    {
                        { "@id_espacio", id_espacio.ToString() }
                    };
                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Buscar_Espacio_Deportivos_Id", parametros);
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                    EspaciosDeportivos espacio = new(respuesta.Rows[0]);
                    return Ok(espacio);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR BUSCANDO DOCENTES DEPORTIVOS");
                return BadRequest();
            }
        }
        /// <summary>
        /// Recupera todos los docentes deportivos
        /// </summary>
        /// <returns>Un listado de todos los docentes deportivos</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Deporte/ObtenerDocentesDeportivos/
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "cuil": "string",
        ///         "nombres": "string",
        ///         "apellidos": "string",
        ///         "activo": true,
        ///         "fecha_nacimiento": "2024-07-25T18:44:33.234Z"
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado completo de los espacios deportivos </response>
        /// <response code="204" >No se encontro ningun espacio deportivo </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerDocentesDeportivos")]
        [ProducesResponseType(typeof(IEnumerable<DocentesDeportivos>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DocentesDeportivos>>> ObtenerDocentesDeportivos()
        {
            try
            {
                if (await TienePermiso(36))
                {
                    DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_DEPORTES_Listar_Docente_Deporte_Completo");
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                    List<DocentesDeportivos> listadoDocentes = new();
                    foreach (DataRow row in respuesta.Rows)
                    {
                        DocentesDeportivos docentes = new(row);
                        listadoDocentes.Add(docentes);
                    }
                    return Ok(listadoDocentes);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR LISTANDO DOCENTES DEPORTIVOS");
                return BadRequest();
            }
        }
        /// <summary>
        /// Recupera todos los espacios deportivos activos
        /// </summary>
        /// <returns>Un listado de todos los espacios deportivos activos</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Deporte/ObtenerDocentesDeportivosActivos/
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "cuil": "string",
        ///         "nombres": "string",
        ///         "apellidos": "string",
        ///         "activo": true,
        ///         "fecha_nacimiento": "2024-07-25T18:44:33.234Z"
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado de los docentes activos </response>
        /// <response code="204" >No se encontro ningun docente </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerDocentesDeportivosActivos")]
        [ProducesResponseType(typeof(IEnumerable<DocentesDeportivos>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DocentesDeportivos>>> ObtenerDocentesDeportivosActivos()
        {
            try
            {
                if (await TienePermiso(36))
                {
                    DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_DEPORTES_Listar_Docente_Deporte_Activos");
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                    List<DocentesDeportivos> listadoDeportivo = new();
                    foreach (DataRow row in respuesta.Rows)
                    {
                        DocentesDeportivos docente = new(row);
                        listadoDeportivo.Add(docente);
                    }
                    return Ok(listadoDeportivo);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR LISTANDO ESPACIOS DEPORTIVOS ACTIVOS");
                return BadRequest();
            }
        }
        /// <summary>
        /// Busca docentes por CUIL
        /// </summary>
        /// <param name="cuil">Es el identificador del docente en la BD</param>
        /// <returns>Un docente encontrado por su CUIL</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Deporte/ObtenerDocenteDeportivoXCuil/{cuil}
        ///     
        ///     RESPONSE:
        ///     {
        ///       "cuil": "string",
        ///       "nombres": "string",
        ///       "apellidos": "string",
        ///       "activo": true,
        ///       "fecha_nacimiento": "2024-07-25T18:44:33.234Z"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un docente deportivo </response>
        /// <response code="204" >No se encontro ningun docente </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{cuil}")]
        [ActionName("ObtenerDocenteDeportivoXCuil")]
        [ProducesResponseType(typeof(DocentesDeportivos), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DocentesDeportivos>> ObtenerDocenteDeportivoXCuil(string cuil)
        {
            try
            {
                if (await TienePermiso(36))
                {
                    Dictionary<string, string> parametros = new()
                    {
                        { "@cuil_doc", cuil }
                    };
                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Buscar_Docente_Deportivo_Cuil", parametros);
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                    DocentesDeportivos docente = new(respuesta.Rows[0]);
                    return Ok(docente);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR BUSCANDO DOCENTE DEPORTIVOS");
                return BadRequest();
            }
        }
        /// <summary>
        /// Recupera todos los deportistas
        /// </summary>
        /// <returns>Un listado de todos los deportistas</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Deporte/ObtenerDeportistasCompleto/
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "id": 0,
        ///         "legajo": "string",
        ///         "nombre_deportista": "string",
        ///         "habilitado_deportado": true,
        ///         "vencimiento_ficha": "2024-07-25T22:29:35.186Z",
        ///         "habilitado_deporte": true
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado completo de deportistas</response>
        /// <response code="204" >No se encontro ningun deportista </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerDeportistasCompleto")]
        [ProducesResponseType(typeof(IEnumerable<Deportista>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Deportista>>> ObtenerDeportistasCompleto()
        {
            try
            {
                if (await TienePermiso(33))
                {
                    DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_DEPORTES_Listar_Deportistas_Completo");
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                    List<Deportista> listadoDeportistas = new();
                    foreach (DataRow row in respuesta.Rows)
                    {
                        Deportista deportista = new(row);
                        listadoDeportistas.Add(deportista);
                    }
                    return Ok(listadoDeportistas);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR LISTANDO DEPORTISTAS");
                return BadRequest();
            }
        }
        /// <summary>
        /// Recupera todos los deportistas habilitados
        /// </summary>
        /// <returns>Un listado de todos los deportistas habilitados</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Deporte/ObtenerDeportistasHabilitados/
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "id": 0,
        ///         "legajo": "string",
        ///         "nombre_deportista": "string",
        ///         "habilitado_deportado": true,
        ///         "vencimiento_ficha": "2024-07-25T22:29:35.186Z",
        ///         "habilitado_deporte": true
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado completo de deportistas habilitados</response>
        /// <response code="204" >No se encontro ningun deportista </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerDeportistasHabilitados")]
        [ProducesResponseType(typeof(IEnumerable<Deportista>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Deportista>>> ObtenerDeportistasHabilitados()
        {
            try
            {
                if (await TienePermiso(33))
                {
                    DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_DEPORTES_Listar_Deportistas_Habilitados");
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                    List<Deportista> listadoDeportistas = new();
                    foreach (DataRow row in respuesta.Rows)
                    {
                        Deportista deportista = new(row);
                        listadoDeportistas.Add(deportista);
                    }
                    return Ok(listadoDeportistas);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR LISTANDO DEPORTISTAS HABILITADOS");
                return BadRequest();
            }
        }
        /// <summary>
        /// Busca un deportista por ID
        /// </summary>
        /// <param name="id_deportista">Es el identificador del deportista en la BD</param>
        /// <returns>Un deportista que tenga ese ID</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Deporte/ObtenerDeportistasXId/{id_deportista}
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "legajo": "string",
        ///       "nombre_deportista": "string",
        ///       "habilitado_deportado": true,
        ///       "vencimiento_ficha": "2024-07-25T22:29:35.186Z",
        ///       "habilitado_deporte": true
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un deportista</response>
        /// <response code="204" >No se encontro ningun deportista </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{id_deportista}")]
        [ActionName("ObtenerDeportistasXId")]
        [ProducesResponseType(typeof(Deportista), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Deportista>> ObtenerDeportistasXId(int id_deportista)
        {
            try
            {
                if (await TienePermiso(33))
                {
                    Dictionary<string, string> parametros = new()
                    {
                        { "@id_deportista", id_deportista.ToString() }
                    };
                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Buscar_Deportistas_Id", parametros);
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                    Deportista docente = new(respuesta.Rows[0]);
                    return Ok(docente);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR BUSCANDO DEPORTISTA POR ID");
                return BadRequest();
            }
        }
        /// <summary>
        /// Busca un deportista por Legajo
        /// </summary>
        /// <param name="legajo">Es el Legajo del deportista en la UTN FRC</param>
        /// <returns>Un deportista que tenga ese Legajo</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Deporte/ObtenerDeportistasXLegajo/{legajo}
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "legajo": "string",
        ///       "nombre_deportista": "string",
        ///       "habilitado_deportado": true,
        ///       "vencimiento_ficha": "2024-07-25T22:29:35.186Z",
        ///       "habilitado_deporte": true
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un deportista</response>
        /// <response code="204" >No se encontro ningun deportista </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{legajo}")]
        [ActionName("ObtenerDeportistasXLegajo")]
        [ProducesResponseType(typeof(Deportista), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Deportista>> ObtenerDeportistasXLegajo(string legajo)
        {
            try
            {
                if (await TienePermiso(33))
                {
                    Dictionary<string, string> parametros = new()
                    {
                        { "@legajo", legajo }
                    };
                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Buscar_Deportistas_Legajo", parametros);
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                    Deportista docente = new(respuesta.Rows[0]);
                    return Ok(docente);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR BUSCANDO DEPORTISTA POR LEGAJO");
                return BadRequest();
            }
        }
        /// <summary>
        /// Lista los deportistas inscriptos en un deporte
        /// </summary>
        /// <param name="id_deporte">El ID del deporte en la BD</param>
        /// <returns>Un listado de todos los deportistas inscriptos</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Deporte/ObtenerDeportistasXDeporte/{id_deporte}
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "id": 0,
        ///         "legajo": "string",
        ///         "nombre_deportista": "string",
        ///         "habilitado_deportado": true,
        ///         "vencimiento_ficha": "2024-07-25T22:29:35.186Z",
        ///         "habilitado_deporte": true
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado completo de deportistas inscriptos</response>
        /// <response code="204" >No se encontro ningun deportista inscripto</response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{id_deporte}")]
        [ActionName("ObtenerDeportistasXDeporte")]
        [ProducesResponseType(typeof(IEnumerable<Deportista>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Deportista>>> ObtenerDeportistasXDeporte(int id_deporte)
        {
            try
            {
                if (await TienePermiso(33))
                {
                    Dictionary<string, string> parametros = new()
                    {
                        { "@id_deporte", id_deporte.ToString() }
                    };
                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Listar_Deportistas_Deporte", parametros);
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                    List<Deportista> listadoDeportistas = new();
                    foreach (DataRow row in respuesta.Rows)
                    {
                        Deportista deportista = new(row);
                        listadoDeportistas.Add(deportista);
                    }
                    return Ok(listadoDeportistas);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR LISTANDO DEPORTISTAS EN DEPORTE");
                return BadRequest();
            }
        }
        /// <summary>
        /// Busca los deportes inscriptos por deportista
        /// </summary>
        /// <param name="id_deportista">Es el identificador del deportista en la BD</param>
        /// <returns>Un listado de inscripciones que tenga ese ID</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Deporte/ObtenerInscripcionesXDeportista/{id_deportista}
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "id": 0,
        ///         "id_deporte":0,
        ///         "nombre_deporte": "string",
        ///         "fecha_inscripcion": "2024-07-25T23:07:14.763Z"
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado de inscripciones</response>
        /// <response code="204" >No se encontro ninguna inscripcion </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{id_deportista}")]
        [ActionName("ObtenerInscripcionesXDeportista")]
        [ProducesResponseType(typeof(IEnumerable<DeporteXInscripcion>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<DeporteXInscripcion>>> ObtenerInscripcionesXDeportista(int id_deportista)
        {
            try
            {
                if (await TienePermiso(32))
                {
                    Dictionary<string, string> parametros = new()
                    {
                        { "@id_deportista", id_deportista.ToString() }
                    };
                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Buscar_Inscripciones_Deportista", parametros);
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                    List<DeporteXInscripcion> listadoInscripciones = new();
                    foreach (DataRow row in respuesta.Rows)
                    {
                        DeporteXInscripcion deportista = new(row);
                        listadoInscripciones.Add(deportista);
                    }
                    return Ok(listadoInscripciones);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR BUSCANDO INSCRIPCIONES A DEPORTES");
                return BadRequest();
            }
        }
        /// <summary>
        /// Recupera todos los torneos
        /// </summary>
        /// <returns>Un listado de todos los torneos</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Deporte/ObtenerTorneosDeportivos/
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "id": 0,
        ///         "nombre_torneo": "string",
        ///         "fecha_inicio": "2024-07-26T13:12:20.514Z",
        ///         "fecha_fin": "2024-07-26T13:12:20.514Z",
        ///         "fecha_limite_inscripcion": "2024-07-26T13:12:20.514Z",
        ///         "activo": true,
        ///         "id_deporte": 0,
        ///         "nombre_deporte": "string",
        ///         "cuil_responsable": "string",
        ///         "docente_responsable": "string",
        ///         "cupo_jugadores": 0
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado completo de torneos deportivos</response>
        /// <response code="204" >No se encontro ningun torneo </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerTorneosDeportivos")]
        [ProducesResponseType(typeof(IEnumerable<TorneoDeportivo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TorneoDeportivo>>> ObtenerTorneosDeportivos()
        {
            try
            {
                if (await TienePermiso(23))
                {
                    DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_DEPORTES_Listar_Torneos_Completo");
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                    List<TorneoDeportivo> listadoTorneos = new();
                    foreach (DataRow row in respuesta.Rows)
                    {
                        TorneoDeportivo torneos = new(row);
                        listadoTorneos.Add(torneos);
                    }
                    return Ok(listadoTorneos);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR LISTANDO TORNEOS");
                return BadRequest();
            }
        }
        /// <summary>
        /// Recupera todos los torneos activos
        /// </summary>
        /// <returns>Un listado de todos los torneos activos</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Deporte/ObtenerTorneosActivos/
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "id": 0,
        ///         "nombre_torneo": "string",
        ///         "fecha_inicio": "2024-07-26T13:12:20.514Z",
        ///         "fecha_fin": "2024-07-26T13:12:20.514Z",
        ///         "fecha_limite_inscripcion": "2024-07-26T13:12:20.514Z",
        ///         "activo": true,
        ///         "id_deporte": 0,
        ///         "nombre_deporte": "string",
        ///         "cuil_responsable": "string",
        ///         "docente_responsable": "string",
        ///         "cupo_jugadores": 0
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado completo de torneos activos</response>
        /// <response code="204" >No se encontro ningun torneo activo</response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerTorneosActivos")]
        [ProducesResponseType(typeof(IEnumerable<TorneoDeportivo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TorneoDeportivo>>> ObtenerTorneosActivos()
        {
            try
            {
                if (await TienePermiso(23))
                {
                    DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_DEPORTES_Listar_Torneos_Activos");
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                    List<TorneoDeportivo> listadoTorneos = new();
                    foreach (DataRow row in respuesta.Rows)
                    {
                        TorneoDeportivo torneos = new(row);
                        listadoTorneos.Add(torneos);
                    }
                    return Ok(listadoTorneos);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR LISTANDO TORNEOS ACTIVOS");
                return BadRequest();
            }
        }
        /// <summary>
        /// Busca torneos por ID
        /// </summary>
        /// <param name="id_torneo">El ID del torneo en la BD</param>
        /// <returns>Un torneo</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Deporte/ObtenerTorneosXId/{id_torneo}
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "nombre_torneo": "string",
        ///       "fecha_inicio": "2024-07-26T13:12:20.514Z",
        ///       "fecha_fin": "2024-07-26T13:12:20.514Z",
        ///       "fecha_limite_inscripcion": "2024-07-26T13:12:20.514Z",
        ///       "activo": true,
        ///       "id_deporte": 0,
        ///       "nombre_deporte": "string",
        ///       "cuil_responsable": "string",
        ///       "docente_responsable": "string",
        ///       "cupo_jugadores": 0
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Un torneo con este ID</response>
        /// <response code="204" >No se encontro ningun torneo</response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{id_torneo}")]
        [ActionName("ObtenerTorneosXId")]
        [ProducesResponseType(typeof(TorneoDeportivo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TorneoDeportivo>> ObtenerTorneosXId(int id_torneo)
        {
            try
            {
                if (await TienePermiso(23))
                {
                    Dictionary<string, string> parametros = new()
                    {
                        { "@id_torneo", id_torneo.ToString() }
                    };
                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Buscar_Torneos_Id", parametros);
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                    TorneoDeportivo torneo = new(respuesta.Rows[0]);
                    return Ok(torneo);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR BUSCANDO TORNEO POR ID");
                return BadRequest();
            }
        }
        /// <summary>
        /// Busca torneos por ID del deporte
        /// </summary>
        /// <param name="id_deporte">El ID del deporte en la BD</param>
        /// <returns>Un listado de torneos de este deporte</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Deporte/ObtenerTorneosXDeporte/{id_deporte}
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "id": 0,
        ///         "nombre_torneo": "string",
        ///         "fecha_inicio": "2024-07-26T13:12:20.514Z",
        ///         "fecha_fin": "2024-07-26T13:12:20.514Z",
        ///         "fecha_limite_inscripcion": "2024-07-26T13:12:20.514Z",
        ///         "activo": true,
        ///         "id_deporte": 0,
        ///         "nombre_deporte": "string",
        ///         "cuil_responsable": "string",
        ///         "docente_responsable": "string",
        ///         "cupo_jugadores": 0
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Un torneo con este ID</response>
        /// <response code="204" >No se encontro ningun torneo</response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{id_deporte}")]
        [ActionName("ObtenerTorneosXDeporte")]
        [ProducesResponseType(typeof(IEnumerable<TorneoDeportivo>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TorneoDeportivo>>> ObtenerTorneosXDeporte(int id_deporte)
        {
            try
            {
                if (await TienePermiso(23))
                {
                    Dictionary<string, string> parametros = new()
                    {
                        { "@id_deporte", id_deporte.ToString() }
                    };
                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Buscar_Torneos_Deportes", parametros);
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                    List<TorneoDeportivo> listadoTorneos = new();
                    foreach (DataRow row in respuesta.Rows)
                    {
                        TorneoDeportivo torneos = new(row);
                        listadoTorneos.Add(torneos);
                    }
                    return Ok(listadoTorneos);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR BUSCANDO TORNEO POR DEPORTE");
                return BadRequest();
            }
        }
        /// <summary>
        /// Busca deportistas inscriptos en el torneo
        /// </summary>
        /// <param name="id_torneo">El ID del torneo en la BD</param>
        /// <returns>Un listado de deportistas inscriptos</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Deporte/ObtenerDeportistasXTorneo/{id_torneo}
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "id": 0,
        ///         "legajo": "string",
        ///         "nombre_deportista": "string",
        ///         "habilitado_deportado": true,
        ///         "vencimiento_ficha": "2024-07-25T22:29:35.186Z",
        ///         "habilitado_deporte": true
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Un listado de deportistas</response>
        /// <response code="204" >No se encontro ningun deportista inscripto</response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{id_torneo}")]
        [ActionName("ObtenerDeportistasXTorneo")]
        [ProducesResponseType(typeof(IEnumerable<Deportista>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Deportista>>> ObtenerDeportistasXTorneo(int id_torneo)
        {
            try
            {
                if (await TienePermiso(33))
                {
                    Dictionary<string, string> parametros = new()
                    {
                        { "@id_torneo", id_torneo.ToString() }
                    };
                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Buscar_Inscriptos_Torneo", parametros);
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();


                    List<Deportista> listadoDeportista = new();
                    foreach (DataRow row in respuesta.Rows)
                    {
                        Deportista deportista = new(row);
                        listadoDeportista.Add(deportista);
                    }
                    return Ok(listadoDeportista);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR BUSCANDO DEPORTISTAS POR TORNEO");
                return BadRequest();
            }
        }
        /// <summary>
        /// Busca deportistas inscriptos en el torneo
        /// </summary>
        /// <param name="id_deportista">El ID del torneo en la BD</param>
        /// <returns>Un listado de deportistas inscriptos</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Deporte/ObtenerDeportistasXTorneo/{id_deportista}
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "id": 0,
        ///         "nombre_torneo": "string",
        ///         "fecha_inscripcion": "2024-07-26T13:14:24.930Z",
        ///         "nombre_deporte": "string"
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Un listado de deportistas</response>
        /// <response code="204" >No se encontro ningun deportista inscripto</response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{id_deportista}")]
        [ActionName("ObtenerInscTorneoXDeportista")]
        [ProducesResponseType(typeof(IEnumerable<TorneosXInscripcion>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<TorneosXInscripcion>>> ObtenerInscTorneoXDeportista(int id_deportista)
        {
            try
            {
                if (await TienePermiso(32))
                {
                    Dictionary<string, string> parametros = new()
                    {
                        { "@id_deportista", id_deportista.ToString() }
                    };
                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Buscar_Torneos_Deportista", parametros);
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                    List<TorneosXInscripcion> listadoDeportista = new();
                    foreach (DataRow row in respuesta.Rows)
                    {
                        TorneosXInscripcion deportista = new(row);
                        listadoDeportista.Add(deportista);
                    }
                    return Ok(listadoDeportista);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR BUSCANDO INSCRIPCIONES POR DEPORTES");
                return BadRequest();
            }
        }
        /// <summary>
        /// Recupera todos los horarios
        /// </summary>
        /// <returns>Un listado de todos los horarios</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Deporte/ObtenerHorariosCompleto/
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "id": 0,
        ///         "id_espacio_deportivo": 0,
        ///         "espacio_deportivo": "string",
        ///         "id_deporte": 0,
        ///         "nombre_deporte": "string",
        ///         "hora_inicio": "string",
        ///         "hora_fin": "string",
        ///         "activo": true,
        ///         "cuil_docente": "string",
        ///         "docente_responsable": "string",
        ///         "dia": 0
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado completo de horarios</response>
        /// <response code="204" >No se encontro ningun horarios </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerHorariosCompleto")]
        [ProducesResponseType(typeof(IEnumerable<HorarioDeportes>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<HorarioDeportes>>> ObtenerHorariosCompleto()
        {
            try
            {
                if (await TienePermiso(39))
                {
                    DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_DEPORTES_Listar_Horarios_Completo");
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                    List<HorarioDeportes> listadoHorarios = new();
                    foreach (DataRow row in respuesta.Rows)
                    {
                        HorarioDeportes horarios = new(row);
                        listadoHorarios.Add(horarios);
                    }
                    return Ok(listadoHorarios);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR LISTANDO HORARIOS COMPLETO");
                return BadRequest();
            }
        }
        /// <summary>
        /// Recupera los horarios activos
        /// </summary>
        /// <returns>Un listado de los horarios activos</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Deporte/ObtenerHorariosActivos/
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "id": 0,
        ///         "id_espacio_deportivo": 0,
        ///         "espacio_deportivo": "string",
        ///         "id_deporte": 0,
        ///         "nombre_deporte": "string",
        ///         "hora_inicio": "string",
        ///         "hora_fin": "string",
        ///         "activo": true,
        ///         "cuil_docente": "string",
        ///         "docente_responsable": "string",
        ///         "dia": 0
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado de horarios activos</response>
        /// <response code="204" >No se encontro ningun horarios activo</response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerHorariosActivos")]
        [ProducesResponseType(typeof(IEnumerable<HorarioDeportes>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<HorarioDeportes>>> ObtenerHorariosActivos()
        {
            try
            {
                if (await TienePermiso(39))
                {
                    DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_DEPORTES_Listar_Horarios_Activos");
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                    List<HorarioDeportes> listadoHorarios = new();
                    foreach (DataRow row in respuesta.Rows)
                    {
                        HorarioDeportes horarios = new(row);
                        listadoHorarios.Add(horarios);
                    }
                    return Ok(listadoHorarios);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR LISTANDO HORARIOS ACTIVOS");
                return BadRequest();
            }
        }
        /// <summary>
        /// Busca un horario por el ID
        /// </summary>
        /// <param name="id_horario">El ID del horario en la BD</param>
        /// <returns>Un horario con ese ID</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Deporte/ObtenerDeportistasXTorneo/{id_horario}
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "id_espacio_deportivo": 0,
        ///       "espacio_deportivo": "string",
        ///       "id_deporte": 0,
        ///       "nombre_deporte": "string",
        ///       "hora_inicio": "string",
        ///       "hora_fin": "string",
        ///       "activo": true,
        ///       "cuil_docente": "string",
        ///       "docente_responsable": "string",
        ///       "dia": 0
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Un horario con este ID</response>
        /// <response code="204" >No se encontro ningun horario</response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{id_horario}")]
        [ActionName("ObtenerHorariosXId")]
        [ProducesResponseType(typeof(IEnumerable<HorarioDeportes>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<HorarioDeportes>>> ObtenerHorariosXId(int id_horario)
        {
            try
            {
                if (await TienePermiso(39))
                {
                    Dictionary<string, string> parametros = new()
                    {
                        { "@id_horario", id_horario.ToString() }
                    };
                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Buscar_Horarios_Id", parametros);
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                    List<HorarioDeportes> listadoHorario = new();
                    foreach (DataRow row in respuesta.Rows)
                    {
                        HorarioDeportes horario = new(row);
                        listadoHorario.Add(horario);
                    }
                    return Ok(listadoHorario);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR BUSCANDO HORARIO POR ID");
                return BadRequest();
            }
        }
        /// <summary>
        /// Busca un horario por el ID del espacio deportivo
        /// </summary>
        /// <param name="id_espacio">El ID del espacio deportivo en la BD</param>
        /// <returns>Un listado de horarios en ese espacio</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Deporte/ObtenerHorariosXEspacio/{id_espacio}
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "id": 0,
        ///         "id_espacio_deportivo": 0,
        ///         "espacio_deportivo": "string",
        ///         "id_deporte": 0,
        ///         "nombre_deporte": "string",
        ///         "hora_inicio": "string",
        ///         "hora_fin": "string",
        ///         "activo": true,
        ///         "cuil_docente": "string",
        ///         "docente_responsable": "string",
        ///         "dia": 0
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Un listado de horario con este ID espacio</response>
        /// <response code="204" >No se encontro ningun horario</response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{id_espacio}")]
        [ActionName("ObtenerHorariosXEspacio")]
        [ProducesResponseType(typeof(IEnumerable<HorarioDeportes>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<HorarioDeportes>>> ObtenerHorariosXEspacio(int id_espacio)
        {
            try
            {
                if (await TienePermiso(39))
                {
                    Dictionary<string, string> parametros = new()
                    {
                        { "@id_espacio", id_espacio.ToString() }
                    };
                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Buscar_Horarios_Espacio", parametros);
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                    List<HorarioDeportes> listadoHorario = new();
                    foreach (DataRow row in respuesta.Rows)
                    {
                        HorarioDeportes horario = new(row);
                        listadoHorario.Add(horario);
                    }
                    return Ok(listadoHorario);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR BUSCANDO HORARIO POR ID ESPACIO");
                return BadRequest();
            }
        }
        /// <summary>
        /// Busca un horario por el ID del deporte
        /// </summary>
        /// <param name="id_deporte">El ID del deporte en la BD</param>
        /// <returns>Un listado de horarios del deporte</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Deporte/ObtenerHorariosXDeporte/{id_deporte}
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "id": 0,
        ///         "id_espacio_deportivo": 0,
        ///         "espacio_deportivo": "string",
        ///         "id_deporte": 0,
        ///         "nombre_deporte": "string",
        ///         "hora_inicio": "string",
        ///         "hora_fin": "string",
        ///         "activo": true,
        ///         "cuil_docente": "string",
        ///         "docente_responsable": "string",
        ///         "dia": 0
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Un listado de horario con este ID deporte</response>
        /// <response code="204" >No se encontro ningun deporte</response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{id_deporte}")]
        [ActionName("ObtenerHorariosXDeporte")]
        [ProducesResponseType(typeof(IEnumerable<HorarioDeportes>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<HorarioDeportes>>> ObtenerHorariosXDeporte(int id_deporte)
        {
            try
            {
                if (await TienePermiso(39))
                {
                    Dictionary<string, string> parametros = new()
                    {
                        { "@id_deporte", id_deporte.ToString() }
                    };
                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Buscar_Horarios_Deporte", parametros);
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                    List<HorarioDeportes> listadoHorario = new();
                    foreach (DataRow row in respuesta.Rows)
                    {
                        HorarioDeportes horario = new(row);
                        listadoHorario.Add(horario);
                    }
                    return Ok(listadoHorario);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR BUSCANDO HORARIO POR ID DEPORTE");
                return BadRequest();
            }
        }
        /// <summary>
        /// Busca un horario por el CUIL del docente
        /// </summary>
        /// <param name="cuil">El CUIL del docente en la BD</param>
        /// <returns>Un listado de horarios que da el docente</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Deporte/ObtenerHorariosXCUIL/{cuil}
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "id": 0,
        ///         "id_espacio_deportivo": 0,
        ///         "espacio_deportivo": "string",
        ///         "id_deporte": 0,
        ///         "nombre_deporte": "string",
        ///         "hora_inicio": "string",
        ///         "hora_fin": "string",
        ///         "activo": true,
        ///         "cuil_docente": "string",
        ///         "docente_responsable": "string",
        ///         "dia": 0
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Un listado de horarios</response>
        /// <response code="204" >No se encontro ningun horario</response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{cuil}")]
        [ActionName("ObtenerHorariosXCUIL")]
        [ProducesResponseType(typeof(IEnumerable<HorarioDeportes>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<HorarioDeportes>>> ObtenerHorariosXCUIL(string cuil)
        {
            try
            {
                if (await TienePermiso(39))
                {
                    Dictionary<string, string> parametros = new()
                    {
                        { "@cuil", cuil }
                    };
                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Buscar_Horarios_Cuil", parametros);
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                    List<HorarioDeportes> listadoHorario = new();
                    foreach (DataRow row in respuesta.Rows)
                    {
                        HorarioDeportes horario = new(row);
                        listadoHorario.Add(horario);
                    }
                    return Ok(listadoHorario);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR BUSCANDO HORARIO POR CUIL DOCENTE");
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite la modificacion de un deporte
        /// </summary>
        /// <param name="id"> El ID del deporte a modificar</param>
        /// <param name="deporte"> Los datos modificados del deporte</param>
        /// <returns>Un deporte modificado en BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     PUT /api/Deporte/ModificarDeporte/{id}
        ///     BODY:
        ///     {
        ///         "id": 0,
        ///         "nombre": "string",
        ///         "activo": true,
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///         "id": 0,
        ///         "nombre": "string",
        ///         "activo": true,
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve el deporte modificado en BD </response>
        /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del deporte a modificar </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{id}")]
        [ActionName("ModificarDeporte")]
        [ProducesResponseType(typeof(Deportes), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Deportes>> ModificarDeporte(int id, Deportes deporte)
        {
            try
            {
                if (id != deporte.id) return BadRequest();

                if (await TienePermiso(19))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    string usuarioActual = userData.Split(',')[2];
                    Dictionary<string, string> parametros = new() {

                        { "@id_deporte",deporte.id.ToString() },
                        { "@nombre",deporte.nombre.ToString() },
                        { "@activo",deporte.activo.ToString() }
                    };

                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Modificar_Deporte", parametros);

                    //En este caso sino modifica nada es un conflicto en la BD
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                    return Ok(new Deportes(respuesta.Rows[0]));
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR MODIFICANDO DEPORTE");
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite la modificacion de un espacio deportivo
        /// </summary>
        /// <param name="id"> El ID del espacio deportivo a modificar</param>
        /// <param name="espacio"> Los datos modificados del espacio</param>
        /// <returns>Un espacio modificado en BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     PUT /api/Deporte/ModificarEspacioDeportivo/{id}
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "nombre": "string",
        ///       "domicilio": "string",
        ///       "activo": true,
        ///       "url_maps": "string"
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "nombre": "string",
        ///       "domicilio": "string",
        ///       "activo": true,
        ///       "url_maps": "string"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve el espacio deportivo modificado en BD </response>
        /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del espacio a modificar </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{id}")]
        [ActionName("ModificarEspacioDeportivo")]
        [ProducesResponseType(typeof(EspaciosDeportivos), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EspaciosDeportivos>> ModificarEspacioDeportivo(int id, EspaciosDeportivos espacio)
        {
            try
            {
                if (id != espacio.id) return BadRequest();

                if (await TienePermiso(41))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    string usuarioActual = userData.Split(',')[2];
                    Dictionary<string, string> parametros = new() {

                        { "@id_espacio",espacio.id.ToString() },
                        { "@nombre",espacio.nombre.ToString() },
                        { "@activo",espacio.activo.ToString() },
                        { "@domicilio",espacio.activo.ToString() },
                        { "@url_maps",espacio.activo.ToString() }
                    };

                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Modificar_Espacio_Deportivo", parametros);

                    //En este caso sino modifica nada es un conflicto en la BD
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                    return Ok(new EspaciosDeportivos(respuesta.Rows[0]));
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR MODIFICANDO ESPACIO DEPORTIVO");
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite la modificacion de un docente deportivo
        /// </summary>
        /// <param name="cuil"> El CUIL del docente a modificar</param>
        /// <param name="docente"> Los datos modificados del docente</param>
        /// <returns>Un docente modificado en BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     PUT /api/Deporte/ModificarDocenteDeportivo/{cuil}
        ///     BODY:
        ///     {
        ///       "cuil": "string",
        ///       "nombres": "string",
        ///       "apellidos": "string",
        ///       "activo": true,
        ///       "fecha_nacimiento": "2024-07-25T18:44:33.234Z"
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "cuil": "string",
        ///       "nombres": "string",
        ///       "apellidos": "string",
        ///       "activo": true,
        ///       "fecha_nacimiento": "2024-07-25T18:44:33.234Z"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve el docente deportivo modificado en BD </response>
        /// <response code="400" >Ocurre un error en la consulta o el CUIL es diferente que el del docente a modificar </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{cuil}")]
        [ActionName("ModificarDocenteDeportivo")]
        [ProducesResponseType(typeof(DocentesDeportivos), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DocentesDeportivos>> ModificarDocenteDeportivo(string cuil, DocentesDeportivos docente)
        {
            try
            {
                if (cuil != docente.cuil) return BadRequest();

                if (await TienePermiso(35))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    string usuarioActual = userData.Split(',')[2];
                    Dictionary<string, string> parametros = new() {

                        { "@cuil_doc",docente.cuil },
                        { "@nombres",docente.nombres },
                        { "@apellidos",docente.apellidos },
                        { "@activo",docente.activo.ToString() },
                        { "@fecha_nacimiento",docente.fecha_nacimiento.ToString() },
                        { "@id_usuario_mod",usuarioActual }
                    };

                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Modificar_Docente_Deportivo", parametros);

                    //En este caso sino modifica nada es un conflicto en la BD
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                    return Ok(new DocentesDeportivos(respuesta.Rows[0]));
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR MODIFICANDO DOCENTE DEPORTIVO");
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite la modificacion de un deportista
        /// </summary>
        /// <param name="id"> El ID del deportista a modificar</param>
        /// <param name="deportista"> Los datos modificados del deportista</param>
        /// <returns>Un deportista modificado en BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     PUT /api/Deporte/ModificarDeportista/{id}
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "legajo": "string",
        ///       "habilitado_deportado": true,
        ///       "vencimiento_ficha": "2024-07-25T22:29:35.186Z",
        ///       "habilitado_deporte": true
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "legajo": "string",
        ///       "nombre_deportista": "string",
        ///       "habilitado_deportado": true,
        ///       "vencimiento_ficha": "2024-07-25T22:29:35.186Z",
        ///       "habilitado_deporte": true
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve el deportista modificado en BD </response>
        /// <response code="400" >Ocurre un error en la consulta o el CUIL es diferente que el del deportista a modificar </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{id}")]
        [ActionName("ModificarDeportista")]
        [ProducesResponseType(typeof(Deportista), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Deportista>> ModificarDeportista(int id, Deportista deportista)
        {
            try
            {
                if (id != deportista.id) return BadRequest();

                if (await TienePermiso(29))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    string usuarioActual = userData.Split(',')[2];
                    Dictionary<string, string> parametros = new() {

                        { "@id_deportista",deportista.id.ToString() },
                        { "@legajo",deportista.legajo },
                        { "@habilitado",deportista.habilitado_deporte.ToString() },
                        { "@vencimiento_ficha",deportista.vencimiento_ficha.ToString() },
                        { "@id_usuario_mod",usuarioActual }
                    };

                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Modificar_Deportista", parametros);

                    //En este caso sino modifica nada es un conflicto en la BD
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                    return Ok(new Deportista(respuesta.Rows[0]));
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR MODIFICANDO DEPORTISTA");
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite la modificacion de un torneo
        /// </summary>
        /// <param name="id"> El ID del torneo a modificar</param>
        /// <param name="torneo"> Los datos modificados del torneo</param>
        /// <returns>Un torneo modificado en BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     PUT /api/Deporte/ModificarTorneo/{id}
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "nombre_torneo": "string",
        ///       "fecha_inicio": "2024-07-26T13:12:20.514Z",
        ///       "fecha_fin": "2024-07-26T13:12:20.514Z",
        ///       "fecha_limite_inscripcion": "2024-07-26T13:12:20.514Z",
        ///       "activo": true,
        ///       "id_deporte": 0,
        ///       "nombre_deporte": "string",
        ///       "cuil_responsable": "string",
        ///       "docente_responsable": "string",
        ///       "cupo_jugadores": 0
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "nombre_torneo": "string",
        ///       "fecha_inicio": "2024-07-26T13:12:20.514Z",
        ///       "fecha_fin": "2024-07-26T13:12:20.514Z",
        ///       "fecha_limite_inscripcion": "2024-07-26T13:12:20.514Z",
        ///       "activo": true,
        ///       "id_deporte": 0,
        ///       "nombre_deporte": "string",
        ///       "cuil_responsable": "string",
        ///       "docente_responsable": "string",
        ///       "cupo_jugadores": 0
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve el torneo modificado en BD </response>
        /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del torneo a modificar </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{id}")]
        [ActionName("ModificarTorneo")]
        [ProducesResponseType(typeof(TorneoDeportivo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TorneoDeportivo>> ModificarTorneo(int id, TorneoDeportivo torneo)
        {
            try
            {
                if (id != torneo.id) return BadRequest();

                if (await TienePermiso(22))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    string usuarioActual = userData.Split(',')[2];
                    Dictionary<string, string> parametros = new() {

                        { "@id_torneo",torneo.id.ToString() },
                        { "@nombre_torneo",torneo.nombre_torneo },
                        { "@fecha_ini",torneo.fecha_inicio.ToString() },
                        { "@fecha_fin",torneo.fecha_fin.ToString() },
                        { "@fecha_limite",torneo.fecha_limite_inscripcion.ToString() },
                        { "@activo",torneo.activo.ToString() },
                        { "@id_deporte",torneo.id_deporte.ToString()},
                        { "@cuil_docente",torneo.cuil_responsable },
                        { "@cupo",torneo.cupo_jugadores.ToString()},
                        { "@id_usuario_mod",usuarioActual }
                    };
                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Modificar_Torneo", parametros);

                    //En este caso sino modifica nada es un conflicto en la BD
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                    return Ok(new TorneoDeportivo(respuesta.Rows[0]));
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR MODIFICANDO TORNEO");
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite la modificacion de un horario depotivo
        /// </summary>
        /// <param name="id"> El ID del horario a modificar</param>
        /// <param name="horario"> Los datos modificados del horario</param>
        /// <returns>Un horario modificado en BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     PUT /api/Deporte/ModificarHorario/{id}
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "id_espacio_deportivo": 0,
        ///       "espacio_deportivo": "Campus de la UTN",
        ///       "id_deporte": 0,
        ///       "nombre_deporte": "Futbol 11",
        ///       "hora_inicio": "10:00:00",
        ///       "hora_fin": "12:00:00",
        ///       "activo": true,
        ///       "cuil_docente": "20422461222",
        ///       "docente_responsable": "Genaro Rafael, Bergesio",
        ///       "dia": 0
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "id_espacio_deportivo": 0,
        ///       "espacio_deportivo": "Campus de la UTN",
        ///       "id_deporte": 0,
        ///       "nombre_deporte": "Futbol 11",
        ///       "hora_inicio": "10:00:00",
        ///       "hora_fin": "12:00:00",
        ///       "activo": true,
        ///       "cuil_docente": "20422461222",
        ///       "docente_responsable": "Genaro Rafael, Bergesio",
        ///       "dia": 0
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve el horario modificado en BD </response>
        /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del horario a modificar </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{id}")]
        [ActionName("ModificarHorario")]
        [ProducesResponseType(typeof(HorarioDeportes), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HorarioDeportes>> ModificarHorario(int id, HorarioDeportes horario)
        {
            try
            {
                if (id != horario.id) return BadRequest();

                if (await TienePermiso(38))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    string usuarioActual = userData.Split(',')[2];
                    Dictionary<string, string> parametros = new() {

                        { "@id_horario",horario.id.ToString() },
                        { "@id_espacio",horario.id_espacio_deportivo.ToString()},
                        { "@id_deporte",horario.id_deporte.ToString() },
                        { "@hora_ini",horario.hora_inicio },
                        { "@hora_fin",horario.hora_fin },
                        { "@activo",horario.activo.ToString() },
                        { "@cuil_doc",horario.cuil_docente},
                        { "@dia",horario.dia.ToString()}
                    };
                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Modificar_Horario", parametros);

                    //En este caso sino modifica nada es un conflicto en la BD
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                    return Ok(new HorarioDeportes(respuesta.Rows[0]));
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR MODIFICANDO HORARIO");
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite crear deportes
        /// </summary>
        /// <param name="deporte">El deporte que deseamos crear, se envia en el Body</param>
        /// <returns>Un deporte creado en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Deporte/CrearDeporte/
        ///     BODY:
        ///     {
        ///         "id": 0,
        ///         "nombre": "string",
        ///         "activo": true,
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///         "id": 0,
        ///         "nombre": "string",
        ///         "activo": true,
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve el deporte creado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost]
        [ActionName("CrearDeporte")]
        [ProducesResponseType(typeof(Deportes), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Deportes>> CrearDeporte([FromBody] Deportes deporte)
        {
            try
            {
                if (await TienePermiso(18))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        string usuarioActual = userData.Split(',')[2];
                        Dictionary<string, string> parametros = new() {
                            {"@nombre", deporte.nombre },
                            {"@activo", deporte.activo.ToString() }
                        };

                        DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Crear_Deporte", parametros);
                        //En este caso sino crea es un error en la BD
                        if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                        return Created("Deporte Creado", new Deportes(respuesta.Rows[0]));
                    }

                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR CREANDO DEPORTE");
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite crear deportistas
        /// </summary>
        /// <param name="deportista">El deportista que deseamos crear, se envia en el Body</param>
        /// <returns>Un deportista creado en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Deporte/CrearDeportista/
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "legajo": "string",
        ///       "habilitado_deportado": true,
        ///       "vencimiento_ficha": "2024-07-25T22:29:35.186Z",
        ///       "habilitado_deporte": true
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "legajo": "string",
        ///       "nombre_deportista": "string",
        ///       "habilitado_deportado": true,
        ///       "vencimiento_ficha": "2024-07-25T22:29:35.186Z",
        ///       "habilitado_deporte": true
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve el deportista creado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost]
        [ActionName("CrearDeportista")]
        [ProducesResponseType(typeof(Deportista), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Deportista>> CrearDeportista([FromBody] Deportista deportista)
        {
            try
            {
                if (await TienePermiso(28))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        string usuarioActual = userData.Split(',')[2];
                        Dictionary<string, string> parametros = new() {
                            {"@legajo", deportista.legajo },
                            {"@id_usuario_alta", usuarioActual }
                        };

                        DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Crear_Deportista", parametros);
                        //En este caso sino crea es un error en la BD
                        if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                        return Created("Deportista Creado", new Deportista(respuesta.Rows[0]));
                    }

                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR CREANDO DEPORTISTA");
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite crear docente deportivo
        /// </summary>
        /// <param name="docente">El docente deportivo que deseamos crear, se envia en el Body</param>
        /// <returns>Un docente deportivo creado en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Deporte/CrearDocenteDeportivo/
        ///     BODY:
        ///     {
        ///       "cuil": "string",
        ///       "nombres": "string",
        ///       "apellidos": "string",
        ///       "activo": true,
        ///       "fecha_nacimiento": "2024-07-25T18:44:33.234Z"
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "cuil": "string",
        ///       "nombres": "string",
        ///       "apellidos": "string",
        ///       "activo": true,
        ///       "fecha_nacimiento": "2024-07-25T18:44:33.234Z"
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve el docente deportivo creado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost]
        [ActionName("CrearDocenteDeportivo")]
        [ProducesResponseType(typeof(DocentesDeportivos), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DocentesDeportivos>> CrearDocenteDeportivo([FromBody] DocentesDeportivos docente)
        {
            try
            {
                if (await TienePermiso(28))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        string usuarioActual = userData.Split(',')[2];
                        Dictionary<string, string> parametros = new() {
                            {"@cuil_doc", docente.cuil },
                            {"@nombres", docente.nombres },
                            {"@apellidos", docente.apellidos },
                            {"@activo", docente.activo.ToString() },
                            {"@fecha_nacimiento", docente.fecha_nacimiento.ToString() },
                            {"@id_usuario_alta", usuarioActual }
                        };
                        DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Crear_Docente_Deportivo", parametros);
                        //En este caso sino crea es un error en la BD
                        if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                        return Created("Docente Deportivo Creado", new DocentesDeportivos(respuesta.Rows[0]));
                    }

                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR CREANDO DOCENTE DEPORTIVO");
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite crear espacio deportivo
        /// </summary>
        /// <param name="espacio">El espacio deportivo que deseamos crear, se envia en el Body</param>
        /// <returns>Un espacio deportivo creado en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Deporte/CrearEspacioDeportivo/
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "nombre": "string",
        ///       "domicilio": "string",
        ///       "activo": true,
        ///       "url_maps": "string"
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "nombre": "string",
        ///       "domicilio": "string",
        ///       "activo": true,
        ///       "url_maps": "string"
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve el espacio deportivo creado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost]
        [ActionName("CrearEspacioDeportivo")]
        [ProducesResponseType(typeof(EspaciosDeportivos), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EspaciosDeportivos>> CrearEspacioDeportivo([FromBody] EspaciosDeportivos espacio)
        {
            try
            {
                if (await TienePermiso(40))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        string usuarioActual = userData.Split(',')[2];
                        Dictionary<string, string> parametros = new() {
                            {"@nombre", espacio.nombre },
                            {"@domicilio", espacio.domicilio },
                            {"@activo", espacio.activo.ToString() },
                            {"@url_maps", espacio.url_maps },
                        };

                        DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Crear_Espacio_Deportivo", parametros);
                        //En este caso sino crea es un error en la BD
                        if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                        return Created("Espacio Deportivo Creado", new EspaciosDeportivos(respuesta.Rows[0]));
                    }

                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR CREANDO ESPACIO DEPORTIVO");
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite crear horario deportivo
        /// </summary>
        /// <param name="horario">El horario deportivo que deseamos crear, se envia en el Body</param>
        /// <returns>Un horario deportivo creado en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Deporte/CrearHorarioDeportivo/
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "id_espacio_deportivo": 0,
        ///       "espacio_deportivo": "string",
        ///       "id_deporte": 0,
        ///       "nombre_deporte": "string",
        ///       "hora_inicio": "string",
        ///       "hora_fin": "string",
        ///       "activo": true,
        ///       "cuil_docente": "string",
        ///       "docente_responsable": "string",
        ///       "dia": 0
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "id_espacio_deportivo": 0,
        ///       "espacio_deportivo": "string",
        ///       "id_deporte": 0,
        ///       "nombre_deporte": "string",
        ///       "hora_inicio": "string",
        ///       "hora_fin": "string",
        ///       "activo": true,
        ///       "cuil_docente": "string",
        ///       "docente_responsable": "string",
        ///       "dia": 0
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve el horario deportivo creado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost]
        [ActionName("CrearHorarioDeportivo")]
        [ProducesResponseType(typeof(HorarioDeportes), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HorarioDeportes>> CrearHorarioDeportivo([FromBody] HorarioDeportes horario)
        {
            try
            {
                if (await TienePermiso(37))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        string usuarioActual = userData.Split(',')[2];
                        Dictionary<string, string> parametros = new() {
                            {"@id_espacio", horario.id_espacio_deportivo.ToString() },
                            {"@id_deporte", horario.id_deporte.ToString() },
                            {"@hora_ini", horario.hora_inicio.ToString() },
                            {"@hora_fin", horario.hora_fin.ToString() },
                            {"@activo", horario.activo.ToString() },
                            {"@cuil_doc", horario.cuil_docente.ToString() },
                            {"@dia", horario.dia.ToString() },
                        };
                        DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Crear_Horario_Deporte", parametros);
                        //En este caso sino crea es un error en la BD
                        if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                        return Created("Horario Deportivo Creado", new HorarioDeportes(respuesta.Rows[0]));
                    }

                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR CREANDO HORARIO DEPORTIVO");
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite crear torneo
        /// </summary>
        /// <param name="torneo">El torneo que deseamos crear, se envia en el Body</param>
        /// <returns>Un torneo creado en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Deporte/CrearTorneo/
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "nombre_torneo": "string",
        ///       "fecha_inicio": "2024-07-26T13:12:20.514Z",
        ///       "fecha_fin": "2024-07-26T13:12:20.514Z",
        ///       "fecha_limite_inscripcion": "2024-07-26T13:12:20.514Z",
        ///       "activo": true,
        ///       "id_deporte": 0,
        ///       "cuil_responsable": "string",
        ///       "cupo_jugadores": 0
        ///     }
        ///       
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "nombre_torneo": "string",
        ///       "fecha_inicio": "2024-07-26T13:12:20.514Z",
        ///       "fecha_fin": "2024-07-26T13:12:20.514Z",
        ///       "fecha_limite_inscripcion": "2024-07-26T13:12:20.514Z",
        ///       "activo": true,
        ///       "id_deporte": 0,
        ///       "nombre_deporte": "string",
        ///       "cuil_responsable": "string",
        ///       "docente_responsable": "string",
        ///       "cupo_jugadores": 0
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve el torneo creado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost]
        [ActionName("CrearTorneo")]
        [ProducesResponseType(typeof(TorneoDeportivo), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TorneoDeportivo>> CrearTorneo([FromBody] TorneoDeportivo torneo)
        {
            try
            {
                if (await TienePermiso(21))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        string usuarioActual = userData.Split(',')[2];
                        Dictionary<string, string> parametros = new() {
                            {"@nombre_torneo", torneo.nombre_torneo },
                            {"@fecha_ini", torneo.fecha_inicio.ToString() },
                            {"@fecha_fin", torneo.fecha_fin.ToString() },
                            {"@fecha_limite", torneo.fecha_limite_inscripcion.ToString() },
                            {"@activo", torneo.activo.ToString() },
                            {"@id_deporte", torneo.id_deporte.ToString() },
                            {"@cuil_docente", torneo.cuil_responsable },
                            {"@cupo", torneo.cupo_jugadores.ToString() },
                            {"@id_usuario_alta", usuarioActual }
                        };
                        DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Crear_Torneo", parametros);
                        //En este caso sino crea es un error en la BD
                        if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                        return Created("Torneo Creado", new TorneoDeportivo(respuesta.Rows[0]));
                    }

                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR CREANDO TORNEO");
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite crear inscripcion a torneo
        /// </summary>
        /// <param name="id_torneo">El ID del torneo al que queremos inscribir al deportista</param>
        /// <param name="id_deportista">El ID del deportista</param>
        /// <returns>Una inscripcion creada en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Deporte/CrearInscripcionTorneo/{id_torneo}/{id_deportista}
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "nombre_torneo": "string",
        ///       "fecha_inscripcion": "2024-07-26T13:14:24.930Z",
        ///       "nombre_deporte": "string"
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "nombre_torneo": "string",
        ///       "fecha_inscripcion": "2024-07-26T13:14:24.930Z",
        ///       "nombre_deporte": "string"
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve la inscripcion creada en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost("{id_torneo}/{id_deportista}")]
        [ActionName("CrearInscripcionTorneo")]
        [ProducesResponseType(typeof(TorneosXInscripcion), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TorneosXInscripcion>> CrearInscripcionTorneo(int id_torneo, int id_deportista)
        {
            try
            {
                if (await TienePermiso(24))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        string usuarioActual = userData.Split(',')[2];
                        Dictionary<string, string> parametros = new() {
                            {"@id_torneo", id_torneo.ToString() },
                            {"@id_inscripto", id_deportista.ToString() },
                        };
                        DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Crear_Inscripcion_Torneo", parametros);
                        //En este caso sino crea es un error en la BD
                        if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                        return Created("Inscripcion a torneo Creado", new TorneosXInscripcion(respuesta.Rows[0]));
                    }

                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR CREANDO INSCRIPCION A TORNEO");
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite crear inscripcion a deporte
        /// </summary>
        /// <param name="id_deporte">El ID del deporte al que queremos inscribir al deportista</param>
        /// <param name="id_deportista">El ID del deportista</param>
        /// <returns>Una inscripcion creada en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Deporte/CrearInscripcionDeporte/{id_deporte}/{id_deportista}
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "nombre_deporte": "string",
        ///       "fecha_inscripcion": "2024-07-25T23:07:14.763Z"
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "nombre_deporte": "string",
        ///       "fecha_inscripcion": "2024-07-25T23:07:14.763Z"
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve la inscripcion creada en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost("{id_deporte}/{id_deportista}")]
        [ActionName("CrearInscripcionDeporte")]
        [ProducesResponseType(typeof(DeporteXInscripcion), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DeporteXInscripcion>> CrearInscripcionDeporte(int id_deporte, int id_deportista)
        {
            try
            {
                if (await TienePermiso(30))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        string usuarioActual = userData.Split(',')[2];
                        Dictionary<string, string> parametros = new() {
                            {"@id_deporte", id_deporte.ToString() },
                            {"@id_inscripto", id_deportista.ToString() },
                        };
                        DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Crear_Inscripcion_Deporte", parametros);
                        //En este caso sino crea es un error en la BD
                        if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                        return Created("Inscripcion a deporte Creado", new DeporteXInscripcion(respuesta.Rows[0]));
                    }

                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR CREANDO INSCRIPCION A DEPORTE");
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite eliminar horarios deportivos
        /// </summary>
        /// <param name="id">Es el id del horario a eliminar</param>
        /// <returns>Un mensaje de OK o el horario que no se pudo eliminar</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Empleados/EliminarHorarioDeportivo/{id}
        ///     
        ///     RESPONSE:
        ///     {
        ///         "Horario Eliminado"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve OK y un mensaje </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El empleado no genero su JWT</response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y devuelve el horario </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>        
        [HttpDelete("{id}")]
        [ActionName("EliminarHorarioDeportivo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> EliminarHorarioDeportivo(int id)
        {
            try
            {
                if (await TienePermiso(147))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        Dictionary<string, string> parametros = new() {
                           { "@id_horario",id.ToString() }
                        };
                        DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Eliminar_Horario_Deportivo", parametros);

                        if (respuesta.Rows.Count > 0)
                        {
                            if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                            //Si es mayor a 0 significa que no se elimino asi que devolvemos dicho registro
                            else return Conflict(new HorarioDeportes(respuesta.Rows[0]));
                        }
                        else return Ok("Horario Eliminado");

                    }

                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR ELIMINANDO HORARIO");
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite eliminar inscripcion a torneo
        /// </summary>
        /// <param name="id_inscripcion">Es el id de la inscripcion a eliminar</param>
        /// <returns>Un mensaje de OK o la inscripcion que no se elimino</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Empleados/EliminarInscripcionTorneo/{id_inscripcion}
        ///     
        ///     RESPONSE:
        ///     {
        ///         "Inscripcion a torneo Eliminada"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve OK y un mensaje </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT</response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y devuelve el horario </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>        
        [HttpDelete("{id_inscripcion}")]
        [ActionName("EliminarInscripcionTorneo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> EliminarInscripcionTorneo(int id_inscripcion)
        {
            try
            {
                if (await TienePermiso(147))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        Dictionary<string, string> parametros = new() {
                           { "@id_inscripcion",id_inscripcion.ToString() }
                        };
                        DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Eliminar_Inscripcion_Torneo", parametros);

                        if (respuesta.Rows.Count > 0)
                        {
                            if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                            //Si es mayor a 0 significa que no se elimino asi que devolvemos dicho registro
                            else return Conflict(new TorneosXInscripcion(respuesta.Rows[0]));
                        }
                        else return Ok("Inscripcion a torneo Eliminada");

                    }

                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR ELIMINANDO INSCRIPCION A TORNEO");
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite eliminar inscripcion a deporte
        /// </summary>
        /// <param name="id_inscripcion">Es el id de la inscripcion a eliminar</param>
        /// <returns>Un mensaje de OK o la inscripcion que no se elimino</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Empleados/EliminarInscripcionDeporte/{id_inscripcion}
        ///     
        ///     RESPONSE:
        ///     {
        ///         "Inscripcion a deporte Eliminada"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve OK y un mensaje </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT</response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y devuelve el horario </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>        
        [HttpDelete("{id_inscripcion}")]
        [ActionName("EliminarInscripcionDeporte")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> EliminarInscripcionDeporte(int id_inscripcion)
        {
            try
            {
                if (await TienePermiso(147))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        Dictionary<string, string> parametros = new() {
                           { "@id_inscripcion",id_inscripcion.ToString() }
                        };
                        DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_DEPORTES_Eliminar_Inscripcion_Deporte", parametros);

                        if (respuesta.Rows.Count > 0)
                        {
                            if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                            //Si es mayor a 0 significa que no se elimino asi que devolvemos dicho registro
                            else return Conflict(new DeporteXInscripcion(respuesta.Rows[0]));
                        }
                        else return Ok("Inscripcion a deporte Eliminada");

                    }

                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR ELIMINANDO INSCRIPCION A DEPORTE");
                return BadRequest();
            }
        }
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
