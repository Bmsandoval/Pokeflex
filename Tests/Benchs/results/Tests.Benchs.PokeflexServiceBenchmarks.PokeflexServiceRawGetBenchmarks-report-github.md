``` ini

BenchmarkDotNet=v0.12.1, OS=macOS 11.2.3 (20D91) [Darwin 20.3.0]
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.203
  [Host] : .NET Core 5.0.6 (CoreCLR 5.0.621.22011, CoreFX 5.0.621.22011), X64 RyuJIT

Toolchain=InProcessEmitToolchain  InvocationCount=1  IterationCount=1  
LaunchCount=1  UnrollFactor=1  WarmupCount=1  
Categories=koolcat,Service,Pokeflex,Raw,Get  

```
|            Method | Groups | Numbers | Mean | Error |
|------------------ |------- |-------- |-----:|------:|
| LeftJoinOnNumList |      5 |      10 |   NA |    NA |

Benchmarks with issues:
  PokeflexServiceRawGetBenchmarks.LeftJoinOnNumList: Job-DFBBRG(Toolchain=InProcessEmitToolchain, InvocationCount=1, IterationCount=1, LaunchCount=1, UnrollFactor=1, WarmupCount=1) [Groups=5, Numbers=10]
