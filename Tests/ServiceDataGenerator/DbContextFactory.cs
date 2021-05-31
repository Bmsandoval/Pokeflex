using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using App.Data;
using App.Models;
using EntityFrameworkCoreMock;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestSupport.EfHelpers;
using TestSupport.Helpers;

namespace Tests.ServiceDataGenerator
{
    public interface IDbContext
    {
        public PokeflexContext PokeflexContext { get; set; }
    }

    public static class DbContextFactory
    {
        public static IDbContext NewUniqueContext(
            string callingClassName,
            Mocker mocker
            )
        {
            var contextType =
                Environment.GetEnvironmentVariable("DotnetTestDbType", EnvironmentVariableTarget.Process)
                ?? "";

            return contextType switch
            {
                "inmemory" => new InMemoryDbContext(callingClassName, mocker),
                "native" => new NativeDbContext(callingClassName, mocker),
                _ => throw new ArgumentException("invalid option for env var PokeflexTestDbType %s", contextType)
            };
        }
    }

    public class InMemoryDbContext : IDbContext 
    {
        public PokeflexContext PokeflexContext { get; set; }
        private string ConnectionString { get; set; }
        private DbContextOptions<PokeflexContext> ContextOptions { get; set; }

        private void SetConnectionString(string dbUniquifier)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder
            {
                DataSource = dbUniquifier,
                Mode = SqliteOpenMode.Memory,
            };
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
        public InMemoryDbContext(string dbUniquifier, Mocker mocker)
        {
            SetConnectionString(dbUniquifier);
            BuildOptions();
            PokeflexContext = new PokeflexContext(ContextOptions);
            PokeflexContext.CreateEmptyViaDelete();
            
            foreach (var group in mocker)
            {
                foreach (var poke in group.Pokemons)
                {
                    PokeflexContext.Pokemons.Add(poke);
                }

                if (group.Id == 0) continue;
                PokeflexContext.Groups.Add(group);
            }
            PokeflexContext.SaveChanges();
            
            UdfMigrations.MigrateUdfs(PokeflexContext);
        }
    }
    
    public class NativeDbContext : IDbContext 
    {
        public PokeflexContext PokeflexContext { get; set; }
        private string ConnectionString { get; set; }
        private DbContextOptions<PokeflexContext> ContextOptions { get; set; }
        private IConfigurationRoot Config { get; set; }
        
        private void SetConnectionString(string dbUniquifier)
        {
            var callingProjectPath = TestData.GetCallingAssemblyTopLevelDir();
            Config = new ConfigurationBuilder()
                .SetBasePath(callingProjectPath)
                .AddJsonFile("appsettings.Test.json").Build();
            string connectionString = Config.GetConnectionString("UnitTestConnection");
            SqlConnectionStringBuilder connectionStringBuilder = !string.IsNullOrEmpty(connectionString) ? new SqlConnectionStringBuilder(connectionString) : throw new InvalidOperationException("You are missing a connection string of name 'UnitTestConnection' in the appsettings.json file.");
            if (!connectionStringBuilder.InitialCatalog.EndsWith("Test"))
                throw new InvalidOperationException("The database name in your connection string must end with 'Test', but is '" + connectionStringBuilder.InitialCatalog + "'. This is a safety measure to help stop DeleteAllUnitTestDatabases from deleting production databases.");
            connectionStringBuilder.InitialCatalog += $"{'_'}{dbUniquifier}";
            ConnectionString = connectionStringBuilder.ToString();
        }

        private void BuildOptions()
        {
            var builder = new DbContextOptionsBuilder<PokeflexContext>();
            builder.UseSqlServer(ConnectionString);
            ContextOptions = builder.Options;
        }
        
        public NativeDbContext(string dbNameUniquifier, Mocker mocker)
        {
            SetConnectionString(dbNameUniquifier);
            BuildOptions();
            PokeflexContext = new PokeflexContext(ContextOptions);
            PokeflexContext.CreateEmptyViaDelete();
            
            foreach (var group in mocker)
            {
                foreach (var poke in group.Pokemons)
                {
                    PokeflexContext.Pokemons.Add(poke);
                }

                if (group.Id == 0) continue;
                PokeflexContext.Groups.Add(group);
            }
            PokeflexContext.SaveChanges();
            
            UdfMigrations.MigrateUdfs(PokeflexContext);
        }
    }
}