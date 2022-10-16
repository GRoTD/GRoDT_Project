using Slipp.Services.Constants;
using Slipp.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Slipp.Services.BlazorServices
{
    public class OrderAPIService
    {

        private readonly ApiService _apiService;
        private readonly LocalStorageService _localStorageService;
        public LoggedInUser User { get; private set; }

        public OrderAPIService(ApiService apiService, LocalStorageService localStorageService)
        {
            _apiService = apiService;
            _localStorageService = localStorageService;
        }

        public async Task Initialize()
        {
            User = await _localStorageService.GetItem<LoggedInUser>("user");
        }

        public async Task<OrderOutput> CreateOrder (List<TicketOutput> chosenTickets)
        {
            var path = ApiPaths.ORDERCONTROLLER;
            return await _apiService.Post<OrderOutput>(path, chosenTickets);
        }
    }
}
