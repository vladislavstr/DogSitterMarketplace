using DogSitterMarketplaceApi.Mappings;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Services;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Repositories;
using DogSitterMarketplaceBll.Mappings;
using DogSitterMarketplaceDal.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAutoMapper(typeof(MapperApiUserProfile), typeof(MapperBllUserProfile));
builder.Services.AddSingleton<UserContext>();

builder.Services.AddScoped<IAppealService, AppealService>();
builder.Services.AddScoped<IAppealRepository, AppealRepository>();
builder.Services.AddAutoMapper(typeof(MapperApiAppealProfile), typeof(MapperBllAppealProfile));
builder.Services.AddSingleton<AppealContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
