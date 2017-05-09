using System;

namespace Entities.Entities
{
    public class Pacient : Entity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public DateTime RegTime { get; set; }

        public Doctor Doctor { get; set; }
        public int DoctorId { get; set; }

        public Procedure DoctorProcedure { get; set; }
        public int DoctorProcedureId { get; set; }
    }
}