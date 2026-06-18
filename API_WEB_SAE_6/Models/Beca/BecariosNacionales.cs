using System.Data;

namespace API_WEB_SAE_6.Models.Beca
{
    /// <summary></summary>
    public class BecariosNacionales
    {
        /// <summary></summary>
        public int id { get; set; } = -1;
        /// <summary></summary>
        public string legajo { get; set; } = "";
        /// <summary></summary>
        public string? nombre_becario { get; set; }
        /// <summary></summary>
        public int tipo_plan { get; set; } = -1;
        /// <summary></summary>
        public int anio_beca { get; set; } = -1;
        /// <summary></summary>
        public bool regularizacion { get; set; }=false;
        /// <summary></summary>
        public bool cumplimiento_servicio { get; set; } = false;
        /// <summary></summary>
        public bool activo { get; set; }
        /// <summary></summary>
        public BecariosNacionales() { }
        /// <summary></summary>
        public BecariosNacionales(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            legajo = data["legajo"].ToString() ?? "0";
            nombre_becario = data["nombre_becario"].ToString() ?? "NO DATA";
            tipo_plan = int.Parse(data["tipo_plan"].ToString() ?? "0");
            anio_beca = int.Parse(data["anio_beca"].ToString() ?? "0");
            regularizacion = data["regularizacion"].ToString() == "1";
            cumplimiento_servicio = data["cumplimiento_servicio"].ToString() == "1";
            activo = data["activo"].ToString() == "1";
        }
    }
}
