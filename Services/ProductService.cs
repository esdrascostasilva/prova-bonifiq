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

		public ProductList  ListProducts(int page, int pageSize)
		{
			var query = _ctx.Products;
			var totalCount = query.Count();

			var products = query
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToList();

			var hasNext = page * pageSize < totalCount;

			return new ProductList() { HasNext = hasNext, TotalCount = totalCount, Products = products };
		}

	}
}
