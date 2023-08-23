using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viewfinder.DataAccess.Repository.IRepository;
using Viewfinder.Models;
using ViewfinderWeb.Data;

namespace Viewfinder.DataAccess.Repository
{
    public class CompositionRepository : Repository<Composition>, ICompositionRepository
    {
        private ApplicationDbContext _db;
        public CompositionRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }
        

        public void Update(Composition obj)
        {
            _db.Compositions.Update(obj);
        }
    }
}
