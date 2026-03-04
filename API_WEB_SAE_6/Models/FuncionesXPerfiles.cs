using System.Data;

namespace API_WEB_SAE_6.Models
{
    /// <summary>
    /// Clase que representa la relación entre funciones y perfiles en el sistema.
    /// </summary>
    public class FuncionesXPerfiles
    {
        /// <summary>
        /// Identificador único de la relación entre función y perfil.
        /// </summary>
        public int id { get; set; } = -1;
        /// <summary>
        /// Identificador de la función asociada al perfil.
        /// </summary>
        public int id_funcion { get; set; } = -1;
        /// <summary>
        /// Identificador del perfil asociado a la función.
        /// </summary>
        public int id_perfil { get; set; } = -1;
        /// <summary>
        /// Constructor que inicializa una instancia de FuncionesXPerfiles a partir de un DataRow.
        /// </summary>
        /// <param name="dr"></param>
        public FuncionesXPerfiles(DataRow dr)
        {
            id = int.Parse(dr["id"].ToString() ?? "0");
            id_funcion = int.Parse(dr["id_funcion"].ToString() ?? "0");
            id_perfil = int.Parse(dr["id_perfil"].ToString() ?? "0");
        }
    }
}
