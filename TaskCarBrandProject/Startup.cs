using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskCarBrandProject.BusinessLogic;
using TaskCarBrandProject.Context;
using TaskCarBrandProject.IBusinessLogic;
using TaskCarBrandProject.IRepository;
using TaskCarBrandProject.Repository;

namespace TaskCarBrandProject
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

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", option =>
                {
                    option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });

            });
            services.AddDbContext<CarDetailsContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("sqlServerConnetion"));
            });



            services.AddAuthentication(findd =>
            {
                findd.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                findd.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.RequireHttpsMetadata = true;
                option.SaveToken = true;
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("veryverysecret....")),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero

                };
            });

            // CarDetailsRepository : IRepositoryCarDetails

            services.AddScoped<IRepositoryCarDetails, CarDetailsRepository>();

            // CarDetailsBusinessLogic : ICarDetailsBusinessLogic
            services.AddScoped<ICarDetailsBusinessLogic, CarDetailsBusinessLogic>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskCarBrandProject", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env ,ILoggerFactory loggerFactory)
        {
                loggerFactory.AddLog4Net();
    

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskCarBrandProject v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("MyPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
