using API_WEB_SAE_6.Logs;
using Newtonsoft.Json;

namespace API_WEB_SAE_6.Tools
{
    /// <summary>
    /// La clase lectora de appsettings.json
    /// </summary>
    public class SettingsReader
    {
        /// <summary>
        /// Es la instancia actual de la configuracion, solo puede existir una a la vez
        /// </summary>
        private static SettingsReader? currentInstance;
        /// <summary>
        /// Que Host permite
        /// </summary>
        public string AllowedHosts { get; set; } = "";
        /// <summary>
        /// Que entorno tenemos
        /// </summary>
        public string Environment { get; set; } = "";
        /// <summary>
        /// La clave que utilizamos para el cifrado
        /// </summary>
        public string SecretKey { get; set; } = "";
        /// <summary>
        /// Para almacenar archivos vamos a utilizar otra direccion para recuperalos
        /// </summary>
        public Dictionary<string, string> FilesLocation { get; set; } = [];
        /// <summary>
        /// Las conexiones para los esquemas basados en MySQL
        /// </summary>
        public Dictionary<string, string> ConexionesMySQL { get; set; } = [];
        /// <summary>
        /// Las conexiones para los esquemas basados en MSSQL
        /// </summary>
        public Dictionary<string, string> ConexionesMSSQL { get; set; } = [];

        /// <summary>
        /// Constructor Vacio
        /// </summary>
        public SettingsReader() { }
        /// <summary>
        /// Al tener varios esquemas distintos lo que hago es tener todas las claves por separado y buscarlas
        /// por nombre en su correspondiente listado.
        /// </summary>
        /// <param name="schema">El tipo de gestor de base de datos que queremos utilizar</param>
        /// <returns>La cadena de conexion para ese esquema</returns>
        public string GetConnectionString(string schema)
        {
            //Al principio quise resolver todo haciendo un diccionario de diccionarios pero JSON no estaba
            //cooperando asi que hice un atributo por cada esquema que es su propio esquema de conexiones.
            string? value;
            switch (schema)
            {
                case "ConexionesMySQL":
                    if (ConexionesMySQL.TryGetValue(Environment, out value)) return value;
                    else return "ERROR";

                case "ConexionesMSSQL":
                    if (ConexionesMSSQL.TryGetValue(Environment, out value)) return value;
                    else return "ERROR";

                default:
                    if (ConexionesMySQL.TryGetValue(Environment, out value)) return value;
                    else return "ERROR";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetFilesLocation()
        {
            //Al principio quise resolver todo haciendo un diccionario de diccionarios pero JSON no estaba
            //cooperando asi que hice un atributo por cada esquema que es su propio esquema de conexiones.
            if (FilesLocation.TryGetValue(Environment, out string? value)) return value;
            else return "ERROR";

        }
        /// <summary>
        /// Metodo estatico que busca el AppSettings
        /// </summary>
        /// <returns>La clase con los datos del appsettings.json</returns>
        public static SettingsReader GetAppSettings()
        {
            try
            {
                if (currentInstance != null) return currentInstance;
                string baseDirectory = System.Environment.CurrentDirectory;
                string separador = "/";
                //Si utiliza la barra invertida es que estamos en Windows
                if (Directory.Exists(baseDirectory) && baseDirectory.Contains(@"\"))
                    separador = "\\";

                string file = baseDirectory + separador + "appsettings.json";

                //Va a leer el archivo y convierte en una cadena
                using StreamReader reader = new(file);
                //Lo lee y convierte en String
                string json = reader.ReadToEnd();
                currentInstance = JsonConvert.DeserializeObject<SettingsReader>(json) ?? new();

                //Prueba si tiene variables de entorno que se utilizan en docker.
                currentInstance.Environment = System.Environment.GetEnvironmentVariable("Environment")?.ToUpper() ?? currentInstance.Environment;

                return currentInstance;
            }
            catch (Exception)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "BUSQUEDA CONEXION", "Error buscando la cadena de conexion", "Sin");
                //Si falla lo crea de manera generica
                return new();
            }

        }
    }
}
