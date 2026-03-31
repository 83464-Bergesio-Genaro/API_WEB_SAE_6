using System.Data;

namespace API_WEB_SAE_6.Models
{
    /// <summary></summary>
    public class BecariosSAE
    {
        /// <summary></summary>
        public int id { get; set; } = -1;
        /// <summary></summary>
        public string legajo { get; set; } = "";
        /// <summary></summary>
        public string? nombre_becario { get; set; }
        /// <summary></summary>
        public bool alquila { get; set; }=false;
        /// <summary></summary>
        public DateTime fecha_solicitud { get; set; } = DateTime.MinValue;
        /// <summary></summary>
        public bool? aceptado_inicio { get; set; }
        /// <summary></summary>
        public bool? puede_pagarle { get; set; }
        /// <summary></summary>
        public bool activo { get; set; } = false;
        /// <summary></summary>
        public int anio_beca { get; set; }=-1;
        /// <summary></summary>
        public int? id_becario_previo { get; set; }
        /// <summary></summary>
        public BecariosSAE(){}
        /// <summary></summary>
        public BecariosSAE(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "-1");
            legajo = data["legajo"].ToString() ?? "0";
            nombre_becario = data["nombre_becario"].ToString() ?? "";
            alquila = data["alquila"].ToString() == "1";
            fecha_solicitud = DateTime.Parse(data["fecha_solicitud"].ToString() ?? DateTime.Now.ToShortDateString());
            aceptado_inicio = data["aceptado_inicio"].ToString() == "" ? null : data["aceptado_inicio"].ToString() == "1";
            puede_pagarle = data["puede_pagarle"].ToString() == "" ? null : data["puede_pagarle"].ToString() == "1";
            activo = data["activo"].ToString() == "1";
            anio_beca = int.Parse(data["anio_beca"].ToString() ?? "0");
            id_becario_previo = data["id_becario_previo"].ToString() == "" ? null : int.Parse(data["id_becario_previo"].ToString() ?? "0");
        }
    }
}
