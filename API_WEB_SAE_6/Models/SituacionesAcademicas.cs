using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class SituacionesAcademicas
    {
        public int id { get; set; }
        public string legajo { get; set; }
        public bool cursando { get; set; }
        public int anio_situacion { get; set; }
        public int cant_materias_cursadas_anterior { get; set; }
        public int cant_materias_aprobadas_periodo_anterior { get; set; }
        public int cant_materias_cursando { get; set; }
        public int cant_materias_aprobadas_total { get; set; }
        public decimal prom_gral_con_aplazos { get; set; }
        public decimal prom_gral_sin_aplazos { get; set; }
        public int ingreso { get; set; }
        public SituacionesAcademicas()
        {

        }

        public SituacionesAcademicas(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            legajo = data["legajo"].ToString() ?? "0";
            cursando = bool.Parse(data["cursando"].ToString() ?? "0");
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
