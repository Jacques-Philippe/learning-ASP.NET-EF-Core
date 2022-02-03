using ContosoRESTAPI.Data;
using ContosoRESTAPI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConsole();

//Get data source from user secrets
var dataSource = builder.Configuration["Db:DataSource"];

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/**
* The following code says the following:
    - Register PizzaContext with ASP.NET's dependency injection system
    - Specify that PizzaContext will use the Sqlite database provider
    - Define a Sqlite connection string pointing to a local ContosoPizza.db file
*/
builder.Services.AddSqlite<PizzaContext>(dataSource);

builder.Services.AddScoped<PizzaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
