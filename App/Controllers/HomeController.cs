using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace App.Controllers
{
    [Route("health")]
    [Authorize("Permission:SuperAdmin")]
    public class HomeController : Controller
    {
        public IActionResult Index() => Ok();
    }
}
