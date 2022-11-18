using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Genre
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<MovieGenre> MovieGenres { get; } = new List<MovieGenre>();
}
