namespace SlippAPI.DTOs;

public class CreateTicketInput
{
    public string Title { get; set; }
    public decimal Price { get; set; }
    public DateTime StartValidTime { get; set; }
    public DateTime EndValidTime { get; set; }
    public int ClubId { get; set; }
}