using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using My.World.Api.Models;
using My.World.Web.Helpers;
using My.World.Web.Services;

namespace My.World.Web
{
    public class Startup
    {
        private ILogger<Startup> _logger;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IWebHostEnvironment Environment { get; set; }
        public IConfiguration Configuration { get; }
        public IHttpContextAccessor HttpContextAccessor { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddMvc(options => options.EnableEndpointRouting = false).AddSessionStateTempDataProvider();
            IMvcBuilder builder = services.AddRazorPages();

#if DEBUG
            if (Environment.IsDevelopment())
            {
                builder.AddRazorRuntimeCompilation();
            }
#endif
            services.AddCors();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var key = Encoding.ASCII.GetBytes(Configuration.GetValue<String>("AppSettings:Secret"));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(x =>
           {
               x.Events = new JwtBearerEvents
               {
                   OnTokenValidated = context =>
                   {
                       var userService = context.HttpContext.RequestServices.GetRequiredService<IUsersApiService>();
                       var userId = int.Parse(context.Principal.Identity.Name);
                       var user = userService.GetUsers(new UsersModel() { id = userId });
                       if (user == null)
                       {
                           context.Fail("Unauthorized");
                       }
                       return Task.CompletedTask;
                   }
               };
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(key),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
           });

            string MyWorldApiUrl = Configuration.GetValue<string>("MyWorldApiUrl");
            string MyWorldContentApiUrl = Configuration.GetValue<string>("MyWorldContentApiUrl");

            #region Admin Services
            services.AddTransient<ITokenService, TokenService>(ts => new TokenService() { SecretString = Configuration.GetValue<String>("AppSettings:Secret") });
            services.AddScoped<IAppConfigApiService, AppConfigApiService>(pr => new AppConfigApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<IContentPlansApiService, ContentPlansApiService>(pr => new ContentPlansApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<IContentTypesApiService, ContentTypesApiService>(pr => new ContentTypesApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<IDocumentsApiService, DocumentsApiService>(pr => new DocumentsApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<IFoldersApiService, FoldersApiService>(pr => new FoldersApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });

            services.AddScoped<IUserDetailsApiService, UserDetailsApiService>(pr => new UserDetailsApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });

            services.AddScoped<IUsersApiService, UsersApiService>(pr => new UsersApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            #endregion

            #region Content Services
            services.AddScoped<IBuildingsApiService, BuildingsApiService>(pr => new BuildingsApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<ICharactersApiService, CharactersApiService>(pr => new CharactersApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<IConditionsApiService, ConditionsApiService>(pr => new ConditionsApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<IContinentsApiService, ContinentsApiService>(pr => new ContinentsApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<ICountriesApiService, CountriesApiService>(pr => new CountriesApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<ICreaturesApiService, CreaturesApiService>(pr => new CreaturesApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<IDeitiesApiService, DeitiesApiService>(pr => new DeitiesApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<IFlorasApiService, FlorasApiService>(pr => new FlorasApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<IFoodsApiService, FoodsApiService>(pr => new FoodsApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<IGovernmentsApiService, GovernmentsApiService>(pr => new GovernmentsApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<IGroupsApiService, GroupsApiService>(pr => new GroupsApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<IItemsApiService, ItemsApiService>(pr => new ItemsApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<IJobsApiService, JobsApiService>(pr => new JobsApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<ILandmarksApiService, LandmarksApiService>(pr => new LandmarksApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<ILanguagesApiService, LanguagesApiService>(pr => new LanguagesApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<ILocationsApiService, LocationsApiService>(pr => new LocationsApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<ILoresApiService, LoresApiService>(pr => new LoresApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<IMagicsApiService, MagicsApiService>(pr => new MagicsApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<IOrganizationsApiService, OrganizationsApiService>(pr => new OrganizationsApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<IPlanetsApiService, PlanetsApiService>(pr => new PlanetsApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<IRacesApiService, RacesApiService>(pr => new RacesApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<IReligionsApiService, ReligionsApiService>(pr => new ReligionsApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<IScenesApiService, ScenesApiService>(pr => new ScenesApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<ISportsApiService, SportsApiService>(pr => new SportsApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<ITechnologiesApiService, TechnologiesApiService>(pr => new TechnologiesApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<ITownsApiService, TownsApiService>(pr => new TownsApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<ITraditionsApiService, TraditionsApiService>(pr => new TraditionsApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<IUniversesApiService, UniversesApiService>(pr => new UniversesApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<IVehiclesApiService, VehiclesApiService>(pr => new VehiclesApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            #endregion

            #region User Defined Services
            services.AddScoped<IDashboardApiService, DashboardApiService>(pr => new DashboardApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            services.AddScoped<IObjectBucketApiService, ObjectBucketApiService>(pr => new ObjectBucketApiService() { MyWorldApiUrl = MyWorldApiUrl, MyWorldContentApiUrl = MyWorldContentApiUrl });
            #endregion

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true; // consent required
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(90000);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddTransient<IEmailClient>(s => new EmailClient(Configuration));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IHttpContextAccessor httpContextAccessor)
        {
            var log4Net = loggerFactory.AddLog4Net();
            _logger = log4Net.CreateLogger<Startup>();

            _logger.LogInformation("Current Environment " + env.EnvironmentName);

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();

                app.UseExceptionHandler("/Home/Error");
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseSession();

            app.Use(async (context, next) =>
            {
                var JWToken = context.Session.GetString(AppConstants.JWTTOKEN);
                if (!string.IsNullOrEmpty(JWToken))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
                }
                await next();
            });

            app.UseStatusCodePages(async context =>
            {
                var response = context.HttpContext.Response;
                if (response.StatusCode == (int)HttpStatusCode.Unauthorized ||
                    response.StatusCode == (int)HttpStatusCode.Forbidden)
                    response.Redirect("/Account/Login");

                if (response.StatusCode == (int)HttpStatusCode.NotFound)
                    response.Redirect("/Home/Error");

            });

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();


            app.Use(async (context, next) =>
            {
                string host = context.Request.Host.Value;
                string scheme = context.Request.Scheme;
                string domain = scheme + "://" + host;

                Helpers.Utility.CurrentDomain = domain;

                await next();
            });

            //app.Use(async (context, next) =>
            //{
            //    string CurrentUserIDSession = context.Session.GetString("CurrentUserID");
            //    if (!context.Request.Path.Value.Contains("/Account/Login") && !context.Request.Path.Value.Contains("/Account/SignUp"))
            //    {
            //        if (string.IsNullOrEmpty(CurrentUserIDSession))
            //        {
            //            var path = $"/Account/Login?ReturnUrl={context.Request.Path}";
            //            context.Response.Redirect(path);
            //            return;
            //        }

            //    }
            //    await next();
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Dashboard}/{action=Index}/{id?}");
            });
        }
    }
}
