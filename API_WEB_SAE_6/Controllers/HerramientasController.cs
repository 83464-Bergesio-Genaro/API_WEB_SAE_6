using API_WEB_SAE_6.Adapters;
using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models.Herramientas;
using API_WEB_SAE_6.Models.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API_WEB_SAE_6.Controllers
{
    /// <summary>
    /// Controlador para gestionar las herramientas disponibles en la aplicación.
    /// </summary>
    [EnableCors("CorsRules")]
    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class HerramientasController : Controller
    {
        /// <summary>
        /// Es el adaptador con respecto a la base de datos para realizar llamadas
        /// </summary>
        private HerramientasAdapter ToolsAdapter = new();
        /// <summary>
        /// Es el adaptador con respecto a la base de datos para realizar llamadas
        /// </summary>
        private UsuarioAdapter UserAdapter = new();
        /// <summary>
        /// 
        /// </summary>
        private readonly string ControllerName = "HerramientasController";
        /// <summary>
        /// 
        /// </summary>
        public HerramientasController(){ }
        /// <summary>
        /// Listado completo de Tipos Documento
        /// </summary>
        /// <returns>Un listado de todos los tipos de documento actuales de la aplicacion</returns>
        /// <remarks>
        /// NOTA: Es un endpoint libre
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Herramientas/ObtenerTiposDocumento/
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///           "id": 0,
        ///           "nombre": "Imagen Caratula",
        ///           "extension": ".png, .jpg, .jpge"
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado de todos los tipos de documento </response>
        /// <response code="204" >No hay ningun tipo de documento cargado</response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerTiposDocumento")]
        [ProducesResponseType(typeof(IEnumerable<TiposDocumento>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<TiposDocumento>> ObtenerTiposDocumento()
        {
            try
            {
                //Si es null es que ocurrio un conflicto en BD.
                List<TiposDocumento>? tiposDocumento = ToolsAdapter.ObtenerTiposDocumento();

                if (tiposDocumento == null) return Conflict();
                if (tiposDocumento.Count == 0) return NoContent();

                return Ok(tiposDocumento);
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, this.Request.Path, ex.Message, ControllerName);
                return BadRequest();
            }
        }
        /// <summary>
        /// Listado completo de las carreras de la UTN FRC
        /// </summary>
        /// <returns>Un listado de todos de carreras actuales de la aplicacion</returns>
        /// <remarks>
        /// NOTA: Es un endpoint libre
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Herramientas/ObtenerCarreras/
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///           "id": 5,
        ///           "nombre": "Ingeniería en Sistemas de Información",
        ///           "sigla": "K"
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado de todas las carreras </response>
        /// <response code="204" >No se encontro ninguna carrera cargada </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerCarreras")]
        [ProducesResponseType(typeof(IEnumerable<Carreras>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Carreras>> ObtenerCarreras()
        {
            try
            {
                //Si es null es que ocurrio un conflicto en BD.
                List<Carreras>? carreras = ToolsAdapter.ObtenerCarreras();

                if (carreras == null) return Conflict();
                if (carreras.Count == 0) return NoContent();

                return Ok(carreras);
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, this.Request.Path, ex.Message, ControllerName);
                return BadRequest();
            }
        }
        /// <summary>
        /// Listado completo de perfiles disponibles en la aplicacion
        /// </summary>
        /// <returns>Un listado con los perfiles</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Herramientas/ObtenerPerfiles/
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///           "id": 0,
        ///           "nombre": "Administrador",
        ///           "activo": true
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado de todos los perfiles </response>
        /// <response code="204" >No hay ningun perfil cargado</response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [Authorize]
        [ActionName("ObtenerPerfiles")]
        [ProducesResponseType(typeof(IEnumerable<Perfiles>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Perfiles>> ObtenerPerfiles()
        {
            try
            {
                if (TienePermiso(9))
                {
                    //Si es null es que ocurrio un conflicto en BD.
                    List<Perfiles>? tiposDocumento = ToolsAdapter.ObtenerPerfiles();

                    if (tiposDocumento == null) return Conflict();
                    if (tiposDocumento.Count == 0) return NoContent();

                    return Ok(tiposDocumento);
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
