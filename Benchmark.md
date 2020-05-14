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
| Method |     N |         Mean |       Error |      StdDev |
|------- |------ |-------------:|------------:|------------:|
| D02_P1 | 10000 |     9.150 us |   0.1829 us |   0.2504 us |
| D02_P2 | 10000 | 7,963.648 us | 148.2815 us | 138.7026 us |

## Day 03
| Method |     N |     Mean |    Error |   StdDev |
|------- |------ |---------:|---------:|---------:|
| D03_P1 | 10000 | 19.53 ms | 0.420 ms | 1.163 ms |
| D03_P2 | 10000 | 21.82 ms | 0.810 ms | 2.350 ms |

## Day 04
| Method |     N |     Mean |    Error |   StdDev |   Median |
|------- |------ |---------:|---------:|---------:|---------:|
| D04_P1 | 10000 | 28.71 us | 1.160 us | 3.364 us | 28.72 us |
| D04_P2 | 10000 | 43.47 us | 2.209 us | 6.231 us | 41.72 us |

## Day 05
| Method |     N |     Mean |    Error |   StdDev |
|------- |------ |---------:|---------:|---------:|
| D05_P1 | 10000 | 51.79 us | 1.189 us | 3.353 us |
| D05_P2 | 10000 | 52.44 us | 1.036 us | 1.192 us |