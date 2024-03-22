using System;
using System.Threading;

namespace SystemEx {
	public class GCNotify {
		static GCNotify instance_ = null;
		public static GCNotify instance {
			get {
				instance_ ??= new GCNotify();
				return instance_;
			}
		}

		public delegate void CollectedEventHandler();
		public event CollectedEventHandler OnCollected;


		public GCNotify() {
			var thread = new Thread(CheckGC);
			thread.Priority = ThreadPriority.Lowest;
			thread.Start();
		}

		void CheckGC() {
			var wait = new SpinWait();
			var check = new WeakReference(new object());
			while (true) {
				if (!check.IsAlive) {
					OnCollected?.Invoke();
					check = new WeakReference(new object());
				}

				wait.SpinOnce();
			}
		}
	}

}