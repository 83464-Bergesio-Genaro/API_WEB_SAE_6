using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class DocentesDeportivos
    {
        public string cuil { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public bool activo { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public DocentesDeportivos()
        {

        }
        public DocentesDeportivos(DataRow data)
        {
            cuil = data["cuil"].ToString() ?? "ERROR";
            nombres = data["nombres"].ToString() ?? "ERROR";
            apellidos = data["apellidos"].ToString() ?? "ERROR";
            activo = bool.Parse(data["activo"].ToString() ?? "0");
            fecha_nacimiento = DateTime.Parse(data["fecha_nacimiento"].ToString() ?? "1900-01-01");
        }
    }
}
