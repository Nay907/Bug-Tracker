namespace capstone_backend.Models
{
    public class User
    {
        public int UserId { get; set; } 
        public string Username { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string Access {  get; set; } 
    }
}
