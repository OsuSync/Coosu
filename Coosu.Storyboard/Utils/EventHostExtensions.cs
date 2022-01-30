﻿using System.Linq;
using Coosu.Storyboard.Common;

// ReSharper disable once CheckNamespace
namespace Coosu.Storyboard
{
    public static class EventHostExtensions
    {
        public static void Adjust(this IDetailedEventHost eventHost, float offsetX, float offsetY, float offsetTiming)
        {
            if (eventHost is ISceneObject iso)
            {
                iso.DefaultX += offsetX;
                iso.DefaultY += offsetY;

                if (iso is Sprite sprite)
                {
                    foreach (var loop in sprite.LoopList)
                        loop.Adjust(offsetX, offsetY, offsetTiming);

                    foreach (var trigger in sprite.TriggerList)
                        trigger.Adjust(offsetX, offsetY, offsetTiming);
                }
            }

            var events = eventHost.Events.GroupBy(k => k.EventType);
            foreach (var kv in events)
            {
                foreach (var e in kv)
                {
                    if (e is IPositionAdjustable adjustable)
                        adjustable.AdjustPosition(offsetX, offsetY);

                    e.AdjustTiming(offsetTiming);
                }
            }
        }
    }
}
