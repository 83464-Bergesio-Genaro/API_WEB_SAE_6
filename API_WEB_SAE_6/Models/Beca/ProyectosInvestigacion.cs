using System.Data;

namespace API_WEB_SAE_6.Models.Beca
{
    /// <summary></summary>
    public class ProyectosInvestigacion
    {
        /// <summary></summary>
        public int id { get; set; } = -1;
        /// <summary></summary>
        public string nombre_proyecto_investigacion { get; set; } = "";
        /// <summary></summary>
        public bool activo { get; set; } = false;
        /// <summary></summary>
        public string centro_investigacion { get; set; } = "";
        /// <summary></summary>
        public ProyectosInvestigacion(){}
        /// <summary></summary>
        public ProyectosInvestigacion(DataRow data)
        {
            id = int.Parse(data["id_proyecto"].ToString() ?? "0");
            nombre_proyecto_investigacion = data["nombre_proyecto_investigacion"].ToString() ?? "NO ASIGNADO";
            activo = data["proyecto_activo"].ToString() == "1";
            centro_investigacion = data["centro_investigacion"].ToString() ?? "NO ASIGNADO";
        }
    }
}
