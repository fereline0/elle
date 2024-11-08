using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elle.Models
{
    public class Home : BaseEntity
    {
        public string Name { get; set; }
        public int CityId { get; set; }
        public ICollection<Immovable> Immovables { get; set; } = new List<Immovable>();
    }
}
