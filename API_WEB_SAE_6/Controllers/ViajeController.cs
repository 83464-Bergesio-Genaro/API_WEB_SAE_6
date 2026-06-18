using API_WEB_SAE_6.Adapters;
using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models.Estudiante;
using API_WEB_SAE_6.Models.Viaje;
using API_WEB_SAE_6.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using MySqlConnector;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Claims;

namespace API_WEB_SAE_6.Controllers
{
    /// <summary>
    /// Es el controlador para todo relacionado a la Jornada de puertas abiertas
    /// </summary>
    [EnableCors("CorsRules")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ViajeController : Controller
    {
        /// <summary>
        /// Es el adaptador de usuarios para consultar los permisos
        /// </summary>
        private UsuarioAdapter UserAdapter = new();
        /// <summary>
        /// Es el adaptador con respecto a la base de datos para realizar llamadas
        /// </summary>
        private ViajeAdapter TravelAdapter = new();
        /// <summary>
        /// 
        /// </summary>
        private readonly string ControllerName = "ViajeController";
        /// <summary></summary>
        public ViajeController() { }
        /// <summary>
        /// Recupera todos los viajes que se realizaron
        /// </summary>
        /// <returns>Un listado de viajes</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Viaje/ObtenerViajesCompleto/
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///           "id": 0,
        ///           "nombre": "string",
        ///           "fecha_inicio": "2026-06-16",
        ///           "fecha_fin": "2026-06-16",
        ///           "seguro_confirmado": true,
        ///           "origen": "string",
        ///           "destino": "string",
        ///           "motivo": "string",
        ///           "cantidad_personas": 0,
        ///           "id_empresa_viaje": 0,
        ///           "nombre_empresa": "string",
        ///           "documentacion_presentada": true,
        ///           "costo_aproximado": 0
        ///         },
        ///     ]
        /// </remarks>
        /// <response code="200" >Devuelve un listado completo de viajes </response>
        /// <response code="204" >No se encontro ningun viaje </response>
        /// <response code="400" >Ocurre un error en la consulta </response>    
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerViajesCompleto")]
        [ProducesResponseType(typeof(IEnumerable<Viajes>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Viajes>> ObtenerViajesCompleto()
        {
            try
            {
                if (TienePermiso(66))
                {
                    List<Viajes>? listadoEventosCompleto = TravelAdapter.ObtenerViajesCompleto();

                    if (listadoEventosCompleto == null) return Conflict();
                    if (listadoEventosCompleto.Count == 0) return NoContent();

                    return Ok(listadoEventosCompleto);
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
        /// Recupera todos los viajes que no hayan terminado
        /// </summary>
        /// <returns>Un listado de viajes actuales</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Viaje/ObtenerViajesActivo/
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///           "id": 0,
        ///           "nombre": "string",
        ///           "fecha_inicio": "2026-06-16",
        ///           "fecha_fin": "2026-06-16",
        ///           "seguro_confirmado": true,
        ///           "origen": "string",
        ///           "destino": "string",
        ///           "motivo": "string",
        ///           "cantidad_personas": 0,
        ///           "id_empresa_viaje": 0,
        ///           "nombre_empresa": "string",
        ///           "documentacion_presentada": true,
        ///           "costo_aproximado": 0
        ///         },
        ///     ]
        /// </remarks>
        /// <response code="200" >Devuelve un listado de viajes activos </response>
        /// <response code="204" >No se encontro ningun viaje activo </response>
        /// <response code="400" >Ocurre un error en la consulta </response>    
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerViajesActivo")]
        [ProducesResponseType(typeof(IEnumerable<Viajes>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Viajes>> ObtenerViajesActivo()
        {
            try
            {
                if (TienePermiso(66))
                {
                    List<Viajes>? listadoEventosCompleto = TravelAdapter.ObtenerViajesActivo();

                    if (listadoEventosCompleto == null) return Conflict();
                    if (listadoEventosCompleto.Count == 0) return NoContent();

                    return Ok(listadoEventosCompleto);
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
        /// Buscar viajes por legajo
        /// </summary>
        /// <returns>Un listado de viajes que el estudiante haga</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Viaje/ObtenerViajesXLegajo/{legajo}
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///           "id": 0,
        ///           "nombre": "string",
        ///           "fecha_inicio": "2026-06-16",
        ///           "fecha_fin": "2026-06-16",
        ///           "seguro_confirmado": true,
        ///           "origen": "string",
        ///           "destino": "string",
        ///           "motivo": "string",
        ///           "cantidad_personas": 0,
        ///           "id_empresa_viaje": 0,
        ///           "nombre_empresa": "string",
        ///           "documentacion_presentada": true,
        ///           "costo_aproximado": 0
        ///         },
        ///     ]
        /// </remarks>
        /// <response code="200" >Devuelve un listado de viajes activos </response>
        /// <response code="204" >No se encontro ningun viaje activo </response>
        /// <response code="400" >Ocurre un error en la consulta </response>    
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{legajo}")]
        [ActionName("ObtenerViajesXLegajo")]
        [ProducesResponseType(typeof(IEnumerable<Viajes>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Viajes>> ObtenerViajesXLegajo([FromRoute] string legajo)
        {
            try
            {
                if (TienePermiso(66))
                {
                    List<Viajes>? listadoEventosCompleto = TravelAdapter.ObtenerViajesXLegajo(legajo);

                    if (listadoEventosCompleto == null) return Conflict();
                    if (listadoEventosCompleto.Count == 0) return NoContent();

                    return Ok(listadoEventosCompleto);
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
        /// Buscar viajes por legajo
        /// </summary>
        /// <returns>Un listado de viajes que el estudiante haga</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Viaje/ObtenerViajeXId/{idViaje}
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "nombre": "string",
        ///       "fecha_inicio": "2026-06-16",
        ///       "fecha_fin": "2026-06-16",
        ///       "seguro_confirmado": true,
        ///       "origen": "string",
        ///       "destino": "string",
        ///       "motivo": "string",
        ///       "cantidad_personas": 0,
        ///       "id_empresa_viaje": 0,
        ///       "nombre_empresa": "string",
        ///       "documentacion_presentada": true,
        ///       "costo_aproximado": 0
        ///     }
        /// </remarks>
        /// <response code="200" >Devuelve un listado de viajes activos </response>
        /// <response code="204" >No se encontro ningun viaje activo </response>
        /// <response code="400" >Ocurre un error en la consulta </response>    
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{idViaje}")]
        [ActionName("ObtenerViajeXId")]
        [ProducesResponseType(typeof(Viajes), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Viajes> ObtenerViajeXId([FromRoute] int idViaje)
        {
            try
            {
                if (TienePermiso(66))
                {

                    Viajes? viajeEncontrad = TravelAdapter.ObtenerViajesXId(idViaje);
                    if (viajeEncontrad == null) return Conflict();
                    if (viajeEncontrad.id == -1) return NoContent();

                    return Ok(viajeEncontrad);
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
        /// Permite la modificacion de un viaje
        /// </summary>
        /// <param name="id"> El ID del viaje a modificar</param>
        /// <param name="viajeSAE"> Los datos modificados del viaje</param>
        /// <returns>Un viaje modificado en BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     PUT /api/Viaje/ModificarViaje/{id}
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "nombre": "string",
        ///       "fecha_inicio": "2026-06-16",
        ///       "fecha_fin": "2026-06-16",
        ///       "seguro_confirmado": true,
        ///       "origen": "string",
        ///       "destino": "string",
        ///       "motivo": "string",
        ///       "cantidad_personas": 0,
        ///       "id_empresa_viaje": 0,
        ///       "nombre_empresa": "string",
        ///       "documentacion_presentada": true,
        ///       "costo_aproximado": 0
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "nombre": "string",
        ///       "fecha_inicio": "2026-06-16",
        ///       "fecha_fin": "2026-06-16",
        ///       "seguro_confirmado": true,
        ///       "origen": "string",
        ///       "destino": "string",
        ///       "motivo": "string",
        ///       "cantidad_personas": 0,
        ///       "id_empresa_viaje": 0,
        ///       "nombre_empresa": "string",
        ///       "documentacion_presentada": true,
        ///       "costo_aproximado": 0
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve el viaje modificado en BD </response>
        /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del viaje a modificar </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{id}")]
        [ActionName("ModificarViaje")]
        [Authorize]
        [ProducesResponseType(typeof(Viajes), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Viajes> ModificarViaje(int id, [FromBody, Required] Viajes viajeSAE)
        {
            try
            {
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                if (id != viajeSAE.id) return BadRequest();
                //El numero de funcion es: 104
                if (userData != null &&
                   userData.Length > 0 &&
                   userData != "NO DATA" &&
                   int.TryParse(userData.Split(',')[2], out int idUserMod) &&
                   TienePermiso(58))
                {
                    viajeSAE = TravelAdapter.ModificarViajeSAE(viajeSAE, idUserMod);
                    if (viajeSAE.id != -1) return Ok(viajeSAE);
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
        /// Permite crear viajes
        /// </summary>
        /// <param name="viajeSae">El viaje que deseamos crear, se envia en el Body</param>
        /// <returns>Un viaje creado en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Viaje/CrearViaje/
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "nombre": "string",
        ///       "fecha_inicio": "2026-06-16",
        ///       "fecha_fin": "2026-06-16",
        ///       "seguro_confirmado": true,
        ///       "origen": "string",
        ///       "destino": "string",
        ///       "motivo": "string",
        ///       "cantidad_personas": 0,
        ///       "id_empresa_viaje": 0,
        ///       "nombre_empresa": "string",
        ///       "documentacion_presentada": true,
        ///       "costo_aproximado": 0
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "nombre": "string",
        ///       "fecha_inicio": "2026-06-16",
        ///       "fecha_fin": "2026-06-16",
        ///       "seguro_confirmado": true,
        ///       "origen": "string",
        ///       "destino": "string",
        ///       "motivo": "string",
        ///       "cantidad_personas": 0,
        ///       "id_empresa_viaje": 0,
        ///       "nombre_empresa": "string",
        ///       "documentacion_presentada": true,
        ///       "costo_aproximado": 0
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve el eventos creado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost]
        [ActionName("CrearViaje")]
        [Authorize]
        [ProducesResponseType(typeof(Viajes), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Viajes> CrearViaje([FromBody] Viajes viajeSae)
        {
            try
            {
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                if (userData != null &&
                   userData.Length > 0 &&
                   userData != "NO DATA" &&
                   int.TryParse(userData.Split(',')[2], out int idUserCrea) &&
                   TienePermiso(57))
                {
                    viajeSae = TravelAdapter.CrearViajeSAE(viajeSae, idUserCrea);
                    if (viajeSae.id != -1) return Created("Viaje Creado", viajeSae);
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
        /// Recupera todos los inscriptos al viaje
        /// </summary>
        /// <returns>Un listado de inscriptos en el viaje</returns>
        /// <param name="idViaje">El identificador del viaje</param>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Viaje/ObtenerInscriptosViaje/
        ///     
        ///     RESPONSE:
        ///     [
        ///     ]
        /// </remarks>
        /// <response code="200" >Devuelve un listado completo de inscriptos </response>
        /// <response code="204" >No se encontro ningun inscripto </response>
        /// <response code="400" >Ocurre un error en la consulta </response>    
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{idViaje}")]
        [ActionName("ObtenerInscriptosViaje")]
        [ProducesResponseType(typeof(IEnumerable<ViajeXInscripcion>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ViajeXInscripcion>> ObtenerInscriptosViaje(int idViaje)
        {
            try
            {
                if (TienePermiso(66))
                {
                    List<ViajeXInscripcion>? listadoEventosCompleto = TravelAdapter.ObtenerInscriptosXViaje(idViaje);

                    if (listadoEventosCompleto == null) return Conflict();
                    if (listadoEventosCompleto.Count == 0) return NoContent();

                    return Ok(listadoEventosCompleto);
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
        /// Permite la modificacion de un inscripto
        /// </summary>
        /// <param name="id"> El ID del inscripto a modificar</param>
        /// <param name="inscripto"> Los datos modificados del inscripto</param>
        /// <returns>Un inscripto modificado en BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     PUT /api/Viaje/ModificarInscripto/{id}
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "id_viaje": 1,
        ///       "legajo_estudiante": "83464@sistemas.frc.utn.edu.ar",
        ///       "nombre_estudiante": "",
        ///       "documentacion_presentada": false
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "id_viaje": 1,
        ///       "legajo_estudiante": "83464@sistemas.frc.utn.edu.ar",
        ///       "nombre_estudiante": "string",
        ///       "documentacion_presentada": false
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve el inscripto modificado en BD </response>
        /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del inscripto a modificar </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{id}")]
        [ActionName("ModificarInscripto")]
        [Authorize]
        [ProducesResponseType(typeof(ViajeXInscripcion), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ViajeXInscripcion> ModificarInscripto(int id, [FromBody, Required] ViajeXInscripcion inscripto)
        {
            try
            {
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                if (id != inscripto.id) return BadRequest();
                if (userData != null &&
                   userData.Length > 0 &&
                   userData != "NO DATA" &&
                   int.TryParse(userData.Split(',')[2], out int idUserMod) &&
                   TienePermiso(64))
                {
                    inscripto = TravelAdapter.ModificarInscriptoViaje(inscripto);
                    if (inscripto.id != -1) return Ok(inscripto);
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
        /// Permite crear viajes
        /// </summary>
        /// <param name="inscripto">El viaje que deseamos crear, se envia en el Body</param>
        /// <returns>Un viaje creado en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Viaje/CrearViaje/
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "id_viaje": 1,
        ///       "legajo_estudiante": "83464@sistemas.frc.utn.edu.ar",
        ///       "nombre_estudiante": "",
        ///       "documentacion_presentada": false
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "id_viaje": 1,
        ///       "legajo_estudiante": "83464@sistemas.frc.utn.edu.ar",
        ///       "nombre_estudiante": "string",
        ///       "documentacion_presentada": false
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve el eventos creado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost]
        [ActionName("CrearInscriptoViaje")]
        [Authorize]
        [ProducesResponseType(typeof(ViajeXInscripcion), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ViajeXInscripcion> CrearInscriptoViaje([FromBody] ViajeXInscripcion inscripto)
        {
            try
            {
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                if (userData != null &&
                   userData.Length > 0 &&
                   userData != "NO DATA" &&
                   int.TryParse(userData.Split(',')[2], out int idUserCrea) &&
                   TienePermiso(64))
                {
                    inscripto = TravelAdapter.CrearInscripcionViaje(inscripto);
                    if (inscripto.id != -1) return Created("Inscripto Creado", inscripto);
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
        /// Permite eliminar inscriptos
        /// </summary>
        /// <param name="id">Es el id del inscripto a eliminar</param>
        /// <returns>Un mensaje de OK o el inscripto que no se pudo eliminar</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Viaje/EliminarInscriptos/{id}
        ///     
        ///     RESPONSE:
        ///     {
        ///         "Inscripto Eliminado"
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
        [ActionName("EliminarInscriptos")]
        [Authorize]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> EliminarInscriptos(int id)
        {
            try
            {
                if (TienePermiso(65))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        if (TravelAdapter.EliminarInscripcionViaje(id)) return Ok("Inscripto Eliminado");
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
        /// Recupera un documento por su ID
        /// </summary>
        /// <returns>Un documento para descargar</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Viaje/DescargarDocumentacionXId/{id}
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///           "id": 0,
        ///           "id_viaje": 0,
        ///           "id_tipo_documento": 0,
        ///           "nombre_documento": "string",
        ///           "datos_documento": byte[],
        ///           "extension": "string"
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un documento solicitado</response>
        /// <response code="204" >No se encontro ningun documento </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{id}")]
        [ActionName("DescargarDocumentacionXId")]
        [ProducesResponseType(typeof(DocumentosViaje), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DocumentosViaje> DescargarDocumentacionXId(int id)
        {
            try
            {
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";

                if (userData != null && userData != "NO DATA" && TienePermiso(63))
                {
                    DocumentosViaje? doc = TravelAdapter.BuscarDocumentoXId(id);

                    if (doc != null && doc.id != -1)
                    {
                        string legajoRegistrado = userData.Split(',')[0];
                        string idPerfil = userData.Split(',')[1];
                        //Valida que quien descarga sea empleado, administrador o sea el legajo del mismo estudiante
                        if (idPerfil == "2" || idPerfil == "5")
                        {
                            SettingsReader set = SettingsReader.GetAppSettings();
                            string uploadsPath = set.GetFilesLocation();
                            if (uploadsPath != "ERROR")
                            {
                                string filePath = Path.Combine(uploadsPath, doc.ruta);
                                //Verifica si existe el archivo
                                FileInfo fileInfo = new(filePath);

                                if (fileInfo.Exists)
                                {
                                    FileStream stream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read); ;
                                    FileExtensionContentTypeProvider provider = new();
                                    //Se asegura que puedas descargar el archivo despues
                                    if (!provider.TryGetContentType(filePath, out string? contentType))
                                        contentType = "application/octet-stream"; // fallback
                                    return File(stream, contentType, doc.nombre_documento);
                                }
                                else return NotFound();
                            }
                            else return Conflict("Sistema de Archivos no encontrado");
                        }
                        else return Unauthorized();

                    }
                    else return NotFound();
                }
                else return Unauthorized();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, this.Request.Path, ex.Message, ControllerName);
                return BadRequest();
            }
        }
        /// <summary>
        /// Recupera todos los documentos cargados de ese viaje
        /// </summary>
        /// <returns>Un listado de todos los documentos</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Viaje/ListarDocumentacionXViaje/{idViaje}
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///           "id": 0,
        ///           "id_viaje": 0,
        ///           "id_tipo_documento": 0,
        ///           "nombre_documento": "string",
        ///           "datos_documento": byte[],
        ///           "extension": "string"
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado de todos los documentos</response>
        /// <response code="204" >No se encontro ningun documento </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{idViaje}")]
        [ActionName("ListarDocumentacionXViaje")]
        [ProducesResponseType(typeof(IEnumerable<DocumentosViaje>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<DocumentosViaje>> ListarDocumentacionXViaje(int idViaje)
        {
            try
            {
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";

                if (userData != null && userData != "NO DATA" && TienePermiso(63))
                {
                    string legajoRegistrado = userData.Split(',')[0];
                    string idPerfil = userData.Split(',')[1];
                    if (idPerfil == "2" || idPerfil == "5")
                    {
                        List<DocumentosViaje>? documentos = TravelAdapter.BuscarDocumentosXViaje(idViaje);

                        if (documentos != null && documentos.Count > 0) return Ok(documentos);
                        else return NoContent();
                    }
                    return Unauthorized();
                }
                else return Unauthorized();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, this.Request.Path, ex.Message, ControllerName);
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite la modificacion de un documento
        /// </summary>
        /// <param name="id"> El ID del documento a modificar</param>
        /// <param name="documento"> Los datos modificados del documento</param>
        /// <returns>Un > modificado en BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     PUT /api/Viaje/ModificarDocumento/{id}
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "id_viaje": 0,
        ///       "id_tipo_documento": 0,
        ///       "nombre_documento": "string",
        ///       "datos_documento": byte[],
        ///       "extension": "string"
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "id_viaje": 0,
        ///       "id_tipo_documento": 0,
        ///       "nombre_documento": "string",
        ///       "datos_documento": byte[],
        ///       "extension": "string"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve el documento modificado en BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
        /// <response code="413" >El archivo que se cargo supero los 50Mb </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{id}")]
        [ActionName("ModificarDocumento")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(DocumentosViaje), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status413PayloadTooLarge)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DocumentosViaje>> ModificarDocumento(int id, IFormFile documento)
        {
            try
            {
                //Busca los datos guardados de la claim para verificar todo
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";

                if (userData != null && userData != "NO DATA" && TienePermiso(60))
                {
                    //Que no supere 50Mb
                    if (documento.Length < 50000000)
                    {
                        string legajo = userData.Split(",")[0];
                        DocumentosViaje? doc = TravelAdapter.BuscarDocumentoXId(id);

                        if (doc != null)
                        {
                            SettingsReader set = SettingsReader.GetAppSettings();
                            string uploadsPath = set.GetFilesLocation();

                            if (uploadsPath != "ERROR" && doc != null)
                            {
                                string idUserMod = userData.Split(',')[2];
                                string filePath = Path.Combine(uploadsPath, doc.ruta);
                                //Verifica si existe el archivo
                                FileInfo fileInfo = new(filePath);

                                if (fileInfo.Exists)
                                {
                                    using var stream = new FileStream(filePath, FileMode.Create);
                                    await documento.CopyToAsync(stream);

                                    //Lo unico que cambia es el tamaño
                                    doc.tamanio = documento.Length;
                                    doc = TravelAdapter.ModificarDocumento(doc, idUserMod);

                                    if (doc.id != -1) return Ok(doc);
                                    else return Conflict("Archivo no Modificado");
                                }
                                else return NotFound();
                            }
                            else return Conflict("Sistema de Archivos no encontrado");
                        }
                        else return NotFound();
                    }
                    else return StatusCode(413, "El archivo no debe superar los 50Mb");
                }
                else return Unauthorized();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, this.Request.Path, ex.Message, ControllerName);
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite crear un nuevo documento
        /// </summary>
        /// <param name="archivo">El archivo que deseamos almacenar en la BD</param>
        /// <param name="idTipoDocumento">El tipo de documento que subimos</param>
        /// <param name="idViaje">El viaje que queremos asociar a este documento</param>
        /// <returns>Una inscripcion creada en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Viaje/CrearDocumentoViaje/{idViaje}/{idTipoDocumento}
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "id_viaje": 0,
        ///       "id_tipo_documento": 0,
        ///       "nombre_documento": "string",
        ///       "datos_documento": byte[],
        ///       "extension": "string"
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "id_viaje": 0,
        ///       "id_tipo_documento": 0,
        ///       "nombre_documento": "string",
        ///       "datos_documento": byte[],
        ///       "extension": "string"
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve el documento creado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="413" >El archivo que se cargo supero los 50Mb </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost("{idViaje}/{idTipoDocumento}")]
        [ActionName("CrearDocumentoViaje")]
        [Consumes("multipart/form-data")]
        [Authorize]
        [ProducesResponseType(typeof(DocumentosViaje), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status413PayloadTooLarge)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DocumentosViaje>> CrearDocumentoViaje(int idViaje,int idTipoDocumento, IFormFile archivo)
        {
            try
            {
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                if (userData != null &&
                   userData.Length > 0 &&
                   userData != "NO DATA" &&
                   int.TryParse(userData.Split(',')[2], out int idUserMod) &&
                   TienePermiso(60))
                {
                    if (archivo.Length < 50000000)
                    {
                        string usuarioActual = userData.Split(',')[2];
                        SettingsReader set = SettingsReader.GetAppSettings();
                        string uploadsPath = set.GetFilesLocation();
                        if (uploadsPath != "ERROR")
                        {
                            //Ruta absoluta
                            uploadsPath = Path.Combine(uploadsPath, "Viajes", "Viaje_" + idViaje);

                            // crear carpeta si no existe
                            if (!Directory.Exists(uploadsPath))
                                Directory.CreateDirectory(uploadsPath);

                            // nombre único
                            var fileName = $"{Guid.NewGuid()}_{archivo.FileName}";
                            var filePath = Path.Combine(uploadsPath, fileName);

                            // guardar archivo
                            using var stream = new FileStream(filePath, FileMode.Create);
                            await archivo.CopyToAsync(stream);

                            DocumentosViaje doc = new()
                            {
                                id = -1,
                                id_tipo_documento = idTipoDocumento,//Tengo que ver que hago con esto
                                nombre_documento = archivo.FileName,
                                id_viaje = idViaje,
                                ruta = Path.Combine("Viajes", "Viaje_" + idViaje, fileName),//Es una ruta relativa desde el origen del sistema de archivos
                                tamanio = archivo.Length
                            };

                            doc = TravelAdapter.CrearDocumento(doc, idUserMod);

                            if (doc.id != -1) return Ok(doc);
                            else
                            {
                                //Sino cargo la referencia en la base lo elimina
                                FileInfo fileInfo = new(filePath);
                                fileInfo.Delete();
                                return Conflict();
                            }
                        }
                        else return Conflict("Sistema de Archivos no encontrado");
                    }
                    else return StatusCode(413, "El archivo no debe superar los 50Mb");
                }
                else return Unauthorized();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, this.Request.Path, ex.Message, ControllerName);
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite eliminar documentos
        /// </summary>
        /// <param name="id_documento">Es el id del documento a eliminar</param>
        /// <returns>Un mensaje de OK o el documento que no se pudo eliminar porque esta vinculado</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization y que el documento no este vinculado a ninguna publicacion
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Viaje/EliminarDocumentoViaje/{id_documento}
        ///     
        ///     RESPONSE:
        ///     {
        ///         "Documento Eliminado"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve OK y un mensaje </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT</response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y devuelve el horario </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>        
        [HttpDelete("{id_documento}")]
        [ActionName("EliminarDocumentoViaje")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> EliminarDocumentoViaje(int id_documento)
        {
            try
            {
                if (TienePermiso(60))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        DocumentosViaje? doc = TravelAdapter.BuscarDocumentoXId(id_documento);
                        if (doc != null && doc.id != -1)
                        {
                            string legajoRegistrado = userData.Split(',')[0];
                            string idPerfil = userData.Split(',')[1];
                            if (idPerfil == "2" || idPerfil == "5")
                            {
                                SettingsReader set = SettingsReader.GetAppSettings();
                                string uploadsPath = set.GetFilesLocation();
                                if (uploadsPath != "ERROR")
                                {
                                    string filePath = Path.Combine(uploadsPath, doc.ruta);
                                    //Verifica si existe el archivo
                                    FileInfo fileInfo = new(filePath);

                                    if (fileInfo.Exists)
                                    {
                                        fileInfo.Delete();
                                        if (TravelAdapter.EliminarDocumento(id_documento)) return Ok("Documento Eliminado");
                                        else return Conflict("Se elimino el archivo del sistema de archivos pero no su registro en BD");
                                    }
                                    else return NotFound();
                                }
                                else return Conflict("Sistema de Archivos no encontrado");

                            }
                            else return Unauthorized();
                        }
                        else return NotFound();
                    }
                }
                else return Unauthorized();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, this.Request.Path, ex.Message, ControllerName);
                return BadRequest();
            }
        }
        /// <summary>
        /// Recupera todas las empresas
        /// </summary>
        /// <returns>Un listado de empresas</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Viaje/ObtenerEmpresasViaje/
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///           "id": 0,
        ///           "nombre": "string",
        ///           "cuit": "string",
        ///           "cbu": "string",
        ///           "email": "string",
        ///           "contacto": "string",
        ///           "activo": true
        ///         }
        ///     ]
        /// </remarks>
        /// <response code="200" >Devuelve un listado de viajes activos </response>
        /// <response code="204" >No se encontro ningun viaje activo </response>
        /// <response code="400" >Ocurre un error en la consulta </response>    
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerEmpresasViaje")]
        [ProducesResponseType(typeof(IEnumerable<EmpresaViaje>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<EmpresaViaje>> ObtenerEmpresasViaje()
        {
            try
            {
                if (TienePermiso(57))
                {
                    List<EmpresaViaje>? listadoEventosCompleto = TravelAdapter.ObtenerEmpresasCompleto();

                    if (listadoEventosCompleto == null) return Conflict();
                    if (listadoEventosCompleto.Count == 0) return NoContent();

                    return Ok(listadoEventosCompleto);
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
        /// Buscar empresas por id
        /// </summary>
        /// <returns>Un listado de empresas</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Viaje/ObtenerEmpresasXId/{idEmpresa}
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "nombre": "string",
        ///       "cuit": "string",
        ///       "cbu": "string",
        ///       "email": "string",
        ///       "contacto": "string",
        ///       "activo": true
        ///     }
        /// </remarks>
        /// <response code="200" >Devuelve un listado de viajes activos </response>
        /// <response code="204" >No se encontro ningun viaje activo </response>
        /// <response code="400" >Ocurre un error en la consulta </response>    
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{idEmpresa}")]
        [ActionName("ObtenerEmpresasXId")]
        [ProducesResponseType(typeof(EmpresaViaje), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EmpresaViaje> ObtenerEmpresasXId([FromRoute] int idEmpresa)
        {
            try
            {
                if (TienePermiso(66))
                {

                    EmpresaViaje? empresaEncontra = TravelAdapter.ObtenerEmpresasXId(idEmpresa);
                    if (empresaEncontra == null) return Conflict();
                    if (empresaEncontra.id == -1) return NoContent();

                    return Ok(empresaEncontra);
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
        /// Permite la modificacion de un empresa
        /// </summary>
        /// <param name="id"> El ID del empresa a modificar</param>
        /// <param name="empresaViaje"> Los datos modificados de la empresas</param>
        /// <returns>Un viaje modificado en BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     PUT /api/Viaje/ModificarViaje/{id}
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "nombre": "string",
        ///       "cuit": "string",
        ///       "cbu": "string",
        ///       "email": "string",
        ///       "contacto": "string",
        ///       "activo": true
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "nombre": "string",
        ///       "cuit": "string",
        ///       "cbu": "string",
        ///       "email": "string",
        ///       "contacto": "string",
        ///       "activo": true
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve el empresa modificado en BD </response>
        /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del empresa a modificar </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{id}")]
        [ActionName("ModificarEmpresa")]
        [Authorize]
        [ProducesResponseType(typeof(EmpresaViaje), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EmpresaViaje> ModificarEmpresa(int id, [FromBody, Required] EmpresaViaje empresaViaje)
        {
            try
            {
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                if (id != empresaViaje.id) return BadRequest();
                //El numero de funcion es: 104
                if (userData != null &&
                   userData.Length > 0 &&
                   userData != "NO DATA" &&
                   int.TryParse(userData.Split(',')[2], out int idUserMod) &&
                   TienePermiso(58))
                {
                    empresaViaje = TravelAdapter.ModificarEmpresa(empresaViaje);
                    if (empresaViaje.id != -1) return Ok(empresaViaje);
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
        /// Permite crear empresas
        /// </summary>
        /// <param name="empresasViaje">Una empresa que deseamos crear, se envia en el Body</param>
        /// <returns>Una empresa creado en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Viaje/CrearEmpresa/
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "nombre": "string",
        ///       "cuit": "string",
        ///       "cbu": "string",
        ///       "email": "string",
        ///       "contacto": "string",
        ///       "activo": true
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "nombre": "string",
        ///       "cuit": "string",
        ///       "cbu": "string",
        ///       "email": "string",
        ///       "contacto": "string",
        ///       "activo": true
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve la empresa creada en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost]
        [ActionName("CrearEmpresa")]
        [Authorize]
        [ProducesResponseType(typeof(EmpresaViaje), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EmpresaViaje> CrearEmpresa([FromBody] EmpresaViaje empresasViaje)
        {
            try
            {
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                if (userData != null &&
                   userData.Length > 0 &&
                   userData != "NO DATA" &&
                   int.TryParse(userData.Split(',')[2], out int idUserCrea) &&
                   TienePermiso(57))
                {
                    empresasViaje = TravelAdapter.CrearEmpresaViaje(empresasViaje);
                    if (empresasViaje.id != -1) return Created("Empresa Creado", empresasViaje);
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
