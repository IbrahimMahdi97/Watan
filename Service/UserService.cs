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
using Shared.RequestFeatures;

namespace Service;

internal sealed class UserService : IUserService
{
    private readonly IRepositoryManager _repository;
    private readonly IFileStorageService _fileStorageService;
    private readonly IConfiguration _configuration;

    public UserService(IRepositoryManager repository, IFileStorageService fileStorageService,
        IConfiguration configuration)
    {
        _repository = repository;
        _fileStorageService = fileStorageService;
        _configuration = configuration;
    }

    public async Task<int> CreateUser(UserForCreationDto userForCreationDto, int userId)
    {
        if (userForCreationDto.UserRegion.RegionId > 0
            || userForCreationDto.UserRegion.TownId > 0
            || userForCreationDto.UserRegion.ProvinceId > 0)
            await IsRegionExist(userForCreationDto.UserRegion.RegionId, userForCreationDto.UserRegion.TownId,
                userForCreationDto.UserRegion.ProvinceId);

        ValidateFields(userForCreationDto);

        var result = await _repository.User.CreateUser(userForCreationDto, userId);

        if (result <= 0)
            if (userForCreationDto.PhoneNumber != null)
                throw new UserPhoneNumberAlreadyExistsBadRequestException(userForCreationDto.PhoneNumber);

        await _repository.User.AddUserRoles(userForCreationDto.Roles, result);
        await _repository.User.AddUserRegion(userForCreationDto.UserRegion, result);
        if (userForCreationDto.UserImage is not null)
            await _fileStorageService.CopyFileToServer(result,
                _configuration["UserImagesSetStorageUrl"]!, userForCreationDto.UserImage);

        return result;
    }

    private static void ValidateFields(UserForManipulationDto userForCreationDto)
    {
        if (userForCreationDto.FullName.Length > 250)
            throw new StringLimitExceededBadRequestException("FullName", 250);
        if (userForCreationDto.MotherName is { Length: > 250 })
            throw new StringLimitExceededBadRequestException("MotherName", 250);
        if (userForCreationDto.ProvinceOfBirth is { Length: > 50 })
            throw new StringLimitExceededBadRequestException("ProvinceOfBirth", 50);
        if (userForCreationDto.PhoneNumber is { Length: > 15 })
            throw new StringLimitExceededBadRequestException("PhoneNumber", 15);
        if (userForCreationDto.EmergencyPhoneNumber is { Length: > 15 })
            throw new StringLimitExceededBadRequestException("EmergencyPhoneNumber", 15);
        if (userForCreationDto.Email is { Length: > 250 })
            throw new StringLimitExceededBadRequestException("Email", 250);

        if (userForCreationDto.District is { Length: > 50 })
            throw new StringLimitExceededBadRequestException("District", 50);
        if (userForCreationDto.StreetNumber is { Length: > 50 })
            throw new StringLimitExceededBadRequestException("StreetNumber", 50);
        if (userForCreationDto.HouseNumber is { Length: > 50 })
            throw new StringLimitExceededBadRequestException("HouseNumber", 50);
        if (userForCreationDto.NationalIdNumber is { Length: > 50 })
            throw new StringLimitExceededBadRequestException("NationalIdNumber", 50);
        if (userForCreationDto.ResidenceCardNumber is { Length: > 50 })
            throw new StringLimitExceededBadRequestException("ResidenceCardNumber", 50);
        if (userForCreationDto.VoterCardNumber is { Length: > 50 })
            throw new StringLimitExceededBadRequestException("VoterCardNumber", 50);
    }

    public async Task<UserDto> ValidateUser(UserForAuthenticationDto userForAuth)
    {
        var user = await CheckIfUserExists(userForAuth);

        var tokens = await CreateToken(user, true);
        user.AccessToken = tokens.AccessToken;
        user.RefreshToken = tokens.RefreshToken;

        user.Roles = await _repository.User.GetUserRoles(user.Id);

        if (!string.IsNullOrEmpty(userForAuth.DeviceId))
            await _repository.User.UpdateDeviceId(user.Id, userForAuth.DeviceId);

        return user;
    }

