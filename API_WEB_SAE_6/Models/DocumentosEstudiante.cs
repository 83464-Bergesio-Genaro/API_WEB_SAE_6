using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class DocumentosEstudiante
    {
        public int id { get; set; }
        public string legajo { get; set; }
        public int id_tipo_documento { get; set; }
        public string nombre_documento { get; set; }
        public byte[]? datos_documento { get; set; }
        public string extension { get; set; }
        public DocumentosEstudiante()
        {

        }

        public DocumentosEstudiante(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            legajo = data["legajo"].ToString() ?? "ERROR";
            id_tipo_documento = int.Parse(data["id_tipo_documento"].ToString() ?? "0");
            nombre_documento = data["nombre_documento"].ToString() ?? "ERROR";
            datos_documento = data["datos_documento"] == null || data["datos_documento"].ToString() == "" ? null : (byte[])data["datos_documento"];
            extension = data["extension"].ToString() ?? "0";
        }
    }
}
