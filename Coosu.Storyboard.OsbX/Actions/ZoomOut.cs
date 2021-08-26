﻿using Coosu.Storyboard.Easing;
using Coosu.Storyboard.Events;

namespace Coosu.Storyboard.OsbX.Actions
{
    public class ZoomOut : BasicEvent
    {
        public ZoomOut()
        {
        }

        public ZoomOut(EasingFunctionBase easing, double startTime, double endTime, double[] start, double[] end)
            : base(easing, startTime, endTime, start, end)
        {
        }

        public override EventType EventType { get; } = new("ZO", 1, 12);
    }
}