using HemenBiletProje.Strategy;
using System;

public class VisaCardStrategy : IPaymentStrategy
{
    private readonly string _cardNumber;
    private readonly string _cardHolder;
    private readonly int _expirationMonth;
    private readonly int _expirationYear;
    private readonly string _cvv;

    public VisaCardStrategy(string cardNumber, string cardHolder, int expirationMonth, int expirationYear, string cvv)
    {
        _cardNumber = cardNumber;
        _cardHolder = cardHolder;
        _expirationMonth = expirationMonth;
        _expirationYear = expirationYear;
        _cvv = cvv;
    }

    public bool ProcessPayment(decimal amount)
    {
        // Simulated payment processing
        Console.WriteLine("Processing Visa payment...");
        return true; // Assume payment is successful
    }
}