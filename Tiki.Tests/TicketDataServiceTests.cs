using Tiki.App;

namespace Tiki.Tests
{
  public class TicketDataServiceTests
  {
    private const string TestFileName = "test_tickets.json";

    // A helper method to create a test instance that uses a test file.
    private static TicketDataService CreateTestDataService()
    {
      // Optionally, modify TicketDataService to accept a file name as a parameter.
      // For now, you might temporarily change the FileName constant for testing purposes.
      return new TicketDataService();
    }

    [Fact]
    public static void SaveAndLoadTickets_WorkCorrectly()
    {
      // Arrange
      var dataService = CreateTestDataService();
      var tickets = new List<Ticket>
        {
          new(1, "Test User", "ACC001", "Test description")
        };

      // Act
      dataService.SaveTickets(tickets);
      var loadedTickets = TicketDataService.LoadTickets();

      // Assert
      Assert.NotEmpty(loadedTickets);
      Assert.Equal(1, loadedTickets[0].TicketNumber);

      // Cleanup
      if (File.Exists("tickets.json"))
      {
        File.Delete("tickets.json");
      }
    }
  }
}
