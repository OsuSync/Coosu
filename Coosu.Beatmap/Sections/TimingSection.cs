﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Coosu.Beatmap.Configurable;
using Coosu.Beatmap.Sections.Timing;
using Coosu.Shared;
using Coosu.Shared.Mathematics;

namespace Coosu.Beatmap.Sections
{
    [SectionProperty("TimingPoints")]
    public sealed class TimingSection : Section
    {
        public List<TimingPoint> TimingList { get; set; } = new();
        public double MinTime => TimingList.Count == 0 ? 0 : TimingList.Min(t => t.Offset);
        public double MaxTime => TimingList.Count == 0 ? 0 : TimingList.Max(t => t.Offset);

        public TimingPoint this[int index] => TimingList[index];

        public override void Match(string line)
        {
            double offset = default;
            double factor = default;
            byte rhythm = default;
            var timingSampleset = TimingSamplesetType.None;
            ushort track = default;
            byte volume = default;
            bool inherit = default;
            bool kiai = default;
            bool positive = default;

            int i = -1;
            foreach (var span in line.SpanSplit(','))
            {
                i++;
#if NETCOREAPP3_1_OR_GREATER
                switch (i)
                {
                    case 0: offset = double.Parse(span); break;
                    case 1: factor = double.Parse(span); break;
                    case 2: rhythm = byte.Parse(span); break;
                    case 3: timingSampleset = (TimingSamplesetType)(byte.Parse(span) - 1); break;
                    case 4: track = ushort.Parse(span); break;
                    case 5: volume = byte.Parse(span); break;
                    case 6: inherit = byte.Parse(span) == 0; break;
                    case 7: kiai = byte.Parse(span) != 0; break;
                    case 8: positive = factor >= 0; break;
                }
#else
                switch (i)
                {
                    case 0: offset = double.Parse(span.ToString()); break;
                    case 1: factor = double.Parse(span.ToString()); break;
                    case 2: rhythm = byte.Parse(span.ToString()); break;
                    case 3: timingSampleset = (TimingSamplesetType)(byte.Parse(span.ToString()) - 1); break;
                    case 4: track = ushort.Parse(span.ToString()); break;
                    case 5: volume = byte.Parse(span.ToString()); break;
                    case 6: inherit = byte.Parse(span.ToString()) == 0; break;
                    case 7: kiai = byte.Parse(span.ToString()) != 0; break;
                    case 8: positive = factor >= 0; break;
                }
#endif
            }

            TimingList.Add(new TimingPoint
            {
                Offset = offset,
                Factor = factor,
                Rhythm = rhythm,
                TimingSampleset = timingSampleset,
                Track = track,
                Volume = volume,
                IsInherit = inherit,
                IsKiai = kiai,
                Positive = positive
            });
        }

        public override void AppendSerializedString(TextWriter textWriter)
        {
            textWriter.WriteLine($"[{SectionName}]");
            foreach (var timingPoint in TimingList)
            {
                timingPoint.AppendSerializedString(textWriter);
            }
        }
    }
}
