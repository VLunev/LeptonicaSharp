using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System;

namespace LeptonicaSharp
{
	//  ./pix.h (155, 8)
	/// <summary>
	/// Colormap of a Pix
	/// </summary>
	/// <example>
	/// <code source="CSource\Struct_PixColormap.txt" language="C" title="./pix.h (155, 8)"/>
	/// </example>
	public partial class PixColormap : IDisposable
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] public IntPtr Pointer;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Marshal_PixColormap Values = new Marshal_PixColormap();
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Dictionary<String, Object> Caching = new Dictionary<String, Object>();

		public void Dispose()
		{
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

		public PixColormap(IntPtr PTR)
		{
			if (PTR != IntPtr.Zero) { Pointer = PTR; }

			Pointer = PTR; Marshal.PtrToStructure(Pointer, Values);
		}
		///  <summary>
		///  colormap table (array of RGBA_QUAD)
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (157, 22)
		///  Org: [void * array]
		///  Msh: void * | 2:Void | Object -  - IntPtr
		///  </remarks>
		public IntPtr array
		{
			get
			{
				if (Pointer == IntPtr.Zero)
				{
					return IntPtr.Zero;
				}
				else
				{
					Marshal.PtrToStructure(Pointer, Values);

					if (Values.array != IntPtr.Zero)
					{
						return Values.array;
					}
					else { return IntPtr.Zero; };
				}
			}
		}

		///  <summary>
		///  of pix (1, 2, 4 or 8 bpp)
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (158, 22)
		///  Org: [l_int32 depth]
		///  Msh: l_int32 | 1:Int |
		///  </remarks>
		public int depth
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
					return Values.depth;
				}
			}
		}

		///  <summary>
		///  number of color entries allocated
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (159, 22)
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
		///  number of color entries used
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (160, 22)
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

		[StructLayout(LayoutKind.Sequential)]
		private class Marshal_PixColormap
		{
			public IntPtr array;
			public int depth;
			public int nalloc;
			public int n;
		}
	}
}
