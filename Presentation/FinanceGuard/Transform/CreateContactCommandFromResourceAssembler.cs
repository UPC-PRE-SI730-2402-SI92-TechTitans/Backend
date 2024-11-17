using Domain.FinanceGuard.Model.Commands;
using Presentation.FinanceGuard.Resources;

namespace Presentation.FinanceGuard.Transform;

public static class CreateContactCommandFromResourceAssembler
{
    public static CreateContactCommand ToCommandFromResource(CreateContactResource resource)
    {
        return new CreateContactCommand(resource.Name, resource.Email);
    }
}