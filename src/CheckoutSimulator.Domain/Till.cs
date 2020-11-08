﻿// Checkout Simulator by Chris Dexter, file="Till.cs"

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

    /// <summary>
    /// Defines the <see cref="Till"/>.
    /// </summary>
    public class Till : ITill
    {
        private readonly List<string> scannedItems = new List<string>();
        private readonly IDiscount[] discounts;

        /// <summary>
        /// Initializes a new instance of the <see cref="Till"/> class.
        /// </summary>
        public Till(IStockKeepingUnit[] stockKeepingUnits, IDiscount[] discounts)
        {
            this.ObjectId = Guid.NewGuid();
            System.Diagnostics.Debug.WriteLine($"Till created: {this.ObjectId}");

            this.stockKeepingUnits = Guard.Against.Null(stockKeepingUnits, nameof(stockKeepingUnits));
            this.discounts = Guard.Against.Null(discounts, nameof(discounts));
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
            return this.scannedItems;
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

            this.scannedItems.Add(barcode);
            return new ScanningResult(true, null);
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
            var totalPriceBeforeDiscounts = 0d;
            foreach (var scannedItem in this.scannedItems)
            {
                var sku = this.stockKeepingUnits.First(x => x.Barcode == scannedItem);

                totalPriceBeforeDiscounts += sku.UnitPrice;
            }

            return totalPriceBeforeDiscounts;
        }
    }
}
