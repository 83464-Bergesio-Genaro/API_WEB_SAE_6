using System.ComponentModel.DataAnnotations;

namespace API_WEB_SAE_6.Models
{
    /// <summary></summary>
    public class Sesiones
    {
        /// <summary> </summary>
        [Key]
        public int id { get; set; }
        /// <summary> </summary>
        public int id_usuario { get; set; }
        /// <summary> </summary>
        public DateTime fecha_hora_inicio { get; set; }
        /// <summary> </summary>
        public DateTime fecha_hora_final { get; set; }
        /// <summary> </summary>
        public DateTime? fecha_hora_servidor { get; set; }
    }
}
