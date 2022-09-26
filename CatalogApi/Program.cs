#region Initialization
using CatalogBusiness.Business;
using CatalogBusiness.BusinessContract;
using CatalogDB;
using CatalogDB.Repo;
using CatalogDB.RepoContract;
using Common.Helper;
using Common.HelperContract;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager _config = builder.Configuration;
#endregion

#region Main
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region DBs
builder.Services.AddDbContext<CatalogDbContext>(options => options.UseSqlServer(_config.GetConnectionString("CatalogDb")));
#endregion

#region Dependency Injection
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IProductBusiness, ProductBusiness>();

builder.Services.AddScoped<IProductRepo, ProductRepo>();

builder.Services.AddScoped<IRabbitMQHelper, RabbitMQHelper>();
#endregion

#region Application Configurations
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
#endregion