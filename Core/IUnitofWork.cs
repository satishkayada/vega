using System.Threading.Tasks;

namespace vega.Core
{
    public interface IUnitofWork
    {
        Task CompleteAsync();
    }

}