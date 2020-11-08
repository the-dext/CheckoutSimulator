// Checkout Simulator by Chris Dexter, file="ItemDiscount.cs"

namespace CheckoutSimulator.Domain.Offers
{
    using Ardalis.GuardClauses;

    /// <summary>
    /// Defines the <see cref="BuyOneGetOneFree"/>.
    /// </summary>
    public class BuyOneGetOneFree : IItemDiscount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuyOneGetOneFree"/> class.
        /// </summary>
        /// <param name="description">The description<see cref="string"/>.</param>
        public BuyOneGetOneFree(string description, string barcode)
        {
            this.Description = Guard.Against.NullOrWhiteSpace(description, nameof(description));
            this.Barcode = Guard.Against.NullOrWhiteSpace(barcode, nameof(barcode));
        }

        /// <summary>
        /// Gets the Description.
        /// </summary>
        public string Description { get; }
        public string Barcode { get; }
    }
}
