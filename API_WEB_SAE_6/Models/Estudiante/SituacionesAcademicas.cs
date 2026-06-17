using System.Data;

namespace API_WEB_SAE_6.Models.Estudiante
{
    /// <summary></summary>
    public class SituacionesAcademicas
    {
        /// <summary></summary>
        public int id { get; set; } = -1;
        /// <summary></summary>
        public string legajo { get; set; } = "";
        /// <summary></summary>
        public bool cursando { get; set; }=false;
        /// <summary></summary>
        public int anio_situacion { get; set; } = -1;
        /// <summary></summary>
        public int cant_materias_cursadas_anterior { get; set; } = -1;
        /// <summary></summary>
        public int cant_materias_aprobadas_periodo_anterior { get; set; } = -1;
        /// <summary></summary>
        public int cant_materias_cursando { get; set; } = -1;
        /// <summary></summary>
        public int cant_materias_aprobadas_total { get; set; } = -1;
        /// <summary></summary>
        public decimal prom_gral_con_aplazos { get; set; } = -1;
        /// <summary></summary>
        public decimal prom_gral_sin_aplazos { get; set; } = -1;
        /// <summary></summary>
        public int ingreso { get; set; } = -1;
        /// <summary></summary>
        public SituacionesAcademicas() { }
        /// <summary></summary>
        public SituacionesAcademicas(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            legajo = data["legajo"].ToString() ?? "0";
            cursando = data["cursando"].ToString() == "1";
            anio_situacion = int.Parse(data["anio_situacion"].ToString() ?? "0");
            cant_materias_cursadas_anterior = int.Parse(data["cant_materias_cursadas_anterior"].ToString() ?? "0");
            cant_materias_aprobadas_periodo_anterior = int.Parse(data["cant_materias_aprobadas_periodo_anterior"].ToString() ?? "0");
            cant_materias_cursando = int.Parse(data["cant_materias_cursando"].ToString() ?? "0");
            cant_materias_aprobadas_total = int.Parse(data["cant_materias_aprobadas_total"].ToString() ?? "0");
            prom_gral_con_aplazos = decimal.Parse(data["prom_gral_con_aplazos"].ToString() ?? "0");
            prom_gral_sin_aplazos = decimal.Parse(data["prom_gral_sin_aplazos"].ToString() ?? "0");
            ingreso = int.Parse(data["ingreso"].ToString() ?? "0");
        }
    }
}
