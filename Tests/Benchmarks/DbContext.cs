using System;
using System.Linq;
using App.Data;
using App.Models;
using EntityFrameworkCoreMock;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestSupport.EfHelpers;
using TestSupport.Helpers;

namespace Benchmarks
{
    public interface IDbContext
    {
        public PokeflexContext PokeflexContext { get; set; }
    }

    public class DbContextFactory
    {
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
                case "inmemory":
                    DbContext = new InMemoryDbContext(pokemons);
                    break;
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
        private DbContextOptions<PokeflexContext> ContextOptions { get; set; }

        private void BuildOptions() { ContextOptions = new DbContextOptionsBuilder<PokeflexContext>().Options; }

        public MockDbContext(Pokemon[] pokemons)
        {
            BuildOptions();
            var dbContextMock = new DbContextMock<PokeflexContext>(ContextOptions);
            dbContextMock.CreateDbSetMock(x => x.Pokemons, pokemons);
            PokeflexContext = dbContextMock.Object;
        }
    }

    public class InMemoryDbContext : IDbContext 
    {
        public PokeflexContext PokeflexContext { get; set; }
        private string ConnectionString { get; set; }
        private DbContextOptions<PokeflexContext> ContextOptions { get; set; }

        private void SetConnectionString()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            ConnectionString = connectionStringBuilder.ToString();
        }

        private void BuildOptions()
        {
            var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            var builder = new DbContextOptionsBuilder<PokeflexContext>();
            builder.UseSqlite(connection);
            ContextOptions = builder.Options;
        }
        public InMemoryDbContext(Pokemon[] pokemons)
        {
            SetConnectionString();
            BuildOptions();
            PokeflexContext = new PokeflexContext(ContextOptions);
            PokeflexContext.CreateEmptyViaDelete();
            PokeflexContext.Pokemons.BulkInsert(pokemons);
        }
    }
    
    public class NativeDbContext : IDbContext 
    {
        public PokeflexContext PokeflexContext { get; set; }
        private string ConnectionString { get; set; }
        private DbContextOptions<PokeflexContext> ContextOptions { get; set; }
        private IConfigurationRoot Config { get; set; }
        
        private void SetConnectionString()
        {
            var callingProjectPath = TestData.GetCallingAssemblyTopLevelDir();
            Config = new ConfigurationBuilder()
                .SetBasePath(callingProjectPath)
                .AddJsonFile("appsettings.Local.json").Build();
            ConnectionString = Config.GetConnectionString("DefaultConnection");
        }

        private void BuildOptions()
        {
            var builder = new DbContextOptionsBuilder<PokeflexContext>();
            builder.UseSqlServer(ConnectionString);
            ContextOptions = builder.Options;

        }
        
        public NativeDbContext(Pokemon[] pokemons)
        {
            SetConnectionString();
            BuildOptions();
            PokeflexContext = new PokeflexContext(ContextOptions);
            PokeflexContext.CreateEmptyViaDelete();
            PokeflexContext.Pokemons.BulkInsert(pokemons);
        }
    }
}