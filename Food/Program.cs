using Food.Databases;
using Food.Services;

var builder = WebApplication.CreateBuilder(args);

// MongoConnection
builder.Services.Configure<MongoConnection>(builder.Configuration.GetSection("MongoSettings"));

// Services
builder.Services.AddSingleton<FoodService>();

// The property names' default camel casing should be changed to match the Pascal casing of the CLR object's property names.
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{

    options.AddPolicy("PoliticaCors",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Cors
app.UseCors("PoliticaCors");

// Add controllers to Swagger
app.MapControllers();

app.Run();
