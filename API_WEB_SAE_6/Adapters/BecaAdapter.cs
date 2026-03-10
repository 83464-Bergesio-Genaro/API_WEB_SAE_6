using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models;
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
        public List<BecariosSAE>? ObtenerDeportesCompleto()
        {
            string method = "ObtenerDeportesCompleto";
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
    }
}
