namespace API_WEB_SAE_6.Models
{
    /// <summary>
    /// Constructor por defecto de la clase JWToken. Este constructor inicializa una nueva instancia de la clase sin asignar un valor específico al campo "token". Es útil para crear objetos de tipo JWToken sin necesidad de proporcionar un token inicial, permitiendo que el valor del token se establezca posteriormente a través de métodos o asignaciones directas.
    /// </summary>
    /// <param name="token"></param>
    /// <param name="legajoArmado"></param>
    /// <param name="nombreUsuario"></param>
    /// <param name="idPerfil"></param>
    public class JWToken(string token,string legajoArmado,string nombreUsuario,int idPerfil)
    {
        /// <summary>
        /// Token JWT generado para autenticación y autorización en la aplicación. Este campo almacena el token JWT que se utiliza para validar la identidad del usuario y controlar el acceso a los recursos protegidos en la aplicación. El token contiene información codificada sobre el usuario, como su ID, roles y permisos, y se utiliza para garantizar la seguridad y la integridad de las solicitudes realizadas por el cliente.
        /// </summary>
        public string token { get; set; } = token;
        /// <summary>
        /// El legajo exacto que tiene en autogestion
        /// </summary>
        public string legajo_armado { get; set; } = legajoArmado;
        /// <summary>
        /// El nombre del usuario que le asignamos en la aplicacion
        /// </summary>
        public string nombre_usuario { get; set; } = nombreUsuario;
        /// <summary>
        /// Es la serie de permisos asociado a este usuario
        /// </summary>
        public int id_perfil { get; set; } = idPerfil;
    }
}
