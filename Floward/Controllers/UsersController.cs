using Common.Enum;
using Common.Models;
using FlowardBusiness.BusinessContract;
using FlowardBusiness.Models;
using Microsoft.AspNetCore.Mvc;

namespace Floward.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Declarations
        private readonly IUsersBusiness _usersBusiness;
        #endregion

        #region Constructor
        public UsersController(IUsersBusiness usersBusiness)
        {
            _usersBusiness = usersBusiness;
        }
        #endregion

        #region Controller Actions
        [HttpPost]
        [Route("registerUser")]
        public async Task<GeneralResult<bool, GeneralStatus>> CreateUser(UserCreationRequest request){
            return await _usersBusiness.CreateNewUser(request);
        }

        [HttpPost]
        [Route("login")]
        public async Task<GeneralResult<TokenResult, GeneralStatus>> UserLogin(LoginRequest request) {
            return await _usersBusiness.AuthenticateUser(request);
        }
        #endregion
    }
}
