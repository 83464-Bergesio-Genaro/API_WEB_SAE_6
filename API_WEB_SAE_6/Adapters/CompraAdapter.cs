using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models.Compra;
using API_WEB_SAE_6.Models.Viaje;
using MySqlConnector;
using System.Data;

namespace API_WEB_SAE_6.Adapters
{
    /// <summary>
    /// 
    /// </summary>
    public class CompraAdapter(string motorDB = "MySQL")
    {
        /// <summary>
        /// Define que tipo de base de datos se usa para consumir la informacion
        /// </summary>
        public string MotorDB { get; set; } = motorDB;
    
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Compras>? ObtenerComprasXFecha(DateOnly fecha_inicio,DateOnly fecha_fin)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    List<MySqlParameter> parameters = [
                        new("i_fecha_inicio", MySqlDbType.Date) { Value = fecha_inicio.ToString("yyyy-MM-dd") },
                        new("i_fecha_fin", MySqlDbType.Date) { Value = fecha_fin.ToString("yyyy-MM-dd") }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_COMPRAS_Buscar_Compras_Fecha", parameters);

                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<Compras> listadoCompras = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        Compras eventos = new(row);
                        listadoCompras.Add(eventos);
                    }
                    return listadoCompras;

                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "ObtenerEmpresasXId", ex.Message, "CompraAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="comp"></param>
        /// <returns></returns>
        public Compras CrearCompra(Compras comp)
        {
            //Inicializa un valor y le asigna el tipo
            List<MySqlParameter> parameters = [
                    new("i_nombre", MySqlDbType.VarChar) { Value = comp.nombre_compra },
                    new("i_precio_sugerido", MySqlDbType.Decimal) { Value = comp.precio_sugerido },
                    new("i_motivo", MySqlDbType.VarChar) { Value = comp.motivo  },
                    new("i_fecha_alta", MySqlDbType.Date) { Value = comp.fecha_compra.ToString("yyyy-MM-dd") },
                    new("i_id_usuario_alta", MySqlDbType.Int32) { Value = comp.id_usuario },
            ];
            GeneralAdapterMySQL consult = new();
            DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_COMPRAS_Crear_Compra", parameters);
            if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
            else return new(respuesta.Rows[0]);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idEvento"></param>
        /// <returns></returns>
        public bool EliminarCompra(int idEvento)
        {
            List<MySqlParameter> parameters = [new("i_id_compra", MySqlDbType.Int32) { Value = idEvento }];

            GeneralAdapterMySQL consult = new();
            DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_COMPRAS_Eliminar_Compra", parameters);

            if (respuesta.Rows.Count > 0) return false;
            else return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public InformesTecnicoCompra? ObtenerInformeXNro(string nro_expediente)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    
                    List<MySqlParameter> parameters = [new("i_nro_expediente", MySqlDbType.VarChar) { Value = nro_expediente }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_COMPRAS_Buscar_Informe_Expediente", parameters);

                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new(respuesta.Rows[0]);

                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "ObtenerInformeXNro", ex.Message, "CompraAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public InformesTecnicoCompra? ObtenerInformeXCompra(int idCompra)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    List<MySqlParameter> parameters = [new("i_id_compra", MySqlDbType.Int32) { Value = idCompra }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_COMPRAS_Buscar_Informe_Compra", parameters);

                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new(respuesta.Rows[0]);

                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "ObtenerInformeXCompra", ex.Message, "CompraAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="informe"></param>
        /// <param name="idUsuarioActual"></param>
        /// <returns></returns>
        public InformesTecnicoCompra ModificarInformeCompra(InformesTecnicoCompra informe, int idUsuarioActual)
        {
            //Inicializa un valor y le asigna el tipo
            List<MySqlParameter> parameters = [
                new("i_nro_expediente", MySqlDbType.VarChar) { Value = informe.nro_expediente },
                new("i_id_compra", MySqlDbType.Int32) { Value = informe.id_compra },
                new("i_fecha_informe", MySqlDbType.Date) { Value = informe.fecha_informe.ToString("yyyy-MM-dd") },
                new("i_fecha_licitacion", MySqlDbType.Date) { Value = informe.fecha_licitacion.ToString("yyyy-MM-dd")  },
                new("i_precio_real", MySqlDbType.Decimal) { Value = informe.precio_real },

                new("i_nombre_solicitante", MySqlDbType.VarChar) { Value = informe.nombre_solicitante },
                new("i_nombre_ganador", MySqlDbType.VarChar) { Value = informe.nombre_ganador },
                new("i_id_usuario_modificacion", MySqlDbType.Int32) { Value = idUsuarioActual }];
            GeneralAdapterMySQL consult = new();
            DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_COMPRAS_Modificar_Informe_Compra", parameters);
            if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
            else return new(respuesta.Rows[0]);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="informe"></param>
        /// <param name="idUsuarioActual"></param>
        /// <returns></returns>
        public InformesTecnicoCompra CrearInformeCompra(InformesTecnicoCompra informe, int idUsuarioActual)
        {
            //Inicializa un valor y le asigna el tipo
            List<MySqlParameter> parameters = [
                new("i_nro_expediente", MySqlDbType.VarChar) { Value = informe.nro_expediente },
                new("i_id_compra", MySqlDbType.Int32) { Value = informe.id_compra },
                new("i_fecha_informe", MySqlDbType.Date) { Value = informe.fecha_informe.ToString("yyyy-MM-dd") },
                new("i_fecha_licitacion", MySqlDbType.Date) { Value = informe.fecha_licitacion.ToString("yyyy-MM-dd")  },
                new("i_precio_real", MySqlDbType.Decimal) { Value = informe.precio_real },

                new("i_nombre_solicitante", MySqlDbType.VarChar) { Value = informe.nombre_solicitante },
                new("i_nombre_ganador", MySqlDbType.VarChar) { Value = informe.nombre_ganador },
                new("i_id_usuario_alta", MySqlDbType.Int32) { Value = idUsuarioActual }];
            GeneralAdapterMySQL consult = new();
            DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_COMPRAS_Crear_Informe_Compra", parameters);
            if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
            else return new(respuesta.Rows[0]);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idDoc"></param>
        /// <returns></returns>
        public DocumentosCompra? BuscarDocumentoXId(int idDoc)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_doc", MySqlDbType.Int32) { Value = idDoc }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_COMPRAS_Buscar_Documento_Id", parameters);

                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new(respuesta.Rows[0]);
                }
                else return new();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "BuscarDocumentoXId", ex.Message, "CompraAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idCompra"></param>
        /// <returns></returns>
        public List<DocumentosCompra>? BuscarDocumentosXCompra(int idCompra)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_compra", MySqlDbType.VarChar) { Value = idCompra }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_COMPRAS_Buscar_Documento_Compra", parameters);

                    if (respuesta.Rows.Count == 0) return [];
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else
                    {
                        List<DocumentosCompra> listadoViaje = [];
                        foreach (DataRow row in respuesta.Rows)
                        {
                            DocumentosCompra documentos = new(row);
                            listadoViaje.Add(documentos);
                        }
                        return listadoViaje;
                    }
                }
                else return [];
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "BuscarDocumentoXId", ex.Message, "CompraAdapter");
                return null;
            }
        }
        /*/// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DocumentosCompra ModificarDocumento(DocumentosCompra documento, string idMod)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                    new("i_id_doc", MySqlDbType.Int32) { Value = documento.id },
                    new("i_id_compra", MySqlDbType.VarChar) { Value = documento.id_compra },
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
                Logger.RegistrarDatos(Logger.LogOptions.Error, "ModificarDocumento", ex.Message, "CompraAdapter");
                return new();
            }
        }*/
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DocumentosCompra CrearDocumento(DocumentosCompra documento, int idCrea)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                    new("i_id_compra", MySqlDbType.VarChar) { Value = documento.id_compra },
                    new("i_id_tipo", MySqlDbType.Int32) { Value = documento.id_tipo_documento },
                    new("i_nombre", MySqlDbType.VarChar) { Value = documento.nombre_documento },
                    new("i_tamanio", MySqlDbType.Int32) { Value = documento.tamanio },
                    new("i_ruta", MySqlDbType.VarChar) { Value = documento.ruta },
                    new("i_id_usuario_alta", MySqlDbType.Int32) { Value = idCrea}];

                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_COMPRAS_Crear_Documento_Compra", parameters);

                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                else return new();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearDocumento", ex.Message, "CompraAdapter");
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
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_COMPRAS_Eliminar_Documento_Compra", parameters);
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
                Logger.RegistrarDatos(Logger.LogOptions.Error, "EliminarDocumento", ex.Message, "CompraAdapter");
                return new();
            }
        }
    }
}
