using System.Data;
using Domain.Contact.Model.Commands;
using Domain.Contact.Model.Entities;
using Domain.Contact.Repositories;
using Domain.Contact.Services;
using Domain.Shared;

namespace Application.Contact.CommandServices;

public class ContactCommandService : IContactCommandService
{
    private readonly IContactRepository _contactRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ContactCommandService(IContactRepository contactRepository, IUnitOfWork unitOfWork)
    {
        _contactRepository = contactRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateContactCommand command)
    {
        if (command.Name.Length == 0) throw new Exception("Contact name mut be more than 0");

        var contact = new Contacto()
        {
            Name = command.Name,
            Email = command.Email
        };

        var existingContact = await _contactRepository.FindEmailAsync(contact.Email);

        if (existingContact != null) throw new DuplicateNameException("Duplicate email");

        await _contactRepository.AddAsync(contact);
        await _unitOfWork.CompleteAsync();
        return contact.Id;
    }

    public async Task<bool> Handle(UpdateContactCommand command)
    {
        var existingContact = await _contactRepository.FindByIdAsync(command.Id);
        if (existingContact == null)
        {
            return false;
        }

        existingContact.Name = command.Name;
        existingContact.Email = command.Email;

        await _contactRepository.UpdateAsync(existingContact);
        await _unitOfWork.CompleteAsync();

        return true;
    }

    public async Task<bool> Handle(DeleteContactCommand command)
    {
        var contact = await _contactRepository.FindByIdAsync(command.Id);

        await _contactRepository.RemoveAsync(contact);
        await _unitOfWork.CompleteAsync();

        return true;
    }
}