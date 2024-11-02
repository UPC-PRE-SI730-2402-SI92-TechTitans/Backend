using Domain.Shared.Model.Entities;

namespace Domain.Contact.Model.Entities;

public class Contacto : ModelBase
{
    public string Name { get; set; }
    public string Email { get; set; }
}