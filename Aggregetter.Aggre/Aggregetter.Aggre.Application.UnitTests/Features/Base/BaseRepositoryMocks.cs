using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Domain.Common;
using Moq;
using System;
using System.Threading;

namespace Aggregetter.Aggre.Application.UnitTests.Features.Base
{
    public class BaseRepositoryMocks<T> where T : BaseEntity, new()
    {
        public static Guid ExistingId = Guid.NewGuid();
        public static Mock<IBaseRepository<T>> GetBaseRepositoryMocks()
        {
            var baseRepositoryMocks = new Mock<IBaseRepository<T>>();

            baseRepositoryMocks.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(
                (Guid id, CancellationToken cancellationToken) =>
                {
                    if (id == ExistingId)
                        return new T();
                    return null;
                });
            return baseRepositoryMocks;
        }
    }
}
