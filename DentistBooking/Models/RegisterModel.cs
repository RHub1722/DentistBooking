using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentistBooking.Models
{
    public class RegisterModel
    {
        public int SelectedProcedure { get; set; }
        public int SelectedDoctor { get; set; }
        public DateTime SelectedTime { get; set; }

        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }

    }
}
