
using BookStore.Api.Extentions;
using BookStore.Repository.ConText;
using BookStore.Repository.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Services
            // Add services to the container.
            builder.Services.ApplicationServices(builder.Configuration);
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(); 
            #endregion

            var app = builder.Build();

            #region Asking Clr To Generate Object From storeContext
            //1-Create scope (using keyword => dispose the scope after using it )
            using var scope = app.Services.CreateScope();

            //2-Create service
            var service = scope.ServiceProvider;

            //3-generate object from StoreContext and _IdentityDbContext
            var _DbContext = service.GetRequiredService<StoreContext>();

            var _IdentityDbContext = service.GetRequiredService<ApplicationIdentityContext>();

            //4- log the ex using loggerFactory Class and generate object from loggerFactory
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();
            try
            {
                //4-add migration
                await _DbContext.Database.MigrateAsync();
                await _IdentityDbContext.Database.MigrateAsync();

                //-data seeding
                await StoreSeed.SeedAsync(_DbContext);
                //var _userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
                //await IdentityContextSeedingData.IdentitySeedAsync(_userManager);

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error has been occured during apply the migration");
            }

            #endregion

            #region MiddleWares
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
