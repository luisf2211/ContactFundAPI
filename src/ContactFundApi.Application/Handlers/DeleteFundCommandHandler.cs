using MediatR;
using ContactFundApi.Application.Commands;
using ContactFundApi.Domain.Interfaces;
using ContactFundApi.Domain.Exceptions;

namespace ContactFundApi.Application.Handlers;

public class DeleteFundCommandHandler : IRequestHandler<DeleteFundCommand>
{
    private readonly IFundRepository _fundRepository;

    public DeleteFundCommandHandler(IFundRepository fundRepository)
    {
        _fundRepository = fundRepository;
    }

    public async Task Handle(DeleteFundCommand request, CancellationToken cancellationToken)
    {
        var fundExists = await _fundRepository.ExistsAsync(request.Id);
        if (!fundExists)
            throw new FundNotFoundException(request.Id);

        await _fundRepository.DeleteAsync(request.Id);
    }
}
