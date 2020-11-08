// Checkout Simulator by Chris Dexter, file="Till.cs"

using System.Collections.Generic;

namespace CheckoutSimulator.Domain
{
    public interface ITill
    {
        void CompleteScanning();
        IEnumerable<string> ListScannedItems();
        double RequestTotalPrice();
        void ScanItem(string barcode);
        void VoidItems();
    }
}