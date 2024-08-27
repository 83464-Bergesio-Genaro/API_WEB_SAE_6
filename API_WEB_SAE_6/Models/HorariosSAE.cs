using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace API_WEB_SAE_6.Models
{
#pragma warning disable CS8618 // No son nulos para que asi tenga conflicto
    /// <summary>
    /// 
    /// </summary>
    public class HorariosSAE
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hora_inicio { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hora_fin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int dia { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int id_empleado { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? nombre_empleado_atencion { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool activo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public HorariosSAE()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public HorariosSAE(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            hora_inicio = data["hora_inicio"].ToString() ?? "00:00:00";
            hora_fin = data["hora_fin"].ToString() ?? "00:00:00";
            dia = int.Parse(data["dia"].ToString() ?? "0");
            id_empleado = int.Parse(data["id_empleado"].ToString() ?? "0");
            nombre_empleado_atencion = data["nombre_empleado_atencion"].ToString() ?? "NO DATA";
            activo = bool.Parse(data["activo"].ToString() ?? "0");
        }

    }
}
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
