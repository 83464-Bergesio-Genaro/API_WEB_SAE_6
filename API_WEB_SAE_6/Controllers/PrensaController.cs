using API_WEB_SAE_6.Adapters;
using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models.Prensa;
using API_WEB_SAE_6.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Data;
using System.IO.Compression;
using System.Security.Claims;

namespace API_WEB_SAE_6.Controllers
{
    /// <summary>
    /// Es el controlador para prensa y sus publicaciones
    /// </summary>
    [EnableCors("CorsRules")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PrensaController : Controller
    {
        /// <summary>
        /// Es el adaptador de usuarios para consultar los permisos
        /// </summary>
        private UsuarioAdapter UserAdapter = new();
        /// <summary>
        /// Es el adaptador con respecto a la base de datos para realizar llamadas
        /// </summary>
        private PrensaAdapter PressAdapter = new();
        /// <summary>
        /// 
        /// </summary>
        private readonly string ControllerName = "PrensaController";
        /// <summary>
        /// 
        /// </summary>
        public PrensaController(){}
        /// <summary>
        /// Recupera todas las publicaciones
        /// </summary>
        /// <returns>Un listado de todas las publicaciones</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Prensa/ListarPublicacionesCompleto/
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///           "id": 0,
        ///           "titulo_publicacion": "string",
        ///           "descripcion": "string",
        ///           "fecha_inicio": "2024-08-01T15:42:11.205Z",
        ///           "fecha_vigencia": "2024-08-01T15:42:11.205Z",
        ///           "prioridad": 0,
        ///           "no_dar_baja": true,
        ///           "visualizaciones": 0,
        ///           "documentos_asociados" "1,documentoPrueba-2,documento2"
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado de publicaciones</response>
        /// <response code="204" >No se encontro ninguna publicacion </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [Authorize]
        [ActionName("ListarPublicacionesCompleto")]
        [ProducesResponseType(typeof(IEnumerable<Publicaciones>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Publicaciones>> ListarPublicacionesCompleto()
        {
            try
            {
                if (TienePermiso(16))
                {
                    List<Publicaciones>? listadoPubli = PressAdapter.ObtenerPublicacionesCompleto();

                    if (listadoPubli == null) return Conflict();
                    if (listadoPubli.Count == 0) return NoContent();

                    return Ok(listadoPubli);
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
        /// Recupera todas las publicaciones activas
        /// </summary>
        /// <returns>Un listado de publicaciones activas</returns>
        /// <remarks>
        /// NOTA: Este endpoint es para libre consumo sin importar el usuario
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Prensa/ListarPublicacionesActivas/
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///           "id": 0,
        ///           "titulo_publicacion": "string",
        ///           "descripcion": "string",
        ///           "fecha_inicio": "2024-08-01T15:42:11.205Z",
        ///           "fecha_vigencia": "2024-08-01T15:42:11.205Z",
        ///           "prioridad": 0,
        ///           "no_dar_baja": true,
        ///           "visualizaciones": 0,
        ///           "documentos_asociados" "1,documentoPrueba-2,documento2"
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado de publicaciones</response>
        /// <response code="204" >No se encontro ninguna publicacion activa </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ListarPublicacionesActivas")]
        [ProducesResponseType(typeof(IEnumerable<Publicaciones>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Publicaciones>>ListarPublicacionesActivas()
        {
            try
            {
                List<Publicaciones>? listadoPubli = PressAdapter.ObtenerPublicacionesActivas();

                if (listadoPubli == null) return Conflict();
                if (listadoPubli.Count == 0) return NoContent();

                return Ok(listadoPubli);
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, this.Request.Path, ex.Message, ControllerName);
                return BadRequest();
            }
        }
        /// <summary>
        /// Una publicacion este ID
        /// </summary>
        /// <param name="id_publicacion">Es el ID de la publicacion en la BD</param>
        /// <returns>Una publicacion</returns>
        /// <remarks>
        /// NOTA: Este endpoint es para libre consumo sin importar el usuario
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Prensa/ListarDocumentoXPublicacion/{id_publicacion}
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "titulo_publicacion": "string",
        ///       "descripcion": "string",
        ///       "fecha_inicio": "2024-08-01T15:42:11.205Z",
        ///       "fecha_vigencia": "2024-08-01T15:42:11.205Z",
        ///       "prioridad": 0,
        ///       "no_dar_baja": true,
        ///       "visualizaciones": 0,
        ///       "documentos_asociados" "1,documentoPrueba-2,documento2"
        ///     }  
        /// </remarks>
        /// <response code="200" >Devuelve una publicacion con este ID </response>
        /// <response code="204" >No se encontro ninguna publicacion asociada</response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{id_publicacion}")]
        [ActionName("ObtenerPublicacionXId")]
        [ProducesResponseType(typeof(Publicaciones), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Publicaciones>> ObtenerPublicacionXId(int id_publicacion)
        {
            try
            {
                Publicaciones? espe = PressAdapter.ObtenerPublicacionesXId(id_publicacion);
                if (espe == null) return Conflict();
                if (espe.id == -1) return NoContent();
                else return Ok(espe);
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, this.Request.Path, ex.Message, ControllerName);
                return BadRequest();
            }
        }
        /// <summary>
        /// Recupera todos los documentos que tenemos guardados
        /// </summary>
        /// <returns>Un listado de todos los documentos</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Prensa/ListarDocumentosSinData/
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///           "id": 0,
        ///           "id_tipo_documento": 0,
        ///           "nombre_documento": "string",
        ///           "datos_documento": null,(Solo recupera los datos cuando se quiere ver o descargar)
        ///           "extension": "string",
        ///           "id_vinculacion": null (Este atributo solo lo recupera si lo buscamos por publicacion)
        ///         }  
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado de documentos</response>
        /// <response code="204" >No se encontro ningun documento </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [Authorize]
        [HttpGet]
        [ActionName("ListarDocumentosSinData")]
        [ProducesResponseType(typeof(IEnumerable<DocumentosPrensa>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<DocumentosPrensa>> ListarDocumentosSinData()
        {
            try
            {
                if (TienePermiso(17))
                {
                    List <DocumentosPrensa>? listadoPubli = PressAdapter.ObtenerDocumentacionSinData();

                    if (listadoPubli == null) return Conflict();
                    if (listadoPubli.Count == 0) return NoContent();

                    return Ok(listadoPubli);
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
        /// Descarga el documento INTERNO por su ID
        /// </summary>
        /// <returns>Un documento para visualizar en el front</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Prensa/DescargarDocumentoXId/{id}
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "id_tipo_documento": 0,
        ///       "nombre_documento": "string",
        ///       "datos_documento": "string",
        ///       "extension": "string",
        ///       "id_vinculacion": null (Este atributo solo lo recupera si lo buscamos por publicacion)
        ///     }  
        /// </remarks>
        /// <response code="200" >Devuelve un documento para su visualizacion o descarga </response>
        /// <response code="204" >No se encontro ningun documento con este ID </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [Authorize]
        [HttpGet("{id}")]
        [ActionName("DescargarDocumentoInternoXId")]
        [ProducesResponseType(typeof(DocumentosPrensa), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DocumentosPrensa> DescargarDocumentoInternoXId(int id)
        {
            try
            {
                DocumentosPrensa? doc = PressAdapter.ObtenerDocumentoXId(id);
                if (doc != null && doc.id != -1 && doc.libre_consumo)
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
                else return NotFound();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, this.Request.Path, ex.Message, ControllerName);
                return BadRequest();
            }
        }
        /// <summary>
        /// Descarga el documento por su ID
        /// </summary>
        /// <returns>Un documento para visualizar en el front</returns>
        /// <remarks>
        /// NOTA: Este endpoint es para libre consumo sin importar el usuario
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Prensa/DescargarDocumentoXId/{id}
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "id_tipo_documento": 0,
        ///       "nombre_documento": "string",
        ///       "datos_documento": "string",
        ///       "extension": "string",
        ///       "id_vinculacion": null (Este atributo solo lo recupera si lo buscamos por publicacion)
        ///     }  
        /// </remarks>
        /// <response code="200" >Devuelve un documento para su visualizacion o descarga </response>
        /// <response code="204" >No se encontro ningun documento con este ID </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{id}")]
        [ActionName("DescargarDocumentoXId")]
        [ProducesResponseType(typeof(DocumentosPrensa), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DocumentosPrensa> DescargarDocumentoXId(int id)
        {
            try
            {
                DocumentosPrensa? doc = PressAdapter.ObtenerDocumentoXId(id);
                if (doc != null && doc.id != -1 && doc.libre_consumo)
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
                else return NotFound();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, this.Request.Path, ex.Message, ControllerName);
                return BadRequest();
            }
        }
        /// <summary>
        /// Lista los documentos asociados a una publicacion
        /// </summary>
        /// <returns>Un listado de documentos asociados a la publicacion</returns>
        /// <remarks>
        /// NOTA: Este endpoint es para libre consumo sin importar el usuario
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Prensa/ListarDocumentoXPublicacion/{id_publicacion}
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///           "id": 0,
        ///           "id_tipo_documento": 0,
        ///           "nombre_documento": "string",
        ///           "datos_documento": null, (Solo recupera los datos cuando se quiere ver o descargar)
        ///           "extension": "string",
        ///           "id_vinculacion": 0
        ///         }  
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado de documentos y su id de vinculacion </response>
        /// <response code="204" >No se encontro ningun documento asociado </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{id_publicacion}")]
        [ActionName("ListarDocumentoXPublicacion")]
        [ProducesResponseType(typeof(IEnumerable<DocumentosPrensa>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<DocumentosPrensa>> ListarDocumentoXPublicacion(int id_publicacion)
        {
            try
            {
                List<DocumentosPrensa>? listadoPubli = PressAdapter.BuscarDocumentosXPublicacion(id_publicacion);

                if (listadoPubli == null) return Conflict();
                if (listadoPubli.Count == 0) return NoContent();

                return Ok(listadoPubli);

            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, this.Request.Path, ex.Message, ControllerName);
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite la modificacion de una publicacion
        /// </summary>
        /// <param name="id"> El ID de la publicacion a modificar</param>
        /// <param name="publicacion"> Los datos modificados de la publicacion</param>
        /// <returns>Un publicacion modificado en BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     PUT /api/Prensa/ModificarPublicacion/{id}
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "titulo_publicacion": "string",
        ///       "descripcion": "string",
        ///       "fecha_inicio": "2024-08-01T15:42:11.205Z",
        ///       "fecha_vigencia": "2024-08-01T15:42:11.205Z",
        ///       "prioridad": 0,
        ///       "no_dar_baja": true,
        ///       "visualizaciones": 0
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "titulo_publicacion": "string",
        ///       "descripcion": "string",
        ///       "fecha_inicio": "2024-08-01T15:42:11.205Z",
        ///       "fecha_vigencia": "2024-08-01T15:42:11.205Z",
        ///       "prioridad": 0,
        ///       "no_dar_baja": true,
        ///       "visualizaciones": 0
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve la publicacion modificada en BD </response>
        /// <response code="400" >Ocurre un error en la consulta</response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{id}")]
        [ActionName("ModificarPublicacion")]
        [ProducesResponseType(typeof(Publicaciones), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Publicaciones> ModificarPublicacion(int id, Publicaciones publicacion)
        {
            try
            {
                if (id != publicacion.id) return BadRequest();

                if ( TienePermiso(15))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData != null &&
                       userData.Length > 0 &&
                       userData != "NO DATA" &&
                       int.TryParse(userData.Split(',')[2], out int idUserMod))
                    {
                        publicacion = PressAdapter.ModificarPublicaciones(publicacion, idUserMod);
                        if (publicacion.id != -1) return Ok(publicacion);
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
        /// Permite la modificacion de un documento
        /// </summary>
        /// <param name="id"> El ID del documento en la BD</param>
        /// <param name="idTipoDocumento"> El ID del documento en la BD</param>
        /// <param name="documento"> Los datos modificados del documento</param>
        /// <returns>Un documento modificado en BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     PUT /api/Prensa/ModificarDocumento/{id}
        ///     BODY:
        ///     Un archivo de los tipos permitidos
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
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
        [HttpPut("{id}/{idTipoDocumento}")]
        [ActionName("ModificarDocumento")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status413PayloadTooLarge)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> ModificarDocumento(int id,int idTipoDocumento,[FromBody] IFormFile documento)
        {
            try
            {
                if (TienePermiso(149))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData != null &&
                       userData.Length > 0 &&
                       userData != "NO DATA" &&
                       int.TryParse(userData.Split(',')[2], out int idUserMod))
                    {
                        //Que no supere 50Mb
                        if (documento.Length < 50000000)
                        {
                            string usuarioActual = userData.Split(',')[2];

                            DocumentosPrensa? doc = PressAdapter.ObtenerDocumentoXId(id);
                            if(doc != null && doc.id != -1)
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
                                        using var stream = new FileStream(filePath, FileMode.Create);
                                        await documento.CopyToAsync(stream);

                                        //Lo unico que cambia es el tamaño y su tipo de documento, el nombre y la ruta se mantienen
                                        doc.id_tipo_documento = idTipoDocumento;
                                        doc.tamanio = documento.Length;
                                        doc = PressAdapter.ModificarDocumento(doc, idUserMod);

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
                else return Forbid();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, this.Request.Path, ex.Message, ControllerName);
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite crear una publicacion
        /// </summary>
        /// <param name="publicacion">La publicacion que deseamos enviar, se envia en el Body</param>
        /// <returns>Una publicacion cargada en la BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Prensa/CrearPublicacion
        ///
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "titulo_publicacion": "string",
        ///       "descripcion": "string",
        ///       "fecha_inicio": "2024-08-01T15:42:11.205Z",
        ///       "fecha_vigencia": "2024-08-01T15:42:11.205Z",
        ///       "prioridad": 0,
        ///       "no_dar_baja": true,
        ///       "visualizaciones": 0
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "titulo_publicacion": "string",
        ///       "descripcion": "string",
        ///       "fecha_inicio": "2024-08-01T15:42:11.205Z",
        ///       "fecha_vigencia": "2024-08-01T15:42:11.205Z",
        ///       "prioridad": 0,
        ///       "no_dar_baja": true,
        ///       "visualizaciones": 0
        ///     }  
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve la publicacion creada en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost]
        [ActionName("CrearPublicacion")]
        [Authorize]
        [ProducesResponseType(typeof(Publicaciones), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Publicaciones> CrearPublicacion([FromBody] Publicaciones publicacion)
        {
            try
            {
                if (TienePermiso(14))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData != null &&
                       userData.Length > 0 &&
                       userData != "NO DATA" &&
                       int.TryParse(userData.Split(',')[2], out int idUserCrea))
                    {
                        publicacion = PressAdapter.CrearPublicacion(publicacion, idUserCrea);
                        if (publicacion.id != -1) return Created("Publicacion Creada", publicacion);
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
        /// Permite crear un nuevo documento que puede ser consumido por CUALQUIERA sin necesidad de TOKEN
        /// </summary>
        /// <param name="archivo">El archivo que deseamos almacenar en la BD</param>
        /// <param name="idTipoDocumento">El tipo de documento</param>
        /// <returns>Una inscripcion creada en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Prensa/CrearDocumentoPrensa
        ///     BODY:
        ///     Un archivo de los tipos permitidos
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
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
        [Authorize]
        [Consumes("multipart/form-data")]
        [ActionName("CrearDocumentoPrensaLibre")]
        [ProducesResponseType(typeof(DocumentosPrensa), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status413PayloadTooLarge)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> CrearDocumentoPrensaLibre(int idTipoDocumento,IFormFile archivo)
        {
            try
            {
                if (TienePermiso(148))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData != null &&
                       userData.Length > 0 &&
                       userData != "NO DATA" &&
                       int.TryParse(userData.Split(',')[2], out int idUserMod))
                    {
                        //Que no supere 50Mb
                        if (archivo.Length < 50000000)
                        {
                            string usuarioActual = userData.Split(',')[2];
                            SettingsReader set = SettingsReader.GetAppSettings();
                            string uploadsPath = set.GetFilesLocation();
                            if (uploadsPath != "ERROR")
                            {
                                uploadsPath = Path.Combine(uploadsPath, "Prensa","Publico");

                                // crear carpeta si no existe
                                if (!Directory.Exists(uploadsPath))
                                    Directory.CreateDirectory(uploadsPath);

                                // nombre único
                                var fileName = $"{Guid.NewGuid()}_{archivo.FileName}";
                                var filePath = Path.Combine(uploadsPath, fileName);

                                // guardar archivo
                                using var stream = new FileStream(filePath, FileMode.Create);
                                await archivo.CopyToAsync(stream);

                                DocumentosPrensa doc = new()
                                {
                                    id = -1,
                                    id_tipo_documento = idTipoDocumento,//Tengo que ver que hago con esto
                                    nombre_documento = archivo.FileName,
                                    ruta = Path.Combine("Prensa", "Publico", fileName),//Es una ruta relativa desde el origen del sistema de archivos
                                    tamanio = archivo.Length
                                };

                                doc = PressAdapter.CrearDocumento(doc, idUserMod);

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
                else return Forbid();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, this.Request.Path, ex.Message, ControllerName);
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite crear un nuevo documento que puede ser consumido unicamente de manera interna
        /// </summary>
        /// <param name="archivo">El archivo que deseamos almacenar en la BD</param>
        /// <returns>Una inscripcion creada en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Prensa/CrearDocumentoPrensa
        ///     BODY:
        ///     Un archivo de los tipos permitidos
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
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
        [Authorize]
        [Consumes("multipart/form-data")]
        [ActionName("CrearDocumentoPrensaInterno")]
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status413PayloadTooLarge)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> CrearDocumentoPrensaInterno(IFormFile archivo)
        {
            try
            {
                if (TienePermiso(148))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData != null &&
                       userData.Length > 0 &&
                       userData != "NO DATA" &&
                       int.TryParse(userData.Split(',')[2], out int idUserMod))
                    {
                        //Que no supere 50Mb
                        if (archivo.Length < 50000000)
                        {
                            string usuarioActual = userData.Split(',')[2];
                            SettingsReader set = SettingsReader.GetAppSettings();
                            string uploadsPath = set.GetFilesLocation();
                            if (uploadsPath != "ERROR")
                            {
                                uploadsPath = Path.Combine(uploadsPath, "Prensa", "Interno");

                                // crear carpeta si no existe
                                if (!Directory.Exists(uploadsPath))
                                    Directory.CreateDirectory(uploadsPath);

                                // nombre único
                                var fileName = $"{Guid.NewGuid()}_{archivo.FileName}";
                                var filePath = Path.Combine(uploadsPath, fileName);

                                // guardar archivo
                                using var stream = new FileStream(filePath, FileMode.Create);
                                await archivo.CopyToAsync(stream);

                                DocumentosPrensa doc = new()
                                {
                                    id = -1,
                                    id_tipo_documento = 0,//Tengo que ver que hago con esto
                                    nombre_documento = archivo.FileName,
                                    ruta = Path.Combine("Prensa", fileName),//Es una ruta relativa desde el origen del sistema de archivos
                                    tamanio = archivo.Length,
                                };

                                doc = PressAdapter.CrearDocumento(doc, idUserMod);

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
                else return Forbid();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, this.Request.Path, ex.Message, ControllerName);
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite crear un vinculo entre publicacion y documentacion
        /// </summary>
        /// <param name="id_publicacion">El ID de la publicacion</param>
        /// <param name="id_documento">El ID del documento</param>
        /// <returns>Una vinculacion entre publicacion y documentor</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Prensa/CrearVinculoDocPubli/{id_publicacion}/{id_documento}
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "nombre_deporte": "string",
        ///       "fecha_inscripcion": "2024-07-25T23:07:14.763Z"
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve la vinculacion creada en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost("{id_publicacion}/{id_documento}")]
        [ActionName("CrearVinculoDocPubli")]
        [Authorize]
        [ProducesResponseType(typeof(VinculoDocPublic), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VinculoDocPublic> CrearVinculoDocPubli(int id_publicacion, int id_documento)
        {
            try
            {
                if (TienePermiso(14))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData != null &&
                       userData.Length > 0 &&
                       userData != "NO DATA" &&
                       int.TryParse(userData.Split(',')[2], out int idUserMod))
                    {
                        VinculoDocPublic doc = PressAdapter.CrearVinPubli(id_publicacion, id_documento);
                        if (doc.id != -1) return Ok(doc);
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
        /// Permite eliminar publicacion
        /// </summary>
        /// <param name="id">Es el id de la publicacion</param>
        /// <returns>Un mensaje de OK o la publicacion que no se pudo eliminar</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Prensa/EliminarPublicacion/{id}
        ///     
        ///     RESPONSE:
        ///     {
        ///         "Publicacion Eliminada"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve OK y un mensaje </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT</response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y devuelve el horario </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>        
        [HttpDelete("{id}")]
        [ActionName("EliminarPublicacion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> EliminarPublicacion(int id)
        {
            try
            {
                if (TienePermiso(17))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        if (PressAdapter.EliminarPublicacion(id)) return Ok("Publicacion Eliminada");
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
        /// Permite eliminar vinculacion entre publicacion y documentos
        /// </summary>
        /// <param name="id_vinculacion">Es el id de la vinculacion (Se puede recuperar en: Prensa/ListarDocumentoXPublicacion/{id_publicacion})</param>
        /// <returns>Un mensaje de OK o el horario que no se pudo eliminar</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Prensa/EliminarVinculacionDocumentacion/{id_vinculacion}
        ///     
        ///     RESPONSE:
        ///     {
        ///         "Vinculacion Eliminada"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve OK y un mensaje </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT</response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y devuelve el horario </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>        
        [HttpDelete("{id_vinculacion}")]
        [ActionName("EliminarVinculacionDocumentacion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> EliminarVinculacionDocumentacion(int id_vinculacion)
        {
            try
            {
                if (TienePermiso(150))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        if (PressAdapter.EliminarVinculacion(id_vinculacion)) return Ok("Publicacion Eliminada");
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
        /// Permite eliminar documentos de prensa
        /// </summary>
        /// <param name="id_documento">Es el id del documento a eliminar</param>
        /// <returns>Un mensaje de OK o el documento que no se pudo eliminar porque esta vinculado</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization y que el documento no este vinculado a ninguna publicacion
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Prensa/EliminarDocumentoPrensa/{id_documento}
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
        [ActionName("EliminarDocumentoPrensa")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> EliminarDocumentoPrensa(int id_documento)
        {
            try
            {
                if (TienePermiso(150))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        DocumentosPrensa? doc = PressAdapter.ObtenerDocumentoXId(id_documento);
                        if (doc != null && doc.id != -1)
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
                                    if (PressAdapter.EliminarDocumento(id_documento)) return Ok("Documento Eliminado");
                                    else return Conflict("Se elimino el archivo del sistema de archivos pero no su registro en BD");
                                }
                                else return NotFound();
                            }
                            else return Conflict("Sistema de Archivos no encontrado");
                        }
                        else return NotFound();
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
