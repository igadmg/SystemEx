using System;
using System.Reflection;

namespace SystemEx
{
	public struct TypeAttributePair<A> where A: Attribute
	{
		public Type Type;
		public A Attribute;
	}

	public struct FieldAttributePair<A> where A : Attribute
	{
		public FieldInfo Field;
		public A Attribute;
	}

	public struct MethodAttributePair<A> where A : Attribute
	{
		public MethodInfo Method;
		public A Attribute;
	}
}
