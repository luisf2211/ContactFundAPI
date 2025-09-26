using MediatR;
using ContactFundApi.Application.DTOs;

namespace ContactFundApi.Application.Commands;

public record CreateContactCommand(CreateContactDto ContactDto) : IRequest<ContactDto>;

public record UpdateContactCommand(int Id, UpdateContactDto ContactDto) : IRequest<ContactDto>;

public record DeleteContactCommand(int Id) : IRequest;

public record AssignContactCommand(int ContactId, int FundId) : IRequest;

public record UnassignContactCommand(int ContactId, int FundId) : IRequest;
