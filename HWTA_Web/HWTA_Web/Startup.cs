using BLL_HWTA.Concrete;
using BLL_HWTA.Interfaces;
using BLL_HWTA.TokenApp;
using DAL_HWTA;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace HWTA_Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       options.RequireHttpsMetadata = false;
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuer = true,
                           ValidIssuer = AuthOptions.ISSUER,


                           ValidateAudience = true,
                           ValidAudience = AuthOptions.AUDIENCE,
                           ValidateLifetime = true,

                           IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                           ValidateIssuerSigningKey = true,
                       };
                   });

            services.AddSwaggerGen();
            services.AddControllers();

            services.AddAuthorization();

            services.AddDbContext<HwtaDbContext>(
               options => options.UseSqlite("Data Source=hwtaDB1.db"));

            services.AddTransient<IAccountManager, AccountsManager>();
            services.AddTransient<IGlobalGoalsManager, GlobalGoalsManager>();
            services.AddTransient<IUserFriendsManager, UserFriendsManager>();
            services.AddTransient<IUserGoalsManager, UserGoalsManager>();
            services.AddTransient<IUserManager, UserManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, HwtaDbContext context)
        {
            context.Database.Migrate();
             var dbSeed = new DataBaseSeed(context);
             dbSeed.Seed();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HWTA_Web v1");
            });

            app.UseRouting();

            app.UseCors(builder => builder.SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
