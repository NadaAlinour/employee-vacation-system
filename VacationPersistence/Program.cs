using Microsoft.EntityFrameworkCore;
using VacationPersistence.Context;
using VacationApplication.Services;
using VacationDomain.Models;
using VacationPersistence.DatabaseAccess;

namespace VacationPersistence
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<VacationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDatabase")));


            // register the service to use with dependency injection in api controller
            builder.Services.AddScoped<IDataRepository<Employee>, EmployeeRepository>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();

           

            
        }
    }
}