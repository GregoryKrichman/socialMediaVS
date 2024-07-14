using socialMedia.Models;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using socialMedia.Data;

namespace socialMedia.Repositories
{
    public class StoryRepository : Repository<Story>,IRepository<Story>
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Story> _dbSet;

        public StoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Story>();
        }

        public async Task AddAsync(Story entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Story> FindAsync(Expression<Func<Story, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Story>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Story> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<Story> FirstOrDefaultAsync(Func<Story, bool> predicate)
        {
            return await Task.FromResult(_dbSet.FirstOrDefault(predicate));
        }

        public async Task Add(Story entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Story entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public Task Delete(int key1, int key2)
        {
            throw new NotImplementedException();
        }
    }
}
