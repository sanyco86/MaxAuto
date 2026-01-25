using AutoMapper;
using MaxAuto.WebApi.Domain.Entities;
using MaxAuto.WebApi.Domain.Models.Requests;
using MaxAuto.WebApi.Domain.Models.Responses;

namespace MaxAuto.WebApi.Infrastructure.Mapping;

/// <summary>
/// AutoMapper profile for mapping between domain entities and DTOs.
/// </summary>
public class MappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MappingProfile"/> class.
    /// </summary>
    public MappingProfile()
    {
        CreateMap<User, UserResponse>();
        CreateMap<User, CurrentUserResponse>();
        CreateMap<UserRegisterRequest, User>();

    }
}
