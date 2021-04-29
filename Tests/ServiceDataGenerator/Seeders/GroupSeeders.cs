using System;
using System.Collections.Generic;
using App.Models;

namespace Tests.ServiceDataGenerator.Seeders
{
    public class GroupSeeder
    {
        public static IEnumerable<object[]> EmptyDatabase()
        {
            yield return new object[] {DbContextFactory.NewDbContext(groups: Array.Empty<Group>()).PokeflexContext};
        }
    }
}
