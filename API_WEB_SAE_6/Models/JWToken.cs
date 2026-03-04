namespace API_WEB_SAE_6.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Constructor por defecto de la clase JWToken. Este constructor inicializa una nueva instancia de la clase sin asignar un valor específico al campo "token". Es útil para crear objetos de tipo JWToken sin necesidad de proporcionar un token inicial, permitiendo que el valor del token se establezca posteriormente a través de métodos o asignaciones directas.
    /// </remarks>
    /// <param name="token"></param>
    public class JWToken(string token)
    {
        /// <summary>
        /// Token JWT generado para autenticación y autorización en la aplicación. Este campo almacena el token JWT que se utiliza para validar la identidad del usuario y controlar el acceso a los recursos protegidos en la aplicación. El token contiene información codificada sobre el usuario, como su ID, roles y permisos, y se utiliza para garantizar la seguridad y la integridad de las solicitudes realizadas por el cliente.
        /// </summary>
        public string token { get; set; } = token;
    }
}
