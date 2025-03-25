namespace ConspectFiles.Dto.AppUSer
{
    public class NewUserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Token {get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}