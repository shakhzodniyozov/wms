using WMS.Application;
using WMS.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication()
                .AddInfrastructure(builder.Configuration);

// builder.Services.AddCors(builder =>
// {
//     builder.AddPolicy("frontend", opt =>
//     {
//         opt.AllowAnyHeader()
//             .AllowAnyOrigin()
//             .AllowAnyMethod();
//     });
// });

var app = builder.Build();

// DbInitializer.Init(app.Services);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();
