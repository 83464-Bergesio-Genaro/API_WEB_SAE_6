using API_WEB_SAE_6.Logs;
using API_WEB_SAE_6.Models.Reporteria;
using API_WEB_SAE_6.Models.Salud;
using MySqlConnector;
using System;
using System.Data;

namespace API_WEB_SAE_6.Adapters
{
    /// <summary>
    /// Es el adaptador que permite consultas de reportes en la base de datos.
    /// </summary>
    public class ReporteAdapter(string motorDB = "MySQL")
    {
        /// <summary>
        /// Define que tipo de base de datos se usa para consumir la informacion
        /// </summary>
        public string MotorDB { get; set; } = motorDB;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startYear"></param>
        /// <param name="endYear"></param>
        /// <returns></returns>
        public EstadisticasBecas? ObtenerEstadisticasBecas(int startYear, int endYear)
        {
            string method = "ObtenerEstadisticasBecas";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    List<MySqlParameter> parameters = [
                        new("i_start_year", MySqlDbType.Int32) { Value = startYear },
                        new("i_end_year", MySqlDbType.Int32) { Value = endYear }
                    ];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_REPORTES_Buscar_Becarios_Tipo", parameters);
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    EstadisticasBecas estadisticas = new();
                    //Por cada año una fila
                    foreach (DataRow row in respuesta.Rows)
                    {
                        //En cada nivel vienen las estadisticas de cada año, por eso se hace un foreach para recorrer cada fila y agregarla a la lista
                        estadisticas.YearXType.AddRange([
                        new()
                        {
                            nombre = "Investigacion",
                            cantidad = Convert.ToInt32(row["investigacion"]),
                            anio = Convert.ToInt32(row["anio_beca"])
                        },
                            new()
                        {
                            nombre = "Economica",
                            cantidad = Convert.ToInt32(row["economica"]),
                            anio = Convert.ToInt32(row["anio_beca"])
                        },
                        new()
                        {
                            nombre = "Servicio",
                            cantidad = Convert.ToInt32(row["servicio"]),
                            anio = Convert.ToInt32(row["anio_beca"])
                        }]);

                    }
                    respuesta = consultor.ExecuteStoredProcedure("MODULO_REPORTES_Buscar_Becarios_Estado", parameters);
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    foreach (DataRow row in respuesta.Rows)
                    {
                        estadisticas.YearXState.Add(new()
                        {
                            nombre = row["estado"].ToString()??"",
                            cantidad = Convert.ToInt32(row["total"]),
                            anio = Convert.ToInt32(row["anio_beca"])
                        });
                    }
                    respuesta = consultor.ExecuteStoredProcedure("MODULO_REPORTES_Buscar_Becarios_Renovacion", parameters);

                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    foreach (DataRow row in respuesta.Rows)
                    {
                        estadisticas.YearXRenovation.Add(new()
                        {
                            nombre = row["situacion"].ToString() ?? "",
                            cantidad = Convert.ToInt32(row["total"]),
                            anio = Convert.ToInt32(row["anio_beca"])
                        });
                    }

                    return estadisticas;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "ReporteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startYear"></param>
        /// <param name="endYear"></param>
        /// <returns></returns>
        public EstadisticasDeportes? ObtenerEstadisticasDeporte(int startYear, int endYear)
        {
            string method = "ObtenerEstadisticasDeporte";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    List<MySqlParameter> parameters = [
                        new("i_start_year", MySqlDbType.Int32) { Value = startYear },
                        new("i_end_year", MySqlDbType.Int32) { Value = endYear }
                    ];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_REPORTES_Buscar_Inscriptos_Deporte", parameters);
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    EstadisticasDeportes estadisticas = new();
                    //Por cada año una fila
                    foreach (DataRow row in respuesta.Rows)
                    {
                        estadisticas.InscripcionesDeportistas.Add(new()
                        {
                            id_deporte = Convert.ToInt32(row["id"]),
                            nombre = row["nombre"].ToString() ?? "",
                            cantidad = Convert.ToInt32(row["total"]),
                            anio = Convert.ToInt32(row["anio_inscripcion"])
                        });
                    }
                    respuesta = consultor.ExecuteStoredProcedure("MODULO_REPORTES_Buscar_Torneos", parameters);
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;
                    foreach (DataRow row in respuesta.Rows)
                    {
                        estadisticas.TorneosXDeporte.Add(new()
                        {
                            id_deporte = Convert.ToInt32(row["id"]),
                            nombre = row["nombre"].ToString() ?? "",
                            cantidad = Convert.ToInt32(row["total"]),
                            anio = Convert.ToInt32(row["anio_inscripcion"])
                        });
                    }
                    
                    return estadisticas;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "ReporteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public EstadisticasSalud? ObtenerEstadisticasSalud(DateOnly startDate, DateOnly endDate)
        {
            string method = "ObtenerEstadisticasSalud";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    List<MySqlParameter> parameters = [
                        new("i_start_date", MySqlDbType.Date) { Value = startDate },
                        new("i_end_date", MySqlDbType.Date) { Value = endDate }
                    ];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_REPORTES_Buscar_Turnos", parameters);
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    EstadisticasSalud estadisticas = new();
                    //Por cada año una fila
                    foreach (DataRow row in respuesta.Rows)
                    {
                        estadisticas.TurnosXEspecialista.Add(new()
                        {
                            nombre = row["especialista"].ToString() ?? "",
                            cantidad = Convert.ToInt32(row["total"]),
                            anio = Convert.ToInt32(row["anio"]),
                            mes = Convert.ToInt32(row["mes"])
                        });
                    }

                    return estadisticas;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "ReporteAdapter");
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public EstadisticasViaje? ObtenerEstadisticasViajes(DateOnly startDate, DateOnly endDate)
        {
            string method = "ObtenerEstadisticasViajes";
            try
            {
                //Por si algun momento les pinta cambiar de motor nuevamente
                if (MotorDB == "MySQL")
                {
                    List<MySqlParameter> parameters = [
                        new("i_start_date", MySqlDbType.Date) { Value = startDate },
                        new("i_end_date", MySqlDbType.Date) { Value = endDate }
                    ];
                    GeneralAdapterMySQL consultor = new();
                    DataTable respuesta = consultor.ExecuteStoredProcedure("MODULO_REPORTES_Buscar_Viajes", parameters);
                    //Con esto verificamos que no haya ocurrido un error, en la capa superior levanta el 409 conflict
                    if (respuesta.Rows.Count > 0 && respuesta.Rows[0][0].ToString() == "ERROR") return null;

                    EstadisticasViaje estadisticas = new();
                    //Por cada año una fila
                    foreach (DataRow row in respuesta.Rows)
                    {
                        estadisticas.ViajesXMesAnio.Add(new()
                        {
                            cantidad = Convert.ToInt32(row["cantidad_viajes"]),
                            cantidad_estudiantes = Convert.ToInt32(row["cantidad_estudiantes"]),
                            anio = Convert.ToInt32(row["anio"]),
                            mes = Convert.ToInt32(row["mes"])
                        });
                    }

                    return estadisticas;
                }
                else return null;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, method, ex.Message, "ReporteAdapter");
                return null;
            }
        }
    }
}
