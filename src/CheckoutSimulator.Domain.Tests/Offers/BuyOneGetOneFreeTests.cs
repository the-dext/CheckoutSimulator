namespace CheckoutSimulator.Domain.Tests.Offers
{
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using AutoFixture.Idioms;
    using CheckoutSimulator.Domain.Offers;
    using CheckoutSimulator.Domain.Scanning;
    using FluentAssertions;
    using FluentAssertions.Execution;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Xunit;
    using static TestUtils.TestIdioms;

    public class BuyOneGetOneFreeTests
    {
        /// <summary>
        /// The Constructors Guards Against Null Args.
        /// </summary>
        [Fact]
        public void Constructor_GuardsAgainstNullArgs()
        {
            AssertConstructorsGuardAgainstNullArgs<BuyOneGetOneFree>();
        }


        /// <summary>
        /// Methods Guards Against Null Args.
        /// </summary>
        [Fact]
        public void Methods_GuardAgainstNullArgs()
        {
            AssertMethodsGuardAgainstNullArgs<BuyOneGetOneFree>();
        }

        [Fact]
        public void Writable_Properties_Behave()
        {
            AssertWritablePropertiesBehaveAsExpected<BuyOneGetOneFree>();
        }

        [Fact]
        public void Implements_IItemDiscount_And_IDiscount()
        {
            typeof(BuyOneGetOneFree).Should().BeAssignableTo<IItemDiscount>();
            typeof(BuyOneGetOneFree).Should().BeAssignableTo<IDiscount>();
        }

        [Fact]
        public void Should_Apply_Discount_When_Valid_TwoItems_Scanned()
        {
            // Arrange
            var testFixture = new TestFixtureBuilder();
            var sut = testFixture
                .WithPreviouslyScannedItem("B15")
                .BuildSut("My Test Discount", "B15");
            var itemBeingScanned = Mock.Of<IScannedItem>(x => x.Barcode == "B15");

            // Act
            sut.ApplyDiscount(itemBeingScanned, testFixture.PreviouslyScannedItems.ToArray());

            // Assert
            Mock.Get(itemBeingScanned).Verify(x => x.ApplyDiscount("My Test Discount", -itemBeingScanned.UnitPrice), Times.Once);
        }

        [Fact]
        public void Should_Not_Apply_Discount_When_One_Item_Scanned()
        {
            // Arrange
            var testFixture = new TestFixtureBuilder();
            var sut = testFixture
                .BuildSut("My Test Discount", "B15");
            var itemBeingScanned = Mock.Of<IScannedItem>(x => x.Barcode == "B15");

            // Act
            sut.ApplyDiscount(itemBeingScanned, Array.Empty<IScannedItem>());

            // Assert
            Mock.Get(itemBeingScanned).Verify(x => x.ApplyDiscount("My Test Discount", -itemBeingScanned.UnitPrice), Times.Never);
        }

        [Fact]
        public void Should_Apply_Discount_Once_If_Three_Items_Scanned()
        {
            // Arrange
            const string Barcode = "B15";
            var testFixture = new TestFixtureBuilder();
            var sut = testFixture
                .WithPreviouslyScannedItem(Barcode)
                .BuildSut("My Test Discount", Barcode);

            var firstItemBeingScanned = new ScannedItemMomento(Barcode, 0.45);
            var secondItemBeingScanned = new ScannedItemMomento(Barcode, 0.45);

            // Act
            sut.ApplyDiscount(firstItemBeingScanned, testFixture.PreviouslyScannedItems.ToArray());
            sut.ApplyDiscount(secondItemBeingScanned, testFixture.PreviouslyScannedItems.ToArray());

            // Assert
            testFixture.PreviouslyScannedItems.First().IsIncludedInADiscountOffer.Should().BeTrue();
            testFixture.PreviouslyScannedItems.First().IsDiscounted.Should().BeFalse();

            firstItemBeingScanned.IsDiscounted.Should().BeTrue();
            firstItemBeingScanned.IsIncludedInADiscountOffer.Should().BeTrue();

            secondItemBeingScanned.IsIncludedInADiscountOffer.Should().BeFalse();
            secondItemBeingScanned.IsIncludedInADiscountOffer.Should().BeFalse();
        }

        [Fact]
        public void Should_Not_Apply_Discount_If_Two_Items_Differ()
        {
            // Arrange
            const string Barcode = "B15";
            var testFixture = new TestFixtureBuilder();
            var sut = testFixture
                .WithPreviouslyScannedItem(Barcode)
                .BuildSut("My Test Discount", Barcode);

            var firstItemBeingScanned = new ScannedItemMomento("A99", 0.45);

            // Act
            sut.ApplyDiscount(firstItemBeingScanned, testFixture.PreviouslyScannedItems.ToArray());

            // Assert
            testFixture.PreviouslyScannedItems.First().IsIncludedInADiscountOffer.Should().BeFalse();
            testFixture.PreviouslyScannedItems.First().IsDiscounted.Should().BeFalse();

            firstItemBeingScanned.IsDiscounted.Should().BeFalse();
            firstItemBeingScanned.IsIncludedInADiscountOffer.Should().BeFalse();
        }

                [Fact]
        public void Should_Not_Apply_Discount_Second_Item_Is_Not_Consecutively_Scanned()
        {
            // Arrange
            const string DiscountedBarcode = "B15";
            var testFixture = new TestFixtureBuilder();
            var sut = testFixture
                .WithPreviouslyScannedItem(DiscountedBarcode)
                .BuildSut("My Test Discount", DiscountedBarcode);

            var firstItemBeingScanned = new ScannedItemMomento("A99", 0.45);
            var secondItemBeingScanned = new ScannedItemMomento(DiscountedBarcode, 0.45);

            // Act
            sut.ApplyDiscount(firstItemBeingScanned, testFixture.PreviouslyScannedItems.ToArray());
            sut.ApplyDiscount(secondItemBeingScanned, testFixture.PreviouslyScannedItems.ToArray());

            // Assert
            testFixture.PreviouslyScannedItems.First().IsIncludedInADiscountOffer.Should().BeTrue();
            testFixture.PreviouslyScannedItems.First().IsDiscounted.Should().BeFalse();

            firstItemBeingScanned.IsDiscounted.Should().BeFalse();
            firstItemBeingScanned.IsIncludedInADiscountOffer.Should().BeFalse();

            secondItemBeingScanned.IsIncludedInADiscountOffer.Should().BeTrue();
            secondItemBeingScanned.IsIncludedInADiscountOffer.Should().BeTrue();
        }

        private class TestFixtureBuilder
        {
            public Fixture Fixture;

            public List<IScannedItem> PreviouslyScannedItems { get; }

            public TestFixtureBuilder()
            {
                this.Fixture = new Fixture();
                this.PreviouslyScannedItems = new List<IScannedItem>();
            }

            public BuyOneGetOneFree BuildSut(string message, string barcode)
            {
                return new BuyOneGetOneFree(message, barcode);
            }

            public TestFixtureBuilder WithPreviouslyScannedItem(string barcode)
            {
                // Real object, not a mock. Because we are testing the domain layer so testing using state instead of behaviour is more helpful.
                this.PreviouslyScannedItems.Add(new ScannedItemMomento(barcode, .45));
                return this;
            }
        }
    }
}
