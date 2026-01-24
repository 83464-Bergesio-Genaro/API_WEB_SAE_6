
using API_WEB_SAE_6.Adapters;
using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Claims;
using System.Security.Cryptography;

namespace API_WEB_SAE_6.Controllers
{
    ///// <summary>
    ///// Es el controlador para empleados y horarios de empleados
    ///// </summary>
    //[EnableCors("CorsRules")]
    //[Route("api/[controller]/[action]")]
    //[Authorize]
    //[ApiController]
    //public class EmpleadosController : Controller
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
    //    public EmpleadosController(IConfiguration config)
    //    {
    //        _config = config;
    //    }
    //    /// <summary>
    //    /// Recupera todos los empleados
    //    /// </summary>
    //    /// <returns>Un listado de todos los empleados</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Empleados/ObtenerEmpleados/
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///       {
    //    ///          "id": 0,
    //    ///          "legajo": "string",
    //    ///          "nombre_empleado": "string",
    //    ///          "activo": true
    //    ///       }
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un listado completo de empleados </response>
    //    /// <response code="204" >No se encontro ningun empleado </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet]
    //    [ActionName("ObtenerEmpleados")]
    //    [ProducesResponseType(typeof(IEnumerable<EmpleadosSAE>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<EmpleadosSAE>>> ObtenerEmpleados()
    //    {
    //        try
    //        {
    //            //El numero de funcion es: 45
    //            if (await TienePermiso(45))
    //            {
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_EMPLEADOS_Listar_empleados_completo");
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<EmpleadosSAE> listadoEmpleadosCompleto = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    EmpleadosSAE empleado = new(row);
    //                    listadoEmpleadosCompleto.Add(empleado);
    //                }
    //                return Ok(listadoEmpleadosCompleto);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR LISTANDO EMPLEADOS");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Recupera solo empleados activos
    //    /// </summary>
    //    /// <returns>Un listado con empleados actuales</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Empleados/ObtenerEmpleadosActivos/
    //    ///     
    //    ///     [
    //    ///       {
    //    ///          "id": 0,
    //    ///          "legajo": "string",
    //    ///          "nombre_empleado": "string",
    //    ///          "activo": true
    //    ///       }
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un listado de empleados actuales </response>
    //    /// <response code="204" >No se encontro ningun empleado </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet]
    //    [ActionName("ObtenerEmpleadosActivos")]
    //    [ProducesResponseType(typeof(IEnumerable<EmpleadosSAE>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<EmpleadosSAE>>> ObtenerEmpleadosActivos()
    //    {
    //        try
    //        {
    //            //El numero de funcion es: 45
    //            if (await TienePermiso(45))
    //            {
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_EMPLEADOS_Listar_empleados_activos");
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<EmpleadosSAE> listadoEmpleadosCompleto = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    EmpleadosSAE empleado = new(row);
    //                    listadoEmpleadosCompleto.Add(empleado);
    //                }
    //                return Ok(listadoEmpleadosCompleto);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR LISTANDO EMPLEADOS ACTIVOS");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Busca empleados por ID
    //    /// </summary>
    //    /// <param name="id">Es el identificacor del empleado en la BD</param>
    //    /// <returns>Un empleado que coincida con ese ID</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Empleados/ObtenerEmpleadosXId/{id}
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///        "id": 0,
    //    ///        "legajo": "string",
    //    ///        "nombre_empleado": "string",
    //    ///        "activo": true
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un empleado con este ID </response>
    //    /// <response code="204" >No se encontro ningun empleado </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet("{id}")]
    //    [ActionName("ObtenerEmpleadosXId")]
    //    [ProducesResponseType(typeof(EmpleadosSAE), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<EmpleadosSAE>> ObtenerEmpleadosXId(int id)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(45))
    //            {
    //                Dictionary<string, string> parametros = new()
    //                {
    //                    { "@id_empleado", id.ToString() }
    //                };
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_EMPLEADOS_Buscar_Empleados_id", parametros);
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                else return Ok(new EmpleadosSAE(respuesta.Rows[0]));
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR BUSCANDO EMPLEADO");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Busca empleados por legajo
    //    /// </summary>
    //    /// <param name="legajo">Es un legajo tanto de alumno, docente o no docente en la UTN FRC </param>
    //    /// <returns>Un empleado que coincida con ese legajo</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Empleados/ObtenerEmpleadosXLegajo/{legajo}
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///        "id": 0,
    //    ///        "legajo": "string",
    //    ///        "nombre_empleado": "string",
    //    ///        "activo": true
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un empleado con este legajo </response>
    //    /// <response code="204" >No se encontro ningun empleado </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet("{legajo}")]
    //    [ActionName("ObtenerEmpleadosXLegajo")]
    //    [ProducesResponseType(typeof(EmpleadosSAE), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<EmpleadosSAE>> ObtenerEmpleadosXLegajo(string legajo)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(45))
    //            {
    //                Dictionary<string, string> parametros = new()
    //                {
    //                    { "@legajo",legajo }
    //                };
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_EMPLEADOS_Buscar_Empleados_legajo", parametros);
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                else return Ok(new EmpleadosSAE(respuesta.Rows[0]));
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR BUSCANDO EMPLEADO POR LEGAJO");
    //            return BadRequest();
    //        }
    //    }

