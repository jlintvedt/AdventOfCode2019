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
| Method |     N |         Mean |      Error |     StdDev |
|------- |------ |-------------:|-----------:|-----------:|
| D02_P1 | 10000 |     18.04 us |   0.620 us |   1.759 us |
| D02_P2 | 10000 | 16,369.56 us | 325.796 us | 908.188 us |

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
| D05_P1 | 10000 | 46.92 us | 0.900 us | 1.232 us |
| D05_P2 | 10000 | 49.39 us | 0.758 us | 0.672 us |

## Day 06
| Method |     N |     Mean |    Error |   StdDev |
|------- |------ |---------:|---------:|---------:|
| D06_P1 | 10000 | 445.5 us |  8.49 us |  7.09 us |
| D06_P2 | 10000 | 814.7 us | 16.26 us | 28.90 us |

## Day 07
| Method |     N |        Mean |     Error |    StdDev |
|------- |------ |------------:|----------:|----------:|
| D07_P1 | 10000 |    909.6 us |  32.78 us |  94.05 us |
| D07_P2 | 10000 | 31,427.3 us | 628.54 us | 587.94 us |

## Day 09
| Method |    N |        Mean |       Error |       StdDev |
|------- |----- |------------:|------------:|-------------:|
| D00_P1 | 1000 |    420.2 us |    11.36 us |     33.32 us |
| D00_P2 | 1000 | 87,843.2 us | 4,228.55 us | 12,401.62 us |