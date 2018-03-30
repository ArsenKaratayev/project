using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace EF
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            services.AddMvc();

            //services.AddAuthorization(options => 
            //{
            //    options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
            //        .RequireAuthenticatedUser()
            //        .Build();
            //});

            //services.AddAuthentication(o => {
            //    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options => {
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = "test",
            //        ValidAudience = "test",
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("secretesecretesecretesecretesecretesecrete"))

            //    };
            //});

            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Filename=./ryp.db"));

            //services.AddDbContext<ApplicationDbContext>(options =>
                //options.UseSqlite(Configuration.GetConnectionString("ConnectionName")));

            //services
                //.AddIdentity<UserEntity, MyUserRole>()
                //.AddEntityFrameworkStores<ApplicationDbContext>()
                //.AddDefaultTokenProviders();

            services.AddDbContext<MyDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("ConnectionName")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("CorsPolicy");
            //app.UseAuthentication();
            app.UseMvc();
        }
    }
}