    public async Task<UserDetailsDto> GetById(int id)
    {
        var users = await GetByParameters(new UsersParameters());

        var userHierarchyDto = users.Select(u => new UserHierarchyDto
        {
            Id = u.Id,
            FullName = u.FullName,
            RoleDescription = u.Roles!.FirstOrDefault()?.Description ?? string.Empty,
            SubordinateManagers = Array.Empty<UserHierarchyDto>(),
            ImageUrl = u.ImageUrl,
            Region = u.Regions!.First()
        }).ToList();

        var hierarchy = userHierarchyDto
            .Find(u => u.Id == id);

        userHierarchyDto = userHierarchyDto.Where(u => u.Id != id).ToList();

        var user = await GetUserAndCheckIfItExists(id);

        if (hierarchy != null)
        {
            BuildHierarchyForUser(hierarchy, userHierarchyDto, "manager");
            user.SubordinateManagers = hierarchy.SubordinateManagers;
        }

        user.Roles = await _repository.User.GetUserRoles(user.Id);
        user.Regions = await _repository.User.GetUserRegions(user.Id);
        user.Children = await _repository.User.GetUserChildren(user.Id);

        var images = _fileStorageService.GetFilesUrlsFromServer(user.Id,
            _configuration["UserImagesSetStorageUrl"]!,
            _configuration["UserImagesGetStorageUrl"]!).ToList();

        user.ImageUrl = images.Any() ? images.First() : "";
        return user;
    }

    private async Task<UserDetailsDto> GetUserAndCheckIfItExists(int id)
    {
        var user = await _repository.User.FindById(id);
        if (user is null) throw new UserNotFoundException(id);

        return user;
    }

