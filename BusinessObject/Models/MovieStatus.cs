using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class MovieStatus
    {
        public string Id { get; set; } = null!;
        public string MovieId { get; set; } = null!;

        public virtual Movie Movie { get; set; } = null!;
        public int StatusId { get; set; } = 0;
        [DisplayName("Trạng thái")]
        public virtual Status Status { get; set; } = null!;
    }
}
