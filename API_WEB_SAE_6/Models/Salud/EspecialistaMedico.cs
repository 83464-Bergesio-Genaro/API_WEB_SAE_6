using System.ComponentModel.DataAnnotations;
using System.Data;

namespace API_WEB_SAE_6.Models.Salud
{
    /// <summary>
    /// Representa a un especialista medico, con su informacion personal y su especialidad, se puede usar para mostrar la informacion de los especialistas medicos en la aplicacion
    /// </summary>
    public class EspecialistaMedico
    {
        /// <summary>
        /// El cuil es el identificador unico de cada especialista medico, se usa para buscarlo en la base de datos y para mostrar su informacion, no puede ser nulo ni vacio, es la clave primaria de la tabla especialistas medicos
        /// </summary>
        [Key]
        public string cuil { get; set; } = "";
        /// <summary>
        /// El apellido es el apellido del especialista medico, se usa para mostrar su informacion, no puede ser nulo ni vacio
        /// </summary>
        public string apellido { get; set; } = "";
        /// <summary>
        /// El nombre es el nombre del especialista medico, se usa para mostrar su informacion, no puede ser nulo ni vacio
        /// </summary>
        public string nombre { get; set; } = "";
        /// <summary>
        /// El presta_servicio indica si el especialista medico esta activo o no, se usa para mostrar su informacion, no puede ser nulo, es un valor booleano que indica si el especialista medico esta activo o no
        /// </summary>
        public bool presta_servicio { get; set; } = false;
        /// <summary>
        /// La especialidad es la especialidad del especialista medico, se usa para mostrar su informacion, no puede ser nulo, es un objeto de la clase Especialidad que representa la especialidad del especialista medico
        /// </summary>
        public Especialidad especialidad { get; set; } = new();
        /// <summary>
        /// 
        /// </summary>
        public EspecialistaMedico(){}
        /// <summary>
        /// El constructor con parametros es necesario para poder crear objetos de la clase EspecialistaMedico con los valores que se le pasan por parametro, se puede usar para crear objetos con la informacion completa de un especialista medico, asigna los valores de los atributos con los valores que se le pasan por parametro, no hace nada en particular, solo asigna los valores de los atributos con los valores que se le pasan por parametro
        /// </summary>
        /// <param name="data"></param>
        public EspecialistaMedico(DataRow data)
        {
            cuil = data["cuil"].ToString() ?? "NO DATA";
            nombre = data["nombre"].ToString() ?? "NO DATA";
            apellido = data["apellido"].ToString() ?? "NO DATA";
            presta_servicio = data["activo"].ToString() == "1";
            int id_especialidad = int.Parse(data["id_especialidad"].ToString() ?? "0");
            string especialidad = data["especialidad"].ToString() ?? "NO DATA";
            string descripcion_actividades = data["descripcion_actividades"].ToString() ?? "NO DATA";
            bool especialidad_activa = data["especialidad_activa"].ToString() == "1";

            this.especialidad = new Especialidad(id_especialidad, especialidad, descripcion_actividades, especialidad_activa);
        }
    }
}
