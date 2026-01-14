using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class DeporteXInscripcion
    {
        public int id { get; set; }
        public int id_deporte { get; set; }
        public string nombre_deporte { get; set; }
        public DateTime fecha_inscripcion { get; set; }
        public DeporteXInscripcion()
        {

        }
        public DeporteXInscripcion(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            id_deporte = int.Parse(data["id_deporte"].ToString() ?? "0");
            nombre_deporte = data["nombre_deporte"].ToString() ?? "ERROR";
            fecha_inscripcion = DateTime.Parse(data["fecha_inscripcion"].ToString() ?? "1900-01-01");
        }
    }
}
