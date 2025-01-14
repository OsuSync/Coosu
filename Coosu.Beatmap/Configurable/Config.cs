﻿namespace Coosu.Beatmap.Configurable;

public abstract class Config
{
    public abstract void HandleCustom(string line);

    public virtual void OnDeserialized()
    {
    }

    [SectionIgnore]
    public ReadOptions Options { get; set; } = null!;
}