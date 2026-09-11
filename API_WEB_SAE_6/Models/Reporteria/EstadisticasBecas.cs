using System.Data;

namespace API_WEB_SAE_6.Models.Reporteria
{
    /// <summary>
    /// Represents the statistics of scholarships (becas) in the system.
    /// </summary>
    public class EstadisticasBecas
    {
        /// <summary>
        /// Representa la cantidad de becas otorgadas por tipo en un año específico.
        /// </summary>
        public class BecaTipo
        {
            /// <summary>
            /// Economica, Investigacion o Servicio
            /// </summary>
            public string nombre { get; set; } = "";
            /// <summary>
            /// La cantidad de alumnos en esta categoria
            /// </summary>
            public int cantidad { get; set; } = 0;
            /// <summary>
            /// El año al que corresponde esta estadistica
            /// </summary>
            public int anio { get; set; } = DateTime.Now.Year;
        }

        /// <summary>
        /// Es el listado completo solicitado desde el endpoint
        /// </summary>
        public List<BecaTipo> YearXType { get; set; } = [];
        /// <summary>
        /// Es el listado de años por estados
        /// </summary>
        public List<BecaTipo> YearXState { get; set; } = [];
        /// <summary>
        /// Sirve para saber la tasa de renovacion
        /// </summary>
        public List<BecaTipo> YearXRenovation { get; set; } = [];
    }
}
