using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Entities.Exceptions;
using Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Interface;
using Shared.DataTransferObjects;
using Shared.Helpers;

namespace Service;

internal sealed class UserService : IUserService
{
    
    private readonly IRepositoryManager _repository;
    private readonly IConfiguration _configuration;

    public UserService(IRepositoryManager repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    public async Task<int> CreateUser(UserForCreationDto userForCreationDto, int userId)
    {
        var result = await _repository.User.CreateUser(userForCreationDto);

        if (result <= 0)
            if (userForCreationDto.PhoneNumber != null)
                throw new UserEmailAlreadyExistsBadRequestException(userForCreationDto.PhoneNumber);

        await _repository.User.AddUserRoles(userForCreationDto.Roles, result);

        return result;
    }
    
    public async Task<UserDto> ValidateUser(UserForAuthenticationDto userForAuth)
    {
        var user = await CheckIfUserExists(userForAuth);

        var tokens = new TokenDto();
        user.AccessToken = tokens.AccessToken;
        user.RefreshToken = tokens.RefreshToken;

        user.Roles = await _repository.User.GetUserRoles(user.Id);

        return user;
    }

    public async Task<UserDto> GetById(int id)
    {
        var user = await GetUserAndCheckIfItExists(id);
        user.Roles = await _repository.User.GetUserRoles(user.Id);
            
        user.RefreshToken = string.Empty;
        return user;
    }
    
    private async Task<UserDto> GetUserAndCheckIfItExists(int id)
    {
        var user = await _repository.User.FindById(id);
        if (user is null) throw new UserNotFoundException(id);

        return user;
    }
    
    private async Task<UserDto> CheckIfUserExists(UserForAuthenticationDto dto)
    {
        var id = await _repository.User.FindIdByPhone(dto.PhoneNumber);
        if (id == 0) throw new InvalidCredentialsUnauthorizedException(dto.PhoneNumber);

        var user = await _repository.User.FindByCredentials(dto.PhoneNumber, (dto.Password + id).ToSha512());
        if (user is null) throw new InvalidCredentialsUnauthorizedException(dto.PhoneNumber);
        return user;
    }
    
    private SigningCredentials GetSigningCredentials()
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = Encoding.UTF8.GetBytes(jwtSettings["secretKey"]!);
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }
    
    private async Task<List<Claim>> GetClaims(UserDto user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email!),
            
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var roles = await _repository.Role.GetUserRoles(user.Id);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role.Description!)));

        return claims;
    }
    
    private JwtSecurityToken GenerateTokenOptions
        (SigningCredentials signingCredentials, IEnumerable<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var tokenOptions = new JwtSecurityToken(
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
            signingCredentials: signingCredentials
        );

        return tokenOptions;
    }
    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
    private async Task<TokenDto> CreateToken(UserDto user, bool populateExp)
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims(user);
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
        var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        var refreshToken = user.RefreshToken = GenerateRefreshToken();
        var refreshTokenExpiryTime = populateExp ? DateTime.Now.AddDays(7) : user.RefreshTokenExpiryTime;
        await _repository.User.UpdateRefreshToken(user.Id, refreshToken, refreshTokenExpiryTime);

        return new TokenDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    
}