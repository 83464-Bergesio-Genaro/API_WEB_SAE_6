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
        public string Enviroment { get; set; } = "";
        /// <summary>
        /// La clave que utilizamos para el cifrado
        /// </summary>
        public string SecretKey { get; set; } = "";
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
                    if (ConexionesMySQL.TryGetValue(Enviroment, out value)) return value;
                    else return "ERROR";

                case "ConexionesMSSQL":
                    if (ConexionesMSSQL.TryGetValue(Enviroment, out value)) return value;
                    else return "ERROR";

                default:
                    if (ConexionesMySQL.TryGetValue(Enviroment, out value)) return value;
                    else return "ERROR";
            }

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
                string baseDirectory = Environment.CurrentDirectory;
                string separador = "/";
                //Si utiliza la barra invertida es que estamos en Windows
                if (Directory.Exists(baseDirectory) && baseDirectory.Contains(@"\"))
                    separador = "\\";

                string file = baseDirectory + separador + "appsettings.json";

                //Va a leer el archivo y convierte en una cadena
                using StreamReader reader = new(file);
                //Lo lee y convierte en String
                string json = reader.ReadToEnd();
                //Prueba convirtiendolo a la clase que creamos
                return currentInstance = JsonConvert.DeserializeObject<SettingsReader>(json) ?? new();
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
