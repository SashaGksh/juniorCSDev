using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        AdDTO AdDTOConvert(Ad ad);
    }
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly AdDbContext _dbContext;
        protected readonly DbSet<TEntity> _entities;
        public Repository(AdDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = dbContext.Set<TEntity>();
        }
        public virtual IEnumerable<TEntity> GetAll()
        {
            return _entities.ToList();
        }
        public virtual AdDTO AdDTOConvert(Ad ad)
        {
            AdDTO adDTO = new AdDTO();
            adDTO.Adtype = ad.AdType.Name;
            adDTO.Category = ad.Category.Name;
            adDTO.ContentStructure = ad.Content.Structure;
            adDTO.ContentLink = ad.Content.Link;
            var adtags = _dbContext.AdTags.Where(a => a.Ad_Id == ad.Id);
            var tags = adtags.Select(a => a.Tag.Name);
            adDTO.Tags = string.Join(" ", tags);
            return adDTO;
        }
    }
}
