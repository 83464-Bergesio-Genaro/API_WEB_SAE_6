using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class BecariosSAEInvestigacion
    {
        public int id { get; set; }
        public ProyectosInvestigacion? proyecto_investigacion { get; set; }
        public int modulos_asignados { get; set; }
        public BecariosSAE becario { get; set; }

        public BecariosSAEInvestigacion()
        {

        }

        public BecariosSAEInvestigacion(DataRow data)
        {
            id = int.Parse(data["id_investigacion"].ToString() ?? "0");
            if (data["id_proyecto"].ToString() == null || data["id_proyecto"].ToString() == "") proyecto_investigacion = null;
            else proyecto_investigacion = new(data);
            modulos_asignados = int.Parse(data["modulos_asignados"].ToString() ?? "0");
            becario = new BecariosSAE(data);
        }
    }
}
