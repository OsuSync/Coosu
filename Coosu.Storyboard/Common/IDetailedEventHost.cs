﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Coosu.Storyboard.Common
{
    public interface IEventHost : IScriptable
    {
        IReadOnlyCollection<IKeyEvent> Events { get; }
        void AddEvent(IKeyEvent @event);
        bool RemoveEvent(IKeyEvent @event);
        void ClearEvents(IComparer<IKeyEvent>? comparer);
    }

    public interface IDetailedEventHost : IScriptable, ICloneable, IEventHost
    {
        float MaxTime();
        float MinTime();
        float MaxStartTime();
        float MinEndTime();

        bool EnableGroupedSerialization { get; set; }
    }
}