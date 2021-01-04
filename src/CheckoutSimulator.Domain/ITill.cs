// Checkout Simulator by Chris Dexter, file="Till.cs"

using System.Collections.Generic;
using CheckoutSimulator.Domain.Scanning;

namespace CheckoutSimulator.Domain
{
    public interface ITill
    {
        void CompleteScanning();
        IEnumerable<string> ListScannedItems();
        double RequestTotalPrice();
        IScanningResult ScanItem(string barcode);
        void VoidItems();
    }
}