using Slipp.Services.DTO;

namespace SlippWeb.Client.BlazorServices.State;

public class ShoppingCartStateContainer
{
    private List<TicketOutput> _currentTickets;

    public List<TicketOutput> CurrentTickets
    {
        get => _currentTickets;
        set
        {
            _currentTickets = value;
            NotifyStateChanged();
        }
    }


    public event Action? OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();
}