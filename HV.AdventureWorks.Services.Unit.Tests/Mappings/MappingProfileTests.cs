using System;
using System.Text.Json;
using AutoMapper;
using NUnit.Framework;
using HV.AdventureWorks.Data.Entities;
using HV.AdventureWorks.Services.Mappings;
using HV.AdventureWorks.Services.Models;

namespace HV.AdventureWorks.Services.Unit.Tests.Mappings
{
    [TestFixture]
    public class MappingProfileTests
    {
        private static readonly DateTime DateTime = new DateTime(2020, 01, 01);
        private static readonly Guid Guid = Guid.NewGuid();

        private readonly Product model = new Product()
        {
            Id = 0,
            Name = "Test",
            ProductNumber = "Test",
            MakeFlag = true,
            FinishedGoodsFlag = true,
            Color = "Test",
            SafetyStockLevel = 0,
            ReorderPoint = 0,
            StandardCost = 0,
            ListPrice = 0,
            Size = "Test",
            Weight = 0,
            DaysToManufacture = 0,
            ProductLine = "Test",
            Class = "Test",
            Style = "Test",
            SellStartDate = DateTime,
            SellEndDate = DateTime,
            DiscontinuedDate = DateTime,
            RowGuid = Guid,
            ModifiedDate = DateTime,
            ProductSubcategoryId = 0,
            ProductModelId = 0,
            SizeUnitMeasureCode = "T",
            WeightUnitMeasureCode = "T"
        };

        private readonly ProductEntity entity = new ProductEntity()
        {
            Id = 0,
            Name = "Test",
            ProductNumber = "Test",
            MakeFlag = true,
            FinishedGoodsFlag = true,
            Color = "Test",
            SafetyStockLevel = 0,
            ReorderPoint = 0,
            StandardCost = 0,
            ListPrice = 0,
            Size = "Test",
            Weight = 0,
            DaysToManufacture = 0,
            ProductLine = "Test",
            Class = "Test",
            Style = "Test",
            SellStartDate = DateTime,
            SellEndDate = DateTime,
            DiscontinuedDate = DateTime,
            RowGuid = Guid,
            ModifiedDate = DateTime,
            ProductSubcategoryId = 0,
            ProductModelId = 0,
            SizeUnitMeasureCode = "T",
            WeightUnitMeasureCode = "T"
        };

        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()).CreateMapper();
        }

        [Test]
        public void Product_To_ProductEntity_SuccessfullyMapped()
        {
            // Act
            var result = _mapper.Map<ProductEntity>(model);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(JsonSerializer.Serialize(entity), JsonSerializer.Serialize(result));
        }

        [Test]
        public void ProductEntity_To_Product_SuccessfullyMapped()
        {
            // Act
            var result = _mapper.Map<Product>(entity);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(JsonSerializer.Serialize(model), JsonSerializer.Serialize(result));
        }
    }
}
