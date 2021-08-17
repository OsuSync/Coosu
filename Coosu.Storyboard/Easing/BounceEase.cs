﻿using System;

namespace Coosu.Storyboard.Easing
{
    /// <summary>
    ///     This class implements an easing function that can be used to simulate bouncing
    /// </summary>
    public class BounceEase : EasingFunctionBase
    {
        public BounceEase()
        {
        }

        /// <summary>
        /// Specifies the number of bounces.  This does not include the final half bounce.
        /// </summary>
        public int Bounces { get; init; } = 3;

        /// <summary>
        /// Specifies the amount of bounciness.  This corresponds to the scale difference between a bounce and the next bounce.  
        /// For example, Bounciness = 2.0 correspondes to the next bounce being twices as high and taking twice as long.
        /// </summary>
        public double Bounciness { get; init; } = 2d;

        protected override double EaseInCore(double normalizedTime)
        {
            // The math below is complicated because we have a few requirements to get the correct look for bounce:
            //  1) The bounces should be symetrical
            //  2) Bounciness should control both the amplitude and the period of the bounces
            //  3) Bounces should control the number of bounces without including the final half bounce to get you back to 1.0
            //
            //  Note: Simply modulating a expo or power curve with a abs(sin(...)) wont work because it violates 1) above.
            //

            // Constants
            double bounces = Math.Max(0.0, (double)Bounces);
            double bounciness = Bounciness;

            // Clamp the bounciness so we dont hit a divide by zero
            if (bounciness < 1.0 || DoubleUtil.IsOne(bounciness))
            {
                // Make it just over one.  In practice, this will look like 1.0 but avoid divide by zeros.
                bounciness = 1.001;
            }

            double pow = Math.Pow(bounciness, bounces);
            double oneMinusBounciness = 1.0 - bounciness;

            // 'unit' space calculations.
            // Our bounces grow in the x axis exponentially.  we define the first bounce as having a 'unit' width of 1.0 and compute
            // the total number of 'units' using a geometric series.
            // We then compute which 'unit' the current time is in.
            double sumOfUnits = (1.0 - pow) / oneMinusBounciness + pow * 0.5; // geometric series with only half the last sum
            double unitAtT = normalizedTime * sumOfUnits;

            // 'bounce' space calculations.
            // Now that we know which 'unit' the current time is in, we can determine which bounce we're in by solving the geometric equation:
            // unitAtT = (1 - bounciness^bounce) / (1 - bounciness), for bounce.
            double bounceAtT = Math.Log(-unitAtT * (1.0 - bounciness) + 1.0, bounciness);
            double start = Math.Floor(bounceAtT);
            double end = start + 1.0;

            // 'time' space calculations.
            // We then project the start and end of the bounce into 'time' space
            double startTime = (1.0 - Math.Pow(bounciness, start)) / (oneMinusBounciness * sumOfUnits);
            double endTime = (1.0 - Math.Pow(bounciness, end)) / (oneMinusBounciness * sumOfUnits);

            // Curve fitting for bounce.
            double midTime = (startTime + endTime) * 0.5;
            double timeRelativeToPeak = normalizedTime - midTime;
            double radius = midTime - startTime;
            double amplitude = Math.Pow(1.0 / bounciness, (bounces - start));

            // Evaluate a quadratic that hits (startTime,0), (endTime, 0), and peaks at amplitude.
            return (-amplitude / (radius * radius)) * (timeRelativeToPeak - radius) * (timeRelativeToPeak + radius);
        }

        public override EasingType? TryGetEasingType()
        {
            if (Bounces != 3 || !Bounciness.Equals(2d)) return null;

            return EasingMode switch
            {
                EasingMode.EaseIn => EasingType.BounceIn,
                EasingMode.EaseOut => EasingType.BounceOut,
                EasingMode.EaseInOut => EasingType.BounceInOut,
                _ => null
            };
        }

        public static BounceEase InstanceIn => new() { EasingMode = EasingMode.EaseIn };
        public static BounceEase InstanceOut => new() { EasingMode = EasingMode.EaseOut };
        public static BounceEase InstanceInOut => new() { EasingMode = EasingMode.EaseInOut };
    }
}