using socialMedia.Models;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using socialMedia.Data;

namespace socialMedia.Repositories
{
    public class PostRepository : Repository<Post>,IRepository<Post>
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Post> _dbSet;

        public PostRepository(AppDbContext context) :base(context)
        {
            _context = context;
            _dbSet = _context.Set<Post>();
        }

        public async Task AddAsync(Post entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Post> FindAsync(Expression<Func<Post, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Post> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<Post> FirstOrDefaultAsync(Func<Post, bool> predicate)
        {
            return await Task.FromResult(_dbSet.FirstOrDefault(predicate));
        }

        public async Task Add(Post entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task Update(Post entity)
        {
            _dbSet.Update(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }
        public Task Delete(int key1, int key2)
        {
            throw new NotImplementedException();
        }
    }
}
