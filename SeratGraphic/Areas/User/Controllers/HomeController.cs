using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SeratGraphic.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        [Route("user")]
        public IActionResult Index()
        {
            return View();
        }
    }
}