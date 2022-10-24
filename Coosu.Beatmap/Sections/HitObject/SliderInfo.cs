﻿using System.Collections.Generic;
using System.IO;
using System.Numerics;
using Coosu.Beatmap.Configurable;
using Coosu.Beatmap.Internal;
using Coosu.Shared;
using Coosu.Shared.Numerics;

namespace Coosu.Beatmap.Sections.HitObject;

public class SliderInfo : SerializeWritableObject
{
    public SliderType SliderType { get; set; }
    public IReadOnlyList<Vector2> ControlPoints { get; set; } = EmptyArray<Vector2>.Value;
    public int Repeat { get; set; }
    public double PixelLength { get; set; }
    public HitsoundType[]? EdgeHitsounds { get; set; }
    public ObjectSamplesetType[]? EdgeSamples { get; set; }
    public ObjectSamplesetType[]? EdgeAdditions { get; set; }

    public Vector2 StartPoint { get; set; }
    public Vector2 EndPoint => ControlPoints[ControlPoints.Count - 1];

    public int StartTime { get; set; }
    public override void AppendSerializedString(TextWriter textWriter)
    {
        textWriter.Write(SliderType.ToSliderFlag());
        textWriter.Write('|');
        for (var i = 0; i < ControlPoints.Count; i++)
        {
            var controlPoint = ControlPoints[i];
            textWriter.Write(controlPoint.X.ToString(ParseHelper.EnUsNumberFormat));
            textWriter.Write(':');
            textWriter.Write(controlPoint.Y.ToString(ParseHelper.EnUsNumberFormat));
            if (i < ControlPoints.Count - 1)
            {
                textWriter.Write('|');
            }
        }

        textWriter.Write(',');
        textWriter.Write(Repeat);
        textWriter.Write(',');
        textWriter.Write(PixelLength.ToString(ParseHelper.EnUsNumberFormat));
        if (EdgeHitsounds == null)
        {
            return;
        }

        textWriter.Write(',');
        for (var i = 0; i < EdgeHitsounds.Length; i++)
        {
            var edgeHitsound = EdgeHitsounds[i];
            textWriter.Write((byte)edgeHitsound);
            if (i < EdgeHitsounds.Length - 1)
            {
                textWriter.Write('|');
            }
        }

        if (EdgeSamples == null || EdgeAdditions == null)
        {
            return;
        }

        textWriter.Write(',');
        for (var i = 0; i < EdgeSamples.Length; i++)
        {
            var edgeSample = EdgeSamples[i];
            var edgeAddition = EdgeAdditions[i];
            textWriter.Write((byte)edgeSample);
            textWriter.Write(':');
            textWriter.Write((byte)edgeAddition);
            if (i < EdgeSamples.Length - 1)
            {
                textWriter.Write('|');
            }
        }
    }
}