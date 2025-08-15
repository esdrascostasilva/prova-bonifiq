using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
	public class RandomService
	{
		int seed;
        TestDbContext _ctx;
		public RandomService()
        {
            var contextOptions = new DbContextOptionsBuilder<TestDbContext>()
                .UseSqlServer(@"Server=localhost; Database=TesteBonifiq; User Id=sa; Password=myPassw0rd; TrustServerCertificate=True;")
                // .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Teste;Trusted_Connection=True;")
                .Options;
            seed = Guid.NewGuid().GetHashCode();

            _ctx = new TestDbContext(contextOptions);
        }
        public async Task<int> GetRandom()
		{
            var number =  new Random(seed).Next(100);

            // preciso tratar o numero gerado para que, caso eu ja tenha no meu banco ele avise o usuario e peça pra gerar outro
            bool exists = await _ctx.Numbers.AnyAsync(n => n.Number == number);

            if (exists)
            {
                throw new InvalidOperationException($"O numero gerado ja existe. Por favor, gere outro");
            }
            
            _ctx.Numbers.Add(new RandomNumber() { Number = number });
            _ctx.SaveChanges();
			return number;
		}

	}
}
