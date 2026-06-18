using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models.Salud;
using MySqlConnector;
using System.Data;

namespace API_WEB_SAE_6.Adapters
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="motorDB"></param>
    public class SaludAdapter(string motorDB = "MySQL")
    {
        /// <summary>
        /// Define que tipo de base de datos se usa para consumir la informacion
        /// </summary>
        public string MotorDB { get; set; } = motorDB;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<EstadosTurno>? ObtenerEstadoTurno()
        {
            string method = "ObtenerEstadoTurno";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_SALUD_Listar_Estados_Turno");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<EstadosTurno> estado = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        EstadosTurno espe = new(row);
                        estado.Add(espe);
                    }
                    return estado;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "SaludAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Especialidad>? ObtenerEspecialidadesCompleto()
        {
            string method = "ObtenerEspecialidadCompleto";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_SALUD_Listar_Especialidades_Medicas");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<Especialidad> especialidad = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        Especialidad espe = new(row);
                        especialidad.Add(espe);
                    }
                    return especialidad;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "SaludAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Especialidad>? ObtenerEspecialidadesActivas()
        {
            string method = "ObtenerEspecialidadesActivas";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_SALUD_Listar_Especialidades_Medicas_Activas");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<Especialidad> especialidad = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        Especialidad espe = new(row);
                        especialidad.Add(espe);
                    }
                    return especialidad;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "SaludAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="espe"></param>
        /// <returns></returns>
        public Especialidad ModificarEspecialidadMed(Especialidad espe)
        {
            string method = "ModificarEspecialidadMed";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_id_especialidad", MySqlDbType.VarChar) { Value = espe.id },
                        new("i_nombre", MySqlDbType.VarChar) { Value = espe.nombre },
                        new("i_descripcion", MySqlDbType.VarChar) { Value = espe.descripcion },
                        new("i_activo", MySqlDbType.Bit) { Value = espe.activo  }
                        ];
                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_SALUD_Modificar_Especialidad_Medica", parameters);
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                else return new();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "SaludAdapter");
                return new();
            }
        }
        /// <summary>
        /// Crea un nuevo especialista medico, devuelve el especialista medico creado con su informacion completa, devuelve un objeto vacio si el resultado es correcto pero no hay datos, devuelve null si ocurre un error
        /// </summary>
        /// <param name="especialista"></param>
        /// <returns></returns>
        public Especialidad CrearEspecialidad(Especialidad especialista)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_nombre", MySqlDbType.VarChar) { Value = especialista.nombre},
                        new("i_descripcion", MySqlDbType.VarChar) { Value = especialista.descripcion }
                        ];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_SALUD_Crear_Especialidad_Medica", parameters);

                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearEspecialidad", ex.Message, "SaludAdapter");
                    return new();
                }
            }
            else return new();
        }
        /// <summary>
        /// Obtiene un listado completo de especialistas medicos, devuelve null si ocurre un error, devuelve una lista vacia si el resultado es correcto pero no hay datos
        /// </summary>
        /// <returns></returns>
        public List<EspecialistaMedico>? ObtenerEspecialistasCompleto()
        {
            string method = "ObtenerEspecialistasCompleto";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_SALUD_Listar_Personal_Medico");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<EspecialistaMedico> listadoEspecialistas = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        EspecialistaMedico espe = new(row);
                        listadoEspecialistas.Add(espe);
                    }
                    return listadoEspecialistas;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "SaludAdapter");
                return null;
            }
        }
        /// <summary>
        /// Obtiene un especialista medico por su cuil, devuelve null si no lo encuentra o si ocurre un error, devuelve un objeto vacio si el resultado es correcto pero no hay datos
        /// </summary>
        /// <param name="cuil"></param>
        /// <returns></returns>
        public EspecialistaMedico? ObtenerEspecialistaXCuil(string cuil)
        {
            string method = "ObtenerEspecialistaXCuil";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_cuil_especialista", MySqlDbType.VarChar) { Value = cuil }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_SALUD_Buscar_Especialista_Cuil", parameters);

                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new EspecialistaMedico(respuesta.Rows[0]);
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "SaludAdapter");
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="espe"></param>
        /// <param name="usuarioActual"></param>
        /// <returns></returns>
        public EspecialistaMedico ModificarEspecialistaMed(EspecialistaMedico espe,int usuarioActual)
        {
            string method = "ModificarEspecialistaMed";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters =[
                        new("i_cuil_especialista", MySqlDbType.VarChar) { Value = espe.cuil },
                        new("i_nombre", MySqlDbType.VarChar) { Value = espe.nombre },
                        new("i_apellido", MySqlDbType.VarChar) { Value = espe.apellido },
                        new("i_id_especialidad", MySqlDbType.Int32) { Value = espe.especialidad.id },
                        new("i_activo", MySqlDbType.Bit) { Value = espe.presta_servicio  },
                        new("i_id_usuario_mod", MySqlDbType.Int32) { Value = usuarioActual},];
                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_SALUD_Modificar_Especialista_Medico", parameters);
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                else return new();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "SaludAdapter");
                return new();
            }
        }
        /// <summary>
        /// Crea un nuevo especialista medico, devuelve el especialista medico creado con su informacion completa, devuelve un objeto vacio si el resultado es correcto pero no hay datos, devuelve null si ocurre un error
        /// </summary>
        /// <param name="especialista"></param>
        /// <param name="idCreacion"></param>
        /// <returns></returns>
        public EspecialistaMedico CrearEspecialista(EspecialistaMedico especialista, int idCreacion)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_cuil_especialista", MySqlDbType.VarChar) { Value = especialista.cuil},
                        new("i_nombre", MySqlDbType.VarChar) { Value = especialista.nombre},
                        new("i_apellido", MySqlDbType.VarChar) { Value = especialista.apellido },
                        new("i_id_especialidad", MySqlDbType.Int32) { Value = especialista.especialidad.id},
                        new("i_activo", MySqlDbType.Bit) { Value = especialista.presta_servicio},
                        new("i_id_usuario_alta", MySqlDbType.Int32) { Value = idCreacion}
                        ];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_SALUD_Crear_Especialista_Medico", parameters);

                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearEspecialista", ex.Message, "SaludAdapter");
                    return new();
                }
            }
            else return new();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<HorariosSalud>? ObtenerHorariosMedicos()
        {
            string method = "ObtenerHorariosMedicos";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_SALUD_Listar_Horarios_Medico");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<HorariosSalud> listadoHorario = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        HorariosSalud horario = new(row);
                        listadoHorario.Add(horario);
                    }
                    return listadoHorario;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "SaludAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idHorario"></param>
        /// <returns></returns>
        public HorariosSalud? ObtenerHorariosXId(int idHorario)
        {
            string method = "ObtenerHorariosXId";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_horario_medico", MySqlDbType.Int32) { Value = idHorario }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_SALUD_Buscar_Horario_Medico_Id", parameters);

                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new HorariosSalud(respuesta.Rows[0]);
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "SaludAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cuil_espe"></param>
        /// <returns></returns>
        public List<HorariosSalud>? ObtenerHorariosXCuil(string cuil_espe)
        {
            string method = "ObtenerHorariosXCuil";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_cuil_es", MySqlDbType.VarChar) { Value = cuil_espe }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_SALUD_Buscar_Horario_Medico_Cuil", parameters);
                    
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<HorariosSalud> listadoHorario = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        HorariosSalud horario = new(row);
                        listadoHorario.Add(horario);
                    }
                    return listadoHorario;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "SaludAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="horarioMedico"></param>
        /// <param name="idModificacion"></param>
        /// <returns></returns>
        public HorariosSalud ModificarHorarioSalud(HorariosSalud horarioMedico,int idModificacion)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    List<MySqlParameter> parameters = [
                        new("i_id_horario", MySqlDbType.Int32) { Value = horarioMedico.id },
                        new("i_hora_ini", MySqlDbType.Time) { Value = horarioMedico.hora_inicio},
                        new("i_hora_fin", MySqlDbType.Time) { Value = horarioMedico.hora_fin},
                        new("i_dia", MySqlDbType.Int32) { Value = horarioMedico.dia },
                        new("i_cuil_es", MySqlDbType.VarChar) { Value =horarioMedico.cuil_especialista},
                        new("i_activo", MySqlDbType.Bit) { Value = horarioMedico.activo},
                        new("i_id_usuario_mod", MySqlDbType.Int32) { Value = idModificacion}
                        ];
                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_SALUD_Modificar_Horario_Medico", parameters);
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                else return new();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "ModificarHorarioSalud", ex.Message, "SaludAdapter");
                return new();
            }
        }
        /// <summary>
        /// Crea un nuevo especialista medico, devuelve el especialista medico creado con su informacion completa, devuelve un objeto vacio si el resultado es correcto pero no hay datos, devuelve null si ocurre un error
        /// </summary>
        /// <param name="horarioMedico"></param>
        /// <param name="idCreacion"></param>
        /// <returns></returns>
        public HorariosSalud CrearHorario(HorariosSalud horarioMedico, int idCreacion)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_hora_ini", MySqlDbType.Date) { Value = horarioMedico.hora_inicio},
                        new("i_hora_fin", MySqlDbType.Date) { Value = horarioMedico.hora_fin},
                        new("i_dia", MySqlDbType.Int32) { Value = horarioMedico.dia },
                        new("i_cuil_es", MySqlDbType.VarChar) { Value =horarioMedico.cuil_especialista},
                        new("i_activo", MySqlDbType.Bit) { Value = horarioMedico.activo},
                        new("i_id_usuario_act", MySqlDbType.Int32) { Value = idCreacion}
                        ];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_SALUD_Crear_Horario_Medico", parameters);

                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearEspecialista", ex.Message, "SaludAdapter");
                    return new();
                }
            }
            else return new();
        }
        /// <summary>
        /// Elimina un horario de salud, devuelve true si se elimino correctamente, devuelve false si ocurre un error o si no se encuentra el horario de salud a eliminar
        /// </summary>
        /// <param name="idHorario"></param>
        /// <returns></returns>
        public bool EliminarHorario(int idHorario)
        {
            List<MySqlParameter> parameters = [new("i_id_horario_medico", MySqlDbType.Int32) { Value = idHorario }];

            GeneralAdapterMySQL consult = new();
            DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_SALUD_Eliminar_Horario_Medico", parameters);

            if (respuesta.Rows.Count > 0) return false;
            else return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<FaltaEspecialista>? ObtenerFaltasXEspecialista(string cuil)
        {
            string method = "ObtenerFaltasXEspecialista";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    List<MySqlParameter> parameters = [new("i_cuil_especialista", MySqlDbType.VarChar) { Value = cuil }];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_SALUD_Listar_Faltas_X_Especialista", parameters);
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<FaltaEspecialista> listadoFaltas = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        FaltaEspecialista cur = new(row);
                        listadoFaltas.Add(cur);
                    }
                    return listadoFaltas;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "SaludAdapter");
                return null;
            }
        }
        /// <summary>
        /// Crea un nuevo especialista medico, devuelve el especialista medico creado con su informacion completa, devuelve un objeto vacio si el resultado es correcto pero no hay datos, devuelve null si ocurre un error
        /// </summary>
        /// <param name="faltaEsp"></param>
        /// <param name="idCreacion"></param>
        /// <returns></returns>
        public FaltaEspecialista CrearFaltaMedica(FaltaEspecialista faltaEsp, int idCreacion)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_cuil", MySqlDbType.VarChar) { Value =faltaEsp.cuil},
                        new("i_fecha_falta", MySqlDbType.Date) { Value = faltaEsp.fecha_alta},
                        new("i_observacion", MySqlDbType.VarChar) { Value = faltaEsp.observacion },
                        new("i_id_usuario_alta", MySqlDbType.Int32) { Value = idCreacion}
                        ];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_SALUD_Registrar_Falta_Especialista", parameters);

                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearFaltaMedica", ex.Message, "SaludAdapter");
                    return new();
                }
            }
            else return new();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<CursosMedicos>? ObtenerCursosMedicos()
        {
            string method = "ObtenerCursosMedicos";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_SALUD_Visualizar_Cursos_Medicos");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<CursosMedicos> listadoCursados = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        CursosMedicos cur = new(row);
                        listadoCursados.Add(cur);
                    }
                    return listadoCursados;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "SaludAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cursoMedico"></param>
        /// <returns></returns>
        public CursosMedicos ModificarCursosMedicos(CursosMedicos cursoMedico)
        {
            string method = "ModificarCursosMedicos";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    List<MySqlParameter> parameters = [
                        new("i_id_curso", MySqlDbType.Int32) { Value = cursoMedico.id },
                        new("i_fecha_inicio", MySqlDbType.Date) { Value = cursoMedico.fecha_inicio},
                        new("i_fecha_fin", MySqlDbType.Date) { Value = cursoMedico.fecha_fin},
                        new("i_nombre_curso", MySqlDbType.Int32) { Value = cursoMedico.nombre_curso },
                        new("i_nombre_docente", MySqlDbType.VarChar) { Value =cursoMedico.nombre_docente},
                        new("i_cupo_maximo", MySqlDbType.Bit) { Value = cursoMedico.cupo_maximo},
                        new("i_activo", MySqlDbType.Int32) { Value = cursoMedico.activo}
                        ];
                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_SALUD_Modificar_Curso_Medico", parameters);
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                else return new();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "SaludAdapter");
                return new();
            }
        }
        /// <summary>
        /// Crea un nuevo especialista medico, devuelve el especialista medico creado con su informacion completa, devuelve un objeto vacio si el resultado es correcto pero no hay datos, devuelve null si ocurre un error
        /// </summary>
        /// <param name="cursoMedico"></param>
        /// <returns></returns>
        public CursosMedicos CrearCursoMedico(CursosMedicos cursoMedico)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_fecha_inicio", MySqlDbType.Date) { Value = cursoMedico.fecha_inicio},
                        new("i_fecha_fin", MySqlDbType.Date) { Value = cursoMedico.fecha_fin},
                        new("i_nombre_curso", MySqlDbType.VarChar) { Value = cursoMedico.nombre_curso },
                        new("i_nombre_docente", MySqlDbType.VarChar) { Value = cursoMedico.nombre_docente },
                        new("i_cupo_maximo", MySqlDbType.Int32) { Value = cursoMedico.cupo_maximo},
                        new("i_activo", MySqlDbType.Bit) { Value = cursoMedico.activo}
                        ];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_SALUD_Crear_Curso_Medico", parameters);

                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearCursoMedico", ex.Message, "SaludAdapter");
                    return new();
                }
            }
            else return new();
        }
        /// <summary>
        /// Elimina un horario de salud, devuelve true si se elimino correctamente, devuelve false si ocurre un error o si no se encuentra el horario de salud a eliminar
        /// </summary>
        /// <param name="idHorario"></param>
        /// <returns></returns>
        public bool EliminarCursoMedico(int idHorario)
        {
            List<MySqlParameter> parameters = [new("i_id_curso", MySqlDbType.Int32) { Value = idHorario }];

            GeneralAdapterMySQL consult = new();
            DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_SALUD_Eliminar_Curso_Medico", parameters);

            if (respuesta.Rows.Count > 0) return false;
            else return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TurnosMedicos>? ObtenerTurnosMedicos()
        {
            string method = "ObtenerTurnosMedicos";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_SALUD_Visualizar_Turnos");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<TurnosMedicos> listaTurnos = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        TurnosMedicos turno = new(row);
                        listaTurnos.Add(turno);
                    }
                    return listaTurnos;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "SaludAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TurnosMedicos>? ObtenerTurnosMedicosActivos()
        {
            string method = "ObtenerTurnosMedicosActivos";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_SALUD_Visualizar_Turnos_Activos");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<TurnosMedicos> listaTurnos = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        TurnosMedicos turno = new(row);
                        listaTurnos.Add(turno);
                    }
                    return listaTurnos;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "SaludAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TurnosMedicos>? ObtenerTurnosMedicosFinalizados()
        {
            string method = "ObtenerTurnosMedicosFinalizados";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_SALUD_Visualizar_Turnos_Finalizados");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<TurnosMedicos> listaTurnos = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        TurnosMedicos turno = new(row);
                        listaTurnos.Add(turno);
                    }
                    return listaTurnos;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "SaludAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TurnosMedicos>? ObtenerTurnosMedicosCancelado()
        {
            string method = "ObtenerTurnosMedicosCancelado";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_SALUD_Visualizar_Turnos_Cancelados");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<TurnosMedicos> listaTurnos = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        TurnosMedicos turno = new(row);
                        listaTurnos.Add(turno);
                    }
                    return listaTurnos;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "SaludAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_turno"></param>
        /// <returns></returns>
        public TurnosMedicos? ObtenerTurnoXId(int id_turno)
        {
            string method = "ObtenerTurnoXId";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_turno", MySqlDbType.Int32) { Value = id_turno }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_SALUD_Buscar_Turnos_Id", parameters);

                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new TurnosMedicos(respuesta.Rows[0]);
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "SaludAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="legajo"></param>
        /// <returns></returns>
        public List<TurnosMedicos>? ObtenerTurnoXlegajo(string legajo)
        {
            string method = "ObtenerTurnoXLegajo";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_legajo", MySqlDbType.VarChar) { Value = legajo }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_SALUD_Buscar_Turnos_Legajo", parameters);

                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<TurnosMedicos> listaTurnos = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        TurnosMedicos turno = new(row);
                        listaTurnos.Add(turno);
                    }
                    return listaTurnos;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "SaludAdapter");
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cursoMedico"></param>
        /// <param name="idUserMod"></param>
        /// <returns></returns>
        public TurnosMedicos ModificarTurnoMedico(TurnosMedicos cursoMedico, int idUserMod)
        {
            string method = "ModificarTurnoMedico";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    List<MySqlParameter> parameters = [
                        new("i_id_turno", MySqlDbType.Int32) { Value = cursoMedico.id },
                        new("i_cuil_medico", MySqlDbType.VarChar) { Value = cursoMedico.cuil_medico},
                        new("i_legajo", MySqlDbType.VarChar) { Value = cursoMedico.legajo},
                        new("i_id_estado_turno", MySqlDbType.Int32) { Value = cursoMedico.estadosTurno?.id??0 },//Lo pasa a pendiente si no tiene ningun estado asignado
                        new("i_fecha_solicitud", MySqlDbType.Date) { Value =cursoMedico.fecha_solicitud},
                        new("i_fecha_atencion", MySqlDbType.Date) { Value = cursoMedico.fecha_atencion},
                        new("i_hora_atencion", MySqlDbType.VarChar) { Value = cursoMedico.hora_atencion},
                        new("i_asunto", MySqlDbType.VarChar) { Value = cursoMedico.asunto},
                        new("i_id_usuario_mod", MySqlDbType.VarChar) { Value = idUserMod}
                        ];
                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_SALUD_Modificar_Turno_Medico", parameters);
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                else return new();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "SaludAdapter");
                return new();
            }
        }
        /// <summary>
        /// Crea un nuevo turno medico, devuelve el turno medico creado con su informacion completa, devuelve un objeto vacio si el resultado es correcto pero no hay datos, devuelve null si ocurre un error
        /// </summary>
        /// <param name="turnoMedico"></param>
        /// <param name="idUserCrea"></param>
        /// <returns></returns>
        public TurnosMedicos CrearTurnosMedicos(TurnosMedicos turnoMedico, int idUserCrea)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_legajo", MySqlDbType.VarChar) { Value = turnoMedico.legajo},
                        new("i_fecha_solicitud", MySqlDbType.Date) { Value = turnoMedico.fecha_solicitud},
                        new("i_asunto", MySqlDbType.VarChar) { Value = turnoMedico.asunto },
                        new("i_id_usuario_alta", MySqlDbType.Int32) { Value = idUserCrea }
                        ];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_SALUD_Crear_Turno_Medico", parameters);

                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearTurnosMedicos", ex.Message, "SaludAdapter");
                    return new();
                }
            }
            else return new();
        }
    }
}
