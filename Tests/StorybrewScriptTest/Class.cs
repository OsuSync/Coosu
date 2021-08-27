﻿using Coosu.Storyboard;
using Coosu.Storyboard.Easing;
using Newtonsoft.Json.Linq;
using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;


namespace StorybrewScriptTest
{
    public class Class : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            var layer = new Layer("coosu default layer");
            layer.Camera2.Scale(12345, 2);
            layer.Camera2.ScaleBy(EasingType.QuartOut, 12345, 15345, 1);

            SpriteGroup textGroup = layer.CreateText("lyric here... owo",
                12345,
                320, 240,
                OriginType.Centre,
                options => options
                    .WithIdentifier("style1")
                    .WithFontFamily("Consolas")
                    .WithFontFamily("SimHei")
                    .WithFontSize(48)
                    .WithWordGap(5)
                    .WithLineGap(10)
                    .ScaleXBy(0.7)
                    .Reverse()
                    .FillBy("#4322145")
                    .WithStroke("#FF13145", 5)
                    .WithShadow("#000000", 5, -60, 4));

            textGroup.MoveYBy(0, 12345, 15123, -30);
            textGroup.Rotate(12345, 0.5);
            textGroup.RotateBy(0, 12345, 15123, -1);

            SpriteGroup subGroup = textGroup.CreateText("easy lyric", 12345, 0, 0);
            var baseGroup = subGroup.BaseHost as SpriteGroup;

            Assert(baseGroup == textGroup);

            int i = 0;
            foreach (var sprite in textGroup)
            {
                var y = i % 2 == 0 ? 80 : -80;
                sprite.MoveY(new PowerEase
                {
                    EasingMode = EasingMode.EaseOut,
                    Power = 32
                }, 12345, 15123, sprite.DefaultY + y, sprite.DefaultY);
                i++;
            }

            layer.ExecuteBrew(this);
        }
    }
}
