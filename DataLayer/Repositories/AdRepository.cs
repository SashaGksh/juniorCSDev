using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface IAdRepository : IRepository<Ad>
    {
        //crud create read update delete
        Ad create(Ad Ad);
        Ad getById(int id);
        Ad update(Ad Ad);
        void delete(int id);

    }
    public class AdRepository : Repository<Ad>, IAdRepository
    {
        public AdRepository(AdDbContext context) : base(context) { }

        public Ad create(Ad Ad)
        {
            var res = _entities.Add(Ad).Entity;
            _dbContext.SaveChanges();
            return res;
        }

        public Ad getById(int id)
        {
            var res = _entities.Where(Ad => Ad.Id == id)
                .FirstOrDefault();
            if (res != null)
            {
                return res;
            }
            throw new Exception("Ad not found");
        }

        public Ad update(Ad Ad)
        {
            var res = _entities.Update(Ad).Entity;
            _dbContext.SaveChanges();
            return res;
        }

        public void delete(int id)
        {
            var res = _entities.FirstOrDefault(b => b.Id == id);
            if (res != null)
            {
                _dbContext.Ads.Remove(res);
                _dbContext.SaveChanges();
            }
            else
                throw new Exception("Ad not founded");

        }

        public override IEnumerable<Ad> GetAll()
        {
            return _dbContext.Ads
                .Include(a => a.AdType)
                .Include(a => a.Category)
                .Include(a => a.Content)
                .Include(a => a.AdTags)
                    .ThenInclude(at => at.Tag);
            
        }
    }
}
