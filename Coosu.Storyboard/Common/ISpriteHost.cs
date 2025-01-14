﻿using System;
using System.Collections.Generic;

namespace Coosu.Storyboard.Common;

public interface ISpriteHost : IScriptable, ICloneable, IEnumerable<Sprite>
{
    double MaxTime();
    double MinTime();
    double MaxStartTime();
    double MinEndTime();
    IList<Sprite> Sprites { get; }
    IList<ISpriteHost> SubHosts { get; }
    Camera2 Camera2 { get; }
    void AddSprite(Sprite sprite);
    void AddSubHost(ISpriteHost spriteHost);
    ISpriteHost? BaseHost { get; }
}