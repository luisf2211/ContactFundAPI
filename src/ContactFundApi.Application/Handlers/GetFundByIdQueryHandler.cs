using MediatR;
using ContactFundApi.Application.DTOs;
using ContactFundApi.Application.Queries;
using ContactFundApi.Domain.Interfaces;

namespace ContactFundApi.Application.Handlers;

public class GetFundByIdQueryHandler : IRequestHandler<GetFundByIdQuery, FundDto?>
{
    private readonly IFundRepository _fundRepository;

    public GetFundByIdQueryHandler(IFundRepository fundRepository)
    {
        _fundRepository = fundRepository;
    }

    public async Task<FundDto?> Handle(GetFundByIdQuery request, CancellationToken cancellationToken)
    {
        var fund = await _fundRepository.GetByIdAsync(request.Id);
        if (fund == null)
            return null;

        return new FundDto
        {
            Id = fund.Id,
            Name = fund.Name
        };
    }
}
