using System;
using Microsoft.AspNetCore.Mvc;
using Calculator.Models;
using CalcLibrary;

namespace Calculator.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Expression model)
        {
            
            return View(model);
        }
    }
}
