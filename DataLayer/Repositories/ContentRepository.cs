using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface IContentRepository : IRepository<Content>
    {
        //crud create reContent update delete
        Content create(Content Content);
        Content getById(int id);
        Content update(Content Content);
        void delete(int id);

    }
    public class ContentRepository : Repository<Content>, IContentRepository
    {
        public ContentRepository(AdDbContext context) : base(context) { }

        public Content create(Content Content)
        {
            var res = _entities.Add(Content).Entity;
            _dbContext.SaveChanges();
            return res;
        }

        public Content getById(int id)
        {
            var res = _entities.Where(Content => Content.Id == id)
                .FirstOrDefault();
            if (res != null)
            {
                return res;
            }
            throw new Exception("Content not found");
        }

        public Content update(Content Content)
        {
            var res = _entities.Update(Content).Entity;
            _dbContext.SaveChanges();
            return res;
        }

        public void delete(int id)
        {
            var res = _entities.FirstOrDefault(b => b.Id == id);
            if (res != null)
            {
                _dbContext.Contents.Remove(res);
                _dbContext.SaveChanges();
            }
            else
                throw new Exception("Content not founded");

        }
    }
}
