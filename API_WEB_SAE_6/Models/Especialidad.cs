namespace API_WEB_SAE_6.Models
{
    /// <summary>
    /// Representa a una especialidad medica, con su informacion basica, se puede usar para mostrar la informacion de las especialidades medicas en la aplicacion
    /// </summary>
    public class Especialidad
    {
        /// <summary>
        /// El id es el identificador unico de cada especialidad medica, se usa para buscarla en la base de datos y para mostrar su informacion, no puede ser nulo ni vacio, es la clave primaria de la tabla especialidades medicas
        /// </summary>
        public int id { get; set; } = -1;
        /// <summary>
        /// El nombre es el nombre de la especialidad medica, se usa para mostrar su informacion, no puede ser nulo ni vacio
        /// </summary>
        public string nombre { get; set; } = "";
        /// <summary>
        /// La descripcion es la descripcion de la especialidad medica, se usa para mostrar su informacion, no puede ser nulo ni vacio, es un texto que describe las actividades que se realizan en esa especialidad medica
        /// </summary>
        public string descripcion { get; set; } = "";
        /// <summary>
        /// El activo indica si la especialidad medica esta activa o no, se usa para mostrar su informacion, no puede ser nulo, es un valor booleano que indica si la especialidad medica esta activa o no
        /// </summary>
        public bool activo { get; set; } = false;

        /// <summary>
        /// El constructor por defecto es necesario para poder crear objetos de la clase Especialidad sin necesidad de pasarle parametros, se puede usar para crear objetos vacios o para asignar los valores posteriormente, no hace nada en particular, solo inicializa los atributos con sus valores por defecto
        /// </summary>
        public Especialidad()
        {

        }
        /// <summary>
        /// El constructor con parametros es necesario para poder crear objetos de la clase Especialidad con los valores que se le pasan por parametro, se puede usar para crear objetos con la informacion completa de una especialidad medica, asigna los valores de los atributos con los valores que se le pasan por parametro, no hace nada en particular, solo asigna los valores de los atributos con los valores que se le pasan por parametro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="descripcion"></param>
        /// <param name="estaActivo"></param>
        public Especialidad(int id, string nombre, string descripcion, bool estaActivo)
        {
            this.id = id;
            this.nombre = nombre;
            this.descripcion = descripcion;
            activo = estaActivo;
        }
    }
}
