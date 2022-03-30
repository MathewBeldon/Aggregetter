using Aggregetter.Aggre.Application.Contracts.Persistence;
using Aggregetter.Aggre.Domain.Common;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Aggregetter.Aggre.Application.UnitTests.Features.Base
{
    public class BaseRepositoryMocks<T> where T : BaseEntity, new()
    {
        public static int ExistingId = 1;
        public static Mock<IBaseRepository<T>> GetBaseRepositoryMocks()
        {
            var baseRepositoryMocks = new Mock<IBaseRepository<T>>();

            baseRepositoryMocks.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(
                (int id, CancellationToken cancellationToken) =>
                {
                    return id == ExistingId ? new T() {  Id = ExistingId } : null;
                });

            baseRepositoryMocks.Setup(repo => repo.CheckExistsByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(
                (int id, CancellationToken cancellationToken) =>
                {
                    return id == ExistingId;
                });

            baseRepositoryMocks.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(
                (CancellationToken cancellationToken) =>
                {
                    return new List<T> { new T() { Id = ExistingId } };
                });

            return baseRepositoryMocks;
        }
    }
}
