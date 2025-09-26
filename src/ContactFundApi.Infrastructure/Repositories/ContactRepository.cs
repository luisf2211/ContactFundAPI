using Microsoft.EntityFrameworkCore;
using ContactFundApi.Domain.Entities;
using ContactFundApi.Domain.Interfaces;
using ContactFundApi.Infrastructure.Data;

namespace ContactFundApi.Infrastructure.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly AppDbContext _context;

    public ContactRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Contact?> GetByIdAsync(int id)
    {
        return await _context.Contacts.FindAsync(id);
    }

    public async Task<IEnumerable<Contact>> GetAllAsync()
    {
        return await _context.Contacts.ToListAsync();
    }

    public async Task<Contact> AddAsync(Contact contact)
    {
        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();
        return contact;
    }

    public async Task UpdateAsync(Contact contact)
    {
        _context.Contacts.Update(contact);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var contact = await _context.Contacts.FindAsync(id);
        if (contact != null)
        {
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Contacts.AnyAsync(c => c.Id == id);
    }

    public async Task<bool> IsAssignedToAnyFundAsync(int contactId)
    {
        return await _context.ContactFunds.AnyAsync(cf => cf.ContactId == contactId);
    }

    public async Task<bool> IsAssignedToFundAsync(int contactId, int fundId)
    {
        return await _context.ContactFunds.AnyAsync(cf => cf.ContactId == contactId && cf.FundId == fundId);
    }

    public async Task<IEnumerable<Contact>> GetContactsForFundAsync(int fundId)
    {
        return await _context.Contacts
            .Where(c => c.ContactFunds.Any(cf => cf.FundId == fundId))
            .ToListAsync();
    }
}
