namespace ContactFundApi.Domain.Entities;

public class ContactFund
{
    public int ContactId { get; set; }
    public int FundId { get; set; }
    
    public virtual Contact Contact { get; set; } = null!;
    public virtual Fund Fund { get; set; } = null!;
}
