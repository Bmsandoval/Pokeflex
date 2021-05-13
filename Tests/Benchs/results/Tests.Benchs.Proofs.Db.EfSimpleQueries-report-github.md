``` ini

BenchmarkDotNet=v0.12.1, OS=macOS 11.2.3 (20D91) [Darwin 20.3.0]
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.201
  [Host] : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT

Toolchain=InProcessEmitToolchain  InvocationCount=1  UnrollFactor=1  

```
|          Method | Groups | Numbers |     Mean |     Error |    StdDev |   Median |
|---------------- |------- |-------- |---------:|----------:|----------:|---------:|
| **ManualPredicate** |     **15** |      **10** | **3.531 ms** | **0.2687 ms** | **0.7882 ms** | **3.260 ms** |
| **ManualPredicate** |     **15** |     **100** | **3.797 ms** | **0.2207 ms** | **0.6474 ms** | **3.660 ms** |
