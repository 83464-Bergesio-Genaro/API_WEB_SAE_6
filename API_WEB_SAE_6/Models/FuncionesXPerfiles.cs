using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class FuncionesXPerfiles
    {
        public int id { get; set; }
        public int id_funcion { get; set; }
        public int id_perfil { get; set; }

        public FuncionesXPerfiles(DataRow dr)
        {
            id = int.Parse(dr["id"].ToString() ?? "0");
            id_funcion = int.Parse(dr["id_funcion"].ToString() ?? "0");
            id_perfil = int.Parse(dr["id_perfil"].ToString() ?? "0");
        }
    }
}
