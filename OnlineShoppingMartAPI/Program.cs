using BusinessLogicLayer.OrderApi;
using BusinessLogicLayer.ProductApi;
using BusinessLogicLayer.UserApi;
using DataAccessLayer.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OnlineShoppingMartAPI.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Online Shopping Mart API", Version = "v1" });
});
var configuration = builder.Configuration;
builder.Services.AddScoped<IUserLogicApi, UserLogicApi>();
builder.Services.AddScoped<IProductLogicApi, ProductLogicApi>();
builder.Services.AddScoped<IOrderLogicApi, OrderLogicApi>();
builder.Services.AddDbContext<OSMDBContext>(
    options => 
    options.UseSqlServer(configuration.GetConnectionString("DBConnectionString"))
    );
Configuration.CryptoKey = configuration.GetValue<string>("CryptoKey");
Configuration.ApiUserName = configuration.GetValue<string>("ApiUserName");
Configuration.ApiPassword = configuration.GetValue<string>("ApiPassword");
builder.Host.ConfigureLogging(builder => {
    builder.AddLog4Net("log4net.config");
});
builder.Services.AddControllers(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
//app.UseSwaggerUI();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Online Shopping Mart API");
});
//}
app.UseCors(c => { c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });

app.UseAuthorization();

app.MapControllers();

app.Run();
