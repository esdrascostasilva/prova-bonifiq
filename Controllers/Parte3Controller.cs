using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;
using ProvaPub.Services.Payments;

namespace ProvaPub.Controllers
{

    /// <summary>
    /// Esse teste simula um pagamento de uma compra.
    /// O método PayOrder aceita diversas formas de pagamento. Dentro desse método é feita uma estrutura de diversos "if" para cada um deles.
    /// Sabemos, no entanto, que esse formato não é adequado, em especial para futuras inclusões de formas de pagamento.
    /// Como você reestruturaria o método PayOrder para que ele ficasse mais aderente com as boas práticas de arquitetura de sistemas?
    /// 
    /// Outra parte importante é em relação à data (OrderDate) do objeto Order. Ela deve ser salva no banco como UTC mas deve retornar para o cliente no fuso horário do Brasil. 
    /// Demonstre como você faria isso.
    /// </summary>
    [ApiController]
	[Route("[controller]")]
	public class Parte3Controller :  ControllerBase
	{
		[HttpGet("orders")]
		public async Task<Order> PlaceOrder(string paymentMethod, decimal paymentValue, int customerId)
		{
            var contextOptions = new DbContextOptionsBuilder<TestDbContext>()
    .UseSqlServer(@"Server=localhost; Database=TesteBonifiq; User Id=sa; Password=myPassw0rd; TrustServerCertificate=True")
    .Options;

            using var context = new TestDbContext(contextOptions);

            // TODO: melhorar isso
            var paymentMethods = new List<IPaymentMethod>
            {
                new PixPayment(),
                new CreditCardPayment(),
                new PaypalPayment()
            };

            var service = new OrderService(context, paymentMethods);
            var order = await service.PayOrder(paymentMethod, paymentValue, customerId);
            
            // var timeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"); // OS Windows
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo"); // MacOS
            order.OrderDate = TimeZoneInfo.ConvertTimeFromUtc(order.OrderDate, timeZone);

            return order;
		}
	}
}
