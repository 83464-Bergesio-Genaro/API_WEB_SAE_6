using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class StandJPA
    {
        public int id { get; set; }
        public string nombre_stand { get; set; }
        public string expositor { get; set; }
        public string ubicacion { get; set; }
        public string horario_inicio { get; set; }
        public string horario_fin { get; set; }
        public StandJPA()
        {

        }
        public StandJPA(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            nombre_stand = data["nombre"].ToString() ?? "NO DATA"; ;
            expositor = data["expositor"].ToString() ?? "NO DATA"; ;
            ubicacion = data["ubicacion"].ToString() ?? "NO DATA"; ;
            horario_inicio = data["horario_inicio"].ToString() ?? "00:00:00"; ;
            horario_fin = data["horario_fin"].ToString() ?? "00:00:00"; ;
        }
    }
}
