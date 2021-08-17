﻿using System.IO;
using System.Threading.Tasks;
using Coosu.Storyboard.Easing;
using Coosu.Storyboard.Utils;

namespace Coosu.Storyboard.Events
{
    public sealed class Parameter : CommonEvent
    {
        public override EventType EventType => EventTypes.Parameter;

        public override int ParamLength => 1;
        public override bool IsStatic => true;

        public ParameterType Type
        {
            get => (ParameterType)(int)Start[0];
            set
            {
                Start[0] = (int)value;
                End[0] = (int)value;
            }
        }

        protected override async Task WriteExtraScriptAsync(TextWriter writer)
        {
            await writer.WriteAsync(Type.ToShortString());
        }

        public Parameter(double startTime, double endTime, ParameterType type) :
            base(EasingType.Linear.ToEasingFunction(), startTime, endTime,
                new double[] { (int)type }, new double[] { (int)type })
        {
            Easing = EasingType.Linear.ToEasingFunction();
        }
    }
}
