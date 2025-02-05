using System.Collections.Generic;
using System.Linq;

namespace Tiki
{
  public class TicketRepository
  {
    private readonly List<Ticket> _tickets = [];

    public void AddTicket(Ticket ticket)
    {
      _tickets.Add(ticket);
    }

    public Ticket GetTicket(int ticketNumber)
    {
      return _tickets.FirstOrDefault(t => t.TicketNumber == ticketNumber);
    }

    public List<Ticket> GetAllTickets()
    {
      return _tickets;
    }

    public bool UpdateTicket(Ticket updatedTicket)
    {
      var existing = GetTicket(updatedTicket.TicketNumber);
      if (existing == null)
        return false;

      existing.CustomerName = updatedTicket.CustomerName;
      existing.AccountNumber = updatedTicket.AccountNumber;
      existing.Description = updatedTicket.Description;
      existing.Status = updatedTicket.Status;
      return true;
    }

    public bool DeleteTicket(int ticketNumber)
    {
      var ticket = GetTicket(ticketNumber);
      if (ticket == null)
        return false;

      _tickets.Remove(ticket);
      return true;
    }
  }
}
