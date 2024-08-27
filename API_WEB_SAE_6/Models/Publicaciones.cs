using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class Publicaciones
    {
        public int id { get; set; }
        public string titulo_publicacion { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_vigencia { get; set; }
        public int prioridad { get; set; }
        public bool no_dar_baja { get; set; }
        public int visualizaciones { get; set; }
        public Publicaciones()
        {

        }

        public Publicaciones(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            titulo_publicacion = data["titulo_publicacion"].ToString() ?? "0";
            descripcion = data["descripcion"].ToString() ?? "0";
            fecha_inicio = DateTime.Parse(data["fecha_inicio"].ToString() ?? "0");
            fecha_vigencia = DateTime.Parse(data["fecha_vigencia"].ToString() ?? "0");
            prioridad = int.Parse(data["prioridad"].ToString() ?? "0");
            no_dar_baja = bool.Parse(data["no_dar_baja"].ToString() ?? "0");
            visualizaciones = int.Parse(data["visualizaciones"].ToString() ?? "0");
        }
    }
}
