using Firebase.Database;
using Firebase.Database.Query;
using SlippAPI.Options;

namespace Slipp.Services;

public class UserService
{
    private readonly FirebaseClient _firebaseClient;
    private readonly RealtimeDbOptions _realtimeDbOptions;

    public UserService(RealtimeDbOptions realtimeDbOptions)
    {
        _realtimeDbOptions = realtimeDbOptions;
        _firebaseClient = new FirebaseClient(_realtimeDbOptions.DatabaseURL);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<DatabaseUser> CreateAppUser(CreateAppUserInput input, string? userid)
    {
        AppUser appUser = new AppUser
        {
            FirstName = input.FirstName,
            LastName = input.LastName
        };
        DatabaseUser dbUser = new DatabaseUser
        {
            UserName = input.Email,
            AppUser = appUser
        };

       try
        {
            await _firebaseClient.Child("users").Child(userid).PutAsync(dbUser);
        }
        catch(Exception ex)
        {
            throw new Exception("No claim id was found for user.", ex);
        }

        return dbUser;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<DatabaseUser> GetAppUser(string? userid)
    {

        var fireBaseUser = await _firebaseClient.Child("users").Child(userid).OnceAsync<DatabaseUser>();

        var user = fireBaseUser?.FirstOrDefault()?.Object;

        return user ?? throw new Exception();
    }

    public async Task ToggleFavouriteTicket(Guid ticketId, string? userid)
    {
        var user = await GetAppUser(userid);
        var favouriteTickets = user.AppUser?.FavouriteTicketIds;

        if (favouriteTickets == null && user.AppUser != null)
        {
            user.AppUser.FavouriteTicketIds = new List<Guid>
            {
                ticketId
            };

            await _firebaseClient.Child("users").Child(userid).PutAsync(user);
            return;
        }

        var ticketIsFavourite = favouriteTickets.Contains(ticketId);

        if (ticketIsFavourite)
        {
            favouriteTickets.Remove(ticketId);
        }
        else
        {
            favouriteTickets.Add(ticketId);
        }

        user.AppUser.FavouriteTicketIds = favouriteTickets;
        await _firebaseClient.Child("users").Child(userid).PutAsync(user);
        return;
    }

    public async Task ToggleFavouriteTicket1(Guid ticketId, string userId)
    {
        //var user = await _ctx.AppUsers
        //    .Include(u => u.FavouriteTickets)
        //    .FirstOrDefaultAsync(u => u.Id == userId);

        //var ticket = await _ctx.Tickets
        //    .FirstOrDefaultAsync(t => t.Id == ticketId);

        //if (user.FavouriteTickets.Contains(ticket))
        //{
        //    user.FavouriteTickets.Remove(ticket);
        //}
        //else
        //{
        //    user.FavouriteTickets.Add(ticket);
        //}

        //var result = await _ctx.SaveChangesAsync();
    }
}