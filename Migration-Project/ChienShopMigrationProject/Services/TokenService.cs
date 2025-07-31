using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ChienShopMigrationProject.Interfaces;
using ChienVHShopOnline.Models;
using ChienVHShopOnline.Models.Dtos;
using Microsoft.IdentityModel.Tokens;


namespace ChienVHShopOnline.Services;

public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _securityKey;
    // private readonly IRepository<int, RefreshTokenModel> _refreshTokenRepository;


    public TokenService(IConfiguration configuration)
    {
        _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Keys:JwtTokenKey"]));
        
        
    }
    public async Task<TokenResponse> GenerateToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim("username", user.Username),
            new Claim(ClaimTypes.Role,"User")
        };

        var creds = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = creds
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);


      

        return new TokenResponse
        {
            AccessToken = tokenHandler.WriteToken(token)
            
        };
    }
    public DateTime GetExpiryFromToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var expClaim = jwtToken.Payload.Exp;
        if (expClaim == null)
            throw new Exception("Token does not contain an expiry claim.");

        return DateTimeOffset.FromUnixTimeSeconds((long)expClaim).UtcDateTime;
    }
  

}

