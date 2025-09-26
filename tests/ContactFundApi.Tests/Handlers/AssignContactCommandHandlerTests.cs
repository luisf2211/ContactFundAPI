using Xunit;
using Moq;
using ContactFundApi.Application.Commands;
using ContactFundApi.Application.Handlers;
using ContactFundApi.Domain.Entities;
using ContactFundApi.Domain.Interfaces;
using ContactFundApi.Domain.Exceptions;

namespace ContactFundApi.Tests.Handlers;

public class AssignContactCommandHandlerTests
{
    [Fact]
    public async Task Handle_WhenContactAndFundExist_ShouldAssignContactToFund()
    {
        // Arrange
        var mockContactRepository = new Mock<IContactRepository>();
        var mockFundRepository = new Mock<IFundRepository>();
        var mockContactFundRepository = new Mock<IContactFundRepository>();
        
        var command = new AssignContactCommand(1, 1);
        
        var contact = new Contact { Id = 1, Name = "John Doe" };
        var fund = new Fund { Id = 1, Name = "Test Fund" };

        mockContactRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(contact);
        
        mockFundRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(fund);
        
        mockContactFundRepository.Setup(r => r.IsContactAssignedToFundAsync(1, 1))
            .ReturnsAsync(false);

        var handler = new AssignContactCommandHandler(
            mockContactRepository.Object,
            mockFundRepository.Object,
            mockContactFundRepository.Object);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        mockContactFundRepository.Verify(r => r.AssignContactToFundAsync(1, 1), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenContactDoesNotExist_ShouldThrowContactNotFoundException()
    {
        // Arrange
        var mockContactRepository = new Mock<IContactRepository>();
        var mockFundRepository = new Mock<IFundRepository>();
        var mockContactFundRepository = new Mock<IContactFundRepository>();
        
        var command = new AssignContactCommand(999, 1);

        mockContactRepository.Setup(r => r.GetByIdAsync(999))
            .ReturnsAsync((Contact?)null);

        var handler = new AssignContactCommandHandler(
            mockContactRepository.Object,
            mockFundRepository.Object,
            mockContactFundRepository.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ContactNotFoundException>(() => handler.Handle(command, CancellationToken.None));
        
        mockContactFundRepository.Verify(r => r.AssignContactToFundAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WhenContactAlreadyAssigned_ShouldThrowContactAlreadyAssignedException()
    {
        // Arrange
        var mockContactRepository = new Mock<IContactRepository>();
        var mockFundRepository = new Mock<IFundRepository>();
        var mockContactFundRepository = new Mock<IContactFundRepository>();
        
        var command = new AssignContactCommand(1, 1);
        
        var contact = new Contact { Id = 1, Name = "John Doe" };
        var fund = new Fund { Id = 1, Name = "Test Fund" };

        mockContactRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(contact);
        
        mockFundRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(fund);
        
        mockContactFundRepository.Setup(r => r.IsContactAssignedToFundAsync(1, 1))
            .ReturnsAsync(true);

        var handler = new AssignContactCommandHandler(
            mockContactRepository.Object,
            mockFundRepository.Object,
            mockContactFundRepository.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ContactAlreadyAssignedException>(() => handler.Handle(command, CancellationToken.None));
        
        mockContactFundRepository.Verify(r => r.AssignContactToFundAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
    }
}
