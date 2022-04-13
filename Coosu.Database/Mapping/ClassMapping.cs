﻿using System.Collections.Generic;

namespace Coosu.Database.Mapping;

internal class ClassMapping : IMapping
{
    public int CurrentMemberIndex { get; set; } = -1;
    public Dictionary<string, IMapping> Mapping { get; set; } = new();
}