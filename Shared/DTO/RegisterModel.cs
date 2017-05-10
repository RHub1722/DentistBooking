using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class RegisterModel
    {
        public int SelectedProcedure { get; set; }
        public int SelectedDoctor { get; set; }
        public DateTime SelectedTime { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string Comment { get; set; }
        public List<KeyValuePair<int, string>> Doctors { get; set; }
        public List<KeyValuePair<int, string>> Procedures { get; set; }
    }
}
