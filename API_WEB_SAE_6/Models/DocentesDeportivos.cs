using System.Data;

namespace API_WEB_SAE_6.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class DocentesDeportivos
    {
        /// <summary> </summary>
        public string cuil { get; set; } = "";
        /// <summary> </summary>
        public string nombres { get; set; } = "";
        /// <summary> </summary>
        public string apellidos { get; set; } = "";
        /// <summary> </summary>
        public bool activo { get; set; }=false;
        /// <summary> </summary>
        public DateTime fecha_nacimiento { get; set; } = DateTime.MinValue;
        /// <summary> </summary>
        public DocentesDeportivos()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public DocentesDeportivos(DataRow data)
        {
            cuil = data["cuil"].ToString() ?? "ERROR";
            nombres = data["nombres"].ToString() ?? "ERROR";
            apellidos = data["apellidos"].ToString() ?? "ERROR";
            activo = (data["activo"].ToString() == "1");
            fecha_nacimiento = DateTime.Parse(data["fecha_nacimiento"].ToString() ?? "1900-01-01");
        }
    }
}
