using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Filters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;

namespace Tests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = ManualConfig.Create(CustomConfig.Instance);
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
                .Run(args, config
                    .WithArtifactsPath(".")
                    .WithOption(ConfigOptions.DisableOptimizationsValidator, true));
        }
    }

    public class CustomConfig : IConfig
    {
        public static readonly IConfig Instance = (IConfig) new CustomConfig();

        private CustomConfig()
        {
        }

        public IEnumerable<IColumnProvider> GetColumnProviders() =>
            (IEnumerable<IColumnProvider>) DefaultColumnProviders.Instance;

        public IEnumerable<IExporter> GetExporters()
        {
            yield return MarkdownExporter.GitHub;
        }

        public IEnumerable<ILogger> GetLoggers()
        {
            if (LinqPadLogger.IsAvailable)
                yield return LinqPadLogger.Instance;
            else
                yield return ConsoleLogger.Default;
        }

        public IEnumerable<IAnalyser> GetAnalysers()
        {
            yield return EnvironmentAnalyser.Default;
            yield return OutliersAnalyser.Default;
            yield return MinIterationTimeAnalyser.Default;
            yield return MultimodalDistributionAnalyzer.Default;
            yield return RuntimeErrorAnalyser.Default;
            yield return ZeroMeasurementAnalyser.Default;
            yield return BaselineCustomAnalyzer.Default;
        }

        public IEnumerable<IValidator> GetValidators()
        {
            yield return (IValidator) BaselineValidator.FailOnError;
            yield return (IValidator) SetupCleanupValidator.FailOnError;
            yield return JitOptimizationsValidator.FailOnError;
            yield return RunModeValidator.FailOnError;
            yield return GenericBenchmarksValidator.DontFailOnError;
            yield return DeferredExecutionValidator.FailOnError;
            yield return (IValidator) ParamsAllValuesValidator.FailOnError;
        }

        public IOrderer Orderer => (IOrderer) null;

        public ConfigUnionRule UnionRule => ConfigUnionRule.Union;

        public CultureInfo CultureInfo => (CultureInfo) null;

        public ConfigOptions Options => ConfigOptions.Default;

        public SummaryStyle SummaryStyle => SummaryStyle.Default;

        public string ArtifactsPath => Path.Combine(Directory.GetCurrentDirectory(), "BenchmarkDotNet.Artifacts");

        public IEnumerable<Job> GetJobs() => (IEnumerable<Job>) Array.Empty<Job>();

        public IEnumerable<BenchmarkLogicalGroupRule> GetLogicalGroupRules() =>
            (IEnumerable<BenchmarkLogicalGroupRule>) Array.Empty<BenchmarkLogicalGroupRule>();

        public IEnumerable<IDiagnoser> GetDiagnosers() => (IEnumerable<IDiagnoser>) Array.Empty<IDiagnoser>();

        public IEnumerable<HardwareCounter> GetHardwareCounters() =>
            (IEnumerable<HardwareCounter>) Array.Empty<HardwareCounter>();

        public IEnumerable<IFilter> GetFilters() => (IEnumerable<IFilter>) Array.Empty<IFilter>();
    }
}