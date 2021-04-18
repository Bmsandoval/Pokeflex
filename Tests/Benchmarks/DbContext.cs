using System;
using System.Linq;
using App.Data;
using App.Models;
using App.Services.Pokeflex;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestSupport.EfHelpers;
using TestSupport.Helpers;

namespace Benchmarks
{
    public class DbContext
    {
        public static DbContext Instance { get; } = new ();
        public PokeflexContext Context { get; set; }
        public Func<Pokemon[],int> BulkInsert { get; set; }
        private IConfigurationRoot Config { get; set; }
        public string DbType { get; set; }
        private DbContext() { }

        public void InitDbContext(string dbType="")
        {
            DbType = dbType;
            switch (dbType)
            {
                case "sqlite":
                case "docker":
                    Config = NewConfiguration();
                    throw new NotImplementedException();
                default:
                    MockDb();
                    break;
            }
        }
        
        public interface IDbContext
        {
            
            
        }

        private static IConfigurationRoot NewConfiguration()
        {
            var callingProjectPath = TestData.GetCallingAssemblyTopLevelDir();
            var builder = new ConfigurationBuilder()
                .SetBasePath(callingProjectPath)
                .AddJsonFile("appsettings.Local.json");
            return builder.Build();
        }
        
        private void MockDb()
        {
            var dummyOptions = new DbContextOptionsBuilder<PokeflexContext>().Options;
            var dbContextMock = new DbContextMock<PokeflexContext>(dummyOptions);
            dbContextMock.CreateDbSetMock(x => x.Pokemons, Array.Empty<Pokemon>());
            Context = dbContextMock.Object;
        }

        private void DockerDb()
        {
            var connectionString = Config.GetConnectionString("DefaultConnection");
            var builder = new DbContextOptionsBuilder<PokeflexContext>();
            builder.UseSqlServer(connectionString);
            using (var context = new PokeflexContext(builder.Options))
            {
                context.Database.EnsureCreated();
                // Run this for each new test
                context.Database.EnsureClean();
            }
        }
        
        private void SqliteDb()
        {
            var connectionString = Config.GetConnectionString("DefaultConnection");
            var builder = new DbContextOptionsBuilder<PokeflexContext>();
            builder.UseSqlServer(connectionString);
            using (var context = new PokeflexContext(builder.Options))
            {
                context.Database.EnsureCreated();
                // Run this for each new test
                context.Database.EnsureClean();
            }
        }
    }
    
}