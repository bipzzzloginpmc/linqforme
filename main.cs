using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Program
{
    // ── Simulated Async Data Source (mimics real DB/API calls) ─────────────────
    public static async Task<List<Customer>> GetCustomersAsync()
    {
        await Task.Delay(100); // simulate DB delay
        return new List<Customer>
        {
            new Customer
            {
                CustomerId = 1, FullName = "Bipin Thapa", Email = "bipin@email.com",
                Tickets = new List<Ticket>
                {
                    new Ticket { TicketId = 101, Title = "No Internet", Status = "Open",
                        Comments = new List<Comment>
                        {
                            new Comment { CommentId = 1, Text = "Router restarted",  CreatedAt = new DateTime(2024, 1, 5) },
                            new Comment { CommentId = 2, Text = "Still not working", CreatedAt = new DateTime(2024, 1, 6) }
                        }
                    },
                    new Ticket { TicketId = 102, Title = "Slow Speed", Status = "Closed",
                        Comments = new List<Comment>
                        {
                            new Comment { CommentId = 3, Text = "Speed test done", CreatedAt = new DateTime(2024, 2, 10) }
                        }
                    }
                }
            },
            new Customer
            {
                CustomerId = 2, FullName = "Ram Sharma", Email = "ram@email.com",
                Tickets = new List<Ticket>
                {
                    new Ticket { TicketId = 103, Title = "IPTV Issue", Status = "Open",
                        Comments = new List<Comment>
                        {
                            new Comment { CommentId = 4, Text = "Channel not loading", CreatedAt = new DateTime(2024, 3, 1) },
                            new Comment { CommentId = 5, Text = "Technician assigned", CreatedAt = new DateTime(2024, 3, 2) }
                        }
                    }
                }
            }
        };
    }

    public static async Task<List<Ticket>> GetTicketsAsync()
    {
        await Task.Delay(100); // simulate DB delay
        var customers = await GetCustomersAsync();
        return customers.SelectMany(c => c.Tickets).ToList();
    }

    // ── Simulated Async per-item Operation (mimics sending email, calling API) ──
    public static async Task<string> NotifyCustomerAsync(Customer customer)
    {
        await Task.Delay(50); // simulate API call delay
        return $"Notified: {customer.FullName} at {customer.Email}";
    }

    public static async Task<string> ResolveTicketAsync(Ticket ticket)
    {
        await Task.Delay(50); // simulate API call delay
        return $"Resolved: [{ticket.TicketId}] {ticket.Title}";
    }

    // ─────────────────────────────────────────────────────────────────────────
    public static async Task Main()
    {
    #region Pattern 1
        // ── Pattern 1 ─────────────────────────────────────────────────────────
        // Await the data source first, then run LINQ on the result
        Console.WriteLine("── Pattern 1: Await data, then LINQ ──────────────────");

        var customers = await GetCustomersAsync();

        var openTickets = from c in customers
                          from t in c.Tickets
                          where t.Status == "Open"
                          select new { c.FullName, t.TicketId, t.Title };

        foreach (var item in openTickets)
            Console.WriteLine($"  {item.FullName,-15} | [{item.TicketId}] {item.Title}");
    #endregion
    #region Pattern 2
        // ── Pattern 2 ─────────────────────────────────────────────────────────
        // Run multiple async data fetches in PARALLEL with Task.WhenAll
        Console.WriteLine("\n── Pattern 2: Parallel async fetch (Task.WhenAll) ────");

        var customerIds = new List<int> { 1, 2 };

        // Fire all customer fetch tasks at once
        var fetchTasks = (from id in customerIds
                          select GetCustomersAsync()).ToList();

        var results = await Task.WhenAll(fetchTasks);

        foreach (var result in results)
            foreach (var c in result)
                Console.WriteLine($"  Fetched: [{c.CustomerId}] {c.FullName}");
    #endregion
    #region Pattern 3
        // ── Pattern 3 ─────────────────────────────────────────────────────────
        // LINQ selects tasks → WhenAll awaits all together (fan-out pattern)
        Console.WriteLine("\n── Pattern 3: Fan-out notify all customers ────────────");

        var notifyTasks = (from c in customers
                           select NotifyCustomerAsync(c)).ToList();

        var notifications = await Task.WhenAll(notifyTasks);

        foreach (var msg in notifications)
            Console.WriteLine($"  {msg}");
    #endregion
    #region Pattern 4
        // ── Pattern 4 ─────────────────────────────────────────────────────────
        // Filter with LINQ first, then fan-out async operation on matches only
        Console.WriteLine("\n── Pattern 4: Resolve only Open tickets (fan-out) ────");

        var resolveTask = (from c in customers
                           from t in c.Tickets
                           where t.Status == "Open"
                           select ResolveTicketAsync(t)).ToList();

        var resolved = await Task.WhenAll(resolveTask);

        foreach (var msg in resolved)
            Console.WriteLine($"  {msg}");
    #endregion        
    #region Pattern 5
        // ── Pattern 5 ─────────────────────────────────────────────────────────
        // Await inside loop with sequential async processing
        Console.WriteLine("\n── Pattern 5: Sequential async per customer ───────────");

        foreach (var c in from c in customers
                          where c.Tickets.Any(t => t.Status == "Open")
                          select c)
        {
            var msg = await NotifyCustomerAsync(c);
            Console.WriteLine($"  {msg}");
        }
    #endregion
    #region Pattern 6
        // ── Pattern 6 ─────────────────────────────────────────────────────────
        // Async projection: build result list with async transformation
        Console.WriteLine("\n── Pattern 6: Async projection into summary ───────────");

        var summaryTasks = (from c in customers
                            select Task.Run(async () =>
                            {
                                await Task.Delay(30); // simulate async work
                                return new
                                {
                                    c.FullName,
                                    TotalTickets  = c.Tickets.Count,
                                    OpenTickets   = c.Tickets.Count(t => t.Status == "Open"),
                                    TotalComments = c.Tickets.Sum(t => t.Comments.Count)
                                };
                            })).ToList();

        var summaries = await Task.WhenAll(summaryTasks);

        foreach (var s in summaries)
            Console.WriteLine($"  {s.FullName,-15} | Tickets: {s.TotalTickets} | Open: {s.OpenTickets} | Comments: {s.TotalComments}");
    #endregion
    #region Pattern 7
        // ── Pattern 7 ─────────────────────────────────────────────────────────
        // Aggregate async results after WhenAll
        Console.WriteLine("\n── Pattern 7: Aggregate after async fetch ─────────────");

        var tickets = await GetTicketsAsync();

        int totalOpen   = (from t in tickets where t.Status == "Open"   select t).Count();
        int totalClosed = (from t in tickets where t.Status == "Closed" select t).Count();
        int totalComments = (from t in tickets select t.Comments.Count).Sum();

        Console.WriteLine($"  Total Open Tickets    : {totalOpen}");
        Console.WriteLine($"  Total Closed Tickets  : {totalClosed}");
        Console.WriteLine($"  Total Comments        : {totalComments}");
        #endregion
    }
}