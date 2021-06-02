``` ini

BenchmarkDotNet=v0.12.1, OS=macOS 11.2.3 (20D91) [Darwin 20.3.0]
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.203
  [Host] : .NET Core 5.0.6 (CoreCLR 5.0.621.22011, CoreFX 5.0.621.22011), X64 RyuJIT

Toolchain=InProcessEmitToolchain  InvocationCount=1  IterationCount=100  
LaunchCount=3  UnrollFactor=1  WarmupCount=15  

```
|                           Method | Groups | Numbers |     Mean |     Error |    StdDev |   Median | Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------------------- |------- |-------- |---------:|----------:|----------:|---------:|------:|------:|------:|----------:|
| **QueryableExtendedWithExpressions** |      **5** |      **10** | **2.370 ms** | **0.0370 ms** | **0.1886 ms** | **2.361 ms** |     **-** |     **-** |     **-** |  **54.65 KB** |
|     QueryableExtendedWithLambdas |      5 |      10 | 2.311 ms | 0.0334 ms | 0.1694 ms | 2.280 ms |     - |     - |     - |   57.8 KB |
| **QueryableExtendedWithExpressions** |      **5** |     **100** | **2.301 ms** | **0.0388 ms** | **0.1992 ms** | **2.281 ms** |     **-** |     **-** |     **-** |  **55.26 KB** |
|     QueryableExtendedWithLambdas |      5 |     100 | 2.305 ms | 0.0394 ms | 0.2001 ms | 2.278 ms |     - |     - |     - |  56.82 KB |
| **QueryableExtendedWithExpressions** |      **5** |    **1000** | **2.438 ms** | **0.0531 ms** | **0.2670 ms** | **2.394 ms** |     **-** |     **-** |     **-** |  **54.98 KB** |
|     QueryableExtendedWithLambdas |      5 |    1000 | 2.503 ms | 0.0533 ms | 0.2736 ms | 2.470 ms |     - |     - |     - |  56.82 KB |
| **QueryableExtendedWithExpressions** |     **10** |      **10** | **2.386 ms** | **0.0416 ms** | **0.2114 ms** | **2.380 ms** |     **-** |     **-** |     **-** |   **54.7 KB** |
|     QueryableExtendedWithLambdas |     10 |      10 | 2.420 ms | 0.0511 ms | 0.2618 ms | 2.395 ms |     - |     - |     - |  56.82 KB |
| **QueryableExtendedWithExpressions** |     **10** |     **100** | **2.336 ms** | **0.0408 ms** | **0.2059 ms** | **2.313 ms** |     **-** |     **-** |     **-** |  **55.26 KB** |
|     QueryableExtendedWithLambdas |     10 |     100 | 2.272 ms | 0.0408 ms | 0.2033 ms | 2.261 ms |     - |     - |     - |   57.5 KB |
| **QueryableExtendedWithExpressions** |     **10** |    **1000** | **2.608 ms** | **0.0638 ms** | **0.3216 ms** | **2.602 ms** |     **-** |     **-** |     **-** |  **54.65 KB** |
|     QueryableExtendedWithLambdas |     10 |    1000 | 2.686 ms | 0.0519 ms | 0.2658 ms | 2.690 ms |     - |     - |     - |  56.82 KB |
| **QueryableExtendedWithExpressions** |     **15** |      **10** | **2.368 ms** | **0.0435 ms** | **0.2194 ms** | **2.358 ms** |     **-** |     **-** |     **-** |  **54.65 KB** |
|     QueryableExtendedWithLambdas |     15 |      10 | 2.475 ms | 0.0514 ms | 0.2571 ms | 2.461 ms |     - |     - |     - |  56.82 KB |
| **QueryableExtendedWithExpressions** |     **15** |     **100** | **2.536 ms** | **0.0532 ms** | **0.2685 ms** | **2.525 ms** |     **-** |     **-** |     **-** |  **55.26 KB** |
|     QueryableExtendedWithLambdas |     15 |     100 | 2.592 ms | 0.0473 ms | 0.2392 ms | 2.592 ms |     - |     - |     - |  56.82 KB |
| **QueryableExtendedWithExpressions** |     **15** |    **1000** | **2.701 ms** | **0.0486 ms** | **0.2465 ms** | **2.680 ms** |     **-** |     **-** |     **-** |  **54.65 KB** |
|     QueryableExtendedWithLambdas |     15 |    1000 | 2.748 ms | 0.0950 ms | 0.4719 ms | 2.653 ms |     - |     - |     - |  56.82 KB |
