using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;
using VacationRental.Core.Mapper;
using VacationRental.Core.Data.Interfaces;
using VacationRental.Core.Data.DTO;
using VacationRental.Core.Data.Repositories;
using VacationRental.Core.Services;
using VacationRental.Api.Services.Interfaces;
using VacationRental.Core.Services.Interfaces;

namespace VacationRental.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(opts => opts.SwaggerDoc("v1", new Info { Title = "Vacation rental information", Version = "v1" }));
            services.AddAutoMapper(typeof(DataMapperProfile)); // assembly
            services.AddSingleton<IDictionary<int, RentalDTO>>(new Dictionary<int, RentalDTO>());
            services.AddSingleton<IDictionary<int, BookingDTO>>(new Dictionary<int, BookingDTO>());
            services.AddSingleton<IDataProvider<BookingDTO>, BookingRepository>();
            services.AddSingleton<IDataProvider<RentalDTO>, RentalRepository>();
            services.AddSingleton<IDataProvider<CalendarDTO>, CalendarRepository>();
            services.AddSingleton<IBookingService, BookingService>();
            services.AddSingleton<IRentalService, RentalService>();
            services.AddSingleton<ICalendarService, CalendarService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(opts => opts.SwaggerEndpoint("/swagger/v1/swagger.json", "VacationRental v1"));
        }
    }
}
