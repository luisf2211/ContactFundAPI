using Xunit;
using Moq;
using ContactFundApi.Application.Commands;
using ContactFundApi.Application.Handlers;
using ContactFundApi.Domain.Entities;
using ContactFundApi.Domain.Interfaces;
using ContactFundApi.Domain.Exceptions;

namespace ContactFundApi.Tests.Handlers;

public class DeleteContactCommandHandlerTests
{
    [Fact]
    public async Task Handle_WhenContactExistsAndNotAssignedToFunds_ShouldDeleteContact()
    {
        // Arrange
        var mockRepository = new Mock<IContactRepository>();
        
        var command = new DeleteContactCommand(1);
        var contact = new Contact { Id = 1, Name = "John Doe" };

        mockRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(contact);
        
        mockRepository.Setup(r => r.IsAssignedToAnyFundAsync(1))
            .ReturnsAsync(false);

        var handler = new DeleteContactCommandHandler(mockRepository.Object);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        mockRepository.Verify(r => r.DeleteAsync(1), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenContactDoesNotExist_ShouldThrowContactNotFoundException()
    {
        // Arrange
        var mockRepository = new Mock<IContactRepository>();
        
        var command = new DeleteContactCommand(999);

        mockRepository.Setup(r => r.GetByIdAsync(999))
            .ReturnsAsync((Contact?)null);

        var handler = new DeleteContactCommandHandler(mockRepository.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ContactNotFoundException>(() => handler.Handle(command, CancellationToken.None));
        
        mockRepository.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WhenContactIsAssignedToFunds_ShouldThrowContactCannotBeDeletedException()
    {
        // Arrange
        var mockRepository = new Mock<IContactRepository>();
        
        var command = new DeleteContactCommand(1);
        var contact = new Contact { Id = 1, Name = "John Doe" };

        mockRepository.Setup(r => r.GetByIdAsync(1))
            .ReturnsAsync(contact);
        
        mockRepository.Setup(r => r.IsAssignedToAnyFundAsync(1))
            .ReturnsAsync(true);

        var handler = new DeleteContactCommandHandler(mockRepository.Object);

        // Act & Assert
        await Assert.ThrowsAsync<ContactCannotBeDeletedException>(() => handler.Handle(command, CancellationToken.None));
        
        mockRepository.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never);
    }
}
