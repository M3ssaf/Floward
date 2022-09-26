using CatalogBusiness.Business;
using CatalogBusiness.BusinessContract;
using CatalogDB;
using CatalogDB.Repo;
using CatalogDB.RepoContract;
using Common.Helper;
using Common.HelperContract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Test.CatalogApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CatalogDbContext>(options => options.UseSqlServer("Server=LAPTOP-MQA4LEOG\\MSSQLSERVER01;Database=dbCatalog;user id=sa;password=m_01004392068;Trusted_Connection=True;"));
            services.AddTransient<IProductBusiness, ProductBusiness>();
            services.AddTransient<IProductRepo, ProductRepo>();
            services.AddTransient<IRabbitMQHelper, RabbitMQHelper>();
        }
    }
}
