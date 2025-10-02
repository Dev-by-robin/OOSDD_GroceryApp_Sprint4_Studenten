
namespace Grocery.Core.Models
{
    public partial class Client : Model
    {
        // enum rollen
        public enum Role
        {
            None,
            Admin
        }
        public Role UserRole { get; set; } = Role.None;

        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public Client(int id, string name, string emailAddress, string password, Role userRole) : base(id, name)
        {
            EmailAddress=emailAddress;
            Password=password;
            UserRole = userRole;
        }
    }
}
