using CatalogDB.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogDB
{
    public class CatalogDbContext:DbContext
    {
        #region Constructor
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options)
            : base(options)
        {
        }

        //public CatalogDbContext()
        //{

        //}
        #endregion

        #region Datasets
        public DbSet<Product> Products { get; set; }
        #endregion

        #region Overrides
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=LAPTOP-MQA4LEOG\\MSSQLSERVER01;Database=dbCatalog;user id=sa;password=m_01004392068;Trusted_Connection=True;");
        //    }
        //}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        #endregion
    }
}
