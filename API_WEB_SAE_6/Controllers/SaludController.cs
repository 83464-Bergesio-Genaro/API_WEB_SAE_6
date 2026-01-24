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

    ///// <summary>
    ///// Es el controlador para todo relacionado al area de salud/bienestar
    ///// </summary>
    //[EnableCors("CorsRules")]
    //[Route("api/[controller]/[action]")]
    //[ApiController]
    //public class SaludController : Controller
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
    //    public SaludController(IConfiguration config)
    //    {
    //        _config = config;
    //    }
    //    /// <summary>
    //    /// Recupera todos los especialistas medicos del area
    //    /// </summary>
    //    /// <returns> Un listado de todos los empleados del area medica</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Salud/ObtenerListadoEspecialistas/
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///         {
    //    ///            "cuil": "string",
    //    ///            "apellido": "string",
    //    ///            "nombre": "string",
    //    ///            "presta_servicio": true,
    //    ///            "especialidad": {
    //    ///                 "id": 0,
    //    ///                 "nombre": "string",
    //    ///                 "descripcion": "string",
    //    ///                 "activo": true
    //    ///             }  
    //    ///         }
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un listado completo de especialistas </response>
    //    /// <response code="204" >No se encontro ningun empleado medico </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>   
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet]
    //    [ActionName("ObtenerListadoEspecialistas")]
    //    [Authorize]
    //    [ProducesResponseType(typeof(IEnumerable<EspecialistaMedico>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<EspecialistaMedico>>> ObtenerListadoEspecialistas()
    //    {
    //        try
    //        {
    //            //El numero de funcion es: 69
    //            if (await TienePermiso(69))
    //            {
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_SALUD_Listar_Personal_Medico");
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<EspecialistaMedico> listadoEspecialistasCompleto = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    EspecialistaMedico especialista = new(row);
    //                    listadoEspecialistasCompleto.Add(especialista);
    //                }
    //                return Ok(listadoEspecialistasCompleto);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR LISTANDO PERSONAL MEDICO");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Busca especialistas por CUIL
    //    /// </summary>
    //    /// <param name="cuil">Es el identificador del especialista en la BD</param>
    //    /// <returns>Un especialista que coincida con ese CUIL</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Salud/ObtenerEspecialistasXCuil/{cuil}
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///        "cuil": "string",
    //    ///        "apellido": "string",
    //    ///        "nombre": "string",
    //    ///        "presta_servicio": true,
    //    ///        "especialidad": {
    //    ///             "id": 0,
    //    ///             "nombre": "string",
    //    ///             "descripcion": "string",
    //    ///             "activo": true
    //    ///         }  
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un especialista con este CUIL </response>
    //    /// <response code="204" >No se encontro ningun especialista </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet("{cuil}")]
    //    [ActionName("ObtenerEspecialistasXCuil")]
    //    [Authorize]
    //    [ProducesResponseType(typeof(EspecialistaMedico), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<EspecialistaMedico>> ObtenerEspecialistasXCuil(string cuil)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(69))
    //            {
    //                Dictionary<string, string> parametros = new()
    //                {
    //                    { "@cuil_especialista", cuil }
    //                };
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_SALUD_Buscar_Especialista_Cuil", parametros);
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                else return Ok(new EspecialistaMedico(respuesta.Rows[0]));
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR BUSCANDO PERSONAL MEDICO");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite la modificacion de un especialista
    //    /// </summary>
    //    /// <param name="cuil"> El CUIL del especialista a modificar</param>
    //    /// <param name="especialista"> Los datos modificados del especialista</param>
    //    /// <returns>Un especialista modificado en BD</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     PUT /api/Salud/ModificarEspecialista/{cuil}
    //    ///     BODY:
    //    ///     {
    //    ///        "cuil": "string",
    //    ///        "apellido": "string",
    //    ///        "nombre": "string",
    //    ///        "presta_servicio": true,
    //    ///        "especialidad": {
    //    ///             "id": 0,
    //    ///             "nombre": "string",
    //    ///             "descripcion": "string",
    //    ///             "activo": true
    //    ///         }  
    //    ///     }
    //    ///     (No se modifica los datos de la especialidad sino que cambiamos el id_especialidad de especialista)    
    //    /// 
    //    ///     RESPONSE:
    //    ///     {
    //    ///        "cuil": "string",
    //    ///        "apellido": "string",
    //    ///        "nombre": "string",
    //    ///        "presta_servicio": true,
    //    ///        "especialidad": {
    //    ///             "id": 0,
    //    ///             "nombre": "string",
    //    ///             "descripcion": "string",
    //    ///             "activo": true
    //    ///         }  
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve el especialista modificado en BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta o el CUIL es diferente que el del especialista a modificar </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPut("{cuil}")]
    //    [ActionName("ModificarEspecialista")]
    //    [Authorize]
    //    [ProducesResponseType(typeof(EspecialistaMedico), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<EspecialistaMedico>> ModificarEspecialista(string cuil, [FromBody, Required] EspecialistaMedico especialista)
    //    {
    //        try
    //        {
    //            if (cuil != especialista.cuil) return BadRequest();
    //            //El numero de funcion es: 68
    //            if (await TienePermiso(68))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                string usuarioActual = userData.Split(',')[2];
    //                Dictionary<string, string> parametros = new() {
    //                    { "@cuil_especialista",especialista.cuil },
    //                    { "@nombre", especialista.nombre },
    //                    { "@apellido",especialista.apellido },
    //                    { "@id_especialidad",especialista.especialidad.id.ToString() },
    //                    { "@activo",especialista.presta_servicio.ToString() },
    //                    { "@id_usuario_mod",usuarioActual}
    //                };

    //                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_SALUD_Modificar_Especialista_Medico", parametros);

    //                //En este caso sino modifica nada es un conflicto en la BD
    //                if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                return Ok(new EspecialistaMedico(respuesta.Rows[0]));
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR MODIFICANDO EMPLEADO");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite crear un especialista
    //    /// </summary>
    //    /// <param name="especialista">El especialista que deseamos crear, se envia en el Body</param>
    //    /// <returns>Un especialista creado en la base de datos o error</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Salud/CrearEspecialista/
    //    ///     BODY:
    //    ///     {
    //    ///        "cuil": "string",
    //    ///        "apellido": "string",
    //    ///        "nombre": "string",
    //    ///        "presta_servicio": true,
    //    ///        "especialidad": {
    //    ///             "id": 0,
    //    ///             "nombre": "string",
    //    ///             "descripcion": "string",
    //    ///             "activo": true
    //    ///         }  
    //    ///     }
    //    ///     (No se crean los datos de la especialidad sino que le asignamos el id_especialidad) 
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///        "cuil": "string",
    //    ///        "apellido": "string",
    //    ///        "nombre": "string",
    //    ///        "presta_servicio": true,
    //    ///        "especialidad": {
    //    ///             "id": 0,
    //    ///             "nombre": "string",
    //    ///             "descripcion": "string",
    //    ///             "activo": true
    //    ///         }  
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="201" >Devuelve el especialista creado en la BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El especialista no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPost]
    //    [ActionName("CrearEspecialista")]
    //    [Authorize]
    //    [ProducesResponseType(typeof(EspecialistaMedico), StatusCodes.Status201Created)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<EspecialistaMedico>> CrearEspecialista([FromBody] EspecialistaMedico especialista)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(67))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {
    //                    string usuarioActual = userData.Split(',')[2];
    //                    Dictionary<string, string> parametros = new() {
    //                        { "@cuil_especialista",especialista.cuil },
    //                        { "@nombre", especialista.nombre },
    //                        { "@apellido",especialista.apellido },
    //                        { "@id_especialidad",especialista.especialidad.id.ToString() },
    //                        { "@activo",especialista.presta_servicio.ToString() },
    //                        { "@id_usuario_alta",usuarioActual}
    //                    };

    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_SALUD_Crear_Especialista_Medico", parametros);
    //                    //En este caso sino crea es un error en la BD
    //                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                    return Created("Especialista Creado", new EspecialistaMedico(respuesta.Rows[0]));
    //                }

    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR CREANDO PERSONAL MEDICO");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Recupera todos los horarios medicos
    //    /// </summary>
    //    /// <returns>Un listado de todos los horarios</returns>
    //    /// <remarks>
    //    /// NOTA: Este endpoint es para libre consumo sin importar el usuario
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Salud/ObtenerHorarioMedicos/
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///         {
    //    ///             "id": 0,
    //    ///             "hora_inicio": "string",
    //    ///             "hora_fin": "string",
    //    ///             "dia": 0,
    //    ///             "cuil_especialista": "string",
    //    ///             "especialista": "string",
    //    ///             "activo": true,
    //    ///             "especialidad": {
    //    ///                 "id": 0,
    //    ///                 "nombre": "string",
    //    ///                 "descripcion": "string",
    //    ///                 "activo": true
    //    ///             }
    //    ///         }
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve todos los horarios medicos</response>
    //    /// <response code="204" >No se encontro ningun empleado </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>    
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet]
    //    [ActionName("ObtenerHorarioMedicos")]
    //    [ProducesResponseType(typeof(IEnumerable<HorariosSalud>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<HorariosSalud>>> ObtenerHorarioMedicos()
    //    {
    //        try
    //        {
    //            DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_SALUD_Listar_Horarios_Medico");
    //            if (respuesta.Rows.Count == 0) return NoContent();
    //            if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //            List<HorariosSalud> listadoCompletoHorario = new();
    //            foreach (DataRow row in respuesta.Rows)
    //            {
    //                HorariosSalud horarioSalud = new(row);
    //                listadoCompletoHorario.Add(horarioSalud);
    //            }
    //            return Ok(listadoCompletoHorario);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR LISTANDO HORARIOS MEDICO");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Busca un horario por su ID
    //    /// </summary>
    //    /// <param name="id">Es el identificador del horario en la BD</param>
    //    /// <returns>Un horario que coincida con ese ID</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Salud/ObtenerHorariosXId/{id}
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///         "id": 0,
    //    ///         "hora_inicio": "string",
    //    ///         "hora_fin": "string",
    //    ///         "dia": 0,
    //    ///         "cuil_especialista": "string",
    //    ///         "especialista": "string",
    //    ///         "activo": true,
    //    ///         "especialidad": {
    //    ///             "id": 0,
    //    ///             "nombre": "string",
    //    ///             "descripcion": "string",
    //    ///             "activo": true
    //    ///         }
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Un horario con ese ID </response>
    //    /// <response code="204" >No se encontro ningun horario </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet("{id}")]
    //    [ActionName("ObtenerHorariosXId")]
    //    [Authorize]
    //    [ProducesResponseType(typeof(HorariosSalud), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<HorariosSalud>> ObtenerHorariosXId(int id)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(73))
    //            {
    //                Dictionary<string, string> parametros = new()
    //                {
    //                    { "@id_horario_medico", id.ToString() }
    //                };
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_SALUD_Buscar_Horario_Medico_Id", parametros);
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                else return Ok(new HorariosSalud(respuesta.Rows[0]));
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR BUSCANDO HORARIO MEDICO");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Busca los horarios por CUIL de especialista
    //    /// </summary>
    //    /// <param name="cuil">Es el identificador del especialista en la BD</param>
    //    /// <returns>Un listado de horarios de este especialista</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Salud/ObtenerHorariosXCuil/{cuil}
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///         {
    //    ///             "id": 0,
    //    ///             "hora_inicio": "string",
    //    ///             "hora_fin": "string",
    //    ///             "dia": 0,
    //    ///             "cuil_especialista": "string",
    //    ///             "especialista": "string",
    //    ///             "activo": true,
    //    ///             "especialidad": {
    //    ///                 "id": 0,
    //    ///                 "nombre": "string",
    //    ///                 "descripcion": "string",
    //    ///                 "activo": true
    //    ///             }
    //    ///         }
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Los horarios del especialista </response>
    //    /// <response code="204" >No se encontro ningun horarios </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet("{cuil}")]
    //    [ActionName("ObtenerHorariosXCuil")]
    //    [Authorize]
    //    [ProducesResponseType(typeof(IEnumerable<HorariosSalud>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<HorariosSalud>>> ObtenerHorariosXCuil(string cuil)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(73))
    //            {
    //                Dictionary<string, string> parametros = new()
    //                {
    //                    { "@cuil_es",cuil }
    //                };
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_SALUD_Buscar_Horario_Medico_Cuil", parametros);

    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<HorariosSalud> listadoCompletoHorario = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    HorariosSalud horarioSalud = new(row);
    //                    listadoCompletoHorario.Add(horarioSalud);
    //                }
    //                return Ok(listadoCompletoHorario);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR BUSCANDO HORARIO MEDICO");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite la modificacion de un horario
    //    /// </summary>
    //    /// <param name="id"> El ID del horario a modificar</param>
    //    /// <param name="horarioSalud"> Los datos modificados del horario</param>
    //    /// <returns>Un horario modificado en BD</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     PUT /api/Salud/ModificarHorarioMedicos/{id}
    //    ///     BODY:
    //    ///     {
    //    ///         "id": 0,
    //    ///         "hora_inicio": "string",
    //    ///         "hora_fin": "string",
    //    ///         "dia": 0,
    //    ///         "cuil_especialista": "string",
    //    ///         "especialista": "string",
    //    ///         "activo": true,
    //    ///         "especialidad": {
    //    ///             "id": 0,
    //    ///             "nombre": "string",
    //    ///             "descripcion": "string",
    //    ///             "activo": true
    //    ///         }
    //    ///     }
    //    ///     (No se modifica los datos de la especialidad sino que cambiamos el id_especialidad del horario)
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///         "id": 0,
    //    ///         "hora_inicio": "string",
    //    ///         "hora_fin": "string",
    //    ///         "dia": 0,
    //    ///         "cuil_especialista": "string",
    //    ///         "especialista": "string",
    //    ///         "activo": true,
    //    ///         "especialidad": {
    //    ///             "id": 0,
    //    ///             "nombre": "string",
    //    ///             "descripcion": "string",
    //    ///             "activo": true
    //    ///         }
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve el horario modificado en BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del horario a modificar </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPut("{id}")]
    //    [ActionName("ModificarHorarioMedicos")]
    //    [Authorize]
    //    [ProducesResponseType(typeof(HorariosSalud), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<HorariosSalud>> ModificarHorarioMedicos(int id, [FromBody, Required] HorariosSalud horarioSalud)
    //    {
    //        try
    //        {
    //            if (id != horarioSalud.id) return BadRequest();
    //            //El numero de funcion es: 71
    //            if (await TienePermiso(71))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                string usuarioActual = userData.Split(',')[2];
    //                Dictionary<string, string> parametros = new() {
    //                    { "@id_horario", horarioSalud.id.ToString() },
    //                    { "@hora_ini",horarioSalud.hora_inicio },
    //                    { "@hora_fin",horarioSalud.hora_fin },
    //                    { "@dia",horarioSalud.dia.ToString() },
    //                    { "@cuil_es",horarioSalud.cuil_especialista },
    //                    { "@activo",horarioSalud.activo.ToString() },
    //                    { "@id_usuario_mod",usuarioActual}
    //                };

    //                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_SALUD_Modificar_Horario_Medico", parametros);

    //                //En este caso sino modifica nada es un conflicto en la BD
    //                if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                return Ok(new HorariosSalud(respuesta.Rows[0]));
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR MODIFICANDO HORARIO MEDICO");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite crear horarios medicos
    //    /// </summary>
    //    /// <param name="horarioMedico">El horario que deseamos crear, se envia en el Body</param>
    //    /// <returns>Un horario creado en la base de datos o error</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Salud/CrearHorarioMedico/
    //    ///     BODY:
    //    ///     {
    //    ///         "id": 0,
    //    ///         "hora_inicio": "string",
    //    ///         "hora_fin": "string",
    //    ///         "dia": 0,
    //    ///         "cuil_especialista": "string",
    //    ///         "especialista": "string",
    //    ///         "activo": true,
    //    ///         "especialidad": {
    //    ///             "id": 0,
    //    ///             "nombre": "string",
    //    ///             "descripcion": "string",
    //    ///             "activo": true
    //    ///         }
    //    ///     }
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///         "id": 0,
    //    ///         "hora_inicio": "string",
    //    ///         "hora_fin": "string",
    //    ///         "dia": 0,
    //    ///         "cuil_especialista": "string",
    //    ///         "especialista": "string",
    //    ///         "activo": true,
    //    ///         "especialidad": {
    //    ///             "id": 0,
    //    ///             "nombre": "string",
    //    ///             "descripcion": "string",
    //    ///             "activo": true
    //    ///         }
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="201" >Devuelve el horario creado en la BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El empleado no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPost]
    //    [ActionName("CrearHorarioMedico")]
    //    [Authorize]
    //    [ProducesResponseType(typeof(HorariosSalud), StatusCodes.Status201Created)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<HorariosSalud>> CrearHorarioMedico([FromBody] HorariosSalud horarioMedico)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(70))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {
    //                    string usuarioActual = userData.Split(',')[2];
    //                    Dictionary<string, string> parametros = new() {
    //                        { "@hora_ini",horarioMedico.hora_inicio },
    //                        { "@hora_fin",horarioMedico.hora_fin },
    //                        { "@dia",horarioMedico.dia.ToString() },
    //                        { "@cuil_es",horarioMedico.cuil_especialista },
    //                        { "@activo",horarioMedico.activo.ToString() },
    //                        { "@id_usuario_act",usuarioActual}
    //                    };

    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_SALUD_Crear_Horario_Medico", parametros);
    //                    //En este caso sino crea es un error en la BD
    //                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                    return Created("Horario Creado", new HorariosSalud(respuesta.Rows[0]));
    //                }

    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR CREANDO HORARIO MEDICO");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite eliminar horarios
    //    /// </summary>
    //    /// <param name="id">Es el id del horario a eliminar</param>
    //    /// <returns>Un mensaje de OK o el horario que no se pudo eliminar</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Salud/EliminarHorario/{id}
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///         "Horario Eliminado"
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve OK y un mensaje </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El empleado no genero su JWT</response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y devuelve el horario </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>        
    //    [HttpDelete("{id}")]
    //    [ActionName("EliminarHorario")]
    //    [Authorize]
    //    [ProducesResponseType(StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult> EliminarHorario(int id)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(72))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {
    //                    Dictionary<string, string> parametros = new() {
    //                       { "@id_horario",id.ToString() }
    //                    };
    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_SALUD_Eliminar_Horario_Medico", parametros);

    //                    if (respuesta.Rows.Count > 0)
    //                    {
    //                        if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                        //Si es mayor a 0 significa que no se elimino asi que devolvemos dicho registro
    //                        else return Conflict(new HorariosSAE(respuesta.Rows[0]));
    //                    }
    //                    else return Ok("Horario Eliminado");

    //                }

    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR ELIMINANDO HORARIO");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite registrar la inasistencia de un especialista
    //    /// </summary>
    //    /// <param name="faltaEspecialista">La inasistencia que deseamos crear, se envia en el Body</param>
    //    /// <returns>Una inasistencia creada en la base de datos o error</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Salud/RegistrarFaltaMedica/
    //    ///     BODY:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "cuil": "string",
    //    ///       "fecha_alta": "2024-07-08T20:17:22.790Z",
    //    ///       "observacion": "string"
    //    ///     }
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "cuil": "string",
    //    ///       "fecha_alta": "2024-07-08T20:17:22.790Z",
    //    ///       "observacion": "string"
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="201" >Devuelve la falta registrada y creada en la BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El empleado no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPost]
    //    [ActionName("RegistrarFaltaMedica")]
    //    [Authorize]
    //    [ProducesResponseType(typeof(FaltaEspecialista), StatusCodes.Status201Created)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<FaltaEspecialista>> RegistrarFaltaMedica([FromBody] FaltaEspecialista faltaEspecialista)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(74))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {
    //                    string usuarioActual = userData.Split(',')[2];
    //                    Dictionary<string, string> parametros = new() {
    //                        { "@cuil",faltaEspecialista.cuil },
    //                        { "@fecha_falta",faltaEspecialista.fecha_alta.ToString() },
    //                        { "@observacion",faltaEspecialista.observacion },
    //                        { "@id_usuario_alta",usuarioActual}
    //                    };

    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_SALUD_Registrar_Falta_Especialista", parametros);
    //                    //En este caso sino crea es un error en la BD
    //                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                    return Created("Falta Registrado", new FaltaEspecialista(respuesta.Rows[0]));
    //                }

    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR REGISTRANDO FALTA");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Recupera todos los cursos medicos
    //    /// </summary>
    //    /// <returns>Un listado con todos los cursos</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Salud/ObtenerCursosMedicos/
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///         {
    //    ///           "id": 0,
    //    ///           "nombre_curso": "string",
    //    ///           "nombre_docente": "string",
    //    ///           "fecha_inicio": "2024-07-08T20:20:49.743Z",
    //    ///           "fecha_fin": "2024-07-08T20:20:49.743Z",
    //    ///           "cupo_maximo": 0,
    //    ///           "activo": true
    //    ///         }
    //    ///     ]
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un listado de todos los cursos medicos </response>
    //    /// <response code="204" >No se encontro ningun curso </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet]
    //    [ActionName("ObtenerCursosMedicos")]
    //    [Authorize]
    //    [ProducesResponseType(typeof(IEnumerable<CursosMedicos>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<CursosMedicos>>> ObtenerCursosMedicos()
    //    {
    //        try
    //        {
    //            if (await TienePermiso(78))
    //            {
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_SALUD_Visualizar_Cursos_Medicos");
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<CursosMedicos> listadoCursosMedico = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    CursosMedicos curso = new(row);
    //                    listadoCursosMedico.Add(curso);
    //                }
    //                return Ok(listadoCursosMedico);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR VISUALIZANDO CURSOS MEDICOS");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite la modificacion de un curso medico
    //    /// </summary>
    //    /// <param name="id"> El ID del curso medico a modificar</param>
    //    /// <param name="cursoMedico"> Los datos modificados del curso medico</param>
    //    /// <returns>Un curso medico modificado en BD</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     PUT /api/Salud/ModificarCursoMedicos/{id}
    //    ///     BODY:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "nombre_curso": "string",
    //    ///       "nombre_docente": "string",
    //    ///       "fecha_inicio": "2024-07-08T20:20:49.743Z",
    //    ///       "fecha_fin": "2024-07-08T20:20:49.743Z",
    //    ///       "cupo_maximo": 0,
    //    ///       "activo": true
    //    ///     }
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "nombre_curso": "string",
    //    ///       "nombre_docente": "string",
    //    ///       "fecha_inicio": "2024-07-08T20:20:49.743Z",
    //    ///       "fecha_fin": "2024-07-08T20:20:49.743Z",
    //    ///       "cupo_maximo": 0,
    //    ///       "activo": true
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve el curso medico modificado en BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del curso medico a modificar </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPut("{id}")]
    //    [ActionName("ModificarCursoMedicos")]
    //    [Authorize]
    //    [ProducesResponseType(typeof(CursosMedicos), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<CursosMedicos>> ModificarCursoMedicos(int id, [FromBody, Required] CursosMedicos cursoMedico)
    //    {
    //        try
    //        {
    //            if (id != cursoMedico.id) return BadRequest();
    //            //El numero de funcion es: 71
    //            if (await TienePermiso(76))
    //            {
    //                Dictionary<string, string> parametros = new() {
    //                    { "@id_curso", cursoMedico.id.ToString() },
    //                    { "@fecha_inicio",cursoMedico.fecha_inicio.ToString()  },
    //                    { "@fecha_fin",cursoMedico.fecha_fin.ToString() },
    //                    { "@nombre_curso ",cursoMedico.nombre_curso },
    //                    { "@nombre_docente",cursoMedico.nombre_docente },
    //                    { "@cupo_maximo",cursoMedico.cupo_maximo.ToString()},
    //                    { "@activo",cursoMedico.activo.ToString() }
    //                };

    //                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_SALUD_Modificar_Curso_Medico", parametros);

    //                //En este caso sino modifica nada es un conflicto en la BD
    //                if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                return Ok(new CursosMedicos(respuesta.Rows[0]));
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR MODIFICANDO CURSO MEDICO");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite crear cursos medicos
    //    /// </summary>
    //    /// <param name="cursoMedico">El curso medico que deseamos crear, se envia en el Body</param>
    //    /// <returns>Un curso medico creado en la base de datos o error</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Salud/CrearCursoMedico/
    //    ///     BODY:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "nombre_curso": "string",
    //    ///       "nombre_docente": "string",
    //    ///       "fecha_inicio": "2024-07-08T20:20:49.743Z",
    //    ///       "fecha_fin": "2024-07-08T20:20:49.743Z",
    //    ///       "cupo_maximo": 0,
    //    ///       "activo": true
    //    ///     }
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "nombre_curso": "string",
    //    ///       "nombre_docente": "string",
    //    ///       "fecha_inicio": "2024-07-08T20:20:49.743Z",
    //    ///       "fecha_fin": "2024-07-08T20:20:49.743Z",
    //    ///       "cupo_maximo": 0,
    //    ///       "activo": true
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="201" >Devuelve el curso medico creado en la BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El empleado no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPost]
    //    [ActionName("CrearCursoMedico")]
    //    [Authorize]
    //    [ProducesResponseType(typeof(CursosMedicos), StatusCodes.Status201Created)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<CursosMedicos>> CrearCursoMedico([FromBody] CursosMedicos cursoMedico)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(75))
    //            {
    //                Dictionary<string, string> parametros = new() {

    //                    { "@fecha_inicio",cursoMedico.fecha_inicio.ToString()  },
    //                    { "@fecha_fin",cursoMedico.fecha_fin.ToString() },
    //                    { "@nombre_curso ",cursoMedico.nombre_curso },
    //                    { "@nombre_docente",cursoMedico.nombre_docente },
    //                    { "@cupo_maximo",cursoMedico.cupo_maximo.ToString()},
    //                    { "@activo",cursoMedico.activo.ToString() }
    //                };

    //                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_SALUD_Crear_Curso_Medico", parametros);
    //                //En este caso sino crea es un error en la BD
    //                if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                return Created("Curso Medico creado", new CursosMedicos(respuesta.Rows[0]));

    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR CREANDO CURSO MEDICO");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite eliminar cursos medicos
    //    /// </summary>
    //    /// <param name="id">Es el id del curso medico a eliminar</param>
    //    /// <returns>Un mensaje de OK o el curso medico que no se pudo eliminar</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Salud/EliminarCursoMedico/{id}
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///         "Curso Eliminado"
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve OK y un mensaje </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El empleado no genero su JWT</response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y devuelve el curso </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>        
    //    [HttpDelete("{id}")]
    //    [ActionName("EliminarCursoMedico")]
    //    [Authorize]
    //    [ProducesResponseType(StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult> EliminarCursoMedico(int id)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(77))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {
    //                    Dictionary<string, string> parametros = new() {
    //                       { "@id_curso",id.ToString() }
    //                    };
    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_SALUD_Eliminar_Curso_Medico", parametros);

    //                    if (respuesta.Rows.Count > 0)
    //                    {
    //                        if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                        //Si es mayor a 0 significa que no se elimino asi que devolvemos dicho registro
    //                        else return Conflict(new CursosMedicos(respuesta.Rows[0]));
    //                    }
    //                    else return Ok("Curso Eliminado");

    //                }

    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR ELIMINANDO CURSO");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Recupera todos los turnos medicos
    //    /// </summary>
    //    /// <returns>Un listado de todos los turnos medicos no importa el estado</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Salud/ObtenerTurnosMedicos/
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///         {
    //    ///           "id": 0,
    //    ///           "cuil_medico": "string",
    //    ///           "especialista": "string",
    //    ///           "legajo": "string",
    //    ///           "paciente": "string",
    //    ///           "fecha_solicitud": "2024-07-08T20:31:46.626Z",
    //    ///           "fecha_atencion": "2024-07-08T20:31:46.626Z",
    //    ///           "hora_atencion": "string",
    //    ///           "asunto": "string",
    //    ///           "id_estado_turno": 0,
    //    ///           "estado": "string"
    //    ///         }
    //    ///     ]
    //    /// </remarks>
    //    /// <response code="200" >Un listado de todos los turnos medicos </response>
    //    /// <response code="204" >No se encontro ningun turno medico</response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet]
    //    [ActionName("ObtenerTurnosMedicos")]
    //    [Authorize]
    //    [ProducesResponseType(typeof(IEnumerable<TurnosMedicos>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<TurnosMedicos>>> ObtenerTurnosMedicos()
    //    {
    //        try
    //        {
    //            if (await TienePermiso(83))
    //            {
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_SALUD_Visualizar_Turnos");
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<TurnosMedicos> listadoTurnosMedico = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    TurnosMedicos turno = new(row);
    //                    listadoTurnosMedico.Add(turno);
    //                }
    //                return Ok(listadoTurnosMedico);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR VISUALIZANDO TURNOS MEDICOS");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Recupera los turnos pendientes
    //    /// </summary>
    //    /// <returns>Un listado con todos los turnos medicos pendientes</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Salud/ObtenerTurnosMedicosPendientes/
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///         {
    //    ///           "id": 0,
    //    ///           "cuil_medico": "string",
    //    ///           "especialista": "string",
    //    ///           "legajo": "string",
    //    ///           "paciente": "string",
    //    ///           "fecha_solicitud": "2024-07-08T20:31:46.626Z",
    //    ///           "fecha_atencion": "2024-07-08T20:31:46.626Z",
    //    ///           "hora_atencion": "string",
    //    ///           "asunto": "string",
    //    ///           "id_estado_turno": 0,
    //    ///           "estado": "string"
    //    ///         }
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un listado con los turnos medicos pendientes </response>
    //    /// <response code="204" >No se encontro ningun turno pendiente</response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet]
    //    [ActionName("ObtenerTurnosMedicosPendientes")]
    //    [Authorize]
    //    [ProducesResponseType(typeof(IEnumerable<TurnosMedicos>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<TurnosMedicos>>> ObtenerTurnosMedicosPendientes()
    //    {
    //        try
    //        {
    //            if (await TienePermiso(83))
    //            {
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_SALUD_Visualizar_Turnos_Pendientes");
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<TurnosMedicos> listadoTurnosMedico = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    TurnosMedicos turno = new(row);
    //                    listadoTurnosMedico.Add(turno);
    //                }
    //                return Ok(listadoTurnosMedico);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR VISUALIZANDO TURNOS MEDICOS PENDIENTES");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Un listado de turnos asignados
    //    /// </summary>
    //    /// <returns>Un listado de turnos que esten en estado asignado</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Salud/ObtenerTurnosMedicosAsignados/
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///         {
    //    ///           "id": 0,
    //    ///           "cuil_medico": "string",
    //    ///           "especialista": "string",
    //    ///           "legajo": "string",
    //    ///           "paciente": "string",
    //    ///           "fecha_solicitud": "2024-07-08T20:31:46.626Z",
    //    ///           "fecha_atencion": "2024-07-08T20:31:46.626Z",
    //    ///           "hora_atencion": "string",
    //    ///           "asunto": "string",
    //    ///           "id_estado_turno": 0,
    //    ///           "estado": "string"
    //    ///         }
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un listado con los turnos medicos asignados </response>
    //    /// <response code="204" >No se encontro ningun turno asignado</response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet]
    //    [ActionName("ObtenerTurnosMedicosAsignados")]
    //    [Authorize]
    //    [ProducesResponseType(typeof(IEnumerable<TurnosMedicos>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<TurnosMedicos>>> ObtenerTurnosMedicosAsignados()
    //    {
    //        try
    //        {
    //            if (await TienePermiso(83))
    //            {
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_SALUD_Visualizar_Turnos_Asignados");
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<TurnosMedicos> listadoTurnosMedico = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    TurnosMedicos turno = new(row);
    //                    listadoTurnosMedico.Add(turno);
    //                }
    //                return Ok(listadoTurnosMedico);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR VISUALIZANDO TURNOS MEDICOS ASIGNADOS");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Un listado de turnos en curso
    //    /// </summary>
    //    /// <returns>Un listado de turnos que esten en estado en curso</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Salud/ObtenerTurnosMedicosEnCurso/
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///         {
    //    ///           "id": 0,
    //    ///           "cuil_medico": "string",
    //    ///           "especialista": "string",
    //    ///           "legajo": "string",
    //    ///           "paciente": "string",
    //    ///           "fecha_solicitud": "2024-07-08T20:31:46.626Z",
    //    ///           "fecha_atencion": "2024-07-08T20:31:46.626Z",
    //    ///           "hora_atencion": "string",
    //    ///           "asunto": "string",
    //    ///           "id_estado_turno": 0,
    //    ///           "estado": "string"
    //    ///         }
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un listado con los turnos medicos en curso </response>
    //    /// <response code="204" >No se encontro ningun turno en curso</response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet]
    //    [ActionName("ObtenerTurnosMedicosEnCurso")]
    //    [Authorize]
    //    [ProducesResponseType(typeof(IEnumerable<TurnosMedicos>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<TurnosMedicos>>> ObtenerTurnosMedicosEnCurso()
    //    {
    //        try
    //        {
    //            if (await TienePermiso(83))
    //            {
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_SALUD_Visualizar_Turnos_En_Curso");
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<TurnosMedicos> listadoTurnosMedico = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    TurnosMedicos turno = new(row);
    //                    listadoTurnosMedico.Add(turno);
    //                }
    //                return Ok(listadoTurnosMedico);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR VISUALIZANDO TURNOS MEDICOS EN CURSO");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Busca turnos por ID
    //    /// </summary>
    //    /// <param name="id">Es el identificacor del turno en la BD</param>
    //    /// <returns>Un turno que coincida con ese ID</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Salud/ObtenerTurnoXId/{id}
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "cuil_medico": "string",
    //    ///       "especialista": "string",
    //    ///       "legajo": "string",
    //    ///       "paciente": "string",
    //    ///       "fecha_solicitud": "2024-07-08T20:31:46.626Z",
    //    ///       "fecha_atencion": "2024-07-08T20:31:46.626Z",
    //    ///       "hora_atencion": "string",
    //    ///       "asunto": "string",
    //    ///       "id_estado_turno": 0,
    //    ///       "estado": "string"
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un turno con este ID </response>
    //    /// <response code="204" >No se encontro ningun turno </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet("{id}")]
    //    [ActionName("ObtenerTurnoXId")]
    //    [Authorize]
    //    [ProducesResponseType(typeof(TurnosMedicos), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<TurnosMedicos>> ObtenerTurnoXId(int id)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(83))
    //            {
    //                Dictionary<string, string> parametros = new()
    //                {
    //                    { "@id_turno", id.ToString() }
    //                };
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_SALUD_Buscar_Turnos_Id", parametros);
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                else return Ok(new TurnosMedicos(respuesta.Rows[0]));
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR BUSCANDO TURNO MEDICO");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Un listado de turnos asociados al legajo
    //    /// </summary>
    //    /// <param name="legajo">Es el identificacor del estudiante/no docente en la BD</param>
    //    /// <returns>Un listados de turnos con este legajo</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Salud/ObtenerTurnosXLegajo/{legajo}
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///         {
    //    ///           "id": 0,
    //    ///           "cuil_medico": "string",
    //    ///           "especialista": "string",
    //    ///           "legajo": "string",
    //    ///           "paciente": "string",
    //    ///           "fecha_solicitud": "2024-07-08T20:31:46.626Z",
    //    ///           "fecha_atencion": "2024-07-08T20:31:46.626Z",
    //    ///           "hora_atencion": "string",
    //    ///           "asunto": "string",
    //    ///           "id_estado_turno": 0,
    //    ///           "estado": "string"
    //    ///         }
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un listado de turnos de esta persona</response>
    //    /// <response code="204" >No se encontro ningun turno </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet("{legajo}")]
    //    [ActionName("ObtenerTurnosXLegajo")]
    //    [Authorize]
    //    [ProducesResponseType(typeof(IEnumerable<TurnosMedicos>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<TurnosMedicos>>> ObtenerTurnosXLegajo(string legajo)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(83))
    //            {
    //                Dictionary<string, string> parametros = new()
    //                {
    //                    { "@legajo", legajo.ToString() }
    //                };
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_SALUD_Buscar_Turnos_Legajo", parametros);
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<TurnosMedicos> listadoTurnosMedico = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    TurnosMedicos turno = new(row);
    //                    listadoTurnosMedico.Add(turno);
    //                }
    //                return Ok(listadoTurnosMedico);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR BUSCANDO TURNO MEDICO");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite la modificacion de un turno
    //    /// </summary>
    //    /// <param name="id_turno"> El ID del turno a modificar</param>
    //    /// <param name="turno"> Los datos modificados del turno</param>
    //    /// <returns>Un turno modificado en BD</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     PUT /api/Salud/ModificarTurnoMedico/{id_turno}
    //    ///     BODY:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "cuil_medico": "string",
    //    ///       "especialista": "string",
    //    ///       "legajo": "string",
    //    ///       "paciente": "string",
    //    ///       "fecha_solicitud": "2024-07-08T20:31:46.626Z",
    //    ///       "fecha_atencion": "2024-07-08T20:31:46.626Z",
    //    ///       "hora_atencion": "string",
    //    ///       "asunto": "string",
    //    ///       "id_estado_turno": 0,
    //    ///       "estado": "string"
    //    ///     }
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "cuil_medico": "string",
    //    ///       "especialista": "string",
    //    ///       "legajo": "string",
    //    ///       "paciente": "string",
    //    ///       "fecha_solicitud": "2024-07-08T20:31:46.626Z",
    //    ///       "fecha_atencion": "2024-07-08T20:31:46.626Z",
    //    ///       "hora_atencion": "string",
    //    ///       "asunto": "string",
    //    ///       "id_estado_turno": 0,
    //    ///       "estado": "string"
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve el turno medico modificado en BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del turno medico a modificar </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPut("{id_turno}")]
    //    [ActionName("ModificarTurnoMedico")]
    //    [Authorize]
    //    [ProducesResponseType(typeof(TurnosMedicos), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<TurnosMedicos>> ModificarTurnoMedico(int id_turno, [FromBody, Required] TurnosMedicos turno)
    //    {
    //        try
    //        {
    //            if (id_turno != turno.id) return BadRequest();
    //            //El numero de funcion es: 68
    //            if (await TienePermiso(81))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                string usuarioActual = userData.Split(',')[2];
    //                Dictionary<string, string> parametros = new() {
    //                    { "@id_turno",turno.id.ToString() },
    //                    { "@cuil_medico",turno.cuil_medico??"NULL" },
    //                    { "@legajo", turno.legajo },
    //                    { "@id_estado_turno",turno.id_estado_turno.ToString() },
    //                    { "@fecha_solicitud",turno.fecha_solicitud.ToString() },
    //                    { "@fecha_atencion",turno.fecha_atencion.ToString()??"NULL" },
    //                    { "@hora_atencion",turno.hora_atencion??"NULL" },
    //                    { "@asunto",turno.asunto},
    //                    { "@id_usuario_mod",usuarioActual}
    //                };

    //                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_SALUD_Modificar_Turno_Medico", parametros);

    //                //En este caso sino modifica nada es un conflicto en la BD
    //                if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                return Ok(new TurnosMedicos(respuesta.Rows[0]));
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR MODIFICANDO EMPLEADO");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite crear turnos medicos
    //    /// </summary>
    //    /// <param name="turno">El turno que deseamos crear, se envia en el Body</param>
    //    /// <returns>Un turno creado en la base de datos o error</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Salud/CrearTurnoMedico/
    //    ///     BODY:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "cuil_medico": "string",
    //    ///       "especialista": "string",
    //    ///       "legajo": "string",
    //    ///       "paciente": "string",
    //    ///       "fecha_solicitud": "2024-07-08T20:31:46.626Z",
    //    ///       "fecha_atencion": "2024-07-08T20:31:46.626Z",
    //    ///       "hora_atencion": "string",
    //    ///       "asunto": "string",
    //    ///       "id_estado_turno": 0,
    //    ///       "estado": "string"
    //    ///     }
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///       "id": 0,
    //    ///       "cuil_medico": "string",
    //    ///       "especialista": "string",
    //    ///       "legajo": "string",
    //    ///       "paciente": "string",
    //    ///       "fecha_solicitud": "2024-07-08T20:31:46.626Z",
    //    ///       "fecha_atencion": "2024-07-08T20:31:46.626Z",
    //    ///       "hora_atencion": "string",
    //    ///       "asunto": "string",
    //    ///       "id_estado_turno": 0,
    //    ///       "estado": "string"
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="201" >Devuelve el turno creado en la BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El empleado no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPost]
    //    [ActionName("CrearTurnoMedico")]
    //    [Authorize]
    //    [ProducesResponseType(typeof(TurnosMedicos), StatusCodes.Status201Created)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<TurnosMedicos>> CrearTurnoMedico([FromBody] TurnosMedicos turno)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(80))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                string usuarioActual = userData.Split(',')[2];
    //                Dictionary<string, string> parametros = new() {
    //                    { "@legajo", turno.legajo },
    //                    { "@fecha_solicitud",turno.fecha_solicitud.ToString() },
    //                    { "@asunto",turno.asunto},
    //                    { "@id_usuario_alta",usuarioActual}
    //                };

    //                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_SALUD_Crear_Turno_Medico", parametros);
    //                //En este caso sino crea es un error en la BD
    //                if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                return Created("Turno Medico creado", new TurnosMedicos(respuesta.Rows[0]));

    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR CREANDO TURNO MEDICO");
    //            return BadRequest();
    //        }
    //    }

    //    /// <summary>
    //    /// Permite validar si el perfil tiene permiso en la BD para ejecutar este endpoint
    //    /// </summary>
    //    /// <param name="id_funcion">Es la funcion que queremos validar </param>
    //    /// <returns> True = Tiene permisos || False = No tiene permisos </returns>
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
