using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace SlippAPI.Services;

public class JwtService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<DatabaseUser> _userManager;

    public JwtService(IConfiguration configuration, UserManager<DatabaseUser> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    public async Task<string> GenerateJwtToken(DatabaseUser dbUser)
    {
        var key = Encoding.ASCII.GetBytes(_configuration["JwtConfig:Secret"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, dbUser.Id),
                new Claim(ClaimTypes.Name, dbUser.NormalizedEmail)
                //TODO:Check what else is fitting to have
            }),
            // Hur länge token ska vara giltlig
            Expires = DateTime.UtcNow.AddHours(6),

            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var roles = await _userManager.GetRolesAsync(dbUser);
        if (roles is not null)
        {
            foreach (var role in roles)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role));
            }
        }

        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var token = jwtTokenHandler.CreateToken(tokenDescriptor);

        var jwtEncryptedToken = jwtTokenHandler.WriteToken(token);
        return jwtEncryptedToken;
    }
}