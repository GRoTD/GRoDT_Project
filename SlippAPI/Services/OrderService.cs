using Slipp.Services.Models;

namespace SlippAPI.Services
{
    public class OrderService
    {
        private readonly SlippDbCtx _slippDbCtx;

        public OrderService(SlippDbCtx slippDbCtx)
        {
            _slippDbCtx = slippDbCtx;
        }

        public async Task<Order> CreateOrder(Guid appUserId, OrderInput orderInput)
        {
            var appUser = await _slippDbCtx.AppUsers.FindAsync(appUserId);

            //TODO: Instead Throw exception to show what actually went wrong.
            if (appUser is null) return null;

            var newOrder = orderInput.CreateOrder(appUser);

            await _slippDbCtx.AddAsync(newOrder);
            _slippDbCtx.SaveChanges(); //behövs denna?

            return newOrder;

        }


    }
}
