using System;
using System.Reflection;
using System.IO;
using AutoMapper;
using CRUD.Models.FactoryDbContext;
using CRUD.Repository;
using CRUD.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Collections.Generic;
using CRUD.Options;
using Microsoft.Data.SqlClient;

namespace CRUD
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
            services.Configure<UnitOptions>(Configuration.GetSection("UnitOptions"));
            services.AddDbContext<FactoryDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DataConnect")));
            SqlCommand command = new SqlCommand();
            // Setting command timeout to 1 second  
            command.CommandTimeout = 60;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("AppSettings:JwtSecret").Value)),
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   ValidateLifetime = true
               };
           });
            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddTransient<IFactoryRepository<Factory>, FactoryRepository>();
            services.AddTransient<IFactoryRepository<Unit>, UnitRepository>();
            services.AddTransient<IFactoryRepository<Tank>, TankRepository>();
            services.AddHostedService<TankService>();
            services.AddHttpClient("events", c =>
            {
                c.BaseAddress = new Uri("https://localhost:44358/api/events");
            });
            services.AddHostedService<EventSyncService>();
            services.AddSwaggerGen(c => {

                // ќбъ€вл€ем новую схему авторизации
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization via Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    // ”казываем тип и схему авторизации
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                // —оздаем референс на свежесозданную схему
                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<String>()
                    }
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                //c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();



            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
