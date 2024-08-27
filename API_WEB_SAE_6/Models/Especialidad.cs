namespace API_WEB_SAE_6.Models
{
    public class Especialidad
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public bool activo { get; set; }

        public Especialidad()
        {

        }

        public Especialidad(int id, string nombre, string descripcion, bool estaActivo)
        {
            this.id = id;
            this.nombre = nombre;
            this.descripcion = descripcion;
            activo = estaActivo;
        }
    }
}
