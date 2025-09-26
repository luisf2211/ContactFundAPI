using MediatR;
using ContactFundApi.Application.Commands;
using ContactFundApi.Domain.Interfaces;
using ContactFundApi.Domain.Exceptions;

namespace ContactFundApi.Application.Handlers;

public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand>
{
    private readonly IContactRepository _contactRepository;

    public DeleteContactCommandHandler(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        var contact = await _contactRepository.GetByIdAsync(request.Id);
        if (contact == null)
        {
            throw new ContactNotFoundException(request.Id);
        }

        var isAssigned = await _contactRepository.IsAssignedToAnyFundAsync(request.Id);
        if (isAssigned)
        {
            throw new ContactCannotBeDeletedException(request.Id);
        }

        await _contactRepository.DeleteAsync(request.Id);
    }
}
