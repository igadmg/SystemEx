using System;
using System.Collections.Generic;

namespace SystemEx
{
    public class MathOperations<T>
    {
        public T empty;
        public T max;
        public T min;

        /// <summary>
        /// Оператор равенства.
        /// </summary>
        public Func<T, T, bool> eq;

        /// <summary>
        /// Оператор больше.
        /// </summary>
        public Func<T, T, bool> gt;

        /// <summary>
        /// Оператор меньше.
        /// </summary>
        public Func<T, T, bool> lt;

        /// <summary>
        /// Оператор линейной интерполяции.
        /// </summary>
        public Func<T, T, float, T> lerp;
    }

    public class MathOperations
    {
        private static float epsilon = 0.0001f;
        private static double epsilonDouble = 0.0000001;
        private static Dictionary<Type, object> operations = new Dictionary<Type, object>();

        public static MathOperations<T> Get<T>()
        {
            return (MathOperations<T>)operations[typeof(T)];
        }

        static MathOperations()
        {
            MathOperations<float> floatOps = new MathOperations<float>();
            floatOps.empty = float.NaN;
            floatOps.max = float.MaxValue;
            floatOps.min = float.MinValue;
            floatOps.eq = eq;
            floatOps.gt = gt;
            floatOps.lt = lt;
            floatOps.lerp = lerp;
            operations.Add(typeof(float), floatOps);

            MathOperations<double> doubleOps = new MathOperations<double>();
            doubleOps.empty = double.NaN;
            doubleOps.max = double.MaxValue;
            doubleOps.min = double.MinValue;
            doubleOps.eq = eqDouble;
            doubleOps.gt = gtDouble;
            doubleOps.lt = ltDouble;
            doubleOps.lerp = lerpDouble;
            operations.Add(typeof(double), floatOps);

            MathOperations<byte> byteOps = new MathOperations<byte>();
            byteOps.empty = byte.MaxValue;
            byteOps.max = byte.MaxValue - 1;
            byteOps.min = byte.MinValue;
            byteOps.eq = eqByte;
            byteOps.gt = gtByte;
            byteOps.lt = ltByte;
            byteOps.lerp = lerpByte;
            operations.Add(typeof(byte), byteOps);

            MathOperations<short> shortOps = new MathOperations<short>();
            shortOps.empty = short.MinValue;
            shortOps.max = short.MaxValue;
            shortOps.min = short.MinValue + 1;
            shortOps.eq = eqShort;
            shortOps.gt = gtShort;
            shortOps.lt = ltShort;
            shortOps.lerp = lerpShort;
            operations.Add(typeof(short), shortOps);

            MathOperations<int> intOps = new MathOperations<int>();
            intOps.empty = int.MinValue;
            intOps.max = int.MaxValue;
            intOps.min = int.MinValue + 1;
            intOps.eq = eqInt;
            intOps.gt = gtInt;
            intOps.lt = ltInt;
            intOps.lerp = lerpInt;
            operations.Add(typeof(int), intOps);
        }

        static public float lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        static public bool eq(float a, float b)
        {
            return Math.Abs(a - b) < epsilon;
        }

        static public bool gt(float a, float b)
        {
            return a > b && !eq(a, b);
        }

        static public bool lt(float a, float b)
        {
            return a < b && !eq(a, b);
        }

        static public double lerpDouble(double a, double b, float t)
        {
            return a + (b - a) * t;
        }

        static public bool eqDouble(double a, double b)
        {
            return Math.Abs(a - b) < epsilonDouble;
        }

        static public bool gtDouble(double a, double b)
        {
            return a > b && !eqDouble(a, b);
        }

        static public bool ltDouble(double a, double b)
        {
            return a < b && !eqDouble(a, b);
        }

        static public byte lerpByte(byte a, byte b, float t)
        {
            return (byte)(a + (b - a) * t + 0.5f);
        }

        static public bool eqByte(byte a, byte b)
        {
            return a == b;
        }

        static public bool gtByte(byte a, byte b)
        {
            return a > b;
        }

        static public bool ltByte(byte a, byte b)
        {
            return a < b;
        }

        static public short lerpShort(short a, short b, float t)
        {
            return (short)(a + (b - a) * t + 0.5f);
        }

        static public bool eqShort(short a, short b)
        {
            return a == b;
        }

        static public bool gtShort(short a, short b)
        {
            return a > b;
        }

        static public bool ltShort(short a, short b)
        {
            return a < b;
        }

        static public int lerpInt(int a, int b, float t)
        {
            return (int)(a + (b - a) * t + 0.5f);
        }

        static public bool eqInt(int a, int b)
        {
            return a == b;
        }

        static public bool gtInt(int a, int b)
        {
            return a > b;
        }

        static public bool ltInt(int a, int b)
        {
            return a < b;
        }
    }
}