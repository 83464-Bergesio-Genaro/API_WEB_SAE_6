using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models.Prensa;
using MySqlConnector;
using System.Data;

namespace API_WEB_SAE_6.Adapters
{
    /// <summary>
    /// Clase adaptadora para la prensa, permite consumir la informacion de la prensa desde diferentes tipos de bases de datos, como MySQL, SQL Server, Oracle, etc.
    /// </summary>
    /// <param name="motorDB"></param>
    public class PrensaAdapter(string motorDB = "MySQL")
    {
        /// <summary>
        /// Define que tipo de base de datos se usa para consumir la informacion
        /// </summary>
        public string MotorDB { get; set; } = motorDB;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Publicaciones>? ObtenerPublicacionesCompleto()
        {
            string method = "ListarPublicacionesCompleto";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_PRENSA_Listar_Publicaciones_Completo");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<Publicaciones> publicaciones = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        Publicaciones publi = new(row);
                        publicaciones.Add(publi);
                    }
                    return publicaciones;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "PrensaAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Publicaciones>? ObtenerPublicacionesActivas()
        {
            string method = "ObtenerPublicacionesActivo";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_PRENSA_Listar_Publicaciones_Activas");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<Publicaciones> publicaciones = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        Publicaciones publi = new(row);
                        publicaciones.Add(publi);
                    }
                    return publicaciones;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "PrensaAdapter");
                return null;
            }
        }
        /// <summary>
        /// Obtiene un especialista medico por su cuil, devuelve null si no lo encuentra o si ocurre un error, devuelve un objeto vacio si el resultado es correcto pero no hay datos
        /// </summary>
        /// <param name="idPublica"></param>
        /// <returns></returns>
        public Publicaciones? ObtenerPublicacionesXId(int idPublica)
        {
            string method = "ObtenerPublicacionesXId";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_publicacion", MySqlDbType.Int32) { Value = idPublica }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_PRENSA_Buscar_Publicacion_Id", parameters);

                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new Publicaciones(respuesta.Rows[0]);
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "PrensaAdapter");
                return null;
            }
        }
        /// <summary>
        /// Obtiene la documentacion de prensa sin la data, devuelve null si ocurre un error, devuelve un objeto vacio si el resultado es correcto pero no hay datos
        /// </summary>
        /// <returns></returns>
        public List<DocumentosPrensa>? ObtenerDocumentacionSinData()
        {
            string method = "ObtenerDocumentacionSinData";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_PRENSA_Listar_Documentos_Prensa");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<DocumentosPrensa> documentacion = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        DocumentosPrensa docus = new(row);
                        documentacion.Add(docus);
                    }
                    return documentacion;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "PrensaAdapter");
                return null;
            }
        }
        /// <summary>
        /// Obtiene un documento de prensa por su id, devuelve null si ocurre un error, devuelve un objeto vacio si el resultado es correcto pero no hay datos
        /// </summary>
        /// <param name="idDoc"></param>
        /// <returns></returns>
        public DocumentosPrensa? ObtenerDocumentoXId(int idDoc)
        {
            string method = "ObtenerDocumentoXId";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_doc", MySqlDbType.Int32) { Value = idDoc }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_PRENSA_Buscar_Documento_Id", parameters);

                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new DocumentosPrensa(respuesta.Rows[0]);
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "PrensaAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idPublicacion"></param>
        /// <returns></returns>
        public List<DocumentosPrensa>? BuscarDocumentosXPublicacion(int idPublicacion)
        {
            string method = "BuscarDocumentosXPublicacion";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_id_publicacion", MySqlDbType.Int32) { Value = idPublicacion }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_PRENSA_Buscar_Documento_Publicacion", parameters);

                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<DocumentosPrensa> documentacion = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        DocumentosPrensa docus = new(row);
                        documentacion.Add(docus);
                    }
                    return documentacion;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "PrensaAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="publi"></param>
        /// <param name="usuarioActual"></param>
        /// <returns></returns>
        public Publicaciones ModificarPublicaciones(Publicaciones publi, int usuarioActual)
        {
            string method = "ModificarPublicaciones";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_id_publi", MySqlDbType.Int32) { Value = publi.id},
                        new("i_titulo", MySqlDbType.VarChar) { Value = publi.titulo_publicacion},
                        new("i_descripcion", MySqlDbType.Text) { Value = publi.descripcion},
                        new("i_fecha_ini", MySqlDbType.Date) { Value = publi.fecha_inicio},
                        new("i_fecha_vig", MySqlDbType.Date) { Value = publi.fecha_vigencia},
                        new("i_no_dar_baja", MySqlDbType.Bit) { Value = publi.no_dar_baja},
                        new("i_prioridad", MySqlDbType.Int32) { Value = publi.prioridad},
                        new("i_visualizaciones", MySqlDbType.Int32) { Value = publi.visualizaciones},
                        new("i_id_usuario_mod", MySqlDbType.Int32) { Value = usuarioActual},];
                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_PRENSA_Modificar_Publicacion", parameters);
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                else return new();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "PrensaAdapter");
                return new();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="docus"></param>
        /// <param name="usuarioActual"></param>
        /// <returns></returns>
        public DocumentosPrensa ModificarDocumento(DocumentosPrensa docus, int usuarioActual)
        {
            string method = "ModificarDocumento";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_id_doc", MySqlDbType.Int32) { Value = docus.id},
                        new("i_id_tipo", MySqlDbType.Int32) { Value = docus.id_tipo_documento},
                        new("i_nombre", MySqlDbType.VarChar) { Value = docus.nombre_documento},
                        new("i_tamanio", MySqlDbType.Int32) { Value = docus.tamanio},
                        new("i_ruta", MySqlDbType.VarChar) { Value = docus.ruta},
                        new("i_id_usuario_mod", MySqlDbType.Int32) { Value = usuarioActual}];
                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_PRENSA_Modificar_Documento_Prensa", parameters);
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                else return new();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "PrensaAdapter");
                return new();
            }
        }
        /// <summary>
        /// Crea una nueva publicación en la base de datos, devuelve un objeto vacío si ocurre un error o si el resultado es correcto pero no hay datos, devuelve null si ocurre un error al ejecutar el procedimiento almacenado
        /// </summary>
        /// <param name="publi"></param>
        /// <param name="idCreacion"></param>
        /// <returns></returns>
        public Publicaciones CrearPublicacion(Publicaciones publi, int idCreacion)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_titulo", MySqlDbType.VarChar) { Value = publi.titulo_publicacion},
                        new("i_descripcion", MySqlDbType.Text) { Value = publi.descripcion},
                        new("i_fecha_ini", MySqlDbType.Date) { Value = publi.fecha_inicio},
                        new("i_fecha_vig", MySqlDbType.Date) { Value = publi.fecha_vigencia},
                        new("i_no_dar_baja", MySqlDbType.Bit) { Value = publi.no_dar_baja},
                        new("i_prioridad", MySqlDbType.Int32) { Value = publi.prioridad},
                        new("i_visualizaciones", MySqlDbType.Int32) { Value = publi.visualizaciones},
                        new("i_id_usuario_alta", MySqlDbType.Int32) { Value = idCreacion},];
                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_PRENSA_Crear_Publicacion", parameters);
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearPublicacion", ex.Message, "SaludAdapter");
                    return new();
                }
            }
            else return new();
        }
        /// <summary>
        /// Crea un nuevo documento de prensa en la base de datos, devuelve un objeto vacío si ocurre un error o si el resultado es correcto pero no hay datos, devuelve null si ocurre un error al ejecutar el procedimiento almacenado
        /// </summary>
        /// <param name="docus"></param>
        /// <param name="idCreacion"></param>
        /// <returns></returns>
        public DocumentosPrensa CrearDocumento(DocumentosPrensa docus, int idCreacion)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    List<MySqlParameter> parameters = [
                        new("i_id_tipo", MySqlDbType.Int32) { Value = docus.id_tipo_documento},
                        new("i_nombre", MySqlDbType.VarChar) { Value = docus.nombre_documento},
                        new("i_tamanio", MySqlDbType.Int32) { Value = docus.tamanio},
                        new("i_ruta", MySqlDbType.VarChar) { Value = docus.ruta},
                        new("i_id_usuario_alta", MySqlDbType.Int32) { Value = idCreacion}];
                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_PRENSA_Crear_Documento_Prensa", parameters);
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearDocumento", ex.Message, "SaludAdapter");
                    return new();
                }
            }
            else return new();
        }
        /// <summary>
        /// Crea una nueva vinculacion entre un documento de prensa y una publicación, devuelve un objeto vacío si ocurre un error o si el resultado es correcto pero no hay datos, devuelve null si ocurre un error al ejecutar el procedimiento almacenado
        /// </summary>
        /// <param name="idPubli"></param>
        /// <param name="idDocu"></param>
        /// <returns></returns>
        public VinculoDocPublic CrearVinPubli(int idPubli,int idDocu)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    List<MySqlParameter> parameters = [
                        new("i_id_publicacion", MySqlDbType.Int32) { Value = idPubli},
                        new("i_id_documento", MySqlDbType.Int32) { Value = idDocu}];
                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_PRENSA_Vincular_Documento_Publicacion", parameters);
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearVinPubli", ex.Message, "SaludAdapter");
                    return new();
                }
            }
            else return new();
        }
        /// <summary>
        /// Elimina una publicación de la base de datos, devuelve true si la eliminación fue exitosa, devuelve false si ocurre un error al ejecutar el procedimiento almacenado o si el procedimiento almacenado devuelve un resultado indicando que no se pudo eliminar la publicación
        /// </summary>
        /// <param name="idPublicacion"></param>
        /// <returns></returns>
        public bool EliminarPublicacion(int idPublicacion)
        {
            List<MySqlParameter> parameters = [new("i_id_publicacion", MySqlDbType.Int32) { Value = idPublicacion }];

            GeneralAdapterMySQL consult = new();
            DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_PRENSA_Eliminar_Publicacion", parameters);

            if (respuesta.Rows.Count > 0) return false;
            else return true;
        }
        /// <summary>
        /// Elimina un documento de prensa de la base de datos, devuelve true si la eliminación fue exitosa, devuelve false si ocurre un error al ejecutar el procedimiento almacenado o si el procedimiento almacenado devuelve un resultado indicando que no se pudo eliminar el documento
        /// </summary>
        /// <param name="idDocumento"></param>
        /// <returns></returns>
        public bool EliminarDocumento(int idDocumento)
        {
            List<MySqlParameter> parameters = [new("i_id_doc", MySqlDbType.Int32) { Value = idDocumento }];

            GeneralAdapterMySQL consult = new();
            DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_PRENSA_Eliminar_Documento_Prensa", parameters);

            if (respuesta.Rows.Count > 0) return false;
            else return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idVinc"></param>
        /// <returns></returns>
        public bool EliminarVinculacion(int idVinc)
        {
            List<MySqlParameter> parameters = [new("i_id_docXPublic", MySqlDbType.Int32) { Value = idVinc }];

            GeneralAdapterMySQL consult = new();
            DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_PRENSA_Eliminar_Vinculacion_Doc_Publi", parameters);

            if (respuesta.Rows.Count > 0) return false;
            else return true;
        }
    }
}
