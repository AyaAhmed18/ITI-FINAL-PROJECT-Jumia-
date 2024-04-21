using Jumia.Application.Contract;
using Jumia.Application.IServices;
using Jumia.Application.Services;
using Jumia.Application.Services.IServices;
using Jumia.Application.Services.Services;
using Jumia.Context;
using Jumia.Infrastructure;
using Jumia.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Text;
namespace JumiaStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });

                option.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                         new string[] {}
                 }
            });
            });

            // Add services to the container.

            //JWT
            builder.Services.AddAuthentication(op =>
            {
                op.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(op =>       //instead of add cookie
            {
                op.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = builder.Configuration["jwt:Issuer"],
                    ValidAudience = builder.Configuration["jwt:Audiences"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey((Encoding.UTF8.GetBytes(builder.Configuration["jwt:Key"])))

                };
            });
            //cors origin
            builder.Services.AddCors(op =>
            {
                op.AddPolicy("Default", policy =>
                {
                    // policy.WithHeaders("auth").WithOrigins("http://localhost:4200").WithMethods("Post")
                    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Rebositories&Services
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderItemsRepository, OrderItemRepository>();
            builder.Services.AddScoped<IShippmentRepository, ShippmentRepository>();
            builder.Services.AddScoped<IProductServices, ProductService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IProductSpecificationSubCategoryServices, ProductSpecificationSubCategoryServices>();
            builder.Services.AddScoped<IOrderItemService, OrderItemService>();
            builder.Services.AddScoped<IShippmentService, ShippmentService>();
            builder.Services.AddScoped<IBrandService,BrandService>();
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
         /*   builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });*/
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
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}
           // app.UseRouting();
            app.UseRequestLocalization();
            app.UseCors("Default");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
