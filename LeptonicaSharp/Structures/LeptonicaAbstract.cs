using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System;

namespace LeptonicaSharp
{
    public abstract class LeptonicaAbstract : IDisposable
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] public IntPtr Pointer { get; protected set; }
        [DebuggerBrowsable(DebuggerBrowsableState.Never)] protected Dictionary<String, Object> Caching = new Dictionary<String, Object>();

		public virtual void Dispose()
		{
			Pointer = IntPtr.Zero;

			foreach (KeyValuePair<string, object> Entry in Caching)
			{
				var disp = Entry.Value as IDisposable;

				if (disp != null)
					disp.Dispose();
			}

			Caching.Clear();
			Caching = null;
			System.GC.SuppressFinalize(this);
		}

		public LeptonicaAbstract(IntPtr PTR)
        {
            if (PTR == IntPtr.Zero)
                throw new ArgumentNullException("PTR is zero");

            Pointer = PTR;
        }

		protected bool GetStruct(object obj)
        {
			if (Pointer == IntPtr.Zero)
				return false;
			Marshal.PtrToStructure(Pointer, obj);
			return true;
		}
    }
}
