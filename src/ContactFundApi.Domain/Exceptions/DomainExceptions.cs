using ContactFundApi.Domain.Entities;

namespace ContactFundApi.Domain.Exceptions;

public class ContactValidationException : Exception
{
    public ContactValidationException(string message) : base(message) { }
}

public class ContactNotFoundException : Exception
{
    public ContactNotFoundException(int contactId) : base($"Contact with ID {contactId} was not found.") { }
}

public class FundNotFoundException : Exception
{
    public FundNotFoundException(int fundId) : base($"Fund with ID {fundId} was not found.") { }
}

public class ContactAlreadyAssignedException : Exception
{
    public ContactAlreadyAssignedException(int contactId, int fundId) 
        : base($"Contact {contactId} is already assigned to Fund {fundId}.") { }
}

public class ContactCannotBeDeletedException : Exception
{
    public ContactCannotBeDeletedException(int contactId) 
        : base($"Contact {contactId} cannot be deleted because it is assigned to one or more funds.") { }
}
