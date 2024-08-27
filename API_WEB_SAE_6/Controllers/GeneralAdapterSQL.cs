
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace API_WEB_SAE_6.Controllers
{
    public class GeneralAdapterSQL
    {
        private static string? ConectionString = null;

        private static void GetConection(IConfiguration config)
        {
            string env = config.GetSection("ConnectionStrings").GetSection("Enviroment").Value?.ToString() ?? "ERROR";

            if (env == "ERROR" || env == "") ConectionString = "DATOS NO ENCONTRADOS";

            switch (env)
            {
                case "GENA":
                    ConectionString = "Data Source=192.168.56.1\\SQLEXPRESS;Initial Catalog=BasePreliminarSAE;User ID=ADM_SAE;Password=M4cKP3aL;Integrated Security=True;Trusted_Connection=true;TrustServerCertificate=True;";
                    break;
                case "JUAN":
                    ConectionString = "Data Source=DESKTOP-7R963Q4\\SQLEXPRESS;Initial Catalog=BasePreliminarSAE;User ID=ADM_SAE;Password=M4cKP3aL;Integrated Security=True;Trusted_Connection=true;TrustServerCertificate=True;";
                    break;
                case "DESA":
                    ConectionString = "Data Source=192.168.56.1\\SQLEXPRESS;Initial Catalog=BasePreliminarSAE;User ID=ADM_SAE;Password=M4cKP3aL;Integrated Security=False;Trusted_Connection=true;TrustServerCertificate=True;";
                    ConectionString = config.GetSection("ConnectionStrings").GetSection("API_WEB_SAEContext").Value?.ToString() ?? "ERROR";
                    break;
                case "PROD":
                    ConectionString = "Data Source=192.168.56.1\\SQLEXPRESS;Initial Catalog=BasePreliminarSAE;User ID=ADM_SAE;Password=M4cKP3aL;Integrated Security=False;Trusted_Connection=true;TrustServerCertificate=True;";
                    ConectionString = config.GetSection("ConnectionStrings").GetSection("API_WEB_SAEContext").Value?.ToString() ?? "ERROR";
                    break;
            }


            ////Microsoft.Extensions.Configuration.ConfigurationManager["AppSettings"]
            //ConectionString = config.GetSection("ConnectionStrings").GetSection("API_WEB_SAEContext").Value?.ToString() ?? "ERROR";
            //if (ConectionString == "ERROR") return;

            //byte[] bytes = System.Text.Encoding.ASCII.GetBytes(ConectionString);
            //string cripting = Encoding.GetEncoding("UTF-8").GetString(bytes);

            //bytes = Encoding.UTF8.GetBytes(cripting);
            //string conection = Encoding.ASCII.GetString(bytes);
            //ConectionString = conection;
        }
        //private static SqlConnection sqlCon = ;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="view"></param>
        /// <returns></returns>
        public static DataTable ExecuteView(IConfiguration config, string view)
        {
            Logs.Logger log = new();
            DataTable data = new();
            data.Columns.Add("MENSAJE");
            if (ConectionString == null || config == null) GetConection(config);
            if (ConectionString == "ERROR")
            {
                log.RegistrarERROR(new("No se pudo encontrar los datos para conectarse a la BD"), "APPSETTINGS ERROR");
                data.Rows.Add("ERROR");
                ConectionString = null;
                return data;
            }
            using var conn = new SqlConnection(ConectionString);
            try
            {
                data = new();
                string com = "SELECT * FROM " + view;
                using var command = new SqlCommand(com, conn) { CommandType = CommandType.Text };
                SqlDataAdapter adapter = new(command);

                conn.Open();
                adapter.Fill(data);
            }
            catch (Exception ex)
            {
                data = new();
                data.Columns.Add("MENSAJE");
                data.Rows.Add("ERROR");
                log.RegistrarERROR(ex, "ERROR EN VISTA: " + view);
            }
            finally
            {
                SqlConnection.ClearAllPools();
                conn.Close();
            }
            return data;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="query"></param>
        ///// <param name="parameters"></param>
        ///// <returns></returns>
        //public static DataTable ExecuteFunction(string query, Dictionary<string, string> parameters)
        //{
        //    DataTable table = new DataTable();
        //    CheckServer();
        //    if (ip == null)
        //    {
        //        MessageBox.Show("No se pudo conectar al servidor");
        //        return table;
        //    }
        //    try
        //    {
        //        using (sqlCon = new SqlConnection(ip))
        //        using (var cmd = new SqlCommand(query, sqlCon))
        //        {
        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            sqlCon.Open();
        //            cmd.CommandType = CommandType.Text;
        //            foreach (var item in parameters) cmd.Parameters.AddWithValue(item.Key, item.Value);
        //            adapter.Fill(table);
        //        }
        //    }
        //    catch (SqlException sql)
        //    {
        //        return table;
        //    }
        //    finally
        //    {
        //        SqlConnection.ClearAllPools();
        //        sqlCon.Close();
        //    }
        //    return table;
        //}
        /// <summary>
        /// Se utiliza para ejecutar un procedimiento almacenado que se encuentre guardado en la base de datos
        /// </summary>
        /// <param name="ProcedureName"> Es el nombre del proceso exactamente igual a como esta escrito en la base de datos </param>
        public static DataTable ExecuteStoredProcedure(IConfiguration config, string ProcedureName, Dictionary<string, string> parameters)
        {
            Logs.Logger log = new();
            DataTable data = new();
            data.Columns.Add("MENSAJE");
            if (ConectionString == null) GetConection(config);
            if (ConectionString == "ERROR")
            {
                log.RegistrarERROR(new("No se pudo encontrar los datos para conectarse a la BD"), "APPSETTINGS ERROR");
                data.Rows.Add("ERROR");
                ConectionString = null;
                return data;
            }
            using var conn = new SqlConnection(ConectionString);
            try
            {
                data = new();
                using var command = new SqlCommand(ProcedureName, conn) { CommandType = CommandType.StoredProcedure };
                SqlDataAdapter adapter = new(command);
                foreach (var item in parameters)
                {
                    if (item.Value == "NULL" || item.Value == null) command.Parameters.AddWithValue(item.Key, DBNull.Value);
                    else command.Parameters.AddWithValue(item.Key, item.Value);
                }
                conn.Open();
                adapter.Fill(data);
            }
            catch (Exception ex)
            {
                data = new();
                //Esto es para que la API pueda devolver un conflict() (409)
                data.Columns.Add("MENSAJE");
                data.Rows.Add("ERROR");
                //Registramos en el Log el error
                log.RegistrarERROR(ex, "ERROR EN PROCEDIMIENTO: " + ProcedureName);
            }
            finally
            {
                SqlConnection.ClearAllPools();
                conn.Close();
            }
            return data;
        }
        /// <summary>
        /// Utilizamos un metodo diferente ya que normalmente usar string es la manera mas facil para evitar conflictos en BD. En este caso no funciona si
        /// mandamos el array de bytes como un string por ende lo deje como object.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="ProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteStoredProcedureDocument(IConfiguration config, string ProcedureName, Dictionary<string, object> parameters)
        {
            Logs.Logger log = new();
            DataTable data = new();
            data.Columns.Add("MENSAJE");
            if (ConectionString == null) GetConection(config);
            if (ConectionString == "ERROR")
            {
                log.RegistrarERROR(new("No se pudo encontrar los datos para conectarse a la BD"), "APPSETTINGS ERROR");
                data.Rows.Add("ERROR");
                ConectionString = null;
                return data;
            }
            using var conn = new SqlConnection(ConectionString);
            try
            {
                data = new();
                using var command = new SqlCommand(ProcedureName, conn) { CommandType = CommandType.StoredProcedure };
                SqlDataAdapter adapter = new(command);
                foreach (var item in parameters)
                {
                    if (item.Value == null || item.Value.ToString() == "NULL") command.Parameters.AddWithValue(item.Key, DBNull.Value);
                    else command.Parameters.AddWithValue(item.Key, item.Value);
                }
                conn.Open();
                adapter.Fill(data);
            }
            catch (Exception ex)
            {
                data = new();
                //Esto es para que la API pueda devolver un conflict() (409)
                data.Columns.Add("MENSAJE");
                data.Rows.Add("ERROR");
                //Registramos en el Log el error
                log.RegistrarERROR(ex, "ERROR EN PROCEDIMIENTO: " + ProcedureName);
            }
            finally
            {
                SqlConnection.ClearAllPools();
                conn.Close();
            }
            return data;
        }

        ///// <summary>
        ///// Se utiliza para encontrar el ultimo valor de un atributo indexado
        ///// </summary>
        ///// <param name="query"> Una sentencia SQL que debe tener el proposito de encontrar el ultimo valor en una tabla. Por ejemplo utilizando la siguiente sentencia: 
        ///// "SELECT MAX(numero_factura) FROM Facturas") nos devolvera el ultimo numero de factura </param>
        ///// <returns> Este ultimo valor (generalmente un numero) como un string </returns>
        //public static string OnlyValue(string query)
        //{
        //    CheckServer();
        //    if (ip == null)
        //    {
        //        MessageBox.Show("No se pudo conectar al servidor");
        //        return "NULL";
        //    }
        //    using (var conn = new SqlConnection(ip))
        //    using (var cmd = new SqlCommand(query, conn))
        //    {
        //        try
        //        {
        //            conn.Open();
        //            string? x = cmd.ExecuteScalar().ToString();
        //            if (x != null) return x;
        //            else return "NULL";
        //        }
        //        catch (Exception)
        //        {
        //            return "NULL";
        //        }
        //        finally { conn.Close(); }

        //    }
        //}
        ///// <summary>
        ///// Se utiliza para encontrar el ultimo valor de un atributo indexado utilizando variables
        ///// </summary>
        ///// <param name="query">  Una sentencia SQL que debe tener el proposito de encontrar el ultimo valor en una tabla. Por ejemplo utilizando la siguiente sentencia: 
        ///// "SELECT MAX(numero_factura) FROM Facturas") nos devolvera el ultimo numero de factura </param>
        ///// <param name="parameters"> Es una dupla de parametros que utiliza "@variable" (Escribiendose igual que en la sentencia SQL) , string variable
        ///// con el formato: new Dictionary{{"@variable", variable},{"@variable2,variable2"} (...) {"@variableN", variableN} } </param>
        ///// <returns> Este ultimo valor (generalmente un numero) como un entero  </returns>
        //public static int OnlyValueInt(string query, Dictionary<string, string> parameters)
        //{
        //    CheckServer();
        //    if (ip == null)
        //    {
        //        MessageBox.Show("No se pudo conectar al servidor");
        //        return 0;
        //    }
        //    using (var conn = new SqlConnection(ip))
        //    using (var cmd = new SqlCommand(query, conn))
        //    {
        //        try
        //        {
        //            conn.Open();
        //            foreach (var item in parameters) cmd.Parameters.AddWithValue(item.Key, item.Value);

        //            string? x = cmd.ExecuteScalar().ToString();

        //            if (x != null) return int.Parse(x);
        //            else return 0;
        //        }
        //        catch (Exception)
        //        {
        //            return 0;
        //        }
        //        finally { conn.Close(); }

        //    }
        //}
        ///// <summary>
        ///// Solo se recomienda su uso si se desea hacer una transaccion con varias ejecuciones dentro
        ///// </summary>
        //public class Executor
        //{
        //    public string Query { get; set; }
        //    public Dictionary<string, string> Dic { get; set; }

        //    public Executor(string query, Dictionary<string, string> dic)
        //    {
        //        this.Dic = dic;
        //        this.Query = query;
        //    }
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="exeList"></param>
        //public static bool ExecuteTransaction(List<Executor> exeList)
        //{
        //    bool ack = false;
        //    CheckServer();
        //    if (ip == null)
        //    {
        //        MessageBox.Show("No se pudo conectar al servidor");
        //        return ack;
        //    }
        //    using (SqlConnection connection = new SqlConnection(ip))
        //    {
        //        connection.Open();

        //        SqlCommand command = connection.CreateCommand();
        //        SqlTransaction transaction;
        //        transaction = connection.BeginTransaction("ContractTransaction");
        //        command.Connection = connection;
        //        command.Transaction = transaction;
        //        command.CommandType = CommandType.StoredProcedure;
        //        try
        //        {
        //            foreach (Executor? exe in exeList)
        //            {
        //                command.CommandText = exe.Query;
        //                command.Parameters.Clear();
        //                foreach (var item in exe.Dic)
        //                {
        //                    if (item.Value == "NULL") command.Parameters.AddWithValue(item.Key, DBNull.Value);
        //                    else command.Parameters.AddWithValue(item.Key, item.Value);
        //                }
        //                command.ExecuteNonQuery();
        //            }
        //            // Attempt to commit the transaction.
        //            transaction.Commit();
        //            ack = true;
        //        }
        //        catch (Exception)
        //        {

        //            // Attempt to roll back the transaction.
        //            try
        //            {
        //                transaction.Rollback();
        //                ack = false;
        //            }
        //            catch (Exception ex2)
        //            {
        //                MessageBox.Show("Error al realizar el rollback, intente nuevamente", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
        //            }
        //        }
        //        finally
        //        {
        //            connection.Close();
        //        }
        //    }
        //    return ack;
        //}
        //public static bool isRoela()
        //{
        //    if (ip == ipRoela) return true;
        //    else return false;
        //}
    }
}
