using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO
{
    public class RegisterModel
    {
        public int SelectedProcedure { get; set; }
        public int SelectedDoctor { get; set; }
        public DateTime SelectedTime { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string Phone { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [EmailAddress(ErrorMessage = "Invalid UserName Address")]
        public string Email { get; set; }
        public string Comment { get; set; }
        public List<KeyValuePair<int, string>> Doctors { get; set; }
        public List<KeyValuePair<int, string>> Procedures { get; set; }
    }
}
