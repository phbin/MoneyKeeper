
using MoneyKeeper.Services.Mail;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MoneyKeeper.Error;
using MoneyKeeper.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net.Mime;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using MoneyKeeper.Services.Auth;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MoneyKeeper.Services.Category;
using MoneyKeeper.Services.Event;
using MoneyKeeper.Services.Transactions;

namespace MoneyKeeper.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
         services.AddCors(options =>
         {
             options.AddDefaultPolicy(policy =>
             policy.SetIsOriginAllowed(origin => true)
             .AllowAnyMethod()
             .AllowAnyHeader().AllowCredentials());
         });

        public static void ConfigureRepository(this IServiceCollection services) =>
            services
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IMailService, MailService>()
            .AddScoped<IMailService, MailService>()
            .AddScoped<INotiService, NotiService>()
            .AddScoped<IEventService, EventService>()
            .AddScoped<IWalletService, WalletService>()
            .AddScoped<ICategoryService, CategoryService>()
            .AddScoped<ITransactionService, TransactionService>()
            .AddSingleton<IDictionary<string, string>>(_ => new Dictionary<string, string>());
        public static void ConfigureSwaggerOptions(this SwaggerGenOptions options)
        {
            options.AddSecurityDefinition("Bearer",
            new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Place to add JWT with Bearer",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
                new string[]{}
                }
            });

        }
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var Key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
            });
        }

        public static void AddCustomOptions(this IMvcBuilder builder)
        {
            builder.AddJsonOptions(x =>
             {
                 x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                 x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
             }).ConfigureApiBehaviorOptions(options =>
             {
                 options.InvalidModelStateResponseFactory = context =>
                 {
                     var result = new ValidationFailedResult(context.ModelState);

                     // TODO: add `using System.Net.Mime;` to resolve MediaTypeNames
                     result.ContentTypes.Add(MediaTypeNames.Application.Json);
                     result.ContentTypes.Add(MediaTypeNames.Application.Xml);
                     return result;
                 };
             });
        }
    }
}
