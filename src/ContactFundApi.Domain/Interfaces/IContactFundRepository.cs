using ContactFundApi.Domain.Entities;

namespace ContactFundApi.Domain.Interfaces;

public interface IContactFundRepository
{
    Task AssignContactToFundAsync(int contactId, int fundId);
    Task UnassignContactFromFundAsync(int contactId, int fundId);
    Task<bool> IsContactAssignedToFundAsync(int contactId, int fundId);
}
