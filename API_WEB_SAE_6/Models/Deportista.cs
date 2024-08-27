using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class Deportista
    {
        public int id { get; set; }
        public string legajo { get; set; }
        public string? nombre_deportista { get; set; }
        public bool habilitado_deporte { get; set; }
        public DateTime? vencimiento_ficha { get; set; }
        public Deportista()
        {

        }
        public Deportista(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            legajo = data["legajo"].ToString() ?? "ERROR";
            nombre_deportista = data["nombre_deportista"].ToString() ?? "ERROR";
            habilitado_deporte = bool.Parse(data["habilitado_deporte"].ToString() ?? "0");
            vencimiento_ficha = data["vencimiento_ficha_medica"] == null || data["vencimiento_ficha_medica"].ToString() == "" ? null : DateTime.Parse(data["vencimiento_ficha_medica"].ToString() ?? "1900-01-01");
        }
    }
}
