using socialMedia.Models;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using socialMedia.Data;

namespace socialMedia.Repositories
{
    public class LikeRepository : Repository<Like>, ICompositeKeyRepository<Like>
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Like> _dbSet;

        public LikeRepository(AppDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Like>();
        }


        public async Task AddAsync(Like entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Like> FindAsync(Expression<Func<Like, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Like>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Like> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Like> FirstOrDefaultAsync(Func<Like, bool> predicate)
        {
            return await Task.FromResult(_dbSet.FirstOrDefault(predicate));
        }

        public async Task Add(Like entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Like entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int userId, int postId)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(l => l.UserId == userId && l.PostId == postId);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
