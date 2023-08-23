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
    public class PhotographRepository : Repository<Photograph>, IPhotographRepository
    {
        private ApplicationDbContext _db;
        public PhotographRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }
        

        public void Update(Photograph obj)
        {
            var objFromDb = _db.Photographs.FirstOrDefault(u=>u.Id == obj.Id);
            if(objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.Description = obj.Description;
                objFromDb.Author = obj.Author;
                objFromDb.Price = obj.Price;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.CompositionId = obj.CompositionId;
                if(obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
