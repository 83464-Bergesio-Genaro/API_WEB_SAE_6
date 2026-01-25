using API_WEB_SAE_6.Adapters;
using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
    public class JPAController : Controller
    {
        /// <summary>
        /// Es el adaptador de usuarios para consultar los permisos
        /// </summary>
        private UsuarioAdapter UserAdapter = new();
        /// <summary>
        /// Es el adaptador con respecto a la base de datos para realizar llamadas
        /// </summary>
        private JPAAdapter JpaAdapter = new();
        /// <summary>
        /// 
        /// </summary>
        private readonly string ControllerName = "JPAController";
        /// <summary>
        /// 
        /// </summary>
        public JPAController(){}
        /// <summary>
        /// Recupera todos los eventos abiertos al publico
        /// </summary>
        /// <returns>Un listado de eventos abiertos al publico</returns>
        /// <remarks>
        /// NOTA: Este endpoint es para libre consumo sin importar el usuario
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/JPA/ObtenerEventosPublicos/
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "id": 0,
        ///         "fecha_evento": "2024-07-08T21:04:26.520Z",
        ///         "horario_inicio": "string",
        ///         "horario_fin": "string",
        ///         "encargado": "string",
        ///         "nombre_evento": "string",
        ///         "informacion_interna": false
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado completo de eventos abiertos </response>
        /// <response code="204" >No se encontro ningun evento </response>
        /// <response code="400" >Ocurre un error en la consulta </response>    
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerEventosPublicos")]
        [ProducesResponseType(typeof(IEnumerable<EventosSAE>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<EventosSAE>> ObtenerEventosPublicos()
        {
            string method = "ObtenerEventosPublicos";
            try
            {
                List<EventosSAE>? listadoEventosCompleto = JpaAdapter.ObtenerEventosPublicos();

                if (listadoEventosCompleto == null) return Conflict();
                if (listadoEventosCompleto.Count == 0) return NoContent();

                return Ok(listadoEventosCompleto);
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, ControllerName);
                return BadRequest();
            }
        }
        /// <summary>
        /// Recupera todos los eventos internos de la SAE
        /// </summary>
        /// <returns>Un listado con eventos internos</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/JPA/ObtenerEventosSAE/
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "id": 0,
        ///         "fecha_evento": "2024-07-08T21:04:26.520Z",
        ///         "horario_inicio": "string",
        ///         "horario_fin": "string",
        ///         "encargado": "string",
        ///         "nombre_evento": "string",
        ///         "informacion_interna": false
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado completo de eventos internos </response>
        /// <response code="204" >No se encontro ningun eventos </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerEventosSAE")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<EventosSAE>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<EventosSAE>> ObtenerEventosSAE()
        {
            string method = "ObtenerEventosSAE";
            try
            {
                //El numero de funcion es: 105
                if (TienePermiso(105))
                {

                    try
                    {
                        List<EventosSAE>? listadoEventosCompleto = JpaAdapter.ObtenerEventosSAE();

                        if (listadoEventosCompleto == null) return Conflict();
                        if (listadoEventosCompleto.Count == 0) return NoContent();

                        return Ok(listadoEventosCompleto);
                    }
                    catch (Exception ex)
                    {
                        Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, ControllerName);
                        return BadRequest();
                    }
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, ControllerName);
                return BadRequest();
            }
        }
        /// <summary>
        /// Recupera un evento por ID
        /// </summary>
        /// <param name="id">Es el identificacor del evento en la BD</param>
        /// <returns>Un evento que coincida con ese ID</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/JPA/ObtenerEventosXId/{id}
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "fecha_evento": "YYYY-MM-DDT00:00:00",
        ///       "horario_inicio": "HH:mm:SS",
        ///       "horario_fin": "HH:mm:SS",
        ///       "encargado": "string",
        ///       "nombre_evento": "string"
        ///       "informacion_interna": true
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un evento con este ID </response>
        /// <response code="204" >No se encontro ningun evento </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{id}")]
        [ActionName("ObtenerEventosXId")]
        [Authorize]
        [ProducesResponseType(typeof(EventosSAE), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EventosSAE> ObtenerEventosXId(int id)
        {
            string method = "ObtenerEventosXId";
            try
            {
                if (TienePermiso(105))
                {
                    EventosSAE? evento = JpaAdapter.ObtenerEventoXId(id);
                    if (evento == null) return Conflict();
                    if (evento.id != -1) return NoContent();
                    else return Ok(evento);
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, ControllerName);
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite la modificacion de un evento
        /// </summary>
        /// <param name="id"> El ID del evento a modificar</param>
        /// <param name="eventoSAE"> Los datos modificados del evento</param>
        /// <returns>Un evento modificado en BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     PUT /api/JPA/ModificarEvento/{id}
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "fecha_evento": "YYYY-MM-DDT00:00:00",
        ///       "horario_inicio": "HH:mm:SS",
        ///       "horario_fin": "HH:mm:SS",
        ///       "encargado": "string",
        ///       "nombre_evento": "string"
        ///       "informacion_interna": true
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "fecha_evento": "YYYY-MM-DDT00:00:00",
        ///       "horario_inicio": "HH:mm:SS",
        ///       "horario_fin": "HH:mm:SS",
        ///       "encargado": "string",
        ///       "nombre_evento": "string"
        ///       "informacion_interna": true
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve el evento modificado en BD </response>
        /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del evento a modificar </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{id}")]
        [ActionName("ModificarEvento")]
        [Authorize]
        [ProducesResponseType(typeof(EventosSAE), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EventosSAE> ModificarEvento(int id, [FromBody, Required] EventosSAE eventoSAE)
        {
            string method = "ModificarEvento";
            try
            {
                if (id != eventoSAE.id) return BadRequest();
                //El numero de funcion es: 104
                if (TienePermiso(104))
                {
                    eventoSAE = JpaAdapter.ModificarEventoSae(eventoSAE);

                    if (eventoSAE.id != -1) return Ok(eventoSAE);
                    else return Conflict();
                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, ControllerName);
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite crear eventos
        /// </summary>
        /// <param name="eventoSAE">El eventos que deseamos crear, se envia en el Body</param>
        /// <returns>Un eventos creado en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/JPA/CrearEvento/
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "fecha_evento": "YYYY-MM-DDT00:00:00",
        ///       "horario_inicio": "HH:mm:SS",
        ///       "horario_fin": "HH:mm:SS",
        ///       "encargado": "string",
        ///       "nombre_evento": "string"
        ///       "informacion_interna": true
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "fecha_evento": "YYYY-MM-DDT00:00:00",
        ///       "horario_inicio": "HH:mm:SS",
        ///       "horario_fin": "HH:mm:SS",
        ///       "encargado": "string",
        ///       "nombre_evento": "string"
        ///       "informacion_interna": true
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
        [ActionName("CrearEvento")]
        [Authorize]
        [ProducesResponseType(typeof(EventosSAE), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EventosSAE> CrearEvento([FromBody] EventosSAE eventoSAE)
        {
            string method = "CrearEvento";
            try
            {
                if (TienePermiso(102))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        //string usuarioActual = userData.Split(',')[2];
                        Dictionary<string, string> parametros = new() {
                                { "@fecha", eventoSAE.fecha_evento.ToShortDateString() },
                                { "@hora_ini",eventoSAE.horario_inicio},
                                { "@hora_fin",eventoSAE.horario_fin},
                                { "@encargado", eventoSAE.encargado },
                                { "@nombre_evento",eventoSAE.nombre_evento},
                                { "@informacion_interna",eventoSAE.informacion_interna.ToString()}
                            };

                        eventoSAE = JpaAdapter.CrearEventoSae(eventoSAE);
                        if (eventoSAE.id != -1) return Created("Evento Creado", eventoSAE);
                        else return Conflict();
                    }

                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, ControllerName);
                return BadRequest();
            }
        }
        /// <summary>
        /// Permite eliminar eventos
        /// </summary>
        /// <param name="id">Es el id del evento a eliminar</param>
        /// <returns>Un mensaje de OK o el evento que no se pudo eliminar</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/JPA/EliminarEvento/{id}
        ///     
        ///     RESPONSE:
        ///     {
        ///         "Evento Eliminado"
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
        [ActionName("EliminarEvento")]
        [Authorize]
        [ProducesResponseType(typeof(string),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> EliminarEvento(int id)
        {
            string method = "EliminarEvento";
            try
            {
                if (TienePermiso(103))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        if (JpaAdapter.EliminarEventoSae(id)) return Ok("Evento Eliminado");
                        else return Conflict();
                    }

                }
                else return Forbid();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, ControllerName);
                return BadRequest();
            }
        }
    /// <summary>
    /// Recupera los stands disponibles
    /// </summary>
    /// <returns>Un listado con stands actuales</returns>
    /// <remarks>
    /// NOTA: Este endpoint es para libre consumo sin importar el usuario
    ///  
    /// Ejemplo de uso:
    /// 
    ///     GET /api/JPA/ObtenerStands/
    ///     
    ///     RESPONSE:
    ///       [
    ///         {
    ///           "id": 0,
    ///           "nombre_stand": "string",
    ///           "expositor": "string",
    ///           "ubicacion": "string",
    ///           "horario_inicio": "string",
    ///           "horario_fin": "string"
    ///         }
    ///       ]
    ///     
    /// </remarks>
    /// <response code="200" >Devuelve un listado de stands actuales </response>
    /// <response code="204" >No se encontro ningun stand </response>
    /// <response code="400" >Ocurre un error en la consulta </response>
    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    [HttpGet]
    [ActionName("ObtenerStands")]
    [ProducesResponseType(typeof(IEnumerable<StandJPA>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<StandJPA>>> ObtenerStands()
    {
        try
        {

            DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_JPA_Listar_Stand");
            if (respuesta.Rows.Count == 0) return NoContent();
            if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

            List<StandJPA> listadoEventosCompleto = new();
            foreach (DataRow row in respuesta.Rows)
            {
                StandJPA eventos = new(row);
                listadoEventosCompleto.Add(eventos);
            }
            return Ok(listadoEventosCompleto);

        }
        catch (Exception ex)
        {
            _logger.RegistrarERROR(ex, "ERROR LISTANDO STANDS PUBLICOS");
            return BadRequest();
        }
    }
    /// <summary>
    /// Permite la modificacion de un stand
    /// </summary>
    /// <param name="id"> El ID del stand a modificar</param>
    /// <param name="stand"> Los datos modificados del stand</param>
    /// <returns>Un stand modificado en BD</returns>
    /// <remarks>
    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    ///  
    /// Ejemplo de uso:
    /// 
    ///     PUT /api/JPA/ModificarStand/{id}
    ///     BODY:
    ///     {
    ///       "id": 0,
    ///       "nombre_stand": "string",
    ///       "expositor": "string",
    ///       "ubicacion": "string",
    ///       "horario_inicio": "string",
    ///       "horario_fin": "string"
    ///     }
    ///     
    ///     RESPONSE:
    ///     {
    ///       "id": 0,
    ///       "nombre_stand": "string",
    ///       "expositor": "string",
    ///       "ubicacion": "string",
    ///       "horario_inicio": "string",
    ///       "horario_fin": "string"
    ///     }
    ///     
    /// </remarks>
    /// <response code="200" >Devuelve el stand modificado en BD </response>
    /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del stand a modificar </response>
    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    [HttpPut("{id}")]
    [ActionName("ModificarStand")]
    [Authorize]
    [ProducesResponseType(typeof(StandJPA), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<StandJPA>> ModificarStand(int id, [FromBody, Required] StandJPA stand)
    {
        try
        {
            if (id != stand.id) return BadRequest();
            //El numero de funcion es: 109
            if (await TienePermiso(109))
            {
                Dictionary<string, string> parametros = new() {
                        { "@id_stand",stand.id.ToString() },
                        { "@nombre_stand",stand.nombre_stand },
                        { "@hora_ini", stand.horario_inicio },
                        { "@hora_fin", stand.horario_fin },
                        { "@ubicacion", stand.ubicacion },
                        { "@expositor", stand.expositor }
                    };
                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_JPA_Modificar_Stand", parametros);

                //En este caso sino modifica nada es un conflicto en la BD
                if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                return Ok(new StandJPA(respuesta.Rows[0]));
            }
            else return Forbid();
        }
        catch (Exception ex)
        {
            _logger.RegistrarERROR(ex, "ERROR MODIFICANDO STAND");
            return BadRequest();
        }
    }
    /// <summary>
    /// Permite crear stand
    /// </summary>
    /// <param name="stand">El stand que deseamos crear, se envia en el Body</param>
    /// <returns>Un stand creado en la base de datos o error</returns>
    /// <remarks>
    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    ///  
    /// Ejemplo de uso:
    /// 
    ///     POST /api/JPA/CrearStand/
    ///     BODY:
    ///     {
    ///       "id": 0,
    ///       "nombre_stand": "string",
    ///       "expositor": "string",
    ///       "ubicacion": "string",
    ///       "horario_inicio": "string",
    ///       "horario_fin": "string"
    ///     }
    ///     
    ///     RESPONSE:
    ///     {
    ///       "id": 0,
    ///       "nombre_stand": "string",
    ///       "expositor": "string",
    ///       "ubicacion": "string",
    ///       "horario_inicio": "string",
    ///       "horario_fin": "string"
    ///     }
    ///     
    /// </remarks>
    /// <response code="201" >Devuelve el stand creado en la BD </response>
    /// <response code="400" >Ocurre un error en la consulta </response>
    /// <response code="401" >El usuario no genero su JWT</response>
    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    [HttpPost]
    [ActionName("CrearStand")]
    [Authorize]
    [ProducesResponseType(typeof(StandJPA), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<StandJPA>> CrearStand(StandJPA stand)
    {
        try
        {
            if (await TienePermiso(107))
            {
                Dictionary<string, string> parametros = new() {
                            { "@nombre_stand",stand.nombre_stand },
                            { "@hora_ini", stand.horario_inicio },
                            { "@hora_fin", stand.horario_fin },
                            { "@ubicacion", stand.ubicacion },
                            { "@expositor", stand.expositor }
                        };
                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_JPA_Crear_Stand", parametros);
                //En este caso sino crea es un error en la BD
                if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                return Created("Stand Creado", new StandJPA(respuesta.Rows[0]));
            }
            else return Forbid();
        }
        catch (Exception ex)
        {
            _logger.RegistrarERROR(ex, "ERROR CREANDO STAND");
            return BadRequest();
        }
    }
    /// <summary>
    /// Permite eliminar stands
    /// </summary>
    /// <param name="id">Es el id del stand a eliminar</param>
    /// <returns>Un mensaje de OK o el stand que no se pudo eliminar</returns>
    /// <remarks>
    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    ///  
    /// Ejemplo de uso:
    /// 
    ///     POST /api/JPA/EliminarStand/{id}
    ///     
    ///     RESPONSE:
    ///     {
    ///         "Stand Eliminado"
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
    [ActionName("EliminarStand")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> EliminarStand(int id)
    {
        try
        {
            if (await TienePermiso(108))
            {
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                if (userData == null || userData == "NO DATA") return Unauthorized();
                else
                {
                    Dictionary<string, string> parametros = new() {
                           { "@id_stand",id.ToString() }
                        };
                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_JPA_Eliminar_Stand", parametros);

                    if (respuesta.Rows.Count > 0)
                    {
                        if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                        //Si es mayor a 0 significa que no se elimino asi que devolvemos dicho registro
                        else return Conflict(new HorariosSAE(respuesta.Rows[0]));
                    }
                    else return Ok("Stand Eliminado");

                }

            }
            else return Forbid();
        }
        catch (Exception ex)
        {
            _logger.RegistrarERROR(ex, "ERROR ELIMINANDO STAND");
            return BadRequest();
        }
    }
    /// <summary>
    /// Recupera todos los interesados al JPA
    /// </summary>
    /// <returns>Un listado con interesados</returns>
    /// <remarks>
    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    ///  
    /// Ejemplo de uso:
    /// 
    ///     GET /api/JPA/ObtenerInteresadosEventos/
    ///     
    ///     RESPONSE:
    ///     [
    ///       {
    ///         "id": 0,
    ///         "nombre_interesado": "string",
    ///         "contacto": "string",
    ///         "email": "string"
    ///       }
    ///     ]
    ///     
    /// </remarks>
    /// <response code="200" >Devuelve un listado de interesados</response>
    /// <response code="204" >No se encontro ningun interesado </response>
    /// <response code="400" >Ocurre un error en la consulta </response>
    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    [HttpGet]
    [ActionName("ObtenerInteresadosEventos")]
    [Authorize]
    [ProducesResponseType(typeof(IEnumerable<InteresadosSAE>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<InteresadosSAE>>> ObtenerInteresadosEventos()
    {
        try
        {
            if (await TienePermiso(106))
            {
                DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_JPA_Listar_Interesados");
                if (respuesta.Rows.Count == 0) return NoContent();
                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

                List<InteresadosSAE> listadoInteresados = new();
                foreach (DataRow row in respuesta.Rows)
                {
                    InteresadosSAE eventos = new(row);
                    listadoInteresados.Add(eventos);
                }
                return Ok(listadoInteresados);
            }
            else return Forbid();

        }
        catch (Exception ex)
        {
            _logger.RegistrarERROR(ex, "ERROR LISTANDO INTERESADOS");
            return BadRequest();
        }
    }
    /// <summary>
    /// Permite la modificacion de un interesado
    /// </summary>
    /// <param name="id"> El ID del interesado a modificar</param>
    /// <param name="interesado"> Los datos modificados del interesado</param>
    /// <returns>Un interesado modificado en BD</returns>
    /// <remarks>
    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    ///  
    /// Ejemplo de uso:
    /// 
    ///     PUT /api/JPA/ModificarInteresado/{id}
    ///     BODY:
    ///     {
    ///       "id": 0,
    ///       "nombre_interesado": "string",
    ///       "contacto": "string",
    ///       "email": "string"
    ///     }
    ///     
    ///     RESPONSE:
    ///     {
    ///       "id": 0,
    ///       "nombre_interesado": "string",
    ///       "contacto": "string",
    ///       "email": "string"
    ///     }
    ///     
    /// </remarks>
    /// <response code="200" >Devuelve el interesado modificado en BD </response>
    /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del interesado a modificar </response>
    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    [HttpPut("{id}")]
    [ActionName("ModificarInteresado")]
    [Authorize]
    [ProducesResponseType(typeof(StandJPA), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<StandJPA>> ModificarInteresado(int id, [FromBody, Required] InteresadosSAE interesado)
    {
        try
        {
            if (id != interesado.id) return BadRequest();
            //El numero de funcion es: 109
            if (await TienePermiso(113))
            {
                Dictionary<string, string> parametros = new() {
                        { "@id_interesado",interesado.id.ToString() },
                        { "@nombre",interesado.nombre_interesado },
                        { "@contacto", interesado.contacto },
                        { "@email", interesado.email }
                    };
                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_JPA_Modificar_Interesados", parametros);

                //En este caso sino modifica nada es un conflicto en la BD
                if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                return Ok(new InteresadosSAE(respuesta.Rows[0]));
            }
            else return Forbid();
        }
        catch (Exception ex)
        {
            _logger.RegistrarERROR(ex, "ERROR MODIFICANDO INTERESADO");
            return BadRequest();
        }
    }
    /// <summary>
    /// Permite crear interesados
    /// </summary>
    /// <param name="interesado">El interesado que deseamos crear, se envia en el Body</param>
    /// <returns>Un interesado creado en la base de datos o error</returns>
    /// <remarks>
    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    ///  
    /// Ejemplo de uso:
    /// 
    ///     POST /api/JPA/CrearInteresados/
    ///     BODY:
    ///     {
    ///       "id": 0,
    ///       "nombre_interesado": "string",
    ///       "contacto": "string",
    ///       "email": "string"
    ///     }
    ///     
    ///     RESPONSE:
    ///     {
    ///       "id": 0,
    ///       "nombre_interesado": "string",
    ///       "contacto": "string",
    ///       "email": "string"
    ///     }
    ///     
    /// </remarks>
    /// <response code="201" >Devuelve el interesado creado en la BD </response>
    /// <response code="400" >Ocurre un error en la consulta </response>
    /// <response code="401" >El empleado no genero su JWT</response>
    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    [HttpPost]
    [ActionName("CrearInteresados")]
    [Authorize]
    [ProducesResponseType(typeof(InteresadosSAE), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<InteresadosSAE>> CrearInteresados(InteresadosSAE interesado)
    {
        try
        {
            if (await TienePermiso(107))
            {
                Dictionary<string, string> parametros = new() {
                            { "@nombre",interesado.nombre_interesado },
                            { "@contacto", interesado.contacto },
                            { "@email", interesado.email }
                        };
                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_JPA_Crear_Interesado", parametros);
                //En este caso sino crea es un error en la BD
                if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                return Created("Interesado Creado", new InteresadosSAE(respuesta.Rows[0]));
            }
            else return Forbid();
        }
        catch (Exception ex)
        {
            _logger.RegistrarERROR(ex, "ERROR CREANDO INTERESADO");
            return BadRequest();
        }
    }
    /// <summary>
    /// Permite eliminar interesados
    /// </summary>
    /// <param name="id">Es el id del interesado a eliminar</param>
    /// <returns>Un mensaje de OK o el interesado que no se pudo eliminar</returns>
    /// <remarks>
    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    ///  
    /// Ejemplo de uso:
    /// 
    ///     POST /api/JPA/EliminarStand/{id}
    ///     
    ///     RESPONSE:
    ///     {
    ///         "Interesado Eliminado"
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
    [ActionName("EliminarInteresado")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> EliminarInteresado(int id)
    {
        try
        {
            if (await TienePermiso(112))
            {
                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                if (userData == null || userData == "NO DATA") return Unauthorized();
                else
                {
                    Dictionary<string, string> parametros = new() {
                           { "@id_interesado",id.ToString() }
                        };
                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_JPA_Eliminar_Interesado", parametros);

                    if (respuesta.Rows.Count > 0)
                    {
                        if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
                        //Si es mayor a 0 significa que no se elimino asi que devolvemos dicho registro
                        else return Conflict(new InteresadosSAE(respuesta.Rows[0]));
                    }
                    else return Ok("Interesado Eliminado");

                }

            }
            else return Forbid();
        }
        catch (Exception ex)
        {
            _logger.RegistrarERROR(ex, "ERROR ELIMINANDO INTERESADO");
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
