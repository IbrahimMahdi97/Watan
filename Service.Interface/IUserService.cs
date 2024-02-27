using System.Collections;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Interface;

public interface IUserService
{
    Task<UserDetailsDto> GetById(int id);
    Task<UserDto> ValidateUser(UserForAuthenticationDto userForAuth);
    Task<int> CreateUser(UserForCreationDto userForCreationDto, int userId);
    Task<TokenDto> RefreshToken(TokenDto tokenDto);
    Task UpdateRating(UserRatingForUpdateDto userRatingForUpdateDto);
    Task<PagedList<UserForListingDto>> GetByParameters(UsersParameters parameters);
    Task<UserHierarchyDto> GetHierarchy();
    Task Update(UserForCreationDto userForCreationDto, int userId);
    Task<int> GetCountByProvinceIdAndTownId(int provinceId, int townId);
    Task<UsersCountDto> GetCountFromDateToDate(DateTime fromDate, DateTime toDate);
    Task AddChildren(int id, IEnumerable<UserChildForManipulation> children);
}