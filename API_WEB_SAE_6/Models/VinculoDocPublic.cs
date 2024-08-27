using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class VinculoDocPublic
    {
        public int id { get; set; }
        public int id_publicacion { get; set; }
        public int id_documento { get; set; }
        public DateTime fecha_creacion { get; set; }
        public VinculoDocPublic()
        {

        }

        public VinculoDocPublic(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            id_publicacion = int.Parse(data["id_publicacion"].ToString() ?? "0");
            id_documento = int.Parse(data["id_documento"].ToString() ?? "0");
            fecha_creacion = DateTime.Parse(data["fecha_creacion"].ToString() ?? "1900-01-01");
        }
    }
}
