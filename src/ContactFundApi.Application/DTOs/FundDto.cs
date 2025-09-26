namespace ContactFundApi.Application.DTOs;

public class FundDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class CreateFundDto
{
    public string Name { get; set; } = string.Empty;
}

public class UpdateFundDto
{
    public string Name { get; set; } = string.Empty;
}
