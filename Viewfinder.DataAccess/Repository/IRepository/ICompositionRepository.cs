using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viewfinder.Models;

namespace Viewfinder.DataAccess.Repository.IRepository
{
    public interface ICompositionRepository : IRepository<Composition>
    {
        void Update(Composition obj);

        
    }
}
