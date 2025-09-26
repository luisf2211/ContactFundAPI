using System.ComponentModel.DataAnnotations;

namespace ContactFundApi.Domain.Entities;

public class Fund
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    public virtual ICollection<ContactFund> ContactFunds { get; set; } = new List<ContactFund>();
}
