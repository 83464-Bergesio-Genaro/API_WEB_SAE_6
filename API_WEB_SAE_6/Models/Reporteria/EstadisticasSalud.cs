namespace API_WEB_SAE_6.Models.Reporteria
{
    /// <summary>
    /// Representa las estadisticas de area de salud
    /// </summary>
    public class EstadisticasSalud
    {
        /// <summary>
        /// Representa la cantidad de turnos de salud en el sistema.
        /// </summary>
        public class EstadisticaTurno
        {
            /// <summary>
            /// La cantidad de turnos en el año
            /// </summary>
            public int cantidad { get; set; } = 0;
            /// <summary>
            /// De la especialidad que corresponde estos turnos
            /// </summary>
            public string nombre { get; set; } = "";
            /// <summary>
            /// El año al que corresponde esta estadistica
            /// </summary>
            public int anio { get; set; } = DateTime.Now.Year;
            /// <summary>
            /// El mes al que corresponde esta estadistica
            /// </summary>
            public int mes {  get; set; } = DateTime.Now.Month;
        }
        /// <summary>
        /// Representa la cantidad de turnos de salud por especialidad en el sistema.
        /// </summary>
        public List<EstadisticaTurno> TurnosXEspecialista { get; set; } = [];
    }
}
