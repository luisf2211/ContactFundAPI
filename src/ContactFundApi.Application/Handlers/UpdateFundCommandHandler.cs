using MediatR;
using ContactFundApi.Application.DTOs;
using ContactFundApi.Application.Commands;
using ContactFundApi.Domain.Entities;
using ContactFundApi.Domain.Interfaces;
using ContactFundApi.Domain.Exceptions;

namespace ContactFundApi.Application.Handlers;

public class UpdateFundCommandHandler : IRequestHandler<UpdateFundCommand, FundDto>
{
    private readonly IFundRepository _fundRepository;

    public UpdateFundCommandHandler(IFundRepository fundRepository)
    {
        _fundRepository = fundRepository;
    }

    public async Task<FundDto> Handle(UpdateFundCommand request, CancellationToken cancellationToken)
    {
        var fund = await _fundRepository.GetByIdAsync(request.Id);
        if (fund == null)
            throw new FundNotFoundException(request.Id);

        fund.Name = request.UpdateFundDto.Name;
        await _fundRepository.UpdateAsync(fund);

        return new FundDto
        {
            Id = fund.Id,
            Name = fund.Name
        };
    }
}
