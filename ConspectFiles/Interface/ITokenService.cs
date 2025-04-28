using ConspectFiles.Model;

namespace ConspectFiles.Interface
{
    public interface ITokenService
    {
        string GenerateRefreshToken();
        bool ValidateRefreshToken(string refreshToken);
        string GenerateJwtToken(AppUser user);
    }
}