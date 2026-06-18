using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models;
using MySqlConnector;
using System.Data;

namespace API_WEB_SAE_6.Adapters
{
    /// <summary>
    /// Clase que se encarga de adaptar la informacion de los empleados a la base de datos que se esta utilizando, en este caso MySQL, pero se puede cambiar a SQL Server o cualquier otra base de datos relacional.
    /// </summary>
    /// <param name="motorDB"></param>
    public class EmpleadoAdapter(string motorDB = "MySQL")
    {
        /// <summary>
        /// Define que tipo de base de datos se usa para consumir la informacion
        /// </summary>
        public string MotorDB { get; set; } = motorDB;
        /// <summary>
        /// Metodo que se encarga de obtener la informacion de los empleados de la base de datos, este metodo se utiliza para mostrar la informacion de los empleados en la API, no se utiliza para guardar la informacion, ya que para eso se utiliza la clase EmpleadoAdapter, esta clase es solo para mostrar la informacion de los empleados en la API.
        /// </summary>
        /// <returns></returns>
        public List<EmpleadosSAE>? ObtenerEmpleadosCompleto()
        {
            string method = "ObtenerEmpleadosCompleto";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_EMPLEADOS_Listar_empleados_completo");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<EmpleadosSAE> listadoEmplea = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        EmpleadosSAE emple = new(row);
                        listadoEmplea.Add(emple);
                    }
                    return listadoEmplea;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "EmpleadoAdapter");
                return null;
            }
        }
        /// <summary>
        /// Metodo que se encarga de obtener la informacion de los empleados activos de la base de datos, este metodo se utiliza para mostrar la informacion de los empleados activos en la API, no se utiliza para guardar la informacion, ya que para eso se utiliza la clase EmpleadoAdapter, esta clase es solo para mostrar la informacion de los empleados activos en la API.
        /// </summary>
        /// <returns></returns>
        public List<EmpleadosSAE>? ObtenerEmpleadosActivo()
        {
            string method = "ObtenerEmpleadosActivo";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_EMPLEADOS_Listar_empleados_activos");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<EmpleadosSAE> listadoEmplea = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        EmpleadosSAE emple = new(row);
                        listadoEmplea.Add(emple);
                    }
                    return listadoEmplea;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "EmpleadoAdapter");
                return null;
            }
        }
        /// <summary>
        /// Metodo que se encarga de obtener la informacion de un empleado en particular de la base de datos, este metodo se utiliza para mostrar la informacion de un empleado en particular en la API, no se utiliza para guardar la informacion, ya que para eso se utiliza la clase EmpleadoAdapter, esta clase es solo para mostrar la informacion de un empleado en particular en la API.
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <returns></returns>
        public EmpleadosSAE? BuscarEmpleadoXId(int idEmpleado)
        {
            string method = "BuscarEmpleadoXId";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_empleado", MySqlDbType.Int32) { Value = idEmpleado }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_EMPLEADOS_Buscar_Empleados_id", parameters);

                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new EmpleadosSAE(respuesta.Rows[0]);
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "EmpleadoAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="legajo"></param>
        /// <returns></returns>
        public EmpleadosSAE? BuscarEmpleadoXLegajo(string legajo)
        {
            string method = "BuscarEmpleadoXLegajo";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_legajo", MySqlDbType.VarChar) { Value = legajo }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_EMPLEADOS_Buscar_Empleados_legajo", parameters);

                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new EmpleadosSAE(respuesta.Rows[0]);
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "EmpleadoAdapter");
                return null;
            }
        }
        /// <summary>
        /// Metodo que se encarga de modificar la informacion de un empleado en particular de la base de datos, este metodo se utiliza para modificar la informacion de un empleado en particular en la API, no se utiliza para guardar la informacion, ya que para eso se utiliza la clase EmpleadoAdapter, esta clase es solo para mostrar la informacion de un empleado en particular en la API.
        /// </summary>
        /// <param name="emplea"></param>
        /// <param name="usuarioActual"></param>
        /// <returns></returns>
        public EmpleadosSAE ModificarEmpleado(EmpleadosSAE emplea, int usuarioActual)
        {
            string method = "ModificarEmpleado";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_id_empleado", MySqlDbType.Int32) { Value = emplea.id },
                        new("i_legajo", MySqlDbType.VarChar) { Value = emplea.legajo },
                        new("i_activo", MySqlDbType.Bit) { Value = emplea.activo },
                        new("i_id_usuario_mod", MySqlDbType.Int32) { Value = usuarioActual},];
                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_EMPLEADOS_Modificar_Empleado", parameters);
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                else return new();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "EmpleadoAdapter");
                return new();
            }
        }
        /// <summary>
        /// Metodo que se encarga de crear un nuevo empleado en la base de datos, este metodo se utiliza para crear un nuevo empleado en la API, no se utiliza para mostrar la informacion, ya que para eso se utiliza la clase EmpleadoAdapter, esta clase es solo para mostrar la informacion de los empleados en la API.
        /// </summary>
        /// <param name="emplead"></param>
        /// <param name="apellidos"></param>
        /// <param name="nombres"></param>
        /// <param name="idCreacion"></param>
        /// <returns></returns>
        public Usuarios CrearEmpleado(Usuarios emplead,string nombres,string apellidos, int idCreacion)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_legajo", MySqlDbType.VarChar) { Value = emplead.legajo},
                        new("i_nombres", MySqlDbType.VarChar) { Value = nombres},
                        new("i_apellidos", MySqlDbType.VarChar) { Value = apellidos},
                        new("i_id_perfil", MySqlDbType.Int32) { Value = emplead.id_perfil},
                        new("i_id_usuario_alta", MySqlDbType.Int32) { Value = idCreacion}
                        ];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_EMPLEADOS_Crear_Empleado", parameters);

                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearEmpleado", ex.Message, "EmpleadoAdapter");
                    return new();
                }
            }
            else return new();
        }
        /// <summary>
        /// Metodo que se encarga de obtener la informacion de los horarios de los empleados de la base de datos, este metodo se utiliza para mostrar la informacion de los horarios de los empleados en la API, no se utiliza para guardar la informacion, ya que para eso se utiliza la clase EmpleadoAdapter, esta clase es solo para mostrar la informacion de los horarios de los empleados en la API.
        /// </summary>
        /// <returns></returns>
        public List<HorariosSAE>? ObtenerHorarioCompleto()
        {
            string method = "ObtenerHorarioCompleto";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_EMPLEADOS_Listar_horarios_completo");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<HorariosSAE> listaHorario = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        HorariosSAE hora = new(row);
                        listaHorario.Add(hora);
                    }
                    return listaHorario;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "EmpleadoAdapter");
                return null;
            }
        }
        /// <summary>
        /// Metodo que se encarga de obtener la informacion de los horarios de los empleados de la base de datos, este metodo se utiliza para mostrar la informacion de los horarios de los empleados en la API, no se utiliza para guardar la informacion, ya que para eso se utiliza la clase EmpleadoAdapter, esta clase es solo para mostrar la informacion de los horarios de los empleados en la API.
        /// </summary>
        /// <returns></returns>
        public List<HorariosSAE>? ObtenerHorarioActivo()
        {
            string method = "ObtenerHorarioActivo";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_EMPLEADOS_Listar_horarios_activos");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<HorariosSAE> listaHorario = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        HorariosSAE hora = new(row);
                        listaHorario.Add(hora);
                    }
                    return listaHorario;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "EmpleadoAdapter");
                return null;
            }
        }
        /// <summary>
        /// Metodo que se encarga de obtener la informacion de un horario en particular de la base de datos, este metodo se utiliza para mostrar la informacion de un horario en particular en la API, no se utiliza para guardar la informacion, ya que para eso se utiliza la clase EmpleadoAdapter, esta clase es solo para mostrar la informacion de un horario en particular en la API.
        /// </summary>
        /// <param name="idHorario"></param>
        /// <returns></returns>
        public HorariosSAE? BuscarHorarioXId(int idHorario)
        {
            string method = "BuscarHorarioXId";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_horario", MySqlDbType.Int32) { Value = idHorario }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_EMPLEADOS_Buscar_Horario_ID", parameters);

                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new HorariosSAE(respuesta.Rows[0]);
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "EmpleadoAdapter");
                return null;
            }
        }
        /// <summary>
        /// Metodo que se encarga de obtener la informacion de un horario en particular de la base de datos, este metodo se utiliza para mostrar la informacion de un horario en particular en la API, no se utiliza para guardar la informacion, ya que para eso se utiliza la clase EmpleadoAdapter, esta clase es solo para mostrar la informacion de un horario en particular en la API.
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <returns></returns>
        public List<HorariosSAE>? BuscarHorarioXEmpleado(int idEmpleado)
        {
            string method = "BuscarHorarioXEmpleado";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_empleado", MySqlDbType.Int32) { Value = idEmpleado }];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_EMPLEADOS_Buscar_Horario_Empleado", parameters);

                    List<HorariosSAE> listaHorario = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        HorariosSAE hora = new(row);
                        listaHorario.Add(hora);
                    }
                    return listaHorario;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "EmpleadoAdapter");
                return null;
            }
        }
        /// <summary>
        /// Metodo que se encarga de modificar la informacion de un horario en particular de la base de datos, este metodo se utiliza para modificar la informacion de un horario en particular en la API, no se utiliza para guardar la informacion, ya que para eso se utiliza la clase EmpleadoAdapter, esta clase es solo para mostrar la informacion de un horario en particular en la API.
        /// </summary>
        /// <param name="horario"></param>
        /// <param name="usuarioActual"></param>
        /// <returns></returns>
        public HorariosSAE ModificarHorario(HorariosSAE horario, int usuarioActual)
        {
            string method = "ModificarHorario";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_id_horario", MySqlDbType.Int32) { Value = horario.id },
                        new("i_hora_inicio", MySqlDbType.Date) { Value = horario.hora_inicio },
                        new("i_hora_fin", MySqlDbType.Date) { Value = horario.hora_fin },
                        new("i_dia", MySqlDbType.Int32) { Value = horario.dia },
                        new("i_id_empleado", MySqlDbType.Int32) { Value = horario.id_empleado },
                        new("i_id_usuario_mod", MySqlDbType.Int32) { Value = usuarioActual},];
                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_EMPLEADOS_Modificar_Horario", parameters);
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                else return new();
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "EmpleadoAdapter");
                return new();
            }
        }
        /// <summary>
        /// Metodo que se encarga de crear un nuevo horario en la base de datos, este metodo se utiliza para crear un nuevo horario en la API, no se utiliza para mostrar la informacion, ya que para eso se utiliza la clase EmpleadoAdapter, esta clase es solo para mostrar la informacion de los horarios de los empleados en la API.
        /// </summary>
        /// <param name="horario"></param>
        /// <param name="idCreacion"></param>
        /// <returns></returns>
        public HorariosSAE CrearHorario(HorariosSAE horario, int idCreacion)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_hora_inicio", MySqlDbType.Date) { Value = horario.hora_inicio },
                        new("i_hora_fin", MySqlDbType.Date) { Value = horario.hora_fin },
                        new("i_dia", MySqlDbType.Int32) { Value = horario.dia },
                        new("i_id_empleado", MySqlDbType.Int32) { Value = horario.id_empleado },
                        new("i_id_usuario_alta", MySqlDbType.Int32) { Value = idCreacion},];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_EMPLEADOS_Crear_Horario", parameters);

                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearHorario", ex.Message, "EmpleadoAdapter");
                    return new();
                }
            }
            else return new();
        }
        /// <summary>
        /// Metodo que se encarga de eliminar un horario en particular de la base de datos, este metodo se utiliza para eliminar un horario en particular en la API, no se utiliza para mostrar la informacion, ya que para eso se utiliza la clase EmpleadoAdapter, esta clase es solo para mostrar la informacion de los horarios de los empleados en la API.
        /// </summary>
        /// <param name="idHorario"></param>
        /// <returns></returns>
        public bool EliminarHorario(int idHorario)
        {
            List<MySqlParameter> parameters = [new("i_id_horario", MySqlDbType.Int32) { Value = idHorario }];

            GeneralAdapterMySQL consult = new();
            DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_EMPLEADOS_Eliminar_Horario", parameters);

            if (respuesta.Rows.Count > 0) return false;
            else return true;
        }
        /// <summary>
        /// Metodo que se encarga de obtener la informacion de los linktree de los empleados de la base de datos, este metodo se utiliza para mostrar la informacion de los linktree de los empleados en la API, no se utiliza para guardar la informacion, ya que para eso se utiliza la clase EmpleadoAdapter, esta clase es solo para mostrar la informacion de los linktree de los empleados en la API.
        /// </summary>
        /// <returns></returns>
        public List<ItemLinktree>? ObtenerLinktree()
        {
            string method = "ObtenerLinktree";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_EMPLEADOS_Listar_Linktree");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    List<ItemLinktree> listadoLinkTree = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        ItemLinktree ite = new(row);
                        listadoLinkTree.Add(ite);
                    }
                    return listadoLinkTree;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "EmpleadoAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ContarVisualizaconLink(int idLinktree)
        {
            string method = "ContarVisualizaconLink";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    List<MySqlParameter> parameters = [new("i_id_item_linktree", MySqlDbType.Int32) { Value = idLinktree }];

                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_EMPLEADOS_Visualizacion_Item_Linktree",parameters);
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR" || respuesta.Rows[0][0].ToString() != "VISUALIZADO") return false;
                    else return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "EmpleadoAdapter");
                return false;
            }
        }
        /// <summary>
        /// Metodo que se encarga de crear un nuevo item linktree en la base de datos, este metodo se utiliza para crear un nuevo item linktree en la API, no se utiliza para mostrar la informacion, ya que para eso se utiliza la clase EmpleadoAdapter, esta clase es solo para mostrar la informacion de los linktree de los empleados en la API.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public ItemLinktree CrearItemLinkTree(ItemLinktree item)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_titulo", MySqlDbType.VarChar) { Value = item.titulo },
                        new("i_id_index_ico", MySqlDbType.Int32) { Value = item.id_index_ico },
                        new("i_url", MySqlDbType.VarChar) { Value = item.hipervinculo }];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_EMPLEADOS_Crear_Item_Linktree", parameters);

                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearItem", ex.Message, "EmpleadoAdapter");
                    return new();
                }
            }
            else return new();
        }
        /// <summary>
        /// Metodo que se encarga de eliminar un item linktree en particular de la base de datos, este metodo se utiliza para eliminar un item linktree en particular en la API, no se utiliza para mostrar la informacion, ya que para eso se utiliza la clase EmpleadoAdapter, esta clase es solo para mostrar la informacion de los linktree de los empleados en la API.
        /// </summary>
        /// <param name="linktree"></param>
        /// <returns></returns>
        public bool EliminarLinkTree(int linktree)
        {
            List<MySqlParameter> parameters = [new("i_id_item_linktree", MySqlDbType.Int32) { Value = linktree }];

            GeneralAdapterMySQL consult = new();
            DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_EMPLEADOS_Eliminar_Item_Linktree", parameters);

            if (respuesta.Rows.Count > 0) return false;
            else return true;
        }
    }
}
