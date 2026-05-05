using System.IO;
using API_WEB_SAE_6.Controllers;
using Microsoft.Extensions.Logging;

namespace API_WEB_SAE_6.Logs
{
    /// <summary>
    /// Esta clase esta creada con el proposito de almacenar mensajes en formato .txt dentro de la carpeta Logs
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Es el directorio para los registros de errores
        /// </summary>
        private static string ErrorPath = Environment.CurrentDirectory;
        /// <summary>
        /// Es el directorio para las alertas del sistema
        /// </summary>
        private static string WarningsPath = Environment.CurrentDirectory;
        /// <summary>
        /// Es el directorio para las IP que se registran
        /// </summary>
        private static string SessionsPath = Environment.CurrentDirectory;
        /// <summary>
        /// Es el directorio para las acciones
        /// </summary>
        private static string InfoPath = Environment.CurrentDirectory;
        /// <summary>
        /// 
        /// </summary>
        public enum LogOptions
        {
            /// <summary>
            /// Para cuando queremos registrar una excepcion no contemplada
            /// </summary>
            Error,
            /// <summary>
            /// Cuando queremos registrar una accion del usuario
            /// </summary>
            Info,
            /// <summary>
            /// Cuando registramos actividad sospechosa o fuera de lo normal
            /// </summary>
            Alerta,
            /// <summary>
            /// Cuando registramos quien envia la informacion
            /// </summary>
            IP
        };

        /// <summary>
        /// Constructor vacio.
        /// </summary>
        public Logger() { }
        /// <summary>
        /// Con esta funcion definimos adonde vamos a estar realizando los registros de errores, advertencas y sesiones.
        /// </summary>
        public static void DefinirDirectorios()
        {
            string baseDirectory = Environment.CurrentDirectory;
            string separador = "/";
            //Si utiliza la barra invertida es que estamos en Windows
            if (Directory.Exists(baseDirectory) && baseDirectory.Contains(@"\"))
                separador = "\\";
            //En teoria el separador se define por el sistema operativo, curiosamente el path combine hace que se rompa por los permisos.
            ErrorPath = baseDirectory + separador + "Logs" + separador + "Registros" + separador;
            WarningsPath = baseDirectory + separador + "Logs" + separador + "Alertas" + separador;
            SessionsPath = baseDirectory + separador + "Logs" + separador + "Sesiones" + separador;
            InfoPath = baseDirectory + separador + "Logs" + separador + "Info" + separador;
        }


        /// <summary>
        /// Esta funcion es una simplificacion del registro de todo tipo de opciones. La opcion seleccionada determina donde lo vamos a guardar.
        /// </summary>
        /// <param name="option">Opcion que vamos a utilizar Ej: Error. </param>
        /// <param name="function">Endpoint o funcion donde se inicia el registro</param>
        /// <param name="description">La descripcion del evento</param>
        /// <param name="controller">El controlador donde ocurre dicho evento</param>
        public static void RegistrarDatos(LogOptions option, string function, string description, string controller)
        {
            string fileName = option switch
            {
                LogOptions.Info => InfoPath,
                LogOptions.Error => ErrorPath,
                LogOptions.Alerta => WarningsPath,
                LogOptions.IP => SessionsPath,
                _ => InfoPath,
            };
            //Sino existe, crea la carpeta para registrar
            if (!Directory.Exists(fileName)) Directory.CreateDirectory(fileName);
            //Ahora al nombre del archivo le agrega el dia de la fecha
            fileName += DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            using var sw = new StreamWriter(fileName, true);
            sw.WriteLine("Funcion: " + function
                + " descripcion: " + description
                + " controlador: " + controller);

        }
    }
}
