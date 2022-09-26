using FlowardBusiness.Models;
using FlowardDB.DTOs;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace FlowardBusiness.BusinessContract
{
    public interface ITokenProvider
    {
        TokenResult GenerateAccessToken(IConfiguration _config, ApplicationUser user, Guid jti);
        JwtSecurityToken ReadToken(string token);
    }
}
