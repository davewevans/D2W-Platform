namespace D2W.WebPortal.DTOs
{
    public class B2cUserDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
        public Guid InviteBy { get; set; }
    }

    public class B2cUserJsonDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string B2cObjectId { get; set; }

        public string Password { get; set; }
    }

    public class UserEmailDto
    {
        public string Email { get; set; }
    }
}
