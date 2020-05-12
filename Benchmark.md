# Benchmarks
The implementaions has been benchmarked using [DotNetBenchmark](https://github.com/dotnet/BenchmarkDotNet)

```
BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Core i7-8650U CPU 1.90GHz (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.1.201
```

## Day 01
| Method |      N |     Mean |    Error |   StdDev |   Median |
|------- |------- |---------:|---------:|---------:|---------:|
| D01_P1 | 100000 | 11.94 us | 0.504 us | 1.453 us | 11.25 us |
| D01_P2 | 100000 | 13.36 us | 0.280 us | 0.719 us | 13.18 us |

## Day 02
| Method |     N |        Mean |     Error |    StdDev |
|------- |------ |------------:|----------:|----------:|
| D02_P1 | 10000 |    10.67 us |  0.204 us |  0.191 us |
| D02_P2 | 10000 | 2,947.95 us | 56.586 us | 50.162 us |

## Day 03
| Method |     N |     Mean |    Error |   StdDev |
|------- |------ |---------:|---------:|---------:|
| D03_P1 | 10000 | 19.53 ms | 0.420 ms | 1.163 ms |
| D03_P2 | 10000 | 21.82 ms | 0.810 ms | 2.350 ms |