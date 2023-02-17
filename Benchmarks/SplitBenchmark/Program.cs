﻿#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;

namespace SplitBenchmark;

internal class Program
{
    static void Main(string[] args)
    {
        var splitTask = new SplitTask();
        var i2 = splitTask.SpanSplit();
        var i = splitTask.SpanSplitOptimized();

        BenchmarkRunner.Run<SplitTask>();
    }
}


[SimpleJob(RuntimeMoniker.Net48)]
[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net70)]
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class SplitTask
{
    private readonly string _text;

    public SplitTask()
    {
        _text =
            "B|211:158|211:158|207:162|207:162|211:158|211:158|207:162|207:162|207:162|207:162|211:158|211:158|207:162|207:162|211:158|211:158|207:162|207:162|207:162|207:162|211:158|211:158|207:162|207:162|211:158|211:158|207:162|207:162|211:158|211:158|207:162|207:162|211:158|211:158|207:162|207:162|211:158|211:158|207:162|207:162|211:158|211:158|207:162|207:162|211:158|211:158|207:162|207:162|211:158|211:158|207:162|207:162|211:158|211:158|207:162|207:162|211:158|211:158|207:162|207:162|211:158|211:158|207:162|207:162|211:158|211:158|207:162|207:162|211:158|211:158|207:162|207:162|211:158|211:158|207:162|207:162|211:158|211:158|207:162|207:162|211:158|211:158|207:162|207:162|211:158|211:158|207:162|207:162|211:158|211:158|207:162|207:162|207:162|207:162|207:162|207:162|211:158|211:158|207:162|207:162|211:158|211:158|207:162|207:162|207:162|207:162|211:158|211:158|207:162|207:162|211:158|211:158|207:162|207:162|211:158|211:158|207:162|207:162|211:158|211:158|207:162|207:162|274:93|274:93|275:101|275:101|274:93|274:93|274:93|274:93|276:101|276:101|274:93|274:93|275:102|275:102|274:93|274:93|275:101|275:101|274:93|274:93|275:101|275:101|274:93|274:93|275:101|275:101|274:93|274:93|275:101|275:101|274:93|274:93|275:101|275:101|274:93|274:93|274:93|274:93|275:101|275:101|274:93|274:93|275:102|275:102|274:93|274:93|275:101|275:101|274:93|274:93|275:101|275:101|274:93|274:93|275:101|275:101|274:93|274:93|275:101|275:101|274:93|274:93|296:269|296:269|301:263|301:263|296:268|296:268|301:264|301:264|296:268|296:268|301:264|301:264|296:268|296:268|301:264|301:264|296:268|296:268|301:264|301:264|296:268|296:268|301:264|301:264|296:268|296:268|301:263|301:263|296:268|296:268|301:263|301:263|296:269|296:269|301:263|301:263|296:268|296:268|301:263|301:263|296:268|296:268|301:263|301:263|296:268|296:268|301:263|301:263|296:268|296:268|301:263|301:263|296:268|296:268|301:263|301:263|296:268|296:268|301:263|301:263|296:269|296:269|301:263|301:263|296:269|296:269|301:263|301:263|296:269|296:269|301:263|301:263|296:269|296:269|301:263|301:263|296:268|296:268|301:264|301:264|296:268|296:268|301:263|301:263|296:268|296:268|296:268|296:268|301:263|301:263|296:268|296:268|353:187|353:187|361:187|361:187|353:187|353:187|360:188|360:188|353:187|353:187|360:188|360:188|353:187|353:187|360:188|360:188|353:187|353:187|360:188|360:188|353:187|353:187|360:188|360:188|353:187|353:187|361:188|361:188|353:187|353:187|360:188|360:188|353:187|353:187|360:188|360:188|353:187|353:187|360:188|360:188|353:187|353:187|360:188|360:188|353:187|353:187|360:189|360:189|353:187|353:187|360:188|360:188|353:187|353:187|360:188|360:188|353:187|353:187|353:187|353:187|360:187|360:187|353:187|353:187|360:187|360:187|353:187|353:187|360:187|360:187|353:187|353:187|360:187|360:187|353:187|353:187|360:187|360:187|353:187|353:187|360:187|360:187|353:187|353:187|360:188|360:188|353:187|353:187|353:187|353:187|360:188|360:188|353:187|353:187|360:187|360:187|353:187|353:187|481:199";
    }
    [Benchmark]
    public object? Split()
    {
        var list = new List<(int, int)>();
        var ts = _text.Split('|');
        for (var i = 1; i < ts.Length; i++)
        {
            var t = ts[i];
            var arr = t.Split(':');

            list.Add((int.Parse(arr[0]), int.Parse(arr[1])));
        }

        return list;
    }

    [Benchmark]
    public object? SpanSplit()
    {
        var list = new List<(int, int)>();
        int i = -1;
        foreach (var t in _text.SpanSplit('|'))
        {
            i++;
            if (i == 0) continue;

            int j = 0;
            int x = 0;
            foreach (var span in t.SpanSplit(':'))
            {
                if (j == 0)
                {
#if NETFRAMEWORK
                        x = int.Parse(span.ToString());
#else
                    x = int.Parse(span);
#endif
                }
                else
                {
#if NETFRAMEWORK
                        var y = int.Parse(span.ToString());
#else
                    var y = int.Parse(span);
#endif
                    list.Add((x, y));
                }
                j++;
            }
        }

        return list;
    }


    [Benchmark]
    public object? SpanSplitOptimized()
    {
        var list = new List<(int, int)>();
        int i = -1;
        foreach (var t in _text.SpanSplit('|'))
        {
            i++;
            if (i == 0) continue;

            int x = 0;

            foreach (var span in t.SpanSplitRange(':'))
            {
                if (span.index == 0)
                {
#if NETFRAMEWORK
                        x = int.Parse(t.Slice(span.range.startIndex, span.range.length).ToString());
#else
                    x = int.Parse(t.Slice(span.range.startIndex, span.range.length));
#endif
                }
                else if (span.index == 1)
                {
#if NETFRAMEWORK
                        var y = int.Parse(t.Slice(span.range.startIndex, span.range.length).ToString());
#else
                    var y = int.Parse(t.Slice(span.range.startIndex, span.range.length));
#endif
                    list.Add((x, y));
                }
            }
        }

        return list;
    }
}