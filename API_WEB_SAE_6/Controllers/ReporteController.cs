using API_WEB_SAE_6.Adapters;
using API_WEB_SAE_6.Models.Reporteria;
using API_WEB_SAE_6.Models.Viaje;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

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

        //     SELECT COUNT(e.id) AS economomica, COUNT(i.id) AS investigacion, COUNT(s.id) AS servicio
        //         FROM BecariosSAE b
        //         JOIN BecariosSAEEconomica e ON b.id = e.id_becario
        //         JOIN BecariosSAEInvestigacion i ON b.id = i.id_becario
        //         JOIN BecariosSAEServicio s ON b.id = s.id_becario
        //             WHERE b.anio_beca >= YEAR(initDate)
        //             AND b.anio_beca <= YEAR(endDate)
        //             GROUP BY b.anio_beca
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
        [HttpGet]
        [Authorize]
        [ActionName("ObtenerEstadisticasBecas")]
        [ProducesResponseType(typeof(EstadisticasBecas), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EstadisticasBecas> ObtenerEstadisticasBecas(int startYear,int endYear)
        {
            return Ok(new EstadisticasBecas());
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
        [HttpGet]
        [Authorize]
        [ActionName("ObtenerEstadisticasDeportes")]
        [ProducesResponseType(typeof(EstadisticasDeportes), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EstadisticasDeportes> ObtenerEstadisticasDeportes(int startYear, int endYear)
        {
            return Ok(new EstadisticasDeportes());
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
        [HttpGet]
        [Authorize]
        [ActionName("ObtenerEstadisticasSalud")]
        [ProducesResponseType(typeof(EstadisticasSalud), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EstadisticasSalud> ObtenerEstadisticasSalud(DateOnly startDate, DateOnly endDate)
        {
            return Ok(new EstadisticasSalud());
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
        [HttpGet]
        [Authorize]
        [ActionName("ObtenerEstadisticasViaje")]
        [ProducesResponseType(typeof(EstadisticasSalud), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EstadisticasSalud> ObtenerEstadisticasViaje(DateOnly startDate, DateOnly endDate)
        {
            return Ok(new EstadisticasSalud());
        }
    }
}
