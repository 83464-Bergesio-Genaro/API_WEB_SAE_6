using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class FaltaEspecialista
    {
        public int id { get; set; }
        public string cuil { get; set; }
        public DateTime fecha_alta { get; set; }
        public string observacion { get; set; }

        public FaltaEspecialista()
        {

        }

        public FaltaEspecialista(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            cuil = data["cuil_especialistas"].ToString() ?? "NO DATA";
            fecha_alta = DateTime.Parse(data["fecha_falta"].ToString() ?? "2000-01-01");
            observacion = data["observacion"].ToString() ?? "NO DATA";
        }
    }
}
