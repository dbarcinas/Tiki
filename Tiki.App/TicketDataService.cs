using System.Text.Json;

namespace Tiki.App
{
  public class TicketDataService
  {
    private const string FileName = "tickets.json";

    public void SaveTickets(IEnumerable<Ticket> tickets)
    {
      var options = new JsonSerializerOptions { WriteIndented = true };
      var json = JsonSerializer.Serialize(tickets, options);
      File.WriteAllText(FileName, json);
    }

    public static List<Ticket> LoadTickets()
    {
      if (!File.Exists(FileName))
        return [];

      var json = File.ReadAllText(FileName);
      return JsonSerializer.Deserialize<List<Ticket>>(json) ?? [];
    }
  }
}
