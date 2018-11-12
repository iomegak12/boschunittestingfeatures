using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bosch.Libraries.Business.Impl;
using Bosch.Libraries.Business.Interfaces;
using Bosch.Libraries.Business.Validations.Impl;
using Bosch.Libraries.Business.Validations.Interfaces;
using Bosch.Libraries.DAL.Impl;
using Bosch.Libraries.DAL.Interfaces;
using Bosch.Libraries.ORM.Impl;
using Bosch.Libraries.ORM.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CRMSystemServicesHosting
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
            var encodedConnectionString = Environment.GetEnvironmentVariable("CRMSystemDBConnectionString");

            if (string.IsNullOrEmpty(encodedConnectionString))
                throw new ApplicationException("Invalid Connection String Specifid in ENV!");

            var connectionString = Encoding.ASCII.GetString(
                Convert.FromBase64String(encodedConnectionString));

            services.AddDbContext<CustomersContext>(
                dbContextOptionsBuilder =>
                {
                    dbContextOptionsBuilder.UseSqlServer(connectionString);
                });

            services.AddScoped<ICustomersContext, CustomersContext>();
            services.AddScoped<ICustomersRepository, CustomersRepository>();
            services.AddScoped<ICustomerNameValidation, CustomerNameValidation>();
            services.AddScoped<ICustomerValidation, CustomerValidation>();
            services.AddScoped<ICustomersBusinessComponent, CustomersBusinessComponent>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
