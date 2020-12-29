using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System;

namespace LeptonicaSharp
{
	//  ./array.h (59, 8)
	/// <summary>
	/// Number array: an array of floats
	/// </summary>
	/// <example>
	/// <code source="CSource\Struct_Numa.txt" language="C" title="./array.h (59, 8)"/>
	/// </example>
	public partial class Numa : IDisposable
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] public IntPtr Pointer;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Marshal_Numa Values = new Marshal_Numa();
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Dictionary<String, Object> Caching = new Dictionary<String, Object>();

		public void Dispose()
		{
			Natives.numaDestroy(ref Pointer);
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

		public Numa(IntPtr PTR)
		{
			if (PTR != IntPtr.Zero) { Pointer = PTR; }

			Pointer = PTR; Marshal.PtrToStructure(Pointer, Values);
		}

		public Numa(String Text)
		{
			this.Pointer = LeptonicaSharp._All.numaCreateFromString(Text).Pointer;
		}

		///  <summary>
		///  size of allocated number array
		///  </summary>
		///  <remarks>
		///  Loc: ./array.h (61, 22)
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
		///  number of numbers saved
		///  </summary>
		///  <remarks>
		///  Loc: ./array.h (62, 22)
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
		///  reference count (1 if no clones)
		///  </summary>
		///  <remarks>
		///  Loc: ./array.h (63, 22)
		///  Org: [l_int32 refcount]
		///  Msh: l_int32 | 1:Int |
		///  </remarks>
		public int refcount
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
		///  x value assigned to array[0]
		///  </summary>
		///  <remarks>
		///  Loc: ./array.h (64, 22)
		///  Org: [l_float32 startx]
		///  Msh: l_float32 | 1:Float |
		///  </remarks>
		public Single startx
		{
			get
			{
				if (Pointer == IntPtr.Zero)
				{
					return 0f;
				}
				else
				{
					Marshal.PtrToStructure(Pointer, Values);
					return Values.startx;
				}
			}
		}

		///  <summary>
		///  change in x value as i --> i + 1
		///  </summary>
		///  <remarks>
		///  Loc: ./array.h (65, 22)
		///  Org: [l_float32 delx]
		///  Msh: l_float32 | 1:Float |
		///  </remarks>
		public Single delx
		{
			get
			{
				if (Pointer == IntPtr.Zero)
				{
					return 0f;
				}
				else
				{
					Marshal.PtrToStructure(Pointer, Values);
					return Values.delx;
				}
			}
		}

		///  <summary>
		///  number array
		///  </summary>
		///  <remarks>
		///  Loc: ./array.h (66, 22)
		///  Org: [l_float32 * array]
		///  Msh: l_float32 * | 2:Float |  ... = Single
		///  </remarks>
		public Single[] array
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

					if (Values.array != IntPtr.Zero)
					{
						Single[] _array = new Single[Values.n];
						Marshal.Copy(Values.array, _array, 0, _array.Length);
						return _array;
					}
					else { return null; };
				}
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		private class Marshal_Numa
		{
			public int nalloc;
			public int n;
			public int refcount;
			public Single startx;
			public Single delx;
			public IntPtr array;
		}
	}
}
