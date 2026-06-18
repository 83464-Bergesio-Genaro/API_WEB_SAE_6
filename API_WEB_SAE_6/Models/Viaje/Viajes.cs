using System.Data;

namespace API_WEB_SAE_6.Models.Viaje
{
    /// <summary>
    /// Son los viajes que se gestionan desde la secretaria
    /// </summary>
    public class Viajes
    {
        /// <summary>
        /// Constructor generico necesario para los endpoints
        /// </summary>
        public Viajes() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public Viajes(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "-1");
            nombre = data["nombre"].ToString()??"";
            fecha_inicio = DateTime.Parse(data["fecha_inicio"].ToString() ?? "");
            fecha_fin = DateTime.Parse(data["fecha_fin"].ToString() ?? "");
            seguro_confirmado = data["seguro_confirmado"].ToString() == "1";
            origen = data["origen"].ToString() ?? "";
            destino = data["destino"].ToString() ?? "";
            motivo = data["motivo"].ToString() ?? "";
            cantidad_personas = int.Parse(data["cantidad_personas"].ToString() ?? "0");
            id_empresa_viaje = int.Parse(data["id_empresa_viaje"].ToString() ?? "0");
            nombre_empresa = data["nombre_empresa"].ToString() ?? "";
            documentacion_presentada = data["documentacion_presentada"].ToString() == "1";
            costo_aproximado = decimal.Parse(data["costo_aproximado"].ToString() ?? "0");
        }
        /// <summary> </summary>
        public int id { get; set; } = -1;
        /// <summary> </summary>
        public string nombre { get; set; } = "";
        /// <summary> </summary>
        public DateTime fecha_inicio { get; set; } = DateTime.MinValue;
        /// <summary> </summary>
        public DateTime fecha_fin { get; set; } = DateTime.MinValue;
        /// <summary> </summary>
        public bool seguro_confirmado { get; set; }= false;
        /// <summary> </summary>
        public string origen { get; set; } = "";
        /// <summary> </summary>
        public string destino { get; set; } = "";
        /// <summary> </summary>
        public string motivo { get; set; } = "";
        /// <summary> </summary>
        public int cantidad_personas { get; set; } = -1;
        /// <summary> </summary>
        public int id_empresa_viaje { get; set; } = -1;
        /// <summary> </summary>
        public string? nombre_empresa { get; set; }
        /// <summary> </summary>
        public bool documentacion_presentada { get; set; } = false;
        /// <summary> </summary>
        public decimal costo_aproximado { get; set; }= 0;

    }
}
