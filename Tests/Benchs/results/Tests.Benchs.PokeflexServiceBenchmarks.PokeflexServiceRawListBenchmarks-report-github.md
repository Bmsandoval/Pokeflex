``` ini

BenchmarkDotNet=v0.12.1, OS=macOS 11.2.3 (20D91) [Darwin 20.3.0]
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.203
  [Host] : .NET Core 5.0.6 (CoreCLR 5.0.621.22011, CoreFX 5.0.621.22011), X64 RyuJIT

Toolchain=InProcessEmitToolchain  InvocationCount=1  IterationCount=1  
LaunchCount=1  UnrollFactor=1  WarmupCount=1  

```
|                 Method |                        Categories | Groups | Numbers | LimitAsPctNumbers |      Mean | Error |     Gen 0 | Gen 1 | Gen 2 |  Allocated |
|----------------------- |---------------------------------- |------- |-------- |------------------ |----------:|------:|----------:|------:|------:|-----------:|
|      **LeftJoinOnNumList** | **koolcat,Service,Pokeflex,Raw,List** |      **5** |      **10** |               **0.1** |  **2.092 ms** |    **NA** |         **-** |     **-** |     **-** |   **36.98 KB** |
|    UnionWhereNotExists |         Service,Pokeflex,Raw,List |      5 |      10 |               0.1 |  2.825 ms |    NA |         - |     - |     - |   31.42 KB |
| UnionWhereNotExistsCte |         Service,Pokeflex,Raw,List |      5 |      10 |               0.1 |  2.277 ms |    NA |         - |     - |     - |    64.2 KB |
|               Coalesce |         Service,Pokeflex,Raw,List |      5 |      10 |               0.1 |  1.993 ms |    NA |         - |     - |     - |   33.88 KB |
|      **LeftJoinOnNumList** | **koolcat,Service,Pokeflex,Raw,List** |      **5** |      **10** |               **0.3** |  **3.702 ms** |    **NA** |         **-** |     **-** |     **-** |    **39.4 KB** |
|    UnionWhereNotExists |         Service,Pokeflex,Raw,List |      5 |      10 |               0.3 |  2.126 ms |    NA |         - |     - |     - |   33.22 KB |
| UnionWhereNotExistsCte |         Service,Pokeflex,Raw,List |      5 |      10 |               0.3 |  3.023 ms |    NA |         - |     - |     - |   58.88 KB |
|               Coalesce |         Service,Pokeflex,Raw,List |      5 |      10 |               0.3 |  2.759 ms |    NA |         - |     - |     - |   33.03 KB |
|      **LeftJoinOnNumList** | **koolcat,Service,Pokeflex,Raw,List** |      **5** |     **100** |               **0.1** |  **2.947 ms** |    **NA** |         **-** |     **-** |     **-** |   **43.27 KB** |
|    UnionWhereNotExists |         Service,Pokeflex,Raw,List |      5 |     100 |               0.1 |  2.735 ms |    NA |         - |     - |     - |   36.98 KB |
| UnionWhereNotExistsCte |         Service,Pokeflex,Raw,List |      5 |     100 |               0.1 |  5.301 ms |    NA |         - |     - |     - |  397.72 KB |
|               Coalesce |         Service,Pokeflex,Raw,List |      5 |     100 |               0.1 |  2.256 ms |    NA |         - |     - |     - |   32.98 KB |
|      **LeftJoinOnNumList** | **koolcat,Service,Pokeflex,Raw,List** |      **5** |     **100** |               **0.3** |  **2.586 ms** |    **NA** |         **-** |     **-** |     **-** |   **54.88 KB** |
|    UnionWhereNotExists |         Service,Pokeflex,Raw,List |      5 |     100 |               0.3 |  2.693 ms |    NA |         - |     - |     - |   50.72 KB |
| UnionWhereNotExistsCte |         Service,Pokeflex,Raw,List |      5 |     100 |               0.3 |  4.261 ms |    NA |         - |     - |     - |  319.73 KB |
|               Coalesce |         Service,Pokeflex,Raw,List |      5 |     100 |               0.3 |  2.530 ms |    NA |         - |     - |     - |   34.19 KB |
|      **LeftJoinOnNumList** | **koolcat,Service,Pokeflex,Raw,List** |      **5** |    **1000** |               **0.1** |  **6.497 ms** |    **NA** |         **-** |     **-** |     **-** |   **89.05 KB** |
|    UnionWhereNotExists |         Service,Pokeflex,Raw,List |      5 |    1000 |               0.1 |  3.695 ms |    NA |         - |     - |     - |   85.49 KB |
| UnionWhereNotExistsCte |         Service,Pokeflex,Raw,List |      5 |    1000 |               0.1 | 35.443 ms |    NA |         - |     - |     - |  3582.3 KB |
|               Coalesce |         Service,Pokeflex,Raw,List |      5 |    1000 |               0.1 |  3.035 ms |    NA |         - |     - |     - |   32.98 KB |
|      **LeftJoinOnNumList** | **koolcat,Service,Pokeflex,Raw,List** |      **5** |    **1000** |               **0.3** |  **6.446 ms** |    **NA** |         **-** |     **-** |     **-** |  **228.27 KB** |
|    UnionWhereNotExists |         Service,Pokeflex,Raw,List |      5 |    1000 |               0.3 |  4.521 ms |    NA |         - |     - |     - |  228.56 KB |
| UnionWhereNotExistsCte |         Service,Pokeflex,Raw,List |      5 |    1000 |               0.3 | 34.022 ms |    NA |         - |     - |     - | 2940.76 KB |
|               Coalesce |         Service,Pokeflex,Raw,List |      5 |    1000 |               0.3 |  2.412 ms |    NA |         - |     - |     - |   32.98 KB |
|      **LeftJoinOnNumList** | **koolcat,Service,Pokeflex,Raw,List** |     **15** |      **10** |               **0.1** |  **2.901 ms** |    **NA** |         **-** |     **-** |     **-** |   **35.45 KB** |
|    UnionWhereNotExists |         Service,Pokeflex,Raw,List |     15 |      10 |               0.1 |  2.846 ms |    NA |         - |     - |     - |   31.28 KB |
| UnionWhereNotExistsCte |         Service,Pokeflex,Raw,List |     15 |      10 |               0.1 |  3.522 ms |    NA |         - |     - |     - |  120.47 KB |
|               Coalesce |         Service,Pokeflex,Raw,List |     15 |      10 |               0.1 |  2.076 ms |    NA |         - |     - |     - |   32.98 KB |
|      **LeftJoinOnNumList** | **koolcat,Service,Pokeflex,Raw,List** |     **15** |      **10** |               **0.3** |  **2.842 ms** |    **NA** |         **-** |     **-** |     **-** |   **37.35 KB** |
|    UnionWhereNotExists |         Service,Pokeflex,Raw,List |     15 |      10 |               0.3 |  3.198 ms |    NA |         - |     - |     - |   33.28 KB |
| UnionWhereNotExistsCte |         Service,Pokeflex,Raw,List |     15 |      10 |               0.3 |  2.922 ms |    NA |         - |     - |     - |   92.47 KB |
|               Coalesce |         Service,Pokeflex,Raw,List |     15 |      10 |               0.3 |  2.424 ms |    NA |         - |     - |     - |   32.98 KB |
|      **LeftJoinOnNumList** | **koolcat,Service,Pokeflex,Raw,List** |     **15** |     **100** |               **0.1** |  **2.187 ms** |    **NA** |         **-** |     **-** |     **-** |   **41.14 KB** |
|    UnionWhereNotExists |         Service,Pokeflex,Raw,List |     15 |     100 |               0.1 |  2.392 ms |    NA |         - |     - |     - |   36.98 KB |
| UnionWhereNotExistsCte |         Service,Pokeflex,Raw,List |     15 |     100 |               0.1 |  7.707 ms |    NA |         - |     - |     - |  980.16 KB |
|               Coalesce |         Service,Pokeflex,Raw,List |     15 |     100 |               0.1 |  2.363 ms |    NA |         - |     - |     - |   32.98 KB |
|      **LeftJoinOnNumList** | **koolcat,Service,Pokeflex,Raw,List** |     **15** |     **100** |               **0.3** |  **2.899 ms** |    **NA** |         **-** |     **-** |     **-** |   **54.88 KB** |
|    UnionWhereNotExists |         Service,Pokeflex,Raw,List |     15 |     100 |               0.3 |  2.464 ms |    NA |         - |     - |     - |   50.72 KB |
| UnionWhereNotExistsCte |         Service,Pokeflex,Raw,List |     15 |     100 |               0.3 |  9.558 ms |    NA |         - |     - |     - |  790.91 KB |
|               Coalesce |         Service,Pokeflex,Raw,List |     15 |     100 |               0.3 |  2.254 ms |    NA |         - |     - |     - |   32.98 KB |
|      **LeftJoinOnNumList** | **koolcat,Service,Pokeflex,Raw,List** |     **15** |    **1000** |               **0.1** |  **7.182 ms** |    **NA** |         **-** |     **-** |     **-** |   **89.05 KB** |
|    UnionWhereNotExists |         Service,Pokeflex,Raw,List |     15 |    1000 |               0.1 |  6.145 ms |    NA |         - |     - |     - |   84.88 KB |
| UnionWhereNotExistsCte |         Service,Pokeflex,Raw,List |     15 |    1000 |               0.1 | 69.862 ms |    NA | 1000.0000 |     - |     - | 9348.19 KB |
|               Coalesce |         Service,Pokeflex,Raw,List |     15 |    1000 |               0.1 |  2.624 ms |    NA |         - |     - |     - |   33.92 KB |
|      **LeftJoinOnNumList** | **koolcat,Service,Pokeflex,Raw,List** |     **15** |    **1000** |               **0.3** | **11.432 ms** |    **NA** |         **-** |     **-** |     **-** |  **220.02 KB** |
|    UnionWhereNotExists |         Service,Pokeflex,Raw,List |     15 |    1000 |               0.3 |  6.416 ms |    NA |         - |     - |     - |  228.08 KB |
| UnionWhereNotExistsCte |         Service,Pokeflex,Raw,List |     15 |    1000 |               0.3 | 61.713 ms |    NA | 1000.0000 |     - |     - | 7352.77 KB |
|               Coalesce |         Service,Pokeflex,Raw,List |     15 |    1000 |               0.3 |  2.571 ms |    NA |         - |     - |     - |   32.98 KB |
