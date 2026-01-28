using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models;
using MySqlConnector;
using System.Data;

namespace API_WEB_SAE_6.Adapters
{
    /// <summary>
    /// 
    /// </summary>
    public class DeporteAdapter(string motorDB = "MySQL")
    {
        /// <summary>
        /// Define que tipo de base de datos se usa para consumir la informacion
        /// </summary>
        public string MotorDB { get; set; } = motorDB;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Deportes>? ObtenerDeportesCompleto()
        {
            string method = "ObtenerDeportesCompleto";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_DEPORTES_Listar_Deportes_Completo");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<Deportes> listadoDeportes = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        Deportes becario = new(row);
                        listadoDeportes.Add(becario);
                    }
                    return listadoDeportes;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "DeporteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Deportes>? ObtenerDeportesActivo()
        {
            string method = "ObtenerDeportesActivo";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_DEPORTES_Listar_Deportes_Activo");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<Deportes> listadoDeportes = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        Deportes becario = new(row);
                        listadoDeportes.Add(becario);
                    }
                    return listadoDeportes;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "DeporteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idDeporte"></param>
        /// <returns></returns>
        public Deportes? ObtenerDeportesXId(int idDeporte)
        {
            string method = "ObtenerDeportesActivo";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_deporte", MySqlDbType.Int32) { Value = idDeporte }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_DEPORTES_Buscar_Deporte_Id", parameters);

                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new Deportes(respuesta.Rows[0]);
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "DeporteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<EspaciosDeportivos>? ObtenerEspaciosDeporCompleto()
        {
            string method = "ObtenerEspaciosDeporCompleto";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_DEPORTES_Listar_Espacios_Deportivos_Completo");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<EspaciosDeportivos> listadoEspDeportes = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        EspaciosDeportivos esp = new(row);
                        listadoEspDeportes.Add(esp);
                    }
                    return listadoEspDeportes;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "DeporteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<EspaciosDeportivos>? ObtenerEspaciosDeporActivos()
        {
            string method = "ObtenerEspaciosDeporActivos";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_DEPORTES_Listar_Espacios_Deportivos_Activo");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<EspaciosDeportivos> listadoEspDeportes = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        EspaciosDeportivos esp = new(row);
                        listadoEspDeportes.Add(esp);
                    }
                    return listadoEspDeportes;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "DeporteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idEspacio"></param>
        /// <returns></returns>
        public EspaciosDeportivos? ObtenerEspaciosDeportXId(int idEspacio)
        {
            string method = "ObtenerEspaciosDeportXId";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_espacio", MySqlDbType.Int32) { Value = idEspacio }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_DEPORTES_Buscar_Espacio_Deportivos_Id", parameters);

                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new EspaciosDeportivos(respuesta.Rows[0]);
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "DeporteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<DocentesDeportivos>? ObtenerDocentesCompletos()
        {
            string method = "ObtenerDocentesCompletos";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_DEPORTES_Listar_Docente_Deporte_Completo");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<DocentesDeportivos> listadodocDeportes = [];
                    foreach (DataRow row in respuesta.Rows)
                        listadodocDeportes.Add(new(row));
                    return listadodocDeportes;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "DeporteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<DocentesDeportivos>? ObtenerDocentesActivos()
        {
            string method = "ObtenerDocentesActivos";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_DEPORTES_Listar_Docente_Deporte_Activos");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<DocentesDeportivos> listadodocDeportes = [];
                    foreach (DataRow row in respuesta.Rows)
                        listadodocDeportes.Add(new(row));
                    return listadodocDeportes;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "DeporteAdapter");
                return null;
            }
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cuil"></param>
        /// <returns></returns>
        public DocentesDeportivos? ObtenerDocentesXCUIL(string cuil)
        {
            string method = "ObtenerDocentesXCUIL";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    List<MySqlParameter> parameters = [new("i_cuil_doc", MySqlDbType.VarChar) { Value = cuil }];
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_DEPORTES_Buscar_Docente_Deportivo_Cuil", parameters);
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new DocentesDeportivos(respuesta.Rows[0]);
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "DeporteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Deportista>? ObtenerDeportistasCompletos()
        {
            string method = "ObtenerDeportistasCompletos";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_DEPORTES_Listar_Deportistas_Completo");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<Deportista> listadoDeportistas = [];
                    foreach (DataRow row in respuesta.Rows)
                        listadoDeportistas.Add(new(row));
                    return listadoDeportistas;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "DeporteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Deportista>? ObtenerDeportistasActivos()
        {
            string method = "ObtenerDeportistasActivos";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_DEPORTES_Listar_Deportistas_Habilitados");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<Deportista> listadoDeportistas = [];
                    foreach (DataRow row in respuesta.Rows)
                        listadoDeportistas.Add(new(row));
                    return listadoDeportistas;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "DeporteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idDepor"></param>
        /// <returns></returns>
        public Deportista? ObtenerDeportistasXId(int idDepor)
        {
            string method = "ObtenerDeportistasXId";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_deportista", MySqlDbType.Int32) { Value = idDepor }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_DEPORTES_Buscar_Deportistas_Id", parameters);

                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new Deportista(respuesta.Rows[0]);
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "DeporteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="legajo"></param>
        /// <returns></returns>
        public Deportista? ObtenerDeportistasXLegajo(string legajo)
        {
            string method = "ObtenerDeportistasXLegajo";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_legajo", MySqlDbType.VarChar) { Value = legajo }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_DEPORTES_Buscar_Deportistas_Legajo", parameters);

                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new Deportista(respuesta.Rows[0]);
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "DeporteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idDeporte"></param>
        /// <returns></returns>
        public List<Deportista>? ObtenerDeportistasXDeporte(int idDeporte)
        {
            string method = "ObtenerDeportistasXDeporte";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_deporte", MySqlDbType.Int32) { Value = idDeporte }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_DEPORTES_Buscar_Deportistas_Legajo", parameters);

                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<Deportista> listadoDeportistas = [];
                    foreach (DataRow row in respuesta.Rows)
                        listadoDeportistas.Add(new(row));
                    return listadoDeportistas;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "DeporteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idDeportista"></param>
        /// <returns></returns>
        public List<DeporteXInscripcion>? ObtenerInscriptosXDeportista(int idDeportista)
        {
            string method = "ObtenerInscriptosXDeportista";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_deportista", MySqlDbType.Int32) { Value = idDeportista }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_DEPORTES_Buscar_Inscripciones_Deportista", parameters);

                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<DeporteXInscripcion> listadoDeportistas = [];
                    foreach (DataRow row in respuesta.Rows)
                        listadoDeportistas.Add(new(row));
                    return listadoDeportistas;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "DeporteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TorneoDeportivo>? ObtenerTorneosCompleto()
        {
            string method = "ObtenerTorneosCompleto";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_DEPORTES_Listar_Torneos_Completo");

                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<TorneoDeportivo> listadoTorneos = [];
                    foreach (DataRow row in respuesta.Rows)
                        listadoTorneos.Add(new(row));
                    return listadoTorneos;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "DeporteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TorneoDeportivo>? ObtenerTorneosActivos()
        {
            string method = "ObtenerTorneosActivo";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_DEPORTES_Listar_Torneos_Completo");

                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<TorneoDeportivo> listadoTorneos = [];
                    foreach (DataRow row in respuesta.Rows)
                        listadoTorneos.Add(new(row));
                    return listadoTorneos;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "DeporteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idTorneo"></param>
        /// <returns></returns>
        public TorneoDeportivo? ObtenerTorneoXId(int idTorneo)
        {
            string method = "ObtenerTorneoXId";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_torneo", MySqlDbType.Int32) { Value = idTorneo }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_DEPORTES_Buscar_Torneos_Id", parameters);

                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new TorneoDeportivo(respuesta.Rows[0]);
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "DeporteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idDeporte"></param>
        /// <returns></returns>
        public List<TorneoDeportivo>? ObtenerTorneosXDeporte(int idDeporte)
        {
            string method = "ObtenerTorneosXDeporte";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_deporte", MySqlDbType.Int32) { Value = idDeporte }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_DEPORTES_Buscar_Torneos_Deportes", parameters);
                    
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    List<TorneoDeportivo> listadoTorneos = [];
                    foreach (DataRow row in respuesta.Rows)
                        listadoTorneos.Add(new(row));
                    return listadoTorneos;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "DeporteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idTorneo"></param>
        /// <returns></returns>
        public List<Deportista>? ObtenerDeportistasXTorneo(int idTorneo)
        {
            string method = "ObtenerDeportistasXTorneo";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_torneo", MySqlDbType.Int32) { Value = idTorneo }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_DEPORTES_Buscar_Inscriptos_Torneo", parameters);

                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    List<Deportista> listadoTorneos = [];
                    foreach (DataRow row in respuesta.Rows)
                        listadoTorneos.Add(new(row));
                    return listadoTorneos;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "DeporteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idDeportista"></param>
        /// <returns></returns>
        public List<TorneosXInscripcion>? ObtenerInscTorneoXDeportista(int idDeportista)
        {
            string method = "ObtenerInscTorneoXDeportista";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_deportista", MySqlDbType.Int32) { Value = idDeportista }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_DEPORTES_Buscar_Torneos_Deportista", parameters);

                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    List<TorneosXInscripcion> listadoTorneos = [];
                    foreach (DataRow row in respuesta.Rows)
                        listadoTorneos.Add(new(row));
                    return listadoTorneos;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "DeporteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<HorarioDeportes>? ObtenerHorariosCompleto()
        {
            string method = "ObtenerHorariosCompleto";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_DEPORTES_Listar_Horarios_Completo");

                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    List<HorarioDeportes> listadoHorarios = [];
                    foreach (DataRow row in respuesta.Rows)
                        listadoHorarios.Add(new(row));
                    return listadoHorarios;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "DeporteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<HorarioDeportes>? ObtenerHorariosActivo()
        {
            string method = "ObtenerHorariosActivo";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_DEPORTES_Listar_Horarios_Activos");

                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    List<HorarioDeportes> listadoHorarios = [];
                    foreach (DataRow row in respuesta.Rows)
                        listadoHorarios.Add(new(row));
                    return listadoHorarios;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "DeporteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idHorario"></param>
        /// <returns></returns>
        public HorarioDeportes? ObtenerHorarioXId(int idHorario)
        {
            string method = "ObtenerHorarioXId";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_horario", MySqlDbType.Int32) { Value = idHorario }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_DEPORTES_Buscar_Horarios_Id", parameters);

                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new HorarioDeportes(respuesta.Rows[0]);
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "DeporteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idEspacio"></param>
        /// <returns></returns>
        public List<HorarioDeportes>? ObtenerHorariosXEspacio(int idEspacio)
        {
            string method = "ObtenerHorarioXEspacio";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_espacio", MySqlDbType.Int32) { Value = idEspacio }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_DEPORTES_Buscar_Horarios_Espacio", parameters);
                    
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    List<HorarioDeportes> listadoHorarios = [];
                    foreach (DataRow row in respuesta.Rows)
                        listadoHorarios.Add(new(row));
                    return listadoHorarios;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "DeporteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idDeporte"></param>
        /// <returns></returns>
        public List<HorarioDeportes>? ObtenerHorariosXDeporte(int idDeporte)
        {
            string method = "ObtenerHorariosXDeporte";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_deporte", MySqlDbType.Int32) { Value = idDeporte }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_DEPORTES_Buscar_Horarios_Deporte", parameters);

                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    List<HorarioDeportes> listadoHorarios = [];
                    foreach (DataRow row in respuesta.Rows)
                        listadoHorarios.Add(new(row));
                    return listadoHorarios;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "DeporteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cuil_Docente"></param>
        /// <returns></returns>
        public List<HorarioDeportes>? ObtenerHorariosXCUIL(string cuil_Docente)
        {
            string method = "ObtenerHorariosXCUIL";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_cuil", MySqlDbType.VarChar) { Value = cuil_Docente }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_DEPORTES_Buscar_Horarios_Cuil", parameters);

                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    List<HorarioDeportes> listadoHorarios = [];
                    foreach (DataRow row in respuesta.Rows)
                        listadoHorarios.Add(new(row));
                    return listadoHorarios;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "DeporteAdapter");
                return null;
            }
        }
    }
}
