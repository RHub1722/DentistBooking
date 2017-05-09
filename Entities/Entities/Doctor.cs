using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Entities
{
    public class Doctor : Entity
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }

        public ICollection<Pacient>  Pacients { get; set; } = new List<Pacient>();
    }
}
