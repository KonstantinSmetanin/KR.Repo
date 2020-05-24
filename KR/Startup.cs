using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using KR.Core.Mocks;

namespace KR.Core
{
    public class Startup
    {
        public static WorkersMock workersMock { get; } = new WorkersMock();

        public Startup()
        {
            workersMock.LoadDB();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseDeveloperExceptionPage();

            app.UseMvc();

            app.UseRouting();

            app.UseEndpoints( endpoints =>
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Workers}/{action=Edit}/{ID?}")
            );
        }
    }
}
