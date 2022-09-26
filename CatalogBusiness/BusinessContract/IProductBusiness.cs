using CatalogDB.DTOs;
using Common.Enum;
using Common.Models;
using Common.Models.Requests;
using Common.Models.Responses;

namespace CatalogBusiness.BusinessContract
{
    public interface IProductBusiness
    {
        Task<GeneralResult<bool, GeneralStatus>> AddProduct(NewProductRequest product);
        Task<GeneralResult<bool, GeneralStatus>> UpdateProduct(EditProductRequest product);
        Task<GeneralResult<bool, GeneralStatus>> DeleteProduct(long id);
        Task<GeneralResult<ProductResponse, GeneralStatus>> GetProduct(long id, bool loadImage = false);
        Task<GeneralResult<List<ProductResponse>, GeneralStatus>> GetAllProduct(bool loadImage = false);
    }
}
