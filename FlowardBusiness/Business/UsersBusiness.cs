using Common.Enum;
using Common.Models;
using FlowardBusiness.BusinessContract;
using FlowardBusiness.Models;
using FlowardDB.DTOs;
using FlowardDB.RepoContract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace FlowardBusiness.Business
{
    public class UsersBusiness : IUsersBusiness
    {
        #region Declarations
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ITokenProvider _tokenProvider;
        private readonly IConfiguration _config;
        private readonly ISessionRepo _userSession;
        private readonly IUsersRepo _usersRepo;
        #endregion

        #region  Constructor
        public UsersBusiness(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            ITokenProvider tokenProvider,
            IConfiguration configuration,
            ISessionRepo sessionRepo,
            IUsersRepo usersRepo)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenProvider = tokenProvider; 
            _config = configuration;
            _userSession = sessionRepo;
            _usersRepo = usersRepo;
        }
        #endregion

        #region Implementation
        public async Task<GeneralResult<TokenResult,GeneralStatus>> AuthenticateUser(LoginRequest loginRequest)
        {
            try
            {
                var SessionJti = Guid.NewGuid();
                #region Get User
                var appUser = await _userManager.FindByNameAsync(loginRequest.Email);
                #endregion
                #region Validate User
                if (appUser is null)
                {
                    return new GeneralResult<TokenResult, GeneralStatus>(GeneralStatus.AppUserNotFound, null);
                }
                if (!appUser.isActive)
                {
                    return new GeneralResult<TokenResult, GeneralStatus>(GeneralStatus.AppUserIsNotActive, null);
                }
                if (appUser.LockoutEnabled)
                {
                    return new GeneralResult<TokenResult, GeneralStatus>(GeneralStatus.AppUserLockedOut, null);
                }
                if (!await _userManager.CheckPasswordAsync(appUser, loginRequest.Password))
                {
                    return new GeneralResult<TokenResult, GeneralStatus>(GeneralStatus.IncorrectPassword, null);
                }
                #endregion
                #region Generate Token
                var AccessToken = _tokenProvider.GenerateAccessToken(_config, appUser, SessionJti);
                #endregion
                #region Create User Session Record
                if (!await _userSession.CreateUserSession(new UserSession { ApplicationUser = appUser, Jti = SessionJti.ToString(), LoginTimeStamp = DateTime.Now, isActive=true }))
                {
                    return new GeneralResult<TokenResult, GeneralStatus>(GeneralStatus.OperationFailed, null);
                } 
                #endregion
                return new GeneralResult<TokenResult, GeneralStatus>(GeneralStatus.OperationSuccessful, AccessToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GeneralResult<bool, GeneralStatus>> CreateNewUser(UserCreationRequest request)
        {
            var _result = new GeneralResult<bool, GeneralStatus>(GeneralStatus.PasswordAndConfirmedPasswordAreNotSame, false);
            try
            {
                if (request.Password != request.confirmedPassword) {
                    return new GeneralResult<bool, GeneralStatus>(GeneralStatus.PasswordAndConfirmedPasswordAreNotSame, false);
                }
                var appUser = new ApplicationUser
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.emailAddress,
                    EmailConfirmed = true,
                    PhoneNumber = request.phoneNumber,
                    PhoneNumberConfirmed = true,
                    UserName = request.emailAddress,
                    isActive = true,
                    CreatedAt = DateTime.Now,
                    CreatedBy = 0
                };
                switch (await _usersRepo.RegisterUser(appUser, request.Password, 1))
                {
                    case 0:
                        _result = new GeneralResult<bool, GeneralStatus>(GeneralStatus.OperationSuccessful, true);
                        break;
                    case 1:
                        _result = new GeneralResult<bool, GeneralStatus>(GeneralStatus.UserRoleNotFound, false);
                        break;
                    case 2:
                        _result = new GeneralResult<bool, GeneralStatus>(GeneralStatus.FailedToCreateUser, false);
                        break;
                    case 3:
                        _result = new GeneralResult<bool, GeneralStatus>(GeneralStatus.EmailAlreadyExists, false);
                        break;
                }
                return _result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
