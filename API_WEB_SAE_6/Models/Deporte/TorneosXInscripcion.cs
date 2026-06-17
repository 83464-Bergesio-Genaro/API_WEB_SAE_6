using System.Data;

namespace API_WEB_SAE_6.Models.Deporte
{
    /// <summary>
    /// 
    /// </summary>
    public class TorneosXInscripcion
    {
        /// <summary></summary>
        public int id { get; set; } = -1;
        /// <summary></summary>
        public string nombre_torneo { get; set; } = "";
        /// <summary></summary>
        public DateTime fecha_inscripcion { get; set; } = DateTime.MinValue;
        /// <summary></summary>
        public string nombre_deporte { get; set; } = "";
        /// <summary></summary>
        public TorneosXInscripcion()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public TorneosXInscripcion(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            nombre_torneo = data["nombre_torneo"].ToString() ?? "ERROR";
            fecha_inscripcion = DateTime.Parse(data["fecha_inscripcion"].ToString() ?? "1900-01-01");
            nombre_deporte = data["nombre_deporte"].ToString() ?? "ERROR";
        }
    }
}
