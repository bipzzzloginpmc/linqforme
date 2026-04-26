// Customer has many Tickets
// Ticket has many Comments

public class Customer
{
    public int CustomerId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public List<Ticket> Tickets { get; set; }
}

public class Ticket
{
    public int TicketId { get; set; }
    public string Title { get; set; }
    public string Status { get; set; }
    public List<Comment> Comments { get; set; }
}

public class Comment
{
    public int CommentId { get; set; }
    public string Text { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class Program
{
    public static void Main()
    {
        //DataSet

        var customers = new List<Customer>
                    {
                        new Customer
                        {
                            CustomerId = 1, FullName = "Bipin Thapa", Email = "bipin@email.com",
                            Tickets = new List<Ticket>
                            {
                                new Ticket { TicketId = 101, Title = "No Internet", Status = "Open",
                                    Comments = new List<Comment>
                                    {
                                        new Comment { CommentId = 1, Text = "Router restarted" },
                                        new Comment { CommentId = 2, Text = "Still not working" }
                                    }
                                },
                                new Ticket { TicketId = 102, Title = "Slow Speed", Status = "Closed",
                                    Comments = new List<Comment>
                                    {
                                        new Comment { CommentId = 3, Text = "Speed test done" }
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
                                        new Comment { CommentId = 4, Text = "Channel not loading" },
                                        new Comment { CommentId = 5, Text = "Technician assigned" }
                                    }
                                }
                            }
                        }
                    };
    
     // ── ElementAt ──────────────────────────────────────────────────────────────
    // Get customer at index 1 (Ram Sharma)
    Customer elementAt = (from c in customers select c)
                            .ElementAt(1);
    Console.WriteLine($"ElementAt(1)                    : {elementAt.FullName}");

    // Get second ticket across all tickets (index 1)
    Ticket ticketAt = (from c in customers
                       from t in c.Tickets
                       select t).ElementAt(1);
    Console.WriteLine($"ElementAt(1) ticket             : {ticketAt.Title}");

    // ── ElementAtOrDefault ─────────────────────────────────────────────────────
    // Index exists → returns customer
    Customer elementAtOrDefault = (from c in customers select c)
                                    .ElementAtOrDefault(0);
    Console.WriteLine($"ElementAtOrDefault(0)           : {elementAtOrDefault?.FullName}");

    // Index out of range → returns null (default for reference type)
    Customer outOfRange = (from c in customers select c)
                            .ElementAtOrDefault(99);
    Console.WriteLine($"ElementAtOrDefault(99)          : {outOfRange?.FullName ?? "null (out of range)"}");

    // ── First ──────────────────────────────────────────────────────────────────
    // First customer in the list
    Customer firstCustomer = (from c in customers select c)
                                .First();
    Console.WriteLine($"First      (customer)           : {firstCustomer.FullName}");

    // First ticket with Status = "Closed"
    Ticket firstClosed = (from c in customers
                          from t in c.Tickets
                          where t.Status == "Closed"
                          select t).First();
    Console.WriteLine($"First      (closed ticket)      : {firstClosed.Title}");

    // ── FirstOrDefault ─────────────────────────────────────────────────────────
    // First customer whose email starts with "bipin"
    Customer firstOrDefault = (from c in customers
                                where c.Email.StartsWith("bipin")
                                select c).FirstOrDefault();
    Console.WriteLine($"FirstOrDefault (bipin@...)      : {firstOrDefault?.FullName}");

    // No match → returns null
    Customer noMatch = (from c in customers
                        where c.Email.StartsWith("xyz")
                        select c).FirstOrDefault();
    Console.WriteLine($"FirstOrDefault (no match)       : {noMatch?.FullName ?? "null (no match)"}");

    // ── Last ───────────────────────────────────────────────────────────────────
    // Last customer in the list
    Customer lastCustomer = (from c in customers select c)
                                .Last();
    Console.WriteLine($"Last       (customer)           : {lastCustomer.FullName}");

    // Last Open ticket across all tickets
    Ticket lastOpen = (from c in customers
                       from t in c.Tickets
                       where t.Status == "Open"
                       select t).Last();
    Console.WriteLine($"Last       (open ticket)        : {lastOpen.Title}");

    // ── LastOrDefault ──────────────────────────────────────────────────────────
    // Last comment across all tickets
    Comment lastComment = (from c in customers
                           from t in c.Tickets
                           from cm in t.Comments
                           select cm).LastOrDefault();
    Console.WriteLine($"LastOrDefault (last comment)    : {lastComment?.Text}");

    // No match → returns null
    Ticket noTicket = (from c in customers
                       from t in c.Tickets
                       where t.Status == "Pending"
                       select t).LastOrDefault();
    Console.WriteLine($"LastOrDefault (no match)        : {noTicket?.Title ?? "null (no match)"}");

    // ── Single ─────────────────────────────────────────────────────────────────
    // Exactly one customer with CustomerId = 1
    Customer singleCustomer = (from c in customers
                                where c.CustomerId == 1
                                select c).Single();
    Console.WriteLine($"Single     (CustomerId = 1)     : {singleCustomer.FullName}");

    // Exactly one closed ticket in the whole list
    Ticket singleClosed = (from c in customers
                           from t in c.Tickets
                           where t.Status == "Closed"
                           select t).Single();
    Console.WriteLine($"Single     (closed ticket)      : {singleClosed.Title}");

    // ── SingleOrDefault ────────────────────────────────────────────────────────
    // One match → returns it
    Customer singleOrDefault = (from c in customers
                                 where c.CustomerId == 2
                                 select c).SingleOrDefault();
    Console.WriteLine($"SingleOrDefault (CustomerId=2)  : {singleOrDefault?.FullName}");

    // No match → returns null (does NOT throw)
    Customer singleNoMatch = (from c in customers
                               where c.CustomerId == 99
                               select c).SingleOrDefault();
    Console.WriteLine($"SingleOrDefault (no match)      : {singleNoMatch?.FullName ?? "null (no match)"}");
    
    }
}