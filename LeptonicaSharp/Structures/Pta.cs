using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System;

namespace LeptonicaSharp
{
	//  ./pix.h (517, 8)
	/// <summary>
	/// Array of points
	/// </summary>
	/// <example>
	/// <code source="CSource\Struct_Pta.txt" language="C" title="./pix.h (517, 8)"/>
	/// </example>
	public partial class Pta : IDisposable
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] public IntPtr Pointer;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Marshal_Pta Values = new Marshal_Pta();
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Dictionary<String, Object> Caching = new Dictionary<String, Object>();

		public void Dispose()
		{
			Natives.ptaDestroy(ref Pointer);
			Pointer = IntPtr.Zero;

			foreach (KeyValuePair<string, object> Entry in Caching)
			{
				var disp = Entry.Value as IDisposable;

				if (disp != null) { disp.Dispose(); }
			}

			Caching.Clear();
			Caching = null;
			System.GC.SuppressFinalize(this);
		}

		public Pta(IntPtr PTR)
		{
			if (PTR != IntPtr.Zero) { Pointer = PTR; }

			Pointer = PTR; Marshal.PtrToStructure(Pointer, Values);
		}

		public Pta(int Count)
		{
			this.Pointer = LeptonicaSharp._All.ptaCreate(Count).Pointer;
		}
		///  <summary>
		///  actual number of pts
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (519, 24)
		///  Org: [l_int32 n]
		///  Msh: l_int32 | 1:Int |
		///  </remarks>
		public int n
		{
			get
			{
				if (Pointer == IntPtr.Zero)
				{
					return 0;
				}
				else
				{
					Marshal.PtrToStructure(Pointer, Values);
					return Values.n;
				}
			}
		}

		///  <summary>
		///  size of allocated arrays
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (520, 24)
		///  Org: [l_int32 nalloc]
		///  Msh: l_int32 | 1:Int |
		///  </remarks>
		public int nalloc
		{
			get
			{
				if (Pointer == IntPtr.Zero)
				{
					return 0;
				}
				else
				{
					Marshal.PtrToStructure(Pointer, Values);
					return Values.nalloc;
				}
			}
		}

		///  <summary>
		///  reference count (1 if no clones)
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (521, 24)
		///  Org: [l_uint32 refcount]
		///  Msh: l_uint32 | 1:UInt |
		///  </remarks>
		public uint refcount
		{
			get
			{
				if (Pointer == IntPtr.Zero)
				{
					return 0;
				}
				else
				{
					Marshal.PtrToStructure(Pointer, Values);
					return Values.refcount;
				}
			}
		}

		///  <summary>
		///  arrays of floats
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (522, 24)
		///  Org: [l_float32 * x]
		///  Msh: l_float32 * | 2:Float |  ... = Single
		///  </remarks>
		public Single[] x
		{
			get
			{
				if (Pointer == IntPtr.Zero)
				{
					return null;
				}
				else
				{
					Marshal.PtrToStructure(Pointer, Values);

					if (Values.x != IntPtr.Zero)
					{
						Single[] _x = new Single[Values.n];
						Marshal.Copy(Values.x, _x, 0, _x.Length);
						return _x;
					}
					else { return null; };
				}
			}
		}

		///  <summary>
		///  arrays of floats
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (522, 28)
		///  Org: [l_float32 * y]
		///  Msh: l_float32 * | 2:Float |  ... = Single
		///  </remarks>
		public Single[] y
		{
			get
			{
				if (Pointer == IntPtr.Zero)
				{
					return null;
				}
				else
				{
					Marshal.PtrToStructure(Pointer, Values);

					if (Values.y != IntPtr.Zero)
					{
						Single[] _y = new Single[Values.n];
						Marshal.Copy(Values.y, _y, 0, _y.Length);
						return _y;
					}
					else { return null; };
				}
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		private class Marshal_Pta
		{
			public int n;
			public int nalloc;
			public uint refcount;
			public IntPtr x;
			public IntPtr y;
		}
	}
}
