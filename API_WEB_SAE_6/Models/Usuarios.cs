using System.ComponentModel.DataAnnotations;
using System.Data;

namespace API_WEB_SAE_6.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Usuarios
    {
        /// <summary> </summary>
        [Key]
        public int id { get; set; } = -1;
        /// <summary> </summary>
        public string legajo { get; set; } = "";
        /// <summary> </summary>
        public string nombre_usuario { get; set; } = "";
        /// <summary> </summary>
        public int id_perfil { get; set; } = -1;
        /// <summary> </summary>
        public bool activo { get; set; } = false;
        /// <summary> </summary>
        public Usuarios(){}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public Usuarios(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            legajo = data["legajo"].ToString() ?? "NO DATA";
            nombre_usuario = data["nombre_usuario"].ToString() ?? "NO DATA";
            id_perfil = int.Parse(data["id_perfil"].ToString() ?? "0");
            activo = (data["activo"].ToString() == "1");
        }
    }
}
