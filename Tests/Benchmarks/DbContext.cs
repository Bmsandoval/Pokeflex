using System;
using System.Linq;
using System.Threading.Tasks;
using App.Data;
using App.Models;
using App.Services.Pokeflex;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TestSupport.EfHelpers;
using TestSupport.Helpers;

namespace Benchmarks
{
    public interface IDbContext
    {
        public PokeflexContext PokeflexContext { get; set; }

        private void BuildContext() {}
    }

    public class DbContextFactory
    {
        public DbContextFactory FactoryInstance { get; } = new();
        public static IDbContext DbContext { get; set; }
        private static string DbType { get; set; }

        private DbContextFactory() { }

        public static void InitDbContext(string dbType = "", Pokemon[] pokemons = null)
        {
            if (string.IsNullOrEmpty(dbType))
            {
                if (string.IsNullOrEmpty(DbType))
                {
                    DbType = "mock";
                } // else DbType stays what it is currently
            }
            else
            {
                DbType = dbType;
            }
            switch (DbType)
            {
                // case "sqlite":
                //     DbContext = new InMemoryDbContext();
                //     break;
                case "native":
                    DbContext = new NativeDbContext(pokemons);
                    break;
                case "mock":
                    DbContext = new MockDbContext(pokemons);
                    break;
                default:
                    throw new ArgumentException();
            }
        }
    }


    public class MockDbContext : IDbContext
    {
        public PokeflexContext PokeflexContext { get; set; }

        public void BuildContext(Pokemon[] pokemons)
        {
            var dummyOptions = new DbContextOptionsBuilder<PokeflexContext>().Options;
            var dbContextMock = new DbContextMock<PokeflexContext>(dummyOptions);
            dbContextMock.CreateDbSetMock(x => x.Pokemons, pokemons);
            PokeflexContext = dbContextMock.Object;
        }

        public MockDbContext(Pokemon[] pokemons)
        {
            BuildContext(pokemons);
        }
    }

    public class DbAccessContext : IDbContext
    {
        protected IConfigurationRoot Config { get; set; }
        public PokeflexContext PokeflexContext { get; set; }
        
        protected void SetConfiguration()
        {
            var callingProjectPath = TestData.GetCallingAssemblyTopLevelDir();
            Config = new ConfigurationBuilder()
                .SetBasePath(callingProjectPath)
                .AddJsonFile("appsettings.Local.json").Build();
        }
    }

    // public class InMemoryDbContext : DbAccessContext 
    // {
    //     public InMemoryDbContext()
    //     {
    //         var connectionString = Config.GetConnectionString("DefaultConnection");
    //         var builder = new DbContextOptionsBuilder<PokeflexContext>();
    //         builder.UseSqlServer(connectionString);
    //         using (var context = new PokeflexContext(builder.Options))
    //         {
    //             context.Database.EnsureCreated();
    //             // Run this for each new test
    //             context.Database.EnsureClean();
    //         }
    //     }
    // }
    
    public class NativeDbContext : DbAccessContext
    {
        private void BuildContext()
        {
            var connectionString = Config.GetConnectionString("DefaultConnection");
            var builder = new DbContextOptionsBuilder<PokeflexContext>();
            builder.UseSqlServer(connectionString);
            PokeflexContext = new PokeflexContext(builder.Options);
            PokeflexContext.Database.EnsureCreated();
            // Run this for each new test
            PokeflexContext.Database.EnsureClean();
        }
        
        public NativeDbContext(Pokemon[] pokemons)
        {
            SetConfiguration();
            BuildContext();
            PokeflexContext.Pokemons.BulkInsert(pokemons);
        }
    }
}