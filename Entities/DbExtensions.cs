using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Entities;
using Microsoft.AspNetCore.Builder;


namespace Entities
{
    public static class DbExtensions
    {
        public static void Seed(this IApplicationBuilder app)
        {
            var context = (EFContext) app.ApplicationServices.GetService(typeof(EFContext));//app.ApplicationServices.GetService<EFContext>();
            if (context.Doctors.Any())
                return;

            var random = new Random();

            Doctor[] doctors = new[]
            {
                new Doctor() {FirstName = "Daniel", SecondName = "Geghard", ObjectState = ObjectState.Added},
                new Doctor() {FirstName = "Geghard", SecondName = "Hagop", ObjectState = ObjectState.Added},
                new Doctor() {FirstName = "Vartan", SecondName = "Ari", ObjectState = ObjectState.Added},
                new Doctor() {FirstName = "Priscus", SecondName = "Agrippa", ObjectState = ObjectState.Added},
                new Doctor() {FirstName = "Gaios", SecondName = "Hesiod", ObjectState = ObjectState.Added},
            };

            context.Doctors.AddRange(doctors);

            Procedure[] procedures = new[]
            {
                new Procedure(){Type = "A", ObjectState = ObjectState.Added},
                new Procedure(){Type = "B", ObjectState = ObjectState.Added},
                new Procedure(){Type = "C", ObjectState = ObjectState.Added},
                new Procedure(){Type = "D", ObjectState = ObjectState.Added},
                new Procedure(){Type = "E", ObjectState = ObjectState.Added},
            };

            context.Procedures.AddRange(procedures);

            Pacient[] pacients = new[]
            {
                new Pacient()
                {
                    Doctor = doctors[1],
                    DoctorProcedure = procedures[1],
                    Comment = "123",
                    Name = "Name1",
                    Email = "1@mail.com",
                    RegTime = DateTime.Now.AddDays(random.Next(2)).AddHours(random.Next(24))
                },
                new Pacient()
                {
                    Doctor = doctors[2],
                    DoctorProcedure = procedures[2],
                    Comment = "123",
                    Name = "Name2",
                    Email = "2@mail.com",
                    RegTime = DateTime.Now.AddDays(random.Next(2)).AddHours(random.Next(24))
                },
                new Pacient()
                {
                    Doctor = doctors[3],
                    DoctorProcedure = procedures[3],
                    Comment = "123",
                    Name = "Name3",
                    Email = "3@mail.com",
                    RegTime = DateTime.Now.AddDays(random.Next(2)).AddHours(random.Next(24))
                },
                new Pacient()
                {
                    Doctor = doctors[1],
                    DoctorProcedure = procedures[3],
                    Comment = "123",
                    Name = "Name4",
                    Email = "4@mail.com",
                    RegTime = DateTime.Now.AddDays(random.Next(2)).AddHours(random.Next(24))
                },
            };
            context.Pacients.AddRange();

            context.SaveChanges();
        }
    }
}
