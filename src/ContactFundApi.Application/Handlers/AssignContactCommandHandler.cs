using MediatR;
using ContactFundApi.Application.Commands;
using ContactFundApi.Domain.Interfaces;
using ContactFundApi.Domain.Exceptions;

namespace ContactFundApi.Application.Handlers;

public class AssignContactCommandHandler : IRequestHandler<AssignContactCommand>
{
    private readonly IContactRepository _contactRepository;
    private readonly IFundRepository _fundRepository;
    private readonly IContactFundRepository _contactFundRepository;

    public AssignContactCommandHandler(
        IContactRepository contactRepository,
        IFundRepository fundRepository,
        IContactFundRepository contactFundRepository)
    {
        _contactRepository = contactRepository;
        _fundRepository = fundRepository;
        _contactFundRepository = contactFundRepository;
    }

    public async Task Handle(AssignContactCommand request, CancellationToken cancellationToken)
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

        var isAlreadyAssigned = await _contactFundRepository.IsContactAssignedToFundAsync(request.ContactId, request.FundId);
        if (isAlreadyAssigned)
        {
            throw new ContactAlreadyAssignedException(request.ContactId, request.FundId);
        }

        await _contactFundRepository.AssignContactToFundAsync(request.ContactId, request.FundId);
    }
}
