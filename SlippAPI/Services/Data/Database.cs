using Microsoft.AspNetCore.Identity;
using Slipp.Services.Models;

namespace Slipp.Services;

public class Database
{
    private readonly SlippDbCtx _slippDbCtx;
    private readonly UserManager<DatabaseUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public Database(SlippDbCtx slippDbCtx, UserManager<DatabaseUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _slippDbCtx = slippDbCtx;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedIfPrductionAndDbIsNotCreated()
    {
        var created = await _slippDbCtx.Database.EnsureCreatedAsync();

        if (created) await Seed();
    }

    public async Task RecreateAndSeed()
    {
        await _slippDbCtx.Database.EnsureDeletedAsync();
        await _slippDbCtx.Database.EnsureCreatedAsync();
        await Seed();
    }

    public async Task Seed()
    {
        foreach (var role in StaticConfig.Roles)
        {
            var r = new IdentityRole(role);
            await _roleManager.CreateAsync(r);
        }

        var company = new Company
        {
            CompanyName = "Första Test Företaget"
        };

        var images = new List<Image>()
        {
            new() {Url = "images/locatelli.jpg"},
            new() {Url = "images/push.jpg"},
            new() {Url = "images/lounge.jpg"},
            new() {Url = "images/portdusoleil.jpg"},
            new() {Url = "images/excet.jpg"},
            new() {Url = "images/putamadre.jpg"},
            new() {Url = "images/stranger.jpg"},
        };
        _slippDbCtx.AddRange(images);
        await _slippDbCtx.SaveChangesAsync();

        #region clubs
        var club = new Club
        {
            Id = new Guid("462a6830-9471-42fb-b0c3-a5dc6a2d8e50"),
            Adress = "KUNGSPORTSAVENYN 36-38, GÖTEBORG",
            City = "Göteborg",
            Company = company,
            Description =
                "Välkomna in till Locatelli! En social, atmosfärisk cocktailbar med livlig musik, frestande meny och ett spetsat fokus på stämning.",
            Name = "Locatelli",
            Website = "www.elite.se",
            Images = new List<Image>() {images[0]}
        };

        var club2 = new Club
        {
            Id = new Guid("9787b1b9-69f9-4f3f-ad17-fedb0dbbc337"),
            Adress = "KUNGSPORTSAVENYN 11, GÖTEBORG",
            City = "Göteborg",
            Company = company,
            Description =
                "Välkommen till den djärva storsatsningen som förändrat Göteborgs nattliv i grunden och äntligen tillfört staden en högklassig nattklubb av internationellt snitt. Push erbjuder en intim och diskret festupplevelse med högsta standard och personlig service utan motstycke.",
            Name = "Push",
            Website = "www.push.se",
            Images = new List<Image>() {images[1]}
        };

        var club3 = new Club
        {
            Id = new Guid("ff759a31-23e7-4640-888d-716a2ea48482"),
            Adress = "KUNGSPORTSAVENYN 5, GÖTEBORG",
            City = "Göteborg",
            Company = company,
            Description =
                "Nattklubb i flera våningar med populär takterrass. En av Avenyns mest etablerade nattklubbar. På flera våningar finns allt som behövs för en lyckad utekväll som barer, dansgolv, roulette och en populär takterass.",
            Name = "Lounge",
            Website = "www.lounge.se",
            Images = new List<Image>() {images[2]}
        };

        var club4 = new Club
        {
            Id = new Guid("1d4f52b4-96d4-456c-901b-f893e42d84b3"),
            Adress = "NYA ALLÉN 11, GÖTEBORG",
            City = "Göteborg",
            Company = company,
            Description =
                "Varje sommar öppnar Port Du Soleil upp en sommarlounge och internationell nattklubb. På den stora solterassen kan du uppleva en känsla av Rivieran mitt i Göteborg med dj:s, spännande mat och ändlösa sommarnätter.",
            Name = "Port du Soleil",
            Website = "www.portdusoleil.se",
            Images = new List<Image>() {images[3]}
        };

        var club5 = new Club
        {
            Id = new Guid("4f58f3c5-a641-4dee-a405-4ea32baf0264"),
            Adress = "VASAGATAN 52, GÖTEBORG",
            City = "Göteborg",
            Company = company,
            Description =
                "Excet är en av Göteborgs största och populäraste nattklubbar, mitt på Avenyn. Med fyra dansgolv, nio barer och en stor takterrass får du massa olika upplevelser under samma tak. Go party!",
            Name = "Excet",
            Website = "www.facebook.com/excetgbg",
            Images = new List<Image>() {images[4]}
        };

        var club6 = new Club
        {
            Id = new Guid("1e59fd90-b60e-4cd5-92ac-eebc07bfa5f5"),
            Adress = "MAGASINSGATAN 3, GÖTEBORG",
            City = "Göteborg",
            Company = company,
            Description =
                "Puta Madre är en spansk svordom som ungefär betyder *”€%!!+##*§§. Förutom detta kan det även betyda att något är jävligt bra. I Göteborg står Puta Madre för jävligt bra mexikansk mat i en härlig miljö. Och om du, likt många andra, förknippar tequila med äckliga shotrace och citronslabb utmanar vi dig att göra ett nytt försök hos oss. Du kommer inte att bli besviken.",
            Name = "Puta Madre",
            Website = "https://www.putamadre.se/",
            Images = new List<Image>() {images[5]}
        };

        var club7 = new Club
        {
            Id = new Guid("6a5f60e8-84d6-43ad-bdea-0a93be9191d2"),
            Adress = "KUNGSTORGET 14, GÖTEBORG",
            City = "Göteborg",
            Company = company,
            Description =
                "Via en hemlig ingång till en källare under Tranquilo ligger cocktailbaren Stranger. Här kan du beställa några av Göteborgs bästa drinkar i en stämningsfull miljö. Med musik och ljussättning skapas känslan av att Stranger är en hemlig bar under den amerikanska förbudstiden.",
            Name = "Stranger",
            Website = "https://www.strangergbg.com/",
            Images = new List<Image>() {images[6]}
        };
        #endregion
        _slippDbCtx.AddRange(new Club[] {club, club2, club3, club4, club5, club6, club7});
        await _slippDbCtx.SaveChangesAsync();

        #region users
        var clubUser = new DatabaseUser
        {
            Id = "ace63942-b146-4843-bfac-b9ee0acda417",
            Club = club,
            UserName = "clubUser@club.se",
            Email = "clubUser@club.se"
        };

        var AppUser = new DatabaseUser
        {
            Id = "ba171f79-bf12-4862-8c8b-5556ee13e123",
            AppUser = new AppUser
            {
                FirstName = "Ellen",
                LastName = "Lundell"
        },
            UserName = "appUser@club.se",
            Email = "appUser@club.se"
        };

        var CompanyUser = new DatabaseUser
        {
            Id = "5963d781-6e33-4072-a585-6c6d6e7f891f",
            Company = company,
            UserName = "companyUser@club.se",
            Email = "companyUser@club.se"
        };
        #endregion
        await _userManager.CreateAsync(AppUser, "Passw0rd!");
        await _userManager.CreateAsync(clubUser, "Passw0rd!");
        await _userManager.CreateAsync(CompanyUser, "Passw0rd!");

        await _userManager.AddToRoleAsync(AppUser, StaticConfig.AppUserRole);
        await _userManager.AddToRoleAsync(clubUser, StaticConfig.ClubRole);
        await _userManager.AddToRoleAsync(CompanyUser, StaticConfig.CompanyRole);

        #region Tickets with Auctions
        List<Ticket> ticketsAuction1 = new List<Ticket>
        {
            new()
            {
                StartValidTime = DateTime.Today + TimeSpan.FromHours(20),
                EndValidTime = DateTime.Today + TimeSpan.FromHours(20) + TimeSpan.FromHours(3),
                Club = club,
                Price = 100,
                EventDescription = "Ticket 1 - Auction 1"
            },
            new()
            {
                StartValidTime = DateTime.Today + TimeSpan.FromHours(20),
                EndValidTime = DateTime.Today + TimeSpan.FromHours(20) + TimeSpan.FromHours(3),
                Club = club,
                Price = 100,
                EventDescription = "Ticket 2 - Auction 1"
            },
            new()
            {
                StartValidTime = DateTime.Today + TimeSpan.FromHours(20),
                EndValidTime = DateTime.Today + TimeSpan.FromHours(20) + TimeSpan.FromHours(3),
                Club = club,
                Price = 100,
                EventDescription = "Ticket 3 - Auction 1"
            },
            new()
            {
                StartValidTime = DateTime.Today + TimeSpan.FromHours(20),
                EndValidTime = DateTime.Today + TimeSpan.FromHours(20) + TimeSpan.FromHours(3),
                Club = club,
                Price = 100,
                EventDescription = "Ticket 4 - Auction 1"
            },
        };
        List<Ticket> ticketsAuction2 = new List<Ticket>
        {
            new()
            {
                StartValidTime = DateTime.Today + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club,
                Price = 100,
                EventDescription = "Ticket 1 - Auction 2"
            },
            new()
            {
                StartValidTime = DateTime.Today + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club,
                Price = 100,
                EventDescription = "Ticket 2 - Auction 2"
            },
            new()
            {
                StartValidTime = DateTime.Today + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club,
                Price = 100,
                EventDescription = "Ticket 3 - Auction 2"
            },
            new()
            {
                StartValidTime = DateTime.Today + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club,
                Price = 100,
                EventDescription = "Ticket 4 - Auction 2"
            },
        };
        List<Ticket> ticketsAuction3 = new List<Ticket>
        {
            new()
            {
                StartValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(20),
                EndValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(20) + TimeSpan.FromHours(3),
                Club = club,
                Price = 100,
                EventDescription = "Ticket 1 - Auction 3"
            },
            new()
            {
                StartValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(20),
                EndValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(20) + TimeSpan.FromHours(3),
                Club = club,
                Price = 100,
                EventDescription = "Ticket 2 - Auction 3"
            }
        };

        List<Auction> auctions = new List<Auction>
        {
            new()
            {
                Club = club,
                ExpiryDateTime = DateTime.Today + TimeSpan.FromHours(22),
                Title = "Auction number 1",
                IssueDateTime = DateTime.Today + TimeSpan.FromHours(20),
                Tickets = ticketsAuction1
            },
            new()
            {
                Club = club,
                ExpiryDateTime = DateTime.Today + TimeSpan.FromHours(23),
                Title = "Auction number 2",
                IssueDateTime = DateTime.Today + TimeSpan.FromHours(21),
                Tickets = ticketsAuction2
            },
            new()
            {
                Club = club,
                ExpiryDateTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(22),
                Title = "Auction number 3",
                IssueDateTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(20),
                Tickets = ticketsAuction3
            },
        };

        #endregion
        #region Tickets without Auctions

       
        List<Ticket> ticketsWithoutAuction = new List<Ticket>
        {
             //TODO Bugg with some blub tickets not displaying still exist. first() images[0] ends up as null on some
             //tickets. Database problem... Since we probalby wont use images on tickets later I wont fix it now. 
            #region club1 Locatelli
            new()
            {
                Id = new Guid("12355e99-cceb-499e-e90c-08da4e4ca855"),
                StartValidTime = DateTime.Today + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club,
                Price = 100,
                EventDescription = "EventDescription for " + club.Name + " event",
                Images = club.Images,
            },
            new()
            {
                Id = new Guid("12355e99-cceb-499e-e90c-08da4e4ca856"),
                StartValidTime = DateTime.Today + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club,
                Price = 100,
                EventDescription = "EventDescription for " + club.Name + " event",
                Images = club.Images,
            },
            new()
            {
                Id = new Guid("8bf6983f-3203-4c71-96d7-4f7d30e8c858"),
                StartValidTime = DateTime.Today + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club,
                Price = 100,
                EventDescription = "EventDescription for " + club.Name + " event",
                Images = club.Images,
            },
             new()
            {
                Id = new Guid("8bf6983f-3203-4c71-96d7-4f7d30e8c859"),
                StartValidTime = DateTime.Today + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club,
                Price = 100,
                EventDescription = "EventDescription for " + club.Name + " event",
                Images = club.Images,
            },
            #endregion
            #region club2 push
            new()
            {
                Id = new Guid("96caf1bd-c3fd-4fd8-8663-1fc8e7dfe6cb"),
                StartValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club2,
                Price = 150,
                EventDescription = "EventDescription for " + club2.Name + " event",
                Images = club2.Images,
            },
            new()
            {
                Id = new Guid("039626ff-8f3d-479a-9aa7-505af6351d78"),
                StartValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club2,
                Price = 150,
                EventDescription = "EventDescription for " + club2.Name + " event",
                Images = club2.Images,
            },
            #endregion
            #region club3 lounge
            new()
            {
                Id = new Guid("534f335c-c9d6-457f-b79d-0e00bb1d046a"),
                StartValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club3,
                Price = 150,
                EventDescription = "EventDescription for " + club3.Name + " event",
                Images = club3.Images,
            },
            new()
            {
                Id = new Guid("f1297cca-3a73-4fd3-974e-c976fab49a03"),
                StartValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club3,
                Price = 150,
                EventDescription = "EventDescription for " + club3.Name + " event",
                Images = club3.Images,
            },
            #endregion
            #region club4 Port du Soleil
            new()
            {
                Id = new Guid("49634be5-50d7-4d26-a156-785f601ea08d"),
                StartValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club4,
                Price = 150,
                EventDescription = "EventDescription for " + club4.Name + " event",
                Images = club4.Images
            },
            new()
            {
                Id = new Guid("a62233f8-ef33-4fe0-8739-e93441b275d0"),
                StartValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club4,
                Price = 150,
                EventDescription = "EventDescription for " + club4.Name + " event",
                Images = club4.Images
            },
            #endregion
            #region club5 Excet
            new()
            {
                Id = new Guid("3702073f-c2ed-4bc1-8a7e-0a31b6bccb49"),
                StartValidTime = DateTime.Today + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club5,
                Price = 150,
                EventDescription = "EventDescription for " + club5.Name + " event",
                Images = club5.Images
            },
             new()
            {
                Id = new Guid("20ffca68-5846-4579-ba45-9918434a1390"),
                StartValidTime = DateTime.Today + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club5,
                Price = 150,
                EventDescription = "EventDescription for " + club5.Name + " event",
                Images = club5.Images
            },
            #endregion
            #region club6 puta madre
            new()
            {
                Id = new Guid("f2e418e7-df58-47d9-99b1-2a0162ce83c5"),
                StartValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club6,
                Price = 150,
                EventDescription = "EventDescription for " + club6.Name + " event",
                Images = club6.Images
            },
            new()
            {
                Id = new Guid("b75ca21d-d528-4b8c-b064-07196d2ccc6b"),
                StartValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club6,
                Price = 150,
                EventDescription = "EventDescription for " + club6.Name + " event",
                Images = club6.Images
            },
            #endregion
            #region club7 stranger
            new()
            {
                Id = new Guid("ab716e08-4150-48cb-805e-5e2e3d3f5667"),
                StartValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club7,
                Price = 150,
                EventDescription = "EventDescription for " + club7.Name + " event",
                Images = club7.Images
            },
             new()
            {
                Id = new Guid("3f8a4710-bad4-4143-9ad1-bdb859b310f2"),
                StartValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(21),
                EndValidTime = DateTime.Today + TimeSpan.FromDays(1) + TimeSpan.FromHours(21) + TimeSpan.FromHours(3),
                Club = club7,
                Price = 150,
                EventDescription = "EventDescription for " + club7.Name + " event",
                Images = club7.Images
            }
            #endregion
        };
        
        
        #endregion

        _slippDbCtx.AddRange(ticketsWithoutAuction);
        _slippDbCtx.AddRange(auctions);
        await _slippDbCtx.SaveChangesAsync();
    }
}
