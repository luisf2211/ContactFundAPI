using Xunit;
using FluentValidation;
using ContactFundApi.Application.DTOs;
using ContactFundApi.Application.Validators;

namespace ContactFundApi.Tests.Validators;

public class CreateContactDtoValidatorTests
{
    private readonly CreateContactDtoValidator _validator;

    public CreateContactDtoValidatorTests()
    {
        _validator = new CreateContactDtoValidator();
    }

    [Fact]
    public void Validate_WhenNameIsEmpty_ShouldReturnValidationError()
    {
        // Arrange
        var dto = new CreateContactDto { Name = "" };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Name" && e.ErrorMessage == "Name is required");
    }

    [Fact]
    public void Validate_WhenNameIsNull_ShouldReturnValidationError()
    {
        // Arrange
        var dto = new CreateContactDto { Name = null! };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Name" && e.ErrorMessage == "Name is required");
    }

    [Fact]
    public void Validate_WhenNameExceedsMaxLength_ShouldReturnValidationError()
    {
        // Arrange
        var dto = new CreateContactDto { Name = new string('A', 151) };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Name" && e.ErrorMessage == "Name cannot exceed 150 characters");
    }

    [Fact]
    public void Validate_WhenEmailIsInvalid_ShouldReturnValidationError()
    {
        // Arrange
        var dto = new CreateContactDto { Name = "Test", Email = "invalid-email" };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Email" && e.ErrorMessage == "Invalid email format");
    }

    [Fact]
    public void Validate_WhenAllFieldsAreValid_ShouldReturnSuccess()
    {
        // Arrange
        var dto = new CreateContactDto 
        { 
            Name = "John Doe", 
            Email = "john@example.com", 
            Phone = "123-456-7890" 
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        Assert.True(result.IsValid);
    }
}
