using System;

namespace HemenBiletProje.Strategy
{
    public interface IPaymentStrategy
    {
        bool ProcessPayment(decimal amount);
    }
}