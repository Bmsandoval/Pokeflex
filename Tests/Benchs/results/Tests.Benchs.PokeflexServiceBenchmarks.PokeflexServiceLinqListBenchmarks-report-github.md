``` ini

BenchmarkDotNet=v0.12.1, OS=macOS 11.2.3 (20D91) [Darwin 20.3.0]
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.203
  [Host] : .NET Core 5.0.6 (CoreCLR 5.0.621.22011, CoreFX 5.0.621.22011), X64 RyuJIT

Toolchain=InProcessEmitToolchain  InvocationCount=1  UnrollFactor=1  
Categories=All,Service,Pokeflex,Linq,List  

```
|                            Method | Groups | Numbers | LimitAsPctNumbers |      Mean |     Error |     StdDev |    Median | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------------------- |------- |-------- |------------------ |----------:|----------:|-----------:|----------:|------:|--------:|------:|------:|------:|----------:|
|                 **SelectListOfBases** |      **5** |      **10** |               **0.1** |  **2.477 ms** | **0.0681 ms** |  **0.1921 ms** |  **2.469 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |  **27.69 KB** |
|               UnionWhereNotExists |      5 |      10 |               0.1 |  2.379 ms | 0.0559 ms |  0.1595 ms |  2.389 ms |  0.96 |    0.08 |     - |     - |     - |  52.18 KB |
|          SqlServerFuncInLinqQuery |      5 |      10 |               0.1 |  2.299 ms | 0.0677 ms |  0.1865 ms |  2.289 ms |  0.93 |    0.10 |     - |     - |     - |  57.14 KB |
| SqlServerFuncInLinqQueryCoalesced |      5 |      10 |               0.1 |  2.324 ms | 0.0557 ms |  0.1524 ms |  2.318 ms |  0.94 |    0.10 |     - |     - |     - |   62.2 KB |
|                          Coalesce |      5 |      10 |               0.1 |  2.634 ms | 0.0805 ms |  0.2271 ms |  2.597 ms |  1.07 |    0.14 |     - |     - |     - |  72.23 KB |
|                                   |        |         |                   |           |           |            |           |       |         |       |       |       |           |
|                 **SelectListOfBases** |      **5** |      **10** |               **0.3** |  **2.402 ms** | **0.0736 ms** |  **0.2101 ms** |  **2.350 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |   **29.6 KB** |
|               UnionWhereNotExists |      5 |      10 |               0.3 |  2.583 ms | 0.0581 ms |  0.1610 ms |  2.578 ms |  1.08 |    0.11 |     - |     - |     - |  55.64 KB |
|          SqlServerFuncInLinqQuery |      5 |      10 |               0.3 |  2.344 ms | 0.0636 ms |  0.1815 ms |  2.291 ms |  0.98 |    0.11 |     - |     - |     - |  57.48 KB |
| SqlServerFuncInLinqQueryCoalesced |      5 |      10 |               0.3 |  2.374 ms | 0.0596 ms |  0.1671 ms |  2.356 ms |  1.00 |    0.10 |     - |     - |     - |  62.55 KB |
|                          Coalesce |      5 |      10 |               0.3 |  2.824 ms | 0.0954 ms |  0.2722 ms |  2.757 ms |  1.18 |    0.14 |     - |     - |     - |  73.27 KB |
|                                   |        |         |                   |           |           |            |           |       |         |       |       |       |           |
|                 **SelectListOfBases** |      **5** |     **100** |               **0.1** |  **2.600 ms** | **0.0577 ms** |  **0.1656 ms** |  **2.581 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |  **32.63 KB** |
|               UnionWhereNotExists |      5 |     100 |               0.1 |  2.783 ms | 0.0587 ms |  0.1665 ms |  2.753 ms |  1.07 |    0.09 |     - |     - |     - |  58.67 KB |
|          SqlServerFuncInLinqQuery |      5 |     100 |               0.1 |  2.289 ms | 0.0898 ms |  0.2504 ms |  2.257 ms |  0.88 |    0.11 |     - |     - |     - |  57.48 KB |
| SqlServerFuncInLinqQueryCoalesced |      5 |     100 |               0.1 |  2.313 ms | 0.0624 ms |  0.1730 ms |  2.294 ms |  0.89 |    0.09 |     - |     - |     - |  62.55 KB |
|                          Coalesce |      5 |     100 |               0.1 |  8.316 ms | 2.9763 ms |  8.4914 ms |  3.537 ms |  3.18 |    3.22 |     - |     - |     - |  77.77 KB |
|                                   |        |         |                   |           |           |            |           |       |         |       |       |       |           |
|                 **SelectListOfBases** |      **5** |     **100** |               **0.3** |  **2.562 ms** | **0.0594 ms** |  **0.1677 ms** |  **2.542 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |  **43.58 KB** |
|               UnionWhereNotExists |      5 |     100 |               0.3 |  2.774 ms | 0.0554 ms |  0.1488 ms |  2.767 ms |  1.08 |    0.09 |     - |     - |     - |  69.41 KB |
|          SqlServerFuncInLinqQuery |      5 |     100 |               0.3 |  9.175 ms | 2.4402 ms |  7.1950 ms |  3.566 ms |  3.74 |    2.84 |     - |     - |     - |  57.48 KB |
| SqlServerFuncInLinqQueryCoalesced |      5 |     100 |               0.3 | 10.413 ms | 2.6129 ms |  7.7041 ms |  6.380 ms |  4.09 |    3.03 |     - |     - |     - |  62.55 KB |
|                          Coalesce |      5 |     100 |               0.3 | 12.799 ms | 3.9955 ms | 11.7809 ms |  4.041 ms |  4.95 |    4.60 |     - |     - |     - |  90.23 KB |
|                                   |        |         |                   |           |           |            |           |       |         |       |       |       |           |
|                 **SelectListOfBases** |      **5** |    **1000** |               **0.1** |  **4.087 ms** | **0.1392 ms** |  **0.4038 ms** |  **4.033 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |  **82.43 KB** |
|               UnionWhereNotExists |      5 |    1000 |               0.1 | 13.240 ms | 2.5768 ms |  7.5977 ms |  7.353 ms |  3.30 |    1.94 |     - |     - |     - |  107.6 KB |
|          SqlServerFuncInLinqQuery |      5 |    1000 |               0.1 | 11.130 ms | 2.4447 ms |  7.2084 ms |  6.052 ms |  2.82 |    1.87 |     - |     - |     - |  57.48 KB |
| SqlServerFuncInLinqQueryCoalesced |      5 |    1000 |               0.1 |  7.737 ms | 1.3942 ms |  4.1108 ms |  5.822 ms |  1.93 |    1.08 |     - |     - |     - |  62.55 KB |
|                          Coalesce |      5 |    1000 |               0.1 | 16.146 ms | 3.8372 ms | 11.3139 ms |  8.817 ms |  4.01 |    2.91 |     - |     - |     - | 134.43 KB |
|                                   |        |         |                   |           |           |            |           |       |         |       |       |       |           |
|                 **SelectListOfBases** |      **5** |    **1000** |               **0.3** |  **4.165 ms** | **0.0898 ms** |  **0.2546 ms** |  **4.113 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** | **220.74 KB** |
|               UnionWhereNotExists |      5 |    1000 |               0.3 | 14.322 ms | 2.6847 ms |  7.9159 ms | 14.367 ms |  3.47 |    1.95 |     - |     - |     - | 245.96 KB |
|          SqlServerFuncInLinqQuery |      5 |    1000 |               0.3 | 15.315 ms | 3.1381 ms |  9.2526 ms | 10.888 ms |  3.68 |    2.26 |     - |     - |     - |  57.48 KB |
| SqlServerFuncInLinqQueryCoalesced |      5 |    1000 |               0.3 | 11.863 ms | 2.4506 ms |  7.2257 ms | 10.678 ms |  2.91 |    1.77 |     - |     - |     - |  62.55 KB |
|                          Coalesce |      5 |    1000 |               0.3 | 17.566 ms | 3.4314 ms | 10.1175 ms | 22.741 ms |  4.30 |    2.47 |     - |     - |     - | 284.11 KB |
|                                   |        |         |                   |           |           |            |           |       |         |       |       |       |           |
|                 **SelectListOfBases** |     **15** |      **10** |               **0.1** |  **2.519 ms** | **0.0824 ms** |  **0.2284 ms** |  **2.493 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |  **27.69 KB** |
|               UnionWhereNotExists |     15 |      10 |               0.1 |  2.682 ms | 0.0844 ms |  0.2339 ms |  2.612 ms |  1.07 |    0.12 |     - |     - |     - |  53.73 KB |
|          SqlServerFuncInLinqQuery |     15 |      10 |               0.1 |  2.311 ms | 0.0617 ms |  0.1636 ms |  2.292 ms |  0.92 |    0.10 |     - |     - |     - |  57.48 KB |
| SqlServerFuncInLinqQueryCoalesced |     15 |      10 |               0.1 |  2.381 ms | 0.0960 ms |  0.2753 ms |  2.323 ms |  0.95 |    0.13 |     - |     - |     - |  62.55 KB |
|                          Coalesce |     15 |      10 |               0.1 |  2.808 ms | 0.0693 ms |  0.1919 ms |  2.780 ms |  1.12 |    0.13 |     - |     - |     - |  72.05 KB |
|                                   |        |         |                   |           |           |            |           |       |         |       |       |       |           |
|                 **SelectListOfBases** |     **15** |      **10** |               **0.3** |  **2.498 ms** | **0.0721 ms** |  **0.2068 ms** |  **2.477 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |   **29.6 KB** |
|               UnionWhereNotExists |     15 |      10 |               0.3 |  2.703 ms | 0.0693 ms |  0.1933 ms |  2.676 ms |  1.08 |    0.11 |     - |     - |     - |  54.77 KB |
|          SqlServerFuncInLinqQuery |     15 |      10 |               0.3 |  2.291 ms | 0.0624 ms |  0.1760 ms |  2.285 ms |  0.92 |    0.11 |     - |     - |     - |  57.48 KB |
| SqlServerFuncInLinqQueryCoalesced |     15 |      10 |               0.3 |  2.444 ms | 0.0694 ms |  0.1912 ms |  2.429 ms |  0.98 |    0.09 |     - |     - |     - |  62.55 KB |
|                          Coalesce |     15 |      10 |               0.3 |  2.807 ms | 0.0648 ms |  0.1786 ms |  2.795 ms |  1.13 |    0.11 |     - |     - |     - |  73.27 KB |
|                                   |        |         |                   |           |           |            |           |       |         |       |       |       |           |
|                 **SelectListOfBases** |     **15** |     **100** |               **0.1** |  **2.902 ms** | **0.1016 ms** |  **0.2850 ms** |  **2.821 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |  **32.63 KB** |
|               UnionWhereNotExists |     15 |     100 |               0.1 |  3.408 ms | 0.1280 ms |  0.3609 ms |  3.326 ms |  1.19 |    0.15 |     - |     - |     - |  58.67 KB |
|          SqlServerFuncInLinqQuery |     15 |     100 |               0.1 |  7.465 ms | 1.8736 ms |  5.4950 ms |  3.904 ms |  2.63 |    2.00 |     - |     - |     - |  57.48 KB |
| SqlServerFuncInLinqQueryCoalesced |     15 |     100 |               0.1 |  8.886 ms | 2.1511 ms |  6.3425 ms |  4.328 ms |  3.03 |    2.24 |     - |     - |     - |  62.55 KB |
|                          Coalesce |     15 |     100 |               0.1 | 14.650 ms | 3.9002 ms | 11.5000 ms |  6.363 ms |  5.08 |    4.10 |     - |     - |     - |  78.63 KB |
|                                   |        |         |                   |           |           |            |           |       |         |       |       |       |           |
|                 **SelectListOfBases** |     **15** |     **100** |               **0.3** |  **2.954 ms** | **0.0873 ms** |  **0.2449 ms** |  **2.914 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |  **44.24 KB** |
|               UnionWhereNotExists |     15 |     100 |               0.3 |  3.577 ms | 0.1263 ms |  0.3564 ms |  3.492 ms |  1.22 |    0.15 |     - |     - |     - |  69.41 KB |
|          SqlServerFuncInLinqQuery |     15 |     100 |               0.3 |  8.608 ms | 1.6780 ms |  4.9477 ms |  8.692 ms |  2.93 |    1.70 |     - |     - |     - |  57.48 KB |
| SqlServerFuncInLinqQueryCoalesced |     15 |     100 |               0.3 | 10.112 ms | 2.5600 ms |  7.5481 ms |  4.408 ms |  3.39 |    2.56 |     - |     - |     - |  62.55 KB |
|                          Coalesce |     15 |     100 |               0.3 | 13.983 ms | 3.7979 ms | 11.1982 ms |  5.604 ms |  4.80 |    3.91 |     - |     - |     - |  90.23 KB |
|                                   |        |         |                   |           |           |            |           |       |         |       |       |       |           |
|                 **SelectListOfBases** |     **15** |    **1000** |               **0.1** |  **6.546 ms** | **0.1291 ms** |  **0.2860 ms** |  **6.560 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |  **81.56 KB** |
|               UnionWhereNotExists |     15 |    1000 |               0.1 | 17.727 ms | 2.2851 ms |  6.7378 ms | 19.450 ms |  2.81 |    1.20 |     - |     - |     - |  107.6 KB |
|          SqlServerFuncInLinqQuery |     15 |    1000 |               0.1 | 19.119 ms | 2.6557 ms |  7.8305 ms | 20.833 ms |  2.85 |    1.26 |     - |     - |     - |  57.48 KB |
| SqlServerFuncInLinqQueryCoalesced |     15 |    1000 |               0.1 | 13.912 ms | 2.4258 ms |  7.1525 ms | 11.440 ms |  2.30 |    1.21 |     - |     - |     - |  62.55 KB |
|                          Coalesce |     15 |    1000 |               0.1 | 20.367 ms | 3.7473 ms | 11.0491 ms | 24.285 ms |  3.10 |    1.77 |     - |     - |     - | 134.43 KB |
|                                   |        |         |                   |           |           |            |           |       |         |       |       |       |           |
|                 **SelectListOfBases** |     **15** |    **1000** |               **0.3** |  **7.242 ms** | **0.1508 ms** |  **0.4153 ms** |  **7.275 ms** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** | **213.18 KB** |
|               UnionWhereNotExists |     15 |    1000 |               0.3 | 18.115 ms | 2.3848 ms |  7.0316 ms | 19.837 ms |  2.55 |    1.00 |     - |     - |     - | 246.83 KB |
|          SqlServerFuncInLinqQuery |     15 |    1000 |               0.3 | 17.822 ms | 2.2977 ms |  6.7748 ms | 19.800 ms |  2.47 |    0.95 |     - |     - |     - |  57.48 KB |
| SqlServerFuncInLinqQueryCoalesced |     15 |    1000 |               0.3 | 13.588 ms | 2.2290 ms |  6.5722 ms | 14.955 ms |  1.90 |    0.90 |     - |     - |     - |  62.55 KB |
|                          Coalesce |     15 |    1000 |               0.3 | 22.257 ms | 3.8570 ms | 11.3725 ms | 25.519 ms |  3.11 |    1.61 |     - |     - |     - | 282.55 KB |
