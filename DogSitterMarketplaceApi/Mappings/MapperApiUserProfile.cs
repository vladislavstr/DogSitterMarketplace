using AutoMapper;
using DogSitterMarketplaceApi.Models.PetsDto.Request;
using DogSitterMarketplaceApi.Models.UsersDto.Request;
using DogSitterMarketplaceApi.Models.UsersDto.Response;
using DogSitterMarketplaceApi.Models.WorksDto.Response;
using DogSitterMarketplaceBll.Models.Pets.Request;
using DogSitterMarketplaceBll.Models.Users.Request;
using DogSitterMarketplaceBll.Models.Users.Response;
using DogSitterMarketplaceBll.Models.Works.Response;

namespace DogSitterMarketplaceApi.Mappings
{
    public class MapperApiUserProfile : Profile
    {
        public MapperApiUserProfile()
        {
            //User 
            CreateMap<UserResponse, UserResponseDto>();
            CreateMap<UserRequestDto, UserRequest>();

            //PassportData 
            CreateMap<UserPassportDataResponse, UserPassportDataResponseDto>();
            CreateMap<UserPassportDataRequestDto, UserPassportDataRequest>();

            //Role 
            CreateMap<UserRoleResponse, UserRoleResponseDto>();
            CreateMap<UserRoleRequestDto, UserRoleRequest>();

            //Status 
            CreateMap<UserStatusRequestDto, UserStatusResponseDto>();
            CreateMap<UserStatusResponse, UserStatusResponseDto>();

            //All 
            CreateMap<PetRequestDto, PetRequest>().ReverseMap();
            CreateMap<UserUpdateDto, UserUpdate>().ReverseMap();
            CreateMap<UserShortLocationWorkResponse, UserShortLocationWorkResponseDto>();
            CreateMap<WorkTypeResponse, WorkTypeResponseDto>();
            CreateMap<WorkTypePriceResponse, WorkTypePriceResponseDto>();
        }
    }
}
