using API_WEB_SAE_6.Adapters;
using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models.Reporteria;
using API_WEB_SAE_6.Models.Viaje;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API_WEB_SAE_6.Controllers
{
    /// <summary>
    /// Controller for handling report-related actions.
    /// </summary>
    [EnableCors("CorsRules")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReporteController : Controller
    {
        /// <summary>
        /// Es el adaptador de usuarios para consultar los permisos
        /// </summary>
        private UsuarioAdapter UserAdapter = new();
        /// <summary>
        /// Es el adaptador con respecto a la base de datos para realizar llamadas
        /// </summary>
        private ReporteAdapter ReportAdapter = new();
        /// <summary>
        /// 
        /// </summary>
        private readonly string ControllerName = "ReporteController";
        /// <summary>
        /// Recupera las estadisticas del area de becas
        /// </summary>
        /// <returns>Un objeto con todos los valores a mostrar</returns>
        /// <param name="startYear"> Año cuando comienza la busqueda </param>
        /// <param name="endYear"> Año hasta donde llega la busqueda </param>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Reporte/ObtenerEstadisticasBecas/
        ///     RESPONSE:
        ///     [
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
        /// <response code="200" >Devuelve las estadisticas en ese periodo de tiempo </response>
        /// <response code="204" >No se encontro ningun valor </response>
        /// <response code="400" >Ocurre un error en la consulta </response>    
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{startYear}/{endYear}")]
        [Authorize]
        [ActionName("ObtenerEstadisticasBecas")]
        [ProducesResponseType(typeof(EstadisticasBecas), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EstadisticasBecas> ObtenerEstadisticasBecas(int startYear,int endYear)
        {
            try
            {
                if (startYear > endYear) return BadRequest("El año de inicio no puede ser mayor al año final");
                if (TienePermiso(55))
                {
                    EstadisticasBecas? esta = ReportAdapter.ObtenerEstadisticasBecas(startYear, endYear);
                    if (esta == null) return Conflict("Fallo algo al recuperar las estadisticas");

                    return Ok(esta);
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
        /// Recupera las estadisticas del area de deportes
        /// </summary>
        /// <returns>Un objeto con todos los valores a mostrar</returns>
        /// <param name="startYear"> Año cuando comienza la busqueda </param>
        /// <param name="endYear"> Año hasta donde llega la busqueda </param>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Reporte/ObtenerEstadisticasDeportes/
        ///     RESPONSE:
        ///     [
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
        /// <response code="200" >Devuelve las estadisticas de deportes </response>
        /// <response code="204" >No se encontro ningun dato </response>
        /// <response code="400" >Ocurre un error en la consulta </response>    
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{startYear}/{endYear}")]
        [Authorize]
        [ActionName("ObtenerEstadisticasDeportes")]
        [ProducesResponseType(typeof(EstadisticasDeportes), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EstadisticasDeportes> ObtenerEstadisticasDeportes(int startYear, int endYear)
        {
            try
            {
                if (startYear > endYear) return BadRequest("El año de inicio no puede ser mayor al año final");
                if (TienePermiso(27))
                {
                    EstadisticasDeportes? esta = ReportAdapter.ObtenerEstadisticasDeporte(startYear, endYear);
                    if (esta == null) return Conflict("Fallo algo al recuperar las estadisticas");

                    return Ok(esta);
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
        /// Recupera las estadisticas del area de salud
        /// </summary>
        /// <returns>Un objeto con todos los valores a mostrar</returns>
        /// <param name="startDate"> Fecha con la que se empieza a buscar </param>
        /// <param name="endDate"> La fecha final</param>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Reporte/ObtenerEstadisticasSalud/
        ///     RESPONSE:
        ///     [
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
        /// <response code="200" >Devuelve las estadisticas de salud </response>
        /// <response code="204" >No se encontro ningun dato </response>
        /// <response code="400" >Ocurre un error en la consulta </response>    
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{startDate}/{endDate}")]
        [Authorize]
        [ActionName("ObtenerEstadisticasSalud")]
        [ProducesResponseType(typeof(EstadisticasSalud), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EstadisticasSalud> ObtenerEstadisticasSalud(DateOnly startDate, DateOnly endDate)
        {
            try
            {
                if (startDate > endDate) return BadRequest("La fecha de inicio no puede ser mayor a la fecha final");
                if (TienePermiso(84))
                {
                    EstadisticasSalud? esta = ReportAdapter.ObtenerEstadisticasSalud(startDate, endDate);
                    if (esta == null) return Conflict("Fallo algo al recuperar las estadisticas");

                    return Ok(esta);
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
        /// Recupera las estadisticas del area de viajes
        /// </summary>
        /// <returns>Un objeto con todos los valores a mostrar</returns>
        /// <param name="startDate"> Fecha con la que se empieza a buscar </param>
        /// <param name="endDate"> La fecha final</param>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Reporte/ObtenerEstadisticasViaje/
        ///     RESPONSE:
        ///     [
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
        /// <response code="200" >Devuelve las estadisticas de salud </response>
        /// <response code="204" >No se encontro ningun dato </response>
        /// <response code="400" >Ocurre un error en la consulta </response>    
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{startDate}/{endDate}")]
        [Authorize]
        [ActionName("ObtenerEstadisticasViaje")]
        [ProducesResponseType(typeof(EstadisticasViaje), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EstadisticasViaje> ObtenerEstadisticasViaje(DateOnly startDate, DateOnly endDate)
        {
            try
            {
                if (startDate > endDate) return BadRequest("La fecha de inicio no puede ser mayor a la fecha final");
                if (TienePermiso(61))
                {
                    EstadisticasViaje? esta = ReportAdapter.ObtenerEstadisticasViajes(startDate, endDate);
                    if (esta == null) return Conflict("Fallo algo al recuperar las estadisticas");

                    return Ok(esta);
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
