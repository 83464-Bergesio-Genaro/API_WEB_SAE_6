using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class ProyectosInvestigacion
    {
        public int id { get; set; }
        public string nombre_proyecto_investigacion { get; set; }
        public bool activo { get; set; }
        public string centro_investigacion { get; set; }
        public ProyectosInvestigacion()
        {

        }

        public ProyectosInvestigacion(DataRow data)
        {
            id = int.Parse(data["id_proyecto"].ToString() ?? "0");
            nombre_proyecto_investigacion = data["nombre_proyecto_investigacion"].ToString() ?? "NO ASIGNADO";
            activo = bool.Parse(data["proyecto_activo"].ToString() ?? "0");
            centro_investigacion = data["centro_investigacion"].ToString() ?? "NO ASIGNADO";
        }
    }
}
