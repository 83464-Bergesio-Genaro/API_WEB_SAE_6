using System.Data;

namespace API_WEB_SAE_6.Models
{
    /// <summary>
    /// Representa a un horario de salud, con su informacion basica, se puede usar para mostrar la informacion de los horarios de salud en la aplicacion
    /// </summary>
    public class HorariosSalud
    {
        /// <summary>
        /// El id es el identificador unico de cada horario de salud, se usa para buscarlo en la base de datos y para mostrar su informacion, no puede ser nulo ni vacio, es la clave primaria de la tabla horarios de salud
        /// </summary>
        public int id { get; set; } = -1;
        /// <summary>
        /// La hora_inicio es la hora de inicio del horario de salud, se usa para mostrar su informacion, no puede ser nulo ni vacio, es un texto que representa la hora de inicio del horario de salud en formato HH:mm:ss
        /// </summary>
        public string hora_inicio { get; set; } = "";
        /// <summary>
        /// La hora_fin es la hora de fin del horario de salud, se usa para mostrar su informacion, no puede ser nulo ni vacio, es un texto que representa la hora de fin del horario de salud en formato HH:mm:ss
        /// </summary>
        public string hora_fin { get; set; } = "";
        /// <summary>
        /// El dia es el dia del horario de salud, se usa para mostrar su informacion, no puede ser nulo ni vacio, es un valor entero que representa el dia de la semana del horario de salud, donde 1 es lunes, 2 es martes, 3 es miercoles, 4 es jueves, 5 es viernes, 6 es sabado y 7 es domingo
        /// </summary>
        public int dia { get; set; } = -1;
        /// <summary>
        /// El cuil_especialista es el cuil del especialista medico que tiene ese horario de salud, se usa para mostrar su informacion, no puede ser nulo ni vacio, es un texto que representa el cuil del especialista medico que tiene ese horario de salud, es una clave foranea que hace referencia al cuil del especialista medico en la tabla especialistas medicos
        /// </summary>
        public string cuil_especialista { get; set; } = "";
        /// <summary>
        /// El especialista es el nombre del especialista medico que tiene ese horario de salud, se usa para mostrar su informacion, no puede ser nulo ni vacio, es un texto que representa el nombre del especialista medico que tiene ese horario de salud, se obtiene a partir del cuil del especialista medico en la tabla especialistas medicos
        /// </summary>
        public string especialista { get; set; } = "";
        /// <summary>
        /// El activo indica si el horario de salud esta activo o no, se usa para mostrar su informacion, no puede ser nulo, es un valor booleano que indica si el horario de salud esta activo o no, se obtiene a partir del valor activo del especialista medico en la tabla especialistas medicos, si el especialista medico esta activo entonces el horario de salud esta activo, si el especialista medico no esta activo entonces el horario de salud no esta activo
        /// </summary>
        public bool activo { get; set; } = false;
        /// <summary>
        /// La especialidad es la especialidad medica que tiene ese horario de salud, se usa para mostrar su informacion, no puede ser nulo, es un objeto de la clase Especialidad que representa la especialidad medica que tiene ese horario de salud, se obtiene a partir del id_especialidad del especialista medico en la tabla especialistas medicos, es una clave foranea que hace referencia al id de la especialidad medica en la tabla especialidades medicas
        /// </summary>
        public Especialidad especialidad { get; set; } = new();
        /// <summary>
        /// 
        /// </summary>
        public HorariosSalud() {}
        /// <summary>
        /// El constructor con parametros es necesario para poder crear objetos de la clase HorariosSalud con los valores que se le pasan por parametro, se puede usar para crear objetos con la informacion completa de un horario de salud, asigna los valores de los atributos con los valores que se le pasan por parametro, no hace nada en particular, solo asigna los valores de los atributos con los valores que se le pasan por parametro
        /// </summary>
        /// <param name="data"></param>
        public HorariosSalud(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            hora_inicio = data["hora_inicio"].ToString() ?? "NO DATA";
            hora_fin = data["hora_fin"].ToString() ?? "NO DATA";
            dia = int.Parse(data["dia"].ToString() ?? "0");
            cuil_especialista = data["cuil_especialista"].ToString() ?? "NO DATA";
            especialista = data["especialista"].ToString() ?? "NO DATA";
            activo = (data["activo"].ToString() == "1");
            int id_especialidad = int.Parse(data["id_especialidad"].ToString() ?? "0");
            string especialidad = data["especialidad"].ToString() ?? "NO DATA";
            string descripcion_actividades = data["descripcion_actividades"].ToString() ?? "NO DATA";
            bool especialidad_activa = (data["especialidad_activa"].ToString() == "1");

            this.especialidad = new Especialidad(id_especialidad, especialidad, descripcion_actividades, especialidad_activa);
        }
    }
}
