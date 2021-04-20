using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using App.Data;
using App.Models;
using App.Services.Pokeflex;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestSupport.Helpers;

namespace Benchmarks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Environment.SetEnvironmentVariable("PokeflexBenchmarkDbType", "");
            }
            else
            {
                Environment.SetEnvironmentVariable("PokeflexBenchmarkDbType", args[0]);
            }
            
            var benchmarkConfig =
                ManualConfig
                .Create(DefaultConfig.Instance)
                .WithOption(ConfigOptions.JoinSummary, true)
                .WithOption(ConfigOptions.DisableOptimizationsValidator, true)
                .WithOption(ConfigOptions.DisableLogFile, true);
            
            
            BenchmarkRunner.Run<PokeflexServiceBenchmarks.PokeflexServiceGetBenchmarks>(benchmarkConfig);
            BenchmarkRunner.Run<PokeflexServiceBenchmarks.PokeflexServiceListBenchmarks>(benchmarkConfig);
        }
    }
}
