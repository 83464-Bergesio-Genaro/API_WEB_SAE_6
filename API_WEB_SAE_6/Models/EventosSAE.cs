using System.Data;

namespace API_WEB_SAE_6.Models
{
    /// <summary> </summary>
    public class EventosSAE
    {
        /// <summary> </summary>
        public int id { get; set; } = -1;
        /// <summary> </summary>
        public DateTime fecha_evento { get; set; } = DateTime.MinValue;
        /// <summary> </summary>
        public string horario_inicio { get; set; } = "00:00";
        /// <summary> </summary>
        public string horario_fin { get; set; } = "00:00";
        /// <summary> </summary>
        public string encargado { get; set; } = "";
        /// <summary> </summary>
        public string nombre_evento { get; set; } = "";
        /// <summary> </summary>
        public bool informacion_interna { get; set; } = false;
        /// <summary> </summary>
        public EventosSAE() { }
        /// <summary> </summary>
        public EventosSAE(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            fecha_evento = DateTime.Parse(data["fecha_evento"].ToString() ?? "00:00:00");
            horario_inicio = data["horario_inicio"].ToString() ?? "00:00:00";
            horario_fin = data["horario_fin"].ToString() ?? "00:00:00";
            encargado = data["encargado"].ToString() ?? "NO DATA";
            nombre_evento = data["nombre_evento"].ToString() ?? "NO DATA";
            informacion_interna = (data["informacion_interna"].ToString() == "1");
        }
    }
}
