/*using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace VolumeRendering
{
    internal class ColorMap
    {
        SortedSet<ColorRange> ranges;

        public ColorMap()
        {
            ranges = new SortedSet<ColorRange>();
            ranges.Add(new ColorRange(
                new Color(0.2, 0.3, 0.4, 0.7),
                0,
                19,
                1,
                new Material(
                        new Color(0.5, 0.6, 0.2, 1.0),
                        new Color(0.3, 0.6, 0.4, 1.0),
                        new Color(0.2, 0.6, 0.6, 1.0),
                        1
                    )));
            ranges.Add(new ColorRange(
                new Color(0.1, 0.3, 0.5, 1),
                19,
                50,
                0.5,
                new Material(
                    new Color(0.5, 0.6, 0.2, 1.0),
                    new Color(0.3, 0.6, 0.4, 1.0),
                    new Color(0.2, 0.6, 0.6, 1.0),
                    1
                )));
            ranges.Add(new ColorRange(
                new Color(0.15, 0.3, 0.5, 1),
                50,
                80,
                0.45,
                new Material(
                        new Color(0.5, 0.6, 0.2, 1.0),
                        new Color(0.3, 0.6, 0.4, 1.0),
                        new Color(0.2, 0.6, 0.6, 1.0),
                        1
                    )));
            ranges.Add(new ColorRange(
                new Color(0.2, 0.3, 0.5, 1),
                80,
                110,
                0.4,
                new Material(
                        new Color(0.5, 0.6, 0.2, 1.0),
                        new Color(0.3, 0.6, 0.4, 1.0),
                        new Color(0.2, 0.6, 0.6, 1.0),
                        1
                    )));
            ranges.Add(new ColorRange(
                new Color(0.4, 0.3, 0.5, 1),
                110,
                130,
                0.38,
                new Material(
                        new Color(0.5, 0.6, 0.2, 1.0),
                        new Color(0.3, 0.6, 0.4, 1.0),
                        new Color(0.2, 0.6, 0.6, 1.0),
                        1
                    )));
            ranges.Add(new ColorRange(
                new Color(0.5, 0.3, 0.5, 1),
                130,
                150,
                0.32,
                new Material(
                        new Color(0.5, 0.6, 0.2, 1.0),
                        new Color(0.3, 0.6, 0.4, 1.0),
                        new Color(0.2, 0.6, 0.6, 1.0),
                        1
                    )));
            ranges.Add(new ColorRange(
                new Color(0.6, 0.3, 0.5, 1),
                150,
                175,
                0.24,
                new Material(
                        new Color(0.5, 0.6, 0.2, 1.0),
                        new Color(0.3, 0.6, 0.4, 1.0),
                        new Color(0.2, 0.6, 0.6, 1.0),
                        1
                    )));
            ranges.Add(new ColorRange(
                new Color(0.7, 0.3, 0.5, 1),
                175,
                190,
                0.1,
                new Material(
                        new Color(0.5, 0.6, 0.2, 1.0),
                        new Color(0.3, 0.6, 0.4, 1.0),
                        new Color(0.2, 0.6, 0.6, 1.0),
                        1
                    )));
            ranges.Add(new ColorRange(
                new Color(0.8, 0.3, 0.5, 1),
                190,
                220,
                0.06,
                new Material(
                        new Color(0.8, 0.3, 0.3, 1.0),
                        new Color(0.8, 0.3, 0.5, 1.0),
                        new Color(0.2, 0.6, 0.6, 1.0),
                        1
                    )));
            ranges.Add(new ColorRange(
                new Color(0.9, 0.3, 0.5, 1),
                220,
                256,
                0.03,
                new Material(
                        new Color(0.5, 0.6, 0.2, 1.0),
                        new Color(0.3, 0.6, 0.4, 1.0),
                        new Color(0.2, 0.6, 0.6, 1.0),
                        1
                    )));
        }

        public ColorRange getColorRangeByValue(double value)
        {
            ColorRange colorRange = ranges.FirstOrDefault(elem => elem.MinValue / 255.0 <= value && elem.MaxValue / 255.0 > value);
            return colorRange;
        }
    }
}
*/