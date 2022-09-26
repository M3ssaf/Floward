using CatalogBusiness.BusinessContract;
using CatalogDB.DTOs;
using CatalogDB.RepoContract;
using Common.Enum;
using Common.HelperContract;
using Common.Models;
using Common.Models.Requests;
using Common.Models.Responses;
using Newtonsoft.Json;
using System.Linq;
using System.Text;

namespace CatalogBusiness.Business
{
    public class ProductBusiness : IProductBusiness
    {
        #region Declarations
        private readonly IProductRepo _product;
        private readonly IRabbitMQHelper _rabbitmqHelper;
        
        #endregion

        #region Constructor
        public ProductBusiness(IProductRepo productRepo,
            IRabbitMQHelper rabbitMQHelper)
        {
            _product = productRepo;
            _rabbitmqHelper = rabbitMQHelper;
        }
        #endregion

        #region Implementation
        public async Task<GeneralResult<bool, GeneralStatus>> AddProduct(NewProductRequest product){
            try{
                byte[] imgBase64 = Encoding.UTF8.GetBytes(product.Base64Image);
                var result = await _product.AddNewProduct(product.Name, product.Price, product.Cost, imgBase64);
                if (result is null) {
                    return new GeneralResult<bool, GeneralStatus>(GeneralStatus.FailedToAddProduct, false);
                }
                _rabbitmqHelper.EnqueueProductMailAnnouncement(result.Id.ToString());
                return new GeneralResult<bool, GeneralStatus>(GeneralStatus.OperationSuccessful, true);
            }
            catch (Exception){
                throw;
            }
        }

        public async Task<GeneralResult<bool, GeneralStatus>> DeleteProduct(long id)
        {
            try
            {
                var result = await _product.DeleteProduct(id);
                if (!result) {
                    return new GeneralResult<bool, GeneralStatus>(GeneralStatus.FailedToDeleteProduct, false);
                }
                return new GeneralResult<bool, GeneralStatus>(GeneralStatus.OperationSuccessful, true);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GeneralResult<List<ProductResponse>, GeneralStatus>> GetAllProduct(bool loadImage = false)
        {
            try
            {
                var response = new List<ProductResponse>();
                var result = await _product.GetAllProducts();
                if (result.Count <= 0) {
                    return new GeneralResult<List<ProductResponse>, GeneralStatus>(GeneralStatus.NoProductsWereFound, null);
                }
                result.ForEach(p =>{
                    response.Add(new ProductResponse{
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        Cost = p.Cost,
                        Base64Image = loadImage ? Encoding.UTF8.GetString(p.Base64Image) : string.Empty
                    });
                });
                return new GeneralResult<List<ProductResponse>, GeneralStatus>(GeneralStatus.OperationSuccessful, response);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GeneralResult<ProductResponse, GeneralStatus>> GetProduct(long id, bool loadImage = false){
            try{
                var result = await _product.GetProductByName(id);
                if (result is null){
                    return new GeneralResult<ProductResponse, GeneralStatus>(GeneralStatus.NoProductsWereFound, null);
                }
                return new GeneralResult<ProductResponse, GeneralStatus>(GeneralStatus.OperationSuccessful,
                    new ProductResponse{
                        Id = result.Id,
                        Name = result.Name,
                        Price = result.Price,
                        Cost = result.Cost,
                        Base64Image = loadImage ? Encoding.UTF8.GetString(result.Base64Image) : string.Empty
                    });
            }
            catch (Exception){
                throw;
            }
        }

        public async Task<GeneralResult<bool, GeneralStatus>> UpdateProduct(EditProductRequest product){
            try{
                var result = await _product.UpdateProduct(product);
                if (!result) {
                    return new GeneralResult<bool, GeneralStatus>(GeneralStatus.FailedToUpdateProduct, false);
                }
                return new GeneralResult<bool, GeneralStatus>(GeneralStatus.OperationSuccessful, true);
            }
            catch (Exception)
            {
                throw;
            }
        } 
        #endregion
    }
}
