using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class DocumentosPrensa
    {
        public int id { get; set; }
        public int id_tipo_documento { get; set; }
        public string nombre_documento { get; set; }
        public byte[]? datos_documento { get; set; }
        public string extension { get; set; }
        public int? id_vinculacion { get; set; }

        public DocumentosPrensa()
        {

        }

        public DocumentosPrensa(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            id_tipo_documento = int.Parse(data["id_tipo_documento"].ToString() ?? "0");
            nombre_documento = data["nombre_documento"].ToString() ?? "ERROR";
            datos_documento = data["datos_documento"] == null || data["datos_documento"].ToString() == "" ? null : (byte[])data["datos_documento"];
            extension = data["extension"].ToString() ?? "0";
            id_vinculacion = data["id_vinculacion"] == null || data["id_vinculacion"].ToString() == "" ? null : int.Parse(data["id_vinculacion"].ToString() ?? "0");
        }
    }
}
