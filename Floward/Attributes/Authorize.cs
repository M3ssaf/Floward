using FlowardBusiness.Business;
using FlowardBusiness.BusinessContract;
using FlowardDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace Floward.Attributes
{
    public class Authorize : Attribute, IAuthorizationFilter
    {
        #region Declarations
        private readonly ITokenProvider _tokenProvider = new TokenProvider();
        private readonly string sp_ValidateUserPermission = "sp_ValidateUserPermission";
        #endregion

        #region Implementation
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var auth = context.HttpContext.Request.Headers[HeaderNames.Authorization];
                string Jti = string.Empty;
                int UserId;
                if (AuthenticationHeaderValue.TryParse(auth, out var headerValue))
                {
                    var xx = _tokenProvider.ReadToken(headerValue.Parameter);
                    #region validating Token Type
                    if (!xx.Claims.FirstOrDefault(c => c.Type.Equals("AuthType")).Value.Equals("Authorize"))
                    {
                        context.Result = new UnauthorizedResult();
                    }
                    if (string.IsNullOrEmpty(xx.Claims.FirstOrDefault(c => c.Type.Equals("session")).Value))
                    {
                        context.Result = new UnauthorizedResult();
                    }
                    Jti = xx.Claims.FirstOrDefault(c => c.Type.Equals("session")).Value;
                    UserId = int.Parse(xx.Claims.FirstOrDefault(x => x.Type.Equals(JwtRegisteredClaimNames.Sub)).Value);
                    #endregion
                    #region Validating Token Expiry
                    if (xx.ValidTo < DateTime.UtcNow)
                    {
                        context.Result = new UnauthorizedResult();
                    }
                    #endregion
                    #region Validating Session Expiry & Permission to Hit Target Endpoint
                    var _db = (context.HttpContext.RequestServices.GetService(typeof(FlowardDbContext)) as FlowardDbContext);
                    var CurrentSession = _db.UserSessions.FirstOrDefault(s => s.Jti.Equals(Jti) && s.ApplicationUserId == UserId);
                    if (CurrentSession is null || !CurrentSession.isActive)
                    {
                        context.Result = new UnauthorizedResult();
                    }
                    #endregion
                    #region Check for Permission using UserId and URL
                    var url = context.HttpContext.Request.Path.Value.Split("/");
                    var path = url[1] + "/" + url[2] + "/" + url[3];
                    var isAllowed = false;
                    var details = _db.UserPermissions.FromSqlInterpolated($"{sp_ValidateUserPermission} {UserId}, {path}").ToList();
                    if (details.Count() > 0) isAllowed = true;
                    if (isAllowed == false)
                    {
                        context.Result = new UnauthorizedResult();
                    }
                    #endregion
                }
                else
                {
                    context.Result = new UnauthorizedResult();
                }
            }
            catch (Exception ex)
            {
                context.Result = new ForbidResult();
            }
        } 
        #endregion
    }
}
