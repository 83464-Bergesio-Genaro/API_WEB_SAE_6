namespace API_WEB_SAE_6.Models.Reporteria
{
    /// <summary>
    /// Representa la estadisticas de viajes de la secretaria
    /// </summary>
    public class EstadisticasViaje
    {
        /// <summary>
        /// Representa la cantidad de alumnos en el sistema.
        /// </summary>
        public class ViajesXFecha
        {
            /// <summary>
            /// La cantidad de viajes que se realizaron en el año y mes especificado
            /// </summary>
            public int cantidad { get; set; } = 0;
            /// <summary>
            /// La cantidad de estudiantes que realizaron viajes en el año y mes especificado
            /// </summary>
            public int cantidad_estudiantes { get; set; } = 0;
            /// <summary>
            /// El id de la carrera que realiza el viaje
            /// </summary>
            public int id_especialidad { get; set; } = 0;
            /// <summary>
            /// El año al que corresponde esta estadistica
            /// </summary>
            public int anio { get; set; } = DateTime.Now.Year;
            /// <summary>
            /// El mes que corresponde esta estadistica
            /// </summary>
            public int mes { get; set; } = DateTime.Now.Year;
        }
 
        /// <summary>
        /// Representa la cantidad de alumnos por año en el sistema.
        /// </summary>
        public List<ViajesXFecha> ViajesXMesAnio = [];
    }
}
