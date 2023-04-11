using AutoMapper;
using DogSitterMarketplaceApi.Models.UsersDto.Response;
using DogSitterMarketplaceApi.Models.UsersDto.Request;
using DogSitterMarketplaceBll.Models.Users.Response;
using DogSitterMarketplaceBll.Models.Users.Request;


namespace DogSitterMarketplaceApi.Mappings
{
    public class MapperApiUserProfile : Profile
    {
        public MapperApiUserProfile()
        {
            CreateMap<UserResponse, UserResponseDto>();
            CreateMap<UserRequestDto, UserRequest>();
            CreateMap<UserUpdateDto, UserUpdate>();
        }
    }
}
