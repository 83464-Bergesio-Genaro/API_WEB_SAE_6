using System.ComponentModel.DataAnnotations;

namespace API_WEB_SAE_6.Models
{
    public class Funciones
    {
        [Key]
        public int id { get; set; }
        public string nombre_funcion { get; set; }
        public string descripcion { get; set; }
    }
}
