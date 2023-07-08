using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Domain.Enums;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HR.LeaveManagement.Identity.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtSetting _jwtSetting;

    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSetting> jwtSetting)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtSetting = jwtSetting.Value;
    }
    public async Task<AuthResponse> Login(AuthRequest request)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            //throw new NotFoundException($"User with {request.Email} not found.", request.Email);
            return new AuthResponse()
            {
                Errors = { $"User with {request.Email} not found." },
                HasError = true
            };
        }

        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (!result.Succeeded)
        {
            //throw new BadRequestException($"Credentials for '{request.Email} aren't valid'.");
            return new AuthResponse()
            {
                Errors = { $"Credentials for '{request.Email} aren't valid'." },
                HasError = true
            };
        }

        JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

        AuthResponse response = new()
        {
            Id = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email,
            UserName = user.UserName
        };

        return response;
    }

    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {
        IList<Claim> userClaims = await _userManager.GetClaimsAsync(user);
        IList<string> roles = await _userManager.GetRolesAsync(user);
        List<Claim> roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

        IEnumerable<Claim> claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id)
        }
        .Union(userClaims)
        .Union(roleClaims);

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_jwtSetting.Key));

        SigningCredentials signingCredentials = new(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new(
                       issuer: _jwtSetting.Issuer, //owner of the token
                       audience: _jwtSetting.Audience,//intended recipient
                       claims: claims,//request for access
                       expires: DateTime.UtcNow.AddMinutes(_jwtSetting.DurationInMinutes),//time for which token is valid
                       signingCredentials: signingCredentials); //signing credentials

        return token;
    }

    public async Task<RegistrationResponse> Register(RegistrationRequest request)
    {
        ApplicationUser user = new()
        {
            Email = request.Email,
            UserName = request.UserName,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        try
        {
            throw new Exception("An error occurred during registration."); // Exception fırlatma

            ApplicationUser? userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                return new RegistrationResponse()
                {
                    Errors = { $"Username '{request.UserName}' is already taken." },
                    HasError = true
                };
            }
            
            IdentityResult result = _userManager.CreateAsync(user, request.Password).Result;

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Role.Employee.ToString());
                return new RegistrationResponse() { UserId = user.Id };
            }
            else
            {
                List<string> errors = result.Errors.Select(x => x.Description).ToList();
                return new RegistrationResponse() { HasError = true, Errors = errors };
            }
        }
        catch (Exception ex)
        {
            return new RegistrationResponse() { HasError = false, Errors = { ex.Message } };
        }

    }
}