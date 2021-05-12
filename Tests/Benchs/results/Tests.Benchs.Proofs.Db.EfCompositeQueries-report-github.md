``` ini

BenchmarkDotNet=v0.12.1, OS=macOS 11.2.3 (20D91) [Darwin 20.3.0]
Intel Core i7-9750H CPU 2.60GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.201
  [Host] : .NET Core 5.0.4 (CoreCLR 5.0.421.11614, CoreFX 5.0.421.11614), X64 RyuJIT

Toolchain=InProcessEmitToolchain  InvocationCount=1  UnrollFactor=1  

```
|   Method | Groups | Numbers |     Mean |    Error |   StdDev |   Median |
|--------- |------- |-------- |---------:|---------:|---------:|---------:|
| **RawQuery** |     **15** |      **10** | **158.2 μs** | **11.30 μs** | **31.31 μs** | **146.0 μs** |
| **RawQuery** |     **15** |     **100** | **121.2 μs** |  **3.16 μs** |  **8.77 μs** | **119.9 μs** |
