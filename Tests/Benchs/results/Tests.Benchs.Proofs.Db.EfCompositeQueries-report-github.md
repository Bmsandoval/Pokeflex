``` ini

BenchmarkDotNet=v0.12.1, OS=macOS 11.2.3 (20D91) [Darwin 20.3.0]
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.201
  [Host] : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT

Toolchain=InProcessEmitToolchain  InvocationCount=1  UnrollFactor=1  

```
|          Method | Groups | Numbers |     Mean |     Error |    StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------- |------- |-------- |---------:|----------:|----------:|------:|------:|------:|----------:|
| **ManualPredicate** |      **5** |      **10** | **2.223 ms** | **0.0731 ms** | **0.2144 ms** |     **-** |     **-** |     **-** |  **18.23 KB** |
|        Proposal |      5 |      10 | 2.319 ms | 0.0605 ms | 0.1737 ms |     - |     - |     - |  42.91 KB |
| **ManualPredicate** |      **5** |    **1000** | **2.243 ms** | **0.0837 ms** | **0.2455 ms** |     **-** |     **-** |     **-** |  **17.48 KB** |
|        Proposal |      5 |    1000 | 2.456 ms | 0.0904 ms | 0.2650 ms |     - |     - |     - |  42.85 KB |
| **ManualPredicate** |      **5** |   **10000** | **2.432 ms** | **0.0799 ms** | **0.2332 ms** |     **-** |     **-** |     **-** |  **17.66 KB** |
|        Proposal |      5 |   10000 | 2.841 ms | 0.0915 ms | 0.2698 ms |     - |     - |     - |  42.85 KB |
| **ManualPredicate** |     **10** |      **10** | **1.952 ms** | **0.0615 ms** | **0.1814 ms** |     **-** |     **-** |     **-** |  **17.54 KB** |
|        Proposal |     10 |      10 | 2.297 ms | 0.0816 ms | 0.2369 ms |     - |     - |     - |  42.85 KB |
| **ManualPredicate** |     **10** |    **1000** | **2.372 ms** | **0.0651 ms** | **0.1908 ms** |     **-** |     **-** |     **-** |  **16.59 KB** |
|        Proposal |     10 |    1000 | 2.605 ms | 0.0639 ms | 0.1853 ms |     - |     - |     - |  43.46 KB |
| **ManualPredicate** |     **10** |   **10000** | **2.589 ms** | **0.1098 ms** | **0.3096 ms** |     **-** |     **-** |     **-** |  **16.59 KB** |
|        Proposal |     10 |   10000 | 2.741 ms | 0.0893 ms | 0.2618 ms |     - |     - |     - |   42.3 KB |
| **ManualPredicate** |     **15** |      **10** | **2.043 ms** | **0.0484 ms** | **0.1397 ms** |     **-** |     **-** |     **-** |  **17.48 KB** |
|        Proposal |     15 |      10 | 2.326 ms | 0.0782 ms | 0.2269 ms |     - |     - |     - |  42.85 KB |
| **ManualPredicate** |     **15** |    **1000** | **2.280 ms** | **0.0707 ms** | **0.2041 ms** |     **-** |     **-** |     **-** |  **17.48 KB** |
|        Proposal |     15 |    1000 | 2.636 ms | 0.0668 ms | 0.1938 ms |     - |     - |     - |  43.05 KB |
| **ManualPredicate** |     **15** |   **10000** | **2.892 ms** | **0.1585 ms** | **0.4548 ms** |     **-** |     **-** |     **-** |  **17.48 KB** |
|        Proposal |     15 |   10000 | 3.043 ms | 0.1288 ms | 0.3738 ms |     - |     - |     - |  42.85 KB |
