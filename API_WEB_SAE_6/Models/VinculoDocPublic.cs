using System.Data;

namespace API_WEB_SAE_6.Models
{
    /// <summary>
    /// Clase que representa el vínculo entre un documento y una publicación.
    /// </summary>
    public class VinculoDocPublic
    {
        /// <summary>
        /// Identificador único del vínculo entre el documento y la publicación.
        /// </summary>
        public int id { get; set; } = -1;
        /// <summary>
        /// Identificador de la publicación a la que está vinculado el documento.
        /// </summary>
        public int id_publicacion { get; set; } = -1;
        /// <summary>
        /// Identificador del documento que está vinculado a la publicación.
        /// </summary>
        public int id_documento { get; set; } = -1;
        /// <summary>
        /// Fecha en la que se creó el vínculo entre el documento y la publicación.
        /// </summary>
        public DateTime fecha_creacion { get; set; } = DateTime.MinValue;
        /// <summary>
        /// Constructor por defecto de la clase VinculoDocPublic.
        /// </summary>
        public VinculoDocPublic(){}
        /// <summary>
        /// Constructor que inicializa una instancia de VinculoDocPublic a partir de un DataRow.
        /// </summary>
        /// <param name="data"></param>
        public VinculoDocPublic(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            id_publicacion = int.Parse(data["id_publicacion"].ToString() ?? "0");
            id_documento = int.Parse(data["id_documento"].ToString() ?? "0");
            fecha_creacion = DateTime.Parse(data["fecha_creacion"].ToString() ?? "1900-01-01");
        }
    }
}
