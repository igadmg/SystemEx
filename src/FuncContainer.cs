using System;

namespace SystemEx
{
	public class FuncContainer
	{
		public Delegate action { get; protected set; }
		public Type ret;
		public Type[] args;

		public FuncContainer(Delegate action, Type ret, params Type[] args)
		{
			this.action = action;
			this.ret = ret;
			this.args = args;
		}

		public object DynamicInvoke(params object[] args)
		{
			return action.DynamicInvoke(args);
		}
	}
}
