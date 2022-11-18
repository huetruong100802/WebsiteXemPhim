namespace MovieWebsite.Models.Episodes
{
    public class EpisodeInputModel
    {
        public string? Id { get; set; }
        public string Title { get; set; } = null!;

        public string VideoName { get; set; } = null!;
        public string MovieId { get; set; } = null!;
        public IFormFile VideoFile { get; set; }= null!;
    }
}
