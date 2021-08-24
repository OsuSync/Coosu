﻿using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Coosu.Storyboard;
using Coosu.Storyboard.Easing;
using Coosu.Storyboard.Extensions;
using Coosu.Storyboard.Extensions.Optimizing;
using Coosu.Storyboard.Utils;

namespace CoosuTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var layer = new Layer();
            //layer.Camera.RotateBy(startTime: 0, endTime: 500, degree: 90);
            //layer.Camera.MoveBy(startTime: 0, endTime: 500, x: 300, y: 30);

            //            layer = Layer.ParseFromText(@"
            //Sprite,Foreground,Centre,""sb\cg\waifu.png"",320,240
            // MX,0,4960,4992,342.24,344.448
            // MX,0,4992,5000,344.448,345
            // MX,0,5000,5024,345,345.12
            // MX,0,5024,5056,345.12,345.28
            // MX,0,5056,5088,345.28,345.44
            //");
            var r = new Random();
            Func<double, double, double> Random = (x, y) => { return r.NextDouble() * (y - x) + x; };
            for (int i = 0; i < 10000; i++)
            {
                var sprite = layer.CreateSprite("one_plane.jpg");
                sprite.Move(new PowerEase()
                {
                    Power = 10,
                    EasingMode = EasingMode.EaseOut
                }, 0, 3000, -50 + Random(-50, 50), 50 + Random(-50, 50), 400 + Random(-50, 50), 250 + Random(-50, 50));

                sprite.MoveBy(new PowerEase()
                {
                    Power = 10,
                    EasingMode = EasingMode.EaseInOut
                }, 0, 3000 + Random(-1000, 1000), 400 + Random(-50, 50), 150 + Random(-50, 50));
                sprite.MoveBy(0, 4000, -100, -100);
            }

            //await layer.WriteScriptAsync(Console.Out);
            //Console.WriteLine("==================");
            string? preP = null;
            var compressor = new SpriteCompressor(layer, new CompressSettings
            {
                ThreadCount = Environment.ProcessorCount + 1
            });

            compressor.ErrorOccured += (s, e) =>
            {
                //Console.WriteLine(e.SourceSprite.GetHeaderString() + ": " + e.Message);
                //Console.ReadLine();
            };
            compressor.ProgressChanged += (s, e) =>
            {
                //var p = (e.Progress / (double)e.TotalCount).ToString("P0");
                //if (preP == p) return;
                //Console.WriteLine(p);
                //preP = p;
            };
            var canceled = await compressor.CompressAsync();
            await layer.WriteScriptAsync(Console.Out);
            return;
            var text = File.ReadAllText(
                "C:\\Users\\milkitic\\Downloads\\" +
                "1037741 Denkishiki Karen Ongaku Shuudan - Gareki no Yume\\" +
                "Denkishiki Karen Ongaku Shuudan - Gareki no Yume (Dored).osb"
            );
            await OutputNewOsb(text);
            //await OutputOldOsb(text);
        }

        private static async Task OutputNewOsb(string text)
        {
            var layer = await Layer.ParseAsyncTextAsync(text);
            var g = new SpriteCompressor(layer);
            //g.SituationFound += (s, e) => { Console.WriteLine(e.Message); };
            g.ErrorOccured += (s, e) => { Console.WriteLine(e.Message); };
            await g.CompressAsync();
            await layer.SaveScriptAsync(Path.Combine(Environment.CurrentDirectory, "output_new.osb"));
        }

        private static async Task OutputOldOsb(string text)
        {
#if NET5_0_OR_GREATER
            var ctx = new System.Runtime.Loader.AssemblyLoadContext("old", false);
            var path = Path.Combine(Environment.CurrentDirectory, @"..\..\..\V1.0.0.0\Coosu.Storyboard.dll");
            var asm = ctx.LoadFromAssemblyPath(path);
            //ctx.Unload();
            var egT = asm.GetType("Coosu.Storyboard.Management.ElementGroup");
            var method = egT?.GetMethod("ParseFromText", BindingFlags.Static | BindingFlags.Public);
            var eg = method?.Invoke(null, new object?[] { text });
            var type = eg?.GetType();
            var compressorT = asm.GetType("Coosu.Storyboard.Management.ElementCompressor");
            var compressor = (dynamic)Activator.CreateInstance(compressorT, eg);
            var task1 = (Task)compressor.CompressAsync();
            await task1;
            var methodSave = type?.GetMethod("SaveOsbFileAsync");
            var task = (Task)methodSave.Invoke(eg,
                new object?[] { Path.Combine(Environment.CurrentDirectory, "output_old.osb") });
            await task;
#endif
        }
    }
}
