using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class ServiciosInternosFacultad
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int nro_telefono { get; set; }
        public int nro_interno_telefono { get; set; }
        public string horario_atencion { get; set; }
        public string horario_atencion_real { get; set; }
        public string email_institucional { get; set; }

        public ServiciosInternosFacultad()
        {

        }

        public ServiciosInternosFacultad(DataRow data)
        {
            id = int.Parse(data["id_servicio"].ToString() ?? "0");
            nombre = data["nombre"].ToString() ?? "0";
            nro_telefono = int.Parse(data["nro_telefono"].ToString() ?? "0");
            nro_interno_telefono = int.Parse(data["nro_interno_telefono"].ToString() ?? "0");
            horario_atencion = data["horario_atencion"].ToString() ?? "0";
            horario_atencion_real = data["horario_atencion_real"].ToString() ?? "0";
            email_institucional = data["email_institucional"].ToString() ?? "0";
        }
    }
}
