using Domain.FinanceGuard.Model.Commands;
using Presentation.FinanceGuard.Resources;

namespace Presentation.FinanceGuard.Transform;

public static class UpdateContactCommandFromResourceAssembler
{
    public static UpdateContactCommand ToCommandFromResource(int id, UpdateContactResource resource)
    {
        return new UpdateContactCommand(id, resource.Name, resource.Email);
    }
}