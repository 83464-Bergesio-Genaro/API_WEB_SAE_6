using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class BecariosSAE
    {
        public int id { get; set; }
        public string legajo { get; set; }
        public string? nombre_becario { get; set; }
        public bool alquila { get; set; }
        public DateTime fecha_solicitud { get; set; }
        public bool? aceptado_inicio { get; set; }
        public bool? puede_pagarle { get; set; }
        public bool activo { get; set; }
        public int anio_beca { get; set; }
        public int? id_becario_previo { get; set; }

        public BecariosSAE()
        {

        }

        public BecariosSAE(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            legajo = data["legajo"].ToString() ?? "0";
            nombre_becario = data["nombre_becario"].ToString() ?? "NO DATA";
            alquila = bool.Parse(data["alquila"].ToString() ?? "0");
            fecha_solicitud = DateTime.Parse(data["fecha_solicitud"].ToString() ?? DateTime.Now.ToShortDateString());
            aceptado_inicio = data["aceptado_inicio"].ToString() == "" ? null : bool.Parse(data["aceptado_inicio"].ToString() ?? "0");
            puede_pagarle = data["puede_pagarle"].ToString() == "" ? null : bool.Parse(data["puede_pagarle"].ToString() ?? "0");
            activo = bool.Parse(data["activo"].ToString() ?? "0");
            anio_beca = int.Parse(data["anio_beca"].ToString() ?? "0");
            id_becario_previo = data["id_becario_previo"].ToString() == "" ? null : int.Parse(data["id_becario_previo"].ToString() ?? "0");
        }
    }
}
