using System.Data;

namespace API_WEB_SAE_6.Models.JPA
{
    /// <summary></summary>
    public class StandJPA
    {
        /// <summary></summary>
        public int id { get; set; } = -1;
        /// <summary></summary>
        public string nombre_stand { get; set; } = "";
        /// <summary></summary>
        public string expositor { get; set; } = "";
        /// <summary></summary>
        public string ubicacion { get; set; } = "";
        /// <summary></summary>
        public string horario_inicio { get; set; } = "";
        /// <summary></summary>
        public string horario_fin { get; set; } = "";
        /// <summary></summary>
        public StandJPA()
        {

        }
        /// <summary></summary>
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
