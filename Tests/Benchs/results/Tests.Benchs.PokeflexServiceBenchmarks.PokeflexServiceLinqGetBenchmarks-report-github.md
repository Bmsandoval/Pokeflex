``` ini

BenchmarkDotNet=v0.12.1, OS=macOS 11.2.3 (20D91) [Darwin 20.3.0]
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.203
  [Host] : .NET Core 5.0.6 (CoreCLR 5.0.621.22011, CoreFX 5.0.621.22011), X64 RyuJIT

Toolchain=InProcessEmitToolchain  InvocationCount=1  UnrollFactor=1  
Categories=All,Service,Pokeflex,Linq,Get  

```
|                  Method | Groups | Numbers |     Mean |     Error |    StdDev |   Median | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------ |------- |-------- |---------:|----------:|----------:|---------:|------:|------:|------:|----------:|
|          **BasicLinqQuery** |      **5** |      **10** | **2.288 ms** | **0.0687 ms** | **0.2004 ms** | **2.279 ms** |     **-** |     **-** |     **-** |  **24.84 KB** |
|         BasicLinqMethod |      5 |      10 | 2.069 ms | 0.0533 ms | 0.1546 ms | 2.063 ms |     - |     - |     - |  23.34 KB |
|                Proposal |      5 |      10 | 2.175 ms | 0.0744 ms | 0.2183 ms | 2.143 ms |     - |     - |     - |  48.52 KB |
|     UnionWhereNotExists |      5 |      10 | 2.133 ms | 0.0568 ms | 0.1638 ms | 2.120 ms |     - |     - |     - |  43.08 KB |
|                Coalesce |      5 |      10 | 2.432 ms | 0.0768 ms | 0.2215 ms | 2.400 ms |     - |     - |     - |  81.23 KB |
|    SequentialWhereNotIn |      5 |      10 | 4.080 ms | 0.1022 ms | 0.2982 ms | 4.060 ms |     - |     - |     - |  51.16 KB |
|     SequentialWorstCase |      5 |      10 | 3.513 ms | 0.1111 ms | 0.3242 ms | 3.473 ms |     - |     - |     - |  43.41 KB |
| SqlServerStoredFunction |      5 |      10 | 2.071 ms | 0.0625 ms | 0.1783 ms | 2.063 ms |     - |     - |     - |  18.63 KB |
|          **BasicLinqQuery** |      **5** |     **100** | **2.147 ms** | **0.0598 ms** | **0.1726 ms** | **2.157 ms** |     **-** |     **-** |     **-** |  **23.34 KB** |
|         BasicLinqMethod |      5 |     100 | 2.118 ms | 0.0657 ms | 0.1907 ms | 2.137 ms |     - |     - |     - |  23.34 KB |
|                Proposal |      5 |     100 | 2.254 ms | 0.0653 ms | 0.1904 ms | 2.249 ms |     - |     - |     - |  49.36 KB |
|     UnionWhereNotExists |      5 |     100 | 2.147 ms | 0.0543 ms | 0.1577 ms | 2.117 ms |     - |     - |     - |  43.92 KB |
|                Coalesce |      5 |     100 | 2.487 ms | 0.0779 ms | 0.2285 ms | 2.470 ms |     - |     - |     - |  81.05 KB |
|    SequentialWhereNotIn |      5 |     100 | 4.104 ms | 0.1381 ms | 0.3986 ms | 4.109 ms |     - |     - |     - |  51.16 KB |
|     SequentialWorstCase |      5 |     100 | 3.631 ms | 0.1077 ms | 0.3107 ms | 3.624 ms |     - |     - |     - |  43.41 KB |
| SqlServerStoredFunction |      5 |     100 | 2.284 ms | 0.0912 ms | 0.2646 ms | 2.206 ms |     - |     - |     - |  18.83 KB |
|          **BasicLinqQuery** |      **5** |    **1000** | **2.401 ms** | **0.0715 ms** | **0.2108 ms** | **2.402 ms** |     **-** |     **-** |     **-** |  **24.18 KB** |
|         BasicLinqMethod |      5 |    1000 | 2.188 ms | 0.0666 ms | 0.1942 ms | 2.168 ms |     - |     - |     - |  23.34 KB |
|                Proposal |      5 |    1000 | 2.559 ms | 0.0714 ms | 0.2073 ms | 2.578 ms |     - |     - |     - |  48.52 KB |
|     UnionWhereNotExists |      5 |    1000 | 2.615 ms | 0.0594 ms | 0.1750 ms | 2.635 ms |     - |     - |     - |  43.08 KB |
|                Coalesce |      5 |    1000 | 2.615 ms | 0.0921 ms | 0.2701 ms | 2.609 ms |     - |     - |     - |  81.05 KB |
|    SequentialWhereNotIn |      5 |    1000 | 4.291 ms | 0.1361 ms | 0.3949 ms | 4.240 ms |     - |     - |     - |  51.69 KB |
|     SequentialWorstCase |      5 |    1000 | 3.961 ms | 0.1305 ms | 0.3829 ms | 3.910 ms |     - |     - |     - |  43.41 KB |
| SqlServerStoredFunction |      5 |    1000 | 2.155 ms | 0.0745 ms | 0.2149 ms | 2.130 ms |     - |     - |     - |  18.63 KB |
|          **BasicLinqQuery** |     **10** |      **10** | **2.169 ms** | **0.0518 ms** | **0.1528 ms** | **2.177 ms** |     **-** |     **-** |     **-** |  **23.34 KB** |
|         BasicLinqMethod |     10 |      10 | 2.098 ms | 0.0569 ms | 0.1613 ms | 2.120 ms |     - |     - |     - |  23.34 KB |
|                Proposal |     10 |      10 | 2.251 ms | 0.0619 ms | 0.1785 ms | 2.258 ms |     - |     - |     - |  48.52 KB |
|     UnionWhereNotExists |     10 |      10 | 2.195 ms | 0.0489 ms | 0.1420 ms | 2.193 ms |     - |     - |     - |  43.08 KB |
|                Coalesce |     10 |      10 | 2.368 ms | 0.0618 ms | 0.1784 ms | 2.356 ms |     - |     - |     - |  81.05 KB |
|    SequentialWhereNotIn |     10 |      10 | 4.118 ms | 0.1012 ms | 0.2889 ms | 4.129 ms |     - |     - |     - |  51.85 KB |
|     SequentialWorstCase |     10 |      10 | 4.100 ms | 0.1070 ms | 0.3138 ms | 4.089 ms |     - |     - |     - |  43.41 KB |
| SqlServerStoredFunction |     10 |      10 | 2.187 ms | 0.0887 ms | 0.2603 ms | 2.173 ms |     - |     - |     - |  18.63 KB |
|          **BasicLinqQuery** |     **10** |     **100** | **2.232 ms** | **0.0819 ms** | **0.2376 ms** | **2.185 ms** |     **-** |     **-** |     **-** |  **23.34 KB** |
|         BasicLinqMethod |     10 |     100 | 2.173 ms | 0.0600 ms | 0.1750 ms | 2.138 ms |     - |     - |     - |  23.34 KB |
|                Proposal |     10 |     100 | 2.428 ms | 0.0832 ms | 0.2453 ms | 2.371 ms |     - |     - |     - |  48.52 KB |
|     UnionWhereNotExists |     10 |     100 | 2.305 ms | 0.0878 ms | 0.2590 ms | 2.277 ms |     - |     - |     - |  43.08 KB |
|                Coalesce |     10 |     100 | 2.618 ms | 0.1012 ms | 0.2935 ms | 2.581 ms |     - |     - |     - |  81.05 KB |
|    SequentialWhereNotIn |     10 |     100 | 3.992 ms | 0.0950 ms | 0.2756 ms | 4.000 ms |     - |     - |     - |  51.16 KB |
|     SequentialWorstCase |     10 |     100 | 3.795 ms | 0.0938 ms | 0.2735 ms | 3.774 ms |     - |     - |     - |  43.41 KB |
| SqlServerStoredFunction |     10 |     100 | 2.157 ms | 0.0682 ms | 0.1979 ms | 2.153 ms |     - |     - |     - |  18.63 KB |
|          **BasicLinqQuery** |     **10** |    **1000** | **2.413 ms** | **0.0804 ms** | **0.2370 ms** | **2.444 ms** |     **-** |     **-** |     **-** |  **23.34 KB** |
|         BasicLinqMethod |     10 |    1000 | 2.502 ms | 0.0663 ms | 0.1934 ms | 2.499 ms |     - |     - |     - |  23.34 KB |
|                Proposal |     10 |    1000 | 2.740 ms | 0.0695 ms | 0.2050 ms | 2.732 ms |     - |     - |     - |  48.52 KB |
|     UnionWhereNotExists |     10 |    1000 | 2.616 ms | 0.0756 ms | 0.2217 ms | 2.621 ms |     - |     - |     - |  43.08 KB |
|                Coalesce |     10 |    1000 | 2.733 ms | 0.0996 ms | 0.2921 ms | 2.707 ms |     - |     - |     - |   81.9 KB |
|    SequentialWhereNotIn |     10 |    1000 | 4.639 ms | 0.1268 ms | 0.3739 ms | 4.649 ms |     - |     - |     - |  51.16 KB |
|     SequentialWorstCase |     10 |    1000 | 4.224 ms | 0.1180 ms | 0.3423 ms | 4.218 ms |     - |     - |     - |  43.41 KB |
| SqlServerStoredFunction |     10 |    1000 | 2.521 ms | 0.0733 ms | 0.2162 ms | 2.505 ms |     - |     - |     - |  18.63 KB |
|          **BasicLinqQuery** |     **15** |      **10** | **2.243 ms** | **0.1039 ms** | **0.3047 ms** | **2.209 ms** |     **-** |     **-** |     **-** |  **23.34 KB** |
|         BasicLinqMethod |     15 |      10 | 2.171 ms | 0.0742 ms | 0.2177 ms | 2.118 ms |     - |     - |     - |  23.34 KB |
|                Proposal |     15 |      10 | 2.247 ms | 0.0780 ms | 0.2224 ms | 2.233 ms |     - |     - |     - |  48.52 KB |
|     UnionWhereNotExists |     15 |      10 | 2.191 ms | 0.0646 ms | 0.1894 ms | 2.197 ms |     - |     - |     - |  43.08 KB |
|                Coalesce |     15 |      10 | 2.558 ms | 0.0797 ms | 0.2313 ms | 2.505 ms |     - |     - |     - |  81.05 KB |
|    SequentialWhereNotIn |     15 |      10 | 3.903 ms | 0.0987 ms | 0.2833 ms | 3.892 ms |     - |     - |     - |   50.3 KB |
|     SequentialWorstCase |     15 |      10 | 3.677 ms | 0.0969 ms | 0.2718 ms | 3.659 ms |     - |     - |     - |  43.41 KB |
| SqlServerStoredFunction |     15 |      10 | 2.110 ms | 0.0786 ms | 0.2281 ms | 2.066 ms |     - |     - |     - |  18.63 KB |
|          **BasicLinqQuery** |     **15** |     **100** | **2.178 ms** | **0.0770 ms** | **0.2257 ms** | **2.157 ms** |     **-** |     **-** |     **-** |  **24.18 KB** |
|         BasicLinqMethod |     15 |     100 | 2.275 ms | 0.0815 ms | 0.2390 ms | 2.286 ms |     - |     - |     - |  23.34 KB |
|                Proposal |     15 |     100 | 2.291 ms | 0.0680 ms | 0.1994 ms | 2.287 ms |     - |     - |     - |  48.52 KB |
|     UnionWhereNotExists |     15 |     100 | 2.277 ms | 0.0902 ms | 0.2616 ms | 2.253 ms |     - |     - |     - |  43.08 KB |
|                Coalesce |     15 |     100 | 2.571 ms | 0.0874 ms | 0.2562 ms | 2.554 ms |     - |     - |     - |  81.05 KB |
|    SequentialWhereNotIn |     15 |     100 | 3.932 ms | 0.0874 ms | 0.2480 ms | 3.909 ms |     - |     - |     - |   50.3 KB |
|     SequentialWorstCase |     15 |     100 | 3.702 ms | 0.1165 ms | 0.3381 ms | 3.636 ms |     - |     - |     - |  43.41 KB |
| SqlServerStoredFunction |     15 |     100 | 2.141 ms | 0.0597 ms | 0.1712 ms | 2.149 ms |     - |     - |     - |  18.63 KB |
|          **BasicLinqQuery** |     **15** |    **1000** | **2.538 ms** | **0.0665 ms** | **0.1951 ms** | **2.516 ms** |     **-** |     **-** |     **-** |  **23.34 KB** |
|         BasicLinqMethod |     15 |    1000 | 2.536 ms | 0.0718 ms | 0.2083 ms | 2.500 ms |     - |     - |     - |  23.34 KB |
|                Proposal |     15 |    1000 | 2.799 ms | 0.1008 ms | 0.2843 ms | 2.763 ms |     - |     - |     - |  48.52 KB |
|     UnionWhereNotExists |     15 |    1000 | 2.659 ms | 0.0903 ms | 0.2532 ms | 2.630 ms |     - |     - |     - |  43.08 KB |
|                Coalesce |     15 |    1000 | 2.872 ms | 0.1061 ms | 0.3111 ms | 2.866 ms |     - |     - |     - |   81.9 KB |
|    SequentialWhereNotIn |     15 |    1000 | 5.036 ms | 0.1208 ms | 0.3542 ms | 5.019 ms |     - |     - |     - |   50.3 KB |
|     SequentialWorstCase |     15 |    1000 | 4.337 ms | 0.1272 ms | 0.3608 ms | 4.351 ms |     - |     - |     - |  43.41 KB |
| SqlServerStoredFunction |     15 |    1000 | 2.431 ms | 0.0991 ms | 0.2922 ms | 2.418 ms |     - |     - |     - |  18.63 KB |
