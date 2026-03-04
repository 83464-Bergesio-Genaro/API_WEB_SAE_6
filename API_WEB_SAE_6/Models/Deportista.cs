using System.Data;

namespace API_WEB_SAE_6.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Deportista
    {
        /// <summary></summary>
        public int id { get; set; } = -1;
        /// <summary></summary>
        public string legajo { get; set; } = "";
        /// <summary></summary>
        public string? nombre_deportista { get; set; } = "";
        /// <summary></summary>
        public bool habilitado_deporte { get; set; }=false;
        /// <summary></summary>
        public DateTime? vencimiento_ficha { get; set; } = DateTime.MinValue;
        /// <summary></summary>
        public Deportista(){}
        /// <summary></summary>
        public Deportista(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            legajo = data["legajo"].ToString() ?? "ERROR";
            nombre_deportista = data["nombre_deportista"].ToString() ?? "ERROR";
            habilitado_deporte = (data["habilitado_deporte"].ToString() == "1");
            vencimiento_ficha = data["vencimiento_ficha_medica"] == null || data["vencimiento_ficha_medica"].ToString() == "" ? null : DateTime.Parse(data["vencimiento_ficha_medica"].ToString() ?? "1900-01-01");
        }
    }
}
