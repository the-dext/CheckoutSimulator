// Checkout Simulator by Chris Dexter, file="ScanItemCommandHandlerTests.cs"

namespace CheckoutSimulator.Application.Tests.CommandHandlers
{
    using AutoFixture;
    using CheckoutSimulator.Application.CommandHandlers;
    using CheckoutSimulator.Application.Commands;
    using CheckoutSimulator.Domain;
    using FluentAssertions;
    using MediatR;
    using Moq;
    using Xunit;
    using static TestUtils.TestIdioms;

    /// <summary>
    /// Defines the <see cref="ScanItemCommandHandlerTests" />.
    /// </summary>
    public class ScanItemCommandHandlerTests
    {
        /// <summary>
        /// The Constructors Guards Against Null Args.
        /// </summary>
        [Fact]
        public void Constructor_GuardsAgainstNullArgs()
        {
            AssertConstructorsGuardAgainstNullArgs<ScanItemCommandHandler>();
        }

        /// <summary>
        /// Methods the guard against null arguments.
        /// </summary>
        [Fact]
        public void Methods_GuardAgainstNullArgs()
        {
            AssertMethodsGuardAgainstNullArgs<ScanItemCommandHandler>();
        }

        /// <summary>
        /// Writable the properties behave as expected.
        /// </summary>
        [Fact]
        public void WritableProperties_Behave()
        {
            AssertWritablePropertiesBehaveAsExpected<ScanItemCommandHandler>();
        }

        [Fact]
        public void ScanItemCommandHandler_Can_Handle_ScanItemCommand()
        {
            // Arrange
            var testFixture = new TestFixtureBuilder();
            var sut = testFixture.BuildSut();

            // Act & assert
            sut.Should().BeAssignableTo<IRequestHandler<ScanItemCommand, bool>>();
        }

        /// <summary>
        /// Defines the <see cref="TestFixture" />.
        /// </summary>
        private class TestFixtureBuilder
        {
            public Fixture Fixture;

            public Mock<Till> mockTill;

            /// <summary>
            /// Initializes a new instance of the <see cref="TestFixture"/> class.
            /// </summary>
            public TestFixtureBuilder()
            {
                this.Fixture = new Fixture();
                this.mockTill = this.Fixture.Freeze<Mock<Till>>();
            }

            /// <summary>
            /// The BuildSut.
            /// </summary>
            /// <returns>The <see cref="ScanItemCommandHandler"/>.</returns>
            public ScanItemCommandHandler BuildSut()
            {
                return new ScanItemCommandHandler(this.mockTill.Object);
            }
        }
    }
}
