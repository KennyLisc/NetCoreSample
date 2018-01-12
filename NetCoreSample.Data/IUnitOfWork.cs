//IMPORTANT - Modifications to this file may be overwritten:
//If you need to implement your own logic/code do it in a partial class/interface.

using System.Threading.Tasks;

namespace FXTech.PDCA.Core.Interfaces.Data
{
    public interface IUnitOfWork
    {
        int Commit();

        Task<int> CommitAsync();
    }
}