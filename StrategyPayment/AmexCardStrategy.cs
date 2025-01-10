using HemenBiletProje.Strategy;
using System;

public class AmexCardStrategy : IPaymentStrategy
{
    private readonly string _cardNumber;
    private readonly string _cardHolder;
    private readonly int _expirationMonth;
    private readonly int _expirationYear;
    private readonly string _cvv;

    public AmexCardStrategy(string cardNumber, string cardHolder, int expirationMonth, int expirationYear, string cvv)
    {
        _cardNumber = cardNumber;
        _cardHolder = cardHolder;
        _expirationMonth = expirationMonth;
        _expirationYear = expirationYear;
        _cvv = cvv;
    }

    public bool ProcessPayment(decimal amount)
    {
        Console.WriteLine("Processing Amex payment...");
        // Ek doğrulama
        if (_cardNumber.StartsWith("34") || _cardNumber.StartsWith("37"))
        {
            Console.WriteLine("Amex card validated.");
            return true;
        }
        Console.WriteLine("Invalid Amex card.");
        return false;
    }
}