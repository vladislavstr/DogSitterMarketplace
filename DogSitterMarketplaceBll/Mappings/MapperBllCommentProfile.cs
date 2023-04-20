using AutoMapper;
using DogSitterMarketplaceBll.Models.Orders.Request;
using DogSitterMarketplaceBll.Models.Orders.Response;
using DogSitterMarketplaceBll.Models.Users.Response;
using DogSitterMarketplaceDal.Models.Orders;
using DogSitterMarketplaceDal.Models.Users;

namespace DogSitterMarketplaceBll.Mappings
{
    public class MapperBllCommentProfile : Profile
    {
        public MapperBllCommentProfile() 
        {
            CreateMap<CommentEntity, CommentOrderResponse>();
            CreateMap<CommentRequest, CommentEntity>();
            CreateMap<CommentUpdate, CommentEntity>();
            CreateMap<CommentEntity, CommentAboutSitterForClientResponse>();
            CreateMap<CommentEntity, CommentAboutClientsForSitterResponse>();
            CreateMap<CommentEntity, CommentWithoutUserResponse>();
            CreateMap<UserEntity, UserForCommentResponse>();
        }
    }
}
