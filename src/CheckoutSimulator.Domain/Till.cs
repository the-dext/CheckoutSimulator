// Checkout Simulator by Chris Dexter, file="Till.cs"

namespace CheckoutSimulator.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Ardalis.GuardClauses;
    using CheckoutSimulator.Domain.Exceptions;
    using CheckoutSimulator.Domain.Offers;
    using CheckoutSimulator.Domain.Repositories;
    using CheckoutSimulator.Domain.Scanning;

    /// <summary>
    /// Defines the <see cref="Till"/>.
    /// </summary>
    public class Till : ITill
    {
        private readonly List<ScannedItemMomento> scannedItems = new List<ScannedItemMomento>();
        private readonly IItemDiscount[] itemDiscounts;
        private readonly ISaleDiscount[] saleDiscounts;

        /// <summary>
        /// Initializes a new instance of the <see cref="Till"/> class.
        /// </summary>
        public Till(IStockKeepingUnit[] stockKeepingUnits, IDiscount[] discounts)
        {
            this.ObjectId = Guid.NewGuid();
            System.Diagnostics.Debug.WriteLine($"Till created: {this.ObjectId}");

            this.stockKeepingUnits = Guard.Against.Null(stockKeepingUnits, nameof(stockKeepingUnits));
            _ = Guard.Against.Null(discounts, nameof(discounts));

            // Sort the discounts into the two supported types so that it's quicker to loop through later.
            this.itemDiscounts = discounts.Where(x => x is IItemDiscount).Cast<IItemDiscount>().ToArray();
            this.saleDiscounts = discounts.Where(x => x is ISaleDiscount).Cast<ISaleDiscount>().ToArray();
        }

        /// <summary>
        /// Gets the object identifier. Useful for debugging to check object references are different/equal...
        /// </summary>
        public Guid ObjectId { get; }

        private IStockKeepingUnit[] stockKeepingUnits;

        /// <summary>
        /// The CompleteScanning.
        /// </summary>
        public void CompleteScanning()
        {
            this.scannedItems.Clear();
        }

        /// <summary>
        /// The ListScannedItems.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{IStockKeepingUnit}"/>.</returns>
        public IEnumerable<string> ListScannedItems()
        {
            return this.scannedItems.Select(x => x.Barcode);
        }

        /// <summary>
        /// The ScanItem.
        /// </summary>
        /// <param name="stockItem">The stockItem<see cref="IStockKeepingUnit"/>.</param>
        public IScanningResult ScanItem(string barcode)
        {
            Guard.Against.Null(barcode, nameof(barcode));

            var sku = this.stockKeepingUnits.FirstOrDefault(x => x.Barcode.Equals(barcode))
                ?? throw new UnknownItemException($"Unrecognised barcode: {barcode}");

            var momento = new ScannedItemMomento(sku.Barcode, sku.UnitPrice);
            this.ApplyItemDiscounts(momento);

            this.scannedItems.Add(momento);
            return new ScanningResult(true, momento.Message);
        }

        private ScannedItemMomento ApplyItemDiscounts(ScannedItemMomento momento)
        {
            foreach (var itemDiscount in this.itemDiscounts)
            {
                itemDiscount.ApplyDiscount(momento);
            }

            return momento;
        }

        /// <summary>
        /// The VoidItems.
        /// </summary>
        public void VoidItems()
        {
            this.scannedItems.Clear();
        }

        public double RequestTotalPrice()
        {
            return this.scannedItems.Sum(x => (x.UnitPrice - x.PriceAdjustment));
        }
    }
}
