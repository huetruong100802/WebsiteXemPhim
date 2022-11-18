using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class MovieGenre
{
    public string Id { get; set; } = null!;

    public string MovieId { get; set; } = null!;

    public string GenreId { get; set; } = null!;

    public virtual Genre Genre { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;
}
