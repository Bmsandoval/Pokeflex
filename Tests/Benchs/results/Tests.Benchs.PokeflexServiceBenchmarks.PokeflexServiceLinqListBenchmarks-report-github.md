``` ini

BenchmarkDotNet=v0.12.1, OS=macOS 11.2.3 (20D91) [Darwin 20.3.0]
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.201
  [Host] : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT

Toolchain=InProcessEmitToolchain  InvocationCount=1  UnrollFactor=1  

```
|              Method |                                 Categories | Groups | Numbers | LimitAsPctNumbers |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 |  Allocated |
|-------------------- |------------------------------------------- |------- |-------- |------------------ |----------:|----------:|----------:|----------:|------:|--------:|------:|------:|------:|-----------:|
|   **SelectListOfBases** |             **All,Service,Pokeflex,Linq,List** |      **5** |     **100** |               **0.1** |  **2.616 ms** | **0.0618 ms** | **0.1784 ms** |  **2.607 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |   **36.66 KB** |
|            Proposal |             All,Service,Pokeflex,Linq,List |      5 |     100 |               0.1 |  2.778 ms | 0.0554 ms | 0.1553 ms |  2.771 ms |  1.07 |    0.10 |     - |     - |     - |   54.84 KB |
| UnionWhereNotExists | ActiveState,All,Service,Pokeflex,Linq,List |      5 |     100 |               0.1 |  2.856 ms | 0.0643 ms | 0.1865 ms |  2.864 ms |  1.10 |    0.11 |     - |     - |     - |   49.85 KB |
|     LiveServiceCode |             All,Service,Pokeflex,Linq,List |      5 |     100 |               0.1 |  3.176 ms | 0.0723 ms | 0.2038 ms |  3.151 ms |  1.22 |    0.11 |     - |     - |     - |   58.75 KB |
|            Coalesce |             All,Service,Pokeflex,Linq,List |      5 |     100 |               0.1 |  3.891 ms | 0.0773 ms | 0.1270 ms |  3.873 ms |  1.44 |    0.11 |     - |     - |     - |   61.71 KB |
|                     |                                            |        |         |                   |           |           |           |           |       |         |       |       |       |            |
|   **SelectListOfBases** |             **All,Service,Pokeflex,Linq,List** |      **5** |     **100** |               **0.3** |  **2.724 ms** | **0.0624 ms** | **0.1822 ms** |  **2.734 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |   **50.48 KB** |
|            Proposal |             All,Service,Pokeflex,Linq,List |      5 |     100 |               0.3 |  2.951 ms | 0.0739 ms | 0.2157 ms |  2.917 ms |  1.09 |    0.11 |     - |     - |     - |   68.41 KB |
| UnionWhereNotExists | ActiveState,All,Service,Pokeflex,Linq,List |      5 |     100 |               0.3 |  3.096 ms | 0.0614 ms | 0.1518 ms |  3.104 ms |  1.15 |    0.08 |     - |     - |     - |   63.94 KB |
|     LiveServiceCode |             All,Service,Pokeflex,Linq,List |      5 |     100 |               0.3 |  3.257 ms | 0.0759 ms | 0.2190 ms |  3.206 ms |  1.20 |    0.12 |     - |     - |     - |   72.32 KB |
|            Coalesce |             All,Service,Pokeflex,Linq,List |      5 |     100 |               0.3 |  6.128 ms | 0.1068 ms | 0.1725 ms |  6.118 ms |  2.35 |    0.14 |     - |     - |     - |   77.47 KB |
|                     |                                            |        |         |                   |           |           |           |           |       |         |       |       |       |            |
|   **SelectListOfBases** |             **All,Service,Pokeflex,Linq,List** |      **5** |    **1000** |               **0.1** |  **4.566 ms** | **0.0909 ms** | **0.2578 ms** |  **4.587 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |   **84.65 KB** |
|            Proposal |             All,Service,Pokeflex,Linq,List |      5 |    1000 |               0.1 |  4.544 ms | 0.1040 ms | 0.3050 ms |  4.449 ms |  0.99 |    0.09 |     - |     - |     - |  102.58 KB |
| UnionWhereNotExists | ActiveState,All,Service,Pokeflex,Linq,List |      5 |    1000 |               0.1 |  6.309 ms | 0.1241 ms | 0.2332 ms |  6.273 ms |  1.38 |    0.11 |     - |     - |     - |    98.1 KB |
|     LiveServiceCode |             All,Service,Pokeflex,Linq,List |      5 |    1000 |               0.1 |  6.377 ms | 0.1476 ms | 0.4329 ms |  6.450 ms |  1.40 |    0.13 |     - |     - |     - |  106.48 KB |
|            Coalesce |             All,Service,Pokeflex,Linq,List |      5 |    1000 |               0.1 |  4.628 ms | 0.1357 ms | 0.3957 ms |  4.612 ms |  1.01 |    0.10 |     - |     - |     - |  119.29 KB |
|                     |                                            |        |         |                   |           |           |           |           |       |         |       |       |       |            |
|   **SelectListOfBases** |             **All,Service,Pokeflex,Linq,List** |      **5** |    **1000** |               **0.3** |  **4.857 ms** | **0.1183 ms** | **0.3470 ms** |  **4.733 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |  **226.58 KB** |
|            Proposal |             All,Service,Pokeflex,Linq,List |      5 |    1000 |               0.3 |  5.096 ms | 0.1390 ms | 0.4099 ms |  5.011 ms |  1.05 |    0.11 |     - |     - |     - |  245.13 KB |
| UnionWhereNotExists | ActiveState,All,Service,Pokeflex,Linq,List |      5 |    1000 |               0.3 |  6.340 ms | 0.1263 ms | 0.3563 ms |  6.235 ms |  1.31 |    0.12 |     - |     - |     - |  239.83 KB |
|     LiveServiceCode |             All,Service,Pokeflex,Linq,List |      5 |    1000 |               0.3 |  6.747 ms | 0.1348 ms | 0.2466 ms |  6.710 ms |  1.39 |    0.11 |     - |     - |     - |  248.98 KB |
|            Coalesce |             All,Service,Pokeflex,Linq,List |      5 |    1000 |               0.3 |  6.610 ms | 0.1602 ms | 0.4673 ms |  6.457 ms |  1.37 |    0.14 |     - |     - |     - |  287.66 KB |
|                     |                                            |        |         |                   |           |           |           |           |       |         |       |       |       |            |
|   **SelectListOfBases** |             **All,Service,Pokeflex,Linq,List** |      **5** |    **5000** |               **0.1** | **12.175 ms** | **0.2411 ms** | **0.4159 ms** | **12.139 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |  **365.88 KB** |
|            Proposal |             All,Service,Pokeflex,Linq,List |      5 |    5000 |               0.1 | 12.220 ms | 0.2422 ms | 0.4947 ms | 12.122 ms |  1.00 |    0.05 |     - |     - |     - |  375.57 KB |
| UnionWhereNotExists | ActiveState,All,Service,Pokeflex,Linq,List |      5 |    5000 |               0.1 | 18.436 ms | 0.2248 ms | 0.2842 ms | 18.492 ms |  1.52 |    0.07 |     - |     - |     - |  364.36 KB |
|     LiveServiceCode |             All,Service,Pokeflex,Linq,List |      5 |    5000 |               0.1 | 19.553 ms | 0.3877 ms | 0.6371 ms | 19.520 ms |  1.61 |    0.07 |     - |     - |     - |  386.59 KB |
|            Coalesce |             All,Service,Pokeflex,Linq,List |      5 |    5000 |               0.1 | 10.994 ms | 0.2194 ms | 0.4175 ms | 10.959 ms |  0.91 |    0.06 |     - |     - |     - |   443.4 KB |
|                     |                                            |        |         |                   |           |           |           |           |       |         |       |       |       |            |
|   **SelectListOfBases** |             **All,Service,Pokeflex,Linq,List** |      **5** |    **5000** |               **0.3** | **18.251 ms** | **0.3629 ms** | **0.6635 ms** | **18.277 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** | **1011.79 KB** |
|            Proposal |             All,Service,Pokeflex,Linq,List |      5 |    5000 |               0.3 | 18.540 ms | 0.3647 ms | 0.6669 ms | 18.441 ms |  1.02 |    0.06 |     - |     - |     - | 1028.17 KB |
| UnionWhereNotExists | ActiveState,All,Service,Pokeflex,Linq,List |      5 |    5000 |               0.3 | 25.084 ms | 0.4841 ms | 0.8852 ms | 25.216 ms |  1.38 |    0.07 |     - |     - |     - | 1017.09 KB |
|     LiveServiceCode |             All,Service,Pokeflex,Linq,List |      5 |    5000 |               0.3 | 26.637 ms | 0.5328 ms | 0.8753 ms | 26.728 ms |  1.47 |    0.06 |     - |     - |     - | 1049.44 KB |
|            Coalesce |             All,Service,Pokeflex,Linq,List |      5 |    5000 |               0.3 | 25.619 ms | 0.5114 ms | 1.0095 ms | 25.482 ms |  1.41 |    0.07 |     - |     - |     - | 1247.49 KB |
|                     |                                            |        |         |                   |           |           |           |           |       |         |       |       |       |            |
|   **SelectListOfBases** |             **All,Service,Pokeflex,Linq,List** |     **15** |     **100** |               **0.1** |  **2.837 ms** | **0.0895 ms** | **0.2640 ms** |  **2.782 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |   **35.72 KB** |
|            Proposal |             All,Service,Pokeflex,Linq,List |     15 |     100 |               0.1 |  3.324 ms | 0.0816 ms | 0.2407 ms |  3.336 ms |  1.18 |    0.13 |     - |     - |     - |   54.67 KB |
| UnionWhereNotExists | ActiveState,All,Service,Pokeflex,Linq,List |     15 |     100 |               0.1 |  3.592 ms | 0.0931 ms | 0.2685 ms |  3.662 ms |  1.27 |    0.15 |     - |     - |     - |    50.2 KB |
|     LiveServiceCode |             All,Service,Pokeflex,Linq,List |     15 |     100 |               0.1 |  3.794 ms | 0.0967 ms | 0.2836 ms |  3.763 ms |  1.35 |    0.15 |     - |     - |     - |   58.58 KB |
|            Coalesce |             All,Service,Pokeflex,Linq,List |     15 |     100 |               0.1 |  5.749 ms | 0.1622 ms | 0.4758 ms |  5.667 ms |  2.04 |    0.24 |     - |     - |     - |   61.54 KB |
|                     |                                            |        |         |                   |           |           |           |           |       |         |       |       |       |            |
|   **SelectListOfBases** |             **All,Service,Pokeflex,Linq,List** |     **15** |     **100** |               **0.3** |  **3.354 ms** | **0.0755 ms** | **0.2227 ms** |  **3.348 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |   **50.48 KB** |
|            Proposal |             All,Service,Pokeflex,Linq,List |     15 |     100 |               0.3 |  3.414 ms | 0.0913 ms | 0.2663 ms |  3.373 ms |  1.02 |    0.10 |     - |     - |     - |   67.89 KB |
| UnionWhereNotExists | ActiveState,All,Service,Pokeflex,Linq,List |     15 |     100 |               0.3 |  3.796 ms | 0.0748 ms | 0.1808 ms |  3.788 ms |  1.14 |    0.11 |     - |     - |     - |   63.94 KB |
|     LiveServiceCode |             All,Service,Pokeflex,Linq,List |     15 |     100 |               0.3 |  4.136 ms | 0.0863 ms | 0.2518 ms |  4.159 ms |  1.24 |    0.11 |     - |     - |     - |   72.32 KB |
|            Coalesce |             All,Service,Pokeflex,Linq,List |     15 |     100 |               0.3 | 11.848 ms | 0.3020 ms | 0.8903 ms | 11.625 ms |  3.54 |    0.32 |     - |     - |     - |   77.47 KB |
|                     |                                            |        |         |                   |           |           |           |           |       |         |       |       |       |            |
|   **SelectListOfBases** |             **All,Service,Pokeflex,Linq,List** |     **15** |    **1000** |               **0.1** |  **6.789 ms** | **0.1348 ms** | **0.2139 ms** |  **6.796 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |   **84.65 KB** |
|            Proposal |             All,Service,Pokeflex,Linq,List |     15 |    1000 |               0.1 |  6.919 ms | 0.1358 ms | 0.2073 ms |  6.880 ms |  1.02 |    0.05 |     - |     - |     - |  102.58 KB |
| UnionWhereNotExists | ActiveState,All,Service,Pokeflex,Linq,List |     15 |    1000 |               0.1 | 10.336 ms | 0.2034 ms | 0.2498 ms | 10.366 ms |  1.53 |    0.06 |     - |     - |     - |    98.1 KB |
|     LiveServiceCode |             All,Service,Pokeflex,Linq,List |     15 |    1000 |               0.1 | 11.052 ms | 0.2139 ms | 0.2100 ms | 11.048 ms |  1.63 |    0.05 |     - |     - |     - |  106.48 KB |
|            Coalesce |             All,Service,Pokeflex,Linq,List |     15 |    1000 |               0.1 |  4.797 ms | 0.0941 ms | 0.1880 ms |  4.798 ms |  0.71 |    0.03 |     - |     - |     - |  119.29 KB |
|                     |                                            |        |         |                   |           |           |           |           |       |         |       |       |       |            |
|   **SelectListOfBases** |             **All,Service,Pokeflex,Linq,List** |     **15** |    **1000** |               **0.3** |  **7.238 ms** | **0.1448 ms** | **0.2296 ms** |  **7.247 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |  **227.45 KB** |
|            Proposal |             All,Service,Pokeflex,Linq,List |     15 |    1000 |               0.3 |  7.556 ms | 0.1408 ms | 0.1382 ms |  7.558 ms |  1.05 |    0.05 |     - |     - |     - |   244.3 KB |
| UnionWhereNotExists | ActiveState,All,Service,Pokeflex,Linq,List |     15 |    1000 |               0.3 | 11.286 ms | 0.2134 ms | 0.3128 ms | 11.242 ms |  1.56 |    0.07 |     - |     - |     - |  242.59 KB |
|     LiveServiceCode |             All,Service,Pokeflex,Linq,List |     15 |    1000 |               0.3 | 11.794 ms | 0.2179 ms | 0.3328 ms | 11.842 ms |  1.63 |    0.08 |     - |     - |     - |  248.98 KB |
|            Coalesce |             All,Service,Pokeflex,Linq,List |     15 |    1000 |               0.3 |  7.516 ms | 0.1714 ms | 0.4890 ms |  7.514 ms |  1.05 |    0.08 |     - |     - |     - |   288.3 KB |
|                     |                                            |        |         |                   |           |           |           |           |       |         |       |       |       |            |
|   **SelectListOfBases** |             **All,Service,Pokeflex,Linq,List** |     **15** |    **5000** |               **0.1** | **22.600 ms** | **0.5410 ms** | **1.5347 ms** | **22.274 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |  **358.16 KB** |
|            Proposal |             All,Service,Pokeflex,Linq,List |     15 |    5000 |               0.1 | 22.215 ms | 0.5294 ms | 1.5275 ms | 21.946 ms |  0.99 |    0.09 |     - |     - |     - |  384.46 KB |
| UnionWhereNotExists | ActiveState,All,Service,Pokeflex,Linq,List |     15 |    5000 |               0.1 | 36.780 ms | 0.7968 ms | 2.3243 ms | 36.482 ms |  1.63 |    0.16 |     - |     - |     - |  378.98 KB |
|     LiveServiceCode |             All,Service,Pokeflex,Linq,List |     15 |    5000 |               0.1 | 39.077 ms | 0.9134 ms | 2.6932 ms | 39.266 ms |  1.74 |    0.17 |     - |     - |     - |  386.59 KB |
|            Coalesce |             All,Service,Pokeflex,Linq,List |     15 |    5000 |               0.1 | 11.670 ms | 0.2391 ms | 0.7012 ms | 11.557 ms |  0.52 |    0.05 |     - |     - |     - |   442.2 KB |
|                     |                                            |        |         |                   |           |           |           |           |       |         |       |       |       |            |
|   **SelectListOfBases** |             **All,Service,Pokeflex,Linq,List** |     **15** |    **5000** |               **0.3** | **28.086 ms** | **0.5606 ms** | **1.4167 ms** | **27.879 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** | **1025.64 KB** |
|            Proposal |             All,Service,Pokeflex,Linq,List |     15 |    5000 |               0.3 | 28.407 ms | 0.8875 ms | 2.6169 ms | 28.451 ms |  1.01 |    0.10 |     - |     - |     - | 1039.73 KB |
| UnionWhereNotExists | ActiveState,All,Service,Pokeflex,Linq,List |     15 |    5000 |               0.3 | 42.980 ms | 1.0312 ms | 2.9752 ms | 42.694 ms |  1.52 |    0.12 |     - |     - |     - | 1010.33 KB |
|     LiveServiceCode |             All,Service,Pokeflex,Linq,List |     15 |    5000 |               0.3 | 46.499 ms | 1.2610 ms | 3.6783 ms | 45.888 ms |  1.66 |    0.16 |     - |     - |     - | 1039.93 KB |
|            Coalesce |             All,Service,Pokeflex,Linq,List |     15 |    5000 |               0.3 | 26.646 ms | 0.5938 ms | 1.7321 ms | 26.494 ms |  0.95 |    0.07 |     - |     - |     - | 1232.87 KB |
