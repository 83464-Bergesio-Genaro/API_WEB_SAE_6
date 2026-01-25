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
                Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearSesionUsuario", ex.Message, "UsuarioAdapter");
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
                Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearSesionUsuario", ex.Message, "UsuarioAdapter");
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
                Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearSesionUsuario", ex.Message, "UsuarioAdapter");
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
            //Inicializa un valor y le asigna el tipo
            List<MySqlParameter> parameters = [
                    new("i_id_evento", MySqlDbType.Int32) { Value = evento.id },
                    new("i_fecha", MySqlDbType.VarChar) { Value = evento.fecha_evento.ToString("yyyy-MM-dd") },
                    new("i_hora_ini", MySqlDbType.VarChar) { Value = evento.horario_inicio },
                    new("i_hora_fin", MySqlDbType.VarChar) { Value = evento.horario_fin  },
                    new("i_encargado", MySqlDbType.VarChar) { Value = evento.encargado },
                    new("i_nombre_evento", MySqlDbType.VarChar) { Value = evento.encargado },
                    new("i_informacion_interna", MySqlDbType.Bit) { Value = evento.informacion_interna }
                ];
            GeneralAdapterMySQL consult = new();
            DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_JPA_Modificar_Evento", parameters);
            if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
            else return new(respuesta.Rows[0]);
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
                    new("i_informacion_interna", MySqlDbType.Bit) { Value = evento.informacion_interna }
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
    }
}
