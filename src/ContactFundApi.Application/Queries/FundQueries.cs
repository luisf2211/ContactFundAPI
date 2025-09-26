using ContactFundApi.Application.DTOs;
using MediatR;

namespace ContactFundApi.Application.Queries;

public record GetFundByIdQuery(int Id) : IRequest<FundDto?>;
public record GetAllFundsQuery() : IRequest<IEnumerable<FundDto>>;
