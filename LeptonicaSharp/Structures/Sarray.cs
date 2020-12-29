using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System;

namespace LeptonicaSharp
{
	//  ./array.h (116, 8)
	/// <summary>
	/// String array: an array of C strings
	/// </summary>
	/// <example>
	/// <code source="CSource\Struct_Sarray.txt" language="C" title="./array.h (116, 8)"/>
	/// </example>
	public partial class Sarray : IDisposable
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] public IntPtr Pointer;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Marshal_Sarray Values = new Marshal_Sarray();
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Dictionary<String, Object> Caching = new Dictionary<String, Object>();

		public void Dispose()
		{
			Natives.sarrayDestroy(ref Pointer);
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

		public Sarray(IntPtr PTR)
		{
			if (PTR != IntPtr.Zero) { Pointer = PTR; }

			Pointer = PTR; Marshal.PtrToStructure(Pointer, Values);
		}

		public Sarray(int Count)
		{
			this.Pointer = LeptonicaSharp._All.sarrayCreate(Count).Pointer;
		}
		///  <summary>
		///  size of allocated ptr array
		///  </summary>
		///  <remarks>
		///  Loc: ./array.h (118, 22)
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
		///  number of strings allocated
		///  </summary>
		///  <remarks>
		///  Loc: ./array.h (119, 22)
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
		///  Loc: ./array.h (120, 22)
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
		///  string array
		///  </summary>
		///  <remarks>
		///  Loc: ./array.h (121, 22)
		///  Org: [char ** array]
		///  Msh: char ** | 3:CharS | String[]
		///  </remarks>
		public String[] array
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
					// Structs.cs : Struct 3 : CharS
					List<String> LSTRET = new List<String>();
					IntPtr[] LSTPTR = new IntPtr[Values.n];
					Marshal.Copy(Values.array, LSTPTR, 0, Values.n);

					foreach (IntPtr Entry in LSTPTR)
					{
						LSTRET.Add(Marshal.PtrToStringAnsi(Entry));
					}

					return LSTRET.ToArray(); ;
				}
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		private class Marshal_Sarray
		{
			public int nalloc;
			public int n;
			public int refcount;
			public IntPtr array;
		}
	}
}
