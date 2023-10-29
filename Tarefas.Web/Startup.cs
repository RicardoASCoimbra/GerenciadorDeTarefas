using AutoMapper;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Tarefas.Infra.IoC;
using Tarefas.Services.AutoMapper;

namespace Tarefas.Web
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
            // AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DomainToViewModelMappingProfile());
                mc.AddProfile(new ViewModelToDomainMappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));
            services.AddHangfireServer();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithOrigins("http://localhost:4200");
                });
            });

            services.AddSignalR();
            services.AddControllers();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Gerenciador de Tarefas",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter 'Bearer' following by space and JWT into the field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
            RegisterAuthentication(services);
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddControllers().AddNewtonsoftJson();
            RegisterCrossOriginRequests(services);
            services.AddMediatR(typeof(Startup));
            NativeInjector.RegisterAppServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RSJ.Intranet5._0.Web v1"));
            }
            else
            {
                app.Use(async (context, next) =>
                {
                    await next();

                    if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
                    {
                        context.Request.Path = "/";
                        context.Response.StatusCode = 200;
                        await next();
                    }
                });

                app.UseDefaultFiles();
                app.UseStaticFiles();
                app.UseHangfireServer();
                app.UseHangfireDashboard();
            }

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterAuthentication(IServiceCollection services)
        {
            var jwtIssuerOptions = new Configurations.Authentication.JwtIssuerOptions();
            Configuration.GetSection("JwtSettings").Bind(jwtIssuerOptions);

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtIssuerOptions.SecretKey));
            jwtIssuerOptions.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            services.AddSingleton(jwtIssuerOptions);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtIssuerOptions.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,
                    ValidAudience = jwtIssuerOptions.Audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                };
            });
        }


        private void RegisterCrossOriginRequests(IServiceCollection services)
        {
            try
            {
                services.AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
                });
            }
            catch (Exception ex)
            {
                var str = ex.Message;
            }
        }
    }
}
