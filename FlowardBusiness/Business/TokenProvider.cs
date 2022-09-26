using FlowardBusiness.BusinessContract;
using FlowardBusiness.Models;
using FlowardDB.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlowardBusiness.Business
{
    public class TokenProvider : ITokenProvider
    {
        #region Declarations
        #endregion

        #region Constructor
        #endregion

        #region Implementation
        public TokenResult GenerateAccessToken(IConfiguration _config, ApplicationUser user, Guid jti)
        {
            try
            {
                var SecretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:SigningKey").Value));
                var signingCredentials = new SigningCredentials(SecretKey, SecurityAlgorithms.HmacSha256);
                var tokenOptions = new JwtSecurityToken(
                    issuer: _config.GetSection("JWT.Issuer").Value,
                    audience: _config.GetSection("JWT.Audience").Value,
                    notBefore: DateTime.Now,
                    claims: new List<Claim> {
                        new Claim("AuthType", "Authorize"),
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                        new Claim("session", jti.ToString())
                    },
                    expires: DateTime.Now.AddHours(2), 
                    signingCredentials: signingCredentials
                    );
                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return new TokenResult
                {
                    Token = token,
                    ValidTo = tokenOptions.ValidTo
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public JwtSecurityToken ReadToken(string token)
        {
            var Handler = new JwtSecurityTokenHandler();
            var Parsed = Handler.ReadJwtToken(token);
            return Parsed;
        }
        #endregion
    }
}
