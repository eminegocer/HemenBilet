using HemenBiletProje.Strategy;
using System;

public class MasterCardStrategy : IPaymentStrategy
{
    private readonly string _cardNumber;
    private readonly string _cardHolder;
    private readonly int _expirationMonth;
    private readonly int _expirationYear;
    private readonly string _cvv;

    public MasterCardStrategy(string cardNumber, string cardHolder, int expirationMonth, int expirationYear, string cvv)
    {
        _cardNumber = cardNumber;
        _cardHolder = cardHolder;
        _expirationMonth = expirationMonth;
        _expirationYear = expirationYear;
        _cvv = cvv;
    }

    public bool ProcessPayment(decimal amount)
    {
        Console.WriteLine("Processing Mastercard payment...");
        // Cashback hesapla
        decimal cashback = amount * 0.05m; // %5 Cashback
        Console.WriteLine($"Cashback earned: {cashback}");
        return true;
    }
}