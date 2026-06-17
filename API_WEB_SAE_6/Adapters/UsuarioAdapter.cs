using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models.Usuario;
using API_WEB_SAE_6.Tools;
using MySqlConnector;
using System.Data;
using System.Security.Claims;

namespace API_WEB_SAE_6.Adapters
{
    /// <summary>
    /// 
    /// </summary>
    public class UsuarioAdapter(string motorDB = "MySQL")
    {
        /// <summary>
        /// Define que tipo de base de datos se usa para consumir la informacion
        /// </summary>
        public string MotorDB { get; set; } = motorDB;
        private List<FuncionesXPerfiles> permisosRegistrados = [];

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Usuarios>? ObtenerUsuariosCompleto()
        {
            if(MotorDB == "MySQL")
            {
                try
                {
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteView("MODULO_USUARIOS_Vista_Usuarios");
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    
                    List<Usuarios> usuarios = [];
                    foreach (DataRow row in respuesta.Rows)
                    {
                        Usuarios user = new(row);
                        usuarios.Add(user);
                    }
                    return usuarios;
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "ObtenerUsuariosCompleto", ex.Message, "UsuarioAdapter");
                    return null;
                }

            }
            else return null;
        }
        /// <summary>
        /// Es un endpoint oculto que sirve para validar la existencia de ese legajo en nuestra BD
        /// </summary>
        /// <param name="legajo">Legajo activo en la base y la aplicacion</param>
        /// <returns>Un usuario existente en nuestra base o error</returns>
        public Usuarios? BuscarUsuarioActivoXLegajo(string legajo)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_legajo", MySqlDbType.VarChar) { Value = legajo }];

                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_USUARIOS_Buscar_Usuario_Activo_Legajo", parameters);
                    if (respuesta.Rows.Count == 0 ) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "BuscarUsuarioActivoXLegajo", ex.Message, "UsuarioAdapter");
                    return null;
                }
            }
            else return new();
        }
        /// <summary>
        /// Es un endpoint oculto que sirve para validar la existencia de ese legajo en nuestra BD
        /// </summary>
        /// <param name="id">ID del usuario en la base de datos</param>
        /// <returns>Un usuario existente en nuestra base o error</returns>
        public Usuarios? BuscarUsuarioXID(int id)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_id_usuario", MySqlDbType.Int32) { Value = id }];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_USUARIOS_Buscar_Usuarios_Id", parameters);
                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "BuscarUsuarioXID", ex.Message, "UsuarioAdapter");
                    return null;
                }
            }
            else return new();
        }
        /// <summary>
        /// Es un endpoint oculto que sirve para validar la existencia de ese legajo en nuestra BD
        /// </summary>
        /// <param name="legajo">ID del usuario en la base de datos</param>
        /// <returns>Un usuario existente en nuestra base o error</returns>
        public Usuarios? BuscarUsuarioXLegajo(string legajo)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [new("i_legajo", MySqlDbType.VarChar) { Value = legajo }];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_USUARIOS_Buscar_Usuarios_Legajo", parameters);
                    if (respuesta.Rows.Count == 0) return new();
                    if (respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "BuscarUsuarioXLegajo", ex.Message, "UsuarioAdapter");
                    return new();
                }
            }
            else return new();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="idUserMod"></param>
        /// <returns></returns>
        public Usuarios ModificarUsuario(Usuarios user,int idUserMod)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_id_usuario", MySqlDbType.Int32) { Value = user.id },
                        new("i_legajo", MySqlDbType.VarChar) { Value = user.legajo },
                        new("i_nombre_usuario", MySqlDbType.VarChar) { Value = user.nombre_usuario },
                        new("i_id_perfil", MySqlDbType.Int32) { Value = user.id_perfil },
                        new("i_activo", MySqlDbType.Bit) { Value = user.activo },
                        new("i_id_usuario_mod", MySqlDbType.Int32) { Value = idUserMod }
                        ];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_USUARIOS_Modificar_Usuario", parameters);
                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "ModificarUsuario", ex.Message, "UsuarioAdapter");
                    return new();
                }

            }
            else return new();
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="idUserCreacion"></param>
        /// <returns></returns>
        public Usuarios CrearUsuario(Usuarios user,int idUserCreacion)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_legajo", MySqlDbType.VarChar) { Value = user.legajo },
                        new("i_nombre_usuario", MySqlDbType.VarChar) { Value = user.nombre_usuario },
                        new("i_id_perfil", MySqlDbType.Int32) { Value = user.id_perfil },
                        new("i_id_usuario_alta", MySqlDbType.Bit) { Value = user.activo }
                        ];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_USUARIOS_Crear_Usuario", parameters);

                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearUsuario", ex.Message, "UsuarioAdapter");
                    return new();
                }
            }
            else return new();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="nombres"></param>
        /// <param name="apellidos"></param>
        /// <param name="id_especialidad"> La carrera que cursa, puede ser nula en caso de empleados</param>
        /// <param name="idUserCreacion"></param>
        /// <returns></returns>
        public Usuarios CrearRegistroConUsuario(Usuarios user,string nombres,string apellidos,int? id_especialidad, int idUserCreacion)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_legajo", MySqlDbType.VarChar) { Value = user.legajo },
                        new("i_nombres", MySqlDbType.VarChar) { Value = nombres },
                        new("i_apellidos", MySqlDbType.VarChar) { Value = apellidos},
                        new("i_id_especialidad", MySqlDbType.VarChar) { Value = id_especialidad},
                        new("i_nombre_usuario", MySqlDbType.VarChar) { Value = user.nombre_usuario },
                        new("i_id_perfil", MySqlDbType.Int32) { Value = user.id_perfil },
                        new("i_id_usuario_alta", MySqlDbType.Bit) { Value = user.activo }
                        ];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_USUARIOS_Crear_Registro_Estudiante", parameters);

                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearRegistroConUsuario", ex.Message, "UsuarioAdapter");
                    return new();
                }
            }
            else return new();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="legajo"></param>
        /// <param name="nombres"></param>
        /// <param name="apellidos"></param>
        /// <param name="idEspecialidad"></param>
        /// <returns></returns>
        public Usuarios CrearEstudiante(string legajo, string nombres, string apellidos,int idEspecialidad)
        {
            if (MotorDB == "MySQL")
            {
                try
                {
                    //Inicializa un valor y le asigna el tipo
                    List<MySqlParameter> parameters = [
                        new("i_legajo", MySqlDbType.VarChar) { Value = legajo },
                        new("i_nombres", MySqlDbType.VarChar) { Value = nombres },
                        new("i_apellidos", MySqlDbType.VarChar) { Value = apellidos},
                        new("i_id_especialidad", MySqlDbType.Int32) { Value = idEspecialidad }
                        ];

                    GeneralAdapterMySQL consult = new();
                    DataTable respuesta = consult.ExecuteStoredProcedure("MODULO_USUARIOS_Crear_Nuevo_Estudiante", parameters);

                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return new();
                    else return new(respuesta.Rows[0]);
                }
                catch (Exception ex)
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearUsuario", ex.Message, "UsuarioAdapter");
                    return new();
                }
            }
            else return new();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_funcion"></param>
        /// <param name="id_perfil"></param>
        /// <returns></returns>
        public bool TienePermiso(int id_funcion,int id_perfil)
        {
            //Si ya tiene en la cache no los vuelve a buscar
            if (permisosRegistrados != null && permisosRegistrados.Count > 0)
            {
                List<FuncionesXPerfiles> permisos = [.. permisosRegistrados.Where(x => x.id_perfil == id_perfil
                && x.id_funcion == id_funcion)];
                if (permisos.Count >= 1) return true;
                else return false;
            }
            else
            {
                if (MotorDB == "MySQL")
                {
                    //Busca en la vista de permisos y los deja cargados, son 300 registros aproximadamente
                    try
                    {
                        GeneralAdapterMySQL consultor = new();
                        DataTable respuesta = consultor.ExecuteView("MODULO_USUARIOS_Vista_Permisos");

                        if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return false;

                        permisosRegistrados = [];
                        foreach (DataRow dr in respuesta.Rows)
                        {
                            FuncionesXPerfiles fp = new(dr);
                            permisosRegistrados.Add(fp);
                        }

                        List<FuncionesXPerfiles> permisos = [.. permisosRegistrados.Where(x => x.id_perfil == id_perfil
                    && x.id_funcion == id_funcion)];
                        if (permisos.Count >= 1) return true;
                        else return false;
                    }
                    catch (Exception ex)
                    {
                        Logger.RegistrarDatos(Logger.LogOptions.Error, "TienePermiso", ex.Message, "UsuarioAdapter");
                        return false;
                    }
                }
                else return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <returns></returns>
        public bool CrearSesionUsuario(int id_usuario)
        {
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    List<MySqlParameter> parameters = [
                    new("i_id_usuario", MySqlDbType.Int32) { Value = id_usuario },
                    new("i_fecha_ini", MySqlDbType.VarChar) { Value = DateTime.UtcNow.ToString("yyyy-MM-dd")},
                    new("i_fecha_fin", MySqlDbType.VarChar) { Value = DateTime.UtcNow.ToString("yyyy-MM-dd")}];

                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_USUARIOS_Crear_Sesion", parameters);

                    if (respuesta.Rows.Count == 0 || respuesta.Rows[0][0].ToString() == "ERROR") return false;
                    else return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "CrearSesionUsuario", ex.Message, "UsuarioAdapter");
                return false;
            }
        }
    }
}
