using Business.Filters.Validation;
using Business.Inventory.Http;
using Business.Inventory.Http.Interfaces;
using Business.Libraries.ServiceResult;
using Business.Libraries.ServiceResult.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Inventory.Data;
using Trolley.Data;
using Trolley.Data.Repositories;
using Trolley.Data.Repositories.Interfaces;
using Trolley.HttpServices;
using Trolley.HttpServices.Interfaces;
using Trolley.Services;
using Trolley.Services.Interfaces;
using Trolley.Tools;
using Trolley.Tools.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<ValidationFilter>();
});

builder.Services.AddFluentValidationAutoValidation(opt => opt.DisableDataAnnotationsValidation = true);
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(typeof(ValidationFilter).Assembly);

builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
builder.Services.AddDbContext<TrolleyContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("TrolleyConnStr"), opt => opt.EnableRetryOnFailure()));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ITrolleyService, TrolleyService>();
builder.Services.AddScoped<ITrolleyProductService, TrolleyProductService>();
builder.Services.AddScoped<ITrolleyPromotionService, TrolleyPromotionService>();
builder.Services.AddScoped<ITrolleyRepository,TrolleyRepository>();
builder.Services.AddScoped<ITrolleyProductsRepository, TrolleyProductsRepository>();
builder.Services.AddScoped<ITrolleyPromotionsRepository, TrolleyPromotionsRepository>();
builder.Services.AddScoped<ITools, TrolleyTools>();
builder.Services.AddScoped<IHttpInventoryService, HttpInventoryService>();

builder.Services.AddHttpClient<IHttpProductPriceClient, HttpProductPriceClient>();
builder.Services.AddHttpClient<IHttpProductClient, HttpProductClient>();
builder.Services.AddHttpClient<IHttpCatalogueProductClient, HttpCatalogueProductClient>();

builder.Services.AddSingleton<IServiceResultFactory, ServiceResultFactory>();


var app = builder.Build();


app.MapControllers();


PrepDB.PrepPopulation(app, app.Environment.IsProduction(), app.Configuration);

app.Run();
