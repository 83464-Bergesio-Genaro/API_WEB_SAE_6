using System.Data;

namespace API_WEB_SAE_6.Models.Compra
{
    /// <summary> </summary>
    public class DocumentosCompra
    {
        /// <summary></summary>
        public int id { get; set; } = -1;
        /// <summary></summary>
        public int id_compra { get; set; } = -1;
        /// <summary></summary>
        public int id_tipo_documento { get; set; } = -1;
        /// <summary></summary>
        public string nombre_documento { get; set; } = "";
        /// <summary></summary>
        /// <summary>
        /// Tamaño en Bytes del archivo, en la base es INT y deberia contemplar hasta los 50mb
        /// </summary>
        public long tamanio { get; set; } = -1;
        /// <summary>
        /// Locacion en el sistema de archivos
        /// </summary>
        public string ruta { get; set; } = "";
        /// <summary></summary>
        public string extension { get; set; } = "";

        /// <summary></summary>
        public DocumentosCompra()
        {

        }
        /// <summary></summary>
        public DocumentosCompra(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            id_tipo_documento = int.Parse(data["id_tipo_documento"].ToString() ?? "0");
            id_compra = int.Parse(data["id_compra"].ToString() ?? "0");
            nombre_documento = data["nombre_documento"].ToString() ?? "ERROR";
            tamanio = int.Parse(data["tamanio"].ToString() ?? "0");
            ruta = data["ruta"].ToString() ?? "";
            extension = data["extension"].ToString() ?? "";
            //id_vinculacion = data["id_vinculacion"] == null || data["id_vinculacion"].ToString() == "" ? null : int.Parse(data["id_vinculacion"].ToString() ?? "0");
        }
    }
}
