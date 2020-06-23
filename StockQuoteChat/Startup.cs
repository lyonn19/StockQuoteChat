using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockMarketBot.Services;
using StockQuoteChat.Factory;
using StockQuoteChat.Hubs;
using StockQuoteChat.Messaging.Background;
using StockQuoteChat.Models;

namespace StockQuoteChat
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR(o =>
            {
                o.EnableDetailedErrors = true;
                o.MaximumReceiveMessageSize = 10240; // bytes
            });

            services.AddSingleton<ChatHub>();
            services.AddTransient<IStockQuoteService, StockQuoteService>();
            services.AddHostedService<ConsumeRabbitMQHostedService>();

            // Database
            services.AddDbContext<ApplicationContext>(opts =>
               opts.UseSqlServer(Configuration.GetConnectionString("sqlConnection")));

            /*
            services.AddIdentity<User, IdentityRole>()
             .AddEntityFrameworkStores<ApplicationContext>();
            */

            services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 7;
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.User.RequireUniqueEmail = true;
            })
             .AddEntityFrameworkStores<ApplicationContext>();

            services.AddScoped<IUserClaimsPrincipalFactory<User>, CustomClaimsFactory>();
            //services.ConfigureApplicationCookie(o => o.LoginPath = "/Account/Login");

            services.AddAutoMapper(typeof(Startup));

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chathub");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
