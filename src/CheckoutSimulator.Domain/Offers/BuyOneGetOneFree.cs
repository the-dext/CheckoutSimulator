// Checkout Simulator by Chris Dexter, file="ItemDiscount.cs"

namespace CheckoutSimulator.Domain.Offers
{
    using System.Linq;
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
        /// <param name="itemBeingScanned">The scannedItem <see cref="IScannedItem"/>.</param>
        /// <param name="previouslyScannedItems">The previouslyScannedItems<see cref="IScannedItem[]"/>.</param>
        public void ApplyDiscount(IScannedItem itemBeingScanned, IScannedItem[] previouslyScannedItems)
        {
            _ = Guard.Against.Null(itemBeingScanned, nameof(itemBeingScanned));
            _ = Guard.Against.Null(previouslyScannedItems, nameof(previouslyScannedItems));

            if (itemBeingScanned.Barcode == this.Barcode)
            {
                // if there is one other item, that hasn't been included in a discount already then this discount can apply
                var previousApplicableItem = previouslyScannedItems.FirstOrDefault(x =>
                    x.Barcode == itemBeingScanned.Barcode &&
                    x.IsIncludedInADiscountOffer == false);

                if (previousApplicableItem != null)
                {
                    previousApplicableItem.SetIncludedInDiscountOffer(true);
                    itemBeingScanned.ApplyDiscount(this.Description, -itemBeingScanned.UnitPrice);
                }
            }
        }
    }
}
