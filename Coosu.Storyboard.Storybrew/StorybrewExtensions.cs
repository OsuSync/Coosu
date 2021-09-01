﻿using System;
using System.Collections.Generic;
using System.Linq;
using Coosu.Storyboard.Advanced.UI;
using Coosu.Storyboard.Common;
using Coosu.Storyboard.Extensions.Optimizing;
using Coosu.Storyboard.Storybrew;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

// ReSharper disable once CheckNamespace
namespace Coosu.Storyboard
{
    /// <summary>
    /// Storybrew extension methods for Coosu.
    /// </summary>
    public static partial class StorybrewExtensions
    {
        /// <summary>
        /// Executing Coosu commands and optimizing automatically in storybrew.
        /// </summary>
        /// <param name="sprite">Specific Coosu <see cref="Sprite"/>.</param>
        /// <param name="layerName">Layer name.</param>
        /// <param name="brewObjectGenerator">Specific storybrew <see cref="StoryboardObjectGenerator"/>.</param>
        /// <param name="configureSettings">Configure compressing options.</param>
        public static void ExecuteBrew(this Sprite sprite, string layerName, StoryboardObjectGenerator brewObjectGenerator,
            Action<CompressOptions>? configureSettings = null)
        {
            ExecuteBrew(new Layer(layerName) { SceneObjects = new List<ISceneObject> { sprite } }, brewObjectGenerator, configureSettings);
        }

        /// <summary>
        /// Executing Coosu commands and optimizing automatically in storybrew.
        /// </summary>
        /// <param name="layer">Specific Coosu <see cref="Layer"/>.</param>
        /// <param name="brewObjectGenerator">Specific storybrew <see cref="StoryboardObjectGenerator"/>.</param>
        /// <param name="configureSettings">Configure compressing options.</param>
        public static void ExecuteBrew(this Layer layer, StoryboardObjectGenerator brewObjectGenerator,
            Action<CompressOptions>? configureSettings = null)
        {
            DelayExecuteContexts(layer, brewObjectGenerator);

            var brewLayer = brewObjectGenerator.GetLayer(layer.Name);
            void EventHandler(object _, ProcessErrorEventArgs e) => throw new Exception(e.Message);

            var compressor = new SpriteCompressor(layer);
            configureSettings?.Invoke(compressor.Options);

            compressor.ErrorOccured += EventHandler;
            compressor.CompressAsync().Wait();
            compressor.ErrorOccured -= EventHandler;
            if (layer.SceneObjects.Count == 0) return;

            layer.WriteScriptAsync(Console.Out).Wait();

            foreach (var sprite in layer.SceneObjects.Where(k => k is Sprite).Cast<Sprite>())
            {
                InnerExecuteBrew(sprite, brewLayer, false, configureSettings);
            }

            UiThreadHelper.Shutdown();
        }

        private static void InnerExecuteBrew(Sprite sprite, StoryboardLayer brewLayer,
            bool optimize, Action<CompressOptions>? configureSettings)
        {
            if (optimize)
            {
                void EventHandler(object _, ProcessErrorEventArgs e) => throw new Exception(e.Message);

                var sceneObjects = new List<ISceneObject> { sprite };
                var compressor = new SpriteCompressor(sceneObjects);
                configureSettings?.Invoke(compressor.Options);

                compressor.ErrorOccured += EventHandler;
                compressor.CompressAsync().Wait();
                compressor.ErrorOccured -= EventHandler;
                if (sceneObjects.Count == 0) return;
            }

            OsbSprite brewObj;
            if (sprite is Animation animation)
                brewObj = brewLayer.CreateAnimation(animation.ImagePath,
                    (int)animation.FrameCount,
                    (int)animation.FrameDelay,
                    StorybrewInteropHelper.ConvertLoopType(animation.LoopType),
                    StorybrewInteropHelper.ConvertOrigin(animation.OriginType),
                    new OpenTK.Vector2((float)animation.DefaultX,
                        (float)animation.DefaultY));
            else
                brewObj = brewLayer.CreateSprite(sprite.ImagePath,
                    StorybrewInteropHelper.ConvertOrigin(sprite.OriginType),
                    new OpenTK.Vector2((float)sprite.DefaultX, (float)sprite.DefaultY)
                );

            InnerExecuteBrew(sprite, brewObj);
            foreach (var l in sprite.LoopList)
            {
                brewObj.StartLoopGroup(l.StartTime, l.LoopCount);
                InnerExecuteBrew(l, brewObj);
                brewObj.EndGroup();
            }

            foreach (var t in sprite.TriggerList)
            {
                brewObj.StartTriggerGroup(t.TriggerName, t.StartTime, t.EndTime);
                InnerExecuteBrew(t, brewObj);
                brewObj.EndGroup();
            }
        }

        private static void InnerExecuteBrew(IEventHost eventHost, OsbSprite brewObj)
        {
            foreach (var keyEvent in eventHost.Events)
                StorybrewInteropHelper.ExecuteEvent(keyEvent, brewObj);
        }
    }
}