    private async Task<UserDto> CheckIfUserExists(UserForAuthenticationDto dto)
    {
        var id = await _repository.User.FindIdByEmailOrPhoneNumber(dto.EmailOrPhoneNumber);
        if (id == 0) throw new InvalidCredentialsEmailOrPhoneNumberUnauthorizedException(dto.EmailOrPhoneNumber);

        var user = await _repository.User.FindByCredentialsEmailOrPhoneNumber(dto.EmailOrPhoneNumber,
            (dto.Password + id).ToSha512());
        if (user is null) throw new InvalidCredentialsEmailOrPhoneNumberUnauthorizedException(dto.EmailOrPhoneNumber);

        var tokens = await CreateToken(user, true);
        user.AccessToken = tokens.AccessToken;
        user.RefreshToken = tokens.RefreshToken;

        user.Roles = await _repository.User.GetUserRoles(user.Id);
        user.Regions = await _repository.User.GetUserRegions(user.Id);
        user.Children = await _repository.User.GetUserChildren(user.Id);
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
            new(ClaimTypes.MobilePhone, user.PhoneNumber!),

            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var roles = await _repository.Role.GetUserRoles(user.Id);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role.Description!)));

        return claims;
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = Encoding.UTF8.GetBytes(jwtSettings["secretKey"]!);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true, ValidateIssuer = true, ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateLifetime = false, ValidIssuer = jwtSettings["validIssuer"],
            ValidAudience = jwtSettings["validAudience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
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

    public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
    {
        var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);
        var userId = Convert.ToInt32(principal.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var user = await _repository.User.FindById(userId);
        if (user == null)
            throw new ExpiredRefreshTokenUnauthorizedException();
        var returnUser = new UserDto
        {
            Id = user.Id,
            FullName = user.FullName,
            DateOfBirth = user.DateOfBirth,
            District = user.District,
            Email = user.Email,
            Gender = user.Gender,
            HouseNumber = user.HouseNumber,
            StreetNumber = user.StreetNumber,
            ImageUrl = user.ImageUrl,
            MotherName = user.MotherName,
            Roles = user.Roles,
            ProvinceOfBirth = user.ProvinceOfBirth,
            NationalIdNumber = user.NationalIdNumber,
            ResidenceCardNumber = user.ResidenceCardNumber,
            VoterCardNumber = user.VoterCardNumber,
            PhoneNumber = user.PhoneNumber,
            RefreshToken = user.RefreshToken ?? "",
            RefreshTokenExpiryTime = user.RefreshTokenExpiryTime ?? DateTime.Now,
        };
        if (returnUser.RefreshToken != tokenDto.RefreshToken ||
            user.RefreshTokenExpiryTime <= DateTime.Now) throw new ExpiredRefreshTokenUnauthorizedException();

        return await CreateToken(returnUser, false);
    }

    public async Task UpdateRating(UserRatingForUpdateDto userRatingForUpdateDto)
    {
        await GetById(userRatingForUpdateDto.UserId);
        await _repository.User.UpdateRating(userRatingForUpdateDto);
    }

    public async Task<PagedList<UserForListingDto>> GetByParameters(UsersParameters parameters)
    {
        var users = await _repository.User.GetByParameters(parameters);

        foreach (var user in users)
        {
            var images = _fileStorageService.GetFilesUrlsFromServer(user.Id,
                _configuration["UserImagesSetStorageUrl"]!,
                _configuration["UserImagesGetStorageUrl"]!).ToList();

            user.ImageUrl = images.Any() ? images.First() : "";
            user.Roles = await _repository.User.GetUserRoles(user.Id);
            user.Regions = await _repository.User.GetUserRegions(user.Id);
        }

        return users;
    }

    public async Task<UserHierarchyDto> GetHierarchy()
    {
        var users = await GetByParameters(new UsersParameters());

        var userHierarchyDto = users.Select(u => new UserHierarchyDto
        {
            Id = u.Id,
            FullName = u.FullName,
            ImageUrl = u.ImageUrl,
            RoleDescription = u.Roles!.FirstOrDefault()?.Description ?? string.Empty,
            SubordinateManagers = Array.Empty<UserHierarchyDto>(),
            Region = u.Regions!.First()
        }).ToList();

        var hierarchy = userHierarchyDto
            .Find(u => u.RoleDescription == "admin");

        BuildHierarchyForUser(hierarchy!, userHierarchyDto, "manager");

        return hierarchy!;
    }

    public async Task Update(UserForCreationDto userForCreationDto, int userId)
    {
        ValidateFields(userForCreationDto);
        userForCreationDto.Password = (userForCreationDto.Password + userId).ToSha512();
        await _repository.User.Update(userForCreationDto, userId);
          _fileStorageService.DeleteFilesFromServer(userId, _configuration["UserImagesSetStorageUrl"]!);

           if (userForCreationDto.UserImage is not null)
             await _fileStorageService.CopyFileToServer(userId,
               _configuration["UserImagesSetStorageUrl"]!, userForCreationDto.UserImage);
    }

    public async Task<int> GetCountByProvinceIdAndTownId(int provinceId, int townId)
    {
        var count = await _repository.User.GetCountByProvinceIdAndTownId(provinceId, townId);
        return count;
    }

    public async Task<UsersCountDto> GetCountFromDateToDate(DateTime fromDate, DateTime toDate)
    {
        var count = await _repository.User.GetCountFromDateToDate(fromDate, toDate);
        return count;
    }

    public async Task AddChildren(int id, IEnumerable<UserChildForManipulation> children)
    {
        await _repository.User.UpdateChildren(children, id);
    }

    private static void BuildHierarchyForUser(UserHierarchyDto manager, List<UserHierarchyDto> allUsers, string role)
    {
        var subordinates = allUsers
            .Where(u => IsSubordinateOf(u, manager, role) && !u.Equals(manager))
            .ToList();

        foreach (var subordinate in subordinates)
        {
            BuildHierarchyForUser(subordinate, allUsers.Except(subordinates).ToList(), role);
        }

        manager.SubordinateManagers = subordinates;
    }

    private static bool IsSubordinateOf(UserHierarchyDto subordinate, UserHierarchyDto manager, string role)
    {
        var roleIsManager = subordinate.RoleDescription.Equals(role);
        var sameProvince = manager.Region.ProvinceId == 0 ||
                           manager.Region.ProvinceId.Equals(subordinate.Region.ProvinceId);
        var sameTown = manager.Region.TownId == 0 || manager.Region.TownId.Equals(subordinate.Region.TownId);

        return roleIsManager & (sameProvince || sameTown);
    }

    private async Task IsRegionExist(int? regionId, int? townId, int? provinceId)
    {
        if (provinceId is > 0)
        {
            var province = await _repository.Province.GetProvinceById(provinceId.Value);
            if (province is null) throw new ProvinceNotFoundException(provinceId.Value);
        }

        if (townId is > 0)
        {
            if (provinceId is not > 0)
            {
                throw new TownWithoutProvinceException(townId.Value);
            }

            var town = await _repository.Town.GetById(townId.Value);
            if (town is null) throw new TownNotFoundException(townId.Value);
            if (provinceId.HasValue && town.ProvinceId != provinceId.Value)
            {
                throw new InvalidTownProvinceException(townId.Value, provinceId.Value);
            }
        }

        if (regionId is > 0)
        {
            if (townId is not > 0)
            {
                throw new RegionWithoutTownException(regionId.Value);
            }

            var region = await _repository.Region.GetById(regionId.Value);
            if (region is null) throw new RegionNotFoundException(regionId.Value);
            if (townId.HasValue && region.TownId != townId.Value)
            {
                throw new InvalidRegionTownException(regionId.Value, townId.Value);
            }
        }
    }
}