using System.ComponentModel.DataAnnotations;

namespace API_WEB_SAE_6.Models
{
    public class Sesiones
    {
        [Key]
        public int id { get; set; }
        public int id_usuario { get; set; }
        public DateTime fecha_hora_inicio { get; set; }
        public DateTime fecha_hora_final { get; set; }
        public DateTime? fecha_hora_servidor { get; set; }
    }
}
