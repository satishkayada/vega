using System.Threading.Tasks;

namespace vega.Persistence
{
    public class UnitofWork : IUnitofWork
    {
        private readonly VegaDbContext context;
        public UnitofWork(VegaDbContext context)
        {
            this.context = context;
        }
        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}