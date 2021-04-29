using System;
using System.Linq;

namespace App.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PokeflexContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}