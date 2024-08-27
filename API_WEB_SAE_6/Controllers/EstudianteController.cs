using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Data;
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
        /// EN: The logger functions as a register of the exception that happen in the runtime. <br/>
        /// ES: El logger funciona como el registro de excpciones que pasan en tiempo de ejecuccion <br/>
        /// </summary>
        private readonly Logger _logger = new();

        private readonly IConfiguration _config;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public EstudianteController(IConfiguration config)
        {
            _config = config;
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
        public async Task<ActionResult<DocumentosEstudiante>> DescargarDocumentacionXId(int id)
        {
            try
            {
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                if (userData == null || userData == "NO DATA") return Unauthorized();

                string legajoRegistrado = userData.Split(',')[0];
                Dictionary<string, string> parametros = new() {
                        {"@id_doc", id.ToString() }
                };
                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_ESTUDIANTE_Buscar_Documento_Id", parametros);
                if (respuesta.Rows.Count == 0) return NoContent();
                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                DocumentosEstudiante documento = new(respuesta.Rows[0]);

                //Solo empleados, administrador y el mismo estudiante puede ver la documentacion asociada a un legajo
                if (legajoRegistrado == documento.legajo || await TienePermiso(154)) return Ok(documento);
                else
                {
                    _logger.RegistrarAnomalia(HttpContext.Request.Host.Value, "Intento descargar documentacion que no era suya conociendo el ID del mismo.");
                    return Forbid();
                }
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR LISTANDO DOCUMENTACION ESTUDIANTE");
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
        public async Task<ActionResult<IEnumerable<DocumentosEstudiante>>> ListarDocumentacionXLegajo(string legajo)
        {
            try
            {
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                if (userData == null || userData == "NO DATA") return Unauthorized();

                string legajoRegistrado = userData.Split(',')[0];
                //Solo la misma persona que sube los archivos puede ver su documentacion
                if (legajoRegistrado == legajo || await TienePermiso(154))
                {
                    Dictionary<string, string> parametros = new() {
                        {"@legajo", legajo }
                    };
                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_ESTUDIANTE_Buscar_Documento_Legajo", parametros);
                    if (respuesta.Rows.Count == 0) return NoContent();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                    List<DocumentosEstudiante> listadoDocumentos = new();
                    foreach (DataRow row in respuesta.Rows)
                    {
                        DocumentosEstudiante documentos = new(row);
                        listadoDocumentos.Add(documentos);
                    }
                    return Ok(listadoDocumentos);
                }
                else
                {
                    if (legajoRegistrado == legajo)
                        _logger.RegistrarAnomalia(HttpContext.Request.Host.Value, "No es Empleado y mando un legajo diferente del suyo en el documento.");
                    return Forbid();
                }
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR LISTANDO DOCUMENTACION ESTUDIANTE");
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
        public async Task<ActionResult<DocumentosEstudiante>> ModificarDocumento(int id, DocumentosEstudiante documento)
        {

            try
            {
                //No manda datos en el archivo
                if (id != documento.id || documento.datos_documento == null) return BadRequest();

                //Busca los datos guardados de la claim para verigicar todo
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                if (userData == null || userData == "NO DATA") return Unauthorized();

                string legajoRegistrado = userData.Split(',')[0];

                Dictionary<string, string> parametros = new() {
                        {"@id_doc", id.ToString() }
                };

                //Vamos a buscar el archivo para verificar que exista y que no le cambiaron el legajo
                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_ESTUDIANTE_Buscar_Documento_Id", parametros);
                if (respuesta.Rows.Count == 0) return NoContent();
                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                DocumentosEstudiante documentoMod = new(respuesta.Rows[0]);

                //Tiene diferente documento al guardado o no es su documento y tampoco es empleado.
                if (documentoMod.legajo != documento.legajo || legajoRegistrado != documentoMod.legajo && !await TienePermiso(152))
                {
                    _logger.RegistrarAnomalia(HttpContext.Request.Host.Value, "Intento la manipulacion de documentos que no era de el o cambiar el legajo del documento");
                    return Forbid();
                }
                else
                {
                    //Que los datos no sean nulos y no supere 50Mb
                    if (documento.datos_documento?.Length < 50000000)
                    {
                        string usuarioActual = userData.Split(',')[2];
                        Dictionary<string, object> parametros2 = new() {
                            {"@id_doc", documento.id.ToString() },
                            {"@legajo", documento.legajo },
                            {"@id_tipo", documento.id_tipo_documento.ToString() },
                            {"@nombre", documento.nombre_documento },
                            {"@datos", documento.datos_documento },
                            {"@id_usuario_mod",usuarioActual },
                        };

                        respuesta = GeneralAdapterSQL.ExecuteStoredProcedureDocument(_config, "MODULO_ESTUDIANTE_Modificar_Documento_Estudiante", parametros2);
                        //En este caso sino modifica nada es un conflicto en la BD
                        if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                        return Ok(new DocumentosEstudiante(respuesta.Rows[0]));
                    }
                    else return StatusCode(413, "El archivo no debe superar los 50Mb");
                }
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR MODIFICANDO DOCUMENTO");
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
        public async Task<ActionResult<DocumentosEstudiante>> CrearDocumentoEstudiante(DocumentosEstudiante documento)
        {
            try
            {
                //No manda datos en el archivo
                if (documento.datos_documento == null) return BadRequest();

                //Lo estudiantes solo pueden crear documentacion referida a su legajo
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                string legajoRegistrado = userData.Split(',')[0];
                if (legajoRegistrado == documento.legajo || await TienePermiso(151))
                {
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        string usuarioActual = userData.Split(',')[2];
                        //Que no supere 50Mb
                        if (documento.datos_documento?.Length < 50000000)
                        {

                            Dictionary<string, object> parametros = new() {
                                {"@legajo", documento.legajo },
                                {"@id_tipo", documento.id_tipo_documento.ToString() },
                                {"@nombre", documento.nombre_documento },
                                {"@datos", documento.datos_documento },
                                {"@id_usuario_alta",usuarioActual },
                            };
                            DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedureDocument(_config, "MODULO_ESTUDIANTE_Crear_Documento_Estudiante", parametros);
                            //En este caso sino crea es un error en la BD
                            if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                            return Created("Documento Creado", new DocumentosEstudiante(respuesta.Rows[0]));
                        }
                        else return StatusCode(413, "El archivo no debe superar los 50Mb");
                    }
                }
                else
                {
                    if (legajoRegistrado != documento.legajo)
                        _logger.RegistrarAnomalia(HttpContext.Request.Host.Value, "No es Empleado y mando un legajo diferente del suyo en el documento.");
                    return Forbid();
                }

            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR CREANDO DOCUMENTO PRENSA");
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
        public async Task<ActionResult> EliminarDocumentoEstudiante(int id_documento)
        {
            try
            {
                //Busca los datos guardados de la claim para verigicar todo
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                if (userData == null || userData == "NO DATA") return Unauthorized();

                string legajoRegistrado = userData.Split(',')[0];

                Dictionary<string, string> parametros = new() {
                        {"@id_doc", id_documento.ToString() }
                };

                //Vamos a buscar el archivo para verificar que exista y que no le cambiaron el legajo
                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_ESTUDIANTE_Buscar_Documento_Id", parametros);
                if (respuesta.Rows.Count == 0) return NoContent();
                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                DocumentosEstudiante documentoEliminar = new(respuesta.Rows[0]);

                //Tiene diferente documento al guardado o no es su documento y tampoco es empleado.
                if (documentoEliminar.legajo != documentoEliminar.legajo || legajoRegistrado != documentoEliminar.legajo && !await TienePermiso(153))
                {
                    _logger.RegistrarAnomalia(Request.Host.Host, "Intento la eliminacion de documentos que no era de este estudiante");
                    return Forbid();
                }
                else
                {
                    respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_PRENSA_Eliminar_Documento_Prensa", parametros);

                    if (respuesta.Rows.Count > 0)
                    {
                        if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                        //Si es mayor a 0 significa que no se elimino asi que devolvemos dicho registro
                        else return Conflict(new DocumentosEstudiante(respuesta.Rows[0]));
                    }
                    else return Ok("Documento Eliminado");
                }
            }
            catch (Exception ex)
            {
                _logger.RegistrarERROR(ex, "ERROR ELIMINANDO DOCUMENTO");
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
