using System;
using UnityEngine;

namespace Cobilas.Unity.Utility {
    using UERandom = UnityEngine.Random;
    public static class Randomico {

        /// <summary>Returns a random rotation (Read Only).</summary>
        public static Quaternion rotation => UERandom.rotation;
        /// <summary>Returns a random point on the surface of a sphere with radius 1 (Read Only).</summary>
        public static Vector3 onUnitSphere => UERandom.onUnitSphere;
        /// <summary>Returns a random point inside a circle with radius 1 (Read Only).</summary>
        public static Vector2 insideUnitCircle => UERandom.insideUnitCircle;
        /// <summary>Returns a random point inside a sphere with radius 1 (Read Only).</summary>
        public static Vector3 insideUnitSphere => UERandom.insideUnitSphere;
        /// <summary>Returns a random rotation with uniform distribution (Read Only).</summary>
        public static Quaternion rotationUniform => UERandom.rotationUniform;
        /// <summary>Gets/Sets the full internal state of the random number generator.</summary>
        public static UERandom.State state { get => UERandom.state; set => UERandom.state = value; }
        /// <summary>Returns a random number between 0.0 [inclusive] and 1.0 [inclusive] (Read Only).</summary>
        public static float value => UERandom.value;
        /// <summary>Less than <c>0.5f</c> is false, greater than <c>0.5f</c> is true.(<c><seealso cref="CobilasRandom"/>.value > 0.5f</c>)</summary>
        public static bool BooleanRandom => value > .5f;

        /// <summary>Initializes the random number generator state with a seed.</summary>
        /// <param name="seed">Seed used to initialize the random number generator.</param>
        public static void InitState(int seed)
            => UERandom.InitState(seed);

        /// <summary>Generates a random color from HSV and alpha ranges.</summary>
        /// <param name="hueMin">Minimum hue [0..1].</param>
        /// <param name="hueMax">Maximum hue [0..1].</param>
        /// <param name="saturationMin">Minimum saturation [0..1].</param>
        /// <param name="saturationMax">Maximum saturation[0..1].</param>
        /// <param name="valueMin">Minimum value [0..1].</param>
        /// <param name="valueMax">Maximum value [0..1].</param>
        /// <param name="alphaMin">Minimum alpha [0..1].</param>
        /// <param name="alphaMax">Maximum alpha [0..1].</param>
        /// <returns>A random color with HSV and alpha values in the input ranges.</returns>
        public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax, float alphaMin, float alphaMax)
            => UERandom.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax, alphaMin, alphaMax);

        /// <summary>Generates a random color from HSV and alpha ranges.</summary>
        /// <param name="hueMin">Minimum hue [0..1].</param>
        /// <param name="hueMax">Maximum hue [0..1].</param>
        /// <param name="saturationMin">Minimum saturation [0..1].</param>
        /// <param name="saturationMax">Maximum saturation[0..1].</param>
        /// <param name="valueMin">Minimum value [0..1].</param>
        /// <param name="valueMax">Maximum value [0..1].</param>
        /// <returns>A random color with HSV and alpha values in the input ranges.</returns>
        public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax)
            => UERandom.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax);

        /// <summary>Generates a random color from HSV and alpha ranges.</summary>
        /// <param name="hueMin">Minimum hue [0..1].</param>
        /// <param name="hueMax">Maximum hue [0..1].</param>
        /// <param name="saturationMin">Minimum saturation [0..1].</param>
        /// <param name="saturationMax">Maximum saturation[0..1].</param>
        /// <returns>A random color with HSV and alpha values in the input ranges.</returns>
        public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax)
            => UERandom.ColorHSV(hueMin, hueMax, saturationMin, saturationMax);

        /// <summary>Generates a random color from HSV and alpha ranges.</summary>
        /// <param name="hueMin">Minimum hue [0..1].</param>
        /// <param name="hueMax">Maximum hue [0..1].</param>
        /// <returns>A random color with HSV and alpha values in the input ranges.</returns>
        public static Color ColorHSV(float hueMin, float hueMax)
            => UERandom.ColorHSV(hueMin, hueMax);

        /// <summary>Generates a random color from HSV and alpha ranges.</summary>
        /// <returns>A random color with HSV and alpha values in the input ranges.</returns>
        public static Color ColorHSV()
            => UERandom.ColorHSV();

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

        public static ulong UlongRange(ulong min, ulong max) {
            float dmin = min < 0 ? 0 : min;
            float dmax = max < 0 ? 0 : max;
            ulong compri = (ulong)Math.Abs(dmin - dmax);
            ulong smin = min < max ? min : max;
            return (ulong)(smin + (value * compri));
        }

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

        /// <summary>Return a random integer number between min [inclusive] and max [exclusive] (ReadOnly).</summary>
        public static int IntRange(int min, int max)
            => UERandom.Range(min, max);

        /// <summary>Return a random integer number between min [-2147483648] and max [exclusive] (ReadOnly).</summary>
        public static int IntRange(int max)
            => UERandom.Range(int.MinValue, max);

        /// <summary>Return a random integer number between min [-2147483648] and max [2147483647] (ReadOnly).</summary>
        public static int IntRange()
            => UERandom.Range(int.MinValue, int.MaxValue);

        public static long LongRange(long min, long max) {
            long compri = Math.Abs(min - max);
            long smin = min < max ? min : max;
            return smin + (long)(value * compri);
        }

        public static long LongRange(long max)
            => LongRange(long.MinValue, max);

        public static long LongRange()
            => LongRange(long.MinValue, long.MaxValue);

        /// <summary>Return a random float number between min [inclusive] and max [exclusive] (ReadOnly).</summary>
        public static float FloatRange(float min, float max)
            => UERandom.Range(min, max);

        /// <summary>Return a random float number between min [3.40282347E+38F] and max [exclusive] (ReadOnly).</summary>
        public static float FloatRange(float max)
            => UERandom.Range(float.MinValue, max);

        /// <summary>Return a random float number between min [3.40282347E+38F] and max [3.40282347E+38F] (ReadOnly).</summary>
        public static float FloatRange()
            => UERandom.Range(float.MinValue, float.MaxValue);

        public static double DoubleRange(double min, double max) {
            double compri = Math.Abs(min - max);
            double smin = min < max ? min : max;
            return smin + (value * compri);
        }

        public static double DoubleRange(double max)
            => DoubleRange(double.MinValue, max);

        public static double DoubleRange()
            => DoubleRange(double.MinValue, double.MaxValue);

        public static decimal DecimalRange(decimal min, decimal max) {
            decimal compri = Math.Abs(min - max);
            decimal smin = min < max ? min : max;
            return smin + ((decimal)value * compri);
        }

        public static decimal DecimalRange(decimal max)
            => DecimalRange(decimal.MinValue, max);

        public static decimal DecimalRange()
            => DecimalRange(decimal.MinValue, decimal.MaxValue);
    }
}
