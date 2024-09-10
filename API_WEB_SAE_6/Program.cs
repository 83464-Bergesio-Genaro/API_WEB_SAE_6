using API_WEB_SAE_6.Logs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
#region CorsRules

var CorsRules = "CorsRules";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsRules,
        builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });

});

#endregion

#region JWT configuration

builder.Configuration.AddJsonFile("appsettings.json");
Logger.DefineDirectorio(builder.Configuration);
// Configura Kestrel para escuchar en el puerto definido en appsettings.json
//builder.WebHost.ConfigureKestrel(serverOptions =>
//{
//    serverOptions.ListenAnyIP(5000); // Puedes cambiar el puerto aquí si lo necesitas
//});
var secretKey = builder.Configuration.GetSection("Settings").GetSection("secretkey").ToString();
var keyBytes = Encoding.UTF8.GetBytes(secretKey ?? "ERROR");

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false
    };
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
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{

//}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service Manager API V1");
});
//Lo saque porque sino se hacia imposible en Linux
//app.UseHttpsRedirection();

//Esto es para utilizar los CorsRules Configuradas
app.UseCors(CorsRules);

//Se debe agregar despues de la configuracion para que utilice JWT
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


app.Run();
