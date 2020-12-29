using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System;

namespace LeptonicaSharp
{
	//  ./pix.h (502, 8)
	/// <summary>
	/// Array of Boxa
	/// </summary>
	/// <example>
	/// <code source="CSource\Struct_Boxaa.txt" language="C" title="./pix.h (502, 8)"/>
	/// </example>
	public partial class Boxaa : IDisposable
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] public IntPtr Pointer;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Marshal_Boxaa Values = new Marshal_Boxaa();
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Dictionary<String, Object> Caching = new Dictionary<String, Object>();

		public void Dispose()
		{
			Natives.boxaaDestroy(ref Pointer);
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

		public Boxaa(IntPtr PTR)
		{
			if (PTR != IntPtr.Zero) { Pointer = PTR; }

			Pointer = PTR; Marshal.PtrToStructure(Pointer, Values);
		}
		///  <summary>
		///  number of boxa in ptr array
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (504, 24)
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
		///  number of boxa ptrs allocated
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (505, 24)
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
		///  boxa ptr array
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (506, 24)
		///  Org: [struct Boxa ** boxa]
		///  Msh: struct Boxa ** | 3:StructDeclaration |  ... Marshal List of Class to PTR | Typedef: Boxa = Boxa
		///  </remarks>
		public List<Boxa> boxa
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
					if (Values.boxa != IntPtr.Zero)
					{
						List<Boxa> LSTRET = new List<Boxa>();
						IntPtr[] LSTPTR = new IntPtr[Values.n];
						Marshal.Copy(Values.boxa, LSTPTR, 0, Values.n);

						foreach (IntPtr Entry in LSTPTR)
						{
							LSTRET.Add(new Boxa(Entry));
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
		private class Marshal_Boxaa
		{
			public int n;
			public int nalloc;
			public IntPtr boxa;
		}
	}
}
