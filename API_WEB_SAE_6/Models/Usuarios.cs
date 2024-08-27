using System.ComponentModel.DataAnnotations;
using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class Usuarios
    {
        [Key]
        public int id { get; set; }
        public string legajo { get; set; }
        public string nombre_usuario { get; set; }
        public int id_perfil { get; set; }
        public bool activo { get; set; }

        public Usuarios()
        {

        }
        public Usuarios(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            legajo = data["legajo"].ToString() ?? "NO DATA";
            nombre_usuario = data["nombre_usuario"].ToString() ?? "NO DATA";
            id_perfil = int.Parse(data["id_perfil"].ToString() ?? "0");
            activo = bool.Parse(data["activo"].ToString() ?? "false");
        }
    }
}
