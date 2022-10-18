using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slipp.Services.Constants;
using SlippAPI.Services;
using System.Data;
using System;

namespace SlippAPI.Controllers
{
    [Route(ApiPaths.ORDERCONTROLLER)]
    [ApiController]
    public class OrderController : ControllerBase
    {
        //Skapa en metod som heter CreateOrder som tar in orderInput och
        //skapar order till databasen och skickar tillbaka on orderOutput

        private readonly OrderService _orderService;


        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Authorize(Roles = $"{StaticConfig.AppUserRole}")] //OK? 
        public async Task<ActionResult<OrderOutput>> CreateOrder(Guid AppUserId, OrderInput orderInput)
        {
            //är det här rätt? 
            var checkUser = User.Claims
                .FirstOrDefault(c => c.Type == SlippClaimTypes.APPUSERID
                                     && c.Value == AppUserId.ToString());
            if (checkUser == null) return Unauthorized();

            var newOrder = await _orderService.CreateOrder(AppUserId, orderInput);

            var createdOrder = OrderOutput.CreateOrderOutput(newOrder);

            return Ok(createdOrder);
        }
    }
}