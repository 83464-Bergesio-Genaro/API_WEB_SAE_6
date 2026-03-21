using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models;
using MySqlConnector;
using System.Data;

namespace API_WEB_SAE_6.Adapters
{
    /// <summary>
    /// 
    /// </summary>
    public class EstudianteAdapter(string motorDB = "MySQL")
    {
        /// <summary>
        /// Define que tipo de base de datos se usa para consumir la informacion
        /// </summary>
        public string MotorDB { get; set; } = motorDB;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idDoc"></param>
        /// <returns></returns>
        public DocumentosEstudiante? BuscarDocumentoXId(int idDoc)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_doc", MySqlDbType.Int32) { Value = idDoc }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_ESTUDIANTE_Buscar_Documento_Id", parameters);

                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new DocumentosEstudiante(respuesta.Rows[0]);
                }
                else return new();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "BuscarDocumentoXId", ex.Message, "EstudianteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="legajo"></param>
        /// <returns></returns>
        public List<DocumentosEstudiante>? BuscarDocumentosXLegajo(string legajo)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_legajo", MySqlDbType.VarChar) { Value = legajo }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_ESTUDIANTE_Buscar_Documento_Legajo", parameters);

                    if (respuesta.Rows.Count == 0) return [];
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else
                    {
                        List<DocumentosEstudiante> listadoDocumentos = [];
                        foreach (DataRow row in respuesta.Rows)
                        {
                            DocumentosEstudiante documentos = new(row);
                            listadoDocumentos.Add(documentos);
                        }
                        return listadoDocumentos;
                    }
                }
                else return [];
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "BuscarDocumentoXId", ex.Message, "EstudianteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DocumentosEstudiante ModificarDocumento(DocumentosEstudiante documento , string idMod)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                    new("i_id_doc", MySqlDbType.Int32) { Value = documento.id },
                    new("i_legajo", MySqlDbType.VarChar) { Value = documento.legajo },
                    new("i_id_tipo", MySqlDbType.Int32) { Value = documento.id_tipo_documento },
                    new("i_nombre", MySqlDbType.VarChar) { Value = documento.nombre_documento },
                    new("i_tamanio", MySqlDbType.Int32) { Value = documento.tamanio },
                    new("i_ruta", MySqlDbType.VarChar) { Value = documento.ruta },
                    new("i_id_usuario_mod", MySqlDbType.Int32) { Value = idMod}];

                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_ESTUDIANTE_Modificar_Documento_Estudiante", parameters);

                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                else return new();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "BuscarDocumentoXId", ex.Message, "EstudianteAdapter");
                return new();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DocumentosEstudiante CrearDocumento(DocumentosEstudiante documento, int idCrea)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                    new("i_legajo", MySqlDbType.VarChar) { Value = documento.legajo },
                    new("i_id_tipo", MySqlDbType.Int32) { Value = documento.id_tipo_documento },
                    new("i_nombre", MySqlDbType.VarChar) { Value = documento.nombre_documento },
                    new("i_tamanio", MySqlDbType.Int32) { Value = documento.tamanio },
                    new("i_ruta", MySqlDbType.VarChar) { Value = documento.ruta },
                    new("i_id_usuario_alta", MySqlDbType.Int32) { Value = idCrea}];

                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_ESTUDIANTE_Crear_Documento_Estudiante", parameters);

                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                else return new();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "BuscarDocumentoXId", ex.Message, "EstudianteAdapter");
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
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_ESTUDIANTE_Buscar_Documento_Id", parameters);
                    if (respuesta.Rows.Count > 0)
                    {
                        if (respuesta.Rows[0][0].ToString() == "ERROR") Logger.RegistrarDatos(Logger.LogOptions.Error, "EliminarStand", "Imposible eliminar el interesado: " + idDocumento, "UsuarioAdapter");
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
    }
}
