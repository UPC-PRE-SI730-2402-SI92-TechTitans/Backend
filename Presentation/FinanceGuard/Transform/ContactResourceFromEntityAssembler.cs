using Domain.FinanceGuard.Model.Entities;
using Presentation.FinanceGuard.Resources;

namespace Presentation.FinanceGuard.Transform;

public static class ContactResourceFromEntityAssembler
{
    public static ContactResource ToResourceFromEntity(Contact entity) =>
        new ContactResource(entity.Id, entity.Name, entity.Email);
}