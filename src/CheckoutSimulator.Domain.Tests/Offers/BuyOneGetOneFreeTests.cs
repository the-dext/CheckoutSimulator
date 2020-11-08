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

        private class TestFixtureBuilder
        {
            public Fixture Fixture;


            public TestFixtureBuilder()
            {
                this.Fixture = new Fixture();

            }

            public BuyOneGetOneFree BuildSut()
            {
                return new BuyOneGetOneFree(this.Fixture.Create<string>(), "B15");
            }
        }
    }
}
