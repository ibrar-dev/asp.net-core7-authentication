namespace AuthenticationApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int PhoneNumber { get; set; }
        public string Role { get; set; } = "User";
        public bool IsActive { get; set; } = false;
        public List<Book> Books { get; set; }

    }
}
