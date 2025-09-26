using System.ComponentModel.DataAnnotations;

namespace ContactFundApi.Domain.Entities;

public class Contact
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(150)]
    public string? Email { get; set; }
    
    [MaxLength(50)]
    public string? Phone { get; set; }
    
    public virtual ICollection<ContactFund> ContactFunds { get; set; } = new List<ContactFund>();
}
