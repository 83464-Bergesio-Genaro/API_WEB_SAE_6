using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class TorneosXInscripcion
    {
        public int id { get; set; }
        public string nombre_torneo { get; set; }
        public DateTime fecha_inscripcion { get; set; }
        public string nombre_deporte { get; set; }
        public TorneosXInscripcion()
        {

        }

        public TorneosXInscripcion(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            nombre_torneo = data["nombre_torneo"].ToString() ?? "ERROR";
            fecha_inscripcion = DateTime.Parse(data["fecha_inscripcion"].ToString() ?? "1900-01-01");
            nombre_deporte = data["nombre_deporte"].ToString() ?? "ERROR";
        }
    }
}
