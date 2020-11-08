// Checkout Simulator by Chris Dexter, file="IRunningTotalDiscount.cs"

namespace CheckoutSimulator.Domain.Offers
{
    using CheckoutSimulator.Domain.Scanning;

    /// <summary>
    /// Defines the <see cref="IItemDiscount"/>. This type of discount is applied as a till is
    /// scanning items.
    /// </summary>
    public interface IItemDiscount : IDiscount
    {
        /// <summary>
        /// Gets the Barcode.
        /// </summary>
        string Barcode { get; }

        /// <summary>
        /// The ApplyDiscount.
        /// </summary>
        /// <param name="scannedItem">The scannedItem <see cref="IScannedItem"/>.</param>
        /// <returns>The <see cref="IScannedItem"/>.</returns>
        void ApplyDiscount(IScannedItem scannedItem);
    }
}
