``` ini

BenchmarkDotNet=v0.12.1, OS=macOS 11.2.3 (20D91) [Darwin 20.3.0]
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.201
  [Host] : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT

Toolchain=InProcessEmitToolchain  InvocationCount=1  IterationCount=1  
LaunchCount=1  UnrollFactor=1  WarmupCount=1  

```
|                        Method | Groups | Numbers |     Mean | Error | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------ |------- |-------- |---------:|------:|------:|------:|------:|----------:|
|               **NeinExpressions** |      **5** |      **10** | **184.8 μs** |    **NA** |     **-** |     **-** |     **-** |  **13.73 KB** |
|         CachedNeinExpressions |      5 |      10 | 169.7 μs |    NA |     - |     - |     - |   13.9 KB |
|               ManualPredicate |      5 |      10 | 214.8 μs |    NA |     - |     - |     - |  12.05 KB |
|               LinqQuerySyntax |      5 |      10 | 141.0 μs |    NA |     - |     - |     - |  10.23 KB |
|              LinqMethodSyntax |      5 |      10 | 133.8 μs |    NA |     - |     - |     - |  12.31 KB |
| LinqMethodSyntaxShortcutFirst |      5 |      10 | 134.0 μs |    NA |     - |     - |     - |   9.18 KB |
|                BuiltPredicate |      5 |      10 | 136.7 μs |    NA |     - |     - |     - |  17.37 KB |
|           IQueryableExtension |      5 |      10 | 159.8 μs |    NA |     - |     - |     - |  10.81 KB |
|    ExtendedLambdaMethodSyntax |      5 |      10 | 176.4 μs |    NA |     - |     - |     - |  10.77 KB |
|               **NeinExpressions** |      **5** |     **100** | **240.5 μs** |    **NA** |     **-** |     **-** |     **-** |  **13.79 KB** |
|         CachedNeinExpressions |      5 |     100 | 150.0 μs |    NA |     - |     - |     - |  13.71 KB |
|               ManualPredicate |      5 |     100 | 168.9 μs |    NA |     - |     - |     - |   11.8 KB |
|               LinqQuerySyntax |      5 |     100 | 158.6 μs |    NA |     - |     - |     - |  10.55 KB |
|              LinqMethodSyntax |      5 |     100 | 288.7 μs |    NA |     - |     - |     - |  10.32 KB |
| LinqMethodSyntaxShortcutFirst |      5 |     100 | 147.1 μs |    NA |     - |     - |     - |   9.23 KB |
|                BuiltPredicate |      5 |     100 | 169.6 μs |    NA |     - |     - |     - |  11.95 KB |
|           IQueryableExtension |      5 |     100 | 130.7 μs |    NA |     - |     - |     - |  10.77 KB |
|    ExtendedLambdaMethodSyntax |      5 |     100 | 186.0 μs |    NA |     - |     - |     - |  10.77 KB |
|               **NeinExpressions** |      **5** |    **1000** | **225.4 μs** |    **NA** |     **-** |     **-** |     **-** |  **13.79 KB** |
|         CachedNeinExpressions |      5 |    1000 | 217.8 μs |    NA |     - |     - |     - |  13.95 KB |
|               ManualPredicate |      5 |    1000 | 183.8 μs |    NA |     - |     - |     - |  12.15 KB |
|               LinqQuerySyntax |      5 |    1000 | 188.5 μs |    NA |     - |     - |     - |  10.28 KB |
|              LinqMethodSyntax |      5 |    1000 | 185.0 μs |    NA |     - |     - |     - |  10.28 KB |
| LinqMethodSyntaxShortcutFirst |      5 |    1000 | 147.2 μs |    NA |     - |     - |     - |    9.2 KB |
|                BuiltPredicate |      5 |    1000 | 180.9 μs |    NA |     - |     - |     - |  11.95 KB |
|           IQueryableExtension |      5 |    1000 | 200.6 μs |    NA |     - |     - |     - |  10.77 KB |
|    ExtendedLambdaMethodSyntax |      5 |    1000 | 243.5 μs |    NA |     - |     - |     - |  10.77 KB |
|               **NeinExpressions** |     **10** |      **10** | **221.2 μs** |    **NA** |     **-** |     **-** |     **-** |  **13.79 KB** |
|         CachedNeinExpressions |     10 |      10 | 168.2 μs |    NA |     - |     - |     - |  14.09 KB |
|               ManualPredicate |     10 |      10 | 165.3 μs |    NA |     - |     - |     - |  12.05 KB |
|               LinqQuerySyntax |     10 |      10 | 139.5 μs |    NA |     - |     - |     - |  10.28 KB |
|              LinqMethodSyntax |     10 |      10 | 154.2 μs |    NA |     - |     - |     - |  10.28 KB |
| LinqMethodSyntaxShortcutFirst |     10 |      10 | 218.2 μs |    NA |     - |     - |     - |    9.2 KB |
|                BuiltPredicate |     10 |      10 | 157.4 μs |    NA |     - |     - |     - |  11.95 KB |
|           IQueryableExtension |     10 |      10 | 138.2 μs |    NA |     - |     - |     - |  10.77 KB |
|    ExtendedLambdaMethodSyntax |     10 |      10 | 141.6 μs |    NA |     - |     - |     - |  10.77 KB |
|               **NeinExpressions** |     **10** |     **100** | **143.9 μs** |    **NA** |     **-** |     **-** |     **-** |  **13.79 KB** |
|         CachedNeinExpressions |     10 |     100 | 190.1 μs |    NA |     - |     - |     - |  13.95 KB |
|               ManualPredicate |     10 |     100 | 157.9 μs |    NA |     - |     - |     - |   11.9 KB |
|               LinqQuerySyntax |     10 |     100 | 143.8 μs |    NA |     - |     - |     - |  10.28 KB |
|              LinqMethodSyntax |     10 |     100 | 133.5 μs |    NA |     - |     - |     - |  10.28 KB |
| LinqMethodSyntaxShortcutFirst |     10 |     100 | 124.0 μs |    NA |     - |     - |     - |   8.95 KB |
|                BuiltPredicate |     10 |     100 | 278.6 μs |    NA |     - |     - |     - |   12.1 KB |
|           IQueryableExtension |     10 |     100 | 152.5 μs |    NA |     - |     - |     - |  10.77 KB |
|    ExtendedLambdaMethodSyntax |     10 |     100 | 154.8 μs |    NA |     - |     - |     - |  10.52 KB |
|               **NeinExpressions** |     **10** |    **1000** | **218.8 μs** |    **NA** |     **-** |     **-** |     **-** |  **13.79 KB** |
|         CachedNeinExpressions |     10 |    1000 | 225.9 μs |    NA |     - |     - |     - |  13.95 KB |
|               ManualPredicate |     10 |    1000 | 489.8 μs |    NA |     - |     - |     - |  12.05 KB |
|               LinqQuerySyntax |     10 |    1000 | 203.0 μs |    NA |     - |     - |     - |  10.28 KB |
|              LinqMethodSyntax |     10 |    1000 | 203.5 μs |    NA |     - |     - |     - |  10.28 KB |
| LinqMethodSyntaxShortcutFirst |     10 |    1000 | 194.8 μs |    NA |     - |     - |     - |    9.2 KB |
|                BuiltPredicate |     10 |    1000 | 228.8 μs |    NA |     - |     - |     - |  11.95 KB |
|           IQueryableExtension |     10 |    1000 | 218.4 μs |    NA |     - |     - |     - |  10.77 KB |
|    ExtendedLambdaMethodSyntax |     10 |    1000 | 235.2 μs |    NA |     - |     - |     - |  10.77 KB |
|               **NeinExpressions** |     **15** |      **10** | **150.8 μs** |    **NA** |     **-** |     **-** |     **-** |  **13.79 KB** |
|         CachedNeinExpressions |     15 |      10 | 166.7 μs |    NA |     - |     - |     - |  13.95 KB |
|               ManualPredicate |     15 |      10 | 145.4 μs |    NA |     - |     - |     - |  12.05 KB |
|               LinqQuerySyntax |     15 |      10 | 161.0 μs |    NA |     - |     - |     - |  10.28 KB |
|              LinqMethodSyntax |     15 |      10 | 135.1 μs |    NA |     - |     - |     - |  10.28 KB |
| LinqMethodSyntaxShortcutFirst |     15 |      10 | 182.1 μs |    NA |     - |     - |     - |    9.2 KB |
|                BuiltPredicate |     15 |      10 | 132.5 μs |    NA |     - |     - |     - |  11.95 KB |
|           IQueryableExtension |     15 |      10 | 133.8 μs |    NA |     - |     - |     - |  10.77 KB |
|    ExtendedLambdaMethodSyntax |     15 |      10 | 184.4 μs |    NA |     - |     - |     - |  10.77 KB |
|               **NeinExpressions** |     **15** |     **100** | **166.5 μs** |    **NA** |     **-** |     **-** |     **-** |  **13.91 KB** |
|         CachedNeinExpressions |     15 |     100 | 165.4 μs |    NA |     - |     - |     - |  13.95 KB |
|               ManualPredicate |     15 |     100 | 170.5 μs |    NA |     - |     - |     - |  12.13 KB |
|               LinqQuerySyntax |     15 |     100 | 170.0 μs |    NA |     - |     - |     - |  10.28 KB |
|              LinqMethodSyntax |     15 |     100 | 145.2 μs |    NA |     - |     - |     - |  10.28 KB |
| LinqMethodSyntaxShortcutFirst |     15 |     100 | 160.9 μs |    NA |     - |     - |     - |    9.2 KB |
|                BuiltPredicate |     15 |     100 | 213.4 μs |    NA |     - |     - |     - |  11.95 KB |
|           IQueryableExtension |     15 |     100 | 233.4 μs |    NA |     - |     - |     - |  10.52 KB |
|    ExtendedLambdaMethodSyntax |     15 |     100 | 145.0 μs |    NA |     - |     - |     - |  10.77 KB |
|               **NeinExpressions** |     **15** |    **1000** | **236.1 μs** |    **NA** |     **-** |     **-** |     **-** |  **13.79 KB** |
|         CachedNeinExpressions |     15 |    1000 | 245.2 μs |    NA |     - |     - |     - |  13.71 KB |
|               ManualPredicate |     15 |    1000 | 258.3 μs |    NA |     - |     - |     - |  12.05 KB |
|               LinqQuerySyntax |     15 |    1000 | 205.7 μs |    NA |     - |     - |     - |  10.04 KB |
|              LinqMethodSyntax |     15 |    1000 | 211.3 μs |    NA |     - |     - |     - |  10.28 KB |
| LinqMethodSyntaxShortcutFirst |     15 |    1000 | 210.7 μs |    NA |     - |     - |     - |    9.2 KB |
|                BuiltPredicate |     15 |    1000 | 270.6 μs |    NA |     - |     - |     - |  11.95 KB |
|           IQueryableExtension |     15 |    1000 | 246.4 μs |    NA |     - |     - |     - |  10.77 KB |
|    ExtendedLambdaMethodSyntax |     15 |    1000 | 259.8 μs |    NA |     - |     - |     - |  10.52 KB |
