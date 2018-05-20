using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hivemind.Entities;
using Hivemind.Managers;
using Hivemind.Managers.Implementation;
using Hivemind.Providers;
using Hivemind.Services;
using Hivemind.Services.Implementation;
using Hivemind.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Hivemind.Api.Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
            _connectionString = Configuration["ConnectionString"];
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }
        private string _connectionString;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "yourdomain.com",
                        ValidAudience = "yourdomain.com",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecurityKey"]))
                    };
                });

            // managers
            services.AddScoped<IGangerManager, GangerManager>();
            services.AddScoped<IGangManager, GangManager>();
            services.AddScoped<IInjuryManager, InjuryManager >();
            services.AddScoped<ISkillManager, SkillManager>();
            services.AddScoped<IWeaponManager, WeaponManager>();
            services.AddScoped<ITerritoryManager, TerritoryManager>();
            services.AddScoped<IUserManager, UserManager>();

            // services
            services.AddScoped<IExperienceService, ExperienceService>();
            services.AddScoped<IIncomeService, IncomeService>();
            services.AddScoped<IInjuryService, InjuryService>();
            services.AddScoped<IGameService, GameService>();

            // providers
            services.AddScoped<IGangerProvider, GangerProvider>(s => new GangerProvider(_connectionString));
            services.AddScoped<IGangProvider, GangProvider>(s => new GangProvider(_connectionString));
            services.AddScoped<IInjuryProvider, InjuryProvider>(s => new InjuryProvider(_connectionString));
            services.AddScoped<ISkillProvider, SkillProvider>(s => new SkillProvider(_connectionString));
            services.AddScoped<ITerritoryProvider, TerritoryProvider>(s => new TerritoryProvider(_connectionString));
            services.AddScoped<IUserProvider, UserProvider>(s => new UserProvider(_connectionString));
            services.AddScoped<IWeaponProvider, WeaponProvider>(s => new WeaponProvider(_connectionString));

            // utilities
            services.AddScoped<IDiceRoller, DiceRoller>();
            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();
        }
    }
}
