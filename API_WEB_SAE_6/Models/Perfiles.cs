using System.ComponentModel.DataAnnotations;

namespace API_WEB_SAE_6.Models
{
    public class Perfiles
    {
        [Key]
        public int id { get; set; }
        public string nombre { get; set; }
        public bool activo { get; set; }
    }
}
