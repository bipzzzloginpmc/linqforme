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
    
     var result = from c in customers select c.FullName.Reverse().ToArray();
        foreach (var item in result)
        {
            Console.WriteLine($"{String.Concat(item)}");
        }
    
    }
}