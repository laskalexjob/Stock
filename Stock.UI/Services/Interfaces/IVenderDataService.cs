using System.Collections.Generic;
using Stock.Model;

namespace Stock.UI.Services.Interfaces
{
    public interface IVenderDataService
    {
        IEnumerable<Vender> GetAllVenders();
    }
}
