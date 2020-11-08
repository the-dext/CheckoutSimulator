// Checkout Simulator by Chris Dexter, file="SaleDiscount.cs"

namespace CheckoutSimulator.Domain.Offers
{
    using Ardalis.GuardClauses;

    /// <summary>
    /// Defines the <see cref="SaleDiscount" />.
    /// </summary>
    public class SaleDiscount : ISaleDiscount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaleDiscount"/> class.
        /// </summary>
        /// <param name="description">The description<see cref="string"/>.</param>
        public SaleDiscount(string description)
        {
            this.Description = Guard.Against.NullOrWhiteSpace(description, nameof(description));
        }

        /// <summary>
        /// Gets the Description.
        /// </summary>
        public string Description { get; }
    }
}
