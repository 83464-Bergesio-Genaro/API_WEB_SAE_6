using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class BecariosSAEServicio
    {
        public int id { get; set; }
        public ServiciosInternosFacultad? servicio { get; set; }
        public int modulos_asignados { get; set; }
        public BecariosSAE becario { get; set; }

        public BecariosSAEServicio()
        {

        }

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
