using DAL;
using DAL.Interfaces;
using DAL.Repositories;

using LN.Interfaces;
using LN.Services;

using Microsoft.EntityFrameworkCore;

// Crear el constructor de la aplicación web.
var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor de dependencias.
builder.Services.AddControllers();

// Agregar configuración de Swagger/OpenAPI para la documentación de la API.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Obtener la cadena de conexión desde la configuración.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configurar el contexto de base de datos con Entity Framework Core.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseSqlServer(connectionString);
});

// Registrar los servicios necesarios en el contenedor de dependencias.
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

// Construir la aplicación.
var app = builder.Build();

// Configurar la canalización de solicitudes HTTP.
if (app.Environment.IsDevelopment())
{
	// Habilitar Swagger en el entorno de desarrollo para la documentación.
	app.UseSwagger();
	app.UseSwaggerUI();

	// Mostrar una página de excepciones detalladas en desarrollo.
	app.UseDeveloperExceptionPage();
}

// Redireccionar las solicitudes HTTP a través de HTTPS.
app.UseHttpsRedirection();

// Habilitar la autorización.
app.UseAuthorization();

// Mapear las rutas de controladores.
app.MapControllers();

// Iniciar la aplicación.
app.Run();
