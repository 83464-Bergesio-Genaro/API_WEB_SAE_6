using System.Data;

namespace API_WEB_SAE_6.Models.Viaje
{
    /// <summary> </summary>
    public class EmpresaViaje
    {
        /// <summary></summary>
        public int id { get; set; } = -1;
        /// <summary></summary>
        public string nombre { get; set; } = "";
        /// <summary></summary>
        public string cuit { get; set; } = "";
        /// <summary></summary>
        public string cbu { get; set; } = "";
        /// <summary></summary>
        public string email { get; set; } = "";
        /// <summary></summary>
        public string contacto { get; set; } = "";
        /// <summary></summary>
        public bool activo { get; set; } = false;
        /// <summary>
        /// 
        /// </summary>
        public EmpresaViaje() { }
        /// <summary>
        /// 
        /// </summary>
        public EmpresaViaje(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "-1");
            nombre = data["nombre"].ToString() ?? "";
            this.cuit = data["cuit"].ToString() ?? "";
            this.cbu = data["cbu"].ToString() ?? "";
            this.email = data["email"].ToString() ?? "";
            this.contacto = data["contacto"].ToString() ?? "";
            this.activo = data["activo"].ToString() == "1";
        }
    }
}
