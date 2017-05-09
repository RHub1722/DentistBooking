using System.Collections.Generic;

namespace Entities.Entities
{
    public class Procedure : Entity
    {
        public string Type { get; set; }
       
        public ICollection<Pacient> Pacients { get; set; } = new List<Pacient>();
    }
}