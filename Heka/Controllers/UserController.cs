using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heka.UI.Controllers
{
    public class UserController : Controller
    {
        public IActionResult AdminPanel()
        {
            return View();
        }
    }
}
