using System.Data;

namespace API_WEB_SAE_6.Models.Deporte
{
    /// <summary>
    /// 
    /// </summary>
    public class TorneoDeportivo
    {
        /// <summary></summary>
        public int id { get; set; } = -1;
        /// <summary></summary>
        public string nombre_torneo { get; set; } = "";
        /// <summary></summary>
        public DateTime fecha_inicio { get; set; }=DateTime.MinValue;
        /// <summary></summary>
        public DateTime fecha_fin { get; set; } = DateTime.MinValue;
        /// <summary></summary>
        public DateTime fecha_limite_inscripcion { get; set; } = DateTime.MinValue;
        /// <summary></summary>
        public bool activo { get; set; } = false;
        /// <summary></summary>
        public int id_deporte { get; set; } = -1;
        /// <summary></summary>
        public string? nombre_deporte { get; set; } = "";
        /// <summary></summary>
        public string cuil_responsable { get; set; } = "";
        /// <summary></summary>
        public string? docente_responsable { get; set; } = "";
        /// <summary></summary>
        public int cupo_jugadores { get; set; } = -1;
        /// <summary></summary>
        public TorneoDeportivo(){ }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public TorneoDeportivo(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            nombre_torneo = data["nombre_torneo"].ToString() ?? "ERROR";
            fecha_inicio = DateTime.Parse(data["fecha_inicio"].ToString() ?? "1900-01-01");
            fecha_fin = DateTime.Parse(data["fecha_fin"].ToString() ?? "1900-01-01");
            fecha_limite_inscripcion = DateTime.Parse(data["fecha_limite_inscripcion"].ToString() ?? "1900-01-01");
            activo = data["activo"].ToString() == "1";
            id_deporte = int.Parse(data["id_deporte"].ToString() ?? "0");
            nombre_deporte = data["nombre_deporte"].ToString() ?? "ERROR";
            cuil_responsable = data["cuil_docente_responsable"].ToString() ?? "ERROR";
            docente_responsable = data["docente_responsable"].ToString() ?? "ERROR";
            cupo_jugadores = int.Parse(data["cupo_jugadores"].ToString() ?? "0");
        }
    }
}
