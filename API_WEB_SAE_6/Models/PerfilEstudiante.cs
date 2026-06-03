using System.Data;

namespace API_WEB_SAE_6.Models
{
    /// <summary>
    /// Son los datos que se muestran en el perfil del estudiante, se pueden mostrar en la aplicacion para que el estudiante pueda ver su informacion personal y su informacion academica, se puede usar para mostrar la informacion del estudiante en la aplicacion, no tiene un constructor con parametros porque no se necesita crear objetos de esta clase con los valores que se le pasan por parametro, se puede usar para mostrar la informacion del estudiante en la aplicacion, asigna los valores de los atributos con los valores que se le pasan por parametro, no hace nada en particular, solo asigna los valores de los atributos con los valores que se le pasan por parametro
    /// </summary>
    public class PerfilEstudiante
    {
        /// <summary>
        /// Identificador unico del estudiante, se usa para buscarlo en la base de datos y para mostrar su informacion, no puede ser nulo ni vacio, es la clave primaria de la tabla estudiantes
        /// </summary>
        public string legajo { get; set; } = "";
        /// <summary>
        /// Los nombres son los nombres del estudiante, se usan para mostrar su informacion, no pueden ser nulos ni vacios, se pueden usar para mostrar la informacion del estudiante en la aplicacion
        /// </summary>
        public string nombres { get; set; } = "";
        /// <summary>
        /// El apellido del estudiante, se usa para mostrar su informacion, no puede ser nulo ni vacio, se puede usar para mostrar la informacion del estudiante en la aplicacion
        /// </summary>
        public string apellidos { get; set; } = "";
        /// <summary>
        /// Correo electronico del estudiante, se usa para mostrar su informacion.
        /// </summary>
        public string? email { get; set; }
        /// <summary>
        /// Es el numero de telefono del estudiante, se usa para mostrar su informacion, no es obligatorio, se puede usar para mostrar la informacion del estudiante en la aplicacion, es un valor opcional que puede ser nulo o vacio, se puede usar para mostrar la informacion del estudiante en la aplicacion
        /// </summary>
        public string? telefono { get; set; }
        /// <summary>
        /// Di de nacimiento del estudiante, se usa para mostrar su informacion, no es obligatorio, se puede usar para mostrar la informacion del estudiante en la aplicacion, es un valor opcional que puede ser nulo, se puede usar para mostrar la informacion del estudiante en la aplicacion
        /// </summary>
        public DateTime? fecha_nacimiento { get; set; }
        /// <summary>
        /// Identificador laboral de la provincia y nacion
        /// </summary>
        public string? cuil { get; set; }
        /// <summary>
        /// Numero de documento del estudiante, se usa para mostrar su informacion, no es obligatorio, se puede usar para mostrar la informacion del estudiante en la aplicacion, es un valor opcional que puede ser nulo o vacio, se puede usar para mostrar la informacion del estudiante en la aplicacion
        /// </summary>
        public string? dni { get; set; }
        /// <summary>
        /// Es donde se encuentra esta persona viviendo actualmente, se usa para mostrar su informacion, no es obligatorio, se puede usar para mostrar la informacion del estudiante en la aplicacion, es un valor opcional que puede ser nulo o vacio, se puede usar para mostrar la informacion del estudiante en la aplicacion
        /// </summary>
        public string? direccion { get; set; }
        /// <summary>
        /// Constructor vacio necesario para que lo tome el endpoint
        /// </summary>
        public PerfilEstudiante()
        {

        }
        /// <summary>
        /// Constructor completo
        /// </summary>
        /// <param name="legajo"></param>
        /// <param name="nombres"></param>
        /// <param name="apellidos"></param>
        /// <param name="email"></param>
        /// <param name="telefono"></param>
        /// <param name="fecha_nacimiento"></param>
        /// <param name="cuil"></param>
        /// <param name="dni"></param>
        /// <param name="direccion"></param>
        public PerfilEstudiante(string legajo, string nombres, string apellidos, string? email, string? telefono, DateTime? fecha_nacimiento, string? cuil, string? dni, string? direccion)
        {
            this.legajo = legajo;
            this.nombres = nombres;
            this.apellidos = apellidos;
            this.email = email;
            this.telefono = telefono;
            this.fecha_nacimiento = fecha_nacimiento;
            this.cuil = cuil;
            this.dni = dni;
            this.direccion = direccion;
        }
        /// <summary>
        /// Constructor completo
        /// </summary>
        public PerfilEstudiante(DataRow data)
        {  
            this.legajo = data["legajo"].ToString()??"";
            this.nombres = data["nombres"].ToString() ?? ""; ;
            this.apellidos = data["apellidos"].ToString() ?? "";
            this.email = data["email"].ToString();
            this.telefono = data["contacto"].ToString();
            this.fecha_nacimiento = DateTime.TryParse(data["fecha_nacimiento"].ToString(), out DateTime fecha)? fecha : null;
            this.cuil = data["cuil"].ToString();
            this.dni = data["dni"].ToString();
            this.direccion = data["domicilio"].ToString();
        }
    }
}
