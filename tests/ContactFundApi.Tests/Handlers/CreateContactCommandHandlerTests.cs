using Xunit;
using Moq;
using ContactFundApi.Application.Commands;
using ContactFundApi.Application.Handlers;
using ContactFundApi.Domain.Entities;
using ContactFundApi.Domain.Interfaces;
using ContactFundApi.Domain.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using ContactFundApi.Application.DTOs;

namespace ContactFundApi.Tests.Handlers;

public class CreateContactCommandHandlerTests
{
    [Fact]
    public async Task Handle_WhenContactDataIsValid_ShouldCreateContact()
    {
        // Arrange
        var mockRepository = new Mock<IContactRepository>();
        var mockValidator = new Mock<IValidator<CreateContactDto>>();
        
        var command = new CreateContactCommand(new CreateContactDto 
        { 
            Name = "John Doe", 
            Email = "john@example.com", 
            Phone = "123-456-7890" 
        });

        var createdContact = new Contact
        {
            Id = 1,
            Name = command.ContactDto.Name,
            Email = command.ContactDto.Email,
            Phone = command.ContactDto.Phone
        };

        mockValidator.Setup(v => v.ValidateAsync(It.IsAny<CreateContactDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());
        
        mockRepository.Setup(r => r.AddAsync(It.IsAny<Contact>()))
            .ReturnsAsync(createdContact);

        var handler = new CreateContactCommandHandler(mockRepository.Object, mockValidator.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(createdContact.Id, result.Id);
        Assert.Equal(createdContact.Name, result.Name);
        Assert.Equal(createdContact.Email, result.Email);
        Assert.Equal(createdContact.Phone, result.Phone);
        
        mockRepository.Verify(r => r.AddAsync(It.IsAny<Contact>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenValidationFails_ShouldThrowValidationException()
    {
        // Arrange
        var mockRepository = new Mock<IContactRepository>();
        var mockValidator = new Mock<IValidator<CreateContactDto>>();
        
        var command = new CreateContactCommand(new CreateContactDto { Name = "" });
        
        var validationErrors = new List<ValidationFailure>
        {
            new ValidationFailure("Name", "Name is required")
        };
        
        mockValidator.Setup(v => v.ValidateAsync(It.IsAny<CreateContactDto>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(validationErrors));

        var handler = new CreateContactCommandHandler(mockRepository.Object, mockValidator.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
        
        mockRepository.Verify(r => r.AddAsync(It.IsAny<Contact>()), Times.Never);
    }
}
