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

	public static class MathOperations
	{
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
			floatOps.eq = MathOperationsFloat.eq;
			floatOps.gt = MathOperationsFloat.gt;
			floatOps.lt = MathOperationsFloat.lt;
			floatOps.lerp = MathOperationsFloat.lerp;
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
			intOps.eq = MathOperationsInt.eq;
			intOps.gt = MathOperationsInt.gt;
			intOps.lt = MathOperationsInt.lt;
			intOps.lerp = MathOperationsInt.lerp;
			operations.Add(typeof(int), intOps);
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
	}

	public static class MathOperationsFloat
	{
		private static float epsilon = 0.0001f;

		static public float lerp(float a, float b, float t)
		{
			return a + (b - a) * t;
		}

		static public bool eq(this float a, float b)
		{
			return Math.Abs(a - b) < epsilon;
		}

		static public bool meq(this float a, float b)
		{
			return eq(Math.Abs(a), Math.Abs(b));
		}

		static public bool gt(this float a, float b)
		{
			return a > b && !eq(a, b);
		}

		static public bool lt(this float a, float b)
		{
			return a < b && !eq(a, b);
		}

		static public float max(this float a, float b)
		{
			return a.gt(b) ? a : b;
		}

		static public float min(this float a, float b)
		{
			return a.lt(b) ? a : b;
		}
	}

	public static class MathOperationsInt
	{
		static public int lerp(int a, int b, float t)
		{
			return (int)(a + (b - a) * t + 0.5f);
		}

		static public bool eq(this int a, int b)
		{
			return a == b;
		}

		static public bool meq(this int a, int b)
		{
			return a == b || a == -b;
		}

		static public bool gt(this int a, int b)
		{
			return a > b;
		}

		static public bool lt(this int a, int b)
		{
			return a < b;
		}

		static public int max(this int a, int b)
		{
			return a.gt(b) ? a : b;
		}

		static public int min(this int a, int b)
		{
			return a.lt(b) ? a : b;
		}
	}
}
