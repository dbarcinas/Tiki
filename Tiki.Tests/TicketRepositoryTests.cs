using System.Linq;
using Tiki;
using Xunit;

namespace Tiki.Tests
{
  public class TicketRepositoryTests
  {
    [Fact]
    public void AddTicket_ShouldStoreTicket()
    {
      // Arrange
      var repo = new TicketRepository();
      var ticket = new Ticket(1, "John Doe", "ACC123", "Issue with login");

      // Act
      repo.AddTicket(ticket);
      var retrieved = repo.GetTicket(1);

      // Assert
      Assert.NotNull(retrieved);
      Assert.Equal("John Doe", retrieved.CustomerName);
    }

    [Fact]
    public void UpdateTicket_ShouldModifyExistingTicket()
    {
      // Arrange
      var repo = new TicketRepository();
      var ticket = new Ticket(2, "Jane Smith", "ACC456", "Password reset");
      repo.AddTicket(ticket);

      // Act
      ticket.Description = "Updated description";
      var updated = repo.UpdateTicket(ticket);

      // Assert
      Assert.True(updated);
      var retrieved = repo.GetTicket(2);
      Assert.Equal("Updated description", retrieved.Description);
    }

    [Fact]
    public void DeleteTicket_ShouldRemoveTicket()
    {
      // Arrange
      var repo = new TicketRepository();
      var ticket = new Ticket(3, "Alice", "ACC789", "Unable to login");
      repo.AddTicket(ticket);

      // Act
      var deleted = repo.DeleteTicket(3);

      // Assert
      Assert.True(deleted);
      var retrieved = repo.GetTicket(3);
      Assert.Null(retrieved);
    }
  }
}
