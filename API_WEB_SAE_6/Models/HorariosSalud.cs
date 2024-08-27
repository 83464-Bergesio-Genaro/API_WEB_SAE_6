using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class HorariosSalud
    {
        public int id { get; set; }
        public string hora_inicio { get; set; }
        public string hora_fin { get; set; }
        public int dia { get; set; }
        public string cuil_especialista { get; set; }
        public string especialista { get; set; }
        public bool activo { get; set; }
        public Especialidad especialidad { get; set; }
        public HorariosSalud()
        {

        }
        public HorariosSalud(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            hora_inicio = data["hora_inicio"].ToString() ?? "NO DATA";
            hora_fin = data["hora_fin"].ToString() ?? "NO DATA";
            dia = int.Parse(data["dia"].ToString() ?? "0");
            cuil_especialista = data["cuil_especialista"].ToString() ?? "NO DATA";
            especialista = data["especialista"].ToString() ?? "NO DATA";
            activo = bool.Parse(data["activo"].ToString() ?? "0");

            int id_especialidad = int.Parse(data["id_especialidad"].ToString() ?? "0");
            string especialidad = data["especialidad"].ToString() ?? "NO DATA";
            string descripcion_actividades = data["descripcion_actividades"].ToString() ?? "NO DATA";
            bool especialidad_activa = bool.Parse(data["especialidad_activa"].ToString() ?? "0");

            this.especialidad = new Especialidad(id_especialidad, especialidad, descripcion_actividades, especialidad_activa);
        }
    }
}
