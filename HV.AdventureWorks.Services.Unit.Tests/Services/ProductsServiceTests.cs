using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Moq;
using NUnit.Framework;
using HV.AdventureWorks.Data.Entities;
using HV.AdventureWorks.Data.Interfaces;
using HV.AdventureWorks.Services.Models;
using HV.AdventureWorks.Services.Services;

namespace HV.AdventureWorks.Services.Unit.Tests.Services
{
    [TestFixture]
    public class ProductsServiceTests
    {
        private const int Id = 1;
        private readonly List<ProductEntity> _entities = new List<ProductEntity>();
        private readonly List<Product> _models = new List<Product>();

        private readonly ProductEntity _entity = new ProductEntity();
        private readonly Product _model = new Product();

        private Mock<IProductsProvider> _productsProviderMock;
        private Mock<IMapper> _mapperMock;
        
        private ProductsService _productsService;

        [SetUp]
        public void Setup()
        {
            _mapperMock = new Mock<IMapper>(MockBehavior.Strict);
            _productsProviderMock = new Mock<IProductsProvider>(MockBehavior.Strict);

            _productsService = new ProductsService(
                _productsProviderMock.Object,
                _mapperMock.Object);

            _mapperMock
                .Setup(x => x.Map<IQueryable<ProductEntity>, IEnumerable<Product>>(It.IsAny<IQueryable<ProductEntity>>()))
                .Returns(_models);
            _mapperMock
                .Setup(x => x.Map<Product>(It.IsAny<ProductEntity>()))
                .Returns(_model);
            _mapperMock
                .Setup(x => x.Map<ProductEntity>(It.IsAny<Product>()))
                .Returns(_entity);

            _productsProviderMock
                .Setup(x => x.GetAll())
                .Returns(_entities.AsQueryable());
            _productsProviderMock
               .Setup(x => x.GetById(It.IsAny<int>()))
               .Returns(_entity);
            _productsProviderMock
               .Setup(x => x.Create(It.IsAny<ProductEntity>()))
               .Returns(_entity);
            _productsProviderMock
               .Setup(x => x.Update(It.IsAny<ProductEntity>()))
               .Returns(_entity);
            _productsProviderMock
              .Setup(x => x.Delete(It.IsAny<int>()))
              .Verifiable();
        }

        [Test]
        public void GetAll_Should_Call_Provider_And_Mapper()
        {
            // Act
            _ = _productsService.GetAll();

            // Assert
            _productsProviderMock.Verify(x => x.GetAll(), Times.Once());
            _mapperMock.Verify(x => x.Map<IQueryable<ProductEntity>, IEnumerable<Product>>(It.IsAny<IQueryable<ProductEntity>>()), Times.Once());
        }

        [Test]
        public void GetAll_Should_Return_CorrectResult()
        {
            // Act
            var result = _productsService.GetAll();

            // Assert
            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(_models, result);
        }

        [Test]
        public void GetById_Should_Call_Provider_And_Mapper()
        {
            // Act
            _ = _productsService.GetById(Id);

            // Assert
            _productsProviderMock.Verify(x => x.GetById(Id), Times.Once());
            _mapperMock.Verify(x => x.Map<Product>(It.IsAny<ProductEntity>()), Times.Once());
        }

        [Test]
        public void GetById_Should_Return_CorrectResult()
        {
            // Act
            var result = _productsService.GetById(Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_model, result);
        }

        [Test]
        public void Create_Should_Call_Provider_And_Mapper()
        {
            // Act
            _ = _productsService.Create(_model);

            // Assert
            _productsProviderMock.Verify(x => x.Create(It.IsAny<ProductEntity>()), Times.Once());
            _mapperMock.Verify(x => x.Map<Product>(It.IsAny<ProductEntity>()), Times.Once());
            _mapperMock.Verify(x => x.Map<ProductEntity>(It.IsAny<Product>()), Times.Once());
        }

        [Test]
        public void Create_Should_Return_CorrectResult()
        {
            // Act
            var result = _productsService.Create(_model);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_model, result);
        }

        [Test]
        public void Update_Should_Call_Provider_And_Mapper()
        {
            // Act
            _ = _productsService.Update(_model);

            // Assert
            _productsProviderMock.Verify(x => x.Update(It.IsAny<ProductEntity>()), Times.Once());
            _mapperMock.Verify(x => x.Map<Product>(It.IsAny<ProductEntity>()), Times.Once());
            _mapperMock.Verify(x => x.Map<ProductEntity>(It.IsAny<Product>()), Times.Once());
        }

        [Test]
        public void Update_Should_Return_CorrectResult()
        {
            // Act
            var result = _productsService.Update(_model);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(_model, result);
        }

        [Test]
        public void Update_Should_Call_Provider()
        {
            // Act
            _productsService.Delete(Id);

            // Assert
            _productsProviderMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
        }
    }
}
