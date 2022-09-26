using Common.Enum;
using Common.Models;
using Common.Models.Requests;
using Common.Models.Responses;
using Floward.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Floward.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        #region Declarations
        private readonly string _baseUrl = "https://localhost:7163/";
        #endregion

        #region Implementation
        [HttpPost]
        [Route("addProduct")]
        public async Task<object> CreateNewProduct(NewProductRequest product)
        {
            dynamic response;
            try
            {
                using (var _httpClient = new Common.Communication.HttpCaller<GeneralResult<bool, GeneralStatus>, NewProductRequest>(_baseUrl)) {
                    response = await _httpClient.HttpCallAsync("/api/Products/addProduct", new Dictionary<string, string>(), Common.Enum.HttpVerbs.Post, product);
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("getProduct")]
        public async Task<object> GetProduct(long id, bool loadImage = false){
            dynamic response=0;
            try{
                using (var _httpClient = new Common.Communication.HttpCaller<GeneralResult<ProductResponse, GeneralStatus>, NewProductRequest>(_baseUrl)){
                    var queryStrings = new KeyValuePair<string, string>[] {
                        new KeyValuePair<string, string>("id", id.ToString())
                    };
                    var Target = _httpClient.GenerateQueryString("/api/Products/getProduct", queryStrings);
                    response = await _httpClient.HttpCallAsync(Target, new Dictionary<string, string>(), Common.Enum.HttpVerbs.Get, null);
                }
                return response;
            }
            catch (Exception){
                throw;
            }
        }

        [HttpGet]
        [Route("getAllProducts")]
        public async Task<object> GetAllProducts()
        {
            dynamic response;
            try
            {
                using (var _httpClient = new Common.Communication.HttpCaller<GeneralResult<bool, GeneralStatus>, NewProductRequest>(_baseUrl))
                {
                    response = await _httpClient.HttpCallAsync("/api/Products/getAllProducts", new Dictionary<string, string>(), Common.Enum.HttpVerbs.Get, null);
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("deleteProduct")]
        public async Task<object> DeleteProduc(long id)
        {
            dynamic response;
            try
            {
                using (var _httpClient = new Common.Communication.HttpCaller<GeneralResult<bool, GeneralStatus>, long>(_baseUrl))
                {
                    var queryStrings = new KeyValuePair<string, string>[] {
                        new KeyValuePair<string, string>("id", id.ToString())
                    };
                    var Target = _httpClient.GenerateQueryString("/api/Products/deleteProduct", queryStrings);
                    response = await _httpClient.HttpCallAsync(Target, new Dictionary<string, string>(), Common.Enum.HttpVerbs.Delete, 0);
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("editProduct")]
        public async Task<object> EditProduct(EditProductRequest product)
        {
            dynamic response;
            try
            {
                using (var _httpClient = new Common.Communication.HttpCaller<GeneralResult<bool, GeneralStatus>, EditProductRequest>(_baseUrl))
                {
                    response = await _httpClient.HttpCallAsync("/api/Products/editProduct", new Dictionary<string, string>(), Common.Enum.HttpVerbs.Post, product);
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        } 
        #endregion
    }
}
