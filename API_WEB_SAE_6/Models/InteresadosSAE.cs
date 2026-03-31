using System.Data;

namespace API_WEB_SAE_6.Models
{
    /// <summary> </summary>
    public class InteresadosSAE
    {
        /// <summary> </summary>
        public int id { get; set; } = -1;
        /// <summary> </summary>
        public string nombre_interesado { get; set; } = "";
        /// <summary> </summary>
        public string contacto { get; set; } = "";
        /// <summary> </summary>
        public string email { get; set; } = "";
        /// <summary> </summary>
        public InteresadosSAE()
        {

        }
        /// <summary> </summary>
        public InteresadosSAE(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            nombre_interesado = data["nombre_entidad"].ToString() ?? "NO DATA";
            contacto = data["contacto"].ToString() ?? "NO DATA";
            email = data["email"].ToString() ?? "NO DATA";
        }
    }
}
