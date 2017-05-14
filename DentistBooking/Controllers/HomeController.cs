using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
            TempData.Put("RegMNodel", registerModel);
            return View(registerModel);
        }

        [HttpPost]
        public IActionResult Index(RegisterModel model)
        {
            model.SelectedTime = DateTime.Parse(Request.Form["SelDate"].ToString());
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("er", "Not all required field is complete");
            }
            else if (_registerPacient.ValidateDate(model.SelectedDoctor, model.SelectedTime))
            {
                _registerPacient.Register(model);
                ViewBag.IsOk = true;
            }
            else
            {
                ModelState.AddModelError("er", "Date and time is not available");
            }
    
            var registerModel = TempData.Get<RegisterModel>("RegMNodel");
            model.Doctors = registerModel.Doctors;
            model.Procedures = registerModel.Procedures;
            TempData.Put("RegMNodel", model);

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Admin(string doc = "", string proc = "", string user = "")
        {
            AdminResultModel model = new AdminResultModel()
            {
                gridModel = _registerPacient.GetAllRegisterProcedures(doc, proc, user)
            };


            return View(model);
        }

    }
}
