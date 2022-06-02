namespace SatisfactoryPlanner.API.Modules.UserAccess
{
    public class RegisterNewUserRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string ConfirmLink { get; set; }
    }
}
