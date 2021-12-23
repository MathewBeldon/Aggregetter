using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aggregetter.Aggre.Persistance.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly AggreDbContext _context;
        protected readonly IDistributedCache _cache;
        public BaseRepository(AggreDbContext context, 
            IDistributedCache cache)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var queryEncoded = await _cache.GetAsync(id.ToString(), cancellationToken);

            if (queryEncoded is null)
            {
                var entity = await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken);

                Task.Run(() => CacheObject(id.ToString(), entity, cancellationToken));
                return entity;
            }

            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(queryEncoded));
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> GetPagedResponseAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            return (await _context.Set<T>().OrderByDescending(x => x.CreatedDateUtc).Skip((page - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync(cancellationToken));
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

        protected async Task CacheObject(string key, T entity, CancellationToken cancellationToken)
        {
            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(1));

            var entitySerialised = JsonConvert.SerializeObject(entity);
            var entityEncoded = Encoding.UTF8.GetBytes(entitySerialised);
            await _cache.RemoveAsync(key, cancellationToken);
            await _cache.SetAsync(key, entityEncoded, options, cancellationToken);
        }
    }
}
