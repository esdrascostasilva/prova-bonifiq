using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services.Payments;

namespace ProvaPub.Services
{
	public class OrderService
	{
        TestDbContext _ctx;
		private readonly IEnumerable<IPaymentMethod> _paymentMethods;

		public OrderService(TestDbContext ctx, IEnumerable<IPaymentMethod> paymentMethods)
		{
			_ctx = ctx;
			_paymentMethods = paymentMethods;
		}

        public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
		{
			var method = _paymentMethods.FirstOrDefault(p => p.Name.Equals(paymentMethod, StringComparison.OrdinalIgnoreCase));

			if (method == null)
				throw new InvalidOperationException($"O meetodo de pagamento {paymentMethod} infelizmente ainda nao eh suportado pela nossa plataforma");

			await method.PayAsync(paymentValue, customerId);

			return await InsertOrder(new Order() //Retorna o pedido para o controller
			{
				Value = paymentValue,
				CustomerId = customerId,
				OrderDate = DateTime.UtcNow
			});
		}

		public async Task<Order> InsertOrder(Order order)
        {
			//Insere pedido no banco de dados
			return (await _ctx.Orders.AddAsync(order)).Entity;
        }
	}
}
