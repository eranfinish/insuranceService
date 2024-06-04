using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using DAL;
using DAL.Repositories;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
         builder => builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod());
});
// Add services to the container.
builder.Services.AddControllers()
     .AddJsonOptions(options =>
      {
          options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
          options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
      });
// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

// Configure SQLite
//var connectionString = "Data Source=InsuranceApi.db";
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlite(connectionString));

// Register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IInsurancePolicyRepository, InsurancePolicyRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
// Enable CORS
app.UseCors("AllowAll");
app.Run();
