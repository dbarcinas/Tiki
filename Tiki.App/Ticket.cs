namespace Tiki.App
{
  public enum TicketStatus
  {
    Open,
    InProgress,
    Closed
  }

  public class Ticket(int ticketNumber, string customerName, string accountNumber, string description)
  {
    public int TicketNumber { get; set; } = ticketNumber;
    public string CustomerName { get; set; } = customerName;
    public string AccountNumber { get; set; } = accountNumber;
    public string Description { get; set; } = description;
    public TicketStatus Status { get; set; } = TicketStatus.Open;
  }
}
