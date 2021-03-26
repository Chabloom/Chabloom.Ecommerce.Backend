// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System.Collections.Generic;
using Chabloom.Ecommerce.Backend.Data;
using Chabloom.Ecommerce.Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Chabloom.Ecommerce.Backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EcommerceDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.All;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://accounts-api-dev-1.chabloom.com";
                    options.Audience = "Chabloom.Ecommerce.Backend";
                    options.RequireHttpsMetadata = !Environment.IsDevelopment();
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "Chabloom.Ecommerce.Backend");
                });
            });

            services.AddScoped<IValidator, Validator>();

            // Setup CORS origins
            var corsOrigins = new List<string>();
            if (Environment.IsDevelopment())
            {
                // Allow CORS from ecommerce DEV, UAT, and local environments
                corsOrigins.Add("http://localhost:3003");
                corsOrigins.Add("https://localhost:3003");
                corsOrigins.Add("https://ecommerce-dev-1.chabloom.com");
                corsOrigins.Add("https://ecommerce-uat-1.chabloom.com");
            }
            else
            {
                // Allow CORS from ecommerce PROD environment
                corsOrigins.Add("https://ecommerce.chabloom.com");
            }

            // Add the CORS policy
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(corsOrigins.ToArray());
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                    builder.AllowCredentials();
                });
            });

            services.AddApplicationInsightsTelemetry();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders();

            app.UseCors();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers().RequireAuthorization("ApiScope"); });
        }
    }
}