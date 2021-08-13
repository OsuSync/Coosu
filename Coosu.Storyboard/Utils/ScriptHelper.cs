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
            var groupedEvents = events.OrderBy(k => k.EventType).GroupBy(k => k.EventType);
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
            foreach (ICommonEvent e in events)
            {
                await writer.WriteAsync(indent);
                await e.WriteScriptAsync(writer);
                await writer.WriteLineAsync();
            }
        }

        public static async Task WriteHostEventsAsync(TextWriter writer, IEventHost host, bool group)
        {
            if (group)
                await WriteGroupedEventAsync(writer, host.Events, 1);
            else
                await WriteSequentialEventAsync(writer, host.Events, 1);
        }

        public static async Task WriteElementEventsAsync(TextWriter writer, ISceneObject sprite, bool group)
        {
            if (group)
                await WriteGroupedEventAsync(writer, sprite.Events, 1);
            else
                await WriteSequentialEventAsync(writer, sprite.Events, 1);

            foreach (var loop in sprite.LoopList)
                await WriteSubEventHostAsync(writer, loop, @group);
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
