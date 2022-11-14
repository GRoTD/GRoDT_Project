using Slipp.Services.Constants;
using Slipp.Services.DTO;

namespace SlippWeb.Client.BlazorServices;

public class OrderAPIService
{
    private readonly IApiService _apiService;
    private readonly IAuthenticationService _authentication;

    public OrderAPIService(IApiService apiService, IAuthenticationService authentication)
    {
        _apiService = apiService;
        _authentication = authentication;
    }


    public async Task<OrderOutput> CreateOrder(OrderInput orderInput)
    {
        var path = ApiPaths.ORDERCONTROLLER;
        return await _apiService.Post<OrderOutput>(path, orderInput);
    }
}