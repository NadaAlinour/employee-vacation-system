using VacationAPI.Controllers;
using VacationApplication;
using VacationDomain.Models;
using VacationApplication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VacationPersistence.Context;
using System.Text.Json.Serialization;
using VacationPersistence.Context;
using VacationPersistence;
using System.Text.Json;
using System.Drawing.Text;
using VacationApplication;
using VacationDomain.Models;
using VacationApplication.Services;
using VacationPersistence.DatabaseAccess;
using VacationApplication.DataService;
using VacationApplication.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace VacationAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyCorsPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });

            // Add services to the container.
            builder.Services.AddControllers();


            builder.Services.AddDbContext<VacationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDatabase")));


            builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


            builder.Services.AddAuthentication()
                .AddJwtBearer("JwtBearer", jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Shared secret key")),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                    };
                });


            builder.Services.AddScoped<LoginService>();


            // register the service to use with dependency injection in api controller
            builder.Services.AddScoped<IUserRepository<Admin>, UserManager>();

            builder.Services.AddScoped<IDataRepository<Employee>, EmployeeRepository>();
            builder.Services.AddScoped<IDataRepository<Vacation>, VacationManager>();
            builder.Services.AddScoped<IDataRepository<Unit>, UnitManager>();
            builder.Services.AddScoped<IDataRepository2<Rule>, RuleManager>();
            builder.Services.AddScoped<IDataRepository5<Hire>, HireManager>();
            builder.Services.AddScoped<IDataRepository3<VacationCount>, StatisticsManager>();
            builder.Services.AddScoped<IDataRepository4<EmployeeVacation>, RequestManager>();
            builder.Services.AddScoped<ISearchRepository<Employee, Vacation>, SearchManager>();


            builder.Services.AddScoped<EmployeeService>();
            builder.Services.AddScoped<RequestService>();
            builder.Services.AddScoped<SearchService>();
            builder.Services.AddScoped<RuleService>();
            builder.Services.AddScoped<HireService>();
            builder.Services.AddScoped<StatisticsService>();
            builder.Services.AddScoped<UnitService>();
            builder.Services.AddScoped<VacationService>();




            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("MyCorsPolicy");

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

            /*DateTime datetimeobj = new DateTime(2012, 12, 03, 00, 00, 00);
            DateTime datetimeobj1 = new DateTime(2013, 12, 03, 00, 00, 00);
            Console.WriteLine(datetimeobj1 - datetimeobj);*/

            /*VacationCount test = new VacationCount()
            {
                VacationID = 1,
                Count = 555
            };

            string unitJsonStr1 = JsonSerializer.Serialize(test);
            Console.WriteLine(unitJsonStr1);*/



            /*Unit newUnit = new Unit
            {
                UnitID = 55,
                UnitName = "testunit"
            };

            string unitJsonStr = JsonSerializer.Serialize(newUnit);
            Console.WriteLine(unitJsonStr);*/
        }
    }
}