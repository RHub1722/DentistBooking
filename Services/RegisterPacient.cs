﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Entities;
using Repository.Interfaces;

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
            var addMinutes = dateTime.AddMinutes(30);
            var res = (from pacient in _pacients.Queryable()
                join doctor in _doctors.Queryable() on pacient.DoctorId equals doctor.Id
                where doctor.Id == docId && pacient.RegTime > dateTime && pacient.RegTime < addMinutes
                select pacient.Id).Any();

            return !res;
        }

    }
}