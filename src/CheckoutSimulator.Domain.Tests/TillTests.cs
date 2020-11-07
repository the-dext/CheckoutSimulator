// Checkout Simulator by Chris Dexter, file="TillTests.cs"

namespace CheckoutSimulator.Domain.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoFixture;
    using CheckoutSimulator.Domain;
    using CheckoutSimulator.Domain.Exceptions;
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
        /// The Can_Scan_Item.
        /// </summary>
        [Fact]
        public void Can_Scan_Item()
        {
            // Arrange
            var testFixture = new TestFixtureBuilder();
            var sut = testFixture
                .WithStockKeepingUnit("B15", 0.45, "Biscuits")
                .BuildSut();
            var expectedBarcode = "B15";

            // Act
            sut.ScanItem(expectedBarcode);

            // Assert
            sut.ListScannedItems().Should().Contain(expectedBarcode);
        }

        /// <summary>
        /// The Can_Scan_MultipleItems.
        /// </summary>
        [Fact]
        public void Can_Scan_MultipleItems()
        {
            // Arrange
            var testFixture = new TestFixtureBuilder();
            var sut = testFixture
                .WithStockKeepingUnit("B15", 0.45, "Biscuits")
                .WithStockKeepingUnit("A12", 0.30, "Apple")
                .BuildSut();
            var expectedBarcodes = new List<string> { "B15", "A12", "B15", "B15" };

            // Act
            expectedBarcodes.ForEach(sut.ScanItem);

            // Assert
            sut.ListScannedItems().Should().BeEquivalentTo(expectedBarcodes);
        }

        /// <summary>
        /// The Can_Total_Scanned_Items.
        /// </summary>
        [Fact]
        public void Can_Total_Scanned_Items()
        {
            // Arrange
            var testFixture = new TestFixtureBuilder();
            var sut = testFixture
                .WithStockKeepingUnit("B15", 0.45, "Biscuits")
                .BuildSut();

            // Act
            sut.ScanItem("B15");
            sut.ScanItem("B15");
            sut.ScanItem("B15");

            // Assert
            sut.RequestTotalPrice().Should().Be(0.45 * 3);
        }

        /// <summary>
        /// The CompleteScanning_StateUnderTest_ExpectedBehavior.
        /// </summary>
        [Fact]
        public void CompleteScanning_Resets_ScannedItems()
        {
            // Arrange
            var testFixture = new TestFixtureBuilder();
            var sut = testFixture
                .WithStockKeepingUnit("B15", 0.45, "Biscuits")
                .WithPreviouslyScannedItem("B15")
                .WithPreviouslyScannedItem("B15")
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
        /// The Scanning_Unknown_Item_Throws_Exception.
        /// </summary>
        [Fact]
        public void Scanning_Unknown_Item_Throws_Exception()
        {
            // Arrange
            var testFixture = new TestFixtureBuilder();
            var sut = testFixture
                .WithStockKeepingUnit("B15", 0.45, "Biscuits")
                .BuildSut();

            var unxpectedBarcode = "A12";

            Action act = () => sut.ScanItem(unxpectedBarcode);

            // Assert
            act.Should().Throw<UnknownItemException>().WithMessage("Unrecognised barcode: A12");
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
                .WithStockKeepingUnit("B15", 0.45, "Biscuits")
                .WithPreviouslyScannedItem("B15")
                .WithPreviouslyScannedItem("B15")
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

            public List<IStockKeepingUnit> StockKeepingUnits;

            private readonly List<Action<Till>> postBuildActions;

            /// <summary>
            /// Initializes a new instance of the <see cref="TestFixtureBuilder"/> class.
            /// </summary>
            public TestFixtureBuilder()
            {
                this.Fixture = new Fixture();
                this.postBuildActions = new List<Action<Till>>();
                this.StockKeepingUnits = new List<IStockKeepingUnit>();
            }

            /// <summary>
            /// The BuildSut.
            /// </summary>
            /// <returns>The <see cref="Till"/>.</returns>
            public Till BuildSut()
            {
                var ret = new Till(this.StockKeepingUnits.ToArray());

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
            /// <param name="barcode">The barcode<see cref="string"/>.</param>
            /// <returns>The <see cref="TestFixtureBuilder"/>.</returns>
            public TestFixtureBuilder WithPreviouslyScannedItem(string barcode)
            {
                this.postBuildActions.Add((Till x) =>
                {
                    x.ScanItem(barcode);
                });
                return this;
            }

            /// <summary>
            /// The WithStockKeepingUnit.
            /// </summary>
            /// <param name="barcode">The barcode<see cref="string"/>.</param>
            /// <param name="price">The price<see cref="double"/>.</param>
            /// <param name="description">The description<see cref="string"/>.</param>
            /// <returns>The <see cref="TestFixtureBuilder"/>.</returns>
            public TestFixtureBuilder WithStockKeepingUnit(string barcode, double price, string description)
            {
                var sku = Mock.Of<IStockKeepingUnit>(x => x.Barcode == barcode
                    && x.UnitPrice == price
                    && x.Description == description);

                this.StockKeepingUnits.Add(sku);

                return this;
            }
        }
    }
}
