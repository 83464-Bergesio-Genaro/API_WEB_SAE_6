using System.Data;

namespace API_WEB_SAE_6.Models
{
    /// <summary> </summary>
    public class Deportes
    {
        /// <summary> </summary>
        public int id { get; set; } = -1;
        /// <summary> </summary>
        public string nombre { get; set; } = "";
        /// <summary> </summary>
        public bool activo { get; set; } = false;
        /// <summary> </summary>
        public Deportes(){}
        /// <summary> </summary>
        public Deportes(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            nombre = data["nombre"].ToString() ?? "ERROR";
            activo = (data["activo"].ToString() == "1");
        }
    }
}
