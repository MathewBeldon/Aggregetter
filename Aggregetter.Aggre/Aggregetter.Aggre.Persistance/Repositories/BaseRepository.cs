using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Persistance.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly AggreDbContext _context;
        public BaseRepository(AggreDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<T>().ToListAsync(cancellationToken);
        }

        public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<IEnumerable<T>> GetPagedResponseAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            var entity = await _context.Set<T>().OrderByDescending(x => x.CreatedDateUtc).Skip((page - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync(cancellationToken);

            return entity;
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _context.Set<T>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
