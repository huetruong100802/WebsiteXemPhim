using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieWebsite.Models.Movies
{
    public class MovieInputModel
    {
        public string Id { get; set; } = null!;
        [DisplayName("Tựa đề")]
        public string Title { get; set; }
        [DisplayName("Cover")]
        public string? ImageName { get; set; } = null!;
        [DisplayName("Thể loại")]
        public virtual List<ManageMovieGenreViewModel> Genres { get; set; }=null!;

        [NotMapped]
        [DisplayName("Cover")]
        public IFormFile ImageFile { get; set; }

        [DisplayName("Nội dung")]
        public string? Description { get; set; } = null!;
        [DisplayName("Năm phát hành")]
        [Range(1906,3000)]
        public int? ReleaseYear { get; set; }
        [DisplayName("Thời lượng")]
        public string? Duration { get; set; } = null!;
        [DisplayName("Quốc gia")]
        public string? Nation { get; set; } = null!;

        public virtual List<ManageMoviePeopleModel> MoviePeople { get; set; } = null!;
        public virtual List<ManageMovieStatusModel> MovieStatuses { get; set; } = null!;
    }
}
