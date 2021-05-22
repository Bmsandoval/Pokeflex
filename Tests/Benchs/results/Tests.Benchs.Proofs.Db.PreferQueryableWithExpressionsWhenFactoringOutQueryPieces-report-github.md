``` ini

BenchmarkDotNet=v0.12.1, OS=macOS 11.2.3 (20D91) [Darwin 20.3.0]
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.201
  [Host] : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT

Toolchain=InProcessEmitToolchain  InvocationCount=1  IterationCount=1  
LaunchCount=1  UnrollFactor=1  WarmupCount=1  

```
|                           Method | Groups | Numbers |     Mean | Error | Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------------------- |------- |-------- |---------:|------:|------:|------:|------:|----------:|
| **QueryableExtendedWithExpressions** |      **5** |      **10** | **381.8 μs** |    **NA** |     **-** |     **-** |     **-** |  **29.96 KB** |
|     QueryableExtendedWithLambdas |      5 |      10 | 612.0 μs |    NA |     - |     - |     - |   30.1 KB |
| **QueryableExtendedWithExpressions** |      **5** |     **100** | **351.1 μs** |    **NA** |     **-** |     **-** |     **-** |  **30.04 KB** |
|     QueryableExtendedWithLambdas |      5 |     100 | 425.1 μs |    NA |     - |     - |     - |  30.15 KB |
| **QueryableExtendedWithExpressions** |      **5** |    **1000** | **432.3 μs** |    **NA** |     **-** |     **-** |     **-** |  **30.25 KB** |
|     QueryableExtendedWithLambdas |      5 |    1000 | 420.5 μs |    NA |     - |     - |     - |   29.8 KB |
| **QueryableExtendedWithExpressions** |     **10** |      **10** | **353.0 μs** |    **NA** |     **-** |     **-** |     **-** |   **29.6 KB** |
|     QueryableExtendedWithLambdas |     10 |      10 | 269.9 μs |    NA |     - |     - |     - |   29.3 KB |
| **QueryableExtendedWithExpressions** |     **10** |     **100** | **358.4 μs** |    **NA** |     **-** |     **-** |     **-** |  **29.66 KB** |
|     QueryableExtendedWithLambdas |     10 |     100 | 252.2 μs |    NA |     - |     - |     - |   29.3 KB |
| **QueryableExtendedWithExpressions** |     **10** |    **1000** | **380.8 μs** |    **NA** |     **-** |     **-** |     **-** |  **29.59 KB** |
|     QueryableExtendedWithLambdas |     10 |    1000 | 360.2 μs |    NA |     - |     - |     - |  29.73 KB |
| **QueryableExtendedWithExpressions** |     **15** |      **10** | **289.1 μs** |    **NA** |     **-** |     **-** |     **-** |  **29.59 KB** |
|     QueryableExtendedWithLambdas |     15 |      10 | 240.6 μs |    NA |     - |     - |     - |  29.73 KB |
| **QueryableExtendedWithExpressions** |     **15** |     **100** | **258.9 μs** |    **NA** |     **-** |     **-** |     **-** |  **29.59 KB** |
|     QueryableExtendedWithLambdas |     15 |     100 | 339.5 μs |    NA |     - |     - |     - |  29.73 KB |
| **QueryableExtendedWithExpressions** |     **15** |    **1000** | **381.2 μs** |    **NA** |     **-** |     **-** |     **-** |  **29.63 KB** |
|     QueryableExtendedWithLambdas |     15 |    1000 | 423.3 μs |    NA |     - |     - |     - |  29.73 KB |
