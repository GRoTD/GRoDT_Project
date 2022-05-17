using Microsoft.EntityFrameworkCore;

namespace SlippAPI.Services;

public class AuctionService
{
    private readonly SlippDbCtx _slippDbCtx;

    public AuctionService(SlippDbCtx slippDbCtx)
    {
        _slippDbCtx = slippDbCtx;
    }

    public async Task<Auction> CreateAuction(Guid clubId, AuctionInputDTO auctionInput)
    {
        var club = await _slippDbCtx.Clubs.FindAsync(clubId);

        if (club == null) return null; //TODO: Throw exception.

        Auction auction = auctionInput.CreateAuction(club);

        _slippDbCtx.Add(auction);
        var result = await _slippDbCtx.SaveChangesAsync();

        return auction;
    }


    public async Task<List<Auction>> GetAuctions(Guid? clubId) //Add lon, lat, radius
    {
        var query = _slippDbCtx.Auctions
            .Include(a => a.Club)
            .Include(a => a.Tickets).ThenInclude(t => t.Sale)
            .Include(a => a.Bids).ThenInclude(b => b.AppUser)
            .AsQueryable();

        //TODO: Can add filtering here. Ex clubId below. 
        if (clubId != null) query = query.Where(a => a.Club.Id == clubId);

        var auctions = await query.ToListAsync();

        return auctions;
    }
}