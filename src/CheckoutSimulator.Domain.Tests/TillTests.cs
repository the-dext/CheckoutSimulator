// Checkout Simulator by Chris Dexter, file="TillTests.cs"

namespace CheckoutSimulator.Domain.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoFixture;
    using CheckoutSimulator.Domain;
    using FluentAssertions;
    using Moq;
    using Xunit;
    using static TestUtils.TestIdioms;

    /// <summary>
    /// Defines the <see cref="TillTests" />.
    /// </summary>
    public class TillTests
    {
        /// <summary>
        /// The CompleteScanning_StateUnderTest_ExpectedBehavior.
        /// </summary>
        [Fact]
        public void CompleteScanning_Resets_ScannedItems()
        {
            // Arrange
            var testFixture = new TestFixtureBuilder();
            var sut = testFixture
                .WithRandomPreviouslyScannedItem()
                .WithRandomPreviouslyScannedItem()
                .BuildSut();
            var originalItemCount = sut.ListScannedItems().Count();

            // Act
            sut.CompleteScanning();

            // Assert
            originalItemCount.Should().Be(2);
            sut.ListScannedItems().Count().Should().Be(0);
        }

        /// <summary>
        /// The Constructors Guards Against Null Args.
        /// </summary>
        [Fact]
        public void Constructor_GuardsAgainstNullArgs()
        {
            AssertConstructorsGuardAgainstNullArgs<Till>();
        }

        /// <summary>
        /// Methods Guards Against Null Args.
        /// </summary>
        [Fact]
        public void Methods_GuardAgainstNullArgs()
        {
            AssertMethodsGuardAgainstNullArgs<Till>();
        }

        /// <summary>
        /// The VoidItems_Resets_ScannedItems.
        /// </summary>
        [Fact]
        public void VoidItems_Resets_ScannedItems()
        {
            // Arrange
            var testFixture = new TestFixtureBuilder();
            var sut = testFixture
                .WithRandomPreviouslyScannedItem()
                .WithRandomPreviouslyScannedItem()
                .BuildSut();
            var originalItemCount = sut.ListScannedItems().Count();

            // Act
            sut.VoidItems();

            // Assert
            originalItemCount.Should().Be(2);
            sut.ListScannedItems().Count().Should().Be(0);
        }

        /// <summary>
        /// Defines the <see cref="TestFixtureBuilder" />.
        /// </summary>
        private class TestFixtureBuilder
        {
            public Fixture Fixture;

            private readonly List<Action<Till>> postBuildActions;

            /// <summary>
            /// Initializes a new instance of the <see cref="TestFixtureBuilder"/> class.
            /// </summary>
            public TestFixtureBuilder()
            {
                this.Fixture = new Fixture();
                this.postBuildActions = new List<Action<Till>>();
            }

            /// <summary>
            /// The BuildSut.
            /// </summary>
            /// <returns>The <see cref="Till"/>.</returns>
            public Till BuildSut()
            {
                var ret = new Till();

                // apply post creation actions to set up test fixture state.
                foreach (var action in this.postBuildActions)
                {
                    action(ret);
                }

                return ret;
            }
            /// <summary>
            /// The WithExistingScannedItem.
            /// </summary>
            /// <returns>The <see cref="TestFixtureBuilder"/>.</returns>
            public TestFixtureBuilder WithRandomPreviouslyScannedItem()
            {
                this.postBuildActions.Add((Till x) =>
                {
                    x.ScanItem(this.Fixture.Create<string>());
                });
                return this;
            }
        }
    }
}
