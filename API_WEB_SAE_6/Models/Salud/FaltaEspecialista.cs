using System.Data;

namespace API_WEB_SAE_6.Models.Salud
{
    /// <summary>
    /// Clase que representa una falta de un especialista. Contiene propiedades como id, cuil, fecha de alta y observación. Proporciona un constructor vacío y otro que inicializa las propiedades a partir de un DataRow.
    /// </summary>
    public class FaltaEspecialista
    {
        /// <summary>
        /// Propiedad que representa el identificador único de la falta del especialista.
        /// </summary>
        public int id { get; set; } = -1;
        /// <summary>
        /// Propiedad que representa el CUIL del especialista que tiene la falta. Es una cadena de texto que identifica de manera única al especialista en cuestión.
        /// </summary>
        public string cuil { get; set; } = "";
        /// <summary>
        /// Propiedad que representa la fecha en la que se registró la falta del especialista. Es de tipo DateTime y se utiliza para llevar un registro temporal de las faltas.
        /// </summary>
        public DateTime fecha_alta { get; set; } = DateTime.MinValue;
        /// <summary>
        /// Propiedad que representa una observación o comentario adicional relacionado con la falta del especialista. Es una cadena de texto que puede contener información relevante sobre la falta, como detalles específicos o circunstancias relacionadas.
        /// </summary>
        public string observacion { get; set; } = "";
        /// <summary>
        /// Constructor vacío que permite crear una instancia de la clase FaltaEspecialista sin inicializar sus propiedades. Este constructor es útil cuando se desea crear un objeto y luego asignar valores a sus propiedades de manera individual.
        /// </summary>
        public FaltaEspecialista(){}
        /// <summary>
        /// Constructor que inicializa las propiedades de la clase FaltaEspecialista a partir de un DataRow. Este constructor toma un DataRow como parámetro, extrae los valores correspondientes a cada propiedad y los asigna a las propiedades de la clase. Es útil para crear una instancia de FaltaEspecialista a partir de datos obtenidos de una base de datos o cualquier otra fuente que devuelva un DataRow.
        /// </summary>
        /// <param name="data"></param>
        public FaltaEspecialista(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            cuil = data["cuil_especialistas"].ToString() ?? "NO DATA";
            fecha_alta = DateTime.Parse(data["fecha_falta"].ToString() ?? "2000-01-01");
            observacion = data["observacion"].ToString() ?? "NO DATA";
        }
    }
}
