using System.IO;
using API_WEB_SAE_6.Controllers;
using Microsoft.Extensions.Logging;

namespace API_WEB_SAE_6.Logs
{
    /// <summary>
    /// 
    /// </summary>
    public class Logger
    {
        private static string DirectoryPath= Environment.CurrentDirectory + "\\Logs";
        private static string Env = "GENA";
        /// <summary>
        /// 
        /// </summary>
        public Logger() {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public static void DefineDirectorio(IConfiguration config)
        {
            string env = config.GetSection("ConnectionStrings").GetSection("Enviroment").Value?.ToString() ?? "ERROR";
            if (env == "ERROR" || env == "") DirectoryPath = "DATOS NO ENCONTRADOS";

            switch (env)
            {
                case "GENA":
                    Env = "GENA";
                    DirectoryPath = Environment.CurrentDirectory + "\\Logs";
                    break;
                case "JUAN":
                    Env = "JUAN";
                    DirectoryPath = Environment.CurrentDirectory + "\\Logs";
                    break;
                case "DESA":
                    Env = "DESA";
                    DirectoryPath = Environment.CurrentDirectory + "/Logs";
                    break;
                case "PROD":
                    Env = "PROD";
                    DirectoryPath = Environment.CurrentDirectory + "/Logs";
                    break;
                default:
                    Env = "GENA";
                    DirectoryPath = Environment.CurrentDirectory + "\\Logs";
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="error"></param>
        public void RegistrarERROR(Exception ex, string error)
        {
            if (Env == "GENA"|| Env == "JUAN")
            {
                //Prueba en Windows
                DateTime actual = DateTime.Now;
                string fileName = DirectoryPath + "\\Registros\\" + actual.ToString("yyyy-MM-dd") + ".txt";
                //Sino existe crea la carpeta para registrar errores
                if (!Directory.Exists(DirectoryPath + "\\Registros")) Directory.CreateDirectory(DirectoryPath + "\\Registros");

                using (var sw = new StreamWriter(fileName, true))
                {
                    sw.WriteLine(actual + ", ERROR: " + error + ", MESSAGE EXCEPTION: " + ex.Message);
                }
            }
            else
            {
                //Prueba en Linux
                DateTime actual = DateTime.Now;
                string fileName = "/var/log/API_SAE_Error_" + actual.ToString("yyyy-MM-dd") + ".txt";

                using (var sw = new StreamWriter(fileName, true))
                {
                    sw.WriteLine(actual + ", ERROR: " + error + ", MESSAGE EXCEPTION: " + ex.Message);
                }

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="origen"></param>
        /// <param name="comportamiento"></param>
        public void RegistrarAnomalia(string origen, string comportamiento)
        {
            if (Env == "GENA" || Env == "JUAN")
            {
                DateTime actual = DateTime.Now;
                string fileName = DirectoryPath + "\\Alertas\\" + actual.ToString("yyyy-MM-dd") + ".txt";

                using (var sw = new StreamWriter(fileName, true))
                {
                    sw.WriteLine(actual + ", ANOMALIA: " + comportamiento + ", DESDE EL HOST: " + origen);
                }
            }
            else
            {
                //Prueba en Linux
                DateTime actual = DateTime.Now;
                string fileName = "/var/log/API_SAE_Warning_" + actual.ToString("yyyy-MM-dd") + ".txt";

                using (var sw = new StreamWriter(fileName, true))
                {
                    sw.WriteLine(actual + ", ANOMALIA: " + comportamiento + ", DESDE EL HOST: " + origen);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="legajo"></param>
        /// <param name="IP"></param>
        public void RegistrarIP(string legajo, string IP)
        {
            if (Env == "GENA" || Env == "JUAN")
            {
                DateTime actual = DateTime.Now;
                string fileName = DirectoryPath + "\\Sesiones\\" + actual.ToString("yyyy-MM-dd") + ".txt";
                //Sino existe crea la carpeta para registrar errores
                if (!Directory.Exists(DirectoryPath + "\\Sesiones")) Directory.CreateDirectory(DirectoryPath + "\\Sesiones");

                using (var sw = new StreamWriter(fileName, true))
                {
                    sw.WriteLine("El legajo: " + legajo + " realizo la conexion desde: " + IP + " en el horario de: " + actual + " (Hora servidor)");
                }
            }
            else {
                DateTime actual = DateTime.Now;
                string fileName = "/var/log/API_SAE_Session_" + actual.ToString("yyyy-MM-dd") + ".txt";

                using (var sw = new StreamWriter(fileName, true))
                {
                    sw.WriteLine("El legajo: " + legajo + " realizo la conexion desde: " + IP + " en el horario de: " + actual + " (Hora servidor)");
                }
            }

        }
    }
}
