using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface IAdTagRepository : IRepository<AdTag>
    {
        //crud create reAdTag update delete
        AdTag create(AdTag AdTag);
        AdTag getById(int id);
        AdTag update(AdTag AdTag);
        void delete(int id);

    }
    public class AdTagRepository : Repository<AdTag>, IAdTagRepository
    {
        public AdTagRepository(AdDbContext context) : base(context) { }

        public AdTag create(AdTag AdTag)
        {
            var res = _entities.Add(AdTag).Entity;
            _dbContext.SaveChanges();
            return res;
        }

        public AdTag getById(int id)
        {
            var res = _entities.Where(AdTag => AdTag.Id == id)
                .FirstOrDefault();
            if (res != null)
            {
                return res;
            }
            throw new Exception("AdTag not found");
        }

        public AdTag update(AdTag AdTag)
        {
            var res = _entities.Update(AdTag).Entity;
            _dbContext.SaveChanges();
            return res;
        }

        public void delete(int id)
        {
            var res = _entities.FirstOrDefault(b => b.Id == id);
            if (res != null)
            {
                _dbContext.AdTags.Remove(res);
                _dbContext.SaveChanges();
            }
            else
                throw new Exception("AdTag not founded");

        }
        public override IEnumerable<AdTag> GetAll()
        {
            return _dbContext.AdTags
                .Include(at => at.Ad)
                    .ThenInclude(a => a.AdType)
                .Include(at => at.Ad)
                    .ThenInclude(a => a.Category)
                .Include(at => at.Ad)
                    .ThenInclude(a => a.Content)
                .Include(at => at.Ad)
                    .ThenInclude(a => a.AdTags)
                    .ThenInclude(at => at.Tag);


        }
    }
}
