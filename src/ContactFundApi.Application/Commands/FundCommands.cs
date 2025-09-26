using ContactFundApi.Application.DTOs;
using MediatR;

namespace ContactFundApi.Application.Commands;

public record CreateFundCommand(CreateFundDto CreateFundDto) : IRequest<FundDto>;
public record UpdateFundCommand(int Id, UpdateFundDto UpdateFundDto) : IRequest<FundDto>;
public record DeleteFundCommand(int Id) : IRequest;
