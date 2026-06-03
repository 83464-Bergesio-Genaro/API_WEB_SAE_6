using API_WEB_SAE_6.Adapters;
using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models;
using API_WEB_SAE_6.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.ComponentModel.DataAnnotations;
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
        private readonly string ControllerName = "EstudianteController";
        /// <summary>
        /// 
        /// </summary>
        public EstudianteController() { }
        /// <summary>
        /// Recupera los datos del estudiante por su legajo
        /// </summary>
        /// <returns>Los datos del estudiante</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Estudiantes/BuscarPerfilEstudiante/{legajo}
        ///     
        ///     RESPONSE:
        ///     {
        ///       "legajo": "83464@sistemas.frc.utn.edu.ar",
        ///       "nombres": "Bergesio Genaro Rafael",
        ///       "apellidos": "",
        ///       "email": "",
        ///       "telefono": "",
        ///       "fecha_nacimiento": null,
        ///       "cuil": "",
        ///       "dni": "",
        ///       "direccion": ""
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve el perfil del estudiante</response>
        /// <response code="204" >No se encontro ningun estudiante </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{legajo}")]
        [ActionName("BuscarPerfilEstudiante")]
        [ProducesResponseType(typeof(PerfilEstudiante), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PerfilEstudiante> BuscarPerfilEstudiante(string legajo)
        {
            try
            {
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                if (userData != null && userData != "NO DATA" && TienePermiso(154))
                {
                    PerfilEstudiante? doc = EstudianteAdapter.BuscarEstudianteXLegajo(legajo);
                    if (doc != null && doc.legajo != "" && doc.legajo == legajo)
                        return Ok(doc);
                    else return NoContent();
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
        /// Permite la modificacion del perfil del alumno
        /// </summary>
        /// <param name="legajo"> El legajo del estudiante a modificar</param>
        /// <param name="perfilEstudiante"> Los datos modificados del estudiante</param>
        /// <returns>Un perfil modificado</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     PUT /api/Salud/ModificarPerfilEstudiante/{legajo}
        ///     BODY:
        ///     {
        ///       "legajo": "83464@sistemas.frc.utn.edu.ar",
        ///       "nombres": "Bergesio Genaro Rafael",
        ///       "apellidos": "",
        ///       "email": "",
        ///       "telefono": "",
        ///       "fecha_nacimiento": null,
        ///       "cuil": "",
        ///       "dni": "",
        ///       "direccion": ""
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "legajo": "83464@sistemas.frc.utn.edu.ar",
        ///       "nombres": "Bergesio Genaro Rafael",
        ///       "apellidos": "",
        ///       "email": "",
        ///       "telefono": "",
        ///       "fecha_nacimiento": null,
        ///       "cuil": "",
        ///       "dni": "",
        ///       "direccion": ""
        ///     }
        /// </remarks>
        /// <response code="200" >Devuelve el estudiante modificado en BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{legajo}")]
        [ActionName("ModificarPerfilEstudiante")]
        [Authorize]
        [ProducesResponseType(typeof(PerfilEstudiante), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PerfilEstudiante> ModificarPerfilEstudiante(string legajo, [FromBody, Required] PerfilEstudiante perfilEstudiante)
        {
            try
            {
                if (legajo != perfilEstudiante.legajo) return BadRequest();
                //El numero de funcion es: 71
                if (TienePermiso(154))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData != null &&
                       userData.Length > 0 &&
                       userData != "NO DATA" &&
                       int.TryParse(userData.Split(',')[2], out int idUserMod))
                    {
                        perfilEstudiante = EstudianteAdapter.ModificarEstudiante(perfilEstudiante);
                        if (perfilEstudiante.legajo != "") return Ok(perfilEstudiante);
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
                    DocumentosEstudiante? doc = EstudianteAdapter.BuscarDocumentoXId(id);

                    if (doc != null && doc.id != -1)
                    {
                        string legajoRegistrado = userData.Split(',')[0];
                        string idPerfil = userData.Split(',')[1];
                        //Valida que quien descarga sea empleado, administrador o sea el legajo del mismo estudiante
                        if (idPerfil == "2" || idPerfil == "5" || legajoRegistrado == doc.legajo)
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
                    string idPerfil = userData.Split(',')[1];
                    if (idPerfil == "2" || idPerfil == "5" || legajoRegistrado == legajo)
                    {
                        List<DocumentosEstudiante>? documentos = EstudianteAdapter.BuscarDocumentosXLegajo(legajo);

                        if (documentos != null &&
                            documentos.Count > 0 ) return Ok(documentos);
                        else
                        {
                            Logger.RegistrarDatos(Logger.LogOptions.Alerta, "DescargarDocumentacionXId", "Intento descargar documentacion que no era suya conociendo el ID del mismo. HOST:" + HttpContext.Request.Host.Value, ControllerName);
                            return Forbid();
                        }
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
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(DocumentosEstudiante), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status413PayloadTooLarge)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DocumentosEstudiante>> ModificarDocumento(int id,IFormFile documento)
        {
            try
            {
                //Busca los datos guardados de la claim para verificar todo
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";

                if (userData != null && userData != "NO DATA" && TienePermiso(154))
                {
                    //Que no supere 50Mb
                    if (documento.Length < 50000000)
                    {
                        string legajo = userData.Split(",")[0];
                        List<DocumentosEstudiante>? docs = EstudianteAdapter.BuscarDocumentosXLegajo(legajo);
                        
                        if (docs != null && docs.Count > 0)
                        {
                            //Que busque entre los documentos de esta persona que el legajo esto este existe. Es decir esta asignado a esta persona
                            DocumentosEstudiante? doc = docs.Find(x => x.legajo == legajo);
                            SettingsReader set = SettingsReader.GetAppSettings();
                            string uploadsPath = set.GetFilesLocation();
                            
                            if (uploadsPath != "ERROR" && doc != null)
                            {
                                string idPerfil = userData.Split(",")[1];
                                string idUserMod = userData.Split(',')[2];
                                if (idPerfil == "1" && doc.legajo != legajo) return BadRequest();
                                string filePath = Path.Combine(uploadsPath, doc.ruta);
                                //Verifica si existe el archivo
                                FileInfo fileInfo = new(filePath);

                                if (fileInfo.Exists)
                                {
                                    using var stream = new FileStream(filePath, FileMode.Create);
                                    await documento.CopyToAsync(stream);

                                    //Lo unico que cambia es el tamaño
                                    doc.tamanio = documento.Length;
                                    doc = EstudianteAdapter.ModificarDocumento(doc, idUserMod);

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
        [HttpPost("{idTipoDocumento}")]
        [ActionName("CrearDocumentoEstudiante")]
        [Consumes("multipart/form-data")]
        [Authorize]
        [ProducesResponseType(typeof(DocumentosEstudiante), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status413PayloadTooLarge)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task <ActionResult<DocumentosEstudiante>> CrearDocumentoEstudiante(int idTipoDocumento,IFormFile archivo)
        {
            try
            {
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                if (userData != null &&
                   userData.Length > 0 &&
                   userData != "NO DATA" &&
                   int.TryParse(userData.Split(',')[2], out int idUserMod) &&
                   TienePermiso(151))
                {
                    if (archivo.Length < 50000000)
                    {
                        string legajoActual = userData.Split(",")[0];
                        string usuarioActual = userData.Split(',')[2];
                        SettingsReader set = SettingsReader.GetAppSettings();
                        string uploadsPath = set.GetFilesLocation();
                        if (uploadsPath != "ERROR")
                        {
                            uploadsPath = Path.Combine(uploadsPath, "Estudiantes", legajoActual);

                            // crear carpeta si no existe
                            if (!Directory.Exists(uploadsPath))
                                Directory.CreateDirectory(uploadsPath);

                            // nombre único
                            var fileName = $"{Guid.NewGuid()}_{archivo.FileName}";
                            var filePath = Path.Combine(uploadsPath, fileName);

                            // guardar archivo
                            using var stream = new FileStream(filePath, FileMode.Create);
                            await archivo.CopyToAsync(stream);

                            DocumentosEstudiante doc = new()
                            {
                                id = -1,
                                id_tipo_documento = idTipoDocumento,//Tengo que ver que hago con esto
                                nombre_documento = archivo.FileName,
                                legajo = legajoActual,
                                ruta = Path.Combine("Estudiantes", legajoActual, fileName),//Es una ruta relativa desde el origen del sistema de archivos
                                tamanio = archivo.Length
                            };

                            doc = EstudianteAdapter.CrearDocumento(doc, idUserMod);

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
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        DocumentosEstudiante? doc = EstudianteAdapter.BuscarDocumentoXId(id_documento);
                        if (doc != null && doc.id != -1)
                        {
                            string legajoRegistrado = userData.Split(',')[0];
                            string idPerfil = userData.Split(',')[1];
                            if (idPerfil == "2" || idPerfil == "5" || legajoRegistrado == doc.legajo)
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
                                        if (EstudianteAdapter.EliminarDocumento(id_documento)) return Ok("Documento Eliminado");
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
                Logger.RegistrarDatos(Logger.LogOptions.Error,this.Request.Path, ex.Message, ControllerName);
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
