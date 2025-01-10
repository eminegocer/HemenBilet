using HemenBiletProje.Strategy;

public interface ICardStrategy : IPaymentStrategy
{
    bool ValidateCardNumber(string cardNumber);
    bool ValidateExpiryDate(string month, string year);
    bool ValidateCVV(string cvv);
    int GetCardNumberLength();
    int GetCVVLength();
}