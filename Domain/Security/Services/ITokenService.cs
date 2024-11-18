using Domain.Security.Model.Entities;

namespace Domain.Security.Services;

public interface ITokenService
{
    string GenerateToken(User user);
    User? ValidateToken(string token);
}