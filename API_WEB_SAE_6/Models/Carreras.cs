using System.Data;

namespace API_WEB_SAE_6.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Carreras
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
        public string sigla { get; set; } = "";
        /// <summary>
        /// 
        /// </summary>
        public Carreras() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public Carreras(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "-1");
            nombre = data["nombre_especialidad"].ToString() ?? "";
            sigla = data["sigla"].ToString() ?? "";
        }
    }
}
