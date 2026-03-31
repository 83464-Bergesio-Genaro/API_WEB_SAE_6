using System.ComponentModel.DataAnnotations;

namespace API_WEB_SAE_6.Models
{
    /// <summary>
    /// Clase que representa las funciones de un empleado en el sistema.
    /// </summary>
    public class Funciones
    {
        /// <summary>
        /// Identificador único de la función. Es la clave primaria de la tabla en la base de datos.
        /// </summary>
        [Key]
        public int id { get; set; } = -1;
        /// <summary>
        /// Nombre de la función que desempeña el empleado. Este campo es obligatorio y no puede ser nulo.
        /// </summary>
        public string nombre_funcion { get; set; } = "";
        /// <summary>
        /// Descripción detallada de la función que desempeña el empleado. Este campo es opcional y puede ser nulo.
        /// </summary>
        public string descripcion { get; set; } = "";
    }
}
