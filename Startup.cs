using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wallet.API.Controllers.Config;
using Wallet.API.Domain.Repositories;
using Wallet.API.Domain.Services;
using Wallet.API.Extensions;
using Wallet.API.Persistence.Contexts;
using Wallet.API.Persistence.Repositories;
using Wallet.API.Services;

namespace Wallet.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomSwagger();

            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                // Adds a custom error response factory when ModelState is invalid
                options.InvalidModelStateResponseFactory = InvalidModelStateResponseFactory.ProduceErrorResponse;
            });

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase(Configuration.GetConnectionString("memory"));
            });

            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IBalanceRepository, BalanceRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IBalanceService, BalanceService>();
            services.AddScoped<ITransactionService, TransactionService>();

            services.AddAutoMapper(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomSwagger();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}