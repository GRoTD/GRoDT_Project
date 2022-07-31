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
        Sale sale = new Sale()
        {
            BoughtDateTime = DateTime.Now,
            /*Buyer = input.Buyer,*/
            IsPayed = false,
            /*PaymentDeadline = input.PaymentDeadLine,*/
            /*Tickets = input.Tickets*/
        };

        _slippDbCtx.Sales.Add(sale);
        await _slippDbCtx.SaveChangesAsync();

        return sale;
    }

    public async Task<Sale> GetSale(Guid id)
    {
        var query = _slippDbCtx.Sales
            .Include(s => s.Buyer)
            .Include(s => s.Tickets)
            .AsQueryable();

        var sale = await query.FirstOrDefaultAsync(s => s.Id == id);

        if (sale == null) return null; //Make Exception

        return sale;
    }

    public async Task<List<Sale>> GetSalesByUser(Guid userId)
    {
        throw new NotImplementedException();
    }
}