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
        /// Corresponde a un estado asignado por nosotros para tratarlo en la base de datos.
        /// </summary>
        public class BecaEstado
        {
            /// <summary>
            /// Nombre del estado, acpetado o rechazado. En el ultimo año cuantos estan activos
            /// </summary>
            public string nombre { get; set; } = "";
            /// <summary>
            /// Una breve descripcion por las dudas
            /// </summary>
            public string descripcion { get; set; } = "";
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
        /// Clase comodin entre ambos tipos de becas, para poder mostrar la distribucion de becas por proyecto o servicio
        /// </summary>
        public class BecaDistribucion
        {
            /// <summary>
            /// Identifica el servicio o el proyecto
            /// </summary>
            public int id { get; set; } = -1;
            /// <summary>
            /// Es el valor que define si refiere a un proyecto o no
            /// </summary>
            public bool es_investigacion { get; set; } = false;
            /// <summary>
            /// Nombre del proyecto o servicio
            /// </summary>
            public string nombre { get; set; } = "";
            /// <summary>
            /// La cantidad de alumnos en esta categoria
            /// </summary>
            public int cantidad { get; set; } = 0;
        }
        /// <summary>
        /// Es el listado completo solicitado desde el endpoint
        /// </summary>
        public List<BecaTipo> YearXType { get; set; } = [];
        /// <summary>
        /// Es el listado de años por estaod
        /// </summary>
        public List<BecaEstado> YearXState { get; set; } = [];
        /// <summary>
        /// Sirve para las clases de servicio y de investigacion
        /// </summary>
        public List<BecaDistribucion> YearXDistribution { get; set; } = [];
    }
}
