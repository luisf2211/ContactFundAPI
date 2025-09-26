using MediatR;
using ContactFundApi.Application.DTOs;
using ContactFundApi.Application.Queries;
using ContactFundApi.Domain.Interfaces;

namespace ContactFundApi.Application.Handlers;

public class GetContactsForFundQueryHandler : IRequestHandler<GetContactsForFundQuery, IEnumerable<ContactDto>>
{
    private readonly IContactRepository _contactRepository;

    public GetContactsForFundQueryHandler(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task<IEnumerable<ContactDto>> Handle(GetContactsForFundQuery request, CancellationToken cancellationToken)
    {
        var contacts = await _contactRepository.GetContactsForFundAsync(request.FundId);
        
        return contacts.Select(contact => new ContactDto
        {
            Id = contact.Id,
            Name = contact.Name,
            Email = contact.Email,
            Phone = contact.Phone
        });
    }
}
