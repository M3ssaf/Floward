using CatalogBusiness.Business;
using CatalogBusiness.BusinessContract;

namespace Test.CatalogApi
{
    public class CatalogProductTest
    {
        #region Declarations
        private readonly IProductBusiness _productBusiness;
        #endregion

        #region Constructor
        public CatalogProductTest(IProductBusiness product)
        {
            _productBusiness = product;
        }
        #endregion
        
        #region Implementation
        [Fact]
        public async void GetProduct()
        {
            var Result = await _productBusiness.GetProduct(9);
            Assert.NotNull(Result.Result);
        }

        [Fact]
        public async void GeAlltProducts()
        {
            var Result = await _productBusiness.GetAllProduct();
            Assert.NotNull(Result.Result);
        }

        [Fact]
        public async void DeleteProduct()
        {
            var Result = await _productBusiness.DeleteProduct(9);
            Assert.Equal(0, Result.StatusCode);
        }
        #endregion
    }
}