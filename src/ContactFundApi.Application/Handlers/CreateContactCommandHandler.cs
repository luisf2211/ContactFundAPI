using MediatR;
using ContactFundApi.Application.DTOs;
using ContactFundApi.Application.Commands;
using ContactFundApi.Domain.Entities;
using ContactFundApi.Domain.Interfaces;
using ContactFundApi.Domain.Exceptions;
using FluentValidation;

namespace ContactFundApi.Application.Handlers;

public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, ContactDto>
{
    private readonly IContactRepository _contactRepository;
    private readonly IValidator<CreateContactDto> _validator;

    public CreateContactCommandHandler(IContactRepository contactRepository, IValidator<CreateContactDto> validator)
    {
        _contactRepository = contactRepository;
        _validator = validator;
    }

    public async Task<ContactDto> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.ContactDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var contact = new Contact
        {
            Name = request.ContactDto.Name,
            Email = request.ContactDto.Email,
            Phone = request.ContactDto.Phone
        };

        var createdContact = await _contactRepository.AddAsync(contact);
        
        return new ContactDto
        {
            Id = createdContact.Id,
            Name = createdContact.Name,
            Email = createdContact.Email,
            Phone = createdContact.Phone
        };
    }
}
