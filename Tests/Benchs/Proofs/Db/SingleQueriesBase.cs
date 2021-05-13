using System;
using BenchmarkDotNet.Attributes;
using Tests.ServiceDataGenerator;

namespace Tests.Benchs.Proofs.Db
{
    public abstract class SingleQueriesBase
    {
        private IDbContext _dbContext;
        private int? _group1;
        private int _number;
        private int? _group;

        protected IDbContext DbContext { get; set; }
        protected static Random Rand { get; set; } = new();

        protected int? Group
        {
            get => _group;
            private set => _group = value != 0 ? value : null;
        }

        protected int Number
        {
            get => _number;
            private set => _number = value;
        }

        public virtual int Groups { get; set; }
        public virtual int Numbers { get; set; }

        [GlobalSetup]
        public void Setup() => DbContext = DbContextFactory
            .NewUniqueContext(GetType().Name, Mocker
                .HasGroups(Groups)
                .WithPokemons(Numbers));

        [IterationSetup]
        public void IterationSetup() =>
            (Number, Group) = (Rand.Next(1, Numbers), Rand.Next(0, Groups));
    }
}