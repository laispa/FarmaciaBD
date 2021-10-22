using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FarmaciaBD.Controllers
{
    public class ReceitaMedicaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
