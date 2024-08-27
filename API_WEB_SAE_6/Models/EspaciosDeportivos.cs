using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class EspaciosDeportivos
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string domicilio { get; set; }
        public bool activo { get; set; }
        public string url_maps { get; set; }
        public EspaciosDeportivos()
        {

        }

        public EspaciosDeportivos(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            nombre = data["nombre"].ToString() ?? "ERROR";
            domicilio = data["domicilio"].ToString() ?? "ERROR";
            activo = bool.Parse(data["activo"].ToString() ?? "0");
            url_maps = data["url_maps"].ToString() ?? "ERROR";
        }
    }
}
