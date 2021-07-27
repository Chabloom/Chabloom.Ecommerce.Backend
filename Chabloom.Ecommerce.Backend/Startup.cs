// Copyright 2020-2021 Chabloom LC. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Keys;
using Chabloom.Ecommerce.Backend.Data;
using Chabloom.Ecommerce.Backend.Models.Tenants;
using Chabloom.Ecommerce.Backend.Services;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Chabloom.Ecommerce.Backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var vaultAddress = "https://chb-dev-1.vault.azure.net/";
            if (!string.IsNullOrEmpty(vaultAddress))
            {
                services.AddAzureClients(builder =>
                {
                    builder.AddKeyClient(new Uri(vaultAddress));
                    builder.AddSecretClient(new Uri(vaultAddress));
                    builder.UseCredential(new DefaultAzureCredential());
                });

                services
                    .AddDataProtection()
                    .ProtectKeysWithAzureKeyVault(
                        new Uri(
                            "https://chb-dev-1.vault.azure.net/keys/key-ecommerce/3659282e50f24191b6281d8549662d19"),
                        new DefaultAzureCredential())
                    .PersistKeysToDbContext<ApplicationDbContext>();
            }

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    Configuration.GetValue<string>("db-ecommerce-application")));

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.All;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });

            services.AddIdentity<TenantUser, TenantRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            const string audience = "Chabloom.Ecommerce.Backend";

            var frontendPublicAddress = Environment.GetEnvironmentVariable("ECOMMERCE_FRONTEND_ADDRESS");
            var identityServerBuilder = services.AddIdentityServer(options =>
                {
                    options.UserInteraction.ErrorUrl = $"{frontendPublicAddress}/account/error";
                    options.UserInteraction.LoginUrl = $"{frontendPublicAddress}/account/signIn";
                    options.UserInteraction.LogoutUrl = $"{frontendPublicAddress}/account/signOut";
                })
                .AddConfigurationStore(options => options.ConfigureDbContext = x =>
                    x.UseNpgsql(Configuration.GetValue<string>("db-ecommerce-configuration"),
                        y => y.MigrationsAssembly(audience)))
                .AddOperationalStore(options => options.ConfigureDbContext = x =>
                    x.UseNpgsql(Configuration.GetValue<string>("db-ecommerce-operation"),
                        y => y.MigrationsAssembly(audience)))
                .AddAspNetIdentity<TenantUser>();

            if (!string.IsNullOrEmpty(vaultAddress))
            {
                var keyClient = new KeyClient(new Uri(vaultAddress), new DefaultAzureCredential());
                var vaultKey = keyClient.GetKey("key-ecommerce").Value;
                if (vaultKey.KeyType == KeyType.Rsa)
                {
                    var rsa = vaultKey.Key.ToRSA();
                    var key = new RsaSecurityKey(rsa)
                    {
                        KeyId = vaultKey.Properties.Version
                    };

                    identityServerBuilder.AddSigningCredential(key, IdentityServerConstants.RsaSigningAlgorithm.PS256);
                }
            }
            else
            {
                identityServerBuilder.AddDeveloperSigningCredential();
            }

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = PathString.Empty;
                    options.Events.OnRedirectToLogin = context =>
                    {
                        context.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    };
                    options.Events.OnRedirectToAccessDenied = context =>
                    {
                        context.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    };
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SameSite = SameSiteMode.None;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.Cookie.IsEssential = true;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = Environment.GetEnvironmentVariable("ECOMMERCE_BACKEND_ADDRESS");
                    options.Audience = audience;
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", audience);
                });
            });

            services.AddScoped<IValidator, Validator>();
            services.AddTransient<EmailSender>();
            services.AddTransient<SmsSender>();

            // Load CORS origins
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    var origins = new List<string>
                    {
                        Environment.GetEnvironmentVariable("ECOMMERCE_FRONTEND_ADDRESS")
                    };
                    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                    {
                        origins.Add("https://localhost:3000");
                        origins.Add("https://localhost:3001");
                        origins.Add("https://localhost:3002");
                        origins.Add("https://localhost:3003");
                    }

                    builder.WithOrigins(origins.ToArray());
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                    builder.AllowCredentials();
                });
            });

            // Setup generated OpenAPI documentation
            var authAddress = Environment.GetEnvironmentVariable("ECOMMERCE_BACKEND_ADDRESS");
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Chabloom Ecommerce",
                    Description = "Chabloom Ecommerce v1 API",
                    Version = "v1"
                });
                options.AddSecurityDefinition("openid", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OpenIdConnect,
                    OpenIdConnectUrl = new Uri($"{authAddress}/.well-known/openid-configuration")
                });
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders();

            app.SeedIdentityServer();

            app.UseCors();

            app.UseIdentityServer();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger(options => options.RouteTemplate = "/swagger/{documentName}/chabloom-ecommerce-api.json");
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/chabloom-ecommerce-api.json", "Chabloom Ecommerce v1 API");
            });

            app.UseEndpoints(endpoints => { endpoints.MapControllers().RequireAuthorization("ApiScope"); });
        }
    }
}