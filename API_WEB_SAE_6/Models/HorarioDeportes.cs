using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class HorarioDeportes
    {
        public int id { get; set; }
        public int id_espacio_deportivo { get; set; }
        public string? espacio_deportivo { get; set; }
        public int id_deporte { get; set; }
        public string? nombre_deporte { get; set; }
        public string hora_inicio { get; set; }
        public string hora_fin { get; set; }
        public bool activo { get; set; }
        public string cuil_docente { get; set; }
        public string? docente_responsable { get; set; }
        public int dia { get; set; }
        public HorarioDeportes()
        {

        }

        public HorarioDeportes(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            id_espacio_deportivo = int.Parse(data["id_espacio_deportivo"].ToString() ?? "0");
            espacio_deportivo = data["espacio_deportivo"].ToString() ?? "NO DATA";
            id_deporte = int.Parse(data["id_deporte"].ToString() ?? "0");
            nombre_deporte = data["nombre_deporte"].ToString() ?? "NO DATA";
            hora_inicio = data["hora_inicio"].ToString() ?? "00:00:00";
            hora_fin = data["hora_fin"].ToString() ?? "00:00:00";
            activo = bool.Parse(data["activo"].ToString() ?? "0");
            cuil_docente = data["cuil_docente"].ToString() ?? "ERROR";
            docente_responsable = data["docente_responsable"].ToString() ?? "ERROR";
            dia = int.Parse(data["dia"].ToString() ?? "0");
        }
    }
}
