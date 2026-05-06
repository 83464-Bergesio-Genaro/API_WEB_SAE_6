using API_WEB_SAE_6.Logs;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace API_WEB_SAE_6.Models
{
    /// <summary> </summary>
    public class Perfiles
    {
        /// <summary> </summary>
        [Key]
        public int id { get; set; } = -1;
        /// <summary> </summary>
        public string nombre { get; set; } = "";
        /// <summary> </summary>
        public bool activo { get; set; } = false;
        /// <summary>
        /// 
        /// </summary>
        public Perfiles(DataRow row)
        {
            try
            {
                id = Convert.ToInt32(row["id"]);
                nombre = row["nombre"].ToString() ?? "";
                activo = Convert.ToBoolean(row["activo"]);
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "Constructor Perfiles", ex.Message, "Perfiles");
            }
        }
    }
}
