using API_WEB_SAE_6.Adapters;
using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace API_WEB_SAE_6.Controllers
{
    ///// <summary>
    ///// Es el controlador para prensa y sus publicaciones
    ///// </summary>
    //[EnableCors("CorsRules")]
    //[Route("api/[controller]/[action]")]
    //[ApiController]
    //public class PrensaController : Controller
    //{
    //    /// <summary>
    //    /// EN: The logger functions as a register of the exception that happen in the runtime. <br/>
    //    /// ES: El logger funciona como el registro de excpciones que pasan en tiempo de ejecuccion <br/>
    //    /// </summary>
    //    private readonly Logger _logger = new();

    //    private readonly IConfiguration _config;
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="config"></param>
    //    public PrensaController(IConfiguration config)
    //    {
    //        _config = config;
    //    }
    //    /// <summary>
    //    /// Recupera todas las publicaciones
    //    /// </summary>
    //    /// <returns>Un listado de todas las publicaciones</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Prensa/ListarPublicacionesCompleto/
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///         {
    //    ///           "id": 0,
    //    ///           "titulo_publicacion": "string",
    //    ///           "descripcion": "string",
    //    ///           "fecha_inicio": "2024-08-01T15:42:11.205Z",
    //    ///           "fecha_vigencia": "2024-08-01T15:42:11.205Z",
    //    ///           "prioridad": 0,
    //    ///           "no_dar_baja": true,
    //    ///           "visualizaciones": 0
    //    ///         }
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un listado de publicaciones</response>
    //    /// <response code="204" >No se encontro ninguna publicacion </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet]
    //    [Authorize]
    //    [ActionName("ListarPublicacionesCompleto")]
    //    [ProducesResponseType(typeof(IEnumerable<Publicaciones>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<Publicaciones>>> ListarPublicacionesCompleto()
    //    {
    //        try
    //        {
    //            if (await TienePermiso(16))
    //            {
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_PRENSA_Listar_Publicaciones_Completo");
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<Publicaciones> listadoDocumentos = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    Publicaciones documentos = new(row);
    //                    listadoDocumentos.Add(documentos);
    //                }
    //                return Ok(listadoDocumentos);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR LISTANDO PUBLICACIONES");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Recupera todas las publicaciones activas
    //    /// </summary>
    //    /// <returns>Un listado de publicaciones activas</returns>
    //    /// <remarks>
    //    /// NOTA: Este endpoint es para libre consumo sin importar el usuario
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Prensa/ListarPublicacionesActivas/
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///         {
    //    ///           "id": 0,
    //    ///           "titulo_publicacion": "string",
    //    ///           "descripcion": "string",
    //    ///           "fecha_inicio": "2024-08-01T15:42:11.205Z",
    //    ///           "fecha_vigencia": "2024-08-01T15:42:11.205Z",
    //    ///           "prioridad": 0,
    //    ///           "no_dar_baja": true,
    //    ///           "visualizaciones": 0
    //    ///         }
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un listado de publicaciones</response>
    //    /// <response code="204" >No se encontro ninguna publicacion activa </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet]
    //    [ActionName("ListarPublicacionesActivas")]
    //    [ProducesResponseType(typeof(IEnumerable<Publicaciones>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<Publicaciones>>> ListarPublicacionesActivas()
    //    {
    //        try
    //        {
    //            DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_PRENSA_Listar_Publicaciones_Activas");
    //            if (respuesta.Rows.Count == 0) return NoContent();
    //            if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //            List<Publicaciones> listadoDocumentos = new();
    //            foreach (DataRow row in respuesta.Rows)
    //            {
    //                Publicaciones documentos = new(row);
    //                listadoDocumentos.Add(documentos);
    //            }
    //            return Ok(listadoDocumentos);

    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR LISTANDO PUBLICACIONES ACTIVAS");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Una publicacion este ID
    //    /// </summary>
    //    /// <param name="id_publicacion">Es el ID de la publicacion en la BD</param>
    //    /// <returns>Una publicacion</returns>
    //    /// <remarks>
    //    /// NOTA: Este endpoint es para libre consumo sin importar el usuario
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Prensa/ListarDocumentoXPublicacion/{id_publicacion}
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "titulo_publicacion": "string",
    //    ///       "descripcion": "string",
    //    ///       "fecha_inicio": "2024-08-01T15:42:11.205Z",
    //    ///       "fecha_vigencia": "2024-08-01T15:42:11.205Z",
    //    ///       "prioridad": 0,
    //    ///       "no_dar_baja": true,
    //    ///       "visualizaciones": 0
    //    ///     }  
    //    /// </remarks>
    //    /// <response code="200" >Devuelve una publicacion con este ID </response>
    //    /// <response code="204" >No se encontro ninguna publicacion asociada</response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet("{id_publicacion}")]
    //    [ActionName("ObtenerPublicacionXId")]
    //    [ProducesResponseType(typeof(Publicaciones), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<Publicaciones>>> ObtenerPublicacionXId(int id_publicacion)
    //    {
    //        try
    //        {
    //            Dictionary<string, string> parametros = new() {
    //                    {"@id_publicacion",id_publicacion.ToString() }
    //            };
    //            DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_PRENSA_Buscar_Publicacion_Id", parametros);
    //            if (respuesta.Rows.Count == 0) return NoContent();
    //            if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //            return Ok(new Publicaciones(respuesta.Rows[0]));

    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR BUSCANDO DOCUMENTOS DE PUBLICACION");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Recupera todos los documentos que tenemos guardados
    //    /// </summary>
    //    /// <returns>Un listado de todos los documentos</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Prensa/ListarDocumentosSinData/
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///         {
    //    ///           "id": 0,
    //    ///           "id_tipo_documento": 0,
    //    ///           "nombre_documento": "string",
    //    ///           "datos_documento": null,(Solo recupera los datos cuando se quiere ver o descargar)
    //    ///           "extension": "string",
    //    ///           "id_vinculacion": null (Este atributo solo lo recupera si lo buscamos por publicacion)
    //    ///         }  
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un listado de documentos</response>
    //    /// <response code="204" >No se encontro ningun documento </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet]
    //    [Authorize]
    //    [ActionName("ListarDocumentosSinData")]
    //    [ProducesResponseType(typeof(IEnumerable<DocumentosPrensa>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<DocumentosPrensa>>> ListarDocumentosSinData()
    //    {
    //        try
    //        {
    //            if (await TienePermiso(17))
    //            {
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_PRENSA_Listar_Documentos_Prensa");
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<DocumentosPrensa> listadoDocumentos = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    DocumentosPrensa documentos = new(row);
    //                    listadoDocumentos.Add(documentos);
    //                }
    //                return Ok(listadoDocumentos);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR LISTANDO DOCUMENTOS PRENSA");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Descarga el documento por su ID
    //    /// </summary>
    //    /// <returns>Un documento para visualizar en el front</returns>
    //    /// <remarks>
    //    /// NOTA: Este endpoint es para libre consumo sin importar el usuario
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Prensa/DescargarDocumentoXId/{id}
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "id_tipo_documento": 0,
    //    ///       "nombre_documento": "string",
    //    ///       "datos_documento": "string",
    //    ///       "extension": "string",
    //    ///       "id_vinculacion": null (Este atributo solo lo recupera si lo buscamos por publicacion)
    //    ///     }  
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un documento para su visualizacion o descarga </response>
    //    /// <response code="204" >No se encontro ningun documento con este ID </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet("{id}")]
    //    [ActionName("DescargarDocumentoXId")]
    //    [ProducesResponseType(typeof(DocumentosPrensa), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<DocumentosPrensa>> DescargarDocumentoXId(int id)
    //    {
    //        try
    //        {
    //            Dictionary<string, string> parametros = new() {
    //                    {"@id_doc",id.ToString() }
    //            };
    //            DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_PRENSA_Buscar_Documento_Id", parametros);
    //            if (respuesta.Rows.Count == 0) return NoContent();
    //            if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //            DocumentosPrensa documentos = new(respuesta.Rows[0]);
    //            //string path = @"C:\Users\grber\Downloads\" + documentos.nombre_documento;
    //            //// Write/Export File content into new text file
    //            //System.IO.File.WriteAllBytes(path, documentos.datos_documento);

    //            if (documentos.datos_documento == null) return NoContent();
    //            else return Ok(documentos);

    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR BUSCANDO DOCUMENTO POR ID");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Lista los documentos asociados a una publicacion
    //    /// </summary>
    //    /// <returns>Un listado de documentos asociados a la publicacion</returns>
    //    /// <remarks>
    //    /// NOTA: Este endpoint es para libre consumo sin importar el usuario
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Prensa/ListarDocumentoXPublicacion/{id_publicacion}
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///         {
    //    ///           "id": 0,
    //    ///           "id_tipo_documento": 0,
    //    ///           "nombre_documento": "string",
    //    ///           "datos_documento": null, (Solo recupera los datos cuando se quiere ver o descargar)
    //    ///           "extension": "string",
    //    ///           "id_vinculacion": 0
    //    ///         }  
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un listado de documentos y su id de vinculacion </response>
    //    /// <response code="204" >No se encontro ningun documento asociado </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet("{id_publicacion}")]
    //    [ActionName("ListarDocumentoXPublicacion")]
    //    [ProducesResponseType(typeof(IEnumerable<DocumentosPrensa>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<DocumentosPrensa>>> ListarDocumentoXPublicacion(int id_publicacion)
    //    {
    //        try
    //        {
    //            Dictionary<string, string> parametros = new() {
    //                    {"@id_publicacion",id_publicacion.ToString() }
    //            };
    //            DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_PRENSA_Buscar_Documento_Publicacion", parametros);
    //            if (respuesta.Rows.Count == 0) return NoContent();
    //            if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //            List<DocumentosPrensa> listadoDocumentos = new();
    //            foreach (DataRow row in respuesta.Rows)
    //            {
    //                DocumentosPrensa documentos = new(row);
    //                listadoDocumentos.Add(documentos);
    //            }
    //            return Ok(listadoDocumentos);

    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR BUSCANDO DOCUMENTOS DE PUBLICACION");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite la modificacion de una publicacion
    //    /// </summary>
    //    /// <param name="id"> El ID de la publicacion a modificar</param>
    //    /// <param name="publicacion"> Los datos modificados de la publicacion</param>
    //    /// <returns>Un publicacion modificado en BD</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     PUT /api/Prensa/ModificarPublicacion/{id}
    //    ///     BODY:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "titulo_publicacion": "string",
    //    ///       "descripcion": "string",
    //    ///       "fecha_inicio": "2024-08-01T15:42:11.205Z",
    //    ///       "fecha_vigencia": "2024-08-01T15:42:11.205Z",
    //    ///       "prioridad": 0,
    //    ///       "no_dar_baja": true,
    //    ///       "visualizaciones": 0
    //    ///     }
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "titulo_publicacion": "string",
    //    ///       "descripcion": "string",
    //    ///       "fecha_inicio": "2024-08-01T15:42:11.205Z",
    //    ///       "fecha_vigencia": "2024-08-01T15:42:11.205Z",
    //    ///       "prioridad": 0,
    //    ///       "no_dar_baja": true,
    //    ///       "visualizaciones": 0
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve la publicacion modificada en BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta</response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPut("{id}")]
    //    [ActionName("ModificarPublicacion")]
    //    [ProducesResponseType(typeof(Publicaciones), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<Publicaciones>> ModificarPublicacion(int id, Publicaciones publicacion)
    //    {
    //        try
    //        {
    //            if (id != publicacion.id) return BadRequest();

    //            if (await TienePermiso(15))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                string usuarioActual = userData.Split(',')[2];
    //                Dictionary<string, string> parametros = new(){
    //                        {"@id_publi",publicacion.titulo_publicacion },
    //                        {"@titulo",publicacion.titulo_publicacion },
    //                        {"@descripcion",publicacion.descripcion},
    //                        {"@fecha_ini",publicacion.fecha_inicio.ToString() },
    //                        {"@fecha_vig",publicacion.fecha_vigencia.ToString()},
    //                        {"@no_dar_baja",publicacion.no_dar_baja.ToString() },
    //                        {"@prioridad",publicacion.prioridad.ToString() },
    //                        {"@visualizaciones",publicacion.visualizaciones.ToString() },
    //                        {"@id_usuario_mod",usuarioActual }
    //                };

    //                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_PRENSA_Modificar_Publicacion", parametros);

    //                //En este caso sino modifica nada es un conflicto en la BD
    //                if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                return Ok(new Publicaciones(respuesta.Rows[0]));
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR MODIFICANDO PUBLICACION");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite la modificacion de un documento
    //    /// </summary>
    //    /// <param name="id"> El ID del documento en la BD</param>
    //    /// <param name="documento"> Los datos modificados del documento</param>
    //    /// <returns>Un documento modificado en BD</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     PUT /api/Prensa/ModificarDocumento/{id}
    //    ///     BODY:
    //    ///     Un archivo de los tipos permitidos
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "id_tipo_documento": 0,
    //    ///       "nombre_documento": "string",
    //    ///       "datos_documento": byte[],
    //    ///       "extension": "string"
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve el documento modificado en BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
    //    /// <response code="413" >El archivo que se cargo supero los 50Mb </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPut("{id}")]
    //    [ActionName("ModificarDocumento")]
    //    [ProducesResponseType(typeof(Publicaciones), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status413PayloadTooLarge)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<Publicaciones>> ModificarDocumento(int id, IFormFile documento)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(149))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                //Que no supere 50Mb
    //                if (documento.Length < 50000000)
    //                {
    //                    string usuarioActual = userData.Split(',')[2];
    //                    byte[] fileBytes = new byte[documento.Length];

    //                    using (var ms = new MemoryStream())
    //                    {
    //                        documento.CopyTo(ms);
    //                        fileBytes = ms.ToArray();
    //                    }

    //                    DocumentosPrensa doc = new()
    //                    {
    //                        id = 0,
    //                        id_tipo_documento = 0,
    //                        nombre_documento = documento.FileName,
    //                        datos_documento = fileBytes
    //                    };

    //                    Dictionary<string, object> parametros = new() {
    //                        {"id_doc",id.ToString()},
    //                        {"@id_tipo", doc.id_tipo_documento.ToString() },
    //                        {"@nombre", doc.nombre_documento },
    //                        {"@datos", doc.datos_documento },
    //                        {"@id_usuario_mod",usuarioActual },
    //                    };

    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedureDocument(_config, "MODULO_PRENSA_Modificar_Documento_Prensa", parametros);

    //                    //En este caso sino modifica nada es un conflicto en la BD
    //                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                    return Ok(new DocumentosPrensa(respuesta.Rows[0]));
    //                }
    //                else return StatusCode(413, "El archivo no debe superar los 50Mb");
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR MODIFICANDO DOCUMENTOS");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite crear una publicacion
    //    /// </summary>
    //    /// <param name="publicacion">La publicacion que deseamos enviar, se envia en el Body</param>
    //    /// <returns>Una publicacion cargada en la BD</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Prensa/CrearPublicacion
    //    ///
    //    ///     BODY:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "titulo_publicacion": "string",
    //    ///       "descripcion": "string",
    //    ///       "fecha_inicio": "2024-08-01T15:42:11.205Z",
    //    ///       "fecha_vigencia": "2024-08-01T15:42:11.205Z",
    //    ///       "prioridad": 0,
    //    ///       "no_dar_baja": true,
    //    ///       "visualizaciones": 0
    //    ///     }
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "titulo_publicacion": "string",
    //    ///       "descripcion": "string",
    //    ///       "fecha_inicio": "2024-08-01T15:42:11.205Z",
    //    ///       "fecha_vigencia": "2024-08-01T15:42:11.205Z",
    //    ///       "prioridad": 0,
    //    ///       "no_dar_baja": true,
    //    ///       "visualizaciones": 0
    //    ///     }  
    //    ///     
    //    /// </remarks>
    //    /// <response code="201" >Devuelve la publicacion creada en la BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPost]
    //    [ActionName("CrearPublicacion")]
    //    [Authorize]
    //    [ProducesResponseType(typeof(Publicaciones), StatusCodes.Status201Created)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<Publicaciones>> CrearPublicacion([FromBody] Publicaciones publicacion)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(14))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {
    //                    string usuarioActual = userData.Split(',')[2];
    //                    Dictionary<string, string> parametros = new() {
    //                        {"@titulo",publicacion.titulo_publicacion },
    //                        {"@descripcion",publicacion.descripcion},
    //                        {"@fecha_ini",publicacion.fecha_inicio.ToString() },
    //                        {"@fecha_vig",publicacion.fecha_vigencia.ToString()},
    //                        {"@no_dar_baja",publicacion.no_dar_baja.ToString() },
    //                        {"@prioridad",publicacion.prioridad.ToString() },
    //                        {"@visualizaciones",publicacion.visualizaciones.ToString() },
    //                        {"@id_usuario_alta",usuarioActual }
    //                    };

    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_PRENSA_Crear_Publicacion", parametros);
    //                    //En este caso sino crea es un error en la BD
    //                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                    return Created("Publicacion Creada", new Publicaciones(respuesta.Rows[0]));
    //                }

    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR CREANDO PUBLICACION");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite crear un nuevo documento
    //    /// </summary>
    //    /// <param name="archivo">El archivo que deseamos almacenar en la BD</param>
    //    /// <returns>Una inscripcion creada en la base de datos o error</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Prensa/CrearDocumentoPrensa
    //    ///     BODY:
    //    ///     Un archivo de los tipos permitidos
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "id_tipo_documento": 0,
    //    ///       "nombre_documento": "string",
    //    ///       "datos_documento": byte[],
    //    ///       "extension": "string"
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="201" >Devuelve el documento creado en la BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="413" >El archivo que se cargo supero los 50Mb </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPost]
    //    [ActionName("CrearDocumentoPrensa")]
    //    [Authorize]
    //    [ProducesResponseType(typeof(DocumentosPrensa), StatusCodes.Status201Created)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status413PayloadTooLarge)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<DocumentosPrensa>> CrearDocumentoPrensa(IFormFile archivo)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(148))
    //            {

    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {
    //                    //Que no supere 50Mb
    //                    if (archivo.Length < 50000000)
    //                    {
    //                        string usuarioActual = userData.Split(',')[2];
    //                        byte[] fileBytes = new byte[archivo.Length];

    //                        using (var ms = new MemoryStream())
    //                        {
    //                            archivo.CopyTo(ms);
    //                            fileBytes = ms.ToArray();
    //                        }

    //                        DocumentosPrensa doc = new()
    //                        {
    //                            id = 0,
    //                            id_tipo_documento = 0,
    //                            nombre_documento = archivo.FileName,
    //                            datos_documento = fileBytes
    //                        };

    //                        Dictionary<string, object> parametros = new() {
    //                            {"@id_tipo", doc.id_tipo_documento.ToString() },
    //                            {"@nombre", doc.nombre_documento },
    //                            {"@datos", doc.datos_documento },
    //                            {"@id_usuario_alta",usuarioActual },
    //                        };
    //                        DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedureDocument(_config, "MODULO_PRENSA_Crear_Documento_Prensa", parametros);
    //                        //En este caso sino crea es un error en la BD
    //                        if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                        return Created("Documento Creado", new DocumentosPrensa(respuesta.Rows[0]));
    //                    }
    //                    else return StatusCode(413, "El archivo no debe superar los 50Mb");
    //                }
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR CREANDO DOCUMENTO PRENSA");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite crear un vinculo entre publicacion y documentacion
    //    /// </summary>
    //    /// <param name="id_publicacion">El ID de la publicacion</param>
    //    /// <param name="id_documento">El ID del documento</param>
    //    /// <returns>Una vinculacion entre publicacion y documentor</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Prensa/CrearVinculoDocPubli/{id_publicacion}/{id_documento}
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "nombre_deporte": "string",
    //    ///       "fecha_inscripcion": "2024-07-25T23:07:14.763Z"
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="201" >Devuelve la vinculacion creada en la BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPost("{id_publicacion}/{id_documento}")]
    //    [ActionName("CrearVinculoDocPubli")]
    //    [Authorize]
    //    [ProducesResponseType(typeof(VinculoDocPublic), StatusCodes.Status201Created)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<VinculoDocPublic>> CrearVinculoDocPubli(int id_publicacion, int id_documento)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(14))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {
    //                    string usuarioActual = userData.Split(',')[2];
    //                    Dictionary<string, string> parametros = new() {
    //                        {"@id_publicacion", id_publicacion.ToString() },
    //                        {"@id_documento", id_documento.ToString() },
    //                    };
    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_PRENSA_Vincular_Documento_Publicacion", parametros);
    //                    //En este caso sino crea es un error en la BD
    //                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                    return Created("Vinculacion Creada", new VinculoDocPublic(respuesta.Rows[0]));
    //                }

    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR CREANDO VINCULACION");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite eliminar publicacion
    //    /// </summary>
    //    /// <param name="id">Es el id de la publicacion</param>
    //    /// <returns>Un mensaje de OK o la publicacion que no se pudo eliminar</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Prensa/EliminarPublicacion/{id}
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///         "Publicacion Eliminada"
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve OK y un mensaje </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT</response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y devuelve el horario </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>        
    //    [HttpDelete("{id}")]
    //    [ActionName("EliminarPublicacion")]
    //    [ProducesResponseType(StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult> EliminarPublicacion(int id)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(17))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {
    //                    Dictionary<string, string> parametros = new() {
    //                       { "@id_publicacion",id.ToString() }
    //                    };
    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_PRENSA_Eliminar_Publicacion", parametros);

    //                    if (respuesta.Rows.Count > 0)
    //                    {
    //                        if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                        //Si es mayor a 0 significa que no se elimino asi que devolvemos dicho registro
    //                        else return Conflict(new HorarioDeportes(respuesta.Rows[0]));
    //                    }
    //                    else return Ok("Publicacion Eliminada");

    //                }

    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR ELIMINANDO PUBLCIACION");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite eliminar vinculacion entre publicacion y documentos
    //    /// </summary>
    //    /// <param name="id_vinculacion">Es el id de la vinculacion (Se puede recuperar en: Prensa/ListarDocumentoXPublicacion/{id_publicacion})</param>
    //    /// <returns>Un mensaje de OK o el horario que no se pudo eliminar</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Prensa/EliminarVinculacionDocumentacion/{id_vinculacion}
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///         "Vinculacion Eliminada"
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve OK y un mensaje </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT</response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y devuelve el horario </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>        
    //    [HttpDelete("{id_vinculacion}")]
    //    [ActionName("EliminarVinculacionDocumentacion")]
    //    [ProducesResponseType(StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult> EliminarVinculacionDocumentacion(int id_vinculacion)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(150))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {
    //                    Dictionary<string, string> parametros = new() {
    //                       { "@id_docXPublic",id_vinculacion.ToString() }
    //                    };
    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_PRENSA_Eliminar_Vinculacion_Doc_Publi", parametros);

    //                    if (respuesta.Rows.Count > 0)
    //                    {
    //                        if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                        //Si es mayor a 0 significa que no se elimino asi que devolvemos dicho registro
    //                        else return Conflict(new VinculoDocPublic(respuesta.Rows[0]));
    //                    }
    //                    else return Ok("Vinculacion Eliminada");

    //                }

    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR ELIMINANDO VINCULACION DOCUMENTO PUBLICACION");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite eliminar documentos de prensa
    //    /// </summary>
    //    /// <param name="id_documento">Es el id del documento a eliminar</param>
    //    /// <returns>Un mensaje de OK o el documento que no se pudo eliminar porque esta vinculado</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization y que el documento no este vinculado a ninguna publicacion
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Prensa/EliminarDocumentoPrensa/{id_documento}
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///         "Documento Eliminado"
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve OK y un mensaje </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT</response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y devuelve el horario </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>        
    //    [HttpDelete("{id_documento}")]
    //    [ActionName("EliminarDocumentoPrensa")]
    //    [ProducesResponseType(StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult> EliminarDocumentoPrensa(int id_documento)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(150))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {
    //                    Dictionary<string, string> parametros = new() {
    //                       { "@id_doc",id_documento.ToString() }
    //                    };
    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_PRENSA_Eliminar_Documento_Prensa", parametros);

    //                    if (respuesta.Rows.Count > 0)
    //                    {
    //                        if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                        //Si es mayor a 0 significa que no se elimino asi que devolvemos dicho registro
    //                        else return Conflict(new DocumentosPrensa(respuesta.Rows[0]));
    //                    }
    //                    else return Ok("Documento Eliminado");

    //                }

    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR ELIMINANDO DOCUMENTO");
    //            return BadRequest();
    //        }
    //    }
    //    private async Task<bool> TienePermiso(int id_funcion)
    //    {
    //        string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //        if (userData == null || userData == "NO DATA") return false;

    //        int id_perfil;
    //        try { id_perfil = int.Parse(userData.Split(',')[1]); }
    //        catch (Exception) { return false; }

    //        PerfilesController p = new();
    //        return await p.TienePermiso(_config, id_perfil, id_funcion);
    //    }
    //}
}
