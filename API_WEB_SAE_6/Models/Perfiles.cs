using System.ComponentModel.DataAnnotations;

namespace API_WEB_SAE_6.Models
{
    /// <summary> </summary>
    public class Perfiles
    {
        /// <summary> </summary>
        [Key]
        public int id { get; set; } = -1;
        /// <summary> </summary>
        public string nombre { get; set; } = "";
        /// <summary> </summary>
        public bool activo { get; set; } = false;
    }
}
