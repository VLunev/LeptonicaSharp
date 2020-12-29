using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System;

namespace LeptonicaSharp
{
	//  ./pix.h (480, 8)
	/// <summary>
	/// Basic rectangle
	/// </summary>
	/// <example>
	/// <code source="CSource\Struct_Box.txt" language="C" title="./pix.h (480, 8)"/>
	/// </example>
	public partial class Box : IDisposable
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] public IntPtr Pointer;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Marshal_Box Values = new Marshal_Box();
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Dictionary<String, Object> Caching = new Dictionary<String, Object>();

		public void Dispose()
		{
			Natives.boxDestroy(ref Pointer);
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

		public Box(IntPtr PTR)
		{
			if (PTR != IntPtr.Zero) { Pointer = PTR; }

			Pointer = PTR; Marshal.PtrToStructure(Pointer, Values);
		}

		public Box(int X, int Y, int W, int H)
		{
			this.Pointer = LeptonicaSharp._All.boxCreate(X, Y, W, H).Pointer;
		}

		///  <summary>
		///  left coordinate
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (482, 24)
		///  Org: [l_int32 x]
		///  Msh: l_int32 | 1:Int |
		///  </remarks>
		public int x
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
					return Values.x;
				}
			}
		}

		///  <summary>
		///  top coordinate
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (483, 24)
		///  Org: [l_int32 y]
		///  Msh: l_int32 | 1:Int |
		///  </remarks>
		public int y
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
					return Values.y;
				}
			}
		}

		///  <summary>
		///  box width
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (484, 24)
		///  Org: [l_int32 w]
		///  Msh: l_int32 | 1:Int |
		///  </remarks>
		public int w
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
					return Values.w;
				}
			}
		}

		///  <summary>
		///  box height
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (485, 24)
		///  Org: [l_int32 h]
		///  Msh: l_int32 | 1:Int |
		///  </remarks>
		public int h
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
					return Values.h;
				}
			}
		}

		///  <summary>
		///  reference count (1 if no clones)
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (486, 24)
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

		[StructLayout(LayoutKind.Sequential)]
		private class Marshal_Box
		{
			public int x;
			public int y;
			public int w;
			public int h;
			public uint refcount;
		}
	}
}
