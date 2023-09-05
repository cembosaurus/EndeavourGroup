using StaticContent.Services.Interfaces;
using StaticContent.Services;
using Business.Middlewares;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddScoped<IImageFilesService, ImageFilesService>();



builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost:5000")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});



var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();


app.UseCors(opt => {
    opt.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});


app.MapControllers();

app.UseDefaultFiles();

app.UseStaticFiles();

app.Run();

