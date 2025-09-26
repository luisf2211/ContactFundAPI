using MediatR;
using ContactFundApi.Application.DTOs;
using ContactFundApi.Application.Commands;
using ContactFundApi.Domain.Entities;
using ContactFundApi.Domain.Interfaces;

namespace ContactFundApi.Application.Handlers;

public class CreateFundCommandHandler : IRequestHandler<CreateFundCommand, FundDto>
{
    private readonly IFundRepository _fundRepository;

    public CreateFundCommandHandler(IFundRepository fundRepository)
    {
        _fundRepository = fundRepository;
    }

    public async Task<FundDto> Handle(CreateFundCommand request, CancellationToken cancellationToken)
    {
        var fund = new Fund
        {
            Name = request.CreateFundDto.Name
        };

        var createdFund = await _fundRepository.AddAsync(fund);

        return new FundDto
        {
            Id = createdFund.Id,
            Name = createdFund.Name
        };
    }
}
