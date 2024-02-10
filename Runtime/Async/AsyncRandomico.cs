using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobilas.Unity.Utility {
    using SYSRandom = System.Random;
    public static class AsyncRandomico {
        private static SYSRandom random = new SYSRandom();

        /// <summary>Returns a random number between 0.0 [inclusive] and 1.0 [inclusive] (Read Only).</summary>
        public static double value => random.NextDouble();
        /// <summary>Less than <c>0.5f</c> is false, greater than <c>0.5f</c> is true.(<c><seealso cref="CobilasRandom"/>.value > 0.5f</c>)</summary>
        public static bool BooleanRandom => value > .5f;

        public static void SetSeed(int seed)
            => random = new SYSRandom(seed);

        public static byte ByteRange(byte min, byte max)
            => (byte)UlongRange(min, max);

        public static byte ByteRange(byte max)
            => (byte)UlongRange(byte.MinValue, max);

        public static byte ByteRange()
            => (byte)UlongRange(byte.MinValue, byte.MaxValue);

        public static ushort UshortRange(ushort min, ushort max)
            => (ushort)UlongRange(min, max);

        public static ushort UshortRange(ushort max)
            => (ushort)UlongRange(ushort.MinValue, max);

        public static ushort UshortRange()
            => (ushort)UlongRange(ushort.MinValue, ushort.MaxValue);

        public static uint UintRange(uint min, uint max)
            => (uint)UlongRange(min, max);

        public static uint UintRange(uint max)
            => (uint)UlongRange(uint.MinValue, max);

        public static uint UintRange()
            => (uint)UlongRange(uint.MinValue, uint.MaxValue);

        public static ulong UlongRange(ulong min, ulong max)
            => (ulong)DecimalRange(min, max);

        public static ulong UlongRange(ulong max)
            => UlongRange(ulong.MinValue, max);

        public static ulong UlongRange()
            => UlongRange(ulong.MinValue, ulong.MaxValue);

        public static sbyte SbyteRange(sbyte min, sbyte max)
            => (sbyte)LongRange(min, max);

        public static sbyte SbyteRange(sbyte max)
            => (sbyte)LongRange(sbyte.MinValue, max);

        public static sbyte SbyteRange()
            => (sbyte)LongRange(sbyte.MinValue, sbyte.MaxValue);

        public static short ShortRange(short min, short max)
            => (short)LongRange(min, max);

        public static short ShortRange(short max)
            => (short)LongRange(short.MinValue, max);

        public static short ShortRange()
            => (short)LongRange(short.MinValue, short.MaxValue);

        public static int IntRange(int min, int max) => random.Next(min, max);

        public static int IntRange(int max) => IntRange(int.MinValue, max);

        public static int IntRange() => IntRange(int.MaxValue);

        public static long LongRange(long min, long max)
            => (long)DecimalRange(min, max);

        public static long LongRange(long max)
            => LongRange(long.MinValue, max);

        public static long LongRange()
            => LongRange(long.MinValue, long.MaxValue);

        public static float FloatRange(float min, float max) => (float)DoubleRange(min, max);

        public static float FloatRange(float max) => FloatRange(float.MinValue, max);

        public static float FloatRange() => FloatRange(float.MaxValue);

        public static double DoubleRange(double min, double max) {
            double smin;
            double compri;
            if ((min < 0d && max > 0d) || (min > 0d && max < 0d)) {
                double minPorc = min < 0d ? min / double.MinValue : min / double.MaxValue;
                double maxPorc = max < 0d ? max / double.MinValue : max / double.MaxValue;
                minPorc = min < 0d ? -minPorc : minPorc;
                maxPorc = max < 0d ? -maxPorc : maxPorc;
                compri = Math.Abs(minPorc - maxPorc);
                smin = minPorc < maxPorc ? minPorc : maxPorc;

                compri = smin + ((double)value * compri);

                if (compri < 0d) return -compri * double.MinValue;
                else if (compri > 0d) return compri * double.MaxValue;
                else return 0d;
            }
            compri = Math.Abs(min - max);
            smin = min < max ? min : max;
            return smin + ((double)value * compri);
        }

        public static double DoubleRange(double max) => DoubleRange(double.MinValue, max);

        public static double DoubleRange() => DoubleRange(double.MaxValue);

        public static decimal DecimalRange(decimal min, decimal max) {
            decimal smin;
            decimal compri;
            if ((min < 0m && max > 0m) || (min > 0m && max < 0m)) {
                decimal minPorc = min < 0m ? min / decimal.MinValue : min / decimal.MaxValue;
                decimal maxPorc = max < 0m ? max / decimal.MinValue : max / decimal.MaxValue;
                minPorc = min < 0m ? -minPorc : minPorc;
                maxPorc = max < 0m ? -maxPorc : maxPorc;
                compri = Math.Abs(minPorc - maxPorc);
                smin = minPorc < maxPorc ? minPorc : maxPorc;

                compri = smin + ((decimal)value * compri);

                if (compri < 0m) return -compri * decimal.MinValue;
                else if (compri > 0m) return compri * decimal.MaxValue;
                else return decimal.Zero;
            }
            compri = Math.Abs(min - max);
            smin = min < max ? min : max;
            return smin + ((decimal)value * compri);
        }

        public static decimal DecimalRange(decimal max) => DecimalRange(decimal.MinValue, max);

        public static decimal DecimalRange() => DecimalRange(decimal.MaxValue);
    }
}
