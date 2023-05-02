using DogSitterMarketplaceApi.Mappings;
using DogSitterMarketplaceBll.IServices;
using DogSitterMarketplaceBll.Mappings;
using DogSitterMarketplaceBll.Services;
using DogSitterMarketplaceDal.Contexts;
using DogSitterMarketplaceDal.IRepositories;
using DogSitterMarketplaceDal.Repositories;
using DogSitterMarketplaceApi.Configuration;
using DogSitterMarketplaceDal.Configurations;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System.Text;
using ILogger = NLog.ILogger;
using LogManager = NLog.LogManager;

var builder = WebApplication.CreateBuilder(args);

InjectAuthenticationDependencies(builder);

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

void InjectAuthenticationDependencies(WebApplicationBuilder builder)
{
    // Get configuration for token generation
    var jwtConfig = builder.Configuration.GetSection("JwtSettings")
        .Get<JwtConfigurationSettings>();

    builder.Services.AddSingleton<IJwtConfigurationSettings>(jwtConfig);

    // Add auth dependencies
    builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(jwt =>
    {
        var key = Encoding.ASCII.GetBytes(
            jwtConfig.Key);

        jwt.SaveToken = true;
        jwt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),

            // Just to avoid issues on localhost, it must be true on prod
            ValidateIssuer = false,
            ValidateAudience = false,

            // To avoid re-generation scenario just for develop
            RequireExpirationTime = false,

            ValidateLifetime = true
        };
    });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("User", policy =>
                          policy.RequireClaim("User"));

        options.AddPolicy("Admin", policy =>
                      policy.RequireClaim("Admin"));

        options.AddPolicy("Sitter", policy =>
                      policy.RequireClaim("Sitter"));
    });

    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
        .AddEntityFrameworkStores<UserContext>();
}