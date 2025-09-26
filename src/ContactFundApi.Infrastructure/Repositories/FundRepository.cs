using Microsoft.EntityFrameworkCore;
using ContactFundApi.Domain.Entities;
using ContactFundApi.Domain.Interfaces;
using ContactFundApi.Infrastructure.Data;

namespace ContactFundApi.Infrastructure.Repositories;

public class FundRepository : IFundRepository
{
    private readonly AppDbContext _context;

    public FundRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Fund?> GetByIdAsync(int id)
    {
        return await _context.Funds.FindAsync(id);
    }

    public async Task<IEnumerable<Fund>> GetAllAsync()
    {
        return await _context.Funds.ToListAsync();
    }

    public async Task<Fund> AddAsync(Fund fund)
    {
        _context.Funds.Add(fund);
        await _context.SaveChangesAsync();
        return fund;
    }

    public async Task UpdateAsync(Fund fund)
    {
        _context.Funds.Update(fund);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var fund = await _context.Funds.FindAsync(id);
        if (fund != null)
        {
            _context.Funds.Remove(fund);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Funds.AnyAsync(f => f.Id == id);
    }
}
