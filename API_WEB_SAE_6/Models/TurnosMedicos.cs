using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class TurnosMedicos
    {
        public int id { get; set; }
        public string? cuil_medico { get; set; }
        public string? especialista { get; set; }
        public string legajo { get; set; }
        public string? paciente { get; set; }
        public DateTime fecha_solicitud { get; set; }
        public DateTime? fecha_atencion { get; set; }
        public string? hora_atencion { get; set; }
        public string asunto { get; set; }
        public int id_estado_turno { get; set; }
        public string? estado { get; set; }

        public TurnosMedicos()
        {

        }

        public TurnosMedicos(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            cuil_medico = data["cuil_medico"].ToString() ?? "NO DATA";
            especialista = data["especialista"].ToString() ?? "NO DATA";
            legajo = data["legajo"].ToString() ?? "NO DATA";
            paciente = data["paciente"].ToString() ?? "NO DATA";
            fecha_solicitud = DateTime.Parse(data["fecha_solicitud"].ToString() ?? "2000-01-01");
            if (data["fecha_atencion"].ToString() == "") fecha_atencion = null;
            else fecha_atencion = DateTime.Parse(data["fecha_atencion"].ToString() ?? "2000-01-01");
            hora_atencion = data["hora_atencion"].ToString() ?? "00:00:00";
            asunto = data["asunto"].ToString() ?? "NO DATA";
            id_estado_turno = int.Parse(data["id_estado_turno"].ToString() ?? "0");
            estado = data["estado"].ToString() ?? "NO DATA";
        }
    }
}
