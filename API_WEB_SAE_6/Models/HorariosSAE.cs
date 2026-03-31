using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace API_WEB_SAE_6.Models
{
    /// <summary>
    /// Clase que representa los horarios de atención en el sistema SAE. Contiene información sobre el horario de inicio, horario de fin, día de la semana, empleado asignado y estado de actividad del horario. Esta clase se utiliza para gestionar y almacenar los horarios de atención en la aplicación.
    /// </summary>
    public class HorariosSAE
    {
        /// <summary>
        /// Identificador único del horario de atención. Este campo es la clave primaria de la tabla y se utiliza para identificar de manera única cada registro de horario en la base de datos.
        /// </summary>
        [Key]
        public int id { get; set; } = -1;
        /// <summary>
        /// Hora de inicio del horario de atención. Este campo almacena la hora en formato de cadena (HH:mm:ss) que indica el momento en que comienza el horario de atención para un empleado específico. Es importante para determinar los períodos de atención y gestionar los turnos de trabajo.
        /// </summary>
        public string hora_inicio { get; set; } = "";
        /// <summary>
        /// Hora de fin del horario de atención. Este campo almacena la hora en formato de cadena (HH:mm:ss) que indica el momento en que finaliza el horario de atención para un empleado específico. Es crucial para definir los períodos de atención y gestionar los turnos de trabajo, asegurando que se respeten los horarios establecidos para cada empleado.
        /// </summary>
        public string hora_fin { get; set; } = "";
        /// <summary>
        /// Día de la semana para el horario de atención. Este campo almacena un valor entero que representa el día de la semana, donde 0 corresponde a domingo, 1 a lunes, 2 a martes, 3 a miércoles, 4 a jueves, 5 a viernes y 6 a sábado. Este campo es esencial para organizar los horarios de atención según los días de la semana y facilitar la planificación de turnos para los empleados.
        /// </summary>
        public int dia { get; set; } = -1;
        /// <summary>
        /// Identificador del empleado asignado al horario de atención. Este campo almacena un valor entero que representa el ID del empleado responsable de atender durante el horario especificado. Es fundamental para vincular los horarios de atención con los empleados correspondientes y gestionar eficazmente los turnos de trabajo en la aplicación.
        /// </summary>
        public int id_empleado { get; set; } = -1;
        /// <summary>
        /// Nombre del empleado asignado al horario de atención. Este campo almacena una cadena que representa el nombre del empleado responsable de atender durante el horario especificado. Es útil para mostrar información más legible y comprensible sobre los horarios de atención, facilitando la identificación de los empleados asignados a cada turno.
        /// </summary>
        public string? nombre_empleado_atencion { get; set; } = "";
        /// <summary>
        /// Indica si el horario de atención está activo o no. Este campo almacena un valor booleano que representa el estado de actividad del horario de atención. Un valor de "true" indica que el horario está activo y se utiliza para gestionar los turnos de trabajo, mientras que un valor de "false" indica que el horario está inactivo y no se considera para la planificación de turnos en la aplicación.
        /// </summary>
        public bool activo { get; set; } = false;

        /// <summary>
        /// Constructor por defecto de la clase HorariosSAE. Este constructor inicializa una nueva instancia de la clase sin asignar valores específicos a las propiedades. Es útil para crear objetos de tipo HorariosSAE sin necesidad de proporcionar datos iniciales, permitiendo que las propiedades se establezcan posteriormente a través de métodos o asignaciones directas.
        /// </summary>
        public HorariosSAE(){}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public HorariosSAE(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            hora_inicio = data["hora_inicio"].ToString() ?? "00:00:00";
            hora_fin = data["hora_fin"].ToString() ?? "00:00:00";
            dia = int.Parse(data["dia"].ToString() ?? "0");
            id_empleado = int.Parse(data["id_empleado"].ToString() ?? "0");
            nombre_empleado_atencion = data["nombre_empleado_atencion"].ToString() ?? "NO DATA";
            activo = data["activo"].ToString() == "1";
        }

    }
}
