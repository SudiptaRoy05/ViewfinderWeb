using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viewfinder.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        ICompositionRepository Composition { get; }
        IPhotographRepository Photograph { get; }
        ICompanyRepository Company { get; }

        void Save();
    }
}
