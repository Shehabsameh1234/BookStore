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
using BookStore.Service.BooksService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
namespace BookStore.Api.Extentions
{
    public static class ApplicationServiceExtentions
    {
        public static IServiceCollection ApplicationServices (this IServiceCollection services ,ConfigurationManager configuration)
        {
            services.AddScoped(typeof(IUnitOfWork),typeof(UnitOfWork));
            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

            services.AddScoped(typeof(IBooksService), typeof(BooksService));
            services.AddScoped(typeof(IAuthService), typeof(AuthService));
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
                
            }).AddEntityFrameworkStores<ApplicationIdentityContext>();

            services.AddSingleton<IConnectionMultiplexer>(serviceProvidor =>
            {
                var Connection = configuration.GetConnectionString("redisConnection");
                return ConnectionMultiplexer.Connect(Connection);
            });

            return services;
        }
    }

}
