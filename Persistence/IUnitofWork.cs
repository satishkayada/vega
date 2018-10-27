using System.Threading.Tasks;

namespace vega.Persistence
{
    public interface IUnitofWork
    {
        Task CompleteAsync();
    }

}