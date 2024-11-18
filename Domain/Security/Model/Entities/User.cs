using Domain.Shared.Model.Entities;

namespace Domain.Security.Model.Entities;

public class User : ModelBase
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
}