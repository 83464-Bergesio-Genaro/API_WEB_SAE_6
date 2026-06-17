using System.Data;

namespace API_WEB_SAE_6.Models.Prensa
{
    /// <summary>
    /// Clase que representa una publicación en el sistema. Contiene información sobre el título, descripción, fechas de inicio y vigencia, prioridad, estado de baja y número de visualizaciones.
    /// </summary>
    public class Publicaciones
    {
        /// <summary>
        /// Identificador único de la publicación. Es un número entero que se asigna automáticamente al crear una nueva publicación.
        /// </summary>
        public int id { get; set; } = -1;
        /// <summary>
        /// Título de la publicación. Es una cadena de texto que describe brevemente el contenido de la publicación. Este campo es obligatorio y no puede estar vacío.
        /// </summary>
        public string titulo_publicacion { get; set; } = "";
        /// <summary>
        /// Descripción de la publicación. Es una cadena de texto que proporciona detalles adicionales sobre el contenido de la publicación. Este campo es opcional y puede estar vacío.
        /// </summary>
        public string descripcion { get; set; } = "";
        /// <summary>
        /// Fecha de inicio de la publicación. Es un valor de tipo DateTime que indica cuándo comienza la vigencia de la publicación. Este campo es obligatorio y debe ser una fecha válida.
        /// </summary>
        public DateTime fecha_inicio { get; set; } = DateTime.MinValue;
        /// <summary>
        /// Fecha de vigencia de la publicación. Es un valor de tipo DateTime que indica cuándo termina la vigencia de la publicación. Este campo es obligatorio y debe ser una fecha válida que sea posterior a la fecha de inicio.
        /// </summary>
        public DateTime fecha_vigencia { get; set; } = DateTime.MinValue;
        /// <summary>
        /// Prioridad de la publicación. Es un número entero que indica la importancia de la publicación en relación con otras publicaciones. Un valor más alto indica una mayor prioridad. Este campo es opcional y puede estar vacío, en cuyo caso se considerará una prioridad predeterminada.
        /// </summary>
        public int prioridad { get; set; } = -1;
        /// <summary>
        /// Indica si la publicación no debe ser dada de baja. Es un valor booleano que, si es verdadero, indica que la publicación no debe ser eliminada o desactivada, incluso si ha pasado su fecha de vigencia. Este campo es opcional y puede estar vacío, en cuyo caso se considerará que la publicación puede ser dada de baja según las reglas del sistema.
        /// </summary>
        public bool no_dar_baja { get; set; } = false;
        /// <summary>
        /// Número de visualizaciones de la publicación. Es un número entero que indica cuántas veces ha sido vista la publicación por los usuarios. Este campo es opcional y puede estar vacío, en cuyo caso se considerará que la publicación no ha sido visualizada.
        /// </summary>
        public int visualizaciones { get; set; } = -1;
        /// <summary>
        /// Cree esta columna para dar mostrar que documentos estan asociados en el front end y el usuario haciendo click pueda descargarlo
        /// </summary>
        public string? documentos_asociados { get; set; }
        /// <summary>
        /// Constructor por defecto de la clase Publicaciones. Inicializa los campos con valores predeterminados.
        /// </summary>
        public Publicaciones(){}
        /// <summary>
        /// Constructor que inicializa una instancia de la clase Publicaciones a partir de un DataRow. Este constructor se utiliza para crear una publicación a partir de los datos obtenidos de una consulta a la base de datos. Los valores de los campos se asignan a partir de las columnas del DataRow, y se realizan conversiones de tipo según sea necesario.
        /// </summary>
        /// <param name="data"></param>
        public Publicaciones(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            titulo_publicacion = data["titulo_publicacion"].ToString() ?? "0";
            descripcion = data["descripcion"].ToString() ?? "0";
            fecha_inicio = DateTime.Parse(data["fecha_inicio"].ToString() ?? "0");
            fecha_vigencia = DateTime.Parse(data["fecha_vigencia"].ToString() ?? "0");
            prioridad = int.Parse(data["prioridad"].ToString() ?? "0");
            no_dar_baja = data["no_dar_baja"].ToString() == "1";
            visualizaciones = int.Parse(data["visualizaciones"].ToString() ?? "0");
            documentos_asociados = data["documentos_asociados"].ToString();
        }
    }
}
