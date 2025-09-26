using MediatR;
using ContactFundApi.Application.DTOs;
using ContactFundApi.Application.Queries;
using ContactFundApi.Domain.Interfaces;

namespace ContactFundApi.Application.Handlers;

public class GetAllFundsQueryHandler : IRequestHandler<GetAllFundsQuery, IEnumerable<FundDto>>
{
    private readonly IFundRepository _fundRepository;

    public GetAllFundsQueryHandler(IFundRepository fundRepository)
    {
        _fundRepository = fundRepository;
    }

    public async Task<IEnumerable<FundDto>> Handle(GetAllFundsQuery request, CancellationToken cancellationToken)
    {
        var funds = await _fundRepository.GetAllAsync();
        return funds.Select(fund => new FundDto
        {
            Id = fund.Id,
            Name = fund.Name
        });
    }
}
