﻿// Checkout Simulator by Chris Dexter, file="TestIdioms.cs"
/// <summary>
/// This is a small set of utilities to make working with AutoFixture and the AutoFixture idioms a
/// little easier
/// </summary>

namespace TestUtils
{
    using AutoFixture;
    using AutoFixture.AutoMoq;
    using AutoFixture.Idioms;

    /// <summary>
    /// Defines the <see cref="TestIdioms"/>.
    /// </summary>
    public static class TestIdioms
    {
        /// <summary>
        /// Asserts that constructors guard against null arguments.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        public static void AssertConstructorsGuardAgainstNullArgs<T>()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var assertion = new GuardClauseAssertion(fixture);
            var constructors = typeof(T).GetConstructors();
            assertion.Verify(constructors);
        }

        /// <summary>
        /// Asserts that methods guard against null arguments.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        public static void AssertMethodsGuardAgainstNullArgs<T>()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var assertion = new GuardClauseAssertion(fixture);
            var constructors = typeof(T).GetMethods();
            assertion.Verify(constructors);
        }

        /// <summary>
        /// Asserts that when writable properties are given a value, that value is the returned by
        /// the property getter.
        /// </summary>
        /// <typeparam name="T">.</typeparam>
        public static void AssertWritablePropertiesBehaveAsExpected<T>()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var assertion = new WritablePropertyAssertion(fixture);
            var constructors = typeof(T).GetMethods();
            assertion.Verify(constructors);
        }
    }
}
