using Microsoft.EntityFrameworkCore;
using ContactFundApi.Domain.Entities;
using ContactFundApi.Domain.Interfaces;
using ContactFundApi.Infrastructure.Data;

namespace ContactFundApi.Infrastructure.Repositories;

public class ContactFundRepository : IContactFundRepository
{
    private readonly AppDbContext _context;

    public ContactFundRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AssignContactToFundAsync(int contactId, int fundId)
    {
        var contactFund = new ContactFund
        {
            ContactId = contactId,
            FundId = fundId
        };

        _context.ContactFunds.Add(contactFund);
        await _context.SaveChangesAsync();
    }

    public async Task UnassignContactFromFundAsync(int contactId, int fundId)
    {
        var contactFund = await _context.ContactFunds
            .FirstOrDefaultAsync(cf => cf.ContactId == contactId && cf.FundId == fundId);

        if (contactFund != null)
        {
            _context.ContactFunds.Remove(contactFund);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> IsContactAssignedToFundAsync(int contactId, int fundId)
    {
        return await _context.ContactFunds
            .AnyAsync(cf => cf.ContactId == contactId && cf.FundId == fundId);
    }
}
