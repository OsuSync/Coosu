﻿using System;
using System.Diagnostics;

namespace Coosu.Beatmap.Extensions.Playback;

[DebuggerDisplay("{DebuggerDisplay}")]
public class PlayableNode : HitsoundNode
{
    /// <summary>
    /// Object identifier
    /// </summary>
    public Guid Guid { get; set; }
    public PlayablePriority PlayablePriority { get; set; }

    public string DebuggerDisplay => $"PL{(UseUserSkin ? "D" : "")}:{Offset}: " +
                                     $"P{(int)PlayablePriority}: " +
                                     $"V{(Volume * 10):#.#}: " +
                                     $"B{(Balance * 10):#.#}: " +
                                     $"{(Filename)}";
}