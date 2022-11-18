using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class ApplicationUser:IdentityUser
{
    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<Rate> Rates { get; } = new List<Rate>();
}
