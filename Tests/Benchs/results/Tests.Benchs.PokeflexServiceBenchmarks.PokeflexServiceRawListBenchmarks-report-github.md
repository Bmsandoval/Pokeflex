``` ini

BenchmarkDotNet=v0.12.1, OS=macOS 11.2.3 (20D91) [Darwin 20.3.0]
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.203
  [Host] : .NET Core 5.0.6 (CoreCLR 5.0.621.22011, CoreFX 5.0.621.22011), X64 RyuJIT

Toolchain=InProcessEmitToolchain  InvocationCount=1  UnrollFactor=1  
Categories=Service,Pokeflex,Raw,List  

```
|                 Method | Groups | Numbers | LimitAsPctNumbers |      Mean |     Error |    StdDev |    Median |     Gen 0 | Gen 1 | Gen 2 |  Allocated |
|----------------------- |------- |-------- |------------------ |----------:|----------:|----------:|----------:|----------:|------:|------:|-----------:|
|      **LeftJoinOnNumList** |      **5** |      **10** |               **0.1** |  **2.375 ms** | **0.0800 ms** | **0.2320 ms** |  **2.378 ms** |         **-** |     **-** |     **-** |   **32.44 KB** |
|    UnionWhereNotExists |      5 |      10 |               0.1 |  2.003 ms | 0.0677 ms | 0.1963 ms |  1.996 ms |         - |     - |     - |   27.82 KB |
| UnionWhereNotExistsCte |      5 |      10 |               0.1 |  2.335 ms | 0.0620 ms | 0.1789 ms |  2.336 ms |         - |     - |     - |   56.85 KB |
|               Coalesce |      5 |      10 |               0.1 |  1.942 ms | 0.0635 ms | 0.1862 ms |  1.939 ms |         - |     - |     - |   28.52 KB |
|      **LeftJoinOnNumList** |      **5** |      **10** |               **0.3** |  **2.060 ms** | **0.0561 ms** | **0.1635 ms** |  **2.014 ms** |         **-** |     **-** |     **-** |   **32.21 KB** |
|    UnionWhereNotExists |      5 |      10 |               0.3 |  2.114 ms | 0.0614 ms | 0.1792 ms |  2.122 ms |         - |     - |     - |   29.71 KB |
| UnionWhereNotExistsCte |      5 |      10 |               0.3 |  2.353 ms | 0.0589 ms | 0.1700 ms |  2.348 ms |         - |     - |     - |   51.62 KB |
|               Coalesce |      5 |      10 |               0.3 |  2.043 ms | 0.0642 ms | 0.1873 ms |  2.040 ms |         - |     - |     - |   28.52 KB |
|      **LeftJoinOnNumList** |      **5** |     **100** |               **0.1** |  **2.252 ms** | **0.0694 ms** | **0.2014 ms** |  **2.246 ms** |         **-** |     **-** |     **-** |   **35.93 KB** |
|    UnionWhereNotExists |      5 |     100 |               0.1 |  2.250 ms | 0.0659 ms | 0.1911 ms |  2.216 ms |         - |     - |     - |   32.77 KB |
| UnionWhereNotExistsCte |      5 |     100 |               0.1 |  4.927 ms | 0.1213 ms | 0.3479 ms |  4.898 ms |         - |     - |     - |  379.74 KB |
|               Coalesce |      5 |     100 |               0.1 |  1.979 ms | 0.0577 ms | 0.1693 ms |  1.984 ms |         - |     - |     - |   28.52 KB |
|      **LeftJoinOnNumList** |      **5** |     **100** |               **0.3** |  **2.385 ms** | **0.0686 ms** | **0.1979 ms** |  **2.366 ms** |         **-** |     **-** |     **-** |   **46.67 KB** |
|    UnionWhereNotExists |      5 |     100 |               0.3 |  2.164 ms | 0.0646 ms | 0.1896 ms |  2.156 ms |         - |     - |     - |   43.51 KB |
| UnionWhereNotExistsCte |      5 |     100 |               0.3 |  3.930 ms | 0.1050 ms | 0.3013 ms |  3.875 ms |         - |     - |     - |  316.51 KB |
|               Coalesce |      5 |     100 |               0.3 |  1.899 ms | 0.0545 ms | 0.1591 ms |  1.883 ms |         - |     - |     - |   28.73 KB |
|      **LeftJoinOnNumList** |      **5** |    **1000** |               **0.1** |  **4.540 ms** | **0.1190 ms** | **0.3434 ms** |  **4.431 ms** |         **-** |     **-** |     **-** |   **84.86 KB** |
|    UnionWhereNotExists |      5 |    1000 |               0.1 |  3.597 ms | 0.0927 ms | 0.2615 ms |  3.593 ms |         - |     - |     - |    81.7 KB |
| UnionWhereNotExistsCte |      5 |    1000 |               0.1 | 33.761 ms | 0.8396 ms | 2.4623 ms | 33.985 ms |         - |     - |     - | 3663.92 KB |
|               Coalesce |      5 |    1000 |               0.1 |  2.267 ms | 0.0574 ms | 0.1673 ms |  2.252 ms |         - |     - |     - |   28.52 KB |
|      **LeftJoinOnNumList** |      **5** |    **1000** |               **0.3** |  **6.060 ms** | **0.2188 ms** | **0.6417 ms** |  **5.880 ms** |         **-** |     **-** |     **-** |  **224.69 KB** |
|    UnionWhereNotExists |      5 |    1000 |               0.3 |  4.106 ms | 0.1238 ms | 0.3491 ms |  4.051 ms |         - |     - |     - |   220.7 KB |
| UnionWhereNotExistsCte |      5 |    1000 |               0.3 | 28.352 ms | 0.7102 ms | 2.0830 ms | 28.321 ms |         - |     - |     - | 2985.13 KB |
|               Coalesce |      5 |    1000 |               0.3 |  2.244 ms | 0.0923 ms | 0.2708 ms |  2.199 ms |         - |     - |     - |   28.52 KB |
|      **LeftJoinOnNumList** |     **15** |      **10** |               **0.1** |  **2.134 ms** | **0.0525 ms** | **0.1523 ms** |  **2.107 ms** |         **-** |     **-** |     **-** |   **30.98 KB** |
|    UnionWhereNotExists |     15 |      10 |               0.1 |  2.195 ms | 0.0730 ms | 0.2130 ms |  2.185 ms |         - |     - |     - |   28.66 KB |
| UnionWhereNotExistsCte |     15 |      10 |               0.1 |  3.030 ms | 0.0606 ms | 0.1508 ms |  3.042 ms |         - |     - |     - |  117.28 KB |
|               Coalesce |     15 |      10 |               0.1 |  1.981 ms | 0.0515 ms | 0.1486 ms |  1.966 ms |         - |     - |     - |   28.52 KB |
|      **LeftJoinOnNumList** |     **15** |      **10** |               **0.3** |  **2.205 ms** | **0.0601 ms** | **0.1696 ms** |  **2.211 ms** |         **-** |     **-** |     **-** |   **32.03 KB** |
|    UnionWhereNotExists |     15 |      10 |               0.3 |  2.177 ms | 0.0679 ms | 0.1971 ms |  2.217 ms |         - |     - |     - |   29.55 KB |
| UnionWhereNotExistsCte |     15 |      10 |               0.3 |  2.985 ms | 0.0584 ms | 0.1619 ms |  2.973 ms |         - |     - |     - |   89.28 KB |
|               Coalesce |     15 |      10 |               0.3 |  2.051 ms | 0.0698 ms | 0.2049 ms |  2.060 ms |         - |     - |     - |   28.52 KB |
|      **LeftJoinOnNumList** |     **15** |     **100** |               **0.1** |  **2.355 ms** | **0.0940 ms** | **0.2711 ms** |  **2.332 ms** |         **-** |     **-** |     **-** |   **35.93 KB** |
|    UnionWhereNotExists |     15 |     100 |               0.1 |  2.509 ms | 0.0540 ms | 0.1550 ms |  2.504 ms |         - |     - |     - |   32.77 KB |
| UnionWhereNotExistsCte |     15 |     100 |               0.1 |  9.728 ms | 0.2455 ms | 0.7084 ms |  9.758 ms |         - |     - |     - |  961.38 KB |
|               Coalesce |     15 |     100 |               0.1 |  2.222 ms | 0.0549 ms | 0.1601 ms |  2.217 ms |         - |     - |     - |   29.37 KB |
|      **LeftJoinOnNumList** |     **15** |     **100** |               **0.3** |  **2.276 ms** | **0.0681 ms** | **0.1953 ms** |  **2.228 ms** |         **-** |     **-** |     **-** |   **46.67 KB** |
|    UnionWhereNotExists |     15 |     100 |               0.3 |  2.375 ms | 0.0681 ms | 0.1920 ms |  2.367 ms |         - |     - |     - |   43.51 KB |
| UnionWhereNotExistsCte |     15 |     100 |               0.3 |  8.089 ms | 0.1906 ms | 0.5561 ms |  8.023 ms |         - |     - |     - |   793.3 KB |
|               Coalesce |     15 |     100 |               0.3 |  2.159 ms | 0.0632 ms | 0.1854 ms |  2.164 ms |         - |     - |     - |   28.52 KB |
|      **LeftJoinOnNumList** |     **15** |    **1000** |               **0.1** |  **6.851 ms** | **0.1356 ms** | **0.3033 ms** |  **6.861 ms** |         **-** |     **-** |     **-** |   **84.86 KB** |
|    UnionWhereNotExists |     15 |    1000 |               0.1 |  5.834 ms | 0.1164 ms | 0.3025 ms |  5.860 ms |         - |     - |     - |    81.7 KB |
| UnionWhereNotExistsCte |     15 |    1000 |               0.1 | 89.013 ms | 2.9214 ms | 8.6138 ms | 89.502 ms | 1000.0000 |     - |     - | 9458.91 KB |
|               Coalesce |     15 |    1000 |               0.1 |  2.416 ms | 0.0578 ms | 0.1687 ms |  2.416 ms |         - |     - |     - |   29.37 KB |
|      **LeftJoinOnNumList** |     **15** |    **1000** |               **0.3** |  **8.057 ms** | **0.1932 ms** | **0.5514 ms** |  **8.111 ms** |         **-** |     **-** |     **-** |  **216.64 KB** |
|    UnionWhereNotExists |     15 |    1000 |               0.3 |  5.934 ms | 0.1967 ms | 0.5798 ms |  5.971 ms |         - |     - |     - |  221.39 KB |
| UnionWhereNotExistsCte |     15 |    1000 |               0.3 | 67.345 ms | 2.3274 ms | 6.8624 ms | 67.790 ms | 1000.0000 |     - |     - | 7437.13 KB |
|               Coalesce |     15 |    1000 |               0.3 |  2.505 ms | 0.0783 ms | 0.2260 ms |  2.517 ms |         - |     - |     - |   28.52 KB |
