using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System;

namespace LeptonicaSharp
{
	//  ./pix.h (532, 8)
	/// <summary>
	/// Array of Pta
	/// </summary>
	/// <example>
	/// <code source="CSource\Struct_Ptaa.txt" language="C" title="./pix.h (532, 8)"/>
	/// </example>
	public partial class Ptaa : IDisposable
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] public IntPtr Pointer;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Marshal_Ptaa Values = new Marshal_Ptaa();
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Dictionary<String, Object> Caching = new Dictionary<String, Object>();

		public void Dispose()
		{
			Natives.ptaaDestroy(ref Pointer);
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

		public Ptaa(IntPtr PTR)
		{
			if (PTR != IntPtr.Zero) { Pointer = PTR; }

			Pointer = PTR; Marshal.PtrToStructure(Pointer, Values);
		}
		///  <summary>
		///  number of pta in ptr array
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (534, 26)
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
		///  number of pta ptrs allocated
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (535, 26)
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
		///  pta ptr array
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (536, 26)
		///  Org: [struct Pta ** pta]
		///  Msh: struct Pta ** | 3:StructDeclaration |  ... Marshal List of Class to PTR | Typedef: Pta = Pta
		///  </remarks>
		public List<Pta> pta
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
					if (Values.pta != IntPtr.Zero)
					{
						List<Pta> LSTRET = new List<Pta>();
						IntPtr[] LSTPTR = new IntPtr[Values.n];
						Marshal.Copy(Values.pta, LSTPTR, 0, Values.n);

						foreach (IntPtr Entry in LSTPTR)
						{
							LSTRET.Add(new Pta(Entry));
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
		private class Marshal_Ptaa
		{
			public int n;
			public int nalloc;
			public IntPtr pta;
		}
	}
}
