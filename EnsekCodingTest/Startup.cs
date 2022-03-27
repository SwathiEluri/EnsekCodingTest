using EnsekCodingTest.Data;
using EnsekCodingTest.Mappers;
using EnsekCodingTest.Models;
using EnsekCodingTest.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace EnsekCodingTest
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
            services.AddDbContext<EnsekDbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("Default")));
            
            services.AddScoped<IDbInitialiser, DbInitialiser>();
            services.AddScoped<IMeterReadingRepository, MeterReadingRepository>();
            services.AddAutoMapper(typeof(TestAccountDtoMapper));
            services.AddControllers();

            //Swagger Integration for the Api
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EnsekCodingTest", Version = "V1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IDbInitialiser db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EnsekCodingTest V1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            db.LoadTestCsvToDb();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
