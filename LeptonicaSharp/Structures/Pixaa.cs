using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System;

namespace LeptonicaSharp
{
	//  ./pix.h (465, 8)
	/// <summary>
	/// Array of arrays of pix
	/// </summary>
	/// <example>
	/// <code source="CSource\Struct_Pixaa.txt" language="C" title="./pix.h (465, 8)"/>
	/// </example>
	public partial class Pixaa : IDisposable
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] public IntPtr Pointer;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Marshal_Pixaa Values = new Marshal_Pixaa();
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Dictionary<String, Object> Caching = new Dictionary<String, Object>();

		public void Dispose()
		{
			Natives.pixaaDestroy(ref Pointer);
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

		public Pixaa(IntPtr PTR)
		{
			if (PTR != IntPtr.Zero) { Pointer = PTR; }

			Pointer = PTR; Marshal.PtrToStructure(Pointer, Values);
		}
		///  <summary>
		///  number of Pixa in ptr array
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (467, 25)
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
		///  number of Pixa ptrs allocated
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (468, 25)
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
		///  array of ptrs to pixa
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (469, 25)
		///  Org: [struct Pixa ** pixa]
		///  Msh: struct Pixa ** | 3:StructDeclaration |  ... Marshal List of Class to PTR | Typedef: Pixa = Pixa
		///  </remarks>
		public List<Pixa> pixa
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
					if (Values.pixa != IntPtr.Zero)
					{
						List<Pixa> LSTRET = new List<Pixa>();
						IntPtr[] LSTPTR = new IntPtr[Values.n];
						Marshal.Copy(Values.pixa, LSTPTR, 0, Values.n);

						foreach (IntPtr Entry in LSTPTR)
						{
							LSTRET.Add(new Pixa(Entry));
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
		///  Loc: ./pix.h (470, 25)
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
		private class Marshal_Pixaa
		{
			public int n;
			public int nalloc;
			public IntPtr pixa;
			public IntPtr boxa;
		}
	}

}
