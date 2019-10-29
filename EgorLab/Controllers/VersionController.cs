using EgorLab.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Serilog;
using Serilog.Sinks.File;

namespace EgorLab.Controllers
{
    [Route("api/version")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
        Log.Information("Acquiring version info");
           Log.Warning("Some warning");
           Log.Error("Here comes an error");
 
          
            var versionInfo = new VersionModel
            {
                Company = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyCompanyAttribute>().Company,
                Product = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyProductAttribute>().Product,
                ProductVersion = Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion
            };

            Log.Information($"Acquired version is {versionInfo.ProductVersion}");
           Log.Debug($"Full version info: {@versionInfo}");

            return Ok(versionInfo);
        }
    }
}
