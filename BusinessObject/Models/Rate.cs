using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Rate
{
    public string Id { get; set; } = null!;

    public string MovieId { get; set; } = null!;

    public string? UserId { get; set; }

    public int? Rating { get; set; }

    public virtual Movie Movie { get; set; } = null!;

    public virtual ApplicationUser? User { get; set; }
}
