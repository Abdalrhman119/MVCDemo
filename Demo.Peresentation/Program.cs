using Demo.BusinessLogic.Profiles;
using Demo.BusinessLogic.Services.AttachmentService;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Contexts;
using Demo.DataAccess.Models.IdintityModaels;
using Demo.DataAccess.Repositories.Classes;
using Demo.DataAccess.Repositories.Interface;
using Demo.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.Peresentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region Add services to the container
            builder.Services.AddControllersWithViews(option => {
                option.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
                options.UseLazyLoadingProxies();

                //options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
                //options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStrings"));
            });
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            //builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            builder.Services.AddAutoMapper(p=>p.AddProfile(new MappingProfiles()));
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();
            //builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<AppDbContext>()
            //    .AddDefaultTokenProviders();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 3;
            })
.AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            //builder.Services.ConfigureApplicationCookie(Config =>
            //{
            //    Config.LoginPath = "/Account/LogIn";
            //});



            #endregion
            var app = builder.Build();
            #region Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=LogIn}/{id?}");

            #endregion
            app.Run();
        }
    }
}
