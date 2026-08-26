using API_WEB_SAE_6.Tools;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using TransporteBoleto_API.Tools;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();

#region CorsRules

var CorsRules = "CorsRules";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsRules,
        policy =>
        {
            policy.WithOrigins("http://localhost:1986",
                "http://localhost:5173",
                "http://localhost:7221",
                 "https://sae-gestion-two.vercel.app",
                "http://tidsrv1.tid.frc.utn.edu.ar",
                "https://tidsrv1.tid.frc.utn.edu.ar") // Tus puertos de React
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials(); // Si tu app usa cookies/sesiones
        });
});

#endregion

#region JWT configuration

builder.Configuration.AddJsonFile("appsettings.json");
// Configura Kestrel para escuchar en el puerto definido en appsettings.json
//builder.WebHost.ConfigureKestrel(serverOptions =>
//{
//    serverOptions.ListenAnyIP(5000); // Puedes cambiar el puerto aquí si lo necesitas
//});
var version = builder.Configuration.GetSection("Version");

//Estos datos son los mismos siempre
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
//Esta escondido
string? secretKey = builder.Configuration["JwtSettings:JwtKey"];
string? ssoKey = builder.Configuration["SsoApiKey"];
//Devolvemos error cuando la clave secreta no existe
if (string.IsNullOrEmpty(secretKey) || string.IsNullOrEmpty(ssoKey)) {throw new Exception("Las claves secretas no se encontraron.");}

byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
//Las guardamos para usar despues
SettingsReader.GetAppSettings().JwtSettings.SecretKey = keyBytes;
SettingsReader.GetAppSettings().Sso_api_key = ssoKey;
string enviroment = SettingsReader.GetAppSettings().Environment;

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
        ValidAudience = jwtSettings.GetValue<string>("Audience"),
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
    };
});

builder.Services.AddAuthorization(options =>
{
    // Si estamos en TEST no controla el Token
    if (enviroment == "DESA")
    {
        options.FallbackPolicy = new AuthorizationPolicyBuilder()
            .RequireAssertion(_ => true) // Permite el paso a todos siempre
            .Build();

        // También dejo que la política por defecto sea permisiva
        options.DefaultPolicy = options.FallbackPolicy;
    }
    else
    {
        // En el servidor de DESA y PROD va a pedir token
        options.DefaultPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();
    }
});
//Esto es necesario para que nos permita usar un Token en las funciones de la aplicacion.
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API SAE", Version = "v2" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Por favor loguear su Token usando las siguientes instrucciones
                      Ingresa 'Bearer'+[Espacio]+Token en la caja de texto abajo
                      Sino posees uno es necesario que lo generes con el EndPoint: 'api/Usuario/ObtenerTokenJWT/{legajo}'
                      Ejemplo: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
        });

    // ESTO ES PARA QUE LEA LA DOCUMENTACION EN EL CODIGO XML
    // Set the comments path for the Swagger JSON and UI.
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});
#endregion


// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Apenas inicia el programa el logger verifica donde guardar los datos
Logger.DefinirDirectorios();
//Aca permitimos que GestorToken acceda a servicios asociados a nuestro builder
builder.Services.AddScoped<GestorToken>();

var app = builder.Build();
// Configure the HTTP request pipeline.
// Habilitar Swagger para desarrollo (O quitar el IF si REALMENTE lo quieres en producción)
if (enviroment != "PROD")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Lo saque porque sino se hacia imposible en Linux
//app.UseHttpsRedirection();

//Esto es para utilizar los CorsRules Configuradas
app.UseCors(CorsRules);
app.Use(async (context, next) =>
{
    if (context.Request.Method == "OPTIONS" &&
    context.Request.Headers.ContainsKey("Access-Control-Request-Private-Network"))
    {
        context.Response.Headers.Append("Access-Control-Allow-Private-Network", "true");
    }
    await next();
});
//Se debe agregar despues de la configuracion para que utilice JWT
app.UseMiddleware<JwtBlacklistMiddleware>(); //Habilita el uso de lista negra para tokens
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
