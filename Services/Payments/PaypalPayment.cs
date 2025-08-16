using System;

namespace ProvaPub.Services.Payments;

public class PaypalPayment : IPaymentMethod
{
    public string Name => "paypal";

    public async Task PayAsync(decimal value, int customerId)
    {
        // pagamento via Paypal
        Console.WriteLine($"Pagamento Paypal no valor de {value} realizado para o cliente {customerId}");
    }
}
