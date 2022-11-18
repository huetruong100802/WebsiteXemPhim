using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class Role
    {
        [Key]
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public virtual IEnumerable<MoviePeople> MoviePeoples { get; set; }= new List<MoviePeople>();
    }
}
