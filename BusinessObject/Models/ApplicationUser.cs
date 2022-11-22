using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BusinessObject.Models;

public partial class ApplicationUser:IdentityUser
{
    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<Rate> Rates { get; } = new List<Rate>(); 
    [DisplayName("Tủ phim")]
    public virtual ICollection<FollowedMovie> FollowedMovies { get; set; } = new List<FollowedMovie>();
}
