using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Episode
{
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? VideoName { get; set; } = null!;

    public string MovieId { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
