using Domain.Security.Model.Commands;
using Presentation.Security.Resources;

namespace Presentation.Security.Transform;

/// <summary>
/// 
/// </summary>
public static class SignInCommandFromResourceAssembler
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resource"></param>
    /// <returns></returns>
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Email, resource.Password);
    }
}