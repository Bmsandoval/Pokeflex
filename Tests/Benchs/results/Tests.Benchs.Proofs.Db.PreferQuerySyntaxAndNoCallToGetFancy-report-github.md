``` ini

BenchmarkDotNet=v0.12.1, OS=macOS 11.2.3 (20D91) [Darwin 20.3.0]
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.203
  [Host] : .NET Core 5.0.6 (CoreCLR 5.0.621.22011, CoreFX 5.0.621.22011), X64 RyuJIT

Toolchain=InProcessEmitToolchain  InvocationCount=1  IterationCount=100  
LaunchCount=3  UnrollFactor=1  WarmupCount=15  

```
|                        Method | Groups | Numbers |     Mean |     Error |    StdDev |   Median | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------------ |------- |-------- |---------:|----------:|----------:|---------:|------:|------:|------:|----------:|
|               **NeinExpressions** |      **5** |      **10** | **2.045 ms** | **0.0354 ms** | **0.1802 ms** | **2.036 ms** |     **-** |     **-** |     **-** |   **23976 B** |
|         CachedNeinExpressions |      5 |      10 | 2.097 ms | 0.0391 ms | 0.2003 ms | 2.083 ms |     - |     - |     - |   24144 B |
|               ManualPredicate |      5 |      10 | 2.138 ms | 0.0421 ms | 0.2140 ms | 2.098 ms |     - |     - |     - |   22192 B |
|               LinqQuerySyntax |      5 |      10 | 2.134 ms | 0.0409 ms | 0.2100 ms | 2.122 ms |     - |     - |     - |   20384 B |
|              LinqMethodSyntax |      5 |      10 | 2.103 ms | 0.0394 ms | 0.2023 ms | 2.069 ms |     - |     - |     - |   20384 B |
| LinqMethodSyntaxShortcutFirst |      5 |      10 | 2.106 ms | 0.0409 ms | 0.2083 ms | 2.061 ms |     - |     - |     - |   18824 B |
|                BuiltPredicate |      5 |      10 | 2.244 ms | 0.0419 ms | 0.2143 ms | 2.219 ms |     - |     - |     - |   28240 B |
|           IQueryableExtension |      5 |      10 | 2.086 ms | 0.0374 ms | 0.1913 ms | 2.076 ms |     - |     - |     - |   19960 B |
|    ExtendedLambdaMethodSyntax |      5 |      10 | 2.051 ms | 0.0436 ms | 0.2244 ms | 2.017 ms |     - |     - |     - |   20880 B |
|               **NeinExpressions** |      **5** |     **100** | **2.168 ms** | **0.0455 ms** | **0.2304 ms** | **2.141 ms** |     **-** |     **-** |     **-** |   **23976 B** |
|         CachedNeinExpressions |      5 |     100 | 2.130 ms | 0.0424 ms | 0.2155 ms | 2.109 ms |     - |     - |     - |   24144 B |
|               ManualPredicate |      5 |     100 | 2.114 ms | 0.0393 ms | 0.1995 ms | 2.103 ms |     - |     - |     - |   22192 B |
|               LinqQuerySyntax |      5 |     100 | 2.103 ms | 0.0373 ms | 0.1910 ms | 2.080 ms |     - |     - |     - |   20384 B |
|              LinqMethodSyntax |      5 |     100 | 2.122 ms | 0.0383 ms | 0.1964 ms | 2.109 ms |     - |     - |     - |   20384 B |
| LinqMethodSyntaxShortcutFirst |      5 |     100 | 2.101 ms | 0.0435 ms | 0.2257 ms | 2.063 ms |     - |     - |     - |   17904 B |
|                BuiltPredicate |      5 |     100 | 2.180 ms | 0.0408 ms | 0.2086 ms | 2.154 ms |     - |     - |     - |   27616 B |
|           IQueryableExtension |      5 |     100 | 2.134 ms | 0.0397 ms | 0.2033 ms | 2.111 ms |     - |     - |     - |   20880 B |
|    ExtendedLambdaMethodSyntax |      5 |     100 | 2.186 ms | 0.0466 ms | 0.2413 ms | 2.153 ms |     - |     - |     - |   19960 B |
|               **NeinExpressions** |      **5** |    **1000** | **2.289 ms** | **0.0499 ms** | **0.2585 ms** | **2.293 ms** |     **-** |     **-** |     **-** |   **23976 B** |
|         CachedNeinExpressions |      5 |    1000 | 2.269 ms | 0.0543 ms | 0.2803 ms | 2.270 ms |     - |     - |     - |   24144 B |
|               ManualPredicate |      5 |    1000 | 2.393 ms | 0.0545 ms | 0.2824 ms | 2.391 ms |     - |     - |     - |   22192 B |
|               LinqQuerySyntax |      5 |    1000 | 2.326 ms | 0.0496 ms | 0.2550 ms | 2.313 ms |     - |     - |     - |   19464 B |
|              LinqMethodSyntax |      5 |    1000 | 2.274 ms | 0.0568 ms | 0.2934 ms | 2.245 ms |     - |     - |     - |   20384 B |
| LinqMethodSyntaxShortcutFirst |      5 |    1000 | 2.245 ms | 0.0465 ms | 0.2406 ms | 2.249 ms |     - |     - |     - |   18824 B |
|                BuiltPredicate |      5 |    1000 | 2.368 ms | 0.0545 ms | 0.2799 ms | 2.361 ms |     - |     - |     - |   26696 B |
|           IQueryableExtension |      5 |    1000 | 2.377 ms | 0.0500 ms | 0.2579 ms | 2.383 ms |     - |     - |     - |   20880 B |
|    ExtendedLambdaMethodSyntax |      5 |    1000 | 2.299 ms | 0.0498 ms | 0.2573 ms | 2.285 ms |     - |     - |     - |   20880 B |
|               **NeinExpressions** |     **10** |      **10** | **2.221 ms** | **0.0483 ms** | **0.2431 ms** | **2.201 ms** |     **-** |     **-** |     **-** |   **23976 B** |
|         CachedNeinExpressions |     10 |      10 | 2.259 ms | 0.0454 ms | 0.2312 ms | 2.275 ms |     - |     - |     - |   24144 B |
|               ManualPredicate |     10 |      10 | 2.205 ms | 0.0534 ms | 0.2704 ms | 2.163 ms |     - |     - |     - |   21272 B |
|               LinqQuerySyntax |     10 |      10 | 2.116 ms | 0.0447 ms | 0.2294 ms | 2.091 ms |     - |     - |     - |   20384 B |
|              LinqMethodSyntax |     10 |      10 | 2.142 ms | 0.0468 ms | 0.2393 ms | 2.089 ms |     - |     - |     - |   20384 B |
| LinqMethodSyntaxShortcutFirst |     10 |      10 | 2.188 ms | 0.0560 ms | 0.2851 ms | 2.130 ms |     - |     - |     - |   18824 B |
|                BuiltPredicate |     10 |      10 | 2.287 ms | 0.0507 ms | 0.2593 ms | 2.253 ms |     - |     - |     - |   27616 B |
|           IQueryableExtension |     10 |      10 | 2.234 ms | 0.0512 ms | 0.2597 ms | 2.220 ms |     - |     - |     - |   19960 B |
|    ExtendedLambdaMethodSyntax |     10 |      10 | 2.181 ms | 0.0524 ms | 0.2649 ms | 2.134 ms |     - |     - |     - |   20880 B |
|               **NeinExpressions** |     **10** |     **100** | **2.262 ms** | **0.0522 ms** | **0.2675 ms** | **2.233 ms** |     **-** |     **-** |     **-** |   **23976 B** |
|         CachedNeinExpressions |     10 |     100 | 2.208 ms | 0.0598 ms | 0.3070 ms | 2.173 ms |     - |     - |     - |   24144 B |
|               ManualPredicate |     10 |     100 | 2.251 ms | 0.0512 ms | 0.2604 ms | 2.246 ms |     - |     - |     - |   22192 B |
|               LinqQuerySyntax |     10 |     100 | 2.241 ms | 0.0521 ms | 0.2662 ms | 2.226 ms |     - |     - |     - |   20384 B |
|              LinqMethodSyntax |     10 |     100 | 2.220 ms | 0.0503 ms | 0.2547 ms | 2.179 ms |     - |     - |     - |   20384 B |
| LinqMethodSyntaxShortcutFirst |     10 |     100 | 2.213 ms | 0.0533 ms | 0.2744 ms | 2.222 ms |     - |     - |     - |   18824 B |
|                BuiltPredicate |     10 |     100 | 2.297 ms | 0.0528 ms | 0.2694 ms | 2.282 ms |     - |     - |     - |   27616 B |
|           IQueryableExtension |     10 |     100 | 2.291 ms | 0.0594 ms | 0.3022 ms | 2.269 ms |     - |     - |     - |   20880 B |
|    ExtendedLambdaMethodSyntax |     10 |     100 | 2.294 ms | 0.0552 ms | 0.2769 ms | 2.248 ms |     - |     - |     - |   20880 B |
|               **NeinExpressions** |     **10** |    **1000** | **2.387 ms** | **0.0527 ms** | **0.2668 ms** | **2.376 ms** |     **-** |     **-** |     **-** |   **23976 B** |
|         CachedNeinExpressions |     10 |    1000 | 2.382 ms | 0.0511 ms | 0.2549 ms | 2.373 ms |     - |     - |     - |   24144 B |
|               ManualPredicate |     10 |    1000 | 2.343 ms | 0.0579 ms | 0.2887 ms | 2.322 ms |     - |     - |     - |   22192 B |
|               LinqQuerySyntax |     10 |    1000 | 2.384 ms | 0.0516 ms | 0.2605 ms | 2.370 ms |     - |     - |     - |   20384 B |
|              LinqMethodSyntax |     10 |    1000 | 2.341 ms | 0.0548 ms | 0.2777 ms | 2.335 ms |     - |     - |     - |   20384 B |
| LinqMethodSyntaxShortcutFirst |     10 |    1000 | 2.333 ms | 0.0562 ms | 0.2825 ms | 2.306 ms |     - |     - |     - |   18824 B |
|                BuiltPredicate |     10 |    1000 | 2.522 ms | 0.0469 ms | 0.2358 ms | 2.522 ms |     - |     - |     - |   27952 B |
|           IQueryableExtension |     10 |    1000 | 2.391 ms | 0.0489 ms | 0.2471 ms | 2.378 ms |     - |     - |     - |   20880 B |
|    ExtendedLambdaMethodSyntax |     10 |    1000 | 2.403 ms | 0.0533 ms | 0.2694 ms | 2.414 ms |     - |     - |     - |   20880 B |
|               **NeinExpressions** |     **15** |      **10** | **2.245 ms** | **0.0531 ms** | **0.2691 ms** | **2.231 ms** |     **-** |     **-** |     **-** |   **23976 B** |
|         CachedNeinExpressions |     15 |      10 | 2.290 ms | 0.0556 ms | 0.2825 ms | 2.268 ms |     - |     - |     - |   23224 B |
|               ManualPredicate |     15 |      10 | 2.255 ms | 0.0539 ms | 0.2728 ms | 2.227 ms |     - |     - |     - |   22192 B |
|               LinqQuerySyntax |     15 |      10 | 2.330 ms | 0.0553 ms | 0.2793 ms | 2.316 ms |     - |     - |     - |   20384 B |
|              LinqMethodSyntax |     15 |      10 | 2.321 ms | 0.0620 ms | 0.3109 ms | 2.330 ms |     - |     - |     - |   20384 B |
| LinqMethodSyntaxShortcutFirst |     15 |      10 | 2.317 ms | 0.0565 ms | 0.2841 ms | 2.277 ms |     - |     - |     - |   18824 B |
|                BuiltPredicate |     15 |      10 | 2.531 ms | 0.0766 ms | 0.3830 ms | 2.481 ms |     - |     - |     - |   27616 B |
|           IQueryableExtension |     15 |      10 | 2.363 ms | 0.0626 ms | 0.3188 ms | 2.352 ms |     - |     - |     - |   20880 B |
|    ExtendedLambdaMethodSyntax |     15 |      10 | 2.338 ms | 0.0630 ms | 0.3176 ms | 2.315 ms |     - |     - |     - |   20880 B |
|               **NeinExpressions** |     **15** |     **100** | **2.382 ms** | **0.0532 ms** | **0.2705 ms** | **2.391 ms** |     **-** |     **-** |     **-** |   **23976 B** |
|         CachedNeinExpressions |     15 |     100 | 2.319 ms | 0.0611 ms | 0.3109 ms | 2.310 ms |     - |     - |     - |   24144 B |
|               ManualPredicate |     15 |     100 | 2.376 ms | 0.0564 ms | 0.2876 ms | 2.367 ms |     - |     - |     - |   22192 B |
|               LinqQuerySyntax |     15 |     100 | 2.316 ms | 0.0501 ms | 0.2527 ms | 2.299 ms |     - |     - |     - |   20384 B |
|              LinqMethodSyntax |     15 |     100 | 2.320 ms | 0.0500 ms | 0.2539 ms | 2.317 ms |     - |     - |     - |   20384 B |
| LinqMethodSyntaxShortcutFirst |     15 |     100 | 2.291 ms | 0.0507 ms | 0.2553 ms | 2.300 ms |     - |     - |     - |   18824 B |
|                BuiltPredicate |     15 |     100 | 2.392 ms | 0.0546 ms | 0.2794 ms | 2.371 ms |     - |     - |     - |   27616 B |
|           IQueryableExtension |     15 |     100 | 2.356 ms | 0.0483 ms | 0.2437 ms | 2.373 ms |     - |     - |     - |   20880 B |
|    ExtendedLambdaMethodSyntax |     15 |     100 | 2.324 ms | 0.0490 ms | 0.2475 ms | 2.324 ms |     - |     - |     - |   20880 B |
|               **NeinExpressions** |     **15** |    **1000** |       **NA** |        **NA** |        **NA** |       **NA** |     **-** |     **-** |     **-** |         **-** |

Benchmarks with issues:
  PreferQuerySyntaxAndNoCallToGetFancy.NeinExpressions: Job-UZLGAD(Toolchain=InProcessEmitToolchain, InvocationCount=1, IterationCount=100, LaunchCount=3, UnrollFactor=1, WarmupCount=15) [Groups=15, Numbers=1000]
