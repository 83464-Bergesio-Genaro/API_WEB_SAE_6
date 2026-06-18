using System.Data;

namespace API_WEB_SAE_6.Models.Viaje
{
    /// <summary> </summary>
    public class ViajeXInscripcion
    {
        /// <summary> </summary>
        public int id { get; set; } = -1;
        /// <summary> </summary>
        public int id_viaje  { get; set; } = -1;
        /// <summary> </summary>
        public string legajo_estudiante { get; set; } = "";
        /// <summary> </summary>
        public string nombre_estudiante { get; set; } = "";
        /// <summary> </summary>
        public bool documentacion_presentada { get; set; } = false;
        /// <summary> </summary>
        public ViajeXInscripcion() { }
        /// <summary> </summary>
        public ViajeXInscripcion(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "-1");
            id_viaje = int.Parse(data["id_viaje"].ToString() ?? "-1");
            legajo_estudiante = data["legajo_estudiante"].ToString() ?? "";
            nombre_estudiante = data["nombre_estudiante"].ToString() ?? "";
            documentacion_presentada = data["documentacion_presentada"].ToString() == "1";
        }
    }
}
