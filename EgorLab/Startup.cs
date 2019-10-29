using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EgorLab.Configuration;
using EgorLab.Models;
using EgorLab.Models.StorageModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Sinks.File;
using System.Reflection;


namespace EgorLab
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
             {
           services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
 
           ConfigureLogger();
       } 
            services.AddSingleton<IStorage<Person>, MemStorage>();

            switch (Configuration["Storage:Type"].ToStorageEnum())
            {
                case StorageEnum.MemCache:
                    services.AddSingleton<IStorage<Person>, MemStorage>();
                    break;
                case StorageEnum.FileStorage:
                    services.AddSingleton<IStorage<Person>>(
                        x => new FileStorage(Configuration["Storage:FileStorage:Filename"], int.Parse(Configuration["Storage:FileStorage:FlushPeriod"])));
                    break;
                default:
                    throw new IndexOutOfRangeException($"Storage type '{Configuration["Storage:Type"]}' is unknown");
            }
            
        }
            
private void ConfigureLogger()
       {
           var log = new LoggerConfiguration()
               .WriteTo.Console()
               .WriteTo.File("logs\\egorLab.log", rollingInterval: RollingInterval.Day)
               .CreateLogger();
 
           Log.Logger = log;
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
