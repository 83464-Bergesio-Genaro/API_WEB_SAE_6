using API_WEB_SAE_6.Models.Usuario;
using API_WEB_SAE_6.Tools;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TransporteBoleto_API.Tools
{
    /// <summary>
    /// 
    /// </summary>
    public class GestorToken
    {
        private readonly IDistributedCache _cache;
        private readonly SettingsReader.ConfiguracionJWT Configuracion = SettingsReader.GetAppSettings().JwtSettings;

        /// <summary>
        /// El constructor permite que .NET inyecte la caché automáticamente
        /// </summary>
        /// <param name="cache"></param>
        public GestorToken(IDistributedCache cache)
        {
            _cache = cache;
        }
        /// <summary>
        /// Este metodo crea el Token con duracion estipulada en el archivo de configuracion y ademas
        /// almacenas los datos del usuario
        /// </summary>
        /// <returns></returns>
        public string CreateToken(string legajoArmado, Usuarios usr)
        {
            try
            {
                //Guardamos todos los datos necesarios para operar mas adelante
                string claimValue = legajoArmado + "," + usr.id_perfil + "," + usr.id;

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                    [
                        new Claim(ClaimTypes.NameIdentifier, claimValue)
                    ]),
                    Expires = DateTime.UtcNow.AddMinutes(Configuracion.MinutesToExpire),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Configuracion.SecretKey),
                        SecurityAlgorithms.HmacSha256Signature),
                    Issuer = Configuracion.Issuer,
                    Audience = Configuracion.Audience,
                };

                JwtSecurityTokenHandler tokenHandler = new();
                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Error, "CreateToken", ex.Message, "GestorToken");
                return "ERROR";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<bool> BlackListToken(HttpRequest request)
        {
            try
            {
                string? authHeader = request.Headers.Authorization.FirstOrDefault();

                // Validación de seguridad por si la cabecera no viene o es incorrecta
                if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Alerta, "BlackListToken", "No posee ningun token asociado", "GestorToken");
                    return false;
                }

                string token = authHeader.Substring(7);
                JwtSecurityTokenHandler handler = new();

                if (!handler.CanReadToken(token))
                {
                    Logger.RegistrarDatos(Logger.LogOptions.Alerta, "BlackListToken", "No se pudo leer el token", "GestorToken");
                    return false;
                }

                JwtSecurityToken jwtToken = handler.ReadJwtToken(token);

                // Calcular el tiempo restante de vida del token
                var expClaim = jwtToken.ValidTo;
                var tiempoRestante = expClaim - DateTime.UtcNow;

                if (tiempoRestante.TotalSeconds > 0)
                {
                    var opcionesCache = new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = tiempoRestante
                    };

                    // Guardar de forma asíncrona
                    await _cache.SetStringAsync($"blacklist:{token}", "revocado", opcionesCache);
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.RegistrarDatos(Logger.LogOptions.Alerta, "BlackListToken", ex.Message, "GestorToken");
                return false;
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class JwtBlacklistMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDistributedCache _cache;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="cache"></param>
        public JwtBlacklistMiddleware(RequestDelegate next, IDistributedCache cache)
        {
            _next = next;
            _cache = cache;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            string? authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                string token = authHeader.Substring(7); // Quitar el texto "Bearer "
                string? esInvalido = await _cache.GetStringAsync($"blacklist:{token}");

                if (esInvalido != null)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync("{\"error\": \"El token ha sido revocado (Cierre de sesión).\"}");
                    return; // Corta el flujo, no llega al controlador
                }
            }

            await _next(context); // El token es válido, continúa a la API
        }
    }
}