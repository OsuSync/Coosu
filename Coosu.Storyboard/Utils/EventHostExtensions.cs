﻿using System;
using System.Collections.Generic;
using System.Linq;
using Coosu.Storyboard.Common;

namespace Coosu.Storyboard.Utils
{
    public static class EventHostExtensions
    {
        public static void Adjust(this IEventHost eventHost, double offsetX, double offsetY, double offsetTiming)
        {
            if (eventHost is ISceneObject iso)
            {
                iso.DefaultX += offsetX;
                iso.DefaultY += offsetY;

                foreach (var loop in iso.LoopList)
                    loop.Adjust(offsetX, offsetY, offsetTiming);

                foreach (var trigger in iso.TriggerList)
                    trigger.Adjust(offsetX, offsetY, offsetTiming);
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

        public static int GetMaxTimeCount(this ISceneObject eventHost)
        {
            var maxTime = eventHost.MaxTime;
            return eventHost.Events.Count(k => k.EndTime.Equals(maxTime)) +
                   eventHost.LoopList.Count(k => k.OuterMaxTime.Equals(maxTime)) +
                   eventHost.TriggerList.Count(k => k.MaxTime.Equals(maxTime));
        }

        public static int GetMinTimeCount(this ISceneObject eventHost)
        {
            var minTime = eventHost.MinTime;
            return eventHost.Events.Count(k => k.StartTime.Equals(minTime)) +
                   eventHost.LoopList.Count(k => k.OuterMinTime.Equals(minTime)) +
                   eventHost.TriggerList.Count(k => k.MinTime.Equals(minTime));
        }

        public static int GetMaxTimeCount(this IEventHost eventHost)
        {
            return eventHost.Events.Count(k => k.EndTime.Equals(eventHost.MaxTime));
        }

        public static int GetMinTimeCount(this IEventHost eventHost)
        {
            return eventHost.Events.Count(k => k.StartTime.Equals(eventHost.MinTime));
        }

        public static double[] ComputeFrame(this IEventHost eventHost, EventType eventType, double time)
        {
            if (eventType.Size < 1) throw new ArgumentOutOfRangeException(nameof(eventType), eventType, "Only support sized event type.");
            var commonEvents = eventHost.Events.Where(k => k.EventType == eventType).ToList();
            if (commonEvents.Count == 0)
                return eventType.GetDefaultValue() ?? throw new NotSupportedException(eventType.Flag + " doesn't have any default value.");

            if (time < commonEvents[0].StartTime)
                return commonEvents[0].Start.ToArray();

            var e = commonEvents.FirstOrDefault(k => k.StartTime <= time && k.EndTime > time);
            if (e != null) return e.ComputeFrame(time);

            var lastE = commonEvents.Last(k => k.EndTime <= time);
            return lastE.End.ToArray();
        }
    }
}