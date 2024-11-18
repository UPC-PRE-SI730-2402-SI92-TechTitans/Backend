using System.Data;
using System.Runtime.CompilerServices;
using Domain.Security.Model.Commands;
using Domain.Security.Model.Entities;
using Domain.Security.Repositories;
using Domain.Security.Services;
using Domain.Shared;

namespace Application.Security.CommandServices;

public class UserCommandService(IUserRepository userRepository, IUnitOfWork unitOfWork, IEncryptService encryptService,ITokenService tokenService)
    : IUserCommandService
{
    public async Task<(User user, string token)> Handle(SignInCommand command)
    {
        var existingemail = await userRepository.FindByusermail(command.Email);
    
        if (existingemail == null) 
            throw new DataException("Invalid password or email");
    
        var isValidPassword = encryptService.Verify(command.Password, existingemail.PasswordHash);
    
        if (!isValidPassword) 
            throw new DataException("Invalid password or email");
    
        var token = tokenService.GenerateToken(existingemail);

        return (existingemail, token);
    }

    public async Task Handle(SignUpCommand command)
    {
        var user = new User
        {
            Email = command.Email,
            PasswordHash = encryptService.Encrypt(command.Password),
        };

        await userRepository.AddAsync(user);
        await unitOfWork.CompleteAsync();
    }
}