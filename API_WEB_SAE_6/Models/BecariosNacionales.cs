using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class BecariosNacionales
    {
        public int id { get; set; }
        public string legajo { get; set; }
        public string? nombre_becario { get; set; }
        public int tipo_plan { get; set; }
        public int anio_beca { get; set; }
        public bool regularizacion { get; set; }
        public bool cumplimiento_servicio { get; set; }
        public bool activo { get; set; }

        public BecariosNacionales()
        {

        }

        public BecariosNacionales(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            legajo = data["legajo"].ToString() ?? "0";
            nombre_becario = data["nombre_becario"].ToString() ?? "NO DATA";
            tipo_plan = int.Parse(data["tipo_plan"].ToString() ?? "0");
            anio_beca = int.Parse(data["anio_beca"].ToString() ?? "0");
            regularizacion = bool.Parse(data["regularizacion"].ToString() ?? "0");
            cumplimiento_servicio = bool.Parse(data["cumplimiento_servicio"].ToString() ?? "0");
            activo = bool.Parse(data["activo"].ToString() ?? "0");
        }
    }
}
