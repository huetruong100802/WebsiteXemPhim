using BusinessObject.Models;
using System.ComponentModel;

namespace MovieWebsite.Models.Movies
{
    public class MovieViewModel:Movie
    {
        [DisplayName("Đánh giá")]
        public virtual new ICollection<Rate> Rates { get; set; } = new List<Rate>();
        [DisplayName("Danh sách tập")]
        public virtual new IEnumerable<Episode> Episodes { get; set; } = new List<Episode>();
        [DisplayName("Bình luận")]
        public virtual new IEnumerable<Comment> Comments { get; set; } = new List<Comment>();
        [DisplayName("Thể loại")]
        public virtual IEnumerable<string>? Genres { get; set; } = null!;
        [DisplayName("Trạng thái phim")]
        public virtual IEnumerable<string>? Statuses { get; set; } = null!;
        [DisplayName("Người tham gia")]
        public virtual new IEnumerable<MoviePeople>? MoviePeople { get; set; } = null!;
        public string? EpisodePath { get; set; }
        public Comment? Comment { get; set; } = null!;
        [DisplayName("Phim được đề xuất")]
        public IEnumerable<MovieSuggestModel> movieSuggests { get; set; } = new List<MovieSuggestModel>();
    }
}
