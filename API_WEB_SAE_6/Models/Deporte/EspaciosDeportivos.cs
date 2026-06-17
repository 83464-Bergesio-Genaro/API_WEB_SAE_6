using System.Data;

namespace API_WEB_SAE_6.Models.Deporte
{
    /// <summary>
    /// 
    /// </summary>
    public class EspaciosDeportivos
    {
        /// <summary> </summary>
        public int id { get; set; } = -1;
        /// <summary> </summary>
        public string nombre { get; set; } = "";
        /// <summary> </summary>
        public string domicilio { get; set; } = "";
        /// <summary> </summary>
        public bool activo { get; set; } = false;
        /// <summary> </summary>
        public string url_maps { get; set; } = "";
        /// <summary> </summary>
        public EspaciosDeportivos()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public EspaciosDeportivos(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            nombre = data["nombre"].ToString() ?? "ERROR";
            domicilio = data["domicilio"].ToString() ?? "ERROR";
            activo = data["activo"].ToString() == "1";
            url_maps = data["url_maps"].ToString() ?? "ERROR";
        }
    }
}
