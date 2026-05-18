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
    /// Es el controlador para todo relacionado al area de salud/bienestar
    /// </summary>
    [EnableCors("CorsRules")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SaludController : Controller
    {
        /// <summary>
        /// Es el adaptador de usuarios para consultar los permisos
        /// </summary>
        private UsuarioAdapter UserAdapter = new();
        /// <summary>
        /// Es el adaptador con respecto a la base de datos para realizar llamadas
        /// </summary>
        private SaludAdapter HealthAdapter = new();
        /// <summary>
        /// 
        /// </summary>
        private readonly string ControllerName = "SaludController";
        /// <summary>
        /// 
        /// </summary>
        public SaludController() { }
        /// <summary>
        /// Recupera todos los estados medicos
        /// </summary>
        /// <returns> Un listado de todos los estados posibles del turno</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Salud/ObtenerEstadosTurno/
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///             "id": 0,
        ///             "nombre": "string"
        ///         }  
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado completo de estados </response>
        /// <response code="204" >No se encontro ningun estado </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>   
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerEstadosTurno")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<EstadosTurno>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<EstadosTurno>> ObtenerEstadosTurno()
        {
            try
            {
                //El numero de funcion es: 69
                if (TienePermiso(69))
                {
                    List<EstadosTurno>? estadosTurno = HealthAdapter.ObtenerEstadoTurno();

                    if (estadosTurno == null) return Conflict();
                    if (estadosTurno.Count == 0) return NoContent();

                    return Ok(estadosTurno);
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
        /// Recupera todos las especialidades medicas del area
        /// </summary>
        /// <returns> Un listado de todas las especialidades</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Salud/ObtenerEspecialidadesCompleto/
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///             "id": 0,
        ///             "nombre": "string",
        ///             "descripcion": "string",
        ///             "activo": true
        ///         }  
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado completo de especialidades </response>
        /// <response code="204" >No se encontro ninguna especialidad </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>   
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerEspecialidadesCompleto")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<Especialidad>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Especialidad>> ObtenerEspecialidadesCompleto()
        {
            try
            {
                //El numero de funcion es: 69
                if (TienePermiso(69))
                {
                    List<Especialidad>? especialidadesMedicas = HealthAdapter.ObtenerEspecialidadesCompleto();

                    if (especialidadesMedicas == null) return Conflict();
                    if (especialidadesMedicas.Count == 0) return NoContent();

                    return Ok(especialidadesMedicas);
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
        /// Recupera todos las especialidades medicas del area
        /// </summary>
        /// <returns> Un listado de todas las especialidades</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Salud/ObtenerEspecialidadesCompleto/
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///             "id": 0,
        ///             "nombre": "string",
        ///             "descripcion": "string",
        ///             "activo": true
        ///         }  
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado completo de especialidades </response>
        /// <response code="204" >No se encontro ninguna especialidad </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>   
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerEspecialidadesActivas")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<Especialidad>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Especialidad>> ObtenerEspecialidadesActivas()
        {
            try
            {
                //El numero de funcion es: 69
                if (TienePermiso(69))
                {
                    List<Especialidad>? especialidadesMedicas = HealthAdapter.ObtenerEspecialidadesActivas();

                    if (especialidadesMedicas == null) return Conflict();
                    if (especialidadesMedicas.Count == 0) return NoContent();

                    return Ok(especialidadesMedicas);
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
        /// Permite la modificacion de una especialidad medica
        /// </summary>
        /// <param name="id_especialidad"> El ID de la especialdiad a modificar</param>
        /// <param name="especialidad"> Los datos modificados de la especialidad</param>
        /// <returns>Una especialidad modificada en BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     PUT /api/Salud/ModificarEspecialista/{id_especialidad}
        ///     BODY:
        ///     {
        ///         "id": 0,
        ///         "nombre": "string",
        ///         "descripcion": "string",
        ///         "activo": true
        ///     }  
        /// 
        ///     RESPONSE:
        ///     {
        ///         "id": 0,
        ///         "nombre": "string",
        ///         "descripcion": "string",
        ///         "activo": true
        ///     }  
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve la especialidad modificada en BD </response>
        /// <response code="400" >Ocurre un error en la consulta o el CUIL es diferente que el del especialista a modificar </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{id_especialidad}")]
        [ActionName("ModificarEspecialidad")]
        [Authorize]
        [ProducesResponseType(typeof(EspecialistaMedico), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EspecialistaMedico> ModificarEspecialidad(int id_especialidad, [FromBody, Required] Especialidad especialidad)
        {
            try
            {
                if (id_especialidad != especialidad.id) return BadRequest();
                //El numero de funcion es: 68
                if (TienePermiso(68))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData != null &&
                       userData.Length > 0 &&
                       userData != "NO DATA" &&
                       int.TryParse(userData.Split(',')[2], out int idUserMod))
                    {
                        especialidad = HealthAdapter.ModificarEspecialidadMed(especialidad);
                        if (especialidad.id != -1) return Ok(especialidad);
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
        /// Permite crear un especialista
        /// </summary>
        /// <param name="espe">El especialista que deseamos crear, se envia en el Body</param>
        /// <returns>Un especialista creado en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Salud/CrearEspecialista/
        ///     BODY:
        ///     {
        ///        "cuil": "string",
        ///        "apellido": "string",
        ///        "nombre": "string",
        ///        "presta_servicio": true,
        ///        "especialidad": {
        ///             "id": 0,
        ///             "nombre": "string",
        ///             "descripcion": "string",
        ///             "activo": true
        ///         }  
        ///     }
        ///     (No se crean los datos de la especialidad sino que le asignamos el id_especialidad) 
        ///     
        ///     RESPONSE:
        ///     {
        ///        "cuil": "string",
        ///        "apellido": "string",
        ///        "nombre": "string",
        ///        "presta_servicio": true,
        ///        "especialidad": {
        ///             "id": 0,
        ///             "nombre": "string",
        ///             "descripcion": "string",
        ///             "activo": true
        ///         }  
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve el especialista creado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El especialista no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost]
        [ActionName("CrearEspecialidad")]
        [Authorize]
        [ProducesResponseType(typeof(EspecialistaMedico), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EspecialistaMedico> CrearEspecialidad([FromBody] Especialidad espe)
        {
            try
            {
                if (TienePermiso(67))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData != null &&
                       userData.Length > 0 &&
                       userData != "NO DATA" &&
                       int.TryParse(userData.Split(',')[2], out int idUserCrea))
                    {
                        espe = HealthAdapter.CrearEspecialidad(espe);
                        if (espe.id != -1) return Created("Especialidad Creada", espe);
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
        /// Recupera todos los especialistas medicos del area
        /// </summary>
        /// <returns> Un listado de todos los empleados del area medica</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Salud/ObtenerListadoEspecialistas/
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///            "cuil": "string",
        ///            "apellido": "string",
        ///            "nombre": "string",
        ///            "presta_servicio": true,
        ///            "especialidad": {
        ///                 "id": 0,
        ///                 "nombre": "string",
        ///                 "descripcion": "string",
        ///                 "activo": true
        ///             }  
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado completo de especialistas </response>
        /// <response code="204" >No se encontro ningun empleado medico </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>   
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerListadoEspecialistas")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<EspecialistaMedico>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<EspecialistaMedico>> ObtenerListadoEspecialistas()
        {
            try
            {
                //El numero de funcion es: 69
                if (TienePermiso(69))
                {
                    List<EspecialistaMedico>? listadoDeportes = HealthAdapter.ObtenerEspecialistasCompleto();

                    if (listadoDeportes == null) return Conflict();
                    if (listadoDeportes.Count == 0) return NoContent();

                    return Ok(listadoDeportes);
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
        /// Busca especialistas por CUIL
        /// </summary>
        /// <param name="cuil">Es el identificador del especialista en la BD</param>
        /// <returns>Un especialista que coincida con ese CUIL</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Salud/ObtenerEspecialistasXCuil/{cuil}
        ///     
        ///     RESPONSE:
        ///     {
        ///        "cuil": "string",
        ///        "apellido": "string",
        ///        "nombre": "string",
        ///        "presta_servicio": true,
        ///        "especialidad": {
        ///             "id": 0,
        ///             "nombre": "string",
        ///             "descripcion": "string",
        ///             "activo": true
        ///         }  
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un especialista con este CUIL </response>
        /// <response code="204" >No se encontro ningun especialista </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{cuil}")]
        [ActionName("ObtenerEspecialistasXCuil")]
        [Authorize]
        [ProducesResponseType(typeof(EspecialistaMedico), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EspecialistaMedico> ObtenerEspecialistasXCuil(string cuil)
        {
            try
            {
                if (TienePermiso(69))
                {
                    EspecialistaMedico? espe = HealthAdapter.ObtenerEspecialistaXCuil(cuil);
                    if (espe == null) return Conflict();
                    if (espe.cuil == "") return NoContent();
                    else return Ok(espe);
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
        /// Permite la modificacion de un especialista
        /// </summary>
        /// <param name="cuil"> El CUIL del especialista a modificar</param>
        /// <param name="especialista"> Los datos modificados del especialista</param>
        /// <returns>Un especialista modificado en BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     PUT /api/Salud/ModificarEspecialista/{cuil}
        ///     BODY:
        ///     {
        ///        "cuil": "string",
        ///        "apellido": "string",
        ///        "nombre": "string",
        ///        "presta_servicio": true,
        ///        "especialidad": {
        ///             "id": 0,
        ///             "nombre": "string",
        ///             "descripcion": "string",
        ///             "activo": true
        ///         }  
        ///     }
        ///     (No se modifica los datos de la especialidad sino que cambiamos el id_especialidad de especialista)    
        /// 
        ///     RESPONSE:
        ///     {
        ///        "cuil": "string",
        ///        "apellido": "string",
        ///        "nombre": "string",
        ///        "presta_servicio": true,
        ///        "especialidad": {
        ///             "id": 0,
        ///             "nombre": "string",
        ///             "descripcion": "string",
        ///             "activo": true
        ///         }  
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve el especialista modificado en BD </response>
        /// <response code="400" >Ocurre un error en la consulta o el CUIL es diferente que el del especialista a modificar </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{cuil}")]
        [ActionName("ModificarEspecialista")]
        [Authorize]
        [ProducesResponseType(typeof(EspecialistaMedico), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EspecialistaMedico> ModificarEspecialista(string cuil, [FromBody, Required] EspecialistaMedico especialista)
        {
            try
            {
                if (cuil != especialista.cuil) return BadRequest();
                //El numero de funcion es: 68
                if (TienePermiso(68))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData != null &&
                       userData.Length > 0 &&
                       userData != "NO DATA" &&
                       int.TryParse(userData.Split(',')[2], out int idUserMod))
                    {
                        especialista = HealthAdapter.ModificarEspecialistaMed(especialista, idUserMod);
                        if (especialista.cuil != "") return Ok(especialista);
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
        /// Permite crear un especialista
        /// </summary>
        /// <param name="especialista">El especialista que deseamos crear, se envia en el Body</param>
        /// <returns>Un especialista creado en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Salud/CrearEspecialista/
        ///     BODY:
        ///     {
        ///        "cuil": "string",
        ///        "apellido": "string",
        ///        "nombre": "string",
        ///        "presta_servicio": true,
        ///        "especialidad": {
        ///             "id": 0,
        ///             "nombre": "string",
        ///             "descripcion": "string",
        ///             "activo": true
        ///         }  
        ///     }
        ///     (No se crean los datos de la especialidad sino que le asignamos el id_especialidad) 
        ///     
        ///     RESPONSE:
        ///     {
        ///        "cuil": "string",
        ///        "apellido": "string",
        ///        "nombre": "string",
        ///        "presta_servicio": true,
        ///        "especialidad": {
        ///             "id": 0,
        ///             "nombre": "string",
        ///             "descripcion": "string",
        ///             "activo": true
        ///         }  
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve el especialista creado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El especialista no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost]
        [ActionName("CrearEspecialista")]
        [Authorize]
        [ProducesResponseType(typeof(EspecialistaMedico), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EspecialistaMedico> CrearEspecialista([FromBody] EspecialistaMedico especialista)
        {
            try
            {
                if (TienePermiso(67))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData != null &&
                       userData.Length > 0 &&
                       userData != "NO DATA" &&
                       int.TryParse(userData.Split(',')[2], out int idUserCrea))
                    {
                        especialista = HealthAdapter.CrearEspecialista(especialista, idUserCrea);
                        if (especialista.cuil != "") return Created("Especialista Creado", especialista);
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
        /// Recupera todos los horarios medicos
        /// </summary>
        /// <returns>Un listado de todos los horarios</returns>
        /// <remarks>
        /// NOTA: Este endpoint es para libre consumo sin importar el usuario
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Salud/ObtenerHorarioMedicos/
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///             "id": 0,
        ///             "hora_inicio": "string",
        ///             "hora_fin": "string",
        ///             "dia": 0,
        ///             "cuil_especialista": "string",
        ///             "especialista": "string",
        ///             "activo": true,
        ///             "especialidad": {
        ///                 "id": 0,
        ///                 "nombre": "string",
        ///                 "descripcion": "string",
        ///                 "activo": true
        ///             }
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve todos los horarios medicos</response>
        /// <response code="204" >No se encontro ningun empleado </response>
        /// <response code="400" >Ocurre un error en la consulta </response>    
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerHorarioMedicos")]
        [ProducesResponseType(typeof(IEnumerable<HorariosSalud>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<HorariosSalud>> ObtenerHorarioMedicos()
        {
            try
            {
                List<HorariosSalud>? listadoHorarios = HealthAdapter.ObtenerHorariosMedicos();

                if (listadoHorarios == null) return Conflict();
                if (listadoHorarios.Count == 0) return NoContent();

                return Ok(listadoHorarios);
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, this.Request.Path, ex.Message, ControllerName);
                return BadRequest();
            }
        }
        /// <summary>
        /// Busca un horario por su ID
        /// </summary>
        /// <param name="id">Es el identificador del horario en la BD</param>
        /// <returns>Un horario que coincida con ese ID</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Salud/ObtenerHorariosXId/{id}
        ///     
        ///     RESPONSE:
        ///     {
        ///         "id": 0,
        ///         "hora_inicio": "string",
        ///         "hora_fin": "string",
        ///         "dia": 0,
        ///         "cuil_especialista": "string",
        ///         "especialista": "string",
        ///         "activo": true,
        ///         "especialidad": {
        ///             "id": 0,
        ///             "nombre": "string",
        ///             "descripcion": "string",
        ///             "activo": true
        ///         }
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Un horario con ese ID </response>
        /// <response code="204" >No se encontro ningun horario </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{id}")]
        [ActionName("ObtenerHorariosXId")]
        [Authorize]
        [ProducesResponseType(typeof(HorariosSalud), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<HorariosSalud> ObtenerHorariosXId(int id)
        {
            try
            {
                if (TienePermiso(73))
                {
                    HorariosSalud? hora = HealthAdapter.ObtenerHorariosXId(id);
                    if (hora == null) return Conflict();
                    if (hora.id == -1) return NoContent();
                    else return Ok(hora);
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
        /// Busca los horarios por CUIL de especialista
        /// </summary>
        /// <param name="cuil">Es el identificador del especialista en la BD</param>
        /// <returns>Un listado de horarios de este especialista</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Salud/ObtenerHorariosXCuil/{cuil}
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///             "id": 0,
        ///             "hora_inicio": "string",
        ///             "hora_fin": "string",
        ///             "dia": 0,
        ///             "cuil_especialista": "string",
        ///             "especialista": "string",
        ///             "activo": true,
        ///             "especialidad": {
        ///                 "id": 0,
        ///                 "nombre": "string",
        ///                 "descripcion": "string",
        ///                 "activo": true
        ///             }
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Los horarios del especialista </response>
        /// <response code="204" >No se encontro ningun horarios </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{cuil}")]
        [ActionName("ObtenerHorariosXCuil")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<HorariosSalud>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<HorariosSalud>> ObtenerHorariosXCuil(string cuil)
        {
            try
            {
                if (TienePermiso(73))
                {
                    List<HorariosSalud>? listadoHorarios = HealthAdapter.ObtenerHorariosXCuil(cuil);
                    if (listadoHorarios == null) return Conflict();
                    if (listadoHorarios.Count == 0) return NoContent();

                    return Ok(listadoHorarios);
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
        /// Permite la modificacion de un horario
        /// </summary>
        /// <param name="id"> El ID del horario a modificar</param>
        /// <param name="horarioSalud"> Los datos modificados del horario</param>
        /// <returns>Un horario modificado en BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     PUT /api/Salud/ModificarHorarioMedicos/{id}
        ///     BODY:
        ///     {
        ///         "id": 0,
        ///         "hora_inicio": "string",
        ///         "hora_fin": "string",
        ///         "dia": 0,
        ///         "cuil_especialista": "string",
        ///         "especialista": "string",
        ///         "activo": true,
        ///         "especialidad": {
        ///             "id": 0,
        ///             "nombre": "string",
        ///             "descripcion": "string",
        ///             "activo": true
        ///         }
        ///     }
        ///     (No se modifica los datos de la especialidad sino que cambiamos el id_especialidad del horario)
        ///     
        ///     RESPONSE:
        ///     {
        ///         "id": 0,
        ///         "hora_inicio": "string",
        ///         "hora_fin": "string",
        ///         "dia": 0,
        ///         "cuil_especialista": "string",
        ///         "especialista": "string",
        ///         "activo": true,
        ///         "especialidad": {
        ///             "id": 0,
        ///             "nombre": "string",
        ///             "descripcion": "string",
        ///             "activo": true
        ///         }
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve el horario modificado en BD </response>
        /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del horario a modificar </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{id}")]
        [ActionName("ModificarHorarioMedicos")]
        [Authorize]
        [ProducesResponseType(typeof(HorariosSalud), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<HorariosSalud> ModificarHorarioMedicos(int id, [FromBody, Required] HorariosSalud horarioSalud)
        {
            try
            {
                if (id != horarioSalud.id) return BadRequest();
                //El numero de funcion es: 71
                if (TienePermiso(71))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData != null &&
                       userData.Length > 0 &&
                       userData != "NO DATA" &&
                       int.TryParse(userData.Split(',')[2], out int idUserMod))
                    {
                        horarioSalud = HealthAdapter.ModificarHorarioSalud(horarioSalud, idUserMod);
                        if (horarioSalud.id != -1) return Ok(horarioSalud);
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
        /// Permite crear horarios medicos
        /// </summary>
        /// <param name="horarioMedico">El horario que deseamos crear, se envia en el Body</param>
        /// <returns>Un horario creado en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Salud/CrearHorarioMedico/
        ///     BODY:
        ///     {
        ///         "id": 0,
        ///         "hora_inicio": "string",
        ///         "hora_fin": "string",
        ///         "dia": 0,
        ///         "cuil_especialista": "string",
        ///         "especialista": "string",
        ///         "activo": true,
        ///         "especialidad": {
        ///             "id": 0,
        ///             "nombre": "string",
        ///             "descripcion": "string",
        ///             "activo": true
        ///         }
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///         "id": 0,
        ///         "hora_inicio": "string",
        ///         "hora_fin": "string",
        ///         "dia": 0,
        ///         "cuil_especialista": "string",
        ///         "especialista": "string",
        ///         "activo": true,
        ///         "especialidad": {
        ///             "id": 0,
        ///             "nombre": "string",
        ///             "descripcion": "string",
        ///             "activo": true
        ///         }
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve el horario creado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El empleado no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost]
        [ActionName("CrearHorarioMedico")]
        [Authorize]
        [ProducesResponseType(typeof(HorariosSalud), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<HorariosSalud> CrearHorarioMedico([FromBody] HorariosSalud horarioMedico)
        {
            try
            {
                if (TienePermiso(70))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData != null &&
                       userData.Length > 0 &&
                       userData != "NO DATA" &&
                       int.TryParse(userData.Split(',')[2], out int idUserCrea))
                    {
                        horarioMedico = HealthAdapter.CrearHorario(horarioMedico, idUserCrea);
                        if (horarioMedico.id != -1) return Created("Horario Creado", horarioMedico);
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
        /// Permite eliminar horarios
        /// </summary>
        /// <param name="id">Es el id del horario a eliminar</param>
        /// <returns>Un mensaje de OK o el horario que no se pudo eliminar</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Salud/EliminarHorario/{id}
        ///     
        ///     RESPONSE:
        ///     {
        ///         "Horario Eliminado"
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
        [ActionName("EliminarHorario")]
        [Authorize]
        [ProducesResponseType(typeof(string),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> EliminarHorario(int id)
        {
            try
            {
                if (TienePermiso(72))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        if (HealthAdapter.EliminarHorario(id)) return Ok("Horario Eliminado");
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
        /// Recupera las faltas del especialista
        /// </summary>
        /// <param name="cuil">El identificador de esta persona</param>
        /// <returns>Un listado con todas las faltas</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Salud/ObtenerFaltasEspecialista/{cuil}
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///           "id": 0,
        ///           "cuil_especialista": "string",
        ///           "fecha_alta": "2024-07-08T20:20:49.743Z",
        ///           "observacion": "string",
        ///         }
        ///     ]
        /// </remarks>
        /// <response code="200" >Devuelve un listado de las faltas de este especialista </response>
        /// <response code="204" >No se encontro ninguna falta </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{cuil}")]
        [ActionName("ObtenerFaltasEspecialista")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<FaltaEspecialista>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<FaltaEspecialista>> ObtenerFaltasEspecialista(string cuil)
        {
            try
            {
                if (TienePermiso(74))
                {
                    List<FaltaEspecialista>? cursos = HealthAdapter.ObtenerFaltasXEspecialista(cuil);
                    if (cursos == null) return Conflict();
                    if (cursos.Count == 0) return NoContent();
                    else return Ok(cursos);
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
        /// Permite registrar la inasistencia de un especialista
        /// </summary>
        /// <param name="faltaEspecialista">La inasistencia que deseamos crear, se envia en el Body</param>
        /// <returns>Una inasistencia creada en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Salud/RegistrarFaltaMedica/
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "cuil": "string",
        ///       "fecha_alta": "2024-07-08T20:17:22.790Z",
        ///       "observacion": "string"
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "cuil": "string",
        ///       "fecha_alta": "2024-07-08T20:17:22.790Z",
        ///       "observacion": "string"
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve la falta registrada y creada en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El empleado no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost]
        [ActionName("RegistrarFaltaMedica")]
        [Authorize]
        [ProducesResponseType(typeof(FaltaEspecialista), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<FaltaEspecialista> RegistrarFaltaMedica([FromBody] FaltaEspecialista faltaEspecialista)
        {
            try
            {
                if (TienePermiso(74))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData != null &&
                       userData.Length > 0 &&
                       userData != "NO DATA" &&
                       int.TryParse(userData.Split(',')[2], out int idUserCrea))
                    {
                        faltaEspecialista = HealthAdapter.CrearFaltaMedica(faltaEspecialista, idUserCrea);
                        if (faltaEspecialista.id != -1) return Created("Falta Registrada", faltaEspecialista);
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
        /// Recupera todos los cursos medicos
        /// </summary>
        /// <returns>Un listado con todos los cursos</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Salud/ObtenerCursosMedicos/
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///           "id": 0,
        ///           "nombre_curso": "string",
        ///           "nombre_docente": "string",
        ///           "fecha_inicio": "2024-07-08T20:20:49.743Z",
        ///           "fecha_fin": "2024-07-08T20:20:49.743Z",
        ///           "cupo_maximo": 0,
        ///           "activo": true
        ///         }
        ///     ]
        /// </remarks>
        /// <response code="200" >Devuelve un listado de todos los cursos medicos </response>
        /// <response code="204" >No se encontro ningun curso </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerCursosMedicos")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<CursosMedicos>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<CursosMedicos>> ObtenerCursosMedicos()
        {
            try
            {
                if (TienePermiso(78))
                {
                    List<CursosMedicos>? cursos = HealthAdapter.ObtenerCursosMedicos();
                    if (cursos == null) return Conflict();
                    if (cursos.Count == 0) return NoContent();
                    else return Ok(cursos);
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
        /// Permite la modificacion de un curso medico
        /// </summary>
        /// <param name="id"> El ID del curso medico a modificar</param>
        /// <param name="cursoMedico"> Los datos modificados del curso medico</param>
        /// <returns>Un curso medico modificado en BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     PUT /api/Salud/ModificarCursoMedicos/{id}
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "nombre_curso": "string",
        ///       "nombre_docente": "string",
        ///       "fecha_inicio": "2024-07-08T20:20:49.743Z",
        ///       "fecha_fin": "2024-07-08T20:20:49.743Z",
        ///       "cupo_maximo": 0,
        ///       "activo": true
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "nombre_curso": "string",
        ///       "nombre_docente": "string",
        ///       "fecha_inicio": "2024-07-08T20:20:49.743Z",
        ///       "fecha_fin": "2024-07-08T20:20:49.743Z",
        ///       "cupo_maximo": 0,
        ///       "activo": true
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve el curso medico modificado en BD </response>
        /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del curso medico a modificar </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{id}")]
        [ActionName("ModificarCursoMedicos")]
        [Authorize]
        [ProducesResponseType(typeof(CursosMedicos), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CursosMedicos> ModificarCursoMedicos(int id, [FromBody, Required] CursosMedicos cursoMedico)
        {
            try
            {
                if (id != cursoMedico.id) return BadRequest();
                //El numero de funcion es: 71
                if (TienePermiso(76))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData != null &&
                       userData.Length > 0 &&
                       userData != "NO DATA" &&
                       int.TryParse(userData.Split(',')[2], out int idUserMod))
                    {
                        cursoMedico = HealthAdapter.ModificarCursosMedicos(cursoMedico);
                        if (cursoMedico.id != -1) return Ok(cursoMedico);
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
        /// Permite crear cursos medicos
        /// </summary>
        /// <param name="cursoMedico">El curso medico que deseamos crear, se envia en el Body</param>
        /// <returns>Un curso medico creado en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Salud/CrearCursoMedico/
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "nombre_curso": "string",
        ///       "nombre_docente": "string",
        ///       "fecha_inicio": "2024-07-08T20:20:49.743Z",
        ///       "fecha_fin": "2024-07-08T20:20:49.743Z",
        ///       "cupo_maximo": 0,
        ///       "activo": true
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "nombre_curso": "string",
        ///       "nombre_docente": "string",
        ///       "fecha_inicio": "2024-07-08T20:20:49.743Z",
        ///       "fecha_fin": "2024-07-08T20:20:49.743Z",
        ///       "cupo_maximo": 0,
        ///       "activo": true
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve el curso medico creado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El empleado no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost]
        [ActionName("CrearCursoMedico")]
        [Authorize]
        [ProducesResponseType(typeof(CursosMedicos), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CursosMedicos> CrearCursoMedico([FromBody] CursosMedicos cursoMedico)
        {
            try
            {
                if (TienePermiso(75))
                {
                    cursoMedico = HealthAdapter.CrearCursoMedico(cursoMedico);
                    if (cursoMedico.id != -1) return Created("Curso Medico Creado",cursoMedico);
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
        /// Permite eliminar cursos medicos
        /// </summary>
        /// <param name="id">Es el id del curso medico a eliminar</param>
        /// <returns>Un mensaje de OK o el curso medico que no se pudo eliminar</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Salud/EliminarCursoMedico/{id}
        ///     
        ///     RESPONSE:
        ///     {
        ///         "Curso Eliminado"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve OK y un mensaje </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El empleado no genero su JWT</response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y devuelve el curso </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>        
        [HttpDelete("{id}")]
        [ActionName("EliminarCursoMedico")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> EliminarCursoMedico(int id)
        {
            try
            {
                if (TienePermiso(77))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        if (HealthAdapter.EliminarCursoMedico(id)) return Ok("Curso Eliminado");
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
        /// Recupera todos los turnos medicos
        /// </summary>
        /// <returns>Un listado de todos los turnos medicos no importa el estado</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Salud/ObtenerTurnosMedicos/
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///           "id": 0,
        ///           "cuil_medico": "string",
        ///           "especialista": "string",
        ///           "legajo": "string",
        ///           "paciente": "string",
        ///           "fecha_solicitud": "2024-07-08T20:31:46.626Z",
        ///           "fecha_atencion": "2024-07-08T20:31:46.626Z",
        ///           "hora_atencion": "string",
        ///           "asunto": "string",
        ///           "id_estado_turno": 0,
        ///           "estado": "string"
        ///         }
        ///     ]
        /// </remarks>
        /// <response code="200" >Un listado de todos los turnos medicos </response>
        /// <response code="204" >No se encontro ningun turno medico</response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerTurnosMedicos")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<TurnosMedicos>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<TurnosMedicos>> ObtenerTurnosMedicos()
        {
            try
            {
                if (TienePermiso(83))
                {
                    List<TurnosMedicos>? cursos = HealthAdapter.ObtenerTurnosMedicos();
                    if (cursos == null) return Conflict();
                    if (cursos.Count == 0) return NoContent();
                    else return Ok(cursos);
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
        /// Recupera los turnos pendientes
        /// </summary>
        /// <returns>Un listado con todos los turnos medicos pendientes</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Salud/ObtenerTurnosMedicosPendientes/
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///           "id": 0,
        ///           "cuil_medico": "string",
        ///           "especialista": "string",
        ///           "legajo": "string",
        ///           "paciente": "string",
        ///           "fecha_solicitud": "2024-07-08T20:31:46.626Z",
        ///           "fecha_atencion": "2024-07-08T20:31:46.626Z",
        ///           "hora_atencion": "string",
        ///           "asunto": "string",
        ///           "id_estado_turno": 0,
        ///           "estado": "string"
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado con los turnos medicos pendientes </response>
        /// <response code="204" >No se encontro ningun turno pendiente</response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerTurnosMedicosPendientes")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<TurnosMedicos>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<TurnosMedicos>> ObtenerTurnosMedicosPendientes()
        {
            try
            {
                if (TienePermiso(83))
                {
                    List<TurnosMedicos>? cursos = HealthAdapter.ObtenerTurnosMedicosPendientes();
                    if (cursos == null) return Conflict();
                    if (cursos.Count == 0) return NoContent();
                    else return Ok(cursos);
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
        /// Un listado de turnos asignados
        /// </summary>
        /// <returns>Un listado de turnos que esten en estado asignado</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Salud/ObtenerTurnosMedicosAsignados/
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///           "id": 0,
        ///           "cuil_medico": "string",
        ///           "especialista": "string",
        ///           "legajo": "string",
        ///           "paciente": "string",
        ///           "fecha_solicitud": "2024-07-08T20:31:46.626Z",
        ///           "fecha_atencion": "2024-07-08T20:31:46.626Z",
        ///           "hora_atencion": "string",
        ///           "asunto": "string",
        ///           "id_estado_turno": 0,
        ///           "estado": "string"
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado con los turnos medicos asignados </response>
        /// <response code="204" >No se encontro ningun turno asignado</response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerTurnosMedicosAsignados")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<TurnosMedicos>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<TurnosMedicos>> ObtenerTurnosMedicosAsignados()
        {
            try
            {
                if (TienePermiso(83))
                {
                    List<TurnosMedicos>? cursos = HealthAdapter.ObtenerTurnosMedicosAsignados();
                    if (cursos == null) return Conflict();
                    if (cursos.Count == 0) return NoContent();
                    else return Ok(cursos);
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
        /// Un listado de turnos en curso
        /// </summary>
        /// <returns>Un listado de turnos que esten en estado en curso</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Salud/ObtenerTurnosMedicosEnCurso/
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///           "id": 0,
        ///           "cuil_medico": "string",
        ///           "especialista": "string",
        ///           "legajo": "string",
        ///           "paciente": "string",
        ///           "fecha_solicitud": "2024-07-08T20:31:46.626Z",
        ///           "fecha_atencion": "2024-07-08T20:31:46.626Z",
        ///           "hora_atencion": "string",
        ///           "asunto": "string",
        ///           "id_estado_turno": 0,
        ///           "estado": "string"
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado con los turnos medicos en curso </response>
        /// <response code="204" >No se encontro ningun turno en curso</response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerTurnosMedicosEnCurso")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<TurnosMedicos>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<TurnosMedicos>> ObtenerTurnosMedicosEnCurso()
        {
            try
            {
                if (TienePermiso(83))
                {
                    List<TurnosMedicos>? cursos = HealthAdapter.ObtenerTurnosMedicosEnCurso();
                    if (cursos == null) return Conflict();
                    if (cursos.Count == 0) return NoContent();
                    else return Ok(cursos);
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
        /// Busca turnos por ID
        /// </summary>
        /// <param name="id">Es el identificacor del turno en la BD</param>
        /// <returns>Un turno que coincida con ese ID</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Salud/ObtenerTurnoXId/{id}
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "cuil_medico": "string",
        ///       "especialista": "string",
        ///       "legajo": "string",
        ///       "paciente": "string",
        ///       "fecha_solicitud": "2024-07-08T20:31:46.626Z",
        ///       "fecha_atencion": "2024-07-08T20:31:46.626Z",
        ///       "hora_atencion": "string",
        ///       "asunto": "string",
        ///       "id_estado_turno": 0,
        ///       "estado": "string"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un turno con este ID </response>
        /// <response code="204" >No se encontro ningun turno </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{id}")]
        [ActionName("ObtenerTurnoXId")]
        [Authorize]
        [ProducesResponseType(typeof(TurnosMedicos), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TurnosMedicos> ObtenerTurnoXId(int id)
        {
            try
            {
                if (TienePermiso(83))
                {
                    TurnosMedicos? hora = HealthAdapter.ObtenerTurnoXId(id);
                    if (hora == null) return Conflict();
                    if (hora.id == -1) return NoContent();
                    else return Ok(hora);
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
        /// Un listado de turnos asociados al legajo
        /// </summary>
        /// <param name="legajo">Es el identificacor del estudiante/no docente en la BD</param>
        /// <returns>Un listados de turnos con este legajo</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Salud/ObtenerTurnosXLegajo/{legajo}
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///           "id": 0,
        ///           "cuil_medico": "string",
        ///           "especialista": "string",
        ///           "legajo": "string",
        ///           "paciente": "string",
        ///           "fecha_solicitud": "2024-07-08T20:31:46.626Z",
        ///           "fecha_atencion": "2024-07-08T20:31:46.626Z",
        ///           "hora_atencion": "string",
        ///           "asunto": "string",
        ///           "id_estado_turno": 0,
        ///           "estado": "string"
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado de turnos de esta persona</response>
        /// <response code="204" >No se encontro ningun turno </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{legajo}")]
        [ActionName("ObtenerTurnosXLegajo")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<TurnosMedicos>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<TurnosMedicos>> ObtenerTurnosXLegajo(string legajo)
        {
            try
            {
                if (TienePermiso(83))
                {
                    List<TurnosMedicos>? cursos = HealthAdapter.ObtenerTurnoXlegajo(legajo);
                    if (cursos == null) return Conflict();
                    if (cursos.Count == 0) return NoContent();
                    else return Ok(cursos);
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
        /// Permite la modificacion de un turno
        /// </summary>
        /// <param name="id_turno"> El ID del turno a modificar</param>
        /// <param name="turno"> Los datos modificados del turno</param>
        /// <returns>Un turno modificado en BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     PUT /api/Salud/ModificarTurnoMedico/{id_turno}
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "cuil_medico": "string",
        ///       "especialista": "string",
        ///       "legajo": "string",
        ///       "paciente": "string",
        ///       "fecha_solicitud": "2024-07-08T20:31:46.626Z",
        ///       "fecha_atencion": "2024-07-08T20:31:46.626Z",
        ///       "hora_atencion": "string",
        ///       "asunto": "string",
        ///       "id_estado_turno": 0,
        ///       "estado": "string"
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "cuil_medico": "string",
        ///       "especialista": "string",
        ///       "legajo": "string",
        ///       "paciente": "string",
        ///       "fecha_solicitud": "2024-07-08T20:31:46.626Z",
        ///       "fecha_atencion": "2024-07-08T20:31:46.626Z",
        ///       "hora_atencion": "string",
        ///       "asunto": "string",
        ///       "id_estado_turno": 0,
        ///       "estado": "string"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve el turno medico modificado en BD </response>
        /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del turno medico a modificar </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{id_turno}")]
        [ActionName("ModificarTurnoMedico")]
        [Authorize]
        [ProducesResponseType(typeof(TurnosMedicos), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TurnosMedicos> ModificarTurnoMedico(int id_turno, [FromBody, Required] TurnosMedicos turno)
        {
            try
            {
                if (id_turno != turno.id) return BadRequest();
                //El numero de funcion es: 68
                if (TienePermiso(81))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData != null &&
                       userData.Length > 0 &&
                       userData != "NO DATA" &&
                       int.TryParse(userData.Split(',')[2], out int idUserMod))
                    {
                        turno = HealthAdapter.ModificarTurnoMedico(turno, idUserMod);
                        if (turno.id != -1) return Ok(turno);
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
        /// Permite crear turnos medicos
        /// </summary>
        /// <param name="turno">El turno que deseamos crear, se envia en el Body</param>
        /// <returns>Un turno creado en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Salud/CrearTurnoMedico/
        ///     BODY:
        ///     {
        ///       "id": 0,
        ///       "cuil_medico": "string",
        ///       "especialista": "string",
        ///       "legajo": "string",
        ///       "paciente": "string",
        ///       "fecha_solicitud": "2024-07-08T20:31:46.626Z",
        ///       "fecha_atencion": "2024-07-08T20:31:46.626Z",
        ///       "hora_atencion": "string",
        ///       "asunto": "string",
        ///       "id_estado_turno": 0,
        ///       "estado": "string"
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "cuil_medico": "string",
        ///       "especialista": "string",
        ///       "legajo": "string",
        ///       "paciente": "string",
        ///       "fecha_solicitud": "2024-07-08T20:31:46.626Z",
        ///       "fecha_atencion": "2024-07-08T20:31:46.626Z",
        ///       "hora_atencion": "string",
        ///       "asunto": "string",
        ///       "id_estado_turno": 0,
        ///       "estado": "string"
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve el turno creado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El empleado no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost]
        [ActionName("CrearTurnoMedico")]
        [Authorize]
        [ProducesResponseType(typeof(TurnosMedicos), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TurnosMedicos> CrearTurnoMedico([FromBody] TurnosMedicos turno)
        {
            try
            {
                if (TienePermiso(80))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData != null &&
                       userData.Length > 0 &&
                       userData != "NO DATA" &&
                       int.TryParse(userData.Split(',')[2], out int idUserCrea))
                    {
                        turno = HealthAdapter.CrearTurnosMedicos(turno, idUserCrea);
                        if (turno.id != -1) return Created("Turno Medico Creado", turno);
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
