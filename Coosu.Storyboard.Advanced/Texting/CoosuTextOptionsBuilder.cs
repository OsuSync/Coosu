﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Coosu.Storyboard.Advanced.Texting
{
    public class CoosuTextOptionsBuilder
    {
        public CoosuTextOptionsBuilder WithIdentifier(string identifier)
        {
            Options.FileIdentifier = identifier;
            return this;
        }

        public CoosuTextOptionsBuilder Reverse()
        {
            Options.RightToLeft = !Options.RightToLeft;
            return this;
        }

        public CoosuTextOptionsBuilder ScaleXBy(double ratio)
        {
            Options.XScale = ratio;
            return this;
        }

        public CoosuTextOptionsBuilder ScaleYBy(double ratio)
        {
            Options.YScale = ratio;
            return this;
        }

        public CoosuTextOptionsBuilder WithWordGap(int pixelGap)
        {
            Options.WordGap = pixelGap;
            return this;
        }

        public CoosuTextOptionsBuilder WithLineGap(int pixelGap)
        {
            Options.LineGap = pixelGap;
            return this;
        }

        public CoosuTextOptionsBuilder WithVerticalLayout()
        {
            Options.Orientation = Orientation.Vertical;
            return this;
        }

        public CoosuTextOptionsBuilder WithItalic()
        {
            Options.FontStyle = FontStyles.Italic;
            return this;
        }

        /// <summary>
        /// Set the font weight.
        /// <para>The font weight can be set with the preset value by <see cref="FontWeights"/>.</para>
        /// </summary>
        /// <param name="openTypeWeight">The value should be between 1 to 999.</param>
        /// <returns></returns>
        public CoosuTextOptionsBuilder WithWeight(int openTypeWeight)
        {
            if (openTypeWeight < 1) openTypeWeight = 1;
            else if (openTypeWeight > 999) openTypeWeight = 999;
            return WithWeight(FontWeight.FromOpenTypeWeight(openTypeWeight));
        }

        /// <summary>
        /// Set the font weight.
        /// </summary>
        /// <param name="fontWeight"></param>
        /// <returns></returns>
        public CoosuTextOptionsBuilder WithWeight(FontWeight fontWeight)
        {
            Options.FontWeight = fontWeight;
            return this;
        }

        public CoosuTextOptionsBuilder WithFontSize(int fontSize)
        {
            Options.FontSize = fontSize;
            return this;
        }

        public CoosuTextOptionsBuilder WithFontFamily(string path, string fontFamilyName)
        {
            if (Options.IsInitialFamily)
            {
                Options.FontFamilies.Clear();
                Options.IsInitialFamily = false;
            }

            Options.FontFamilies.Add(FontFamilySource.FromFiles(path, fontFamilyName));
            return this;
        }

        public CoosuTextOptionsBuilder WithFontFamily(string fontFamilyName)
        {
            if (Options.IsInitialFamily)
            {
                Options.FontFamilies.Clear();
                Options.IsInitialFamily = false;
            }

            Options.FontFamilies.Add(new FontFamilySource(fontFamilyName));
            return this;
        }

        public CoosuTextOptionsBuilder FillBy(string hexColor)
        {
            Options.FillBrush = new BrushConverter().ConvertFrom(hexColor) as Brush ?? Brushes.White;
            return this;
        }

        public CoosuTextOptionsBuilder FillBy(byte a, byte r, byte g, byte b)
        {
            Options.FillBrush = new SolidColorBrush(Color.FromArgb(a, r, g, b));
            return this;
        }

        public CoosuTextOptionsBuilder FillBy(Brush brush)
        {
            Options.FillBrush = brush;
            return this;
        }

        public CoosuTextOptionsBuilder WithStroke(double strokeThickness)
        {
            Options.StrokeMode = OptionType.With;
            return StrokeMethod(Brushes.White, strokeThickness);
        }

        public CoosuTextOptionsBuilder WithStroke(string hexColor, double strokeThickness)
        {
            Options.StrokeMode = OptionType.With;
            return StrokeMethod(hexColor, strokeThickness);
        }

        public CoosuTextOptionsBuilder WithStroke(byte a, byte r, byte g, byte b, double strokeThickness)
        {
            Options.StrokeMode = OptionType.With;
            return StrokeMethod(a, r, g, b, strokeThickness);
        }

        public CoosuTextOptionsBuilder WithStroke(Brush brush, double strokeThickness)
        {
            Options.StrokeMode = OptionType.With;
            return StrokeMethod(brush, strokeThickness);
        }

        public CoosuTextOptionsBuilder WithStrokeOnly(double strokeThickness)
        {
            Options.StrokeMode = OptionType.Only;
            return StrokeMethod(Brushes.White, strokeThickness);
        }

        public CoosuTextOptionsBuilder WithStrokeOnly(string hexColor, double strokeThickness)
        {
            Options.StrokeMode = OptionType.Only;
            return StrokeMethod(hexColor, strokeThickness);
        }

        public CoosuTextOptionsBuilder WithStrokeOnly(byte a, byte r, byte g, byte b, double strokeThickness)
        {
            Options.StrokeMode = OptionType.Only;
            return StrokeMethod(a, r, g, b, strokeThickness);
        }

        public CoosuTextOptionsBuilder WithStrokeOnly(Brush brush, double strokeThickness)
        {
            Options.StrokeMode = OptionType.Only;
            return StrokeMethod(brush, strokeThickness);
        }

        public CoosuTextOptionsBuilder WithShadow(double blurRadius = 5, double direction = -45, double depth = 5)
        {
            Options.ShadowMode = OptionType.With;
            return ShadowMethod(Brushes.White, blurRadius, direction, depth);
        }

        public CoosuTextOptionsBuilder WithShadow(string hexColor,
            double blurRadius = 5, double direction = -45, double depth = 5)
        {
            Options.ShadowMode = OptionType.With;
            return ShadowMethod(hexColor, blurRadius, direction, depth);
        }

        public CoosuTextOptionsBuilder WithShadow(byte a, byte r, byte g, byte b,
            double blurRadius = 5, double direction = -45, double depth = 5)
        {
            Options.ShadowMode = OptionType.With;
            return ShadowMethod(a, r, g, b, blurRadius, direction, depth);
        }

        public CoosuTextOptionsBuilder WithShadow(Brush brush,
            double blurRadius = 5, double direction = -45, double depth = 5)
        {
            Options.ShadowMode = OptionType.With;
            return ShadowMethod(brush, blurRadius, direction, depth);
        }

        public CoosuTextOptionsBuilder WithShadowOnly(double blurRadius = 5, double direction = -45, double depth = 5)
        {
            Options.ShadowMode = OptionType.Only;
            return ShadowMethod(Brushes.White, blurRadius, direction, depth);
        }

        public CoosuTextOptionsBuilder WithShadowOnly(string hexColor,
            double blurRadius = 5, double direction = -45, double depth = 5)
        {
            Options.ShadowMode = OptionType.Only;
            return ShadowMethod(hexColor, blurRadius, direction, depth);
        }

        public CoosuTextOptionsBuilder WithShadowOnly(byte a, byte r, byte g, byte b,
            double blurRadius = 5, double direction = -45, double depth = 5)
        {
            Options.ShadowMode = OptionType.Only;
            return ShadowMethod(a, r, g, b, blurRadius, direction, depth);
        }

        public CoosuTextOptionsBuilder WithShadowOnly(Brush brush,
            double blurRadius = 5, double direction = -45, double depth = 5)
        {
            Options.ShadowMode = OptionType.Only;
            return ShadowMethod(brush, blurRadius, direction, depth);
        }

        private CoosuTextOptionsBuilder ShadowMethod(string hexColor, double blurRadius, double direction, double depth)
        {
            Options.ShadowBrush = new BrushConverter().ConvertFrom(hexColor) as Brush ?? Brushes.Black;
            Options.ShadowBlurRadius = blurRadius;
            Options.ShadowDirection = direction;
            Options.ShadowDepth = depth;
            return this;
        }

        private CoosuTextOptionsBuilder ShadowMethod(byte a, byte r, byte g, byte b, double blurRadius, double direction, double depth)
        {
            Options.ShadowBrush = new SolidColorBrush(Color.FromArgb(a, r, g, b));
            Options.ShadowBlurRadius = blurRadius;
            Options.ShadowDirection = direction;
            Options.ShadowDepth = depth;
            return this;
        }

        private CoosuTextOptionsBuilder ShadowMethod(Brush brush, double blurRadius, double direction, double depth)
        {
            Options.ShadowBrush = brush;
            Options.ShadowBlurRadius = blurRadius;
            Options.ShadowDirection = direction;
            Options.ShadowDepth = depth;
            return this;
        }

        private CoosuTextOptionsBuilder StrokeMethod(string hexColor, double strokeThickness)
        {
            Options.StrokeBrush = new BrushConverter().ConvertFrom(hexColor) as Brush ?? Brushes.Black;
            Options.StrokeThickness = strokeThickness;
            return this;
        }

        private CoosuTextOptionsBuilder StrokeMethod(byte a, byte r, byte g, byte b, double strokeThickness)
        {
            Options.StrokeBrush = new SolidColorBrush(Color.FromArgb(a, r, g, b));
            Options.StrokeThickness = strokeThickness;
            return this;
        }

        private CoosuTextOptionsBuilder StrokeMethod(Brush brush, double strokeThickness)
        {
            Options.StrokeBrush = brush;
            Options.StrokeThickness = strokeThickness;
            return this;
        }

        public CoosuTextOptions Options { get; } = new()
        {
            FileIdentifier = "default",
        };
    }
}