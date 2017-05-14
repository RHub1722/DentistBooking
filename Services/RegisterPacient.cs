using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.Entities;
using Repository.Interfaces;
using Shared.DTO;

namespace Services
{
    public class RegisterPacient : IRegisterPacient
    {
        private readonly IRepository<Doctor> _doctors;
        private readonly IRepository<Procedure> _procedures;
        private readonly IRepository<Pacient> _pacients;

        public RegisterPacient(IRepository<Doctor> doctors, 
            IRepository<Procedure> procedures,
            IRepository<Pacient> pacients
            )
        {
            _doctors = doctors;
            _procedures = procedures;
            _pacients = pacients;
        }

        public List<KeyValuePair<int, string>> GetAllDoctors()
        {
            return _doctors.Queryable()
                .Select(x => new KeyValuePair<int, string>(x.Id, x.FirstName + " " + x.SecondName))
                .ToList();
        }

        public List<KeyValuePair<int, string>> GetProcedures()
        {
            return _procedures.Queryable()
                .Select(x => new KeyValuePair<int, string>(x.Id, x.Type)).ToList();
        }

        public bool ValidateDate(int docId, DateTime dateTime)
        {
            var fromM = dateTime.AddMinutes(-30);
            var addMinutes = dateTime.AddMinutes(30);

            var res = (from pacient in _pacients.Queryable()
                join doctor in _doctors.Queryable() on pacient.DoctorId equals doctor.Id
                where doctor.Id == docId && pacient.RegTime > fromM && pacient.RegTime < addMinutes
                select pacient.Id).Any();

            return !res;
        }

        public void Register(RegisterModel model)
        {
            var pacient = new Pacient()
            {
                DoctorId = model.SelectedDoctor,
                DoctorProcedureId = model.SelectedProcedure,
                Comment = model.Comment,
                Email = model.Email,
                Name = model.Name,
                Phone = model.Phone,
                RegTime = model.SelectedTime,
                ObjectState = ObjectState.Added
            };
            _pacients.Insert(pacient);
            _pacients.Save();
        }

        public List<ResultListModel> GetAllRegisterProcedures(string doc, string proc, string user)
        {
            var resp = (from docs in _doctors.Queryable()
                join pacient in _pacients.Queryable() on docs.Id equals pacient.DoctorId
                join procedure in _procedures.Queryable() on pacient.DoctorProcedureId equals procedure.Id
                select new ResultListModel()
                {
                    Procedure = procedure.Type,
                    PacentName = pacient.Name,
                    DoctorName = docs.FirstName + " " + docs.SecondName,
                    PacentComment = pacient.Comment,
                    PacentEmail = pacient.Email,
                    PacentPhone = pacient.Phone,
                    Time = pacient.RegTime
                });

            if (!string.IsNullOrEmpty(doc))
                resp = resp.Where(x => x.DoctorName.Contains(doc));

            if (!string.IsNullOrEmpty(user))
                resp = resp.Where(x => x.PacentName.Contains(user));

            if (!string.IsNullOrEmpty(proc))
                resp = resp.Where(x => x.Procedure.Contains(proc));

            return resp.ToList();


        }
    }
}
