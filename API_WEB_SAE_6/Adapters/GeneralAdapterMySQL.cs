using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Tools;
using MySqlConnector;
using System.Data;

namespace API_WEB_SAE_6.Adapters
{
    /// <summary>
    /// Esta clase copiada en la mayoria de mis proyectos es una forma generica para llamar a las base de
    /// datos basadas en PLSQL. En este caso tuve que hacer algunas modificaciones porque se cuenta con varios esquemas.
    /// </summary>
    /// <remarks>
    /// Constructor generico, la idea es inicializarlo con el esquema que se desea utilizar y de ahi proceder a hacer las consultas
    /// </remarks>
    public class GeneralAdapterMySQL
    {
        /// <summary>
        /// 
        /// </summary>
        public GeneralAdapterMySQL() {}
        /// <summary>
        /// Realiza esta operacion al principio y es para obtener la configuracion una unica vez
        /// con todo lo necesario para llamar a la BD
        /// </summary>
        private static readonly SettingsReader CurrentConfiguration = SettingsReader.GetAppSettings();

        /// <summary>
        /// Metodo muy utilizado permite la utilizacion de un procedimiento almacenado, este codigo originalmente lo
        /// saque de un proyecto de provincia y lo fui mejorando para obtener lo que hay hoy. Estoy abierto a sugerencias
        /// </summary>
        /// <param name="storeProcedure">El nombre del procedimiento a ejecutar</param>
        /// <param name="parameters">Los parametros definidos por tipo y con un valor asignado</param>
        /// <returns>Un data set con tabla/s con los datos o un codigo de error</returns>
        public DataTable ExecuteStoredProcedure(string storeProcedure, List<MySqlParameter>? parameters = null)
        {
            //Lo inicializo como si diera error
            DataTable table = new();

            //Se busca la cadena de conexion dependiendo del esquema
            string connectionString = CurrentConfiguration.GetConnectionString("ConexionesMySQL");
            if (connectionString == "ERROR")
            {
                table.Columns.Add("RESULT");
                table.Rows.Add("ERROR");
                Logger.RegistrarDatos(Logger.LogOptions.Error, "BUSQUEDA CONEXION", "Error buscando la cadena de conexion", "GeneralAdapter");
                return table;
            }

            MySqlConnection connection = new(connectionString);

            try
            {
                using MySqlCommand cmd = new(storeProcedure,connection);
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                    cmd.Parameters.AddRange(parameters.ToArray<MySqlParameter>());
                //Tambien se puede agregar el p_resultado muy comun de algunos proyectos.
                //if (returning)
                //{
                //    cmd.Parameters.Add(new MySqlParameter("p_cursor", MySqlDbType.RefCursor)).Direction = ParameterDirection.Output;
                //}

                //Ejecuta un procedimiento y lee la respuesta
                connection.Open();

                using var adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(table);

                connection.CloseAsync();

            }
            catch (Exception ex)
            {
                //transaction.Rollback();
                Logger.RegistrarDatos(Logger.LogOptions.Error, storeProcedure, ex.Message, "GeneralAdapter");
                table.Columns.Add("RESULT");
                table.Rows.Add("ERROR");
            }
            finally
            {
                //Por las dudas limpio las conexiones siempre pero se puede sacar para no perder el tiempo.
                
                connection?.Dispose();
            }
            return table;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public DataTable ExecuteView(string viewName)
        {
            //Lo inicializo como si diera error
            DataTable table = new();
            //Se busca la cadena de conexion dependiendo del esquema
            string connectionString = CurrentConfiguration.GetConnectionString("ConexionesMySQL");
            if (connectionString == "ERROR")
            {
                table.Columns.Add("RESULT");
                table.Rows.Add("ERROR");
                Logger.RegistrarDatos(Logger.LogOptions.Error, "BUSQUEDA CONEXION", "Error buscando la cadena de conexion", "GeneralAdapter");
                return table;
            }

            MySqlConnection connection = new(connectionString);

            try
            {
                //Consultamos plano a la vista
                using MySqlCommand cmd = new("SELECT * FROM "+ viewName, connection);
                cmd.CommandType = CommandType.Text;


                //Ejecuta un procedimiento y lee la respuesta
                connection.Open();

                using var adapter = new MySqlDataAdapter(cmd);
                adapter.Fill(table);

                connection.CloseAsync();


            }
            catch (Exception ex)
            {
                //transaction.Rollback();
                Logger.RegistrarDatos(Logger.LogOptions.Error, viewName, ex.Message, "GeneralAdapter");
                table.Columns.Add("RESULT");
                table.Rows.Add("ERROR");
            }
            finally
            {
                //Por las dudas limpio las conexiones siempre pero se puede sacar para no perder el tiempo.
                connection?.Dispose();
            }
            return table;
        }
    }
}
