using System.Data;
using System.IO.Compression;

namespace API_WEB_SAE_6.Models.Prensa
{
    /// <summary>
    /// Clase modelo para los documentos de prensa, esta clase representa la informacion de los documentos de prensa que se encuentran en la base de datos, como el id, el tipo de documento, el nombre del documento, los datos del documento, la extension del documento y el id de vinculacion del documento.
    /// </summary>
    public class DocumentosPrensa
    {
        /// <summary>
        /// Propiedad que representa el id del documento de prensa, esta propiedad es de tipo entero y es la clave primaria de la tabla de documentos de prensa en la base de datos.
        /// </summary>
        public int id { get; set; } = -1;
        /// <summary>
        /// Propiedad que representa el id del tipo de documento de prensa, esta propiedad es de tipo entero y es una clave foranea que hace referencia a la tabla de tipos de documentos de prensa en la base de datos.
        /// </summary>
        public int id_tipo_documento { get; set; } = -1;
        /// <summary>
        /// Propiedad que representa el nombre del documento de prensa, esta propiedad es de tipo string y es el nombre del documento que se encuentra en la base de datos, esta propiedad no puede ser nula ni vacia, si ocurre un error al obtener el nombre del documento se asigna el valor "ERROR" a esta propiedad.
        /// </summary>
        public string nombre_documento { get; set; } = "";
        /// <summary>
        /// Propiedad que representa la extension del documento de prensa, esta propiedad es de tipo string y es la extension del documento que se encuentra en la base de datos, esta propiedad no puede ser nula ni vacia, si ocurre un error al obtener la extension del documento se asigna el valor "ERROR" a esta propiedad.
        /// </summary>
        public string extension { get; set; } = "";
        /// <summary>
        /// Tamaño en Bytes del archivo, en la base es INT y deberia contemplar hasta los 50mb
        /// </summary>
        public long tamanio { get; set; } = -1;
        /// <summary>
        /// Locacion en el sistema de archivos
        /// </summary>
        public string ruta { get; set; } = "";
        /// <summary>
        /// Propiedad que representa el id de vinculacion del documento de prensa, esta propiedad es de tipo entero y es una clave foranea que hace referencia a la tabla de vinculaciones en la base de datos, esta propiedad puede ser nula si no se encuentran datos en la base de datos o si ocurre un error al obtener el id de vinculacion del documento, si ocurre un error al obtener el id de vinculacion del documento se asigna el valor null a esta propiedad.
        /// </summary>
        public int? id_vinculacion { get; set; }
        /// <summary>
        /// Cree este campo que no tiene un reflejo en la base de datos para evitar la descarga de cualquier tipo de archivo
        /// </summary>
        public bool libre_consumo { get; set; }=false;
        /// <summary>
        /// Constructor de la clase DocumentosPrensa, este constructor es el constructor por defecto de la clase, este constructor no recibe parametros y no realiza ninguna accion, este constructor es necesario para poder crear objetos de la clase DocumentosPrensa sin necesidad de pasarle datos al constructor, este constructor es utilizado principalmente para crear objetos vacios de la clase DocumentosPrensa que luego pueden ser llenados con datos obtenidos de la base de datos o de otras fuentes, si se desea crear un objeto de la clase DocumentosPrensa con datos se debe utilizar el constructor que recibe un DataRow como parametro.
        /// </summary>
        public DocumentosPrensa(){}
        /// <summary>
        /// Constructor de la clase DocumentosPrensa, este constructor recibe un DataRow como parametro y asigna los valores de las propiedades de la clase a partir de los datos del DataRow, este constructor es utilizado principalmente para crear objetos de la clase DocumentosPrensa a partir de los datos obtenidos de la base de datos, si se desea crear un objeto de la clase DocumentosPrensa sin datos se debe utilizar el constructor por defecto que no recibe parametros, si ocurre un error al obtener los datos del DataRow se asignan valores por defecto a las propiedades de la clase, como -1 para los enteros, "" para los strings y null para los byte arrays y los enteros nulos.
        /// </summary>
        /// <param name="data"></param>
        public DocumentosPrensa(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            id_tipo_documento = int.Parse(data["id_tipo_documento"].ToString() ?? "0");
            nombre_documento = data["nombre_documento"].ToString() ?? "ERROR";
            tamanio = int.Parse(data["tamanio"].ToString() ?? "0");
            ruta = data["ruta"].ToString() ?? "";
            extension = data["extension"].ToString() ?? "";
            id_vinculacion = data["id_vinculacion"] == null || data["id_vinculacion"].ToString() == "" ? null : int.Parse(data["id_vinculacion"].ToString() ?? "0");
            libre_consumo = ruta.Contains("Publico");//Solo los archivos publicos pueden ser consumidos desde descargas endpoint
        }
    }
}
