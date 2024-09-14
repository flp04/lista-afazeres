using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Modulo.Data;

using Quartz;
using Quartz.Impl;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), 
		new MySqlServerVersion(new Version(8, 0, 21))));

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddControllers();

// builder.Services.AddCors();
builder.Services.AddScoped<AtualizarRegistroJob>();

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
    
    q.ScheduleJob<AtualizarRegistroJob>(trigger => trigger
        .WithIdentity("triggerAtualizarRegistro", "group1")
        .WithCronSchedule("0 10 23 * * ?")); // Cron para 00:00 (meia-noite)
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);


// Adiciona o serviço de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .AllowAnyOrigin()    // Permite qualquer origem (domínio)
                .AllowAnyMethod()    // Permite qualquer método HTTP (GET, POST, etc.)
                .AllowAnyHeader();   // Permite qualquer cabeçalho
        });


	options.AddPolicy("AllowSpecificOrigins",
			builder =>
			{
					builder
							.WithOrigins("http://127.0.0.1:5500/index.html") // Permite apenas estes domínios
							.AllowAnyHeader()
							.AllowAnyMethod();
			});
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

// app.UseCors(c => {
// 								c.AllowAnyOrigin();    // Permite qualquer origem (domínio)
//                 c.AllowAnyMethod();    // Permite qualquer método HTTP (GET, POST, etc.)
//                 c.AllowAnyHeader();   // Permite qualquer cabeçalho	
// });

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();

// Ativa o CORS usando a política definida
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
