using System;
using System.Linq;

namespace App.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PokeflexContext context)
        {
            context.Database.EnsureCreated();

            // // Look for any pokemons.
            // if (context.Pokemons.Any())
            // {
            //     return;   // DB has been seeded
            // }
            //
            // var pokemons = new Pokemon[]
            // {
            // new Pokemon{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
            // new Pokemon{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
            // new Pokemon{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
            // new Pokemon{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
            // new Pokemon{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
            // new Pokemon{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
            // new Pokemon{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
            // new Pokemon{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}
            // };
            // foreach (Pokemon s in pokemons)
            // {
            //     context.Pokemons.Add(s);
            // }
            // context.SaveChanges();
        }
    }
}