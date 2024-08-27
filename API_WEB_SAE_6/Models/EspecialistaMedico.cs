using System.ComponentModel.DataAnnotations;
using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class EspecialistaMedico
    {
        [Key]
        public string cuil { get; set; }
        public string apellido { get; set; }
        public string nombre { get; set; }
        public bool presta_servicio { get; set; }
        public Especialidad especialidad { get; set; }

        public EspecialistaMedico()
        {

        }

        public EspecialistaMedico(DataRow data)
        {
            cuil = data["cuil"].ToString() ?? "NO DATA";
            nombre = data["nombre"].ToString() ?? "NO DATA";
            apellido = data["apellido"].ToString() ?? "NO DATA";
            presta_servicio = bool.Parse(data["activo"].ToString() ?? "0");
            int id_especialidad = int.Parse(data["id_especialidad"].ToString() ?? "0");
            string especialidad = data["especialidad"].ToString() ?? "NO DATA";
            string descripcion_actividades = data["descripcion_actividades"].ToString() ?? "NO DATA";
            bool especialidad_activa = bool.Parse(data["especialidad_activa"].ToString() ?? "0");

            this.especialidad = new Especialidad(id_especialidad, especialidad, descripcion_actividades, especialidad_activa);
        }
    }
}
