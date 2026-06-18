using System.Data;

namespace API_WEB_SAE_6.Models.Beca
{
    /// <summary></summary>
    public class BecariosSAEServicio
    {
        /// <summary></summary>
        public int id { get; set; } = -1;
        /// <summary></summary>
        public ServiciosInternosFacultad? servicio { get; set; }
        /// <summary></summary>
        public int modulos_asignados { get; set; } = -1;
        /// <summary></summary>
        public BecariosSAE becario { get; set; } = new();
        /// <summary></summary>
        public BecariosSAEServicio() { }
        /// <summary></summary>
        public BecariosSAEServicio(DataRow data)
        {
            id = int.Parse(data["id_beca_servicio"].ToString() ?? "0");
            if (data["id_servicio"].ToString() == null || data["id_servicio"].ToString() == "") servicio = null;
            else servicio = new(data);
            modulos_asignados = int.Parse(data["modulos_asignados"].ToString() ?? "0");
            becario = new(data);
        }
    }
}
