﻿using System.Collections.Generic;
using Coosu.Storyboard.Common;
using Coosu.Storyboard.Easing;

namespace Coosu.Storyboard.Events;

public sealed class MoveX : BasicEvent, IPositionAdjustable
{
    public override EventType EventType => EventTypes.MoveX;

    public double StartX
    {
        get => GetValue(0);
        set => SetValue(0, value);
    }

    public double EndX
    {
        get => GetValue(1);
        set => SetValue(1, value);
    }

    public MoveX(EasingFunctionBase easing, double startTime, double endTime, List<double> values)
        : base(easing, startTime, endTime, values)
    {
    }

    public MoveX()
    {
    }

    public void AdjustPosition(double x, double y)
    {
        StartX += x;
        EndX += x;
    }
}