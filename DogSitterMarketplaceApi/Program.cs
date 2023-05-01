
using DogSitterMarketplaceApi.Mappings;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Mappings;
using DogSitterMarketplaceBll.Services;
using DogSitterMarketplaceDal.Contexts;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Repositories;
using NLog;
using ILogger = NLog.ILogger;
using LogManager = NLog.LogManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<OrdersAndPetsAndCommentsContext>();
builder.Services.AddSingleton<WorkContext>();
builder.Services.AddSingleton<DogSitterMarketplaceContext>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddAutoMapper(typeof(MapperApiOrderProfile), typeof(MapperBllOrderProfile),
                               typeof(MapperApiPetProfile), typeof(MapperBllPetProfile),
                               typeof(MapperApiCommentProfile), typeof(MapperBllCommentProfile));
//InjectLogger(builder);


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAutoMapper(typeof(MapperApiUserProfile), typeof(MapperBllUserProfile), typeof(MapperApiAppealProfile), typeof(MapperBllAppealProfile));
builder.Services.AddSingleton<UserContext>();

builder.Services.AddScoped<IAppealService, AppealService>();
builder.Services.AddScoped<IAppealRepository, AppealRepository>();
builder.Services.AddSingleton<AppealContext>();

var nlog = LogManager.Setup().GetCurrentClassLogger();
builder.Services.AddSingleton<ILogger>(nlog);
builder.Services.AddScoped<IWorkAndLocationRepository, WorkAndLocationRepository>();
builder.Services.AddScoped<ITimeWorkRepository, TimeWorkRepository>();
builder.Services.AddScoped<ITimeWorkService, TimeWorkService>();
builder.Services.AddAutoMapper(typeof(MapperApiWorkProfile), typeof(MapperBllWorkProfile));
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

//void InjectLogger(WebApplicationBuilder builder)
//{
//    var config = new NLog.Config.LoggingConfiguration();
//    var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "logs.txt" };
//    var logconsole = new NLog.Targets.ConsoleTarget("logconsole");
//    config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, logconsole);
//    config.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, logfile);
//    LogManager.Configuration = config;

//    var logger = NLog.LogManager.Setup().GetCurrentClassLogger();
//    builder.Services.AddSingleton<NLog.ILogger>(logger);
//}
