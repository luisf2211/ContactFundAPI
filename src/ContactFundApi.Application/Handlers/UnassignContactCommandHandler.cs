using MediatR;
using ContactFundApi.Application.Commands;
using ContactFundApi.Domain.Interfaces;
using ContactFundApi.Domain.Exceptions;

namespace ContactFundApi.Application.Handlers;

public class UnassignContactCommandHandler : IRequestHandler<UnassignContactCommand>
{
    private readonly IContactRepository _contactRepository;
    private readonly IFundRepository _fundRepository;
    private readonly IContactFundRepository _contactFundRepository;

    public UnassignContactCommandHandler(
        IContactRepository contactRepository,
        IFundRepository fundRepository,
        IContactFundRepository contactFundRepository)
    {
        _contactRepository = contactRepository;
        _fundRepository = fundRepository;
        _contactFundRepository = contactFundRepository;
    }

    public async Task Handle(UnassignContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await _contactRepository.GetByIdAsync(request.ContactId);
        if (contact == null)
        {
            throw new ContactNotFoundException(request.ContactId);
        }

        var fund = await _fundRepository.GetByIdAsync(request.FundId);
        if (fund == null)
        {
            throw new FundNotFoundException(request.FundId);
        }

        var isAssigned = await _contactFundRepository.IsContactAssignedToFundAsync(request.ContactId, request.FundId);
        if (!isAssigned)
        {
            throw new ContactNotFoundException(request.ContactId); // Contact not assigned to this fund
        }

        await _contactFundRepository.UnassignContactFromFundAsync(request.ContactId, request.FundId);
    }
}
