// Checkout Simulator by Chris Dexter, file="GetTotalPriceQueryHandlerTests.cs"

namespace CheckoutSimulator.Application.Tests.Queries
{
    using System;
    using System.Threading.Tasks;
    using AutoFixture;
    using CheckoutSimulator.Application.Queries;
    using CheckoutSimulator.Domain;
    using FluentAssertions;
    using MediatR;
    using Moq;
    using Xunit;
    using static TestUtils.TestIdioms;

    /// <summary>
    /// Defines the <see cref="GetTotalPriceQueryHandlerTests" />.
    /// </summary>
    public class GetTotalPriceQueryHandlerTests
    {
        /// <summary>
        /// The Constructors Guards Against Null Args.
        /// </summary>
        [Fact]
        public void Constructor_GuardsAgainstNullArgs()
        {
            AssertConstructorsGuardAgainstNullArgs<GetTotalPriceQueryHandler>();
        }

        [Fact]
        public void GetTotalPriceQueryHandler_Is_Typeof_IRequest()
        {
            typeof(GetTotalPriceQueryHandler).Should().BeAssignableTo<IRequestHandler<GetTotalPriceQuery, double>>();
        }

        /// <summary>
        /// The Handle_Returns_Total.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        [Fact]
        public async Task Handle_Returns_Total()
        {
            // Arrange
            var testFixture = new TestFixtureBuilder();
            const double ExpectedUnitPrice = 0.45;
            var sut = testFixture
                .WithTotalCheckoutPrice(ExpectedUnitPrice)
                .BuildSut();

            var cancellationToken = default(global::System.Threading.CancellationToken);

            // Act
            var result = await sut.Handle(new GetTotalPriceQuery(), cancellationToken);

            // Assert
            result.Should().Be(ExpectedUnitPrice);
        }

        /// <summary>
        /// Methods Guards Against Null Args.
        /// </summary>
        [Fact]
        public void Methods_GuardAgainstNullArgs()
        {
            AssertMethodsGuardAgainstNullArgs<GetTotalPriceQueryHandler>();
        }

        /// <summary>
        /// Defines the <see cref="TestFixtureBuilder" />.
        /// </summary>
        private class TestFixtureBuilder
        {
            public Fixture Fixture;

            public ITill Till;

            /// <summary>
            /// Initializes a new instance of the <see cref="TestFixtureBuilder"/> class.
            /// </summary>
            public TestFixtureBuilder()
            {
                this.Fixture = new Fixture();
            }

            /// <summary>
            /// The BuildSut.
            /// </summary>
            /// <returns>The <see cref="GetTotalPriceQueryHandler"/>.</returns>
            public GetTotalPriceQueryHandler BuildSut()
            {
                return new GetTotalPriceQueryHandler(this.Till);
            }

            /// <summary>
            /// The WithScannedItem.
            /// </summary>
            /// <param name="price">The price<see cref="double"/>.</param>
            /// <returns>The <see cref="TestFixtureBuilder"/>.</returns>
            public TestFixtureBuilder WithTotalCheckoutPrice(double price)
            {
                var sku = Mock.Of<IStockKeepingUnit>(x => x.Barcode == "B14" && x.UnitPrice == price && x.Description == "Biscuits");
                this.Till = Mock.Of<ITill>();
                Mock.Get(this.Till).Setup(x => x.RequestTotalPrice()).Returns(price);
                return this;
            }
        }
    }
}
