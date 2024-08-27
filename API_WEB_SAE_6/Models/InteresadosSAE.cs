using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class InteresadosSAE
    {
        public int id { get; set; }
        public string nombre_interesado { get; set; }
        public string contacto { get; set; }
        public string email { get; set; }

        public InteresadosSAE()
        {

        }

        public InteresadosSAE(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            nombre_interesado = data["nombre_entidad"].ToString() ?? "NO DATA";
            contacto = data["contacto"].ToString() ?? "NO DATA";
            email = data["email"].ToString() ?? "NO DATA";
        }
    }
}
