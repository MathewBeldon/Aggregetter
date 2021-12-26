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

        public Task<int> GetCount()
        {
            return _context.Articles.CountAsync();
        }

        public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var queryEncoded = await _cache.GetAsync(id.ToString(), cancellationToken);

            if (queryEncoded is null)
            {
                var entity = await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken);
                _ = Task.Run(() => CacheObject(id.ToString(), EncodeObject(entity), cancellationToken));
                return entity;
            }

            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(queryEncoded));
        }

        public async Task<(IEnumerable<T>, int)> GetPagedResponseAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            var key = $"{typeof(T).Name}-{page}-{pageSize}";
            var queryEncoded = await _cache.GetAsync(key, cancellationToken);

            if (queryEncoded is null)
            {
                var total = await _context.Articles.CountAsync();
                var entity = await _context.Set<T>().OrderByDescending(x => x.CreatedDateUtc).Skip((page - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync(cancellationToken);
                _ = Task.Run(() => CacheObject(key, EncodeObject((entity, total)), cancellationToken));
                return (entity, total);
            }

            return JsonConvert.DeserializeObject<(IEnumerable<T>, int)>(Encoding.UTF8.GetString(queryEncoded));
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

        protected byte[] EncodeObject(object obj)
        {
            if (obj is null) throw new ArgumentNullException(nameof(obj));

            var entitySerialised = JsonConvert.SerializeObject(obj);
            return Encoding.UTF8.GetBytes(entitySerialised);
        }

        protected async Task CacheObject(string key, byte[] serialiseEntity, CancellationToken cancellationToken)
        {
            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(1));
            
            await _cache.RemoveAsync(key, cancellationToken);
            await _cache.SetAsync(key, serialiseEntity, options, cancellationToken);
        }
    }
}
