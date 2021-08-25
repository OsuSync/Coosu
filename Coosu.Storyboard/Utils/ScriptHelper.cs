﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Coosu.Storyboard.Common;

namespace Coosu.Storyboard.Utils
{
    public static class ScriptHelper
    {
        public static async Task WriteGroupedEventAsync(TextWriter writer, IEnumerable<ICommonEvent> events, int index)
        {
            var indent = new string(' ', index);
            var groupedEvents = events
                .OrderBy(k => k.EventType)
                .ThenBy(k => k.EndTime)
                .ThenBy(k => k.EventType.Index)
                .GroupBy(k => k.EventType);
            foreach (var grouping in groupedEvents)
                foreach (ICommonEvent e in grouping)
                {
                    await writer.WriteAsync(indent);
                    await e.WriteScriptAsync(writer);
                    await writer.WriteLineAsync();
                }
        }

        public static async Task WriteSequentialEventAsync(TextWriter writer, IEnumerable<ICommonEvent> events, int index)
        {
            var indent = new string(' ', index);
            foreach (ICommonEvent e in events
                .OrderBy(k => k.StartTime)
                .ThenBy(k => k.EndTime)
                .ThenBy(k => k.EventType.Index))
            {
                await writer.WriteAsync(indent);
                await e.WriteScriptAsync(writer);
                await writer.WriteLineAsync();
            }
        }

        public static async Task WriteHostEventsAsync(TextWriter writer, IDetailedEventHost host, bool group)
        {
            if (group)
                await WriteGroupedEventAsync(writer, host.Events, 1);
            else
                await WriteSequentialEventAsync(writer, host.Events, 1);
        }

        public static async Task WriteElementEventsAsync(TextWriter writer, ISceneObject sceneObject, bool group)
        {
            var sprite = sceneObject as Sprite;
            if (sprite != null)
                foreach (var loop in sprite.LoopList)
                    await WriteSubEventHostAsync(writer, loop, @group);

            if (group)
                await WriteGroupedEventAsync(writer, sceneObject.Events, 1);
            else
                await WriteSequentialEventAsync(writer, sceneObject.Events, 1);

            if (sprite != null)
                foreach (var trigger in sprite.TriggerList)
                    await WriteSubEventHostAsync(writer, trigger, @group);
        }

        public static async Task WriteSubEventHostAsync(TextWriter writer, ISubEventHost subEventHost, bool group)
        {
            await writer.WriteAsync(' ');
            await subEventHost.WriteHeaderAsync(writer);
            await writer.WriteLineAsync();

            if (group)
                await WriteGroupedEventAsync(writer, subEventHost.Events, 2);
            else
                await WriteSequentialEventAsync(writer, subEventHost.Events, 2);
        }
    }
}
