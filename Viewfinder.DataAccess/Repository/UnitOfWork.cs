using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viewfinder.DataAccess.Repository.IRepository;
using ViewfinderWeb.Data;

namespace Viewfinder.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Composition = new CompositionRepository(_db);   
            Photograph = new PhotographRepository(_db);
            Company = new CompanyRepository(_db);
            
        }

        public ICategoryRepository Category { get; private set; }
        public ICompositionRepository Composition { get; private set; }
        public IPhotographRepository Photograph { get; private set; }
        public ICompanyRepository Company { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
