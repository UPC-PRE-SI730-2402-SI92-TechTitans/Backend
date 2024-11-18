using Domain.Security.Model.Commands;
using Domain.Security.Model.Entities;

namespace Domain.Security.Services;

public interface IUserCommandService
{
    Task<(User user, string token)> Handle(SignInCommand command);
    Task Handle(SignUpCommand command);
}