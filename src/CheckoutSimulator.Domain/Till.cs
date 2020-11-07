// Checkout Simulator by Chris Dexter, file="Till.cs"

namespace CheckoutSimulator.Domain
{
    using System;
    using System.Collections.Generic;
    using Ardalis.GuardClauses;

    /// <summary>
    /// Defines the <see cref="Till"/>.
    /// </summary>
    public class Till
    {
        private readonly List<string> scannedItems = new List<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Till"/> class.
        /// </summary>
        public Till()
        {
            this.ObjectId = Guid.NewGuid();
            System.Diagnostics.Debug.WriteLine($"Till created: {this.ObjectId}");
        }

        /// <summary>
        /// Gets the object identifier. Useful for debugging to check object references are different/equal...
        /// </summary>
        public Guid ObjectId { get; }

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
            return this.scannedItems;
        }

        /// <summary>
        /// The ScanItem.
        /// </summary>
        /// <param name="stockItem">The stockItem<see cref="IStockKeepingUnit"/>.</param>
        public void ScanItem(string barcode)
        {
            this.scannedItems.Add(Guard.Against.Null(barcode, nameof(barcode)));
        }

        /// <summary>
        /// The VoidItems.
        /// </summary>
        public void VoidItems()
        {
            this.scannedItems.Clear();
        }
    }
}
