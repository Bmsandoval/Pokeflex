using System;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Tests.Benchs.PokeflexServiceBenchmarks;

namespace Tests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
                .Run(args, ManualConfig
                    .Create(DefaultConfig.Instance)
                    .WithOption(ConfigOptions.DisableOptimizationsValidator, true));
            // var benchmarkConfig =
            //     ManualConfig
            //     .Create(DefaultConfig.Instance)
            //     .WithOption(ConfigOptions.JoinSummary, true)
            //     .WithOption(ConfigOptions.DisableOptimizationsValidator, true)
            //     .WithOption(ConfigOptions.DisableLogFile, true);
            //
            //
            // BenchmarkRunner.Run<PokeflexServiceGetBenchmarks>(benchmarkConfig);
            // BenchmarkRunner.Run<PokeflexServiceListBenchmarks>(benchmarkConfig);
        }
    }
}
