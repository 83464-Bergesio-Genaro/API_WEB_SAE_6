using System.ComponentModel.DataAnnotations;
using System.Data;

namespace API_WEB_SAE_6.Models
{
    public class EmpleadosSAE
    {
        [Key]
        public int id { get; set; }

        public string legajo { get; set; }
        //Esto es para que cuando mostremos sea mas facil y para guardar no hace falta mandarlo
        public string? nombre_empleado { get; set; }

        public bool activo { get; set; }
        public EmpleadosSAE()
        {

        }
        public EmpleadosSAE(DataRow data)
        {
            id = int.Parse(data["id"].ToString() ?? "0");
            legajo = data["legajo"].ToString() ?? "ERROR";
            nombre_empleado = data["nombre_empleado"].ToString() ?? "ERROR";
            activo = bool.Parse(data["activo"].ToString() ?? "0");
        }
    }
}
