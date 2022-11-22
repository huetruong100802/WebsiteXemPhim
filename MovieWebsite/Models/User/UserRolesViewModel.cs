using System.ComponentModel;

namespace MovieWebsite.Models.User
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        [DisplayName("User name")]
        public string UserName { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Email confirm")]
        public bool EmailConfirm { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
