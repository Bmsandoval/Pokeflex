using System.Collections.Generic;

namespace Tests.ServiceDataGenerator
{
    public class Seeder
    {
        public static IEnumerable<object[]> EmptyDb()
        {
            yield return new object[] {Mocker.Empty()};
        }

        // MIXED
        public static IEnumerable<object[]> MixedPokeDb()
        {
            var number = 42;
            yield return new object[] {
                Mocker.HasGroups(1).WithPokemon(42)
                , number
            };
        }

        // BASE ONLY
        public static IEnumerable<object[]> BasePokeOnlyDb()
        {
            yield return new object[] {
                Mocker.HasGroup(0).WithPokemons(1)
            };
        }

        // FLEX ONLY
        public static IEnumerable<object[]> FlexPokeOnlyDb()
        {
            yield return new object[] {
                Mocker.HasGroup(1).WithPokemon(1)
            };
        }
    }
}
