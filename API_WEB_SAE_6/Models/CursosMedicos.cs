using System.Data;

namespace API_WEB_SAE_6.Models
{
    /// <summary>
    /// Clase que representa un curso médico, con propiedades como id, nombre del curso, nombre del docente, fechas de inicio y fin, cupo máximo y estado activo.
    /// </summary>
    public class CursosMedicos
    {
        /// <summary>
        /// Propiedad que representa el identificador único del curso médico.
        /// </summary>
        public int id { get; set; } = -1;
        /// <summary>
        /// Propiedad que representa el nombre del curso médico.
        /// </summary>
        public string nombre_curso { get; set; } = "";
        /// <summary>
        /// Propiedad que representa el nombre del docente encargado del curso médico.
        /// </summary>
        public string nombre_docente { get; set; } = "";
        /// <summary>
        /// Propiedades que representan las fechas de inicio y fin del curso médico, respectivamente.
        /// </summary>
        public DateTime fecha_inicio { get; set; } = DateTime.MinValue;
        /// <summary>
        /// Propiedad que representa la fecha de fin del curso médico.
        /// </summary>
        public DateTime fecha_fin { get; set; } = DateTime.MinValue;
        /// <summary>
        /// Propiedad que representa el cupo máximo de participantes permitido en el curso médico.
        /// </summary>
        public int cupo_maximo { get; set; } = -1;
        /// <summary>
        /// Propiedad que representa el estado activo del curso médico, indicando si el curso está disponible para inscripción o no.
        /// </summary>
        public bool activo { get; set; } = false;
        /// <summary>
        /// Constructor por defecto de la clase CursosMedicos, que inicializa las propiedades con valores predeterminados.
        /// </summary>
        public CursosMedicos(){}
        /// <summary>
        /// Constructor que recibe un DataRow como parámetro y asigna los valores de las propiedades de la clase CursosMedicos a partir de los datos contenidos en el DataRow. Este constructor permite crear una instancia de CursosMedicos a partir de una fila de datos obtenida de una base de datos o cualquier otra fuente de datos estructurada.
        /// </summary>
        /// <param name="data"></param>
        public CursosMedicos(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            nombre_curso = data["nombre_curso"].ToString() ?? "NO DATA";
            nombre_docente = data["nombre_docente"].ToString() ?? "NO DATA";
            fecha_inicio = DateTime.Parse(data["fecha_inicio"].ToString() ?? "2000-01-01");
            fecha_fin = DateTime.Parse(data["fecha_fin"].ToString() ?? "2000-01-01");
            cupo_maximo = int.Parse(data["cupo_maximo"].ToString() ?? "0");
            activo = (data["activo"].ToString() == "1");
        }
    }
}
