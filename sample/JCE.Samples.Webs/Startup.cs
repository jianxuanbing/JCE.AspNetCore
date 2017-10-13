using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JCE.Logs.Exceptionless;
using JCE.Logs.NLog;
using JCE.Utils.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using JCE.Logs.Log4Net;

namespace JCE.Samples.Webs
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //services.AddNLog();
            services.AddLog4Net();
            //services.AddExceptionless(config =>
            //{
            //    config.ApiKey = "CqcBoQlNP1FBxCWLe0o5ZpX3eSmB3JqK4QUvDGUw";
            //    config.ServerUrl = "http://192.168.88.20:10240";
            //});
            var serviceProvider = services.AddJce();            

            var accessor = serviceProvider.GetService<IHttpContextAccessor>();
            Web.SetHttpContextAccessor(accessor);

            return serviceProvider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            ConfigRoute(app);
        }

        /// <summary>
        /// 路由配置，支持区域
        /// </summary>
        /// <param name="app">应用生成器</param>
        private void ConfigRoute(IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute("areaRoute", "{area:exists}/{controller}/{action=Index}/{id?}");
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
