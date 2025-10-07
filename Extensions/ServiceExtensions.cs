//using Contracts;
//using Entities.Models;
//using LoggerService;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
//using System.Text;
//using Vadickart.Repository;
//using VadicKart.Repository.Contract;
//using Vedickart.Services;
//using VedicKart.Service.Contract;

//namespace vedickartApi.Extensions
//{
//    public static class ServiceExtensions
//    {
//        public static void ConfigureCors(this IServiceCollection services) =>
//           services.AddCors(options => {
//               options.AddPolicy("CorsPolicy", builder =>
//               builder.AllowAnyOrigin()
//               .AllowAnyMethod()
//               .AllowAnyHeader());
//           });

//        public static void ConfigureIISIntegration(this IServiceCollection services) =>
//          services.Configure<IISOptions>(options =>
//          {
//          });

//        public static void ConfigureLoggerService(this IServiceCollection services) =>
//              services.AddSingleton<ILoggerManager, LoggerManager>();

//        public static void ConfigureSwagger(this IServiceCollection services)
//        {
//            services.AddSwaggerGen(s =>
//            {
//                s.SwaggerDoc("v1", new OpenApiInfo
//                {
//                    Title = "Code Maze API",
//                    Version = "v1"
//                });
//                s.SwaggerDoc("v2", new OpenApiInfo
//                {
//                    Title = "Code Maze API",
//                    Version = "v2"
//                });
//            });
//        }

//        public static void ConfigureServiceManager(this IServiceCollection services)
//        {
//            services.AddScoped<IServiceManager, ServiceManager>();
//            ////services.AddScoped<IServiceManager, ServiceManager>();
//            //services.AddScoped<IRepositoryManager, RepositoryManager>();
//            //// services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//            //services.AddHttpContextAccessor();
//        }

//        public static void ConfigureRepositoryManager(this IServiceCollection services)
//        {
//            services.AddScoped<IRepositoryManager, RepositoryManager>();
//        }

//        public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
//        {
//            var jwtConfiguration = new JwtConfiguration();
//            configuration.Bind(jwtConfiguration.Section, jwtConfiguration);

//            var secretKey = Environment.GetEnvironmentVariable(Settings.SecretKey)
//                ?? throw new Exception(Settings.SecretNotFoundMessage);

//            services.AddAuthentication(opt =>
//            {
//                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//            })
//            .AddJwtBearer(options =>
//            {
//                options.TokenValidationParameters = new TokenValidationParameters
//                {
//                    ValidateIssuer = true,
//                    ValidateAudience = true,
//                    ValidateLifetime = true,
//                    ValidateIssuerSigningKey = true,
//                    ValidIssuer = jwtConfiguration.ValidIssuer,
//                    ValidAudience = jwtConfiguration.ValidAudience,
//                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
//                };
//            });
//        }

//        public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration) =>
//            services.Configure<JwtConfiguration>(configuration.GetSection("AuthenticationSettings:Jwt"));




//    }
//}
using Contracts;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Vadickart.Repository;
using VadicKart.Repository.Contract;
using Vedickart.Services;
using VedicKart.Service.Contract;

namespace vedickartApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
           services.AddCors(options => {
               options.AddPolicy("CorsPolicy", builder =>
               builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
           });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
          services.Configure<IISOptions>(options =>
          {
          });

        public static void ConfigureLoggerService(this IServiceCollection services) =>
              services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "VedicKart API",
                    Version = "v1"
                });

                // Add JWT authentication to Swagger
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        public static void ConfigureServiceManager(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure JWT settings
            var jwtConfiguration = new JwtConfiguration();
            configuration.Bind(jwtConfiguration.Section, jwtConfiguration);
            services.AddSingleton(jwtConfiguration);

            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddHttpContextAccessor();
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfiguration = new JwtConfiguration();
            configuration.Bind(jwtConfiguration.Section, jwtConfiguration);

            // Try to get secret from environment variable first, then fallback to Settings
            var secretKey = Environment.GetEnvironmentVariable("SECRET") ?? Settings.SecretKey;

            if (string.IsNullOrEmpty(secretKey))
            {
                throw new Exception(Settings.SecretNotFoundMessage);
            }

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfiguration.ValidIssuer ?? Settings.ValidIssuer,
                    ValidAudience = jwtConfiguration.ValidAudience ?? Settings.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ClockSkew = TimeSpan.Zero // Remove clock skew for more precise token validation
                };
            });
        }

        public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration) =>
            services.Configure<JwtConfiguration>(configuration.GetSection("AuthenticationSettings:Jwt"));
    }
}