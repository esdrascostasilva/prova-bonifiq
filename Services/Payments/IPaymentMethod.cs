using System;

namespace ProvaPub.Services.Payments;

public interface IPaymentMethod
{
    // Respeitando o principio Open/Closed do SOLID, aberto para extensao e fechado pra modificacao, foi implementando uma interface de pagamento, onde os metodos de pagamento
    // implementam esse contrato. Quando houver novos metodos de pagamento (boleto por exmeplo), criamos o metodo e estendemos dessa interface evitando if's na classe service
    string Name { get; }
    Task PayAsync(decimal value, int customerId);
}
