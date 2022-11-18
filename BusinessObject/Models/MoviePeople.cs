using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class MoviePeople
    {
        public string Id { get; set; } = null!;
        public string PeopleId { get; set; } = null!;
        public virtual People People { get; set; } = null!;
        public string MovieId { get; set; } = null!;
        public virtual Movie Movie { get; set; } = null!;
        public string RoleId { get; set; } = null!;
        public virtual Role Roles { get; set; } = null!;
    }
}
