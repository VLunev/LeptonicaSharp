using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System;

namespace LeptonicaSharp
{
	//  ./pix.h (492, 8)
	/// <summary>
	/// Array of Box
	/// </summary>
	/// <example>
	/// <code source="CSource\Struct_Boxa.txt" language="C" title="./pix.h (492, 8)"/>
	/// </example>
	public partial class Boxa : IDisposable
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] public IntPtr Pointer;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Marshal_Boxa Values = new Marshal_Boxa();
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Dictionary<String, Object> Caching = new Dictionary<String, Object>();

		public void Dispose()
		{
			Natives.boxaDestroy(ref Pointer);
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

		public Boxa(IntPtr PTR)
		{
			if (PTR != IntPtr.Zero) { Pointer = PTR; }

			Pointer = PTR; Marshal.PtrToStructure(Pointer, Values);
		}
		///  <summary>
		///  number of box in ptr array
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (494, 24)
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
		///  number of box ptrs allocated
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (495, 24)
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
		///  Loc: ./pix.h (496, 24)
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
		///  box ptr array
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (497, 24)
		///  Org: [struct Box ** box]
		///  Msh: struct Box ** | 3:StructDeclaration |  ... Marshal List of Class to PTR | Typedef: Box = Box
		///  </remarks>
		public List<Box> box
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

					// Struct Level 3 (Struct)
					if (Values.box != IntPtr.Zero)
					{
						List<Box> LSTRET = new List<Box>();
						IntPtr[] LSTPTR = new IntPtr[Values.n];
						Marshal.Copy(Values.box, LSTPTR, 0, Values.n);

						foreach (IntPtr Entry in LSTPTR)
						{
							LSTRET.Add(new Box(Entry));
						}

						return LSTRET;
					}
					else
					{
						return null;
					};
				}
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		private class Marshal_Boxa
		{
			public int n;
			public int nalloc;
			public uint refcount;
			public IntPtr box;
		}
	}

}
