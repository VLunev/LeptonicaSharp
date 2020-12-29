using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System;

namespace LeptonicaSharp
{
	//  ./ptra.h (62, 8)
	/// <summary>
	/// Array of generic pointer arrays
	/// </summary>
	/// <example>
	/// <code source="CSource\Struct_L_Ptraa.txt" language="C" title="./ptra.h (62, 8)"/>
	/// </example>
	public partial class L_Ptraa : IDisposable
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] public IntPtr Pointer;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Marshal_L_Ptraa Values = new Marshal_L_Ptraa();
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

		public L_Ptraa(IntPtr PTR)
		{
			if (PTR != IntPtr.Zero) { Pointer = PTR; }

			Pointer = PTR; Marshal.PtrToStructure(Pointer, Values);
		}
		///  <summary>
		///  size of allocated ptr array
		///  </summary>
		///  <remarks>
		///  Loc: ./ptra.h (64, 22)
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
		///  array of ptra
		///  </summary>
		///  <remarks>
		///  Loc: ./ptra.h (65, 22)
		///  Org: [struct L_Ptra ** ptra]
		///  Msh: struct L_Ptra ** | 3:StructDeclaration |  ... Marshal List of Class to PTR | Typedef: L_Ptra = L_Ptra
		///  </remarks>
		public List<L_Ptra> ptra
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
					if (Values.ptra != IntPtr.Zero)
					{
						List<L_Ptra> LSTRET = new List<L_Ptra>();
						IntPtr[] LSTPTR = new IntPtr[Values.nalloc];
						Marshal.Copy(Values.ptra, LSTPTR, 0, Values.nalloc);

						foreach (IntPtr Entry in LSTPTR)
						{
							LSTRET.Add(new L_Ptra(Entry));
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
		private class Marshal_L_Ptraa
		{
			public int nalloc;
			public IntPtr ptra;
		}
	}
}
