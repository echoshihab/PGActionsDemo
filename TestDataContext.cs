using Microsoft.EntityFrameworkCore;
using SelfServeDemo;

namespace ActionsPg
{
    public class TestDataContext : DbContext
    {
        public TestDataContext(DbContextOptions<TestDataContext> options) : base(options)
        {

        }
        public DbSet<TestData> TestData { get;set;}

    }
}
