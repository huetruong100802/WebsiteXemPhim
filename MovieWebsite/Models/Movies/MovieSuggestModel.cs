using BusinessObject.Models;

namespace MovieWebsite.Models.Movies
{
    public class MovieSuggestModel
    {
        public string Title { get; set; } = null!;
        public IEnumerable<Movie> MoviesSuggestion { get; set; } = null!;
    }
}
