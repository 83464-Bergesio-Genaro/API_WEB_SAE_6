using System.Data;

namespace API_WEB_SAE_6.Models.Compra
{
    /// <summary></summary>
    public class Compras
    {
        /// <summary></summary>
        public int id { get; set; } = -1;
        /// <summary></summary>
        public string nombre_compra { get; set; } = "";
        /// <summary></summary>
        public decimal precio_sugerido { get; set; } = 0;
        /// <summary></summary>
        public string motivo { get; set; } = "";
        /// <summary></summary>
        public DateTime fecha_compra { get; set; } = DateTime.MinValue;
        /// <summary></summary>
        public int id_usuario { get; set; } = -1;
        /// <summary></summary>
        public string? nombre_usuario { get; set; }
        /// <summary></summary>
        public Compras() { }
        /// <summary></summary>
        public Compras(DataRow data)
        {
            this.id = int.Parse(data["id"].ToString() ?? "-1");
            this.nombre_compra = data["nombre"].ToString() ?? "";
            this.precio_sugerido = decimal.Parse(data["precio_sugerido"].ToString() ?? "-1");
            this.motivo = data["motivo"].ToString() ?? "";
            this.fecha_compra = DateTime.Parse(data["fecha_compra"].ToString() ?? "-1");
            this.id_usuario = int.Parse(data["id_usuario"].ToString() ?? "-1");
            this.nombre_usuario = data["nombre_usuario"].ToString() ?? "";
        }
    }
}
