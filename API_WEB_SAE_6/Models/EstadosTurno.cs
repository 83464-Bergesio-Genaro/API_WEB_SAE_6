using System.Data;

namespace API_WEB_SAE_6.Models
{
    /// <summary>
    /// Es una clase representa los estados de los turnos médicos en la aplicación. Contiene propiedades para almacenar información relevante sobre cada estado, como el ID del estado y su descripción. Esta clase se utiliza para gestionar y representar los diferentes estados que un turno médico puede tener en el sistema, facilitando la organización y seguimiento de los turnos médicos en la aplicación.
    /// </summary>
    public class EstadosTurno
    {
        /// <summary>
        /// El identificador del estado
        /// </summary>
        public int id { get; set; } = -1;
        /// <summary>
        /// El nombre con el cual se identifica
        /// </summary>
        public string estado_turno { get; set; } = "";
        /// <summary>
        /// Este constructor es para utilizarlo en los turnos médicos, para asignar el estado del turno directamente desde el ID del estado, sin necesidad de realizar una consulta adicional a la base de datos para obtener la descripción del estado. Esto es útil para optimizar el rendimiento y simplificar el proceso de asignación de estados a los turnos médicos en la aplicación.
        /// </summary>
        /// <param name="id">El identificador</param>
        /// <param name="estado_turno">El nombre del estado</param>
        public EstadosTurno(int id, string estado_turno)
        {
            this.id = id;
            this.estado_turno = estado_turno;
        }
        /// <summary>
        /// Este constructor es para consumirlo desde la vista directamente desde la base de datos.
        /// </summary>
        /// <param name="data">Una fila de datos</param>
        public EstadosTurno(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            estado_turno = data["estado_turno"].ToString() ?? "NO DATA";
        }
    }
}
