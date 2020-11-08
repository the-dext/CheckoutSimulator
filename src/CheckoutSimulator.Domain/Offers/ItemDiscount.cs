// Checkout Simulator by Chris Dexter, file="ItemDiscount.cs"

namespace CheckoutSimulator.Domain.Offers
{
    using Ardalis.GuardClauses;

    /// <summary>
    /// Defines the <see cref="ItemDiscount"/>.
    /// </summary>
    public class ItemDiscount : IItemDiscount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemDiscount"/> class.
        /// </summary>
        /// <param name="description">The description<see cref="string"/>.</param>
        public ItemDiscount(string description)
        {
            this.Description = Guard.Against.NullOrWhiteSpace(description, nameof(description));
        }

        /// <summary>
        /// Gets the Description.
        /// </summary>
        public string Description { get; }
    }
}
