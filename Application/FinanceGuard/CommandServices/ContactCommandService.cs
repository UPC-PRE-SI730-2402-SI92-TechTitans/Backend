using System.Data;
using Domain.FinanceGuard.Model.Commands;
using Domain.FinanceGuard.Model.Entities;
using Domain.FinanceGuard.Repositories;
using Domain.FinanceGuard.Services;
using Domain.Shared;

namespace Application.FinanceGuard.CommandServices;

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
        var contact = new Contact()
        {
            Name = command.Name,
            Email = command.Email
        };

        var existingContact = await _contactRepository.FindByEmailAsync(contact.Email);

        if (existingContact != null) throw new DuplicateNameException("Email duplicated name");

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