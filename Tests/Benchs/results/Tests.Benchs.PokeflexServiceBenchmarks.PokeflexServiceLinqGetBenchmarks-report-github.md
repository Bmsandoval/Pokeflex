``` ini

BenchmarkDotNet=v0.12.1, OS=macOS 11.2.3 (20D91) [Darwin 20.3.0]
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.201
  [Host] : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT

Toolchain=InProcessEmitToolchain  InvocationCount=1  IterationCount=100  
LaunchCount=3  UnrollFactor=1  WarmupCount=15  

```
|                                     Method |                                Categories | Groups | Numbers |       Mean |     Error |    StdDev |     Median | Ratio | RatioSD |
|------------------------------------------- |------------------------------------------ |------- |-------- |-----------:|----------:|----------:|-----------:|------:|--------:|
|                        &#39;Linq Query Syntax&#39; |   LinqQuery,All,Service,Pokeflex,Linq,Get |     15 |     100 | 2,956.1 μs |  65.03 μs | 336.62 μs | 2,894.0 μs |  1.00 |    0.00 |
|                       &#39;Linq Method Syntax&#39; |  LinqMethod,All,Service,Pokeflex,Linq,Get |     15 |     100 |   265.9 μs |  10.10 μs |  51.90 μs |   255.5 μs |  0.09 |    0.02 |
| UnionWhereNotExistsWithoutExtensionMethods |             All,Service,Pokeflex,Linq,Get |     15 |     100 | 2,988.6 μs |  62.05 μs | 318.41 μs | 2,938.2 μs |  1.03 |    0.17 |
|                        UnionWhereNotExists | ActiveState,All,Service,Pokeflex,Linq,Get |     15 |     100 | 2,913.7 μs |  55.26 μs | 283.54 μs | 2,870.0 μs |  1.00 |    0.15 |
|                                   Coalesce |             All,Service,Pokeflex,Linq,Get |     15 |     100 | 3,197.9 μs |  63.59 μs | 327.45 μs | 3,183.2 μs |  1.10 |    0.16 |
|                       SequentialWhereNotIn |             All,Service,Pokeflex,Linq,Get |     15 |     100 | 5,969.5 μs | 150.28 μs | 777.86 μs | 5,951.3 μs |  2.05 |    0.36 |
|                        SequentialWorstCase |             All,Service,Pokeflex,Linq,Get |     15 |     100 | 5,661.0 μs | 127.60 μs | 658.19 μs | 5,706.4 μs |  1.94 |    0.32 |
