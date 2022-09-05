using System;
using Cobilas.Unity.Utility;

namespace UnityEngine {
    using UERandom = UnityEngine.Random;
    [Obsolete("Use Cobilas.Unity.Utility.Randomico class")]
    public static class CobilasRandom {

        /// <summary>Returns a random rotation (Read Only).</summary>
        public static Quaternion rotation => Randomico.rotation;
        /// <summary>Returns a random point on the surface of a sphere with radius 1 (Read Only).</summary>
        public static Vector3 onUnitSphere => Randomico.onUnitSphere;
        /// <summary>Returns a random point inside a circle with radius 1 (Read Only).</summary>
        public static Vector2 insideUnitCircle => Randomico.insideUnitCircle;
        /// <summary>Returns a random point inside a sphere with radius 1 (Read Only).</summary>
        public static Vector3 insideUnitSphere => Randomico.insideUnitSphere;
        /// <summary>Returns a random rotation with uniform distribution (Read Only).</summary>
        public static Quaternion rotationUniform => Randomico.rotationUniform;
        /// <summary>Gets/Sets the full internal state of the random number generator.</summary>
        public static UERandom.State state { get => Randomico.state; set => Randomico.state = value; }
        /// <summary>Returns a random number between 0.0 [inclusive] and 1.0 [inclusive] (Read Only).</summary>
        public static float value => Randomico.value;
        /// <summary>Less than <c>0.5f</c> is false, greater than <c>0.5f</c> is true.(<c><seealso cref="CobilasRandom"/>.value > 0.5f</c>)</summary>
        public static bool BooleanRandom => Randomico.BooleanRandom;

        /// <summary>Initializes the random number generator state with a seed.</summary>
        /// <param name="seed">Seed used to initialize the random number generator.</param>
        public static void InitState(int seed)
            => Randomico.InitState(seed);

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
            => Randomico.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax, alphaMin, alphaMax);

        /// <summary>Generates a random color from HSV and alpha ranges.</summary>
        /// <param name="hueMin">Minimum hue [0..1].</param>
        /// <param name="hueMax">Maximum hue [0..1].</param>
        /// <param name="saturationMin">Minimum saturation [0..1].</param>
        /// <param name="saturationMax">Maximum saturation[0..1].</param>
        /// <param name="valueMin">Minimum value [0..1].</param>
        /// <param name="valueMax">Maximum value [0..1].</param>
        /// <returns>A random color with HSV and alpha values in the input ranges.</returns>
        public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax)
            => Randomico.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax);

        /// <summary>Generates a random color from HSV and alpha ranges.</summary>
        /// <param name="hueMin">Minimum hue [0..1].</param>
        /// <param name="hueMax">Maximum hue [0..1].</param>
        /// <param name="saturationMin">Minimum saturation [0..1].</param>
        /// <param name="saturationMax">Maximum saturation[0..1].</param>
        /// <returns>A random color with HSV and alpha values in the input ranges.</returns>
        public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax)
            => Randomico.ColorHSV(hueMin, hueMax, saturationMin, saturationMax);

        /// <summary>Generates a random color from HSV and alpha ranges.</summary>
        /// <param name="hueMin">Minimum hue [0..1].</param>
        /// <param name="hueMax">Maximum hue [0..1].</param>
        /// <returns>A random color with HSV and alpha values in the input ranges.</returns>
        public static Color ColorHSV(float hueMin, float hueMax)
            => Randomico.ColorHSV(hueMin, hueMax);

        /// <summary>Generates a random color from HSV and alpha ranges.</summary>
        /// <returns>A random color with HSV and alpha values in the input ranges.</returns>
        public static Color ColorHSV()
            => Randomico.ColorHSV();

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
            => Randomico.UlongRange(min, max);

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
            => Randomico.IntRange(min, max);

        /// <summary>Return a random integer number between min [-2147483648] and max [exclusive] (ReadOnly).</summary>
        public static int IntRange(int max)
            => Randomico.IntRange(int.MinValue, max);

        /// <summary>Return a random integer number between min [-2147483648] and max [2147483647] (ReadOnly).</summary>
        public static int IntRange()
            => Randomico.IntRange(int.MinValue, int.MaxValue);

        public static long LongRange(long min, long max)
            => Randomico.LongRange(min, max);

        public static long LongRange(long max)
            => LongRange(long.MinValue, max);

        public static long LongRange()
            => LongRange(long.MinValue, long.MaxValue);

        /// <summary>Return a random float number between min [inclusive] and max [exclusive] (ReadOnly).</summary>
        public static float FloatRange(float min, float max)
            => Randomico.FloatRange(min, max);

        /// <summary>Return a random float number between min [3.40282347E+38F] and max [exclusive] (ReadOnly).</summary>
        public static float FloatRange(float max)
            => Randomico.FloatRange(float.MinValue, max);

        /// <summary>Return a random float number between min [3.40282347E+38F] and max [3.40282347E+38F] (ReadOnly).</summary>
        public static float FloatRange()
            => Randomico.FloatRange(float.MinValue, float.MaxValue);

        public static double DoubleRange(double min, double max)
            => Randomico.DoubleRange(min, max);

        public static double DoubleRange(double max)
            => DoubleRange(double.MinValue, max);

        public static double DoubleRange()
            => DoubleRange(double.MinValue, double.MaxValue);

        public static decimal DecimalRange(decimal min, decimal max)
            => Randomico.DecimalRange(min, max);

        public static decimal DecimalRange(decimal max)
            => DecimalRange(decimal.MinValue, max);

        public static decimal DecimalRange()
            => DecimalRange(decimal.MinValue, decimal.MaxValue);
    }
}
