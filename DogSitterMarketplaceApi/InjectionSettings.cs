using DogSitterMarketplaceApi.Mappings;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Mappings;
using DogSitterMarketplaceBll.Services;
using DogSitterMarketplaceDal.Contexts;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Repositories;

namespace DogSitterMarketplaceApi
{
    public class InjectionSettings
    {
        public InjectionSettings(WebApplicationBuilder? builder)
        {
            builder.Services.AddAutoMapper(typeof(MapperApiOrderProfile), typeof(MapperBllOrderProfile),
                typeof(MapperApiPetProfile), typeof(MapperBllPetProfile),
                typeof(MapperApiCommentProfile), typeof(MapperBllCommentProfile),
                typeof(MapperApiWorkProfile), typeof(MapperBllWorkProfile),
                typeof(MapperApiUserProfile), typeof(MapperBllUserProfile),
                typeof(MapperApiAppealProfile), typeof(MapperBllAppealProfile));

            builder.Services.AddSingleton<DogSitterMarketplaceContext>();
            builder.Services.AddSingleton<AppealContext>();
            builder.Services.AddSingleton<OrdersAndPetsAndCommentsContext>();
            builder.Services.AddSingleton<WorkContext>();
            builder.Services.AddSingleton<UserContext>();

            builder.Services.AddScoped<IAppealRepository, AppealRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<IPetRepository, PetRepository>();
            builder.Services.AddScoped<ITimeWorkRepository, TimeWorkRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IWorkAndLocationRepository, WorkAndLocationRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();

            builder.Services.AddScoped<IAppealService, AppealService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<ILocationService, LocationService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IPetService, PetService>();
            builder.Services.AddScoped<ITimeWorkService, TimeWorkService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IWorkService, WorkService>();
        }
    }
}
