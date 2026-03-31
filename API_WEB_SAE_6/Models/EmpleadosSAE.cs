using System.ComponentModel.DataAnnotations;
using System.Data;

namespace API_WEB_SAE_6.Models
{
    /// <summary>
    /// Clase que representa a los empleados del sistema SAE, esta clase se utiliza para mapear la informacion de la base de datos y para mostrarla en la API, no se utiliza para guardar la informacion, ya que para eso se utiliza la clase EmpleadoAdapter, esta clase es solo para mostrar la informacion de los empleados en la API.
    /// </summary>
    public class EmpleadosSAE
    {
        /// <summary>
        /// Id del empleado, es la clave primaria de la tabla empleados, se utiliza para identificar univocamente a cada empleado, no se puede repetir, es autoincremental, no se puede modificar, se utiliza para relacionar con otras tablas, como por ejemplo la tabla de turnos, para saber que empleado tiene un turno asignado.
        /// </summary>
        [Key]
        public int id { get; set; } = -1;
        /// <summary>
        /// Legajo del empleado, es un numero unico que se asigna a cada empleado, se utiliza para identificar univocamente a cada empleado, no se puede repetir, se utiliza para relacionar con otras tablas, como por ejemplo la tabla de turnos, para saber que empleado tiene un turno asignado.
        /// </summary>
        public string legajo { get; set; } = "";
        /// <summary>
        /// Nombre del empleado, es el nombre completo del empleado, se utiliza para mostrar el nombre del empleado en la API, no se puede repetir, se utiliza para relacionar con otras tablas, como por ejemplo la tabla de turnos, para saber que empleado tiene un turno asignado.
        /// </summary>
        public string? nombre_empleado { get; set; }
        /// <summary>
        /// Activo del empleado, es un booleano que indica si el empleado esta activo o no, se utiliza para mostrar solo los empleados activos en la API, no se puede modificar, se utiliza para relacionar con otras tablas, como por ejemplo la tabla de turnos, para saber que empleado tiene un turno asignado. Si el empleado esta inactivo, no se muestra en la API y no se le pueden asignar turnos.
        /// </summary>
        public bool activo { get; set; } = false;
        /// <summary>
        /// Constructor de la clase EmpleadosSAE, se utiliza para crear un nuevo empleado, se puede utilizar para crear un nuevo empleado a partir de un DataRow, que es el resultado de una consulta a la base de datos, se utiliza para mapear la informacion de la base de datos a la clase EmpleadosSAE, se utiliza para mostrar la informacion de los empleados en la API, no se utiliza para guardar la informacion, ya que para eso se utiliza la clase EmpleadoAdapter, esta clase es solo para mostrar la informacion de los empleados en la API.
        /// </summary>
        public EmpleadosSAE(){}
        /// <summary>
        /// Constructor de la clase EmpleadosSAE, se utiliza para crear un nuevo empleado a partir de un DataRow, que es el resultado de una consulta a la base de datos, se utiliza para mapear la informacion de la base de datos a la clase EmpleadosSAE, se utiliza para mostrar la informacion de los empleados en la API, no se utiliza para guardar la informacion, ya que para eso se utiliza la clase EmpleadoAdapter, esta clase es solo para mostrar la informacion de los empleados en la API.
        /// </summary>
        /// <param name="data"></param>
        public EmpleadosSAE(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            legajo = data["legajo"].ToString() ?? "ERROR";
            nombre_empleado = data["nombre_empleado"].ToString() ?? "ERROR";
            activo = (data["activo"].ToString() == "1");
        }
    }
}
