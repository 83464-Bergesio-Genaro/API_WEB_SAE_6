using API_WEB_SAE_6.Adapters;
using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models.Compra;
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
    public class CompraController : Controller
    {
        /// <summary>
        /// Es el adaptador de usuarios para consultar los permisos
        /// </summary>
        private UsuarioAdapter UserAdapter = new();
        /// <summary>
        /// Es el adaptador con respecto a la base de datos para realizar llamadas
        /// </summary>
        private CompraAdapter PurchaseAdapter = new();
        /// <summary>
        /// 
        /// </summary>
        private readonly string ControllerName = "CompraController";
        /// <summary>
        /// 
        /// </summary>
        public CompraController() { }
        /// <summary>
        /// Busca las compras por fecha
        /// </summary>
        /// <returns>Un listado de compras</returns>
        /// <param name="fecha_inicio"></param>
        /// <param name="fecha_fin"></param>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Compra/ObtenerComprasXFecha/{fecha_inicio}/{fecha_fin}
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///           "id": 0,
        ///           "nombre": "string",
        ///         },
        ///     ]
        /// </remarks>
        /// <response code="200" >Devuelve un listado de compras </response>
        /// <response code="204" >No se encontro ninguna compra </response>
        /// <response code="400" >Ocurre un error en la consulta </response>    
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{fecha_inicio}/{fecha_fin}")]
        [ActionName("ObtenerComprasXFecha")]
        [ProducesResponseType(typeof(IEnumerable<Compras>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Compras>> ObtenerViajesXLegajo([FromRoute] DateOnly fecha_inicio, DateOnly fecha_fin)
        {
            try
            {
                if (TienePermiso(157))
                {
                    List<Compras>? listadoEventosCompleto = PurchaseAdapter.ObtenerComprasXFecha(fecha_inicio, fecha_fin);

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
        /// Permite cargar compras
        /// </summary>
        /// <param name="compraSae">La compra que deseamos crear, se envia en el Body</param>
        /// <returns>Una compra creado en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Compra/CrearCompra/
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "nombre": "string",
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve la compra creada en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost]
        [ActionName("CrearCompra")]
        [Authorize]
        [ProducesResponseType(typeof(Compras), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Compras> CrearCompra([FromBody] Compras compraSae)
        {
            try
            {
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                if (userData != null &&
                   userData.Length > 0 &&
                   userData != "NO DATA" &&
                   int.TryParse(userData.Split(',')[2], out int idUserCrea) &&
                   TienePermiso(155))
                {
                    compraSae = PurchaseAdapter.CrearCompra(compraSae);
                    if (compraSae.id != -1) return Created("Compra Creada", compraSae);
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
        /// Permite eliminar compras
        /// </summary>
        /// <param name="id">Es el id de la compra a eliminar</param>
        /// <returns>Un mensaje de OK o la compra no se puede eliminar porque tiene un informe cargado</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Compra/EliminarCompra/{id}
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
        [ActionName("EliminarCompra")]
        [Authorize]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> EliminarCompra(int id)
        {
            try
            {
                if (TienePermiso(156))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        if (PurchaseAdapter.EliminarCompra(id)) return Ok("Compra Eliminada");
                        else return Conflict("Hay datos asociados a esta compra");
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
        /// Buscar informe por numero expediente
        /// </summary>
        /// <returns>Un listado de viajes que el estudiante haga</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Viaje/ObtenerInformeXId/{nro_expediente}
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "nombre": "string",
        ///     }
        /// </remarks>
        /// <response code="200" >Devuelve un listado de viajes activos </response>
        /// <response code="204" >No se encontro ningun viaje activo </response>
        /// <response code="400" >Ocurre un error en la consulta </response>    
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{nro_expediente}")]
        [ActionName("ObtenerInformeXExpediente")]
        [ProducesResponseType(typeof(InformesTecnicoCompra), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<InformesTecnicoCompra> ObtenerInformeXExpediente([FromRoute] string nro_expediente)
        {
            try
            {
                if (TienePermiso(157))
                {

                    InformesTecnicoCompra? viajeEncontrad = PurchaseAdapter.ObtenerInformeXNro(nro_expediente);
                    if (viajeEncontrad == null) return Conflict();
                    if (viajeEncontrad.nro_expediente == "") return NoContent();

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
        /// Busca el informe de la compra
        /// </summary>
        /// <returns>Un listado de viajes que el estudiante haga</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Viaje/ObtenerInformeXId/{nro_expediente}
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "nombre": "string",
        ///     }
        /// </remarks>
        /// <response code="200" >Devuelve un listado de viajes activos </response>
        /// <response code="204" >No se encontro ningun viaje activo </response>
        /// <response code="400" >Ocurre un error en la consulta </response>    
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{idCompra}")]
        [ActionName("ObtenerInformeXCompra")]
        [ProducesResponseType(typeof(InformesTecnicoCompra), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<InformesTecnicoCompra> ObtenerInformeXCompra([FromRoute] int idCompra)
        {
            try
            {
                if (TienePermiso(157))
                {

                    InformesTecnicoCompra? viajeEncontrad = PurchaseAdapter.ObtenerInformeXCompra(idCompra);
                    if (viajeEncontrad == null) return Conflict();
                    if (viajeEncontrad.nro_expediente == "") return NoContent();

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
        /// Permite la modificacion de un informe
        /// </summary>
        /// <param name="nro_expediente"> El identificador del informe</param>
        /// <param name="informeCompra"> Los datos modificados del informe</param>
        /// <returns>Un informe modificado en BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     PUT /api/Viaje/ModificarInforme/{id}
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "nombre": "string",
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "nombre": "string"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve el informe modificado en BD </response>
        /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del informe a modificar </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{nro_expediente}")]
        [ActionName("ModificarInforme")]
        [Authorize]
        [ProducesResponseType(typeof(InformesTecnicoCompra), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<InformesTecnicoCompra> ModificarInforme(string nro_expediente, [FromBody, Required] InformesTecnicoCompra informeCompra)
        {
            try
            {
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                if (nro_expediente != informeCompra.nro_expediente) return BadRequest();
                //El numero de funcion es: 104
                if (userData != null &&
                   userData.Length > 0 &&
                   userData != "NO DATA" &&
                   int.TryParse(userData.Split(',')[2], out int idUserMod) &&
                   TienePermiso(156))
                {
                    informeCompra = PurchaseAdapter.ModificarInformeCompra(informeCompra, idUserMod);
                    if (informeCompra.nro_expediente != "") return Ok(informeCompra);
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
        /// Permite crear informes
        /// </summary>
        /// <param name="informeTecnico">Un informe que deseamos crear, se envia en el Body</param>
        /// <returns>Un informe creado en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Compra/CrearInforme/
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "nombre": "string",
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "nombre": "string",
        ///       "fecha_inicio": "2026-06-16",
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve el informe creado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost]
        [ActionName("CrearInforme")]
        [Authorize]
        [ProducesResponseType(typeof(InformesTecnicoCompra), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<InformesTecnicoCompra> CrearInforme([FromBody] InformesTecnicoCompra informeTecnico)
        {
            try
            {
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                if (userData != null &&
                   userData.Length > 0 &&
                   userData != "NO DATA" &&
                   int.TryParse(userData.Split(',')[2], out int idUserCrea) &&
                   TienePermiso(155))
                {
                    informeTecnico = PurchaseAdapter.CrearInformeCompra(informeTecnico, idUserCrea);
                    if (informeTecnico.nro_expediente != "") return Created("Informe Creado", informeTecnico);
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
        ///           "id_compra": 0,
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
        [ProducesResponseType(typeof(DocumentosCompra), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DocumentosCompra> DescargarDocumentacionXId(int id)
        {
            try
            {
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";

                if (userData != null && userData != "NO DATA" && TienePermiso(157))
                {
                    DocumentosCompra? doc = PurchaseAdapter.BuscarDocumentoXId(id);

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
        /// Recupera todos los documentos
        /// </summary>
        /// <returns>Un listado de documentos de la compra</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Viaje/ListarDocumentacionXCompra/{idCompra}
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///           "id": 0,
        ///           "id_compra": 0,
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
        [HttpGet("{idCompra}")]
        [ActionName("ListarDocumentacionXCompra")]
        [ProducesResponseType(typeof(IEnumerable<DocumentosCompra>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<DocumentosCompra>> ListarDocumentacionXCompra(int idCompra)
        {
            try
            {
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";

                if (userData != null && userData != "NO DATA" && TienePermiso(157))
                {
                    string legajoRegistrado = userData.Split(',')[0];
                    string idPerfil = userData.Split(',')[1];
                    if (idPerfil == "2" || idPerfil == "5")
                    {
                        List<DocumentosCompra>? documentos = PurchaseAdapter.BuscarDocumentosXCompra(idCompra);

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
        /// Permite crear un nuevo documento
        /// </summary>
        /// <param name="archivo">El archivo que deseamos almacenar en la BD</param>
        /// <param name="idTipoDocumento">El tipo de documento que subimos</param>
        /// <param name="idCompra">El viaje que queremos asociar a este documento</param>
        /// <returns>Una inscripcion creada en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Viaje/CrearDocumentoCompra/{idCompra}/{idTipoDocumento}
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
        [HttpPost("{idCompra}/{idTipoDocumento}")]
        [ActionName("CrearDocumentoCompra")]
        [Consumes("multipart/form-data")]
        [Authorize]
        [ProducesResponseType(typeof(DocumentosCompra), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status413PayloadTooLarge)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DocumentosCompra>> CrearDocumentoCompra(int idCompra, int idTipoDocumento, IFormFile archivo)
        {
            try
            {
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                if (userData != null &&
                   userData.Length > 0 &&
                   userData != "NO DATA" &&
                   int.TryParse(userData.Split(',')[2], out int idUserMod) &&
                   TienePermiso(155))
                {
                    if (archivo.Length < 50000000)
                    {
                        string usuarioActual = userData.Split(',')[2];
                        SettingsReader set = SettingsReader.GetAppSettings();
                        string uploadsPath = set.GetFilesLocation();
                        if (uploadsPath != "ERROR")
                        {
                            string today = DateTime.Now.ToString("yyyy-MM-dd");
                            //Ruta absoluta
                            uploadsPath = Path.Combine(uploadsPath, "Compras", today);

                            // crear carpeta si no existe
                            if (!Directory.Exists(uploadsPath))
                                Directory.CreateDirectory(uploadsPath);

                            // nombre único
                            var fileName = $"{Guid.NewGuid()}_{archivo.FileName}";
                            var filePath = Path.Combine(uploadsPath, fileName);

                            // guardar archivo
                            using var stream = new FileStream(filePath, FileMode.Create);
                            await archivo.CopyToAsync(stream);

                            DocumentosCompra doc = new()
                            {
                                id = -1,
                                id_tipo_documento = idTipoDocumento,//Tengo que ver que hago con esto
                                nombre_documento = archivo.FileName,
                                id_compra = idCompra,
                                ruta = Path.Combine("Compras", today, fileName),//Es una ruta relativa desde el origen del sistema de archivos
                                tamanio = archivo.Length
                            };

                            doc = PurchaseAdapter.CrearDocumento(doc, idUserMod);

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
        [ActionName("EliminarDocumentoCompra")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> EliminarDocumentoCompra(int id_documento)
        {
            try
            {
                if (TienePermiso(156))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        DocumentosCompra? doc = PurchaseAdapter.BuscarDocumentoXId(id_documento);
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
                                        if (PurchaseAdapter.EliminarDocumento(id_documento)) return Ok("Documento Eliminado");
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
