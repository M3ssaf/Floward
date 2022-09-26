using CatalogBusiness.BusinessContract;
using Common.Enum;
using Common.Models;
using Common.Models.Requests;
using Common.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CatalogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        #region Declarations
        private readonly IProductBusiness _productBusiness;
        #endregion

        #region Constructor
        public ProductsController(IProductBusiness productBusiness){
            _productBusiness = productBusiness;
        }
        #endregion

        #region Controller Actions
        [HttpPost]
        [Route("addProduct")]
        public async Task<GeneralResult<bool, GeneralStatus>> CreateNewProduct(NewProductRequest product) {
            try
            {
                return await _productBusiness.AddProduct(product);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("getProduct")]
        public async Task<GeneralResult<ProductResponse, GeneralStatus>> GetProduct(long id, bool loadImage = false) {
            try
            {
                return await _productBusiness.GetProduct(id, loadImage);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("getAllProducts")]
        public async Task<GeneralResult<List<ProductResponse>, GeneralStatus>> GetAllProducts(){
            try
            {
                return await _productBusiness.GetAllProduct();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("deleteProduct")]
        public async Task<GeneralResult<bool, GeneralStatus>> DeleteProduc(long id){
            try
            {
                return await _productBusiness.DeleteProduct(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("editProduct")]
        public async Task<GeneralResult<bool, GeneralStatus>> EditProduct(EditProductRequest product)
        {
            try
            {
                return await _productBusiness.UpdateProduct(product);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
