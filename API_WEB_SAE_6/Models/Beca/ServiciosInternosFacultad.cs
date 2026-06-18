using System.Data;

namespace API_WEB_SAE_6.Models.Beca
{
    /// <summary></summary>
    public class ServiciosInternosFacultad
    {
        /// <summary></summary>
        public int id { get; set; } = -1;
        /// <summary></summary>
        public string nombre { get; set; } = "";
        /// <summary></summary>
        public string nro_telefono { get; set; } = "";
        /// <summary></summary>
        public string nro_interno_telefono { get; set; } = "";
        /// <summary></summary>
        public string horario_atencion { get; set; } = "";
        /// <summary></summary>
        public string horario_atencion_final { get; set; } = "";
        /// <summary></summary>
        public string email_institucional { get; set; } = "";
        /// <summary></summary>
        public ServiciosInternosFacultad() { }
        /// <summary></summary>
        public ServiciosInternosFacultad(DataRow data)
        {
            if(data.Table.Columns.Contains("id_servicio")) id = int.Parse(data["id_servicio"].ToString() ?? "-1");
            else id = int.Parse(data["id"].ToString() ?? "-1");
            nombre = data["nombre"].ToString() ?? "0";
            nro_telefono = data["nro_telefono"].ToString() ?? "";
            nro_interno_telefono = data["nro_interno_telefono"].ToString() ?? "0";
            horario_atencion = data["horario_atencion"].ToString() ?? "0";
            horario_atencion_final = data["horario_atencion_final"].ToString() ?? "0";
            email_institucional = data["email_institucional"].ToString() ?? "0";
        }
    }
}
