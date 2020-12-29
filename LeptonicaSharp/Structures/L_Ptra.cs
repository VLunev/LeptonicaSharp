using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System;

namespace LeptonicaSharp
{
	//  ./ptra.h (51, 8)
	/// <summary>
	/// Generic pointer array
	/// </summary>
	/// <example>
	/// <code source="CSource\Struct_L_Ptra.txt" language="C" title="./ptra.h (51, 8)"/>
	/// </example>
	public partial class L_Ptra : IDisposable
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] public IntPtr Pointer;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Marshal_L_Ptra Values = new Marshal_L_Ptra();
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

		public L_Ptra(IntPtr PTR)
		{
			if (PTR != IntPtr.Zero) { Pointer = PTR; }

			Pointer = PTR; Marshal.PtrToStructure(Pointer, Values);
		}

		public L_Ptra(int Count)
		{
			this.Pointer = LeptonicaSharp._All.ptraCreate(Count).Pointer;
		}
		///  <summary>
		///  size of allocated ptr array
		///  </summary>
		///  <remarks>
		///  Loc: ./ptra.h (53, 22)
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
		///  greatest valid index
		///  </summary>
		///  <remarks>
		///  Loc: ./ptra.h (54, 22)
		///  Org: [l_int32 imax]
		///  Msh: l_int32 | 1:Int |
		///  </remarks>
		public int imax
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
					return Values.imax;
				}
			}
		}

		///  <summary>
		///  actual number of stored elements
		///  </summary>
		///  <remarks>
		///  Loc: ./ptra.h (55, 22)
		///  Org: [l_int32 nactual]
		///  Msh: l_int32 | 1:Int |
		///  </remarks>
		public int nactual
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
					return Values.nactual;
				}
			}
		}

		///  <summary>
		///  ptr array
		///  </summary>
		///  <remarks>
		///  Loc: ./ptra.h (56, 22)
		///  Org: [void ** array]
		///  Msh: void ** | 3:Void | IntPtr[]
		///  </remarks>
		public IntPtr[] array
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
					// Structs.cs : Struct 3 : Void
					return null; ;
				}
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		private class Marshal_L_Ptra
		{
			public int nalloc;
			public int imax;
			public int nactual;
			public IntPtr array;
		}
	}
}
