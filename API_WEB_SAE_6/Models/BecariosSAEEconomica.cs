using System.Data;

namespace API_WEB_SAE_6.Models
{
    /// <summary></summary>
    public class BecariosSAEEconomica
    {
        /// <summary></summary>
        public int id { get; set; } = -1;
        /// <summary></summary>
        public bool entrevista_realizada { get; set; } = false;
        /// <summary></summary>
        public int modulos_asignados { get; set; } = -1;
        /// <summary></summary>
        public BecariosSAE becario { get; set; } = new();
        /// <summary></summary>
        public BecariosSAEEconomica(){}
        /// <summary></summary>
        public BecariosSAEEconomica(DataRow data)
        {
            id = int.Parse(data["id_economica"].ToString() ?? "0");
            entrevista_realizada = bool.Parse(data["entrevista_realizada"].ToString() ?? "0");
            modulos_asignados = int.Parse(data["modulos_asignados"].ToString() ?? "0");
            becario = new BecariosSAE(data);
        }
    }
}
