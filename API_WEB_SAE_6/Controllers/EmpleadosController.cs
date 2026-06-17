using API_WEB_SAE_6.Adapters;
using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models.Empleados;
using API_WEB_SAE_6.Models.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Claims;
using System.Security.Cryptography;

namespace API_WEB_SAE_6.Controllers
{
    /// <summary>
    /// Es el controlador para empleados y horarios de empleados
    /// </summary>
    [EnableCors("CorsRules")]
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class EmpleadosController : Controller
    {
        /// <summary>
        /// Es el adaptador de usuarios para consultar los permisos
        /// </summary>
        private UsuarioAdapter UserAdapter = new();
        /// <summary>
        /// Es el adaptador con respecto a la base de datos para realizar llamadas
        /// </summary>
        private EmpleadoAdapter EmployAdapter = new();
        /// <summary>
        /// 
        /// </summary>
        private readonly string ControllerName = "EmpleadosController";
        /// <summary>
        /// 
        /// </summary>
        public EmpleadosController() { }
        /// <summary>
        /// Recupera todos los empleados
        /// </summary>
        /// <returns>Un listado de todos los empleados</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Empleados/ObtenerEmpleados/
        ///     
        ///     RESPONSE:
        ///     [
        ///         {
        ///           "id": 0,
        ///           "legajo": "gbergesio@frc.utn.edu.ar",
        ///           "nombre_empleado": ", Genaro Rafael Bergesio",
        ///           "id_perfil": 5,
        ///           "nombre_perfil": "Administrador",
        ///           "activo": true
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado completo de empleados </response>
        /// <response code="204" >No se encontro ningun empleado </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerEmpleados")]
        [ProducesResponseType(typeof(IEnumerable<EmpleadosSAE>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<EmpleadosSAE>> ObtenerEmpleados()
        {
            try
            {
                //El numero de funcion es: 45
                if (TienePermiso(45))
                {
                    List<EmpleadosSAE>? listadoDeportes = EmployAdapter.ObtenerEmpleadosCompleto();

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
        /// Recupera solo empleados activos
        /// </summary>
        /// <returns>Un listado con empleados actuales</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Empleados/ObtenerEmpleadosActivos/
        ///     
        ///     [
        ///         {
        ///           "id": 0,
        ///           "legajo": "gbergesio@frc.utn.edu.ar",
        ///           "nombre_empleado": ", Genaro Rafael Bergesio",
        ///           "id_perfil": 5,
        ///           "nombre_perfil": "Administrador",
        ///           "activo": true
        ///         }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado de empleados actuales </response>
        /// <response code="204" >No se encontro ningun empleado </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerEmpleadosActivos")]
        [ProducesResponseType(typeof(IEnumerable<EmpleadosSAE>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<EmpleadosSAE>> ObtenerEmpleadosActivos()
        {
            try
            {
                //El numero de funcion es: 45
                if (TienePermiso(45))
                {
                    List<EmpleadosSAE>? listadoDeportes = EmployAdapter.ObtenerEmpleadosActivo();

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
        /// Busca empleados por ID
        /// </summary>
        /// <param name="id">Es el identificacor del empleado en la BD</param>
        /// <returns>Un empleado que coincida con ese ID</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Empleados/ObtenerEmpleadosXId/{id}
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "legajo": "gbergesio@frc.utn.edu.ar",
        ///       "nombre_empleado": ", Genaro Rafael Bergesio",
        ///       "id_perfil": 5,
        ///       "nombre_perfil": "Administrador",
        ///       "activo": true
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un empleado con este ID </response>
        /// <response code="204" >No se encontro ningun empleado </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{id}")]
        [ActionName("ObtenerEmpleadosXId")]
        [ProducesResponseType(typeof(EmpleadosSAE), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EmpleadosSAE> ObtenerEmpleadosXId(int id)
        {
            try
            {
                if ( TienePermiso(45))
                {
                    EmpleadosSAE? espe = EmployAdapter.BuscarEmpleadoXId(id);
                    if (espe == null) return Conflict();
                    if (espe.id == -1) return NoContent();
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
        /// Busca empleados por legajo
        /// </summary>
        /// <param name="legajo">Es un legajo tanto de alumno, docente o no docente en la UTN FRC </param>
        /// <returns>Un empleado que coincida con ese legajo</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Empleados/ObtenerEmpleadosXLegajo/{legajo}
        ///     
        ///     RESPONSE:
        ///     {
        ///       "id": 0,
        ///       "legajo": "gbergesio@frc.utn.edu.ar",
        ///       "nombre_empleado": ", Genaro Rafael Bergesio",
        ///       "id_perfil": 5,
        ///       "nombre_perfil": "Administrador",
        ///       "activo": true
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un empleado con este legajo </response>
        /// <response code="204" >No se encontro ningun empleado </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{legajo}")]
        [ActionName("ObtenerEmpleadosXLegajo")]
        [ProducesResponseType(typeof(EmpleadosSAE), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EmpleadosSAE> ObtenerEmpleadosXLegajo(string legajo)
        {
            try
            {
                if ( TienePermiso(45))
                {
                    EmpleadosSAE? espe = EmployAdapter.BuscarEmpleadoXLegajo(legajo);
                    if (espe == null) return Conflict();
                    if (espe.id == -1) return NoContent();
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
        //Teoricamente no se debe modificar mas el empleado sino que es lo mismo que modificar un usuario
        ///// <summary>
        ///// Permite la modificacion de un empleado
        ///// </summary>
        ///// <param name="id"> El ID del empleado a modificar</param>
        ///// <param name="empleado"> Los datos modificados del empleado</param>
        ///// <returns>Un empleado modificado en BD</returns>
        ///// <remarks>
        ///// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        /////  
        ///// Ejemplo de uso:
        ///// 
        /////     PUT /api/Empleados/ModificarEmpleado/{id}
        /////     BODY:
        /////     {
        /////         "id": 0,
        /////         "legajo": "string",
        /////         "activo": true,
        /////     }
        /////     
        /////     RESPONSE:
        /////     {
        /////        "id": 0,
        /////        "legajo": "string",
        /////        "nombre_empleado": "string",
        /////        "activo": true
        /////     }
        /////     
        ///// </remarks>
        ///// <response code="200" >Devuelve el empleado modificado en BD </response>
        ///// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del usuario a modificar </response>
        ///// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        ///// <response code="403" >Su perfil no cuenta con este permiso</response>
        ///// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
        ///// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        //[HttpPut("{id}")]
        //[ActionName("ModificarEmpleado")]
        //[ProducesResponseType(typeof(EmpleadosSAE), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status409Conflict)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public ActionResult<EmpleadosSAE> ModificarEmpleado(int id, [FromBody, Required] EmpleadosSAE empleado)
        //{
        //    try
        //    {
        //        if (id != empleado.id) return BadRequest();
        //        //El numero de funcion es: 12
        //        if ( TienePermiso(44))
        //        {
        //            string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
        //            if (userData != null &&
        //               userData.Length > 0 &&
        //               userData != "NO DATA" &&
        //               int.TryParse(userData.Split(',')[2], out int idUserMod))
        //            {
        //                empleado = EmployAdapter.ModificarEmpleado(empleado, idUserMod);
        //                if (empleado.id != -1) return Ok(empleado);
        //                else return Conflict();
        //            }
        //            else return Unauthorized();
        //        }
        //        else return Forbid();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.RegistrarDatos(Logger.LogOptions.Error, this.Request.Path, ex.Message, ControllerName);
        //        return BadRequest();
        //    }
        //}
        /// <summary>
        /// Permite crear empleados
        /// </summary>
        /// <param name="nuevoUsuario"></param>
        /// <param name="nombres"></param>
        /// <param name="apellidos"></param>
        /// <returns>Un empleado creado en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Empleados/CrearEmpleado/
        ///     BODY:
        ///     {
        ///         "id": 0,
        ///         "legajo": "string",
        ///         "activo": true,
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///        "id": 0,
        ///        "legajo": "string",
        ///        "nombre_empleado": "string",
        ///        "activo": true
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve el empleado creado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El empleado no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost]
        [ActionName("CrearEmpleado")]
        [ProducesResponseType(typeof(Usuarios), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Usuarios> CrearEmpleado(
            [FromBody]Usuarios nuevoUsuario,
            [FromQuery] string nombres,
            [FromQuery] string apellidos)
        {
            try
            {
                if ( TienePermiso(43))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData != null &&
                       userData.Length > 0 &&
                       userData != "NO DATA" &&
                       int.TryParse(userData.Split(',')[2], out int idUserCrea))
                    {
                        Usuarios empleado = EmployAdapter.CrearEmpleado(nuevoUsuario,nombres,apellidos, idUserCrea);
                        if (empleado.id != -1) return Created("Empleado Creado", empleado);
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
        /// Recupera todos los horarios registrados
        /// </summary>
        /// <returns>Un listado de todos los horarios</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Empleados/ObtenerHorarios/
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///          "id": 0,
        ///          "hora_inicio": "HH:MM:SS",
        ///          "hora_fin":    "HH:MM:SS",
        ///          "dia": true,
        ///          "nombre_empleado_atencion": "string",
        ///          "activo": true
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado completo de los horarios </response>
        /// <response code="204" >No se encontro ningun horario </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerHorarios")]
        [ProducesResponseType(typeof(IEnumerable<HorariosSAE>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<HorariosSAE>> ObtenerHorarios()
        {
            try
            {
                //El numero de funcion es: 50
                if ( TienePermiso(50))
                {
                    List<HorariosSAE>? listadoHora = EmployAdapter.ObtenerHorarioCompleto();

                    if (listadoHora == null) return Conflict();
                    if (listadoHora.Count == 0) return NoContent();

                    return Ok(listadoHora);
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
        /// Recupera todos los horarios activos
        /// </summary>
        /// <returns>Un listado de los horarios activos</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Empleados/ObtenerHorariosActivos/
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///          "id": 0,
        ///          "hora_inicio": "HH:MM:SS",
        ///          "hora_fin":    "HH:MM:SS",
        ///          "dia": true,
        ///          "nombre_empleado_atencion": "string",
        ///          "activo": true
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado de los horarios activos </response>
        /// <response code="204" >No se encontro ningun horario activo </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerHorariosActivos")]
        [ProducesResponseType(typeof(IEnumerable<HorariosSAE>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<HorariosSAE>> ObtenerHorariosActivos()
        {
            try
            {
                //El numero de funcion es: 50
                if ( TienePermiso(50))
                {
                    List<HorariosSAE>? listadoHora = EmployAdapter.ObtenerHorarioActivo();

                    if (listadoHora == null) return Conflict();
                    if (listadoHora.Count == 0) return NoContent();

                    return Ok(listadoHora);
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
        /// Busca horario por su ID
        /// </summary>
        /// <param name="id_horario">Es el identificador del horario en la BD</param>
        /// <returns>Un horario encontrado por ID</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Empleados/ObtenerHorariosXId/{id_horario}
        ///     
        ///     RESPONSE:
        ///     {
        ///        "id": 0,
        ///        "hora_inicio": "HH:MM:SS",
        ///        "hora_fin":    "HH:MM:SS",
        ///        "dia": true,
        ///        "nombre_empleado_atencion": "string",
        ///        "activo": true
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un horario con este ID </response>
        /// <response code="204" >No se encontro ningun horario </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{id_horario}")]
        [ActionName("ObtenerHorariosXId")]
        [ProducesResponseType(typeof(HorariosSAE), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<HorariosSAE> ObtenerHorariosXId(int id_horario)
        {
            try
            {
                if ( TienePermiso(50))
                {
                    HorariosSAE? hora = EmployAdapter.BuscarHorarioXId(id_horario);
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
        /// Busca horarios por empleado
        /// </summary>
        /// <param name="id_empleado">Es el identificador del empleado en la BD</param>
        /// <returns>Un listado de horarios para ese empleado</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Empleados/ObtenerHorariosXEmpleado/{id_empleado}
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///          "id": 0,
        ///          "hora_inicio": "HH:MM:SS",
        ///          "hora_fin":    "HH:MM:SS",
        ///          "dia": true,
        ///          "nombre_empleado_atencion": "string",
        ///          "activo": true
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado de horarios del empleado </response>
        /// <response code="204" >No se encontro ningun horario </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet("{id_empleado}")]
        [ActionName("ObtenerHorariosXEmpleado")]
        [ProducesResponseType(typeof(IEnumerable<HorariosSAE>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<HorariosSAE>> ObtenerHorariosXEmpleado(int id_empleado)
        {
            try
            {
                //Este lo puede visualizar los empleados
                if ( TienePermiso(49))
                {
                    List<HorariosSAE>? listadoHora = EmployAdapter.BuscarHorarioXEmpleado(id_empleado);
                    if (listadoHora == null) return Conflict();
                    if (listadoHora.Count == 0) return NoContent();

                    return Ok(listadoHora);
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
        /// <param name="horario"> Los datos modificados del horario</param>
        /// <returns>Un horario modificado en BD</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     PUT /api/Empleados/ModificarHorario/{id}
        ///     BODY:
        ///     {
        ///     "id": 0,
        ///     "hora_inicio": "HH:MM:SS",
        ///     "hora_fin": "HH:MM:SS",
        ///     "dia": true,
        ///     "activo": true
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///     "id": 0,
        ///     "hora_inicio": "HH:MM:SS",
        ///     "hora_fin": "HH:MM:SS",
        ///     "dia": true,
        ///     "nombre_empleado_atencion": "string",
        ///     "activo": true
        ///     }
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve el horario modificado en BD </response>
        /// <response code="400" >Ocurre un error en la consulta o el ID es diferente que el del usuario a modificar </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos y no se modifica el usuario </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{id}")]
        [ActionName("ModificarHorario")]
        [ProducesResponseType(typeof(HorariosSAE), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<HorariosSAE> ModificarHorario(int id, [FromBody, Required] HorariosSAE horario)
        {
            try
            {
                if (id != horario.id) return BadRequest();
                //El numero de funcion es: 47
                if ( TienePermiso(47))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData != null &&
                       userData.Length > 0 &&
                       userData != "NO DATA" &&
                       int.TryParse(userData.Split(',')[2], out int idUserMod))
                    {
                        horario = EmployAdapter.ModificarHorario(horario, idUserMod);
                        if (horario.id != -1) return Ok(horario);
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
        /// Permite crear horarios
        /// </summary>
        /// <param name="horario">El horario que deseamos crear, se envia en el Body</param>
        /// <returns>Un horario creado en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Empleados/CrearHorario/
        ///     BODY:
        ///     {
        ///         "id": 0,
        ///         "hora_inicio": "HH:MM:SS",
        ///         "hora_fin": "HH:MM:SS",
        ///         "dia": true,
        ///         "activo": true
        ///     }
        ///     
        ///     RESPONSE:
        ///     {
        ///         "id": 0,
        ///         "hora_inicio": "HH:MM:SS",
        ///         "hora_fin": "HH:MM:SS",
        ///         "dia": true,
        ///         "nombre_empleado_atencion": "string",
        ///         "activo": true
        ///     }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve el horario creado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El empleado no genero su JWT</response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost]
        [ActionName("CrearHorario")]
        [ProducesResponseType(typeof(HorariosSAE), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<HorariosSAE> CrearHorario(HorariosSAE horario)
        {
            try
            {
                if ( TienePermiso(46))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData != null &&
                       userData.Length > 0 &&
                       userData != "NO DATA" &&
                       int.TryParse(userData.Split(',')[2], out int idUserCrea))
                    {
                        horario = EmployAdapter.CrearHorario(horario, idUserCrea);
                        if (horario.id != -1) return Created("Horario Creado", horario);
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
        ///     POST /api/Empleados/EliminarHorario/{id}
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> EliminarHorario(int id)
        {
            try
            {
                if ( TienePermiso(48))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        if (EmployAdapter.EliminarHorario(id)) return Ok("Horario Eliminado");
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
        /// Recupera todo el Linktree
        /// </summary>
        /// <returns>Un listado de todos los items</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Empleados/ObtenerLinktree/
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         "id": 0,
        ///         "titulo": "string",
        ///         "id_index_ico": 0,
        ///         "hipervinculo": "string",
        ///         "contador_clicks": 0
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Devuelve un listado completo de empleados </response>
        /// <response code="204" >No se encontro ningun empleado </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpGet]
        [ActionName("ObtenerLinktree")]
        [ProducesResponseType(typeof(IEnumerable<ItemLinktree>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<ItemLinktree>> ObtenerLinktree()
        {
            try
            {
                //El numero de funcion es: 140
                if (TienePermiso(140))
                {
                    List<ItemLinktree>? linktree = EmployAdapter.ObtenerLinktree();

                    if (linktree == null) return Conflict();
                    if (linktree.Count == 0) return NoContent();

                    return Ok(linktree);
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
        /// Cuenta una visualizacion en el Item
        /// </summary>
        /// <returns>Un mensaje de exito</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     GET /api/Empleados/ContarVisualizacionItem/{id}
        ///     
        ///     RESPONSE:
        ///     [
        ///       {
        ///         Visualizado
        ///       }
        ///     ]
        ///     
        /// </remarks>
        /// <response code="200" >Un mensaje de exito </response>
        /// <response code="400" >Ocurre un error en la contabilizacion </response>
        /// <response code="401" >El usuario no genero su JWT o su perfil no cuenta con este permiso </response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>        
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPut("{id}")]
        [ActionName("ContarVisualizacionItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> ContarVisualizacionItem(int id)
        {
            try
            {
                //El numero de funcion es: 140
                if ( TienePermiso(140))
                {
                    if (EmployAdapter.ContarVisualizaconLink(id)) return Conflict();
                    else return Ok("Visualizado");
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
        /// Permite crear items del linktree
        /// </summary>
        /// <param name="item">El item que deseamos crear, se envia en el Body</param>
        /// <returns>Un item creado en la base de datos o error</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Empleados/CrearItemLinkTree/
        ///     BODY:
        ///       {
        ///         "id": 0,
        ///         "titulo": "string",
        ///         "id_index_ico": 0,
        ///         "hipervinculo": "string",
        ///       }
        ///     
        ///     RESPONSE:
        ///       {
        ///         "id": 0,
        ///         "titulo": "string",
        ///         "id_index_ico": 0,
        ///         "hipervinculo": "string",
        ///         "contador_clicks": 0
        ///       }
        ///     
        /// </remarks>
        /// <response code="201" >Devuelve el item creado en la BD </response>
        /// <response code="400" >Ocurre un error en la consulta </response>
        /// <response code="401" >El empleado no genero su JWT</response>
        /// <response code="403" >Su perfil no cuenta con este permiso</response>
        /// <response code="409" >Ocurre un error en el procedimiento/vista de la base de datos </response>
        /// <response code="500" >Ocurre un error en la API o en el Servidor no documentada </response>
        [HttpPost]
        [ActionName("CrearItemLinkTree")]
        [ProducesResponseType(typeof(ItemLinktree), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ItemLinktree> CrearItemLinkTree(ItemLinktree item)
        {
            try
            {
                if ( TienePermiso(138))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData != null &&
                       userData.Length > 0 &&
                       userData != "NO DATA" &&
                       int.TryParse(userData.Split(',')[2], out int idUserCrea))
                    {
                        item = EmployAdapter.CrearItemLinkTree(item);
                        if (item.id != -1) return Created("Item Creado", item);
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
        /// Permite eliminar items del linktree
        /// </summary>
        /// <param name="id">Es el id del item a eliminar</param>
        /// <returns>Un mensaje de OK o el item que no se pudo eliminar</returns>
        /// <remarks>
        /// NOTA: Es necesario usar el JWT en el encabezado de Authorization
        ///  
        /// Ejemplo de uso:
        /// 
        ///     POST /api/Empleados/EliminarItem/{id}
        ///     
        ///     RESPONSE:
        ///     {
        ///         "Item Eliminado"
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
        [ActionName("EliminarItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<string> EliminarItem(int id)
        {
            try
            {
                if ( TienePermiso(139))
                {
                    string userData = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "NO DATA";
                    if (userData == null || userData == "NO DATA") return Unauthorized();
                    else
                    {
                        if (EmployAdapter.EliminarLinkTree (id)) return Ok("Item Eliminado");
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
