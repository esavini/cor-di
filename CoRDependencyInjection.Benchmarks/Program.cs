using BenchmarkDotNet.Running;
using CoRDependencyInjection.Benchmarks;

var summary = BenchmarkRunner.Run<Benchmarks>();
