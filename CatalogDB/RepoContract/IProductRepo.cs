using CatalogDB.DTOs;
using Common.Models.Requests;

namespace CatalogDB.RepoContract
{
    public interface IProductRepo
    {
        Task<Product> AddNewProduct(string Name, decimal Price, decimal Cost, byte[] Image);
        Task<bool> DeleteProduct(long id);
        Task<bool> UpdateProduct(EditProductRequest product);
        Task<Product> GetProductByName(long id);
        Task<List<Product>> GetAllProducts();
    }
}
