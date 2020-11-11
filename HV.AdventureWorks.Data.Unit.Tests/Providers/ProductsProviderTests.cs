using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using HV.AdventureWorks.Core.Data.Interfaces;
using HV.AdventureWorks.Data.Entities;
using HV.AdventureWorks.Data.Providers;

namespace HV.AdventureWorks.Data.Unit.Tests.Providers
{
    [TestFixture]
    public class ProductsProviderTests
    {
        private const int Id = 1;
        private static readonly ProductEntity _entity = new ProductEntity() { Id = Id };
        private readonly List<ProductEntity> _entities = new List<ProductEntity>() { _entity };

        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IRepository<ProductEntity>> _repositoryMock;

        private ProductsProvider _productsProvider;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
            _repositoryMock = new Mock<IRepository<ProductEntity>>(MockBehavior.Strict);

            _productsProvider = new ProductsProvider(_unitOfWorkMock.Object);

            _unitOfWorkMock
                .Setup(x => x.Repository<ProductEntity>())
                .Returns(_repositoryMock.Object);
            _unitOfWorkMock
               .Setup(x => x.SaveChanges())
               .Verifiable();

            _repositoryMock
                .Setup(x => x.GetAsNoTracking())
                .Returns(_entities.AsQueryable());
            _repositoryMock
                .Setup(x => x.Add(It.IsAny<ProductEntity>()))
                .Callback((ProductEntity entity) =>
                {
                    entity.Id = 1;
                })
                .Verifiable();
            _repositoryMock
                .Setup(x => x.Attach(It.IsAny<ProductEntity>()))
                .Verifiable();
            _repositoryMock
                .Setup(x => x.Delete(It.IsAny<ProductEntity>()))
                .Verifiable();
        }

        [Test]
        public void GetAll_Should_Call_UnitOfWork_And_Repository()
        {
            // Act
            _ = _productsProvider.GetAll();

            // Assert
            _unitOfWorkMock.Verify(x => x.Repository<ProductEntity>(), Times.Once());
            _repositoryMock.Verify(x => x.GetAsNoTracking(), Times.Once());
        }

        [Test]
        public void GetAll_Should_Return_CorrectResult()
        {
            // Act
            var result = _productsProvider.GetAll();

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(_entities, result);
        }

        [Test]
        public void GetById_Should_Call_UnitOfWork_And_Repository()
        {
            // Act
            _ = _productsProvider.GetById(Id);

            // Assert
            _unitOfWorkMock.Verify(x => x.Repository<ProductEntity>(), Times.Once());
            _repositoryMock.Verify(x => x.GetAsNoTracking(), Times.Once());
        }

        [Test]
        public void GetById_Should_Return_CorrectResult()
        {
            // Act
            var result = _productsProvider.GetById(Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_entity, result);
        }

        [Test]
        public void Create_Should_Call_UnitOfWork_And_Repository()
        {
            // Act
            _ = _productsProvider.Create(new ProductEntity());

            // Assert
            _unitOfWorkMock.Verify(x => x.Repository<ProductEntity>(), Times.Once());
            _repositoryMock.Verify(x => x.Add(It.IsAny<ProductEntity>()), Times.Once());
            _unitOfWorkMock.Verify(x => x.SaveChanges(), Times.Once());
        }

        [Test]
        public void Create_Should_Return_CorrectResult()
        {
            // Act
            var result = _productsProvider.Create(new ProductEntity());

            // Assert
            Assert.IsNotNull(result);
            Assert.NotZero(result.Id);
        }

        [Test]
        public void Update_Should_Call_UnitOfWork_And_Repository()
        {
            // Act
            _ = _productsProvider.Update(new ProductEntity());

            // Assert
            _unitOfWorkMock.Verify(x => x.Repository<ProductEntity>(), Times.Once());
            _repositoryMock.Verify(x => x.Attach(It.IsAny<ProductEntity>()), Times.Once());
            _unitOfWorkMock.Verify(x => x.SaveChanges(), Times.Once());
        }

        [Test]
        public void Update_Should_Return_CorrectResult()
        {
            // Act
            var result = _productsProvider.Update(new ProductEntity());

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Delete_Should_Call_UnitOfWork_And_Repository()
        {
            // Act
            _productsProvider.Delete(Id);

            // Assert
            _unitOfWorkMock.Verify(x => x.Repository<ProductEntity>(), Times.Exactly(2));
            _repositoryMock.Verify(x => x.GetAsNoTracking(), Times.Once());
            _repositoryMock.Verify(x => x.Delete(It.Is<ProductEntity>(e => e == _entity)), Times.Once());
            _unitOfWorkMock.Verify(x => x.SaveChanges(), Times.Once());
        }
    }
}
