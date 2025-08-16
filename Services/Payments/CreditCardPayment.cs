using System;

namespace ProvaPub.Services.Payments;

public class CreditCardPayment : IPaymentMethod
{
    public string Name => "creditcard";

    public async Task PayAsync(decimal value, int customerId)
    {
        // pagamento via Cartao de Credito
        Console.WriteLine($"Pagamento Cartao de Credito no valor de {value} realizado para o cliente {customerId}");
    }
}
