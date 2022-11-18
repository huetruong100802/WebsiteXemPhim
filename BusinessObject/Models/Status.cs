using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public virtual ICollection<MovieStatus> MovieStatuses { get; } = new List<MovieStatus>();
    }
}