    //    /// <summary>
    //    /// Permite la modificacion de un empleado
    //    /// </summary>
    //    /// <param name="id"> El ID del empleado a modificar</param>
    //    /// <param name="empleado"> Los datos modificados del empleado</param>
    //    /// <returns>Un empleado modificado en BD</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     PUT /api/Empleados/ModificarEmpleado/{id}
    //    ///     BODY:
    //    ///     {
    //    ///         "id": 0,
    //    ///         "legajo": "string",
    //    ///         "activo": true,
    //    ///     }
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///        "id": 0,
    //    ///        "legajo": "string",
    //    ///        "nombre_empleado": "string",
    //    ///        "activo": true
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve el empleado modificado en BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del usuario a modificar </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPut("{id}")]
    //    [ActionName("ModificarEmpleado")]
    //    [ProducesResponseType(typeof(EmpleadosSAE), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<EmpleadosSAE>> ModificarEmpleado(int id, [FromBody, Required] EmpleadosSAE empleado)
    //    {
    //        try
    //        {
    //            if (id != empleado.id) return BadRequest();
    //            //El numero de funcion es: 12
    //            if (await TienePermiso(44))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                string usuarioActual = userData.Split(',')[2];
    //                Dictionary<string, string> parametros = new() {

    //                    { "@id_empleado",empleado.id.ToString() },
    //                    { "@legajo", empleado.legajo },
    //                    { "@activo",empleado.activo.ToString() },
    //                    { "@id_usuario_mod",usuarioActual}
    //                };

    //                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_EMPLEADOS_Modificar_Empleado", parametros);

    //                //En este caso sino modifica nada es un conflicto en la BD
    //                if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                return Ok(new EmpleadosSAE(respuesta.Rows[0]));
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
    //    /// Permite crear empleados
    //    /// </summary>
    //    /// <param name="empleado">El empleado que deseamos crear, se envia en el Body</param>
    //    /// <returns>Un empleado creado en la base de datos o error</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Empleados/CrearEmpleado/
    //    ///     BODY:
    //    ///     {
    //    ///         "id": 0,
    //    ///         "legajo": "string",
    //    ///         "activo": true,
    //    ///     }
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///        "id": 0,
    //    ///        "legajo": "string",
    //    ///        "nombre_empleado": "string",
    //    ///        "activo": true
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="201" >Devuelve el empleado creado en la BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El empleado no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPost]
    //    [ActionName("CrearEmpleado")]
    //    [ProducesResponseType(typeof(EmpleadosSAE), StatusCodes.Status201Created)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<EmpleadosSAE>> CrearEmpleado([FromBody] EmpleadosSAE empleado)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(43))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {
    //                    string usuarioActual = userData.Split(',')[2];
    //                    Dictionary<string, string> parametros = new() {
    //                        { "@legajo", empleado.legajo },
    //                        {"@id_usuario_alta",usuarioActual.ToString() }
    //                    };

    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_EMPLEADOS_Crear_Empleado", parametros);
    //                    //En este caso sino crea es un error en la BD
    //                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                    return Created("Empleado Creado", new EmpleadosSAE(respuesta.Rows[0]));
    //                }

    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR CREANDO EMPLEADO");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Recupera todos los horarios registrados
    //    /// </summary>
    //    /// <returns>Un listado de todos los horarios</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Empleados/ObtenerHorarios/
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///       {
    //    ///          "id": 0,
    //    ///          "hora_inicio": "HH:MM:SS",
    //    ///          "hora_fin":    "HH:MM:SS",
    //    ///          "dia": true,
    //    ///          "nombre_empleado_atencion": "string",
    //    ///          "activo": true
    //    ///       }
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un listado completo de los horarios </response>
    //    /// <response code="204" >No se encontro ningun horario </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet]
    //    [ActionName("ObtenerHorarios")]
    //    [ProducesResponseType(typeof(IEnumerable<HorariosSAE>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<HorariosSAE>>> ObtenerHorarios()
    //    {
    //        try
    //        {
    //            //El numero de funcion es: 50
    //            if (await TienePermiso(50))
    //            {
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_EMPLEADOS_Listar_horarios_completo");
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<HorariosSAE> listadoHorarioCompleto = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    HorariosSAE empleado = new(row);
    //                    listadoHorarioCompleto.Add(empleado);
    //                }
    //                return Ok(listadoHorarioCompleto);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR LISTANDO HORARIOS");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Recupera todos los horarios activos
    //    /// </summary>
    //    /// <returns>Un listado de los horarios activos</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Empleados/ObtenerHorariosActivos/
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///       {
    //    ///          "id": 0,
    //    ///          "hora_inicio": "HH:MM:SS",
    //    ///          "hora_fin":    "HH:MM:SS",
    //    ///          "dia": true,
    //    ///          "nombre_empleado_atencion": "string",
    //    ///          "activo": true
    //    ///       }
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un listado de los horarios activos </response>
    //    /// <response code="204" >No se encontro ningun horario activo </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet]
    //    [ActionName("ObtenerHorariosActivos")]
    //    [ProducesResponseType(typeof(IEnumerable<HorariosSAE>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<HorariosSAE>>> ObtenerHorariosActivos()
    //    {
    //        try
    //        {
    //            //El numero de funcion es: 50
    //            if (await TienePermiso(50))
    //            {
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_EMPLEADOS_Listar_horarios_activos");
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<HorariosSAE> listadoHorarioCompleto = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    HorariosSAE empleado = new(row);
    //                    listadoHorarioCompleto.Add(empleado);
    //                }
    //                return Ok(listadoHorarioCompleto);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR LISTANDO HORARIOS ACTIVOS");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Busca horario por su ID
    //    /// </summary>
    //    /// <param name="id_horario">Es el identificador del horario en la BD</param>
    //    /// <returns>Un horario encontrado por ID</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Empleados/ObtenerHorariosXId/{id_horario}
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///        "id": 0,
    //    ///        "hora_inicio": "HH:MM:SS",
    //    ///        "hora_fin":    "HH:MM:SS",
    //    ///        "dia": true,
    //    ///        "nombre_empleado_atencion": "string",
    //    ///        "activo": true
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un horario con este ID </response>
    //    /// <response code="204" >No se encontro ningun horario </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet("{id_horario}")]
    //    [ActionName("ObtenerHorariosXId")]
    //    [ProducesResponseType(typeof(HorariosSAE), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<HorariosSAE>> ObtenerHorariosXId(int id_horario)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(50))
    //            {
    //                Dictionary<string, string> parametros = new()
    //                {
    //                    { "@id_horario", id_horario.ToString() }
    //                };
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_EMPLEADOS_Buscar_Horario_ID", parametros);
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                return Ok(new HorariosSAE(respuesta.Rows[0]));
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR BUSCANDO HORARIOS PARA EL EMPLEADO");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Busca horarios por empleado
    //    /// </summary>
    //    /// <param name="id_empleado">Es el identificador del empleado en la BD</param>
    //    /// <returns>Un listado de horarios para ese empleado</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Empleados/ObtenerHorariosXEmpleado/{id_empleado}
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///       {
    //    ///          "id": 0,
    //    ///          "hora_inicio": "HH:MM:SS",
    //    ///          "hora_fin":    "HH:MM:SS",
    //    ///          "dia": true,
    //    ///          "nombre_empleado_atencion": "string",
    //    ///          "activo": true
    //    ///       }
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un listado de horarios del empleado </response>
    //    /// <response code="204" >No se encontro ningun horario </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet("{id_empleado}")]
    //    [ActionName("ObtenerHorariosXEmpleado")]
    //    [ProducesResponseType(typeof(IEnumerable<HorariosSAE>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<HorariosSAE>>> ObtenerHorariosXEmpleado(int id_empleado)
    //    {
    //        try
    //        {
    //            //Este lo puede visualizar los empleados
    //            if (await TienePermiso(49))
    //            {
    //                Dictionary<string, string> parametros = new()
    //                {
    //                    { "@id_empleado", id_empleado.ToString() }
    //                };
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_EMPLEADOS_Buscar_Horario_Empleado", parametros);
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<HorariosSAE> listadoHorarioCompleto = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    HorariosSAE horario = new(row);
    //                    listadoHorarioCompleto.Add(horario);
    //                }
    //                return Ok(listadoHorarioCompleto);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR BUSCANDO HORARIOS PARA EL EMPLEADO");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite la modificacion de un horario
    //    /// </summary>
    //    /// <param name="id"> El ID del horario a modificar</param>
    //    /// <param name="horario"> Los datos modificados del horario</param>
    //    /// <returns>Un horario modificado en BD</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     PUT /api/Empleados/ModificarHorario/{id}
    //    ///     BODY:
    //    ///     {
    //    ///     "id": 0,
    //    ///     "hora_inicio": "HH:MM:SS",
    //    ///     "hora_fin": "HH:MM:SS",
    //    ///     "dia": true,
    //    ///     "activo": true
    //    ///     }
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///     "id": 0,
    //    ///     "hora_inicio": "HH:MM:SS",
    //    ///     "hora_fin": "HH:MM:SS",
    //    ///     "dia": true,
    //    ///     "nombre_empleado_atencion": "string",
    //    ///     "activo": true
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve el horario modificado en BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del usuario a modificar </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPut("{id}")]
    //    [ActionName("ModificarHorario")]
    //    [ProducesResponseType(typeof(HorariosSAE), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<HorariosSAE>> ModificarHorario(int id, [FromBody, Required] HorariosSAE horario)
    //    {
    //        try
    //        {
    //            if (id != horario.id) return BadRequest();
    //            //El numero de funcion es: 47
    //            if (await TienePermiso(47))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                string usuarioActual = userData.Split(',')[2];
    //                Dictionary<string, string> parametros = new() {
    //                    { "@id_horario",horario.id.ToString() },
    //                    { "@hora_inicio", horario.hora_inicio },
    //                    { "@hora_fin", horario.hora_fin },
    //                    { "@dia", horario.dia.ToString() },
    //                    { "@activo",horario.activo.ToString() },
    //                    { "@id_empleado",horario.id_empleado.ToString() },
    //                    { "@id_usuario_mod",usuarioActual}
    //                };
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_EMPLEADOS_Modificar_Horario", parametros);

    //                //En este caso sino modifica nada es un conflicto en la BD
    //                if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                return Ok(new HorariosSAE(respuesta.Rows[0]));
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR MODIFICANDO HORARIO");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite crear horarios
    //    /// </summary>
    //    /// <param name="horario">El horario que deseamos crear, se envia en el Body</param>
    //    /// <returns>Un horario creado en la base de datos o error</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Empleados/CrearHorario/
    //    ///     BODY:
    //    ///     {
    //    ///         "id": 0,
    //    ///         "hora_inicio": "HH:MM:SS",
    //    ///         "hora_fin": "HH:MM:SS",
    //    ///         "dia": true,
    //    ///         "activo": true
    //    ///     }
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///         "id": 0,
    //    ///         "hora_inicio": "HH:MM:SS",
    //    ///         "hora_fin": "HH:MM:SS",
    //    ///         "dia": true,
    //    ///         "nombre_empleado_atencion": "string",
    //    ///         "activo": true
    //    ///     }
    //    ///     
    //    /// </remarks>
    //    /// <response code="201" >Devuelve el horario creado en la BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El empleado no genero su JWT</response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPost]
    //    [ActionName("CrearHorario")]
    //    [ProducesResponseType(typeof(HorariosSAE), StatusCodes.Status201Created)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<HorariosSAE>> CrearHorario(HorariosSAE horario)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(46))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {
    //                    string usuarioActual = userData.Split(',')[2];
    //                    Dictionary<string, string> parametros = new() {
    //                        { "@hora_inicio", horario.hora_inicio },
    //                        { "@hora_fin", horario.hora_fin },
    //                        { "@dia", horario.dia.ToString() },
    //                        { "@activo",horario.activo.ToString() },
    //                        { "@id_empleado",horario.id_empleado.ToString() },
    //                        { "@id_usuario_alta",usuarioActual.ToString() }
    //                    };
    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_EMPLEADOS_Crear_Horario", parametros);
    //                    //En este caso sino crea es un error en la BD
    //                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                    return Created("Horario Creado", new HorariosSAE(respuesta.Rows[0]));
    //                }

    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR CREANDO HORARIO");
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
    //    ///     POST /api/Empleados/EliminarHorario/{id}
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
    //            if (await TienePermiso(48))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {
    //                    Dictionary<string, string> parametros = new() {
    //                       { "@id_horario",id.ToString() }
    //                    };
    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_EMPLEADOS_Eliminar_Horario", parametros);

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
    //    /// Recupera todo el Linktree
    //    /// </summary>
    //    /// <returns>Un listado de todos los items</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Empleados/ObtenerLinktree/
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///       {
    //    ///         "id": 0,
    //    ///         "titulo": "string",
    //    ///         "id_index_ico": 0,
    //    ///         "hipervinculo": "string",
    //    ///         "contador_clicks": 0
    //    ///       }
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Devuelve un listado completo de empleados </response>
    //    /// <response code="204" >No se encontro ningun empleado </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpGet]
    //    [ActionName("ObtenerLinktree")]
    //    [ProducesResponseType(typeof(IEnumerable<ItemLinktree>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<IEnumerable<ItemLinktree>>> ObtenerLinktree()
    //    {
    //        try
    //        {
    //            //El numero de funcion es: 140
    //            if (await TienePermiso(140))
    //            {
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteView(_config, "MODULO_EMPLEADOS_Listar_Linktree");
    //                if (respuesta.Rows.Count == 0) return NoContent();
    //                if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();

    //                List<ItemLinktree> linktree = new();
    //                foreach (DataRow row in respuesta.Rows)
    //                {
    //                    ItemLinktree item = new(row);
    //                    linktree.Add(item);
    //                }
    //                return Ok(linktree);
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR LISTANDO EMPLEADOS");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Cuenta una visualizacion en el Item
    //    /// </summary>
    //    /// <returns>Un mensaje de exito</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     GET /api/Empleados/ContarVisualizacionItem/{id}
    //    ///     
    //    ///     RESPONSE:
    //    ///     [
    //    ///       {
    //    ///         Visualizado
    //    ///       }
    //    ///     ]
    //    ///     
    //    /// </remarks>
    //    /// <response code="200" >Un mensaje de exito </response>
    //    /// <response code="400" >Ocurre un error en la contabilizacion </response>
    //    /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>        
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPut("{id}")]
    //    [ActionName("ContarVisualizacionItem")]
    //    [ProducesResponseType(StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult> ContarVisualizacionItem(int id)
    //    {
    //        try
    //        {
    //            //El numero de funcion es: 140
    //            if (await TienePermiso(140))
    //            {
    //                Dictionary<string, string> parametros = new() {
    //                       { "@id_item_linktree",id.ToString() }
    //                };
    //                DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_EMPLEADOS_Visualizacion_Item_Linktree", parametros);

    //                if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR" || respuesta.Rows[0][0].ToString() != "VISUALIZADO") return Conflict();
    //                else return Ok("Visualizado");
    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR LISTANDO EMPLEADOS");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite crear items del linktree
    //    /// </summary>
    //    /// <param name="item">El item que deseamos crear, se envia en el Body</param>
    //    /// <returns>Un item creado en la base de datos o error</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Empleados/CrearItemLinkTree/
    //    ///     BODY:
    //    ///       {
    //    ///         "id": 0,
    //    ///         "titulo": "string",
    //    ///         "id_index_ico": 0,
    //    ///         "hipervinculo": "string",
    //    ///       }
    //    ///     
    //    ///     RESPONSE:
    //    ///       {
    //    ///         "id": 0,
    //    ///         "titulo": "string",
    //    ///         "id_index_ico": 0,
    //    ///         "hipervinculo": "string",
    //    ///         "contador_clicks": 0
    //    ///       }
    //    ///     
    //    /// </remarks>
    //    /// <response code="201" >Devuelve el item creado en la BD </response>
    //    /// <response code="400" >Ocurre un error en la consulta </response>
    //    /// <response code="401" >El empleado no genero su JWT</response>
    //    /// <response code="403" >Su perfil no cuenta con este permiso</response>
    //    /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
    //    /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
    //    [HttpPost]
    //    [ActionName("CrearItemLinkTree")]
    //    [ProducesResponseType(typeof(ItemLinktree), StatusCodes.Status201Created)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult<ItemLinktree>> CrearItemLinkTree(ItemLinktree item)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(138))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {
    //                    string usuarioActual = userData.Split(',')[2];
    //                    Dictionary<string, string> parametros = new() {
    //                        { "@titulo",item.titulo },
    //                        { "@id_index_ico", item.id_index_ico.ToString() },
    //                        { "@url", item.hipervinculo},
    //                    };
    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_EMPLEADOS_Crear_Item_Linktree", parametros);
    //                    //En este caso sino crea es un error en la BD
    //                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                    return Created("Item Creado", new ItemLinktree(respuesta.Rows[0]));
    //                }

    //            }
    //            else return Forbid();
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.RegistrarERROR(ex, "ERROR CREANDO ITEM LINKTREE");
    //            return BadRequest();
    //        }
    //    }
    //    /// <summary>
    //    /// Permite eliminar items del linktree
    //    /// </summary>
    //    /// <param name="id">Es el id del item a eliminar</param>
    //    /// <returns>Un mensaje de OK o el item que no se pudo eliminar</returns>
    //    /// <remarks>
    //    /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
    //    ///  
    //    /// Ejemplo de uso:
    //    /// 
    //    ///     POST /api/Empleados/EliminarItem/{id}
    //    ///     
    //    ///     RESPONSE:
    //    ///     {
    //    ///         "Item Eliminado"
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
    //    [ActionName("EliminarItem")]
    //    [ProducesResponseType(StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    //    [ProducesResponseType(StatusCodes.Status409Conflict)]
    //    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //    public async Task<ActionResult> EliminarItem(int id)
    //    {
    //        try
    //        {
    //            if (await TienePermiso(139))
    //            {
    //                string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
    //                if (userData == null || userData == "NO DATA") return Unauthorized();
    //                else
    //                {
    //                    Dictionary<string, string> parametros = new() {
    //                       { "@id_item_linktree",id.ToString() }
    //                    };
    //                    DataTable respuesta = GeneralAdapterSQL.ExecuteStoredProcedure(_config, "MODULO_EMPLEADOS_Eliminar_Item_Linktree", parametros);

    //                    if (respuesta.Rows.Count > 0)
    //                    {
    //                        if (respuesta.Rows[0][0].ToString() == "ERROR") return Conflict();
    //                        //Si es mayor a 0 significa que no se elimino asi que devolvemos dicho registro
    //                        else return Conflict(new ItemLinktree(respuesta.Rows[0]));
    //                    }
    //                    else return Ok("Item Eliminado");

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
