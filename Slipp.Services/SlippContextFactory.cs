using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Slipp.Services;

public class SlippContextFactory : IDesignTimeDbContextFactory<SlippDbCtx>
{
    public SlippDbCtx CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SlippDbCtx>();
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\mssqllocaldb;Database=SlippDb;Trusted_Connection=True;MultipleActiveResultSets=true",
            b => b.MigrationsAssembly("SlippAPI"));

        return new SlippDbCtx(optionsBuilder.Options);
    }
}