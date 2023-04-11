using DogSitterMarketplaceApi.Mappings;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Mappings;
using DogSitterMarketplaceBll.Services;
using DogSitterMarketplaceDal;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<OrdersAndPetsAndCommentsContext>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddAutoMapper(typeof(MapperApiOrderProfile), typeof(MapperBllOrderProfile),  
                               typeof(MapperApiPetProfile), typeof(MapperBllPetProfile),
                               typeof(MapperApiCommentProfile), typeof(MapperBllCommentProfile));


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
