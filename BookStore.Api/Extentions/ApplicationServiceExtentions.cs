using BookStore.Core.Entities;
using BookStore.Core.Helpers;
using BookStore.Core.IUnitOfWork;
using BookStore.Core.Repository.Contract;
using BookStore.Core.Service.Contract;
using BookStore.Repository.BasketRepo;
using BookStore.Repository.ConText;
using BookStore.Repository.Identity;
using BookStore.Repository.UnitOfWork;
using BookStore.Service.AuthService;
using BookStore.Service.BasketService;
using BookStore.Service.BooksService;
using BookStore.Service.OrderService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;
namespace BookStore.Api.Extentions
{
    public static class ApplicationServiceExtentions
    {
        public static IServiceCollection ApplicationServices (this IServiceCollection services ,ConfigurationManager configuration)
        {
            services.AddScoped(typeof(IUnitOfWork),typeof(UnitOfWork));
            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

            services.AddScoped(typeof(IBooksService), typeof(BooksService));
            services.AddScoped(typeof(IBasketService), typeof(BasketService));
            services.AddScoped(typeof(IOrderService), typeof(OrderService));


            services.AddAutoMapper(typeof(MappingProfile));
            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddDbContext<ApplicationIdentityContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password = null ;
            }).AddEntityFrameworkStores<ApplicationIdentityContext>();

            services.AddSingleton<IConnectionMultiplexer>(serviceProvidor =>
            {
                var Connection = configuration.GetConnectionString("redisConnection");
                return ConnectionMultiplexer.Connect(Connection);
            });

            //stripe configuration
            services.Configure<StripeSettings>(configuration.GetSection("StripeSettings"));

            return services;
        }
        public static IServiceCollection AddAuthServicees(this IServiceCollection services, IConfiguration configuration)
        {

            //add auth service
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(JwtBearerOptions =>
                {
                    JwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["jwt:validIssuer"],
                        ValidateAudience = true,
                        ValidAudience = configuration["jwt:validAudience"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:AuthKey"])),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                    };
                });
            //add DI for auth service to add token
            services.AddScoped(typeof(IAuthService), typeof(AuthService));

            return services;
        }
    }

}
