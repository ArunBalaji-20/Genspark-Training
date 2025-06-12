using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BugTrackingAPI.Interfaces;
using BugTrackingAPI.Models;
using BugTrackingAPI.Models.DTO;
using Microsoft.IdentityModel.Tokens;

namespace BugTrackingAPI.Services;

public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _securityKey;
    private readonly IRepository<int, RefreshTokenModel> _refreshTokenRepository;


    public TokenService(IConfiguration configuration, IRepository<int, RefreshTokenModel> refreshTokenRepository)
    {
        _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Keys:JwtTokenKey"]));
        _refreshTokenRepository = refreshTokenRepository;
        
    }
    public async Task<TokenResponse> GenerateToken(Employee emp)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim("email", emp.Email),
            new Claim(ClaimTypes.Role,emp.Role)
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


        var refreshToken = GenerateRefreshToken();
        var refreshTokenExpiry = DateTime.UtcNow.AddDays(7); // for example, 7 days valid

        var refreshTokenRecord = new RefreshTokenModel
        {
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = refreshTokenExpiry,
            EmployeeId = emp.EmployeeId
        };

         await _refreshTokenRepository.Add(refreshTokenRecord);

        return new TokenResponse
        {
            AccessToken = tokenHandler.WriteToken(token),
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = refreshTokenExpiry
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
    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

}

