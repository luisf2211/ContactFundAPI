using ContactFundApi.Domain.Entities;

namespace ContactFundApi.Domain.Interfaces;

public interface IContactRepository
{
    Task<Contact?> GetByIdAsync(int id);
    Task<IEnumerable<Contact>> GetAllAsync();
    Task<Contact> AddAsync(Contact contact);
    Task UpdateAsync(Contact contact);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<bool> IsAssignedToAnyFundAsync(int contactId);
    Task<bool> IsAssignedToFundAsync(int contactId, int fundId);
    Task<IEnumerable<Contact>> GetContactsForFundAsync(int fundId);
}

public interface IFundRepository
{
    Task<Fund?> GetByIdAsync(int id);
    Task<IEnumerable<Fund>> GetAllAsync();
    Task<Fund> AddAsync(Fund fund);
    Task UpdateAsync(Fund fund);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
