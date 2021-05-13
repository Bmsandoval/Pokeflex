``` ini

BenchmarkDotNet=v0.12.1, OS=macOS 11.2.3 (20D91) [Darwin 20.3.0]
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.201
  [Host] : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT

Toolchain=InProcessEmitToolchain  InvocationCount=1  UnrollFactor=1  

```
|   Method | Groups | Numbers |     Mean |     Error |    StdDev |
|--------- |------- |-------- |---------:|----------:|----------:|
| **RawQuery** |     **15** |      **10** | **3.589 ms** | **0.1509 ms** | **0.4426 ms** |
| **RawQuery** |     **15** |     **100** | **3.193 ms** | **0.1106 ms** | **0.3260 ms** |
