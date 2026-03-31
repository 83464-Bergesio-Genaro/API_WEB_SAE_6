using API_WEB_SAE_6.Adapters;
using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
    public class DeporteController : Controller
    {
        /// <summary>
        /// Es el adaptador de usuarios para consultar los permisos
        /// </summary>
        private UsuarioAdapter UserAdapter = new();
        /// <summary>
        /// Es el adaptador con respecto a la base de datos para realizar llamadas
        /// </summary>
        private DeporteAdapter DeportAdapter = new();
        /// <summary>
        /// 
        /// </summary>
        private readonly string ControllerName = "DeporteController";
        /// <summary>
        /// 
        /// </summary>
        public DeporteController(){}
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
        public ActionResult<IEnumerable<Deportes>> ObtenerDeportesCompleto()
        {
            try
            {
                if (TienePermiso(20))
                {
                    List<Deportes>? listadoDeportes = DeportAdapter.ObtenerDeportesCompleto();

                    if (listadoDeportes == null) return Conflict();
                    if (listadoDeportes.Count == 0) return NoContent();

                    return Ok(listadoDeportes);
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
        public ActionResult<IEnumerable<Deportes>> ObtenerDeportesActivos()
        {
            try
            {
                //El numero de funcion es: 20
                if (TienePermiso(20))
                {
                    List<Deportes>? listadoDeportes = DeportAdapter.ObtenerDeportesActivo();

                    if (listadoDeportes == null) return Conflict();
                    if (listadoDeportes.Count == 0) return NoContent();

                    return Ok(listadoDeportes);
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
        public ActionResult<Deportes> ObtenerDeportesXId(int id_deporte)
        {
            try
            {
                if (TienePermiso(20))
                {
                    Deportes? depor = DeportAdapter.ObtenerDeportesXId(id_deporte);
                    if (depor == null) return Conflict();
                    if (depor.id == -1) return NoContent();
                    else return Ok(depor);
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
        public ActionResult<IEnumerable<EspaciosDeportivos>> ObtenerEspDeportivoCompleto()
        {
            try
            {
                if (TienePermiso(42))
                {
                    List<EspaciosDeportivos>? listadoDeportes = DeportAdapter.ObtenerEspaciosDeporCompleto();

                    if (listadoDeportes == null) return Conflict();
                    if (listadoDeportes.Count == 0) return NoContent();

                    return Ok(listadoDeportes);
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
        public ActionResult<IEnumerable<EspaciosDeportivos>> ObtenerEspDeportivoActivos()
        {
            try
            {
                if (TienePermiso(42))
                {
                    List<EspaciosDeportivos>? listadoDeportes = DeportAdapter.ObtenerEspaciosDeporActivos();

                    if (listadoDeportes == null) return Conflict();
                    if (listadoDeportes.Count == 0) return NoContent();

                    return Ok(listadoDeportes);
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
        public ActionResult<EspaciosDeportivos> ObtenerEspDeportivoXId(int id_espacio)
        {
            try
            {
                if (TienePermiso(42))
                {
                    EspaciosDeportivos? espacio = DeportAdapter.ObtenerEspaciosDeportXId(id_espacio);
                    if (espacio == null) return Conflict();
                    if (espacio.id == -1) return NoContent();
                    else return Ok(espacio);
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
        public ActionResult<IEnumerable<DocentesDeportivos>> ObtenerDocentesDeportivos()
        {
            try
            {
                if (TienePermiso(36))
                {
                    List<DocentesDeportivos>? listadoDeportes = DeportAdapter.ObtenerDocentesCompletos();

                    if (listadoDeportes == null) return Conflict();
                    if (listadoDeportes.Count == 0) return NoContent();

                    return Ok(listadoDeportes);
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
        public ActionResult<IEnumerable<DocentesDeportivos>> ObtenerDocentesDeportivosActivos()
        {
            try
            {
                if (TienePermiso(36))
                {
                    List<DocentesDeportivos>? listadoDeportes = DeportAdapter.ObtenerDocentesActivos();

                    if (listadoDeportes == null) return Conflict();
                    if (listadoDeportes.Count == 0) return NoContent();

                    return Ok(listadoDeportes);
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
        public ActionResult<DocentesDeportivos> ObtenerDocenteDeportivoXCuil(string cuil)
        {
            try
            {
                if (TienePermiso(36))
                {
                    DocentesDeportivos? docente = DeportAdapter.ObtenerDocentesXCUIL(cuil);
                    if (docente == null) return Conflict();
                    if (docente.cuil == "") return NoContent();
                    else return Ok(docente);
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
        public ActionResult<IEnumerable<Deportista>> ObtenerDeportistasCompleto()
        {
            try
            {
                if (TienePermiso(33))
                {
                    List<Deportista>? listadoDeportistas = DeportAdapter.ObtenerDeportistasCompletos();

                    if (listadoDeportistas == null) return Conflict();
                    if (listadoDeportistas.Count == 0) return NoContent();

                    return Ok(listadoDeportistas);
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
        public ActionResult<IEnumerable<Deportista>> ObtenerDeportistasHabilitados()
        {
            try
            {
                if (TienePermiso(33))
                {
                    List<Deportista>? listadoDeportistas = DeportAdapter.ObtenerDeportistasActivos();

                    if (listadoDeportistas == null) return Conflict();
                    if (listadoDeportistas.Count == 0) return NoContent();

                    return Ok(listadoDeportistas);
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
        public ActionResult<Deportista> ObtenerDeportistasXId(int id_deportista)
        {
            try
            {
                if (TienePermiso(33))
                {
                    Deportista? depor = DeportAdapter.ObtenerDeportistasXId(id_deportista);
                    if (depor == null) return Conflict();
                    if (depor.id == -1) return NoContent();
                    else return Ok(depor);
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
        public ActionResult<Deportista> ObtenerDeportistasXLegajo(string legajo)
        {
            try
            {
                if (TienePermiso(33))
                {
                    Deportista? depor = DeportAdapter.ObtenerDeportistasXLegajo(legajo);
                    if (depor == null) return Conflict();
                    if (depor.id == -1) return NoContent();
                    else return Ok(depor);
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
        public ActionResult<IEnumerable<Deportista>> ObtenerDeportistasXDeporte(int id_deporte)
        {
            try
            {
                if (TienePermiso(33))
                {
                    List<Deportista>? depor = DeportAdapter.ObtenerDeportistasXDeporte(id_deporte);
                    if (depor == null) return Conflict();
                    if (depor.Count == 0) return NoContent();
                    else return Ok(depor);
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
        public ActionResult<IEnumerable<DeporteXInscripcion>> ObtenerInscripcionesXDeportista(int id_deportista)
        {
            try
            {
                if (TienePermiso(32))
                {
                    List<DeporteXInscripcion>? listadoInscripciones = DeportAdapter.ObtenerInscriptosXDeportista(id_deportista);
                    if (listadoInscripciones == null) return Conflict();
                    if (listadoInscripciones.Count == 0) return NoContent();
                    else return Ok(listadoInscripciones);
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
        public ActionResult<IEnumerable<TorneoDeportivo>> ObtenerTorneosDeportivos()
        {
            try
            {
                if (TienePermiso(23))
                {
                    List<TorneoDeportivo>? listadoTorneos = DeportAdapter.ObtenerTorneosCompleto();
                    if (listadoTorneos == null) return Conflict();
                    if (listadoTorneos.Count == 0) return NoContent();
                    else return Ok(listadoTorneos);
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
        public ActionResult<IEnumerable<TorneoDeportivo>> ObtenerTorneosActivos()
        {
            try
            {
                if (TienePermiso(23))
                {
                    List<TorneoDeportivo>? listadoTorneos = DeportAdapter.ObtenerTorneosActivos();
                    if (listadoTorneos == null) return Conflict();
                    if (listadoTorneos.Count == 0) return NoContent();
                    else return Ok(listadoTorneos);
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
        public ActionResult<TorneoDeportivo> ObtenerTorneosXId(int id_torneo)
        {
            try
            {
                if (TienePermiso(23))
                {
                    TorneoDeportivo? depor = DeportAdapter.ObtenerTorneoXId(id_torneo);
                    if (depor == null) return Conflict();
                    if (depor.id == -1) return NoContent();
                    else return Ok(depor);
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
        public ActionResult<IEnumerable<TorneoDeportivo>> ObtenerTorneosXDeporte(int id_deporte)
        {
            try
            {
                if (TienePermiso(23))
                {
                    List<TorneoDeportivo>? listadoTorneos = DeportAdapter.ObtenerTorneosXDeporte(id_deporte);
                    if (listadoTorneos == null) return Conflict();
                    if (listadoTorneos.Count == 0) return NoContent();
                    else return Ok(listadoTorneos);
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
        public ActionResult<IEnumerable<Deportista>> ObtenerDeportistasXTorneo(int id_torneo)
        {
            try
            {
                if (TienePermiso(33))
                {
                    List<Deportista>? listadoTorneos = DeportAdapter.ObtenerDeportistasXTorneo(id_torneo);
                    if (listadoTorneos == null) return Conflict();
                    if (listadoTorneos.Count == 0) return NoContent();
                    else return Ok(listadoTorneos);
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
        public ActionResult<IEnumerable<TorneosXInscripcion>> ObtenerInscTorneoXDeportista(int id_deportista)
        {
            try
            {
                if (TienePermiso(32))
                {
                    List<TorneosXInscripcion>? listadoTorneos = DeportAdapter.ObtenerInscTorneoXDeportista(id_deportista);
                    if (listadoTorneos == null) return Conflict();
                    if (listadoTorneos.Count == 0) return NoContent();
                    else return Ok(listadoTorneos);
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
        public ActionResult<IEnumerable<HorarioDeportes>> ObtenerHorariosCompleto()
        {
            try
            {
                if (TienePermiso(39))
                {
                    List<HorarioDeportes>? listadoHorarios = DeportAdapter.ObtenerHorariosCompleto();
                    if (listadoHorarios == null) return Conflict();
                    if (listadoHorarios.Count == 0) return NoContent();
                    else return Ok(listadoHorarios);
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
        public ActionResult<IEnumerable<HorarioDeportes>> ObtenerHorariosActivos()
        {
            try
            {
                if (TienePermiso(39))
                {
                    List<HorarioDeportes>? listadoHorarios = DeportAdapter.ObtenerHorariosActivo();
                    if (listadoHorarios == null) return Conflict();
                    if (listadoHorarios.Count == 0) return NoContent();
                    else return Ok(listadoHorarios);
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
        public ActionResult<IEnumerable<HorarioDeportes>> ObtenerHorariosXId(int id_horario)
        {
            try
            {
                if (TienePermiso(39))
                {
                    HorarioDeportes? depor = DeportAdapter.ObtenerHorarioXId(id_horario);
                    if (depor == null) return Conflict();
                    if (depor.id == -1) return NoContent();
                    else return Ok(depor);
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
        public ActionResult<IEnumerable<HorarioDeportes>> ObtenerHorariosXEspacio(int id_espacio)
        {
            try
            {
                if (TienePermiso(39))
                {
                    List<HorarioDeportes>? listadoHorarios = DeportAdapter.ObtenerHorariosXEspacio(id_espacio);
                    if (listadoHorarios == null) return Conflict();
                    if (listadoHorarios.Count == 0) return NoContent();
                    else return Ok(listadoHorarios);
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
        public ActionResult<IEnumerable<HorarioDeportes>> ObtenerHorariosXDeporte(int id_deporte)
        {
            try
            {
                if (TienePermiso(39))
                {
                    List<HorarioDeportes>? listadoHorarios = DeportAdapter.ObtenerHorariosXDeporte(id_deporte);
                    if (listadoHorarios == null) return Conflict();
                    if (listadoHorarios.Count == 0) return NoContent();
                    else return Ok(listadoHorarios);
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
        public ActionResult<IEnumerable<HorarioDeportes>> ObtenerHorariosXCUIL(string cuil)
        {
            try
            {
                if (TienePermiso(39))
                {
                    List<HorarioDeportes>? listadoHorarios = DeportAdapter.ObtenerHorariosXCUIL(cuil);
                    if (listadoHorarios == null) return Conflict();
                    if (listadoHorarios.Count == 0) return NoContent();
                    else return Ok(listadoHorarios);
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
        public ActionResult<Deportes> ModificarDeporte(int id, Deportes deporte)
        {
            try
            {
                if (id != deporte.id) return BadRequest();

                if (TienePermiso(19))
                {
                    deporte = DeportAdapter.ModificarDeporte(deporte);
                    if (deporte.id != -1) return Ok(deporte);
                    else return Conflict();
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
        public ActionResult<EspaciosDeportivos> ModificarEspacioDeportivo(int id, EspaciosDeportivos espacio)
        {
            try
            {
                if (id != espacio.id) return BadRequest();

                if (TienePermiso(41))
                {
                    espacio = DeportAdapter.ModificarEspacioDeportivo(espacio);
                    if (espacio.id != -1) return Ok(espacio);
                    else return Conflict();
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
        public ActionResult<DocentesDeportivos> ModificarDocenteDeportivo(string cuil, DocentesDeportivos docente)
        {
            try
            {
                if (cuil != docente.cuil) return BadRequest();

                if (TienePermiso(35))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData != null &&
                       userData.Length > 0 &&
                       userData != "NO DATA" &&
                       int.TryParse(userData.Split(',')[2], out int idUserMod))
                    {
                        docente = DeportAdapter.ModificarDocente(docente, idUserMod);
                        if (docente.cuil != "") return Ok(docente);
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
        public ActionResult<Deportista> ModificarDeportista(int id, Deportista deportista)
        {
            try
            {
                if (id != deportista.id) return BadRequest();

                if (TienePermiso(29))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData != null &&
                       userData.Length > 0 &&
                       userData != "NO DATA" &&
                       int.TryParse(userData.Split(',')[2], out int idUserMod))
                    {
                        deportista = DeportAdapter.ModificarDeportista(deportista, idUserMod);
                        if (deportista.id != -1) return Ok(deportista);
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
        public ActionResult<TorneoDeportivo> ModificarTorneo(int id, TorneoDeportivo torneo)
        {
            try
            {
                if (id != torneo.id) return BadRequest();

                if (TienePermiso(22))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData != null &&
                       userData.Length > 0 &&
                       userData != "NO DATA" &&
                       int.TryParse(userData.Split(',')[2], out int idUserMod))
                    {
                        torneo = DeportAdapter.ModificarTorneo(torneo, idUserMod);
                        if (torneo.id != -1) return Ok(torneo);
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
        public ActionResult<HorarioDeportes> ModificarHorario(int id, HorarioDeportes horario)
        {
            try
            {
                if (id != horario.id) return BadRequest();

                if (TienePermiso(38))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData != null &&
                       userData.Length > 0 &&
                       userData != "NO DATA" &&
                       int.TryParse(userData.Split(',')[2], out int idUserMod))
                    {
                        horario = DeportAdapter.ModificarHorario(horario);
                        if (horario.id != -1) return Ok(horario);
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
        public ActionResult<Deportes> CrearDeporte([FromBody] Deportes deporte)
        {
            try
            {
                if (TienePermiso(18))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        deporte = DeportAdapter.CrearDeporte(deporte);
                        if (deporte.id != -1) return Created("Deporte Creado", deporte);
                        else return Conflict();
                    }
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
        public ActionResult<Deportista> CrearDeportista([FromBody] Deportista deportista)
        {
            try
            {
                if (TienePermiso(28))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData != null &&
                       userData.Length > 0 &&
                       userData != "NO DATA" &&
                       int.TryParse(userData.Split(',')[2], out int idUserCrea))
                    {
                        deportista = DeportAdapter.CrearDeportista(deportista.legajo, idUserCrea);
                        if (deportista.id != -1) return Created("Deportista Creado", deportista);
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
        public ActionResult<DocentesDeportivos> CrearDocenteDeportivo([FromBody] DocentesDeportivos docente)
        {
            try
            {
                if (TienePermiso(28))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData != null &&
                       userData.Length > 0 &&
                       userData != "NO DATA" &&
                       int.TryParse(userData.Split(',')[2], out int idUserCrea))
                    {
                        docente = DeportAdapter.CrearDocente(docente, idUserCrea);
                        if (docente.cuil != "") return Created("Docente Creado", docente);
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
        public ActionResult<EspaciosDeportivos> CrearEspacioDeportivo([FromBody] EspaciosDeportivos espacio)
        {
            try
            {
                if (TienePermiso(40))
                {
                    espacio = DeportAdapter.CrearEspacioDeportivo(espacio);
                    if (espacio.id != -1) return Created("Espacio Creado", espacio);
                    else return Conflict();
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
        public ActionResult<HorarioDeportes> CrearHorarioDeportivo([FromBody] HorarioDeportes horario)
        {
            try
            {
                if (TienePermiso(37))
                {
                    horario = DeportAdapter.CrearHorario(horario);
                    if (horario.id != -1) return Created("Horario Creado", horario);
                    else return Conflict();
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
        public ActionResult<TorneoDeportivo> CrearTorneo([FromBody] TorneoDeportivo torneo)
        {
            try
            {
                if (TienePermiso(21))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData != null &&
                       userData.Length > 0 &&
                       userData != "NO DATA" &&
                       int.TryParse(userData.Split(',')[2], out int idUserCrea))
                    {
                        torneo = DeportAdapter.CrearTorneo(torneo, idUserCrea);
                        if (torneo.id != -1) return Created("Torneo Creado", torneo);
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
        public ActionResult<TorneosXInscripcion> CrearInscripcionTorneo(int id_torneo, int id_deportista)
        {
            try
            {
                if (TienePermiso(24))
                {
                    TorneosXInscripcion insc = DeportAdapter.CrearInscripcionTorneo(id_torneo, id_deportista);
                    if (insc.id != -1) return Created("Inscripcion Creada", insc);
                    else return Conflict();
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
        public ActionResult<DeporteXInscripcion> CrearInscripcionDeporte(int id_deporte, int id_deportista)
        {
            try
            {
                if (TienePermiso(30))
                {
                    DeporteXInscripcion insc = DeportAdapter.CrearInscripcionDeporte(id_deporte, id_deportista);
                    if (insc.id != -1) return Created("Inscripcion Creada", insc);
                    else return Conflict();
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
        public ActionResult<string> EliminarHorarioDeportivo(int id)
        {
            try
            {
                if (TienePermiso(147))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        if (DeportAdapter.EliminarHorarioDeportivo(id)) return Ok("Horario Eliminado");
                        else return Conflict();
                    }
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
        public ActionResult<string> EliminarInscripcionTorneo(int id_inscripcion)
        {
            try
            {
                if (TienePermiso(147))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        if (DeportAdapter.EliminarInscripcionTorneo(id_inscripcion)) return Ok("Inscripcion Eliminada");
                        else return Conflict();
                    }
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
        public ActionResult<string> EliminarInscripcionDeporte(int id_inscripcion)
        {
            try
            {
                if (TienePermiso(147))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        if (DeportAdapter.EliminarInscripcionDeporte(id_inscripcion)) return Ok("Inscripcion Eliminada");
                        else return Conflict();
                    }
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
