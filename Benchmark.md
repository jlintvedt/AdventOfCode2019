# Benchmarks
The implementaions has been benchmarked using [DotNetBenchmark](https://github.com/dotnet/BenchmarkDotNet)

```
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.572 (2004/?/20H1)
AMD Ryzen 9 3900X, 1 CPU, 24 logical and 12 physical cores
.NET Core SDK=3.1.201
```

## Day 01
| Method |      N |     Mean |     Error |    StdDev |
|------- |------- |---------:|----------:|----------:|
| D01_P1 | 100000 | 5.378 us | 0.0374 us | 0.0313 us |
| D01_P2 | 100000 | 6.297 us | 0.0530 us | 0.0470 us |

## Day 02
| Method |     N |          Mean |      Error |     StdDev |
|------- |------ |--------------:|-----------:|-----------:|
| D02_P1 | 10000 |      8.244 us |  0.1533 us |  0.1434 us |
| D02_P2 | 10000 | 10,186.802 us | 48.1997 us | 37.6312 us |

## Day 03
| Method |     N |     Mean |    Error |   StdDev |
|------- |------ |---------:|---------:|---------:|
| D03_P1 | 10000 | 11.85 ms | 0.080 ms | 0.075 ms |
| D03_P2 | 10000 | 12.04 ms | 0.074 ms | 0.066 ms |

## Day 04
| Method |     N |     Mean |    Error |   StdDev |
|------- |------ |---------:|---------:|---------:|
| D04_P1 | 10000 | 21.35 us | 0.192 us | 0.179 us |
| D04_P2 | 10000 | 27.21 us | 0.384 us | 0.359 us |

## Day 05
| Method |     N |     Mean |    Error |   StdDev |
|------- |------ |---------:|---------:|---------:|
| D05_P1 | 10000 | 77.37 us | 0.613 us | 0.543 us |
| D05_P2 | 10000 | 57.18 us | 0.479 us | 0.425 us |

## Day 06
| Method |     N |     Mean |   Error |  StdDev |
|------- |------ |---------:|--------:|--------:|
| D06_P1 | 10000 | 224.7 us | 2.68 us | 2.51 us |
| D06_P2 | 10000 | 388.6 us | 1.95 us | 1.83 us |

## Day 07
| Method |     N |      Mean |     Error |    StdDev |
|------- |------ |----------:|----------:|----------:|
| D07_P1 | 10000 |  4.935 ms | 0.0956 ms | 0.1371 ms |
| D07_P2 | 10000 | 26.709 ms | 0.1940 ms | 0.1815 ms |

## Day 08
| Method |      N |     Mean |   Error |  StdDev |
|------- |------- |---------:|--------:|--------:|
| D08_P1 | 100000 | 144.5 us | 1.84 us | 1.72 us |
| D08_P2 | 100000 | 119.7 us | 0.96 us | 0.90 us |

## Day 09
| Method |     N |         Mean |      Error |     StdDev |
|------- |------ |-------------:|-----------:|-----------:|
| D09_P1 | 10000 |     86.21 us |   0.594 us |   0.556 us |
| D09_P2 | 10000 | 22,026.44 us | 136.255 us | 127.453 us |

## Day 10
| Method |     N |       Mean |    Error |   StdDev |
|------- |------ |-----------:|---------:|---------:|
| D10_P1 | 10000 | 6,156.0 us | 86.40 us | 76.59 us |
| D10_P2 | 10000 |   251.0 us |  3.28 us |  3.07 us |