using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models.Viaje;
using MySqlConnector;
using System.Data;

namespace API_WEB_SAE_6.Adapters
{
    /// <summary>
    /// 
    /// </summary>
    public class ViajeAdapter(string motorDB = "MySQL")
    {
        /// <summary>
        /// Define que tipo de base de datos se usa para consumir la informacion
        /// </summary>
        public string MotorDB { get; set; } = motorDB;
    
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Viajes>? ObtenerViajesCompleto()
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_VIAJES_Listar_Viajes_Completo");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<Viajes> listadoViajes = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        Viajes eventos = new(row);
                        listadoViajes.Add(eventos);
                    }
                    return listadoViajes;

                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "ObtenerViajesCompleto", ex.Message, "ViajeAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Viajes>? ObtenerViajesActivo()
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_VIAJES_Listar_Viajes_Activos");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<Viajes> listadoEventosCompleto = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        Viajes eventos = new(row);
                        listadoEventosCompleto.Add(eventos);
                    }
                    return listadoEventosCompleto;

                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "ObtenerViajesActivo", ex.Message, "ViajeAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Viajes>? ObtenerViajesXLegajo(string legajo)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    List<MySqlParameter> parameters = [new("i_legajo", MySqlDbType.Int32) { Value = legajo }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_VIAJES_Buscar_Viajes_Legajo", parameters);

                    List<Viajes> listadoEventosCompleto = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        Viajes eventos = new(row);
                        listadoEventosCompleto.Add(eventos);
                    }
                    return listadoEventosCompleto;

                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "ObtenerViajesXLegajo", ex.Message, "ViajeAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Viajes? ObtenerViajesXId(int idViaje)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    List<MySqlParameter> parameters = [new("i_id_viaje", MySqlDbType.Int32) { Value = idViaje }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_VIAJES_Buscar_Viajes_Id", parameters);

                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new Viajes(respuesta.Rows[0]);

                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "ObtenerViajesXId", ex.Message, "ViajeAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viaje"></param>
        /// <param name="idUsuarioActual"></param>
        /// <returns></returns>
        public Viajes CrearViajeSAE(Viajes viaje,int idUsuarioActual)
        {
            //Inicializa un valor y le asigna el tipo
            List<MySqlParameter> parameters = [
                    new("i_nombre", MySqlDbType.VarChar) { Value = viaje.nombre },
                    new("i_fecha_inicio", MySqlDbType.Date) { Value = viaje.fecha_inicio.ToString("yyyy-MM-dd") },
                    new("i_fecha_fin", MySqlDbType.Date) { Value = viaje.fecha_fin.ToString("yyyy-MM-dd")  },
                    new("i_seguro_confirmado", MySqlDbType.Bit) { Value = viaje.seguro_confirmado },

                    new("i_origen", MySqlDbType.VarChar) { Value = viaje.origen },
                    new("i_destino", MySqlDbType.Date) { Value = viaje.destino },
                    new("i_motivo", MySqlDbType.Date) { Value = viaje.motivo  },
                    new("i_cantidad_personas", MySqlDbType.Int32) { Value = viaje.cantidad_personas },

                    new("i_id_empresa_viaje", MySqlDbType.Int32) { Value = viaje.id_empresa_viaje },
                    new("i_documentacion_presentada", MySqlDbType.Date) { Value = viaje.documentacion_presentada },
                    new("i_costo_aproximado", MySqlDbType.Decimal) { Value = viaje.costo_aproximado  },
                    new("i_id_usuario_alta", MySqlDbType.Int32) { Value = idUsuarioActual }];
            GeneralAdapterMySQL consult = new();
            DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_VIAJES_Crear_Viaje", parameters);
            if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
            else return new(respuesta.Rows[0]);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viaje"></param>
        /// <param name="idUsuarioActual"></param>
        /// <returns></returns>
        public Viajes ModificarViajeSAE(Viajes viaje, int idUsuarioActual)
        {
            //Inicializa un valor y le asigna el tipo
            List<MySqlParameter> parameters = [
                new("i_id_viaje", MySqlDbType.Int32) { Value = viaje.id },
                new("i_nombre", MySqlDbType.VarChar) { Value = viaje.nombre },
                new("i_fecha_inicio", MySqlDbType.Date) { Value = viaje.fecha_inicio.ToString("yyyy-MM-dd") },
                new("i_fecha_fin", MySqlDbType.Date) { Value = viaje.fecha_fin.ToString("yyyy-MM-dd")  },
                new("i_seguro_confirmado", MySqlDbType.Bit) { Value = viaje.seguro_confirmado },

                new("i_origen", MySqlDbType.VarChar) { Value = viaje.origen },
                new("i_destino", MySqlDbType.Date) { Value = viaje.destino },
                new("i_motivo", MySqlDbType.Date) { Value = viaje.motivo  },
                new("i_cantidad_personas", MySqlDbType.Int32) { Value = viaje.cantidad_personas },

                new("i_id_empresa_viaje", MySqlDbType.Int32) { Value = viaje.id_empresa_viaje },
                new("i_documentacion_presentada", MySqlDbType.Date) { Value = viaje.documentacion_presentada },
                new("i_costo_aproximado", MySqlDbType.Decimal) { Value = viaje.costo_aproximado  },
                new("i_id_usuario_modificacion", MySqlDbType.Int32) { Value = idUsuarioActual }];
            GeneralAdapterMySQL consult = new();
            DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_VIAJES_Modificar_Viaje", parameters);
            if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
            else return new(respuesta.Rows[0]);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<ViajeXInscripcion>? ObtenerInscriptosXViaje(int idViaje)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    List<MySqlParameter> parameters = [new("i_id_viaje", MySqlDbType.Int32) { Value = idViaje }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_VIAJES_Buscar_Inscriptos_Viaje", parameters);

                    List<ViajeXInscripcion> listadoInscriptos = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        ViajeXInscripcion eventos = new(row);
                        listadoInscriptos.Add(eventos);
                    }
                    return listadoInscriptos;

                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "ObtenerViajesXLegajo", ex.Message, "ViajeAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inscrip"></param>
        /// <returns></returns>
        public ViajeXInscripcion CrearInscripcionViaje(ViajeXInscripcion inscrip)
        {
            //Inicializa un valor y le asigna el tipo
            List<MySqlParameter> parameters = [
                    new("i_id_viaje", MySqlDbType.VarChar) { Value = inscrip.id_viaje },
                    new("i_legajo_estudiante ", MySqlDbType.Date) { Value = inscrip.legajo_estudiante }];

            GeneralAdapterMySQL consult = new();
            DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_VIAJES_Crear_Inscriptos_Viaje", parameters);
            if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
            else return new(respuesta.Rows[0]);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viaje"></param>
        /// <returns></returns>
        public ViajeXInscripcion ModificarInscriptoViaje(ViajeXInscripcion viaje)
        {
            //Inicializa un valor y le asigna el tipo
            List<MySqlParameter> parameters = [
                new("i_id_inscripcion", MySqlDbType.Int32) { Value = viaje.id },
                new("i_documentacion_presentada", MySqlDbType.Int32) { Value = viaje.documentacion_presentada }];
            GeneralAdapterMySQL consult = new();
            DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_VIAJES_Modificar_Inscriptos_Viaje", parameters);
            if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
            else return new(respuesta.Rows[0]);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idEvento"></param>
        /// <returns></returns>
        public bool EliminarInscripcionViaje(int idEvento)
        {
            List<MySqlParameter> parameters = [new("i_id_inscripcion", MySqlDbType.Int32) { Value = idEvento }];

            GeneralAdapterMySQL consult = new();
            DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_VIAJES_Eliminar_Inscriptos_Viaje", parameters);

            if (respuesta.Rows.Count > 0) return false;
            else return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idDoc"></param>
        /// <returns></returns>
        public DocumentosViaje? BuscarDocumentoXId(int idDoc)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_doc", MySqlDbType.Int32) { Value = idDoc }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_VIAJES_Buscar_Documento_Id", parameters);

                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new(respuesta.Rows[0]);
                }
                else return new();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "BuscarDocumentoXId", ex.Message, "ViajeAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idViaje"></param>
        /// <returns></returns>
        public List<DocumentosViaje>? BuscarDocumentosXViaje(int idViaje)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_viaje", MySqlDbType.VarChar) { Value = idViaje }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_VIAJES_Buscar_Documento_Viaje", parameters);

                    if (respuesta.Rows.Count == 0) return [];
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else
                    {
                        List<DocumentosViaje> listadoViaje = [];
                        foreach (DataRow row in respuesta.Rows)
                        {
                            DocumentosViaje documentos = new(row);
                            listadoViaje.Add(documentos);
                        }
                        return listadoViaje;
                    }
                }
                else return [];
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "BuscarDocumentoXId", ex.Message, "ViajeAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DocumentosViaje ModificarDocumento(DocumentosViaje documento, string idMod)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                    new("i_id_doc", MySqlDbType.Int32) { Value = documento.id },
                    new("i_id_viaje", MySqlDbType.VarChar) { Value = documento.id_viaje },
                    new("i_id_tipo", MySqlDbType.Int32) { Value = documento.id_tipo_documento },
                    new("i_nombre", MySqlDbType.VarChar) { Value = documento.nombre_documento },
                    new("i_tamanio", MySqlDbType.Int32) { Value = documento.tamanio },
                    new("i_ruta", MySqlDbType.VarChar) { Value = documento.ruta },
                    new("i_id_usuario_mod", MySqlDbType.Int32) { Value = idMod}];

                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_VIAJES_Modificar_Documento_Viaje", parameters);

                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                else return new();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "ModificarDocumento", ex.Message, "ViajeAdapter");
                return new();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DocumentosViaje CrearDocumento(DocumentosViaje documento, int idCrea)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                    new("i_id_viaje", MySqlDbType.VarChar) { Value = documento.id_viaje },
                    new("i_id_tipo", MySqlDbType.Int32) { Value = documento.id_tipo_documento },
                    new("i_nombre", MySqlDbType.VarChar) { Value = documento.nombre_documento },
                    new("i_tamanio", MySqlDbType.Int32) { Value = documento.tamanio },
                    new("i_ruta", MySqlDbType.VarChar) { Value = documento.ruta },
                    new("i_id_usuario_alta", MySqlDbType.Int32) { Value = idCrea}];

                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_VIAJES_Crear_Documento_Viaje", parameters);

                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                else return new();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearDocumento", ex.Message, "ViajeAdapter");
                return new();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idDocumento"></param>
        /// <returns></returns>
        public bool EliminarDocumento(int idDocumento)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    List<MySqlParameter> parameters = [new("i_id_doc", MySqlDbType.Int32) { Value = idDocumento }];
                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_VIAJES_Eliminar_Documento_Viaje", parameters);
                    if (respuesta.Rows.Count > 0)
                    {
                        if (respuesta.Rows[0][0].ToString() == "ERROR") Logger.RegistrarDatos(Logger.LogOptions.Error, "EliminarDocumento", "Imposible eliminar el documento: " + idDocumento, "ViajeAdapter");
                        return false;
                    }
                    else return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "EliminarInteresado", ex.Message, "UsuarioAdapter");
                return new();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<EmpresaViaje>? ObtenerEmpresasCompleto()
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_VIAJES_Listar_Empresas_Viaje_Completo");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<EmpresaViaje> listadoEmpresas = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        EmpresaViaje eventos = new(row);
                        listadoEmpresas.Add(eventos);
                    }
                    return listadoEmpresas;

                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "ObtenerEmpresasCompleto", ex.Message, "ViajeAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public EmpresaViaje? ObtenerEmpresasXId(int idViaje)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    List<MySqlParameter> parameters = [new("i_id_empresa", MySqlDbType.Int32) { Value = idViaje }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_VIAJES_Buscar_Empresa_Id", parameters);

                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new(respuesta.Rows[0]);

                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "ObtenerEmpresasXId", ex.Message, "ViajeAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public EmpresaViaje ModificarEmpresa(EmpresaViaje stand)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                    new("i_id_empresa", MySqlDbType.Int32) { Value = stand.id },
                    new("i_nombre", MySqlDbType.VarChar) { Value = stand.nombre },
                    new("i_cuit", MySqlDbType.VarChar) { Value = stand.cuit },
                    new("i_cbu", MySqlDbType.VarChar) { Value = stand.cbu  },
                    new("i_email", MySqlDbType.VarChar) { Value = stand.email },
                    new("i_contacto", MySqlDbType.VarChar) { Value = stand.contacto },
                    new("i_activo", MySqlDbType.Bit) { Value = stand.activo }
                    ];
                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_VIAJES_Modificar_Empresa_Viajes", parameters);
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);

                }
                else return new();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "ModificarEmpresa", ex.Message, "ViajeAdapter");
                return new();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stand"></param>
        /// <returns></returns>
        public EmpresaViaje CrearEmpresaViaje(EmpresaViaje stand)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_nombre", MySqlDbType.VarChar) { Value = stand.nombre },
                        new("i_cuit", MySqlDbType.VarChar) { Value = stand.cuit },
                        new("i_cbu", MySqlDbType.VarChar) { Value = stand.cbu  },
                        new("i_email", MySqlDbType.VarChar) { Value = stand.email },
                        new("i_contacto", MySqlDbType.VarChar) { Value = stand.contacto },
                        new("i_activo", MySqlDbType.Bit) { Value = stand.activo }
                    ];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_VIAJES_Crear_Empresa_Viajes", parameters);
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                else return new();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearEmpresaViaje", ex.Message, "ViajeAdapter");
                return new();
            }
        }
    }
}
