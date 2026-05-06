using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models;
using System.Data;

namespace API_WEB_SAE_6.Adapters
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="motorDB"></param>
    public class HerramientasAdapter(string motorDB = "MySQL")
    {
        /// <summary>
        /// Define que tipo de base de datos se usa para consumir la informacion
        /// </summary>
        public string MotorDB { get; set; } = motorDB;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TiposDocumento>? ObtenerTiposDocumento()
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_HERRAMIENTAS_Listar_Tipos_Documento");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<TiposDocumento> listadoDocs = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        TiposDocumento docs = new(row);
                        listadoDocs.Add(docs);
                    }
                    return listadoDocs;
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "ObtenerTiposDocumento", ex.Message, "HerramientasAdapter");
                    return null;
                }

            }
            else return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Carreras>? ObtenerCarreras()
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_HERRAMIENTAS_Listar_Carreras");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<Carreras> listadoCarrera = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        Carreras car = new(row);
                        listadoCarrera.Add(car);
                    }
                    return listadoCarrera;
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "ObtenerCarreras", ex.Message, "HerramientasAdapter");
                    return null;
                }

            }
            else return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Perfiles>? ObtenerPerfiles()
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_HERRAMIENTAS_Listar_Perfiles");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<Perfiles> listadoDocs = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        Perfiles docs = new(row);
                        listadoDocs.Add(docs);
                    }
                    return listadoDocs;
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "ObtenerTiposDocumento", ex.Message, "HerramientasAdapter");
                    return null;
                }

            }
            else return null;
        }
    }
}
