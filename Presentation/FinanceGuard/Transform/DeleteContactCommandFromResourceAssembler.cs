using Domain.FinanceGuard.Model.Commands;
using Presentation.FinanceGuard.Resources;

namespace Presentation.FinanceGuard.Transform;

public static class DeleteContactCommandFromResourceAssembler
{
    public static DeleteContactCommand ToCommandFromResource(DeleteContactResource resouce)
    {
        return new DeleteContactCommand(resouce.Id);
    }
}