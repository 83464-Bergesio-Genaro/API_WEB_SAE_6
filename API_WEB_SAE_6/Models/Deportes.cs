using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class Deportes
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public bool activo { get; set; }

        public Deportes()
        {

        }
        public Deportes(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            nombre = data["nombre"].ToString() ?? "ERROR";
            activo = bool.Parse(data["activo"].ToString() ?? "0");
        }
    }
}
