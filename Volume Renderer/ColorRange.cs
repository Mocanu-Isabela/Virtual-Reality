/*using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace VolumeRendering
{
    internal class ColorRange : IComparable<ColorRange>
    {
        public Color Color { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public double AlphaFactor { get; set; }
        public Material Material { get; set; }

        public ColorRange(Color color, int minValue, int maxValue, double alphaFactor, Material material)
        {
            Color = color;
            MinValue = minValue;
            MaxValue = maxValue;
            AlphaFactor = alphaFactor;
            Material = material;
        }

        public static bool operator <=(ColorRange a, ColorRange b)
        {
            return a.MinValue <= b.MinValue;
        }

        public static bool operator <(ColorRange a, ColorRange b)
        {
            return a.MinValue < b.MinValue;
        }

        public static bool operator >(ColorRange a, ColorRange b)
        {
            return a.MinValue > b.MinValue;
        }

        public static bool operator >=(ColorRange a, ColorRange b)
        {
            return a.MinValue >= b.MinValue;
        }

        public static bool operator ==(ColorRange a, ColorRange b)
        {
            return a.MinValue == b.MinValue && a.MaxValue == b.MaxValue;
        }

        public static bool operator !=(ColorRange a, ColorRange b)
        {
            return a.MinValue != b.MinValue;
        }

        public int CompareTo([AllowNull] ColorRange other)
        {
            if (this < other)
            {
                return -1;
            }
            if (this > other)
            {
                return 1;
            }
            return 0;
        }
    }
}
*/