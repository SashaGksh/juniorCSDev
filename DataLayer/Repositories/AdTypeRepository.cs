using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface IAdTypeRepository : IRepository<AdType>
    {
        //crud create reAdType update delete
        AdType create(AdType AdType);
        AdType getById(int id);
        AdType getByName(string adtype);
        AdType update(AdType AdType);
        void delete(int id);

    }
    public class AdTypeRepository : Repository<AdType>, IAdTypeRepository
    {
        public AdTypeRepository(AdDbContext context) : base(context) { }

        public AdType create(AdType AdType)
        {
            var res = _entities.Add(AdType).Entity;
            _dbContext.SaveChanges();
            return res;
        }

        public AdType getById(int id)
        {
            var res = _entities.Where(AdType => AdType.Id == id)
                .FirstOrDefault();
            if (res != null)
            {
                return res;
            }
            throw new Exception("AdType not found");
        }

        public AdType getByName(string adtype)
        {
            var res = _entities.Where(AdType => AdType.Name == adtype)
                .FirstOrDefault();
            if (res != null)
            {
                return res;
            }
            throw new Exception("Invalid type");
        }


        public AdType update(AdType AdType)
        {
            var res = _entities.Update(AdType).Entity;
            _dbContext.SaveChanges();
            return res;
        }

        public void delete(int id)
        {
            var res = _entities.FirstOrDefault(b => b.Id == id);
            if (res != null)
            {
                _dbContext.AdTypes.Remove(res);
                _dbContext.SaveChanges();
            }
            else
                throw new Exception("AdType not founded");

        }
    }
}
