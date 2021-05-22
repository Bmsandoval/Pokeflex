using System;
using System.Collections;
using System.Collections.Generic;
using App.Models;

namespace Tests.ServiceDataGenerator
{
    public class Mocker : IEnumerable<Group>
    {
        private int _pokeMax=-1;
        private int _pokeMin=1;
        private int _groupMax=-1;
        private int _groupMin=0;

        private Mocker() { }
        
        public static AppUser MockUser() => new AppUser(); 
        public static Pokemon MockPokemon(int? groupId, int number) => new (){
            GroupId = groupId,
            Number = number,
            ApiSource = Faker.Lorem.GetFirstWord(),
            Name = Faker.Lorem.GetFirstWord()};
        public static Group MockGroup(int? id = null) =>
                new (){Pokemons = new List<Pokemon>()};
        
        public static Mocker Empty()
        {
            return new();
        }

        public static Mocker HasGroup(int id) =>
            new() {_groupMin = id, _groupMax = id};
        public static Mocker HasGroups(int max) =>
            new () { _groupMax = max };
        
        public Mocker WithPokemon(int number)
        {
            _pokeMin = number;
            _pokeMax = number;
            return this;
        }
        public Mocker WithPokemons(int maxNumbers)
        {
            _pokeMax = maxNumbers;
            return this;
        }
        
        public IEnumerator<Group> GetEnumerator()
        {
            for (var g = _groupMin; g <= _groupMax; g++) 
            {
                var group = MockGroup(g);
                for (var p = _pokeMin; p <= _pokeMax; p++)
                {
                    Pokemon pokemon;
                    if (g == 0) pokemon = MockPokemon(null, p);
                    else
                    {
                        pokemon = MockPokemon(g, p);
                        pokemon.Group = group;
                    }
                    group.Pokemons.Add(pokemon);
                }
                yield return group;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
