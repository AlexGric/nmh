namespace BusinessLogic.ViewModels
{
    public enum AccountStatus
    {
        Active,
        Inactive
    }

    public enum UserRole
    {
        Admin,
        Manager,
        Executor
    }

    public class SecureUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public AccountStatus AccountStatus { get; set; }
    }
}