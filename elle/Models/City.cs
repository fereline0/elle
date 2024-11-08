using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elle.Models
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Home> Homes { get; set; } = new List<Home>();
    }
}
