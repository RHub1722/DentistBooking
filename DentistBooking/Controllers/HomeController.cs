using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;
using Shared.DTO;

namespace DentistBooking.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRegisterPacient _registerPacient;

        public HomeController(IRegisterPacient registerPacient)
        {
            _registerPacient = registerPacient;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var doctors = _registerPacient.GetAllDoctors();
            var procedures = _registerPacient.GetProcedures();
            var registerModel = new RegisterModel()
            {
                Doctors = doctors,
                Procedures = procedures

            };
            return View(registerModel);
        }

        [HttpPost]
        public IActionResult Index(RegisterModel model)
        {
            model.SelectedTime = DateTime.Parse(Request.Form["SelDate"].ToString());
            if (_registerPacient.ValidateDate(model.SelectedDoctor, model.SelectedTime))
            {
                _registerPacient.Register(model);
            }
            else
            {
                //todo: return error;
            }

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
