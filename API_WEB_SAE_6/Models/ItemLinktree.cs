using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace API_WEB_SAE_6.Models
{
    /// <summary>
    /// Clase que representa un item de linktree, esta clase se utiliza para mostrar la informacion de los items de linktree en la API, no se utiliza para guardar la informacion, ya que para eso se utiliza la clase ItemLinktreeAdapter, esta clase es solo para mostrar la informacion de los items de linktree en la API.
    /// </summary>
    public class ItemLinktree
    {
        /// <summary>
        /// Propiedad que representa el id del item de linktree, esta propiedad es la clave primaria de la tabla de items de linktree en la base de datos, esta propiedad se utiliza para identificar un item de linktree en la base de datos, esta propiedad es autoincrementable, por lo que no se debe asignar un valor a esta propiedad al crear un nuevo item de linktree, ya que la base de datos se encargara de asignar un valor a esta propiedad automaticamente.
        /// </summary>
        [Key]
        public int id { get; set; } = -1;
        /// <summary>
        /// Propiedad que representa el titulo del item de linktree, esta propiedad se utiliza para mostrar el titulo del item de linktree en la API, esta propiedad es obligatoria, por lo que no se debe asignar un valor nulo a esta propiedad al crear un nuevo item de linktree, ya que la base de datos no permitira guardar un item de linktree con un titulo nulo, esta propiedad tiene una longitud maxima de 255 caracteres, por lo que no se debe asignar un valor con una longitud mayor a 255 caracteres a esta propiedad al crear un nuevo item de linktree, ya que la base de datos no permitira guardar un item de linktree con un titulo con una longitud mayor a 255 caracteres.
        /// </summary>
        public string titulo { get; set; } = "";
        /// <summary>
        /// Propiedad que representa el id del icono del item de linktree, esta propiedad se utiliza para mostrar el icono del item de linktree en la API, esta propiedad es obligatoria, por lo que no se debe asignar un valor nulo a esta propiedad al crear un nuevo item de linktree, ya que la base de datos no permitira guardar un item de linktree con un id de icono nulo, esta propiedad es una clave foranea que hace referencia a la tabla de iconos en la base de datos, por lo que el valor asignado a esta propiedad debe existir en la tabla de iconos en la base de datos, esta propiedad se utiliza para identificar el icono del item de linktree en la base de datos, esta propiedad se utiliza para mostrar el icono del item de linktree en la API, esta propiedad se utiliza para mostrar el icono del item de linktree en la API, esta propiedad se utiliza para mostrar el icono del item de linktree en la API.
        /// </summary>
        public int id_index_ico { get; set; } = -1;
        /// <summary>
        /// Propiedad que representa el hipervinculo del item de linktree, esta propiedad se utiliza para mostrar el hipervinculo del item de linktree en la API, esta propiedad es obligatoria, por lo que no se debe asignar un valor nulo a esta propiedad al crear un nuevo item de linktree, ya que la base de datos no permitira guardar un item de linktree con un hipervinculo nulo, esta propiedad tiene una longitud maxima de 255 caracteres, por lo que no se debe asignar un valor con una longitud mayor a 255 caracteres a esta propiedad al crear un nuevo item de linktree, ya que la base de datos no permitira guardar un item de linktree con un hipervinculo con una longitud mayor a 255 caracteres.
        /// </summary>
        public string hipervinculo { get; set; } = "";
        /// <summary>
        /// Propiedad que representa el contador de clicks del item de linktree, esta propiedad se utiliza para mostrar el contador de clicks del item de linktree en la API, esta propiedad es opcional, por lo que se puede asignar un valor nulo a esta propiedad al crear un nuevo item de linktree, ya que la base de datos permitira guardar un item de linktree con un contador de clicks nulo, esta propiedad se utiliza para mostrar el contador de clicks del item de linktree en la API, esta propiedad se utiliza para mostrar el contador de clicks del item de linktree en la API, esta propiedad se utiliza para mostrar el contador de clicks del item de linktree en la API.
        /// </summary>
        public int? contador_clicks { get; set; }
        /// <summary>
        /// Constructor de la clase ItemLinktree, este constructor se utiliza para crear un nuevo item de linktree, este constructor no asigna valores a las propiedades de la clase, por lo que se deben asignar valores a las propiedades de la clase despues de crear un nuevo item de linktree, este constructor se utiliza para crear un nuevo item de linktree con valores por defecto, este constructor se utiliza para crear un nuevo item de linktree con valores por defecto, este constructor se utiliza para crear un nuevo item de linktree con valores por defecto.
        /// </summary>
        public ItemLinktree() { }
        /// <summary>
        /// Constructor de la clase ItemLinktree, este constructor se utiliza para crear un nuevo item de linktree a partir de un DataRow, este constructor asigna valores a las propiedades de la clase a partir de los valores del DataRow, este constructor se utiliza para crear un nuevo item de linktree a partir de un DataRow obtenido de la base de datos, este constructor se utiliza para crear un nuevo item de linktree a partir de un DataRow obtenido de la base de datos, este constructor se utiliza para crear un nuevo item de linktree a partir de un DataRow obtenido de la base de datos.
        /// </summary>
        /// <param name="data"></param>
        public ItemLinktree(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            titulo = data["titulo"].ToString() ?? "Error";
            id_index_ico = int.Parse(data["id_index_ico"].ToString() ?? "0");
            hipervinculo = data["hipervinculo"].ToString() ?? "Error";
            contador_clicks = int.Parse(data["contador_clicks"].ToString() ?? "0");
        }
    }
}
