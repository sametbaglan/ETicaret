using Business.Layer.Abstrack;
using Business.Layer.Concrete;
using DataAccess.Layer.Abstrack;
using DataAccess.Layer.Concrete;
using ETicaret.BusinessLayer.Abstrack;
using ETicaret.BusinessLayer.Concrete;
using ETicaret.DataAccessLayer.Abstrack;
using ETicaret.DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using UI.Layer.CustomExceptionMiddleware;

namespace UI.Layer
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
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(1);
            });
            services.AddCors();
            services.AddControllers();
            var tokenKey = Configuration.GetValue<string>("TokenKey");
            var key = Encoding.ASCII.GetBytes(tokenKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
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

            services.AddControllersWithViews();
            services.AddTransient<IShipmenItemsService, ShipmenItemsManager>();
            services.AddTransient<IShipmenItemDal, EfShipmenItemsDal>();

            services.AddTransient<IShipmenService, ShipmenManager>();
            services.AddTransient<IShipmenDal, EfShipmenDal>();

            services.AddTransient<IAddressesService, AddressesManager>();
            services.AddTransient<IAdressesDal, EfAddressesDal>();

            services.AddTransient<IProductService, ProductManager>();
            services.AddTransient<IProductDal, EfProductDal>();

            services.AddTransient<ICartItemService, CartItemManager>();
            services.AddTransient<ICartItemDal, EfCartItemDal>();


            services.AddTransient<ICategoriaService, CategoriaManager>();
            services.AddTransient<ICategoriaDal, EfCategoriaDal>();

            services.AddTransient<IItemBodySizeService, ItemBodySizeManager>();
            services.AddTransient<IItemBodySizeDal, EfItemBodySizeDal>();

            services.AddTransient<IImagesService, ImagesManager>();
            services.AddTransient<IImagesDal, EfImagesDal>();

            services.AddTransient<IItemColorService, ItemColorManager>();
            services.AddTransient<IItemColorDal, EfItemColorDal>();

            services.AddTransient<IUsersService, UsersManager>();
            services.AddTransient<IUsersDal, EfUsersDal>();

            services.AddTransient<ICartDalService, CartDalManager>();
            services.AddTransient<ICartDal, EfCartDal>();
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
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.ConfigureExceptionHandler();
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Product}/{action=ProductList}/{id?}");
            });
        }
    }
}
