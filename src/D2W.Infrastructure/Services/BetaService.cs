using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Infrastructure.Services
{
    public class BetaService : IBetaService
    {
        private readonly ApplicationDbContext _dbContext;

        public bool IsMaxBetaTestersReached()
        {
            throw new NotImplementedException();
        }
    }
}
