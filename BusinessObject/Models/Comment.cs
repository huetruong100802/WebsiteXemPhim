using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public partial class Comment
{
    public string Id { get; set; } = null!;
    public string CommentDetail { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string UserId { get; set; } = null!;

    public string MovieId { get; set; } = null!;

    public virtual Movie Movie { get; set; } = null!;

    public virtual ApplicationUser User { get; set; } = null!;
}
