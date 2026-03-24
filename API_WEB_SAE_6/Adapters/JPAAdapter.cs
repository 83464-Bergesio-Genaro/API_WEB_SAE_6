using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models;
using MySqlConnector;
using System.Data;

namespace API_WEB_SAE_6.Adapters
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="motorDB"></param>
    public class JPAAdapter(string motorDB = "MySQL")
    {
        /// <summary>
        /// Define que tipo de base de datos se usa para consumir la informacion
        /// </summary>
        public string MotorDB { get; set; } = motorDB;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<EventosSAE>? ObtenerEventosPublicos()
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_JPA_Listar_Eventos_Publicos");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<EventosSAE> listadoEventosCompleto = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        EventosSAE eventos = new(row);
                        listadoEventosCompleto.Add(eventos);
                    }
                    return [.. listadoEventosCompleto.OrderBy(x => x.fecha_evento)];

                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearSesionUsuario", ex.Message, "JPAAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<EventosSAE>? ObtenerEventosSAE()
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_JPA_Listar_Eventos_SAE");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<EventosSAE> listadoEventosCompleto = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        EventosSAE eventos = new(row);
                        listadoEventosCompleto.Add(eventos);
                    }
                    return [.. listadoEventosCompleto.OrderBy(x => x.fecha_evento)];

                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearSesionUsuario", ex.Message, "JPAAdapter");
                return null;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public EventosSAE? ObtenerEventoXId(int idEvento)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_evento", MySqlDbType.Int32) { Value = idEvento }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_JPA_Buscar_Eventos_Id", parameters);

                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new EventosSAE(respuesta.Rows[0]);
                }
                else return new();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearSesionUsuario", ex.Message, "JPAAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="evento"></param>
        /// <returns></returns>
        public EventosSAE ModificarEventoSae(EventosSAE evento)
        {
            if (MotorDB == "MySQL")
            {
                //Inicializa un valor y le asigna el tipo
                List<MySqlParameter> parameters = [
                    new("i_id_evento", MySqlDbType.Int32) { Value = evento.id },
                    new("i_fecha", MySqlDbType.VarChar) { Value = evento.fecha_evento.ToString("yyyy-MM-dd") },
                    new("i_hora_ini", MySqlDbType.VarChar) { Value = evento.horario_inicio },
                    new("i_hora_fin", MySqlDbType.VarChar) { Value = evento.horario_fin  },
                    new("i_encargado", MySqlDbType.VarChar) { Value = evento.encargado },
                    new("i_nombre_evento", MySqlDbType.VarChar) { Value = evento.nombre_evento },
                    new("i_informacion_interna", MySqlDbType.Bit) { Value = evento.informacion_interna },
                    new("i_ubicacion", MySqlDbType.Text) { Value = evento.ubicacion }
                ];
                GeneralAdapterMySQL consult = new();
                DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_JPA_Modificar_Evento", parameters);
                if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                else return new(respuesta.Rows[0]);
            }
            else return new();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="evento"></param>
        /// <returns></returns>
        public EventosSAE CrearEventoSae(EventosSAE evento)
        {
            //Inicializa un valor y le asigna el tipo
            List<MySqlParameter> parameters = [
                    new("i_fecha", MySqlDbType.VarChar) { Value = evento.fecha_evento.ToString("yyyy-MM-dd") },
                    new("i_hora_ini", MySqlDbType.VarChar) { Value = evento.horario_inicio },
                    new("i_hora_fin", MySqlDbType.VarChar) { Value = evento.horario_fin  },
                    new("i_encargado", MySqlDbType.VarChar) { Value = evento.encargado },
                    new("i_nombre_evento", MySqlDbType.VarChar) { Value = evento.encargado },
                    new("i_informacion_interna", MySqlDbType.Bit) { Value = evento.informacion_interna },
                    new("i_ubicacion", MySqlDbType.Text) { Value = evento.ubicacion }
                ];
            GeneralAdapterMySQL consult = new();
            DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_JPA_Crear_Evento", parameters);
            if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
            else return new(respuesta.Rows[0]);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idEvento"></param>
        /// <returns></returns>
        public bool EliminarEventoSae(int idEvento)
        {
            List<MySqlParameter> parameters = [new("i_id_evento", MySqlDbType.Int32) { Value = idEvento }];

            GeneralAdapterMySQL consult = new();
            DataTable respuesta = consult.ExecuteStoredProcedure( "MODULO_JPA_Eliminar_Evento", parameters);

            if (respuesta.Rows.Count > 0) return false;
            else return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<StandJPA>? ObtenerStandsCompleto()
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_JPA_Listar_Stand");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<StandJPA> listadoStandsCompleto = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        StandJPA eventos = new(row);
                        listadoStandsCompleto.Add(eventos);
                    }
                    return listadoStandsCompleto;

                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearSesionUsuario", ex.Message, "JPAAdapter");
                return null;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public StandJPA ModificarStandJPA(StandJPA stand)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                            new("i_id_stand", MySqlDbType.Int32) { Value = stand.id },
                    new("i_nombre_stand", MySqlDbType.VarChar) { Value = stand.nombre_stand },
                    new("i_hora_ini", MySqlDbType.VarChar) { Value = stand.horario_inicio },
                    new("i_hora_fin", MySqlDbType.VarChar) { Value = stand.horario_fin  },
                    new("i_ubicacion", MySqlDbType.VarChar) { Value = stand.ubicacion },
                    new("i_expositor", MySqlDbType.VarChar) { Value = stand.expositor }
                        ];
                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_JPA_Modificar_Stand", parameters);
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);

                }
                else return new();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearSesionUsuario", ex.Message, "JPAAdapter");
                return new();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stand"></param>
        /// <returns></returns>
        public StandJPA CrearStandJPA(StandJPA stand)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                                new("i_nombre_stand", MySqlDbType.VarChar) { Value = stand.nombre_stand },
                        new("i_hora_ini", MySqlDbType.VarChar) { Value = stand.horario_inicio },
                        new("i_hora_fin", MySqlDbType.VarChar) { Value = stand.horario_fin  },
                        new("i_ubicacion", MySqlDbType.VarChar) { Value = stand.ubicacion },
                        new("i_expositor", MySqlDbType.VarChar) { Value = stand.expositor }
                    ];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_JPA_Crear_Stand", parameters);
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                else return new();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearSesionUsuario", ex.Message, "JPAAdapter");
                return new();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool EliminarStand(int idStand)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    List<MySqlParameter> parameters = [new("i_id_stand", MySqlDbType.Int32) { Value = idStand }];
                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_JPA_Eliminar_Stand", parameters);
                    if (respuesta.Rows.Count > 0)
                    {
                        if (respuesta.Rows[0][0].ToString() == "ERROR") Logger.RegistrarDatos(Logger.LogOptions.Error, "EliminarStand", "Imposible eliminar el stand: " + idStand, "JPAAdapter");
                        return false;
                    }
                    else return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "EliminarStand", ex.Message, "JPAAdapter");
                return new();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<InteresadosSAE>? ObtenerInteresadosEventos()
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteView("MODULO_JPA_Listar_Interesados");

                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<InteresadosSAE> listadoInteresados = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        InteresadosSAE eventos = new(row);
                        listadoInteresados.Add(eventos);
                    }
                    return listadoInteresados;
                }
                else return [];
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "ObtenerInteresadosEventos", ex.Message, "JPAAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public InteresadosSAE ModificarInteresado(InteresadosSAE interesado)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    List<MySqlParameter> parameters = [
                        new("i_id_interesado", MySqlDbType.Int32) { Value = interesado.id },
                        new("i_nombre", MySqlDbType.VarChar) { Value = interesado.nombre_interesado },
                        new("i_contacto", MySqlDbType.VarChar) { Value = interesado.contacto  },
                        new("i_email", MySqlDbType.VarChar) { Value = interesado.email },
                    ];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_JPA_Modificar_Interesados", parameters);
                    
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                else return new();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "ModificarInteresado", ex.Message, "JPAAdapter");
                return new();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="interesado"></param>
        /// <returns></returns>
        public InteresadosSAE CrearInteresado(InteresadosSAE interesado)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    List<MySqlParameter> parameters = [
                        new("i_nombre", MySqlDbType.VarChar) { Value = interesado.nombre_interesado },
                        new("i_contacto", MySqlDbType.VarChar) { Value = interesado.contacto  },
                        new("i_email", MySqlDbType.VarChar) { Value = interesado.email },
                    ];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_JPA_Crear_Interesado", parameters);

                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                else return new();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearInteresado", ex.Message, "JPAAdapter");
                return new();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool EliminarInteresado(int idInteresado)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    List<MySqlParameter> parameters = [new("i_id_interesado", MySqlDbType.Int32) { Value = idInteresado }];
                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_JPA_Eliminar_Interesado", parameters);
                    if (respuesta.Rows.Count > 0)
                    {
                        if (respuesta.Rows[0][0].ToString() == "ERROR") Logger.RegistrarDatos(Logger.LogOptions.Error, "EliminarStand", "Imposible eliminar el interesado: " + idInteresado, "JPAAdapter");
                        return false;
                    }
                    else return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "EliminarInteresado", ex.Message, "JPAAdapter");
                return new();
            }
        }
    }
}
