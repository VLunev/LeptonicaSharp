using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System;

namespace LeptonicaSharp
{
	//  ./morph.h (74, 8)
	/// <summary>
	/// Array of Sel
	/// </summary>
	/// <example>
	/// <code source="CSource\Struct_Sela.txt" language="C" title="./morph.h (74, 8)"/>
	/// </example>
	public partial class Sela : IDisposable
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] public IntPtr Pointer;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Marshal_Sela Values = new Marshal_Sela();
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Dictionary<String, Object> Caching = new Dictionary<String, Object>();

		public void Dispose()
		{
			Natives.selaDestroy(ref Pointer);
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

		public Sela(IntPtr PTR)
		{
			if (PTR != IntPtr.Zero) { Pointer = PTR; }

			Pointer = PTR; Marshal.PtrToStructure(Pointer, Values);
		}
		///  <summary>
		///  number of sel actually stored
		///  </summary>
		///  <remarks>
		///  Loc: ./morph.h (76, 22)
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
		///  size of allocated ptr array
		///  </summary>
		///  <remarks>
		///  Loc: ./morph.h (77, 22)
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
		///  sel ptr array
		///  </summary>
		///  <remarks>
		///  Loc: ./morph.h (78, 22)
		///  Org: [struct Sel ** sel]
		///  Msh: struct Sel ** | 3:StructDeclaration |  ... Marshal List of Class to PTR | Typedef: Sel = Sel
		///  </remarks>
		public List<Sel> sel
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
					if (Values.sel != IntPtr.Zero)
					{
						List<Sel> LSTRET = new List<Sel>();
						IntPtr[] LSTPTR = new IntPtr[Values.n];
						Marshal.Copy(Values.sel, LSTPTR, 0, Values.n);

						foreach (IntPtr Entry in LSTPTR)
						{
							LSTRET.Add(new Sel(Entry));
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
		private class Marshal_Sela
		{
			public int n;
			public int nalloc;
			public IntPtr sel;
		}
	}

}
