// Checkout Simulator by Chris Dexter, file="ScanItemCommandHandlerTests.cs"

namespace CheckoutSimulator.Application.Tests.CommandHandlers
{
    using AutoFixture;
    using CheckoutSimulator.Application.CommandHandlers;
    using CheckoutSimulator.Application.Commands;
    using CheckoutSimulator.Domain;
    using CheckoutSimulator.Domain.Scanning;
    using FluentAssertions;
    using MediatR;
    using Moq;
    using Xunit;
    using static TestUtils.TestIdioms;

    /// <summary>
    /// Defines the <see cref="ScanItemCommandHandlerTests"/>.
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
        /// The ScanItemCommandHandler_Can_Handle_ScanItemCommand.
        /// </summary>
        [Fact]
        public void ScanItemCommandHandler_Can_Handle_ScanItemCommand()
        {
            typeof(ScanItemCommandHandler).Should().BeAssignableTo<IRequestHandler<ScanItemCommand, IScanningResult>>();
        }

        /// <summary>
        /// Writable the properties behave as expected.
        /// </summary>
        [Fact]
        public void WritableProperties_Behave()
        {
            AssertWritablePropertiesBehaveAsExpected<ScanItemCommandHandler>();
        }

        /// <summary>
        /// Defines the <see cref="TestFixture"/>.
        /// </summary>
        private class TestFixtureBuilder
        {
            public Fixture Fixture;

            public Mock<ITill> mockTill;

            /// <summary>
            /// Initializes a new instance of the <see cref="TestFixtureBuilder"/> class.
            /// </summary>
            public TestFixtureBuilder()
            {
                this.Fixture = new Fixture();
                this.mockTill = this.Fixture.Freeze<Mock<ITill>>();
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
