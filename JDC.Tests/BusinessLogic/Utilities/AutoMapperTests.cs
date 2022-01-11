using System;
using JDC.BusinessLogic.Utilities.AutoMapper;
using NUnit.Framework;

namespace JDC.Tests.BusinessLogic.Utilities
{
    [TestFixture]
    public class AutoMapperTests
    {
        private CompiledMapper<DtoType> mapper;

        [SetUp]
        public void Setup()
        {
            this.mapper = new CompiledMapper<DtoType>();
        }

        [Test]
        public void AutoMapper_ReturnDestinationObject()
        {
            // Arrange
            var source = new ModelType() { TheProperty = "string value" };

            // Act
            var destination = this.mapper.Map(source);

            // Assert
            Assert.AreEqual(source.TheProperty, destination.TheProperty);
        }

        [Test]
        public void AutoMapper_SourceObjectIsNull_ThrowArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => this.mapper.Map(null), message: "source");
        }

        public class ModelType
        {
            public string TheProperty { get; set; }
        }

        public class DtoType
        {
            public string TheProperty { get; set; }
        }
    }
}
