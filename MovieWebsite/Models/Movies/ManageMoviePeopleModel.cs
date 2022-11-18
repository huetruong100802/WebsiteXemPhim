namespace MovieWebsite.Models.Movies
{
    public class ManageMoviePeopleModel
    {
        public string Name { get; set; } = null!;
        public string RoleId { get; set; } = null!;
        public bool Selected { get; set; } = false;
    }
}
