using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class BecariosSAEEconomica
    {
        public int id { get; set; }
        public bool entrevista_realizada { get; set; }
        public int modulos_asignados { get; set; }
        public BecariosSAE becario { get; set; }
        public BecariosSAEEconomica()
        {

        }

        public BecariosSAEEconomica(DataRow data)
        {
            id = int.Parse(data["id_economica"].ToString() ?? "0");
            entrevista_realizada = bool.Parse(data["entrevista_realizada"].ToString() ?? "0");
            modulos_asignados = int.Parse(data["modulos_asignados"].ToString() ?? "0");
            becario = new BecariosSAE(data);
        }
    }
}
