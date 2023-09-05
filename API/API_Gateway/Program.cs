using API_Gateway.HttpServices.Inventory;
using API_Gateway.HttpServices.Inventory.Interfaces;
using API_Gateway.HttpServices.Trolley;
using API_Gateway.HttpServices.Trolley.Interfaces;
using API_Gateway.Services.Inventory;
using API_Gateway.Services.Inventory.Interfaces;
using API_Gateway.Services.Trolley;
using API_Gateway.Services.Trolley.Interfaces;
using Business.Inventory.Http;
using Business.Inventory.Http.Interfaces;
using Business.Libraries.ServiceResult;
using Business.Libraries.ServiceResult.Interfaces;
using Business.Middlewares;
using Business.Trolley.Http;
using Business.Trolley.Http.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IServiceResultFactory, ServiceResultFactory>();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<ICatalogueProductService, CatalogueProductService>();
builder.Services.AddScoped<IProductPriceService, ProductPriceService>();
builder.Services.AddScoped<ITrolleyService, TrolleyService>();
builder.Services.AddScoped<ITrolleyProductService, TrolleyProductService>();
builder.Services.AddScoped<ITrolleyPromotionService, TrolleyPromotionService>();

builder.Services.AddScoped<IHttpCatalogueProductService, HttpCatalogueProductService>();
builder.Services.AddScoped<IHttpProductPriceService, HttpProductPriceService>();
builder.Services.AddScoped<IHttpTrolleyService, HttpTrolleyService>();
builder.Services.AddScoped<IHttpTrolleyProductService, HttpTrolleyProductService>();
builder.Services.AddScoped<IHttpTrolleyPromotionService, HttpTrolleyPromotionService>();


builder.Services.AddHttpClient<IHttpCatalogueProductClient, HttpCatalogueProductClient>(client => {
    client.BaseAddress = new Uri(builder.Configuration.GetSection("RemoteServices:InventoryService").Value ?? "");
});
builder.Services.AddHttpClient<IHttpProductPriceClient, HttpProductPriceClient>(client => {
    client.BaseAddress = new Uri(builder.Configuration.GetSection("RemoteServices:InventoryService").Value ?? "");
});
builder.Services.AddHttpClient<IHttpTrolleyClient, HttpTrolleyClient>(client => {
    client.BaseAddress = new Uri(builder.Configuration.GetSection("RemoteServices:TrolleyService").Value ?? "");
});
builder.Services.AddHttpClient<IHttpTrolleyProductClient, HttpTrolleyProductClient>(client => {
    client.BaseAddress = new Uri(builder.Configuration.GetSection("RemoteServices:TrolleyService").Value ?? "");
});
builder.Services.AddHttpClient<IHttpTrolleyPromotionClient, HttpTrolleyPromotionClient>(client => {
    client.BaseAddress = new Uri(builder.Configuration.GetSection("RemoteServices:TrolleyService").Value ?? "");
});



builder.Services.AddTransient<IServiceResultFactory, ServiceResultFactory>();



builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("https://localhost:4001", "http://localhost:4000", "http://localhost:4200")
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

app.Run();
