﻿using Microsoft.AspNetCore.Mvc;

namespace nosqllllll.Controllers
{
        public class HomeController : Controller
        {
            public IActionResult Index()
            {
                return RedirectToAction("Index", "Telebe");
            }
        }

}
