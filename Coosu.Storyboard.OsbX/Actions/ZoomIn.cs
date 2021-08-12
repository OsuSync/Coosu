﻿using Coosu.Storyboard.Events;

namespace Coosu.Storyboard.OsbX.Actions
{
    public class ZoomIn : CommonEvent
    {
        public ZoomIn()
        {
        }

        public ZoomIn(EasingType easing, double startTime, double endTime, double[] start, double[] end) 
            : base(easing, startTime, endTime, start, end)
        {
        }

        public override EventType EventType => "ZI";
    }
}
