namespace CheckoutSimulator.Domain.Tests.Offers
{
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using AutoFixture.Idioms;
    using CheckoutSimulator.Domain.Offers;
    using FluentAssertions;
    using FluentAssertions.Execution;
    using Moq;
    using System;
    using System.Threading;
    using Xunit;
    using static TestUtils.TestIdioms;

    public class ItemDiscountTests
    {
        /// <summary>
        /// The Constructors Guards Against Null Args.
        /// </summary>
        [Fact]
        public void Constructor_GuardsAgainstNullArgs()
        {
            AssertConstructorsGuardAgainstNullArgs<ItemDiscount>();
        }


        /// <summary>
        /// Methods Guards Against Null Args.
        /// </summary>
        [Fact]
        public void Methods_GuardAgainstNullArgs()
        {
            AssertMethodsGuardAgainstNullArgs<ItemDiscount>();
        }


        [Fact]
        public void Writable_Properties_Behave()
        {
            AssertWritablePropertiesBehaveAsExpected<ItemDiscount>();
        }

        [Fact]
        public void Implements_IItemDiscount_And_IDiscount()
        {
            typeof(ItemDiscount).Should().BeAssignableTo<IItemDiscount>();
            typeof(ItemDiscount).Should().BeAssignableTo<IDiscount>();
        }

        private class TestFixtureBuilder
        {
            public Fixture Fixture;


            public TestFixtureBuilder()
            {
                this.Fixture = new Fixture();

            }

            public ItemDiscount BuildSut()
            {
                return new ItemDiscount(this.Fixture.Create<string>());
            }
        }
    }
}
