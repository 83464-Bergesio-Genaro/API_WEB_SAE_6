namespace API_WEB_SAE_6.Models.Reporteria
{
    /// <summary>
    /// Representa las estadisticias del modulo de deportes
    /// </summary>
    public class EstadisticasDeportes
    {
        /// <summary>
        /// Representa la cantidad de deportistas en un deporte
        /// </summary>
        public class DeporteTipo
        {
            /// <summary>
            /// El identificador del deporte en la base de datos
            /// </summary>
            public int id_deporte { get; set; } = -1;
            /// <summary>
            /// Futbol 11, Basquet, Voley, etc
            /// </summary>
            public string nombre { get; set; } = "";
            /// <summary>
            /// La cantidad de alumnos en el torneo o deporte
            /// </summary>
            public int cantidad { get; set; } = 0;
            /// <summary>
            /// El año al que corresponde esta estadistica
            /// </summary>
            public int anio { get; set; } = DateTime.Now.Year;
        }
        /// <summary>
        /// Representa la cantidad de deportistas inscritos en cada deporte en cada año
        /// </summary>
        public List<DeporteTipo> InscripcionesDeportistas { get; set; } = [];
        /// <summary>
        /// Representa la cantidad de torneos realizados por deporte en cada año
        /// </summary>
        public List<DeporteTipo> TorneosXDeporte{ get; set; } = [];
    }
}
