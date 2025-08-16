using System;

namespace ProvaPub.Services.Payments;

public class PixPayment : IPaymentMethod
{
    public string Name => "pix";

    public async Task PayAsync(decimal value, int customerId)
    {
        // pagamento via Pix
        Console.WriteLine($"Pagamento Pix no valor de {value} realizado para o cliente {customerId}");
    }
}