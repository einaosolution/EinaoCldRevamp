using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace IPORevamp.Data
{
    public class IPOContextFactory : IDesignTimeDbContextFactory<IPOContext>
    {
        public IPOContext CreateDbContext(string[] args)
        {

            var optionsBuilder = new DbContextOptionsBuilder<IPOContext>();


            optionsBuilder.UseSqlServer("Server=5.77.54.44; Database=IPORevampDB;User ID=TestUser;Password=Password12345;MultipleActiveResultSets=true;");
            return new IPOContext(optionsBuilder.Options);
        }

    }
}