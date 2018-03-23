using System;

namespace SystemEx
{
	public class ActionContainer
	{
		public Delegate action { get; protected set; }

		public Type[] args;

		public ActionContainer(Delegate action, params Type[] args)
		{
			this.action = action;
			this.args = args;
		}

		public void DynamicInvoke(params object[] args)
		{
			action.DynamicInvoke(args);
		}
	}
}
