using DataLayer.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        //crud create reCategory update delete
        Category create(Category Category);
        Category getById(int id);
        Category getByName(string category);
        Category update(Category Category);
        void delete(int id);

    }
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AdDbContext context) : base(context) { }

        public Category create(Category Category)
        {
            var res = _entities.Add(Category).Entity;
            _dbContext.SaveChanges();
            return res;
        }

        public Category getById(int id)
        {
            var res = _entities.Where(Category => Category.Id == id)
                .FirstOrDefault();
            if (res != null)
            {
                return res;
            }
            throw new Exception("Category not found");
        }
        public Category getByName(string category)
        {
            var res = _entities.Where(Category => Category.Name == category)
                .FirstOrDefault();
            if (res != null)
            {
                return res;
            }
            throw new Exception("Invalid type");
        }


        public Category update(Category Category)
        {
            var res = _entities.Update(Category).Entity;
            _dbContext.SaveChanges();
            return res;
        }

        public void delete(int id)
        {
            var res = _entities.FirstOrDefault(b => b.Id == id);
            if (res != null)
            {
                _dbContext.Categories.Remove(res);
                _dbContext.SaveChanges();
            }
            else
                throw new Exception("Category not founded");

        }
        public override IEnumerable<Category> GetAll()
        {
            return _dbContext.Categories
                .Include(c => c.Ads);

        }
    }
}
