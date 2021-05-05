Create new unit tests:

# Not sure why you'd have multiple test projects, but you can
$ dotnet new xunit -o Tests
$ dotnet add ./Tests/PokeflexTests.csproj reference ./App/Pokeflex.csproj






# Create ram disk 512mb ramdisk (512*2048=1048576)
$ diskutil erasevolume HFS+ 'RAM Disk' `hdiutil attach -nomount ram://1048576`
```
Started erase on disk2
Unmounting disk
Erasing
Initialized /dev/rdisk2 as a 512 MB case-insensitive HFS Plus volume
Mounting disk
Finished erase on disk2 (RAM Disk)
```
# symlink bin folder to the ramdisk
$ mkdir -p /Volumes/RAM\ Disk/Pokeflex/bin
$ rm -rf bin
$ ln -s /Volumes/RAM\ Disk/Pokeflex/bin /Users/bryansandoval/projects/C#/Pokeflex/App






// Benchmark: PokeflexServiceListBenchmarks.SelectOneLinq: DefaultJob [Groups=10, Numbers=100, Offset=10, Limit=10]
// Benchmark: PokeflexServiceListBenchmarks.UnionWhereNotExistsLinq: DefaultJob [Groups=10, Numbers=100, Offset=10, Limit=10]
// Benchmark: PokeflexServiceListBenchmarks.CoalesceLinq: DefaultJob [Groups=10, Numbers=100, Offset=10, Limit=10]
// ***** BenchmarkRunner: Finish  *****
|                  Method | Groups | Numbers | Offset | Limit |     Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------ |------- |-------- |------- |------ |---------:|----------:|----------:|------:|--------:|
|           SelectOneLinq |     10 |     100 |     10 |    10 | 1.442 ms | 0.0285 ms | 0.0556 ms |  1.00 |    0.00 |
| UnionWhereNotExistsLinq |     10 |     100 |     10 |    10 | 2.240 ms | 0.0442 ms | 0.0605 ms |  1.58 |    0.08 |
|            CoalesceLinq |     10 |     100 |     10 |    10 | 3.021 ms | 0.0588 ms | 0.0550 ms |  2.15 |    0.09 |
// ***** BenchmarkRunner: End *****
// ***** BenchmarkRunner: Start   *****
// ***** Found 5 benchmark(s) in total *****
// ***** Building 1 exe(s) in Parallel: Start   *****
// ***** Done, took 00:00:05 (5.03 sec)   *****
// Benchmark: PokeflexServiceSelectBenchmarks.SelectOne: DefaultJob [Groups=10, Numbers=100]
// Benchmark: PokeflexServiceSelectBenchmarks.UnionWhereNotExistsLinq: DefaultJob [Groups=10, Numbers=100]
// Benchmark: PokeflexServiceSelectBenchmarks.CoalesceLinq: DefaultJob [Groups=10, Numbers=100]
// Benchmark: PokeflexServiceSelectBenchmarks.SequentialLinqWhereNotIn: DefaultJob [Groups=10, Numbers=100]
// Benchmark: PokeflexServiceSelectBenchmarks.SequentialLinq: DefaultJob [Groups=10, Numbers=100]
// ***** BenchmarkRunner: Finish  *****
|                   Method | Categories | Groups | Numbers |     Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------- |----------- |------- |-------- |---------:|----------:|----------:|------:|--------:|
|                SelectOne |            |     10 |     100 | 1.360 ms | 0.0271 ms | 0.0723 ms |  1.00 |    0.00 |
|  UnionWhereNotExistsLinq |       Linq |     10 |     100 | 1.506 ms | 0.0300 ms | 0.0549 ms |  1.10 |    0.06 |
|             CoalesceLinq |       Linq |     10 |     100 | 1.547 ms | 0.0302 ms | 0.0537 ms |  1.13 |    0.08 |
| SequentialLinqWhereNotIn |       Linq |     10 |     100 | 3.091 ms | 0.0613 ms | 0.1384 ms |  2.27 |    0.14 |
|           SequentialLinq |       Linq |     10 |     100 | 2.766 ms | 0.0551 ms | 0.1341 ms |  2.04 |    0.15 |