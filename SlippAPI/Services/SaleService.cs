using Microsoft.EntityFrameworkCore;

namespace SlippAPI.Services;

public class SaleService
{
    private readonly SlippDbCtx _slippDbCtx;

    public SaleService(SlippDbCtx slippDbCtx)
    {
        _slippDbCtx = slippDbCtx;
    }

    public async Task<Sale> CreateSale(SaleInput input)
    {
        var order = _slippDbCtx.Orders.FirstOrDefault(o => o.Id == input.OrderId);

        if (order == null) throw new Exception("Order must exist");

        Sale sale = new Sale()
        {
            BoughtDateTime = DateTime.Now,
            Order = order
        };

        _slippDbCtx.Sales.Add(sale);
        await _slippDbCtx.SaveChangesAsync();

        return sale;
    }

    public async Task<Sale> GetSale(Guid id)
    {
        var query = _slippDbCtx.Sales
            .Include(s => s.Order)
            .AsQueryable();

        var sale = await query.FirstOrDefaultAsync(s => s.Id == id);

        if (sale == null) throw new Exception("Sale does not exist"); //Make Exception

        return sale;
    }

    public async Task<List<Sale>> GetSalesByUser(Guid userId)
    {
        throw new NotImplementedException();
    }
}