using System.Data;

namespace API_WEB_SAE_6.Models.Compra
{
    /// <summary></summary>
    public class InformesTecnicoCompra
    {
        /// <summary></summary>
        public InformesTecnicoCompra() { }
        /// <summary></summary>
        public InformesTecnicoCompra(DataRow data)
        {
            this.nro_expediente = data["nro_expediente"].ToString() ?? "-1";
            this.id_compra = int.Parse(data["id_compra"].ToString() ?? "-1");
            this.precio_real = decimal.Parse(data["precio_real"].ToString() ?? "-1");
            this.fecha_licitacion = DateTime.Parse(data["fecha_licitacion"].ToString() ?? "");
            this.fecha_informe = DateTime.Parse(data["fecha_informe"].ToString() ?? "");
            this.nombre_solicitante = data["nombre_solicitante"].ToString() ?? "-1"; ;
            this.nombre_ganador = data["nombre_ganador"].ToString() ?? "-1"; ;
        }

        /// <summary> </summary>
        public string nro_expediente { get; set; } = "";
        /// <summary></summary>
        public int id_compra { get; set; } = -1;
        /// <summary></summary>
        public decimal precio_real { get; set; } = 0;
        /// <summary></summary>
        public DateTime fecha_licitacion { get; set; } = DateTime.MinValue;
        /// <summary></summary>
        public DateTime fecha_informe { get; set; } = DateTime.MinValue;
        /// <summary></summary>
        public string nombre_solicitante { get; set; } = "";
        /// <summary></summary>
        public string nombre_ganador { get; set; } = "";
    }
}
