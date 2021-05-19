``` ini

BenchmarkDotNet=v0.12.1, OS=macOS 11.2.3 (20D91) [Darwin 20.3.0]
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.201
  [Host] : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT

Toolchain=InProcessEmitToolchain  InvocationCount=1  UnrollFactor=1  

```
|                        Method | Groups | Numbers |     Mean |     Error |    StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------ |------- |-------- |---------:|----------:|----------:|------:|------:|------:|----------:|
|               **NeinExpressions** |      **5** |      **10** | **2.216 ms** | **0.0844 ms** | **0.2450 ms** |     **-** |     **-** |     **-** |  **19.38 KB** |
|         CachedNeinExpressions |      5 |      10 | 1.997 ms | 0.0572 ms | 0.1633 ms |     - |     - |     - |  18.75 KB |
|               ManualPredicate |      5 |      10 | 2.068 ms | 0.0663 ms | 0.1944 ms |     - |     - |     - |  16.84 KB |
|               LinqQuerySyntax |      5 |      10 | 1.998 ms | 0.0529 ms | 0.1527 ms |     - |     - |     - |  15.08 KB |
|              LinqMethodSyntax |      5 |      10 | 2.031 ms | 0.0700 ms | 0.2030 ms |     - |     - |     - |   15.2 KB |
| LinqMethodSyntaxShortcutFirst |      5 |      10 | 1.979 ms | 0.0615 ms | 0.1784 ms |     - |     - |     - |  14.16 KB |
|                BuiltPredicate |      5 |      10 | 2.169 ms | 0.0691 ms | 0.1973 ms |     - |     - |     - |  20.69 KB |
|           IQueryableExtension |      5 |      10 | 2.062 ms | 0.0624 ms | 0.1831 ms |     - |     - |     - |  15.51 KB |
|    ExtendedLambdaMethodSyntax |      5 |      10 | 2.074 ms | 0.0668 ms | 0.1969 ms |     - |     - |     - |  15.51 KB |
|               **NeinExpressions** |      **5** |     **100** | **2.032 ms** | **0.0616 ms** | **0.1776 ms** |     **-** |     **-** |     **-** |  **18.53 KB** |
|         CachedNeinExpressions |      5 |     100 | 2.053 ms | 0.0592 ms | 0.1679 ms |     - |     - |     - |   18.7 KB |
|               ManualPredicate |      5 |     100 | 2.024 ms | 0.0706 ms | 0.2049 ms |     - |     - |     - |     16 KB |
|               LinqQuerySyntax |      5 |     100 | 1.950 ms | 0.0698 ms | 0.2025 ms |     - |     - |     - |  15.02 KB |
|              LinqMethodSyntax |      5 |     100 | 2.160 ms | 0.0724 ms | 0.2099 ms |     - |     - |     - |  14.23 KB |
| LinqMethodSyntaxShortcutFirst |      5 |     100 | 2.116 ms | 0.0825 ms | 0.2433 ms |     - |     - |     - |  14.16 KB |
|                BuiltPredicate |      5 |     100 | 2.113 ms | 0.0618 ms | 0.1822 ms |     - |     - |     - |  20.56 KB |
|           IQueryableExtension |      5 |     100 | 2.067 ms | 0.0745 ms | 0.2137 ms |     - |     - |     - |  14.72 KB |
|    ExtendedLambdaMethodSyntax |      5 |     100 | 2.090 ms | 0.0534 ms | 0.1557 ms |     - |     - |     - |  15.51 KB |
|               **NeinExpressions** |      **5** |    **1000** | **2.269 ms** | **0.0855 ms** | **0.2520 ms** |     **-** |     **-** |     **-** |  **18.53 KB** |
|         CachedNeinExpressions |      5 |    1000 | 2.338 ms | 0.0872 ms | 0.2572 ms |     - |     - |     - |   18.7 KB |
|               ManualPredicate |      5 |    1000 | 2.296 ms | 0.0764 ms | 0.2251 ms |     - |     - |     - |  16.79 KB |
|               LinqQuerySyntax |      5 |    1000 | 2.133 ms | 0.0677 ms | 0.1941 ms |     - |     - |     - |  15.02 KB |
|              LinqMethodSyntax |      5 |    1000 | 2.194 ms | 0.0960 ms | 0.2829 ms |     - |     - |     - |  15.02 KB |
| LinqMethodSyntaxShortcutFirst |      5 |    1000 | 2.307 ms | 0.0796 ms | 0.2348 ms |     - |     - |     - |  14.16 KB |
|                BuiltPredicate |      5 |    1000 | 2.591 ms | 0.0932 ms | 0.2703 ms |     - |     - |     - |  20.36 KB |
|           IQueryableExtension |      5 |    1000 | 2.186 ms | 0.0788 ms | 0.2287 ms |     - |     - |     - |  14.72 KB |
|    ExtendedLambdaMethodSyntax |      5 |    1000 | 2.352 ms | 0.0695 ms | 0.2038 ms |     - |     - |     - |  15.51 KB |
|               **NeinExpressions** |     **10** |      **10** | **1.981 ms** | **0.0670 ms** | **0.1934 ms** |     **-** |     **-** |     **-** |  **18.53 KB** |
|         CachedNeinExpressions |     10 |      10 | 1.961 ms | 0.0655 ms | 0.1891 ms |     - |     - |     - |   18.7 KB |
|               ManualPredicate |     10 |      10 | 2.034 ms | 0.0787 ms | 0.2309 ms |     - |     - |     - |  17.48 KB |
|               LinqQuerySyntax |     10 |      10 | 1.992 ms | 0.0587 ms | 0.1683 ms |     - |     - |     - |  15.02 KB |
|              LinqMethodSyntax |     10 |      10 | 2.117 ms | 0.0955 ms | 0.2801 ms |     - |     - |     - |  15.02 KB |
| LinqMethodSyntaxShortcutFirst |     10 |      10 | 2.129 ms | 0.0586 ms | 0.1728 ms |     - |     - |     - |  14.16 KB |
|                BuiltPredicate |     10 |      10 | 2.214 ms | 0.0816 ms | 0.2394 ms |     - |     - |     - |  20.36 KB |
|           IQueryableExtension |     10 |      10 | 2.075 ms | 0.0851 ms | 0.2483 ms |     - |     - |     - |  15.51 KB |
|    ExtendedLambdaMethodSyntax |     10 |      10 | 2.113 ms | 0.0782 ms | 0.2293 ms |     - |     - |     - |  15.51 KB |
|               **NeinExpressions** |     **10** |     **100** | **2.107 ms** | **0.0926 ms** | **0.2717 ms** |     **-** |     **-** |     **-** |  **18.53 KB** |
|         CachedNeinExpressions |     10 |     100 | 2.188 ms | 0.0767 ms | 0.2237 ms |     - |     - |     - |   18.7 KB |
|               ManualPredicate |     10 |     100 | 2.204 ms | 0.0773 ms | 0.2267 ms |     - |     - |     - |  16.79 KB |
|               LinqQuerySyntax |     10 |     100 | 2.125 ms | 0.0745 ms | 0.2172 ms |     - |     - |     - |  15.02 KB |
|              LinqMethodSyntax |     10 |     100 | 2.065 ms | 0.0924 ms | 0.2694 ms |     - |     - |     - |  14.23 KB |
| LinqMethodSyntaxShortcutFirst |     10 |     100 | 2.095 ms | 0.0809 ms | 0.2386 ms |     - |     - |     - |  14.16 KB |
|                BuiltPredicate |     10 |     100 | 2.450 ms | 0.0796 ms | 0.2321 ms |     - |     - |     - |  20.36 KB |
|           IQueryableExtension |     10 |     100 | 2.164 ms | 0.0652 ms | 0.1882 ms |     - |     - |     - |  15.51 KB |
|    ExtendedLambdaMethodSyntax |     10 |     100 | 2.200 ms | 0.0875 ms | 0.2482 ms |     - |     - |     - |  15.51 KB |
|               **NeinExpressions** |     **10** |    **1000** | **2.390 ms** | **0.0687 ms** | **0.1972 ms** |     **-** |     **-** |     **-** |  **18.53 KB** |
|         CachedNeinExpressions |     10 |    1000 | 2.249 ms | 0.0905 ms | 0.2667 ms |     - |     - |     - |   18.7 KB |
|               ManualPredicate |     10 |    1000 | 2.533 ms | 0.0798 ms | 0.2315 ms |     - |     - |     - |  16.79 KB |
|               LinqQuerySyntax |     10 |    1000 | 2.469 ms | 0.0789 ms | 0.2288 ms |     - |     - |     - |  15.02 KB |
|              LinqMethodSyntax |     10 |    1000 | 2.172 ms | 0.0972 ms | 0.2835 ms |     - |     - |     - |  15.02 KB |
| LinqMethodSyntaxShortcutFirst |     10 |    1000 | 2.275 ms | 0.0707 ms | 0.2062 ms |     - |     - |     - |  14.16 KB |
|                BuiltPredicate |     10 |    1000 | 3.164 ms | 0.1679 ms | 0.4899 ms |     - |     - |     - |  19.12 KB |
|           IQueryableExtension |     10 |    1000 | 2.464 ms | 0.0707 ms | 0.2041 ms |     - |     - |     - |  15.51 KB |
|    ExtendedLambdaMethodSyntax |     10 |    1000 | 2.433 ms | 0.0824 ms | 0.2405 ms |     - |     - |     - |  15.51 KB |
|               **NeinExpressions** |     **15** |      **10** | **2.148 ms** | **0.0661 ms** | **0.1929 ms** |     **-** |     **-** |     **-** |  **18.53 KB** |
|         CachedNeinExpressions |     15 |      10 | 2.181 ms | 0.0733 ms | 0.2139 ms |     - |     - |     - |   18.7 KB |
|               ManualPredicate |     15 |      10 | 2.292 ms | 0.0978 ms | 0.2867 ms |     - |     - |     - |  16.79 KB |
|               LinqQuerySyntax |     15 |      10 | 1.997 ms | 0.0673 ms | 0.1940 ms |     - |     - |     - |  15.02 KB |
|              LinqMethodSyntax |     15 |      10 | 2.131 ms | 0.0689 ms | 0.1987 ms |     - |     - |     - |  15.02 KB |
| LinqMethodSyntaxShortcutFirst |     15 |      10 | 2.223 ms | 0.0898 ms | 0.2632 ms |     - |     - |     - |  14.16 KB |
|                BuiltPredicate |     15 |      10 | 2.129 ms | 0.0559 ms | 0.1614 ms |     - |     - |     - |  20.36 KB |
|           IQueryableExtension |     15 |      10 | 2.285 ms | 0.0689 ms | 0.1989 ms |     - |     - |     - |  15.51 KB |
|    ExtendedLambdaMethodSyntax |     15 |      10 | 2.129 ms | 0.0681 ms | 0.1975 ms |     - |     - |     - |  14.72 KB |
|               **NeinExpressions** |     **15** |     **100** | **2.184 ms** | **0.0848 ms** | **0.2487 ms** |     **-** |     **-** |     **-** |  **18.53 KB** |
|         CachedNeinExpressions |     15 |     100 | 2.181 ms | 0.1040 ms | 0.3033 ms |     - |     - |     - |   18.7 KB |
|               ManualPredicate |     15 |     100 | 2.289 ms | 0.0722 ms | 0.2095 ms |     - |     - |     - |  16.79 KB |
|               LinqQuerySyntax |     15 |     100 | 2.127 ms | 0.0693 ms | 0.2010 ms |     - |     - |     - |  15.02 KB |
|              LinqMethodSyntax |     15 |     100 | 2.287 ms | 0.0817 ms | 0.2396 ms |     - |     - |     - |  15.02 KB |
| LinqMethodSyntaxShortcutFirst |     15 |     100 | 2.284 ms | 0.0837 ms | 0.2415 ms |     - |     - |     - |  14.16 KB |
|                BuiltPredicate |     15 |     100 | 2.510 ms | 0.0806 ms | 0.2326 ms |     - |     - |     - |  20.36 KB |
|           IQueryableExtension |     15 |     100 | 2.208 ms | 0.0705 ms | 0.2034 ms |     - |     - |     - |  15.51 KB |
|    ExtendedLambdaMethodSyntax |     15 |     100 | 2.238 ms | 0.0704 ms | 0.1986 ms |     - |     - |     - |  15.51 KB |
|               **NeinExpressions** |     **15** |    **1000** | **2.551 ms** | **0.0891 ms** | **0.2572 ms** |     **-** |     **-** |     **-** |  **18.53 KB** |
|         CachedNeinExpressions |     15 |    1000 | 2.709 ms | 0.1133 ms | 0.3286 ms |     - |     - |     - |   18.7 KB |
|               ManualPredicate |     15 |    1000 | 2.480 ms | 0.0796 ms | 0.2323 ms |     - |     - |     - |  16.79 KB |
|               LinqQuerySyntax |     15 |    1000 | 2.331 ms | 0.0814 ms | 0.2361 ms |     - |     - |     - |  15.02 KB |
|              LinqMethodSyntax |     15 |    1000 | 2.430 ms | 0.0837 ms | 0.2429 ms |     - |     - |     - |  15.02 KB |
| LinqMethodSyntaxShortcutFirst |     15 |    1000 | 2.516 ms | 0.0873 ms | 0.2518 ms |     - |     - |     - |  14.16 KB |
|                BuiltPredicate |     15 |    1000 | 3.570 ms | 0.2796 ms | 0.8113 ms |     - |     - |     - |  19.12 KB |
|           IQueryableExtension |     15 |    1000 | 2.558 ms | 0.0983 ms | 0.2820 ms |     - |     - |     - |  15.51 KB |
|    ExtendedLambdaMethodSyntax |     15 |    1000 | 2.410 ms | 0.0775 ms | 0.2210 ms |     - |     - |     - |  15.51 KB |
