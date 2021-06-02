``` ini

BenchmarkDotNet=v0.12.1, OS=macOS 11.2.3 (20D91) [Darwin 20.3.0]
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.203
  [Host] : .NET Core 5.0.6 (CoreCLR 5.0.621.22011, CoreFX 5.0.621.22011), X64 RyuJIT

Toolchain=InProcessEmitToolchain  InvocationCount=1  UnrollFactor=1  
Categories=fakeyfakey,Service,Pokeflex,Raw,Get  

```
|            Method | Groups | Numbers | Mean | Error |
|------------------ |------- |-------- |-----:|------:|
| **LeftJoinOnNumList** |      **5** |      **10** |   **NA** |    **NA** |
| **LeftJoinOnNumList** |      **5** |     **100** |   **NA** |    **NA** |
| **LeftJoinOnNumList** |      **5** |    **1000** |   **NA** |    **NA** |
| **LeftJoinOnNumList** |     **10** |      **10** |   **NA** |    **NA** |
| **LeftJoinOnNumList** |     **10** |     **100** |   **NA** |    **NA** |
| **LeftJoinOnNumList** |     **10** |    **1000** |   **NA** |    **NA** |
| **LeftJoinOnNumList** |     **15** |      **10** |   **NA** |    **NA** |
| **LeftJoinOnNumList** |     **15** |     **100** |   **NA** |    **NA** |
| **LeftJoinOnNumList** |     **15** |    **1000** |   **NA** |    **NA** |

Benchmarks with issues:
  PokeflexServiceRawGetBenchmarks.LeftJoinOnNumList: Job-ULWGET(Toolchain=InProcessEmitToolchain, InvocationCount=1, UnrollFactor=1) [Groups=5, Numbers=10]
  PokeflexServiceRawGetBenchmarks.LeftJoinOnNumList: Job-ULWGET(Toolchain=InProcessEmitToolchain, InvocationCount=1, UnrollFactor=1) [Groups=5, Numbers=100]
  PokeflexServiceRawGetBenchmarks.LeftJoinOnNumList: Job-ULWGET(Toolchain=InProcessEmitToolchain, InvocationCount=1, UnrollFactor=1) [Groups=5, Numbers=1000]
  PokeflexServiceRawGetBenchmarks.LeftJoinOnNumList: Job-ULWGET(Toolchain=InProcessEmitToolchain, InvocationCount=1, UnrollFactor=1) [Groups=10, Numbers=10]
  PokeflexServiceRawGetBenchmarks.LeftJoinOnNumList: Job-ULWGET(Toolchain=InProcessEmitToolchain, InvocationCount=1, UnrollFactor=1) [Groups=10, Numbers=100]
  PokeflexServiceRawGetBenchmarks.LeftJoinOnNumList: Job-ULWGET(Toolchain=InProcessEmitToolchain, InvocationCount=1, UnrollFactor=1) [Groups=10, Numbers=1000]
  PokeflexServiceRawGetBenchmarks.LeftJoinOnNumList: Job-ULWGET(Toolchain=InProcessEmitToolchain, InvocationCount=1, UnrollFactor=1) [Groups=15, Numbers=10]
  PokeflexServiceRawGetBenchmarks.LeftJoinOnNumList: Job-ULWGET(Toolchain=InProcessEmitToolchain, InvocationCount=1, UnrollFactor=1) [Groups=15, Numbers=100]
  PokeflexServiceRawGetBenchmarks.LeftJoinOnNumList: Job-ULWGET(Toolchain=InProcessEmitToolchain, InvocationCount=1, UnrollFactor=1) [Groups=15, Numbers=1000]
