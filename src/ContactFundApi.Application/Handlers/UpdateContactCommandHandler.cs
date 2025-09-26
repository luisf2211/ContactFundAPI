using MediatR;
using ContactFundApi.Application.DTOs;
using ContactFundApi.Application.Commands;
using ContactFundApi.Domain.Entities;
using ContactFundApi.Domain.Interfaces;
using ContactFundApi.Domain.Exceptions;
using FluentValidation;

namespace ContactFundApi.Application.Handlers;

public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, ContactDto>
{
    private readonly IContactRepository _contactRepository;
    private readonly IValidator<UpdateContactDto> _validator;

    public UpdateContactCommandHandler(IContactRepository contactRepository, IValidator<UpdateContactDto> validator)
    {
        _contactRepository = contactRepository;
        _validator = validator;
    }

    public async Task<ContactDto> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.ContactDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var contact = await _contactRepository.GetByIdAsync(request.Id);
        if (contact == null)
        {
            throw new ContactNotFoundException(request.Id);
        }

        contact.Name = request.ContactDto.Name;
        contact.Email = request.ContactDto.Email;
        contact.Phone = request.ContactDto.Phone;

        await _contactRepository.UpdateAsync(contact);

        return new ContactDto
        {
            Id = contact.Id,
            Name = contact.Name,
            Email = contact.Email,
            Phone = contact.Phone
        };
    }
}
