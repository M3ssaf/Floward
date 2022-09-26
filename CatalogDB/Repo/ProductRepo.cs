using CatalogDB.DTOs;
using CatalogDB.RepoContract;
using Common.Models.Requests;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace CatalogDB.Repo
{
    public class ProductRepo : IProductRepo
    {
        #region Declarations
        private readonly CatalogDbContext _dbContext;
        #endregion

        #region Constrcutor
        public ProductRepo(CatalogDbContext catalogDb)
        {
            _dbContext = catalogDb;
        }
        #endregion

        #region Implementation
        public async Task<Product> AddNewProduct(string Name, decimal Price, decimal Cost, byte[] Image){
            try{
                if (_dbContext.Products.Any(p => p.Name == Name)) {
                    return null;
                }
                var NewProduct = new Product{
                    Name = Name,
                    Price = Price,
                    Cost = Cost,
                    Base64Image = Image
                };
                _dbContext.Products.Add(NewProduct);
                var result = await _dbContext.SaveChangesAsync();
                if (result <= 0) { return null; }
                return NewProduct;
            }
            catch (Exception){
                throw;
            }
        }

        public async Task<bool> DeleteProduct(long id)
        {
            try
            {
                var ProductToRemove = _dbContext.Products.FirstOrDefault(p => p.Id == id);
                _dbContext.Products.Remove(ProductToRemove);
                var result = await _dbContext.SaveChangesAsync();
                if (result <= 0) { return false; }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                return await _dbContext.Products.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Product> GetProductByName(long id)
        {
            try
            {
                return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateProduct(EditProductRequest product)
        {
            try
            {
                var p =await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
                p.Name = product.Name;
                p.Price = product.Price;
                p.Cost = product.Cost;
                p.Base64Image = Encoding.UTF8.GetBytes(product.Base64Image);
                var result = await _dbContext.SaveChangesAsync();
                if (result <= 0) { return false; }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        } 
        #endregion
    }
}
