using API_WEB_SAE_6.Adapters;
using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Security.Claims;

namespace API_WEB_SAE_6.Controllers
{
    /// <summary>
    /// Es el controlador para todos los datos del estudiante
    /// </summary>
    [EnableCors("CorsRules")]
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class EstudianteController : Controller
    {
        /// <summary>
        /// Es el adaptador de usuarios para consultar los permisos
        /// </summary>
        private UsuarioAdapter UserAdapter = new();
        /// <summary>
        /// Es el adaptador con respecto a la base de datos para realizar llamadas
        /// </summary>
        private EstudianteAdapter EstudianteAdapter = new();
        /// <summary>
        /// 
        /// </summary>
        private readonly string ControllerName = "EstudianteAdapter";
        /// <summary>
        /// 
        /// </summary>
        public EstudianteController() {}
        /// <summary>
        /// Recupera un documento por su ID
        /// </summary>
        /// <returns>Un documento para descargar</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Estudiantes/DescargarDocumentacionXId/{id}
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///           "id": 0,
        ///           "legajo": "string",
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
        [ProducesResponseType(typeof(DocumentosEstudiante), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DocumentosEstudiante> DescargarDocumentacionXId(int id)
        {
            try
            {
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";

                if (userData != null && userData != "NO DATA" && TienePermiso(154))
                {
                    string legajoRegistrado = userData.Split(',')[0];
                    DocumentosEstudiante? documento = EstudianteAdapter.BuscarDocumentoXId(id);
                    
                    if(documento != null && legajoRegistrado == documento.legajo) return Ok(documento);
                    else
                    {
                        Logger.RegistrarDatos(Logger.LogOptions.Alerta, "DescargarDocumentacionXId", "Intento descargar documentacion que no era suya conociendo el ID del mismo. HOST:"+ HttpContext.Request.Host.Value, ControllerName);
                        return Forbid();
                    }
                }
                else return Unauthorized();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "DescargarDocumentacionXId", ex.Message, ControllerName);
                return BadRequest();
            }
        }
        /// <summary>
        /// Recupera todos los documentos cargados de un legajo
        /// </summary>
        /// <returns>Un listado de todos los documentos</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Estudiantes/ListarDocumentacionXLegajo/{legajo}
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///           "id": 0,
        ///           "legajo": "string",
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
        [HttpGet("{legajo}")]
        [ActionName("ListarDocumentacionXLegajo")]
        [ProducesResponseType(typeof(IEnumerable<DocumentosEstudiante>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<DocumentosEstudiante>> ListarDocumentacionXLegajo(string legajo)
        {
            try
            {
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";

                if (userData != null && userData != "NO DATA" && TienePermiso(154))
                {
                    string legajoRegistrado = userData.Split(',')[0];
                    List<DocumentosEstudiante>? documentos = EstudianteAdapter.BuscarDocumentosXLegajo(legajo);

                    if (documentos != null &&
                        documentos.Count > 0 &&
                        legajoRegistrado == documentos[0].legajo) return Ok(documentos);
                    else
                    {
                        Logger.RegistrarDatos(Logger.LogOptions.Alerta, "DescargarDocumentacionXId", "Intento descargar documentacion que no era suya conociendo el ID del mismo. HOST:" + HttpContext.Request.Host.Value, ControllerName);
                        return Forbid();
                    }
                }
                else return Unauthorized();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "ListarDocumentacionXLegajo", ex.Message, ControllerName);
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
        ///     PUT /api/Estudiante/ModificarDocumento/{id}
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "legajo": "string",
        ///       "id_tipo_documento": 0,
        ///       "nombre_documento": "string",
        ///       "datos_documento": byte[],
        ///       "extension": "string"
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "legajo": "string",
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
        [ProducesResponseType(typeof(DocumentosEstudiante), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status413PayloadTooLarge)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DocumentosEstudiante> ModificarDocumento(int id, DocumentosEstudiante documento)
        {
            try
            {
                //No manda datos en el archivo
                if (id != documento.id || documento.datos_documento == null) return BadRequest();

                //Busca los datos guardados de la claim para verigicar todo
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";

                if (userData != null && userData != "NO DATA" && TienePermiso(154))
                {
                    if (documento.datos_documento?.Length < 50000000)
                    {
                        string legajoRegistrado = userData.Split(',')[0];
                        string usuarioActual = userData.Split(',')[2];
                        documento=EstudianteAdapter.ModificarDocumento(documento, usuarioActual);
                        if (documento.id != -1) return Ok(documento);
                        else return Conflict();
                    }
                    else return StatusCode(413, "El archivo no debe superar los 50Mb");
                }
                else return Unauthorized();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "ModificarDocumento", ex.Message, ControllerName);
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite crear un nuevo documento
        /// </summary>
        /// <param name="documento">El archivo que deseamos almacenar en la BD</param>
        /// <returns>Una inscripcion creada en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Estudiante/CrearDocumentoEstudiante
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "legajo": "string",
        ///       "id_tipo_documento": 0,
        ///       "nombre_documento": "string",
        ///       "datos_documento": byte[],
        ///       "extension": "string"
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "legajo": "string",
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
        [HttpPost]
        [ActionName("CrearDocumentoEstudiante")]
        [Authorize]
        [ProducesResponseType(typeof(DocumentosEstudiante), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status413PayloadTooLarge)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DocumentosEstudiante> CrearDocumentoEstudiante(DocumentosEstudiante documento)
        {
            try
            {
                //No manda datos en el archivo
                if (documento.datos_documento == null) return BadRequest();

                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                if(userData != null && userData != "NO DATA" &&
                    documento.legajo == userData.Split(',')[0] &&
                    TienePermiso(151))
                {
                    if (documento.datos_documento?.Length < 50000000)
                    {
                        string usuarioActual = userData.Split(',')[2];
                        documento = EstudianteAdapter.CrearDocumento(documento, usuarioActual);
                        if (documento.id != -1) return Ok(documento);
                        else return Conflict();
                    }
                    else return StatusCode(413, "El archivo no debe superar los 50Mb");
                }
                else return Unauthorized();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearDocumento", ex.Message, ControllerName);
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
        ///     POST /api/Estudiante/EliminarDocumentoEstudiante/{id_documento}
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
        [ActionName("EliminarDocumentoEstudiante")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> EliminarDocumentoEstudiante(int id_documento)
        {
            try
            {
                if (TienePermiso(153))
                {
                    if (EstudianteAdapter.EliminarDocumento(id_documento)) return Ok("Documento Eliminado.");
                    else return Conflict();
                }
                else return Unauthorized();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearDocumento", ex.Message, ControllerName);
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
