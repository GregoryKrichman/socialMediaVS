using socialMedia.Models;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using socialMedia.Data;

namespace socialMedia.Repositories
{
    public class CommentRepository : Repository<Comment>, IRepository<Comment>
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Comment> _dbSet;

        public CommentRepository(AppDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Comment>();
        }

        public async Task AddAsync(Comment entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync(); 
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Comment> FindAsync(Expression<Func<Comment, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Comment>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Comment> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<Comment> FirstOrDefaultAsync(Func<Comment, bool> predicate)
        {
            return await Task.FromResult(_dbSet.FirstOrDefault(predicate));
        }

        public async Task Add(Comment entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync(); 
        }

        public async Task Update(Comment entity)
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
