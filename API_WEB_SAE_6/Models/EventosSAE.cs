using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class EventosSAE
    {
        public int id { get; set; }
        public DateTime fecha_evento { get; set; }
        public string horario_inicio { get; set; }
        public string horario_fin { get; set; }
        public string encargado { get; set; }
        public string nombre_evento { get; set; }
        public bool informacion_interna { get; set; }

        public EventosSAE() { }

        public EventosSAE(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            fecha_evento = DateTime.Parse(data["fecha_evento"].ToString() ?? "00:00:00");
            horario_inicio = data["horario_inicio"].ToString() ?? "00:00:00";
            horario_fin = data["horario_fin"].ToString() ?? "00:00:00";
            encargado = data["encargado"].ToString() ?? "NO DATA";
            nombre_evento = data["nombre_evento"].ToString() ?? "NO DATA";
            informacion_interna = bool.Parse(data["informacion_interna"].ToString() ?? "0");
        }
    }
}
