using MediatR;
using ContactFundApi.Application.DTOs;

namespace ContactFundApi.Application.Queries;

public record GetContactByIdQuery(int Id) : IRequest<ContactDto?>;

public record GetContactsForFundQuery(int FundId) : IRequest<IEnumerable<ContactDto>>;
