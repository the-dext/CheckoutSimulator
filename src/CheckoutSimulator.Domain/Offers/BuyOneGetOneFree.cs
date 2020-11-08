// Checkout Simulator by Chris Dexter, file="ItemDiscount.cs"

namespace CheckoutSimulator.Domain.Offers
{
    using Ardalis.GuardClauses;
    using CheckoutSimulator.Domain.Scanning;

    /// <summary>
    /// Defines the <see cref="BuyOneGetOneFree"/>.
    /// </summary>
    public class BuyOneGetOneFree : IItemDiscount
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuyOneGetOneFree"/> class.
        /// </summary>
        /// <param name="description">The description <see cref="string"/>.</param>
        /// <param name="barcode">The barcode <see cref="string"/>.</param>
        public BuyOneGetOneFree(string description, string barcode)
        {
            this.Description = Guard.Against.NullOrWhiteSpace(description, nameof(description));
            this.Barcode = Guard.Against.NullOrWhiteSpace(barcode, nameof(barcode));
        }

        /// <summary>
        /// Gets the Barcode.
        /// </summary>
        public string Barcode { get; }

        /// <summary>
        /// Gets the Description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The ApplyDiscount.
        /// </summary>
        /// <param name="scannedItem">The scannedItem <see cref="IScannedItem"/>.</param>
        public void ApplyDiscount(IScannedItem scannedItem)
        {
            _ = Guard.Against.Null(scannedItem, nameof(scannedItem));

            if (scannedItem.Barcode == this.Barcode)
            {
                scannedItem.ApplyDiscount(this.Description, -scannedItem.UnitPrice);
            }
        }
    }
}
