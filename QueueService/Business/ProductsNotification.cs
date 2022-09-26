using Common.Enum;
using Common.Models.Requests;
using Common.Models;
using Common.Models.Responses;
using QueueService.BusinessContract;

namespace QueueService.Business
{
    public class ProductsNotification : IProductsNotification
    {
        #region Declarations
        private readonly string _baseUrl = "https://localhost:7163/";
        #endregion

        #region Constructor
        #endregion

        #region Implementation
        public async Task<ProductResponse> GetProduct(long id)
        {
            dynamic response = null;
            try
            {
                using (var _httpClient = new Common.Communication.HttpCaller<GeneralResult<ProductResponse, GeneralStatus>, NewProductRequest>(_baseUrl))
                {
                    var queryStrings = new KeyValuePair<string, string>[] {
                        new KeyValuePair<string, string>("id", id.ToString()),
                        new KeyValuePair<string, string>("loadImage", "true")
                    };
                    var Target = _httpClient.GenerateQueryString("/api/Products/getProduct", queryStrings);
                    response = await _httpClient.HttpCallAsync(Target, new Dictionary<string, string>(), Common.Enum.HttpVerbs.Get, null);
                }
                return (response as GeneralResult<ProductResponse, GeneralStatus>).Result;
            }
            catch (Exception)
            {
                throw;
            }
        } 
        #endregion
    }
}
