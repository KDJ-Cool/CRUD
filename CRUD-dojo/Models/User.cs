namespace CRUD_dojo.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }
    }

    public enum Roles
    {
        User,
        Admin
    }
}