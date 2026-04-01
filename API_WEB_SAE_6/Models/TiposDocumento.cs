using System.Data;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API_WEB_SAE_6.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class TiposDocumento
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; } = -1;
        /// <summary>
        /// 
        /// </summary>
        public string nombre { get; set; } = "";
        /// <summary>
        /// 
        /// </summary>
        public string extension { get; set; } = "";
        /// <summary>
        /// 
        /// </summary>
        public TiposDocumento() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public TiposDocumento(DataRow data) 
        {
            id = int.Parse(data["id"].ToString() ?? "-1");
            nombre = data["nombre_documento"].ToString() ?? "";
            extension = data["extension"].ToString() ?? "";
        }
    }
}
