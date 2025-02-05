using Spectre.Console;
using System;
using Tiki;

namespace Tiki
{
  class Program
  {
    static TicketRepository repository = new TicketRepository();

    static void Main(string[] args)
    {
      bool exit = false;
      while (!exit)
      {
        // Display the menu using Spectre.Console
        AnsiConsole.Clear();
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Welcome to [green]Tiki Ticket System[/] - Select an option:")
                .PageSize(10)
                .AddChoices(new[] {
                            "Create Ticket",
                            "View All Tickets",
                            "Update Ticket",
                            "Delete Ticket",
                            "Exit"
                }));

        switch (choice)
        {
          case "Create Ticket":
            CreateTicket();
            break;
          case "View All Tickets":
            ViewAllTickets();
            break;
          case "Update Ticket":
            UpdateTicket();
            break;
          case "Delete Ticket":
            DeleteTicket();
            break;
          case "Exit":
            exit = true;
            break;
        }
      }
    }

    static void CreateTicket()
    {
      // For simplicity, we ask for a ticket number (in a real app, you might auto-generate this)
      var ticketNumber = AnsiConsole.Ask<int>("Enter [blue]ticket number[/]:");
      var customerName = AnsiConsole.Ask<string>("Enter [blue]customer name[/]:");
      var accountNumber = AnsiConsole.Ask<string>("Enter [blue]account number[/]:");
      var description = AnsiConsole.Ask<string>("Enter [blue]description[/]:");

      var ticket = new Ticket(ticketNumber, customerName, accountNumber, description);
      repository.AddTicket(ticket);

      AnsiConsole.MarkupLine("[green]Ticket created successfully![/]");
      AnsiConsole.WriteLine("Press any key to return to the menu...");
      Console.ReadKey();
    }

    static void ViewAllTickets()
    {
      var tickets = repository.GetAllTickets();
      var table = new Table();
      table.AddColumn("Ticket Number");
      table.AddColumn("Customer Name");
      table.AddColumn("Account Number");
      table.AddColumn("Description");
      table.AddColumn("Status");

      foreach (var ticket in tickets)
      {
        table.AddRow(
          ticket.TicketNumber.ToString(),
          ticket.CustomerName,
          ticket.AccountNumber,
          ticket.Description,
          ticket.Status.ToString()
        );
      }

      AnsiConsole.Write(table);
      AnsiConsole.WriteLine("Press any key to return to the menu...");
      Console.ReadKey();
    }

    static void UpdateTicket()
    {
      var ticketNumber = AnsiConsole.Ask<int>("Enter the [blue]ticket number[/] to update:");
      var ticket = repository.GetTicket(ticketNumber);
      if (ticket == null)
      {
        AnsiConsole.MarkupLine("[red]Ticket not found![/]");
      }
      else
      {
        ticket.CustomerName = AnsiConsole.Ask<string>($"Enter new [blue]customer name[/] (current: {ticket.CustomerName}):", ticket.CustomerName);
        ticket.AccountNumber = AnsiConsole.Ask<string>($"Enter new [blue]account number[/] (current: {ticket.AccountNumber}):", ticket.AccountNumber);
        ticket.Description = AnsiConsole.Ask<string>($"Enter new [blue]description[/] (current: {ticket.Description}):", ticket.Description);
        // Optionally update status:
        var status = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select new status:")
                .AddChoices(Enum.GetNames(typeof(TicketStatus))));
        ticket.Status = Enum.TryParse<TicketStatus>(status, out var newStatus) ? newStatus : ticket.Status;

        repository.UpdateTicket(ticket);
        AnsiConsole.MarkupLine("[green]Ticket updated successfully![/]");
      }
      AnsiConsole.WriteLine("Press any key to return to the menu...");
      Console.ReadKey();
    }

    static void DeleteTicket()
    {
      var ticketNumber = AnsiConsole.Ask<int>("Enter the [blue]ticket number[/] to delete:");
      if (repository.DeleteTicket(ticketNumber))
      {
        AnsiConsole.MarkupLine("[green]Ticket deleted successfully![/]");
      }
      else
      {
        AnsiConsole.MarkupLine("[red]Ticket not found![/]");
      }
      AnsiConsole.WriteLine("Press any key to return to the menu...");
      Console.ReadKey();
    }
  }
}

