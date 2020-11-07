namespace CheckoutSimulator.Domain.Tests
{
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using AutoFixture.Idioms;
    using CheckoutSimulator.Domain;
    using CheckoutSimulator.Domain.Repositories;
    using FluentAssertions;
    using FluentAssertions.Execution;
    using Moq;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;
    using static TestUtils.TestIdioms;

    public class TillFactoryTests
    {
        /// <summary>
        /// The Constructors Guards Against Null Args.
        /// </summary>
        [Fact]
        public void Constructor_GuardsAgainstNullArgs()
        {
            AssertConstructorsGuardAgainstNullArgs<TillFactory>();
        }


        /// <summary>
        /// Methods Guards Against Null Args.
        /// </summary>
        [Fact]
        public void Methods_GuardAgainstNullArgs()
        {
            AssertMethodsGuardAgainstNullArgs<TillFactory>();
        }

        [Fact]
        public async Task CreateTillAsync_Calls_Repository()
        {
            // Arrange
            var testFixture = new TestFixtureBuilder();
            var sut = testFixture.BuildSut();

            // Act
            var result = await sut.CreateTillAsync();

            // Assert
            testFixture.MockStockRepository.Verify(x => x.GetStockItemsAsync(), Times.Once);
        }

        private class TestFixtureBuilder
        {
            public Mock<IStockRepository> MockStockRepository;

            public TestFixtureBuilder()
            {
                this.MockStockRepository = new Mock<IStockRepository>();
                this.MockStockRepository.Setup(x => x.GetStockItemsAsync())
                    .Returns(Task.FromResult(Array.Empty<IStockKeepingUnit>().AsEnumerable()));
            }

            public TillFactory BuildSut()
            {
                return new TillFactory(this.MockStockRepository.Object);
            }
        }
    }
}
