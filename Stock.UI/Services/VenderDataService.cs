using System;
using System.Collections.Generic;
using System.Linq;
using Stock.DataAccess.Repositories;
using Stock.Model;
using Stock.UI.Services.Interfaces;

namespace Stock.UI.Services
{
    public class VenderDataService : IVenderDataService
    {
        private Func<EFGenericRepository<Vender>> _vendersRepo;

        public VenderDataService(Func<EFGenericRepository<Vender>> verdersRepo)
        {
            _vendersRepo = verdersRepo;
        }

        public IEnumerable<Vender> GetAllVenders()
        {
            return _vendersRepo.Invoke().GetWithInclude(x => x.Items).ToList();
        }
    }
}
