using socialMedia.Models;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using socialMedia.Data;

namespace socialMedia.Repositories
{
    public class RelationshipRepository : Repository<Relationship>,IRepository<Relationship>
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Relationship> _dbSet;

        public RelationshipRepository(AppDbContext context) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Relationship>();
        }

        public async Task AddAsync(Relationship entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Relationship> FindAsync(Expression<Func<Relationship, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Relationship>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Relationship> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<Relationship> FirstOrDefaultAsync(Func<Relationship, bool> predicate)
        {
            return await Task.FromResult(_dbSet.FirstOrDefault(predicate));
        }

        public async Task Add(Relationship entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Relationship entity)
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
