using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KR.Core.Mocks;
using KR.Core.Models;

namespace KR.Core.Controllers
{
    public class WorkersController : Controller
    {
        [Route("")]
        public IActionResult TitlePage()
        {
            ViewBag.Title = "Title page";
            return View();
        }


        [Route("/Workers/List/{outputDataType?}")]
        public IActionResult List(string outputDataType)
        {
            switch (outputDataType)
            {
                case "Younger30":
                    ViewBag.Title = "Список сотрудников младше 30";
                    return View(Startup.workersMock.Younger30);
                    break;

                case "ThisYearWorkers":
                    ViewBag.Title = "Список сотрудников, принятых на работу в этом году";
                    return View(Startup.workersMock.ThisYearEmployedWorkers);
                    break;

                default:
                    ViewBag.Title = "Список сотрудников";
                    return View(Startup.workersMock.Workers);
                    break;

            }

        }

        [HttpGet]
        public IActionResult Edit(ulong ID)
        {
            ViewBag.Title = "Редактирование";
            Worker worker = Startup.workersMock.Find(ID);
            ViewBag.BirthDate = WorkersMock.ConvertToDatePicker(worker.BirthDate);

            return View(worker);
        }

        [HttpPost]
        public RedirectToActionResult Edit(ulong ID, string LastName, string BirthDate, string Gender, string EmploymentDate, string Education, string Specialty, string MilitaryServiceRelation)
        {
            Worker worker = Startup.workersMock.Find(ID);
            worker.LastName = LastName;
            worker.BirthDate = Convert.ToDateTime(BirthDate);
            worker.Gender = Gender;
            worker.EmploymentDate = Convert.ToDateTime(EmploymentDate);
            worker.Education = Education;
            worker.Speciality = Specialty;
            worker.MilitaryServiceRelation = MilitaryServiceRelation;
            Startup.workersMock.SaveDB();
            Startup.workersMock.LoadDB();
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Title = "Добавить запись о сотруднике";
            return View();
        }

        [HttpPost]
        public RedirectToActionResult Add(string LastName, string BirthDate, string Gender, string EmploymentDate, string Education, string Specialty, string MilitaryServiceRelation)
        {
            Startup.workersMock.AddToDB(new Worker { LastName = LastName, BirthDate = Convert.ToDateTime(BirthDate), Gender = Gender, EmploymentDate = Convert.ToDateTime(EmploymentDate), Education = Education, Speciality = Specialty, MilitaryServiceRelation = MilitaryServiceRelation });
            Startup.workersMock.LoadDB();
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Delete(ulong ID)
        {
            ViewBag.Title = "Удалить запись о сотруднике";
            return View(Startup.workersMock.Find(ID));
        }

        [HttpPost]
        public RedirectToActionResult Delete(ulong ID, bool isAgreed)
        {
            if (isAgreed)
            {
                Startup.workersMock.DeleteFromDB(ID);
                Startup.workersMock.LoadDB();
            }
            return RedirectToAction("List");
        }

        public IActionResult GenderDivision()
        {
            ViewBag.Title = "Средний возраст мужчин и женщин";
            return View(Startup.workersMock.GenderDivision);
        }

        public IActionResult More(ulong ID)
        {
            ViewBag.Title = "О сотруднике";
            return View(Startup.workersMock.Find(ID));
        }
    }
}