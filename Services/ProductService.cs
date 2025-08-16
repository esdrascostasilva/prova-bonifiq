using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
	public class ProductService
	{
		TestDbContext _ctx;

		public ProductService(TestDbContext ctx)
		{
			_ctx = ctx;
		}

		public GenericList<Product> ListProducts(int page, int pageSize)
		{
			var query = _ctx.Products;
			var totalCount = query.Count();

			var products = query
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToList();

			var hasNext = page * pageSize < totalCount;

			return new GenericList<Product> { HasNext = hasNext, TotalCount = totalCount, Items = products };
		}

	}
}
