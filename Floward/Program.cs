using FlowardBusiness.Business;
using FlowardBusiness.BusinessContract;
using FlowardDB;
using FlowardDB.DTOs;
using FlowardDB.Repo;
using FlowardDB.RepoContract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

#region Initialization
var builder = WebApplication.CreateBuilder(args);
ConfigurationManager _config = builder.Configuration;
#endregion

#region Main
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region DBs
builder.Services.AddDbContext<FlowardDbContext>(options => options.UseSqlServer(_config.GetConnectionString("flowardDb")))
    .AddIdentity<ApplicationUser, ApplicationRole>(op =>{
        op.Password = new Microsoft.AspNetCore.Identity.PasswordOptions{
            RequireDigit = true,
            RequiredLength = 1,
            RequiredUniqueChars = 1,
            RequireLowercase = true,
            RequireUppercase = true,
            RequireNonAlphanumeric = false
        };
    })
    .AddEntityFrameworkStores<FlowardDbContext>()
    .AddDefaultTokenProviders();
#endregion

#region Auth
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>{
        options.SaveToken = true;
        options.RequireHttpsMetadata = true;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters{
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidAudience = _config.GetValue<string>("JWT:Audience"),
            ValidIssuer = _config.GetValue<string>("JWT:Issuer"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("JWT:SigningKey")))
        };
    });
#endregion

#region Dependency Injections
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<IUsersBusiness, UsersBusiness>();

builder.Services.AddScoped<IRolesRepo, RolesRepo>();
builder.Services.AddScoped<ISessionRepo, SessionRepo>();
builder.Services.AddScoped<IUsersRepo, UsersRepo>();
#endregion

#region Application Configurations
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run(); 
#endregion