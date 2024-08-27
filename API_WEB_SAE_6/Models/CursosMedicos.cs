using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class CursosMedicos
    {
        public int id { get; set; }
        public string nombre_curso { get; set; }
        public string nombre_docente { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_fin { get; set; }
        public int cupo_maximo { get; set; }
        public bool activo { get; set; }

        public CursosMedicos()
        {

        }

        public CursosMedicos(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            nombre_curso = data["nombre_curso"].ToString() ?? "NO DATA";
            nombre_docente = data["nombre_docente"].ToString() ?? "NO DATA";
            fecha_inicio = DateTime.Parse(data["fecha_inicio"].ToString() ?? "2000-01-01");
            fecha_fin = DateTime.Parse(data["fecha_fin"].ToString() ?? "2000-01-01");
            cupo_maximo = int.Parse(data["cupo_maximo"].ToString() ?? "0");
            activo = bool.Parse(data["activo"].ToString() ?? "0");
        }
    }
}
