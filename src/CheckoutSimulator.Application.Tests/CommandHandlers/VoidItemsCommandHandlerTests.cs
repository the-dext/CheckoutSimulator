// Checkout Simulator by Chris Dexter, file="VoidItemsCommandHandlerTests.cs"

namespace CheckoutSimulator.Application.Tests.CommandHandlers
{
    using AutoFixture;
    using CheckoutSimulator.Application.CommandHandlers;
    using CheckoutSimulator.Application.Commands;
    using CheckoutSimulator.Domain;
    using FluentAssertions;
    using Moq;
    using Xunit;
    using static TestUtils.TestIdioms;

    /// <summary>
    /// Defines the <see cref="VoidItemsCommandHandlerTests" />.
    /// </summary>
    public class VoidItemsCommandHandlerTests
    {
        /// <summary>
        /// The Constructors Guards Against Null Args.
        /// </summary>
        [Fact]
        public void Constructor_GuardsAgainstNullArgs()
        {
            AssertConstructorsGuardAgainstNullArgs<VoidItemsCommandHandler>();
        }

        /// <summary>
        /// The Handle_Calls_Till_ScanItem.
        /// </summary>
        [Fact]
        public void Handle_Calls_Till_ScanItem()
        {
            var testFixtureBuilder = new TestFixtureBuilder();
            var sut = testFixtureBuilder.BuildSut();

            var result = sut.Handle(new VoidItemsCommand(), default);

            testFixtureBuilder.MockTill.Verify(x => x.VoidItems());
        }

        /// <summary>
        /// Methods the guard against null arguments.
        /// </summary>
        [Fact]
        public void Methods_GuardAgainstNullArgs()
        {
            AssertMethodsGuardAgainstNullArgs<VoidItemsCommandHandler>();
        }

        /// Defines the <see cref="TestFixture"/>.
        /// </summary>
        /// <summary>
        /// Defines the <see cref="TestFixtureBuilder" />.
        /// </summary>
        private class TestFixtureBuilder
        {
            public Fixture Fixture;

            public Mock<ITill> MockTill;

            /// <summary>
            /// Initializes a new instance of the <see cref="TestFixtureBuilder"/> class.
            /// </summary>
            public TestFixtureBuilder()
            {
                this.Fixture = new Fixture();
                this.MockTill = this.Fixture.Freeze<Mock<ITill>>();
            }

            /// <summary>
            /// The BuildSut.
            /// </summary>
            /// <returns>The <see cref="ScanItemCommandHandler"/>.</returns>
            public VoidItemsCommandHandler BuildSut()
            {
                return new VoidItemsCommandHandler(this.MockTill.Object);
            }
        }
    }
}
