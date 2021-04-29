using System;
using System.Collections.Generic;
using App.Models;

namespace Tests.ServiceDataGenerator.Seeders
{
    public class UserSeeder
    {
        public static IEnumerable<object[]> EmptyDatabase()
        {
            yield return new object[] {DbContextFactory.NewDbContext(users: Array.Empty<User>()).PokeflexContext};
        }
    }
}
