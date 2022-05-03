namespace SlippAPI.DTOs;

public class CreateTicketOutput
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public DateTime StartValidTime { get; set; }
    public DateTime EndValidTime { get; set; }
}