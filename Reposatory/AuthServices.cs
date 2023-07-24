using AngularApi.DataBase.Entity;
using AngularApi.Interface;
using AngularApi.Modell;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AngularApi.Reposatory
{
    public class AuthServices : IAuthServices
    {
      private readonly  UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly JWT jwt;

        public AuthServices(UserManager<AppUser> userManager, IOptions<JWT> jwt, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            this.roleManager = roleManager;
            this.jwt = jwt.Value;
        }

        public async Task<AuthModell> Login(LoginVM login)
        {
            var authmodel = new AuthModell();

            var user = await _userManager.FindByEmailAsync(login.Email!);

            if (user is null || !await _userManager.CheckPasswordAsync(user, login.Password!))
            {
                authmodel.Massage = "Email or Password is incorrect";
                return authmodel;
            }

            var jwtSecurityToken = await CreatejwtToken(user);
            var rolelist = await _userManager.GetRolesAsync(user);


            authmodel.IsAuth = true;
            authmodel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authmodel.Email = user.Email;
            authmodel.UserName = user.UserName;
            authmodel.expiration = jwtSecurityToken.ValidTo;
            authmodel.Role = rolelist.ToList();


            return authmodel;
        }

        public async Task<AuthModell> Registration(RegisterVM model)
        {
            if (await  _userManager.FindByEmailAsync(model.Email!) is not null)
                return new AuthModell { Massage = "Email is already Registered" };

            if (await _userManager.FindByNameAsync(model.UserName!) is not null)
                return new AuthModell { Massage = "UserName is already Registered" };

            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            var result = await _userManager.CreateAsync(user, model.Password!);

            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}==";
                }

                return new AuthModell { Massage = errors };
            }

            await _userManager.AddToRoleAsync(user, "User");

            var jwtSecurityToken = await CreatejwtToken(user);
            return new AuthModell
            {
                Email = user.Email,
                expiration = jwtSecurityToken.ValidTo,
                IsAuth = true,
                Role = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = user.UserName,
            };
        }



        public async Task<string> AddRole(RoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user is null || !await roleManager.RoleExistsAsync(model.Role))
            {
                return "Invalid user Id OR Role";
            }
            if (await _userManager.IsInRoleAsync(user, model.Role))
            {
                return "User already assing to this role";
            }

            var result = await _userManager.AddToRoleAsync(user, model.Role);

            return result.Succeeded ? string.Empty : "Somthing was Wroning";

        }



        private async Task<JwtSecurityToken> CreatejwtToken(AppUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email!),
                new Claim("uid",user.Id),
            }.Union(userClaims).Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key!));
            var SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var Jwtsecuritytoken = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(jwt.DurationInDays),
                signingCredentials: SigningCredentials
                );
            return Jwtsecuritytoken;
        }
    }
}
