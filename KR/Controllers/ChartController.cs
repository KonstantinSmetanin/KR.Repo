using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KR.Core.Mocks;

namespace KR.Core.Controllers
{
    public class ChartController : Controller
    {
        public IActionResult AgeEducation()
        {
            ViewBag.Title = "Графики изменения уровня образования от возраста";
            return View();
        }

        [HttpGet]
        public JsonResult GetAgeEducationDivisionChartData() => Json(Startup.workersMock.AgeEducationDivision);

        public IActionResult Age()
        {
            ViewBag.Title = "Возрастное соотношение сотрудников";
            return View();
        }

        [HttpGet]
        public JsonResult GetAgeChartData() => Json(Startup.workersMock.AgeDivisionChartData);

        public IActionResult Education()
        {
            ViewBag.Title = "Соотношение сотрудников относительно уровня образования";
            return View();
        }

        [HttpGet]
        public JsonResult GetEducationChartData() => Json(Startup.workersMock.EducationDivisionChartData);

    }
}