``` ini

BenchmarkDotNet=v0.12.1, OS=macOS 11.2.3 (20D91) [Darwin 20.3.0]
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.203
  [Host] : .NET Core 5.0.6 (CoreCLR 5.0.621.22011, CoreFX 5.0.621.22011), X64 RyuJIT

Toolchain=InProcessEmitToolchain  InvocationCount=1  UnrollFactor=1  
Categories=Service,Pokeflex,Raw,Get  

```
|                 Method | Groups | Numbers |      Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|----------------------- |------- |-------- |----------:|----------:|----------:|------:|--------:|------:|------:|------:|----------:|
|   **BaselineSimpleSelect** |      **5** |      **10** |  **2.300 ms** | **0.0853 ms** | **0.2503 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |  **25.01 KB** |
| CteUnionWhereNotExists |      5 |      10 |  2.290 ms | 0.0613 ms | 0.1770 ms |  1.01 |    0.14 |     - |     - |     - |  21.32 KB |
|    UnionWhereNotExists |      5 |      10 |  2.178 ms | 0.0517 ms | 0.1516 ms |  0.96 |    0.12 |     - |     - |     - |  32.74 KB |
|               Coalesce |      5 |      10 |  1.962 ms | 0.0563 ms | 0.1641 ms |  0.86 |    0.12 |     - |     - |     - |   30.3 KB |
|                        |        |         |           |           |           |       |         |       |       |       |           |
|   **BaselineSimpleSelect** |      **5** |     **100** |  **1.968 ms** | **0.0634 ms** | **0.1808 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |  **25.14 KB** |
| CteUnionWhereNotExists |      5 |     100 |  4.137 ms | 0.1385 ms | 0.3952 ms |  2.12 |    0.30 |     - |     - |     - |  27.48 KB |
|    UnionWhereNotExists |      5 |     100 |  1.888 ms | 0.0429 ms | 0.1245 ms |  0.97 |    0.11 |     - |     - |     - |  30.72 KB |
|               Coalesce |      5 |     100 |  2.070 ms | 0.0571 ms | 0.1674 ms |  1.06 |    0.14 |     - |     - |     - |   30.3 KB |
|                        |        |         |           |           |           |       |         |       |       |       |           |
|   **BaselineSimpleSelect** |      **5** |    **1000** |  **2.069 ms** | **0.0621 ms** | **0.1811 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |   **24.4 KB** |
| CteUnionWhereNotExists |      5 |    1000 | 34.495 ms | 1.0891 ms | 3.1769 ms | 16.79 |    2.10 |     - |     - |     - |  88.13 KB |
|    UnionWhereNotExists |      5 |    1000 |  2.274 ms | 0.0612 ms | 0.1765 ms |  1.11 |    0.13 |     - |     - |     - |  30.72 KB |
|               Coalesce |      5 |    1000 |  2.216 ms | 0.0562 ms | 0.1640 ms |  1.08 |    0.11 |     - |     - |     - |   30.3 KB |
|                        |        |         |           |           |           |       |         |       |       |       |           |
|   **BaselineSimpleSelect** |     **10** |      **10** |  **1.931 ms** | **0.0504 ms** | **0.1456 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |   **24.4 KB** |
| CteUnionWhereNotExists |     10 |      10 |  2.276 ms | 0.0843 ms | 0.2445 ms |  1.19 |    0.15 |     - |     - |     - |  21.32 KB |
|    UnionWhereNotExists |     10 |      10 |  2.123 ms | 0.0569 ms | 0.1660 ms |  1.11 |    0.11 |     - |     - |     - |  30.72 KB |
|               Coalesce |     10 |      10 |  1.939 ms | 0.0420 ms | 0.1205 ms |  1.01 |    0.09 |     - |     - |     - |  30.51 KB |
|                        |        |         |           |           |           |       |         |       |       |       |           |
|   **BaselineSimpleSelect** |     **10** |     **100** |  **1.908 ms** | **0.0474 ms** | **0.1382 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |   **24.4 KB** |
| CteUnionWhereNotExists |     10 |     100 |  7.048 ms | 0.2913 ms | 0.8588 ms |  3.72 |    0.56 |     - |     - |     - |  32.09 KB |
|    UnionWhereNotExists |     10 |     100 |  2.126 ms | 0.0566 ms | 0.1669 ms |  1.12 |    0.13 |     - |     - |     - |  31.56 KB |
|               Coalesce |     10 |     100 |  2.064 ms | 0.0535 ms | 0.1544 ms |  1.08 |    0.10 |     - |     - |     - |   30.3 KB |
|                        |        |         |           |           |           |       |         |       |       |       |           |
|   **BaselineSimpleSelect** |     **10** |    **1000** |  **2.224 ms** | **0.0807 ms** | **0.2379 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |   **24.4 KB** |
| CteUnionWhereNotExists |     10 |    1000 | 58.822 ms | 1.7745 ms | 5.2044 ms | 26.77 |    3.87 |     - |     - |     - | 155.88 KB |
|    UnionWhereNotExists |     10 |    1000 |  2.343 ms | 0.0841 ms | 0.2468 ms |  1.06 |    0.15 |     - |     - |     - |  31.56 KB |
|               Coalesce |     10 |    1000 |  2.267 ms | 0.0917 ms | 0.2690 ms |  1.03 |    0.18 |     - |     - |     - |   30.3 KB |
|                        |        |         |           |           |           |       |         |       |       |       |           |
|   **BaselineSimpleSelect** |     **15** |      **10** |  **2.174 ms** | **0.0643 ms** | **0.1895 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |   **24.4 KB** |
| CteUnionWhereNotExists |     15 |      10 |  2.514 ms | 0.0560 ms | 0.1569 ms |  1.16 |    0.13 |     - |     - |     - |  22.86 KB |
|    UnionWhereNotExists |     15 |      10 |  2.109 ms | 0.0835 ms | 0.2436 ms |  0.98 |    0.14 |     - |     - |     - |  30.72 KB |
|               Coalesce |     15 |      10 |  2.171 ms | 0.0624 ms | 0.1819 ms |  1.01 |    0.12 |     - |     - |     - |  31.15 KB |
|                        |        |         |           |           |           |       |         |       |       |       |           |
|   **BaselineSimpleSelect** |     **15** |     **100** |  **1.968 ms** | **0.0516 ms** | **0.1522 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |  **25.24 KB** |
| CteUnionWhereNotExists |     15 |     100 |  9.495 ms | 0.2805 ms | 0.8092 ms |  4.87 |    0.57 |     - |     - |     - |  42.02 KB |
|    UnionWhereNotExists |     15 |     100 |  2.103 ms | 0.0617 ms | 0.1781 ms |  1.08 |    0.12 |     - |     - |     - |  30.72 KB |
|               Coalesce |     15 |     100 |  1.978 ms | 0.0673 ms | 0.1963 ms |  1.01 |    0.11 |     - |     - |     - |  31.15 KB |
|                        |        |         |           |           |           |       |         |       |       |       |           |
|   **BaselineSimpleSelect** |     **15** |    **1000** |  **2.374 ms** | **0.0746 ms** | **0.2176 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |  **25.24 KB** |
| CteUnionWhereNotExists |     15 |    1000 | 92.503 ms | 2.9039 ms | 8.4709 ms | 39.32 |    5.21 |     - |     - |     - | 218.95 KB |
|    UnionWhereNotExists |     15 |    1000 |  2.509 ms | 0.0761 ms | 0.2220 ms |  1.07 |    0.14 |     - |     - |     - |  30.72 KB |
|               Coalesce |     15 |    1000 |  2.448 ms | 0.0686 ms | 0.2021 ms |  1.04 |    0.11 |     - |     - |     - |  31.15 KB |
