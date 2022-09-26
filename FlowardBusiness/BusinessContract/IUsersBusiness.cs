using Common.Enum;
using Common.Models;
using FlowardBusiness.Models;

namespace FlowardBusiness.BusinessContract
{
    public interface IUsersBusiness
    {
        Task<GeneralResult<bool, GeneralStatus>> CreateNewUser(UserCreationRequest request);
        Task<GeneralResult<TokenResult, GeneralStatus>> AuthenticateUser(LoginRequest loginRequest);
    }
}
