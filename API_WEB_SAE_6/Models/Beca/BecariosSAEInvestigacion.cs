using System.Data;

namespace API_WEB_SAE_6.Models.Beca
{
    /// <summary></summary>
    public class BecariosSAEInvestigacion
    {
        /// <summary></summary>
        public int id { get; set; } = -1;
        /// <summary></summary>
        public ProyectosInvestigacion? proyecto_investigacion { get; set; }
        /// <summary></summary>
        public int modulos_asignados { get; set; } = -1;
        /// <summary></summary>
        public BecariosSAE becario { get; set; } = new();
        /// <summary></summary>
        public BecariosSAEInvestigacion() { }
        /// <summary></summary>
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
