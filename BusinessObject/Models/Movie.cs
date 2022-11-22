using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models;

public partial class Movie
{
    public string Id { get; set; } = null!;
    [DisplayName("Tựa đề")]
    public string Title { get; set; } = null!;
    [DisplayName("Cover")]
    public string? ImageName { get; set; } = null!;
    [DisplayName("Đánh giá")]
    public float? Rating { get; set; } = 0;
    [DisplayName("Đã xóa")]
    public bool Deleted { get; set; }= false;
    [DisplayName("Nội dung")]
    public string? Description { get; set; } = null!;
    [DisplayName("Năm phát hành")]
    public int? ReleaseYear { get; set; }
    [DisplayName("Bình luận")]
    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();
    public virtual ICollection<Episode> Episodes { get; } = new List<Episode>();
    [DisplayName("Thể loại")]
    public virtual ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
    [DisplayName("Đánh giá")]
    public virtual ICollection<Rate> Rates { get; } = new List<Rate>();
    [DisplayName("Thời lượng")]
    public string? Duration { get; set; } = null!;
    [DisplayName("Quốc gia")]
    public string? Nation { get; set; } = null!;
    public virtual ICollection<MoviePeople> MoviePeople { get; } = new List<MoviePeople>();
    public virtual ICollection<MovieStatus> MovieStatuses { get; } = new List<MovieStatus>();
    [DisplayName("Tổng lượt xem")]
    public int Views { get; set; } = 0; 
    [DisplayName("Tủ phim")]
    public virtual ICollection<FollowedMovie> FollowedMovies { get; set; } = new List<FollowedMovie>();
}