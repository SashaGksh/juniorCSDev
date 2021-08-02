using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    {
        //crud create reTag update delete
        Tag create(Tag Tag);
        Tag getById(int id);
        Tag update(Tag Tag);
        void delete(int id);

    }
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        public TagRepository(AdDbContext context) : base(context) { }

        public Tag create(Tag Tag)
        {
            var res = _entities.Add(Tag).Entity;
            _dbContext.SaveChanges();
            return res;
        }

        public Tag getById(int id)
        {
            var res = _entities.Where(Tag => Tag.Id == id)
                .FirstOrDefault();
            if (res != null)
            {
                return res;
            }
            throw new Exception("Tag not found");
        }

        public Tag update(Tag Tag)
        {
            var res = _entities.Update(Tag).Entity;
            _dbContext.SaveChanges();
            return res;
        }

        public void delete(int id)
        {
            var res = _entities.FirstOrDefault(b => b.Id == id);
            if (res != null)
            {
                _dbContext.Tags.Remove(res);
                _dbContext.SaveChanges();
            }
            else
                throw new Exception("Tag not founded");
        }

        
    }
}
