using Jumia.Application.Contract;
using Jumia.Application.IServices;
using Jumia.Application.Services;
using Jumia.Application.Services.IServices;
using Jumia.Application.Services.Services;
using Jumia.Context;
using Jumia.Infrastructure;
using Jumia.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace AdminDashBoard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddScoped<IProductRepository, ProductRepository>();
             builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
             builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
             builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
             builder.Services.AddScoped<IOrderRepository, OrderRepository>();
             builder.Services.AddScoped<IOrderItemsRepository, OrderItemRepository>();
             builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
             builder.Services.AddScoped<IShippmentRepository, ShippmentRepository>();
            //builder.Services.AddScoped<IProductServices, ProductService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderItemService, OrderItemService>();
            builder.Services.AddScoped<IShippmentService, ShippmentService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddIdentity<UserIdentity, UserRole>()
            .AddEntityFrameworkStores<JumiaContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddDbContext<JumiaContext>(op =>
            {
                op.UseSqlServer(builder.Configuration.GetConnectionString("Db"));
            });

            #region Localization
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddLocalization(opt =>
             {
                 opt.ResourcesPath = "";
             });

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                List<CultureInfo> supportedCultures = new List<CultureInfo>
                {
                        new CultureInfo("en-US"),
                         new CultureInfo("de-DE"),
                        new CultureInfo("ar-EG")
               };

                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            
            #endregion




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
           
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();
            var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
