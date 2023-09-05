using Business.Filters.Validation;
using Business.Libraries.ServiceResult;
using Business.Libraries.ServiceResult.Interfaces;
using FluentValidation.AspNetCore;
using Inventory.Services;
using Inventory.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Inventory.Data;
using Services.Inventory.Data.Repositories.Interfaces;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<ValidationFilter>();
});

builder.Services.AddFluentValidation(conf => {
    conf.DisableDataAnnotationsValidation = true;
    conf.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
    conf.AutomaticValidationEnabled = true;
});

builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
builder.Services.AddDbContext<InventoryContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("InventoryConnStr"), opt => opt.EnableRetryOnFailure()));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICatalogueProductService, CatalogueProductService>();
builder.Services.AddScoped<IProductPriceService, ProductPriceService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductPriceRepository, ProductPriceRepository>();
builder.Services.AddScoped<ICatalogueProductRepository, CatalogueProductRepository>();
builder.Services.AddTransient<IServiceResultFactory, ServiceResultFactory>();



var app = builder.Build();

app.MapControllers();

PrepDB.PrepPopulation(app, app.Environment.IsProduction(), app.Configuration);


app.Run();
