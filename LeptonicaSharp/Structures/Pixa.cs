using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System;

namespace LeptonicaSharp
{
	//  ./pix.h (454, 8)
	/// <summary>
	/// Array of pix
	/// </summary>
	/// <example>
	/// <code source="CSource\Struct_Pixa.txt" language="C" title="./pix.h (454, 8)"/>
	/// </example>
	public partial class Pixa : IDisposable
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] public IntPtr Pointer;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Marshal_Pixa Values = new Marshal_Pixa();
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Dictionary<String, Object> Caching = new Dictionary<String, Object>();

		public Pixa(IntPtr PTR)
		{
			if (PTR == IntPtr.Zero)
				throw new ArgumentNullException("Pixa PTR is zero");

			Pointer = PTR;
			Marshal.PtrToStructure(Pointer, Values);
		}

		public Pixa(int Count)
		{
			this.Pointer = LeptonicaSharp._All.pixaCreate(Count).Pointer;
		}

		public void Dispose()
		{
			Natives.pixaDestroy(ref Pointer);
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
		///  <summary>
		///  number of Pix in ptr array
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (456, 25)
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
		///  number of Pix ptrs allocated
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (457, 25)
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
		///  Loc: ./pix.h (458, 25)
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
		///  the array of ptrs to pix
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (459, 25)
		///  Org: [struct Pix ** pix]
		///  Msh: struct Pix ** | 3:StructDeclaration |  ... Marshal List of Class to PTR | Typedef: Pix = Pix
		///  </remarks>
		public List<Pix> pix
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
					if (Values.pix != IntPtr.Zero)
					{
						List<Pix> LSTRET = new List<Pix>();
						IntPtr[] LSTPTR = new IntPtr[Values.n];
						Marshal.Copy(Values.pix, LSTPTR, 0, Values.n);

						foreach (IntPtr Entry in LSTPTR)
						{
							LSTRET.Add(new Pix(Entry));
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

		///  <summary>
		///  array of boxes
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (460, 25)
		///  Org: [struct Boxa * boxa]
		///  Msh: struct Boxa * | 2:Struct |
		///  </remarks>
		public Boxa boxa
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

					// Depth:2 'StructureDeclaration'
					if (Values.boxa != IntPtr.Zero)
					{
						return new Boxa(Values.boxa);
					}
					else
					{
						return null;
					};
				}
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		private class Marshal_Pixa
		{
			public int n;
			public int nalloc;
			public uint refcount;
			public IntPtr pix;
			public IntPtr boxa;
		}
	}

}
