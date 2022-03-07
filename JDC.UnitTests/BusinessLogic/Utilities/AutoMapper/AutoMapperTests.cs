using System;
using AutoFixture.NUnit3;
using JDC.BusinessLogic.Utilities.AutoMapper;
using JDC.Tests.BusinessLogic.AutoMapper.Models.Utilities;
using NUnit.Framework;

namespace JDC.Tests.BusinessLogic.AutoMapper.Utilities
{
    [TestFixture]
    public class AutoMapperTests
    {
        private CompiledMapper<DtoType> mapper;

        [SetUp]
        public void Setup()
        {
            mapper = new CompiledMapper<DtoType>();
        }

        [Test]
        [Theory, AutoData]
        public void AutoMapper_ReturnDestinationObject([Frozen]ModelType source)
        {
            // Act
            var destination = mapper.Map(source);

            // Assert
            Assert.AreEqual(source.TheProperty, destination.TheProperty);
        }

        [Test]
        public void AutoMapper_SourceObjectIsNull_ThrowArgumentNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => mapper.Map(null), message: "source");
        }
    }
}
