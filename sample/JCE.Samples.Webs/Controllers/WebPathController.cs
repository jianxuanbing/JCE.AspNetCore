using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JCE.Utils.Helpers;
using Microsoft.AspNetCore.Mvc;
using JCE.Utils.Configs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JCE.Samples.Webs.Controllers
{
    [Route("api/[controller]")]
    public class WebPathController : Controller
    {
        [HttpGet("[action]")]
        public string GetWebRootPath()
        {
            return Web.HostingEnvironment.WebRootPath;
        }

        [HttpGet("[action]")]
        public string GetContentRootPath()
        {
            return Web.HostingEnvironment.ContentRootPath;
        }

        [HttpGet("[action]")]
        public string GetConsoleLogLevel()
        {
            return ConfigUtil.GetJsonConfig().GetSection("Logging:Console:LogLevel:Default").Value;
        }
    }
}
