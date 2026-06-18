using System.Data;

namespace API_WEB_SAE_6.Models.Deporte
{
    /// <summary>
    /// 
    /// </summary>
    public class DeporteXInscripcion
    {
        /// <summary></summary>
        public int id { get; set; } = -1;
        /// <summary></summary>
        public int id_deporte { get; set; } = -1;
        /// <summary></summary>
        public string nombre_deporte { get; set; } = "";
        /// <summary></summary>
        public DateTime fecha_inscripcion { get; set; } = DateTime.MinValue;
        /// <summary></summary>
        public DeporteXInscripcion()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public DeporteXInscripcion(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            id_deporte = int.Parse(data["id_deporte"].ToString() ?? "0");
            nombre_deporte = data["nombre_deporte"].ToString() ?? "ERROR";
            fecha_inscripcion = DateTime.Parse(data["fecha_inscripcion"].ToString() ?? "1900-01-01");
        }
    }
}
