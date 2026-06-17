using System.Data;

namespace API_WEB_SAE_6.Models.Salud
{
    /// <summary>
    /// Clase que representa los turnos médicos en la aplicación. Contiene propiedades para almacenar información relevante sobre cada turno, como el ID del turno, el CUIL del médico, el especialista, el legajo, el paciente, las fechas de solicitud y atención, la hora de atención, el asunto del turno, el estado del turno y su descripción.
    /// </summary>
    public class TurnosMedicos
    {
        /// <summary>
        /// Propiedad que representa el ID del turno médico. Es un entero que identifica de manera única cada turno en la base de datos.
        /// </summary>
        public int id { get; set; } = -1;
        /// <summary>
        /// Propiedad que representa el CUIL del médico asociado al turno. Es una cadena de texto que puede ser nula, ya que no todos los turnos pueden tener un médico asignado en el momento de la solicitud.
        /// </summary>
        public string? cuil_medico { get; set; }
        /// <summary>
        /// Propiedad que representa el nombre del especialista asociado al turno. Es una cadena de texto que puede ser nula, ya que no todos los turnos pueden tener un especialista asignado en el momento de la solicitud.
        /// </summary>
        public string? especialista { get; set; }
        /// <summary>
        /// Propiedad que representa el legajo del médico asociado al turno. Es una cadena de texto que puede ser nula, ya que no todos los turnos pueden tener un médico asignado en el momento de la solicitud.
        /// </summary>
        public string legajo { get; set; } = "";
        /// <summary>
        /// Propiedad que representa el nombre del paciente asociado al turno. Es una cadena de texto que puede ser nula, ya que no todos los turnos pueden tener un paciente asignado en el momento de la solicitud.
        /// </summary>
        public string? paciente { get; set; }
        /// <summary>
        /// Propiedad que representa la fecha en que se solicitó el turno médico. Es un valor de tipo DateTime que almacena la fecha y hora de la solicitud del turno. Esta propiedad es obligatoria, ya que cada turno debe tener una fecha de solicitud para ser registrado en la base de datos.
        /// </summary>
        public DateTime fecha_solicitud { get; set; } = DateTime.MinValue;
        /// <summary>
        /// Propiedad que representa la fecha en que se atendió el turno médico. Es un valor de tipo DateTime que almacena la fecha y hora de la atención del turno. Esta propiedad es opcional, ya que no todos los turnos pueden haber sido atendidos en el momento de la consulta o registro del turno en la base de datos.
        /// </summary>
        public DateTime? fecha_atencion { get; set; } = DateTime.MinValue;
        /// <summary>
        /// Propiedad que representa la hora en que se atendió el turno médico. Es una cadena de texto que almacena la hora de atención del turno. Esta propiedad es opcional, ya que no todos los turnos pueden haber sido atendidos en el momento de la consulta o registro del turno en la base de datos.
        /// </summary>
        public string? hora_atencion { get; set; }
        /// <summary>
        /// Propiedad que representa el asunto del turno médico. Es una cadena de texto que almacena el motivo o la razón por la cual se solicitó el turno. Esta propiedad es opcional, ya que no todos los turnos pueden tener un asunto registrado en el momento de la consulta o registro del turno en la base de datos.
        /// </summary>
        public string asunto { get; set; } = "";
        /// <summary>
        /// El estado en el cual esta el turno actualmente
        /// </summary>
        public EstadosTurno? estadosTurno { get; set; }
        /// <summary>
        /// Constructor por defecto de la clase TurnosMedicos. Este constructor no realiza ninguna inicialización específica y se utiliza principalmente para crear instancias de la clase sin proporcionar datos iniciales. Las propiedades de la clase se inicializan con valores predeterminados, como -1 para enteros, cadenas vacías para textos y DateTime.MinValue para fechas.
        /// </summary>
        public TurnosMedicos(){}
        /// <summary>
        /// Constructor de la clase TurnosMedicos que recibe un objeto DataRow como parámetro. Este constructor se utiliza para crear una instancia de la clase a partir de los datos contenidos en el DataRow, que generalmente proviene de una consulta a la base de datos. El constructor asigna los valores de las columnas del DataRow a las propiedades correspondientes de la clase, realizando conversiones de tipo cuando es necesario. Si alguna columna no contiene datos válidos, se asignan valores predeterminados para evitar errores en la creación del objeto.
        /// </summary>
        /// <param name="data"></param>
        public TurnosMedicos(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            cuil_medico = data["cuil_medico"].ToString() ?? "NO DATA";
            especialista = data["especialista"].ToString() ?? "NO DATA";
            legajo = data["legajo"].ToString() ?? "NO DATA";
            paciente = data["paciente"].ToString() ?? "NO DATA";
            fecha_solicitud = DateTime.Parse(data["fecha_solicitud"].ToString() ?? "2000-01-01");
            if (data["fecha_atencion"].ToString() == "") fecha_atencion = null;
            else fecha_atencion = DateTime.Parse(data["fecha_atencion"].ToString() ?? "2000-01-01");
            hora_atencion = data["hora_atencion"].ToString() ?? "00:00:00";
            asunto = data["asunto"].ToString() ?? "NO DATA";

            estadosTurno = new(int.Parse(data["id_estado_turno"].ToString() ?? "0"), data["estado"].ToString() ?? "NO DATA");
        }
    }
}
