﻿namespace Coosu.Storyboard.Easing
{
    public class LinearEase : EasingFunctionBase
    {
        protected override double EaseInCore(double normalizedTime)
        {
            return normalizedTime;
        }

        public override EasingType? TryGetEasingType() => EasingType.Linear;

        public static LinearEase Instance { get; } = new();
    }
}