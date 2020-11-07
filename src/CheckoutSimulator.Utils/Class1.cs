using System;

namespace CheckoutSimulator.Utils
{
    public static class GuardExtensions
    {
        public static T CheckIsNotNull<T>(this T obj) where T: object
        {
            return obj ?? throw new NullA
        }
    }
}
