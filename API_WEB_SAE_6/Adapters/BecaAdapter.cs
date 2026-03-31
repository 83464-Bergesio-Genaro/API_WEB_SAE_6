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
    public class BecaAdapter(string motorDB = "MySQL")
    {
        /// <summary>
        /// Define que tipo de base de datos se usa para consumir la informacion
        /// </summary>
        public string MotorDB { get; set; } = motorDB;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<BecariosSAE>? ObtenerBecariosCompleto()
        {
            string method = "ObtenerBecariosCompleto";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_BECAS_Listar_Becarios_SAE");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<BecariosSAE> listadoBecario = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        BecariosSAE becario = new(row);
                        listadoBecario.Add(becario);
                    }
                    return listadoBecario;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "BecaAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<BecariosSAE>? ObtenerBecariosActivo()
        {
            string method = "ObtenerBecariosActivo";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_BECAS_Listar_Becarios_SAE_Activos");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<BecariosSAE> listadoBecario = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        BecariosSAE becario = new(row);
                        listadoBecario.Add(becario);
                    }
                    return listadoBecario;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "BecaAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BecariosSAE? BuscarBecarioXID(int id)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_becario", MySqlDbType.Int32) { Value = id }];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_BECAS_Buscar_Becarios_SAE_Id", parameters);
                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "BuscarBecarioXID", ex.Message, "BecaAdapter");
                    return null;
                }
            }
            else return new();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="legajo"></param>
        /// <returns></returns>
        public BecariosSAE? BuscarBecarioXLegajo(string legajo)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_legajo", MySqlDbType.VarChar) { Value = legajo }];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_BECAS_Buscar_Becarios_SAE_Legajo", parameters);
                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "BuscarBecarioXLegajo", ex.Message, "BecaAdapter");
                    return null;
                }
            }
            else return new();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<BecariosSAEEconomica>? ObtenerBecariosEconomica()
        {
            string method = "ObtenerBecariosEconomico";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_BECAS_Listar_Becarios_Economica");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<BecariosSAEEconomica> listadoBecario = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        BecariosSAEEconomica becario = new(row);
                        listadoBecario.Add(becario);
                    }
                    return listadoBecario;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "BecaAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<BecariosSAEEconomica>? ObtenerBecariosEconomicaActivos()
        {
            string method = "ObtenerBecariosEconomicaActivos";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_BECAS_Listar_Becarios_Economica_Activo");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<BecariosSAEEconomica> listadoBecario = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        BecariosSAEEconomica becario = new(row);
                        listadoBecario.Add(becario);
                    }
                    return listadoBecario;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "BecaAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BecariosSAEEconomica? BuscarBecarioEconomicaXID(int id)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_becario", MySqlDbType.Int32) { Value = id }];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_BECAS_Buscar_Becarios_Economica_Id_becario", parameters);
                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "BuscarBecarioEconomicaXID", ex.Message, "BecaAdapter");
                    return null;
                }
            }
            else return new();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="legajo"></param>
        /// <returns></returns>
        public BecariosSAEEconomica? BuscarBecarioEconomicaXLegajo(string legajo)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_legajo", MySqlDbType.VarChar) { Value = legajo }];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_BECAS_Buscar_Becarios_Economica_legajo", parameters);
                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "BuscarBecarioEconomicaXLegajo", ex.Message, "BecaAdapter");
                    return null;
                }
            }
            else return new();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<BecariosSAEInvestigacion>? ObtenerBecariosInvestigacion()
        {
            string method = "ObtenerBecariosInvestigacion";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_BECAS_Listar_Becarios_Investigacion");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<BecariosSAEInvestigacion> listadoBecario = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        BecariosSAEInvestigacion becario = new(row);
                        listadoBecario.Add(becario);
                    }
                    return listadoBecario;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "BecaAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<BecariosSAEInvestigacion>? ObtenerBecariosInvestigacionActivos()
        {
            string method = "ObtenerBecariosInvestigacionActivos";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_BECAS_Listar_Becarios_Investigacion_Activos");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<BecariosSAEInvestigacion> listadoBecario = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        BecariosSAEInvestigacion becario = new(row);
                        listadoBecario.Add(becario);
                    }
                    return listadoBecario;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "BecaAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BecariosSAEInvestigacion? BuscarBecarioInvestigacionXID(int id)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_becario", MySqlDbType.Int32) { Value = id }];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_BECAS_Buscar_Becarios_Investigacion_Id_becario", parameters);
                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "BuscarBecarioInvestigacionXID", ex.Message, "BecaAdapter");
                    return null;
                }
            }
            else return new();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="legajo"></param>
        /// <returns></returns>
        public BecariosSAEInvestigacion? BuscarBecarioInvestigacionXLegajo(string legajo)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_legajo", MySqlDbType.VarChar) { Value = legajo }];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_BECAS_Buscar_Becarios_Investigacion_Legajo", parameters);
                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "BuscarBecarioInvestigacionXLegajo", ex.Message, "BecaAdapter");
                    return null;
                }
            }
            else return new();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<BecariosSAEServicio>? ObtenerBecariosServicio()
        {
            string method = "ObtenerBecariosServicio";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_BECAS_Listar_Becarios_Servicios");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<BecariosSAEServicio> listadoBecario = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        BecariosSAEServicio becario = new(row);
                        listadoBecario.Add(becario);
                    }
                    return listadoBecario;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "BecaAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<BecariosSAEServicio>? ObtenerBecariosServicioActivos()
        {
            string method = "ObtenerBecariosServicioActivos";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_BECAS_Listar_Becarios_Servicios_Activos");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<BecariosSAEServicio> listadoBecario = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        BecariosSAEServicio becario = new(row);
                        listadoBecario.Add(becario);
                    }
                    return listadoBecario;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "BecaAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BecariosSAEServicio? BuscarBecarioServicioXID(int id)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_becario", MySqlDbType.Int32) { Value = id }];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_BECAS_Buscar_Becarios_Servicios_Id_becario", parameters);
                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "BuscarUsuarioXID", ex.Message, "BecaAdapter");
                    return null;
                }
            }
            else return new();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="legajo"></param>
        /// <returns></returns>
        public BecariosSAEServicio? BuscarBecarioServicioXLegajo(string legajo)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_legajo", MySqlDbType.VarChar) { Value = legajo }];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_BECAS_Buscar_Becarios_Servicios_Legajo", parameters);
                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "BuscarBecarioServicioXLegajo", ex.Message, "BecaAdapter");
                    return null;
                }
            }
            else return new();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<BecariosNacionales>? ObtenerBecariosNacionales()
        {
            string method = "ObtenerBecariosNacionales";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_BECAS_Listar_Becarios_Nacional");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<BecariosNacionales> listadoBecario = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        BecariosNacionales becario = new(row);
                        listadoBecario.Add(becario);
                    }
                    return listadoBecario;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "BecaAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BecariosNacionales? BuscarBecarioNacionalXID(int id)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_becario", MySqlDbType.Int32) { Value = id }];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_BECAS_Buscar_Becario_Nacional_Id", parameters);
                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "BuscarUsuarioXID", ex.Message, "BecaAdapter");
                    return null;
                }
            }
            else return new();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="legajo"></param>
        /// <returns></returns>
        public BecariosNacionales? BuscarBecarioNacionalXLegajo(string legajo)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_legajo", MySqlDbType.VarChar) { Value = legajo }];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_BECAS_Buscar_Becario_Nacional_Legajo", parameters);
                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "BuscarUsuarioXID", ex.Message, "BecaAdapter");
                    return null;
                }
            }
            else return new();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ProyectosInvestigacion>? ObtenerProyectos()
        {
            string method = "ObtenerProyectos";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_BECAS_Listar_Proyectos_Investigacion");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<ProyectosInvestigacion> listadoBecario = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        ProyectosInvestigacion becario = new(row);
                        listadoBecario.Add(becario);
                    }
                    return listadoBecario;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "BecaAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ServiciosInternosFacultad>? ObtenerServiciosDisponibles()
        {
            string method = "ObtenerProyectos";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_BECAS_Listar_Servicios");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<ServiciosInternosFacultad> listadoBecario = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        ServiciosInternosFacultad becario = new(row);
                        listadoBecario.Add(becario);
                    }
                    return listadoBecario;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "BecaAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="legajo"></param>
        /// <returns></returns>
        public List<SituacionesAcademicas>? BuscarSituacionesAcademicasXLegajo(string legajo)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_legajo", MySqlDbType.VarChar) { Value = legajo }];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_BECAS_Buscar_Situaciones_Academicas_Legajo", parameters);
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<SituacionesAcademicas> listadoSituaciones = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        SituacionesAcademicas becario = new(row);
                        listadoSituaciones.Add(becario);
                    }
                    return listadoSituaciones;
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "BuscarUsuarioXID", ex.Message, "BecaAdapter");
                    return null;
                }
            }
            else return [];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="becario"></param>
        /// <param name="idUserMod"></param>
        /// <returns></returns>
        public BecariosSAE ModificarBecario(BecariosSAE becario, int idUserMod)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_id_becario", MySqlDbType.Int32) { Value = becario.id },
                        new("i_legajo", MySqlDbType.VarChar) { Value = becario.legajo },
                        new("i_alquila", MySqlDbType.Bit) { Value = becario.alquila },
                        new("i_fecha_solicitud", MySqlDbType.Date) { Value = becario.fecha_solicitud },
                        new("i_aceptado", MySqlDbType.Bit) { Value = becario.aceptado_inicio },
                        new("i_puede_pagarle", MySqlDbType.Bit) { Value = becario.puede_pagarle },
                        new("i_activo", MySqlDbType.Bit) { Value = becario.activo },
                        new("i_anio_beca", MySqlDbType.Int32) { Value = becario.anio_beca },
                        new("i_id_becario_previo", MySqlDbType.Int32) { Value = becario.id_becario_previo },
                        new("i_id_usuario_mod", MySqlDbType.Int32) { Value = idUserMod }
                        ];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_BECAS_Modificar_Becario_SAE", parameters);
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "ModificarBecario", ex.Message, "BecaAdapter");
                    return new();
                }

            }
            else return new();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="becario"></param>
        /// <param name="idUserMod"></param>
        /// <returns></returns>
        public BecariosSAEEconomica ModificarBecarioEconomica(BecariosSAEEconomica becario, int idUserMod)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_id_economica", MySqlDbType.Int32) { Value = becario.id },
                        new("i_id_becario", MySqlDbType.Int32) { Value = becario.becario.id },
                        new("i_entrevista", MySqlDbType.Bit) { Value = becario.entrevista_realizada },
                        new("i_modulos", MySqlDbType.Int32) { Value = becario.modulos_asignados },
                        new("i_id_usuario_mod", MySqlDbType.Int32) { Value = idUserMod }
                        ];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_BECAS_Modificar_Becario_Economica", parameters);
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "ModificarBecarioEconomica", ex.Message, "BecaAdapter");
                    return new();
                }
            }
            else return new();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="becario"></param>
        /// <param name="idUserMod"></param>
        /// <returns></returns>
        public BecariosSAEInvestigacion ModificarBecarioInvestigacion(BecariosSAEInvestigacion becario, int idUserMod)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_id_investigacion", MySqlDbType.Int32) { Value = becario.id },
                        new("i_id_becario", MySqlDbType.Int32) { Value = becario.becario.id },
                        new("i_id_proyecto", MySqlDbType.Int32) { Value = becario.proyecto_investigacion?.id },
                        new("i_modulos", MySqlDbType.Int32) { Value = becario.modulos_asignados },
                        new("i_id_usuario_mod", MySqlDbType.Int32) { Value = idUserMod }
                        ];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_BECAS_Modificar_Becario_Investigacion", parameters);
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "ModificarBecarioInvestigacion", ex.Message, "BecaAdapter");
                    return new();
                }

            }
            else return new();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="becario"></param>
        /// <param name="idUserMod"></param>
        /// <returns></returns>
        public BecariosSAEServicio ModificarBecarioServicio(BecariosSAEServicio becario, int idUserMod)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_id_beca_servicio", MySqlDbType.Int32) { Value = becario.id },
                        new("i_id_becario", MySqlDbType.Int32) { Value = becario.becario.id },
                        new("i_id_servicio", MySqlDbType.Int32) { Value = becario.servicio?.id },
                        new("i_modulos", MySqlDbType.Int32) { Value = becario.modulos_asignados },
                        new("i_id_usuario_mod", MySqlDbType.Int32) { Value = idUserMod }
                        ];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_BECAS_Modificar_Becario_Servicio", parameters);
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "ModificarBecarioServicio", ex.Message, "BecaAdapter");
                    return new();
                }
            }
            else return new();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="becario"></param>
        /// <param name="idUserMod"></param>
        /// <returns></returns>
        public BecariosNacionales ModificarBecarioNacional(BecariosNacionales becario, int idUserMod)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_id_becario", MySqlDbType.Int32) { Value = becario.id },
                        new("i_legajo", MySqlDbType.Int32) { Value = becario.legajo },
                        new("i_tipo_plan", MySqlDbType.Int32) { Value = becario.tipo_plan },
                        new("i_regular", MySqlDbType.Bit) { Value = becario.regularizacion },
                        new("i_cumplio", MySqlDbType.Bit) { Value = becario.cumplimiento_servicio },
                        new("i_anio_beca", MySqlDbType.Int32) { Value = becario.anio_beca },
                        new("i_activo", MySqlDbType.Bit) { Value = becario.activo },
                        new("i_id_usuario_mod", MySqlDbType.Int32) { Value = idUserMod }
                        ];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_BECAS_Modificar_Becario_Nacional", parameters);
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "ModificarBecarioNacional", ex.Message, "BecaAdapter");
                    return new();
                }

            }
            else return new();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="proyecto"></param>
        /// <returns></returns>
        public ProyectosInvestigacion ModificarProyecto(ProyectosInvestigacion proyecto)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_id_proyecto", MySqlDbType.Int32) { Value = proyecto.id },
                        new("i_nombre", MySqlDbType.VarChar) { Value = proyecto.nombre_proyecto_investigacion },
                        new("i_centro", MySqlDbType.VarChar) { Value = proyecto.centro_investigacion },
                        new("i_activo", MySqlDbType.Bit) { Value = proyecto.activo }
                        ];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_BECAS_Modificar_Proyecto_Investigacion", parameters);
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "ModificarProyecto", ex.Message, "BecaAdapter");
                    return new();
                }
            }
            else return new();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="situacion"></param>
        /// <param name="idUserMod"></param>
        /// <returns></returns>
        public SituacionesAcademicas ModificarSituacionAcademica(SituacionesAcademicas situacion, int idUserMod)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_id_situacion", MySqlDbType.Int32) { Value = situacion.id },
                        new("i_legajo", MySqlDbType.VarChar) { Value = situacion.legajo },
                        new("i_cursando", MySqlDbType.Bit) { Value = situacion.cursando },
                        new("i_anio_situacion", MySqlDbType.Int32) { Value = situacion.anio_situacion },
                        new("i_cant_cur_anterior", MySqlDbType.Int32) { Value = situacion.cant_materias_cursadas_anterior },
                        new("i_cant_aprob_anterior", MySqlDbType.Int32) { Value = situacion.cant_materias_aprobadas_periodo_anterior },
                        new("i_cant_cur", MySqlDbType.Int32) { Value = situacion.cant_materias_cursando },
                        new("i_cant_aprob", MySqlDbType.Int32) { Value = situacion.cant_materias_aprobadas_total },
                        new("i_prom_con_apla", MySqlDbType.Decimal) { Value = situacion.prom_gral_con_aplazos},
                        new("i_anio_ingre", MySqlDbType.Int32) { Value = situacion.anio_situacion },
                        new("i_prom_sin_apla", MySqlDbType.Decimal) { Value = situacion.prom_gral_sin_aplazos },
                        new("i_id_usuario_mod", MySqlDbType.Int32) { Value = idUserMod }
                        ];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_BECAS_Modificar_Situacion_Academica", parameters);
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "ModificarSituacionAcademica", ex.Message, "BecaAdapter");
                    return new();
                }
            }
            else return new();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="becario"></param>
        /// <param name="idUserCreacion"></param>
        /// <returns></returns>
        public BecariosSAE CrearBecario(BecariosSAE becario, int idUserCreacion)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_legajo", MySqlDbType.VarChar) { Value = becario.legajo },
                        new("i_alquila", MySqlDbType.Bit) { Value = becario.alquila },
                        new("i_fecha_solicitud", MySqlDbType.Date) { Value = becario.fecha_solicitud },
                        new("i_anio_beca", MySqlDbType.Int32) { Value = becario.anio_beca },
                        new("i_id_becario_previo", MySqlDbType.Int32) { Value = becario.id_becario_previo },
                        new("i_id_usuario_alta", MySqlDbType.Int32) { Value = idUserCreacion }
                        ];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_BECAS_Crear_Becario_SAE", parameters);

                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearBecario", ex.Message, "BecaAdapter");
                    return new();
                }
            }
            else return new();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idBecario"></param>
        /// <param name="idUserCreacion"></param>
        /// <returns></returns>
        public BecariosSAEEconomica CrearBecarioEconomica(int idBecario, int idUserCreacion)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_id_becario", MySqlDbType.Int32) { Value = idBecario},
                        new("i_id_usuario_alta", MySqlDbType.Int32) { Value = idUserCreacion }
                        ];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_BECAS_Crear_Becario_Economica", parameters);

                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearBecario", ex.Message, "BecaAdapter");
                    return new();
                }
            }
            else return new();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idBecario"></param>
        /// <param name="idUserCreacion"></param>
        /// <returns></returns>
        public BecariosSAEInvestigacion CrearBecarioInvestigacion(int idBecario, int idUserCreacion)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_id_becario", MySqlDbType.Int32) { Value = idBecario},
                        new("i_id_usuario_alta", MySqlDbType.Int32) { Value = idUserCreacion }
                        ];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_BECAS_Crear_Becario_Investigacion", parameters);

                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearBecarioInvestigacion", ex.Message, "BecaAdapter");
                    return new();
                }
            }
            else return new();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idBecario"></param>
        /// <param name="idUserCreacion"></param>
        /// <returns></returns>
        public BecariosSAEServicio CrearBecarioServicio(int idBecario, int idUserCreacion)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_id_becario", MySqlDbType.Int32) { Value = idBecario},
                        new("i_id_usuario_alta", MySqlDbType.Int32) { Value = idUserCreacion }
                        ];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_BECAS_Crear_Becario_Servicio", parameters);

                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearBecarioServicio", ex.Message, "BecaAdapter");
                    return new();
                }
            }
            else return new();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="becario"></param>
        /// <param name="idUserCreacion"></param>
        /// <returns></returns>
        public BecariosNacionales CrearBecarioNacional(BecariosNacionales becario, int idUserCreacion)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_legajo", MySqlDbType.Int32) { Value = becario.legajo },
                        new("i_tipo_plan", MySqlDbType.Int32) { Value = becario.tipo_plan },
                        new("i_regular", MySqlDbType.Bit) { Value = becario.regularizacion },
                        new("i_cumplio", MySqlDbType.Bit) { Value = becario.cumplimiento_servicio },
                        new("i_anio_beca", MySqlDbType.Int32) { Value = becario.anio_beca },
                        new("i_activo", MySqlDbType.Bit) { Value = becario.activo },
                        new("i_id_usuario_alta", MySqlDbType.Int32) { Value = idUserCreacion }
                        ];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_BECAS_Crear_Becario_Nacional", parameters);

                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearBecarioNacional", ex.Message, "BecaAdapter");
                    return new();
                }
            }
            else return new();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="proyecto"></param>
        /// <returns></returns>
        public ProyectosInvestigacion CrearProyecto(ProyectosInvestigacion proyecto)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_nombre", MySqlDbType.VarChar) { Value = proyecto.nombre_proyecto_investigacion},
                        new("i_activo", MySqlDbType.Bit) { Value = proyecto.activo },
                        new("i_centro_inv", MySqlDbType.VarChar) { Value = proyecto.centro_investigacion }
                        ];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_BECAS_Crear_Proyecto_Investigacion", parameters);

                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearProyectoInv", ex.Message, "BecaAdapter");
                    return new();
                }
            }
            else return new();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="situacion"></param>
        /// <param name="idUserCreacion"></param>
        /// <returns></returns>
        public SituacionesAcademicas CrearSituacionAcademica(SituacionesAcademicas situacion, int idUserCreacion)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_legajo", MySqlDbType.VarChar) { Value = situacion.legajo },
                        new("i_cursando", MySqlDbType.Bit) { Value = situacion.cursando },
                        new("i_anio_situacion", MySqlDbType.Int32) { Value = situacion.anio_situacion },
                        new("i_cant_cur_anterior", MySqlDbType.Int32) { Value = situacion.cant_materias_cursadas_anterior },
                        new("i_cant_aprob_anterior", MySqlDbType.Int32) { Value = situacion.cant_materias_aprobadas_periodo_anterior },
                        new("i_cant_cur", MySqlDbType.Int32) { Value = situacion.cant_materias_cursando },
                        new("i_cant_aprob", MySqlDbType.Int32) { Value = situacion.cant_materias_aprobadas_total },
                        new("i_prom_con_apla", MySqlDbType.Decimal) { Value = situacion.prom_gral_con_aplazos },
                        new("i_anio_ingre", MySqlDbType.Int32) { Value = situacion.anio_situacion },
                        new("i_prom_sin_apla", MySqlDbType.Decimal) { Value = situacion.prom_gral_sin_aplazos },
                        new("i_id_usuario_alta", MySqlDbType.Int32) { Value = idUserCreacion }
                       ];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_BECAS_Crear_Situacion_Academica", parameters);

                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearSituacion", ex.Message, "BecaAdapter");
                    return new();
                }
            }
            else return new();
        }
        private static int GetDecimalForBase(decimal valor)
        {
            valor = Math.Round(valor * 100, 0);
            return int.Parse(valor.ToString());//En la version de ms me hacia renegar con las comas asi que le saco las comas y lo paso como entero
        }
    }
}
