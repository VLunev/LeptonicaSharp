using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System;

namespace LeptonicaSharp.Structures
{
	//  ./ccbord.h (106, 8)
	/// <summary>
	/// Array of CCBord
	/// </summary>
	/// <example>
	/// <code source="CSource\Struct_CCBorda.txt" language="C" title="./ccbord.h (106, 8)"/>
	/// </example>
	public partial class CCBorda : IDisposable
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] public IntPtr Pointer;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Marshal_CCBorda Values = new Marshal_CCBorda();
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

		public CCBorda(IntPtr PTR)
		{
			if (PTR != IntPtr.Zero) { Pointer = PTR; }

			Pointer = PTR; Marshal.PtrToStructure(Pointer, Values);
		}
		///  <summary>
		///  input pix (may be null)
		///  </summary>
		///  <remarks>
		///  Loc: ./ccbord.h (108, 26)
		///  Org: [struct Pix * pix]
		///  Msh: struct Pix * | 2:Struct |
		///  </remarks>
		public Pix pix
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
					if (Values.pix != IntPtr.Zero)
					{
						return new Pix(Values.pix);
					}
					else
					{
						return null;
					};
				}
			}
		}

		///  <summary>
		///  width of pix
		///  </summary>
		///  <remarks>
		///  Loc: ./ccbord.h (109, 26)
		///  Org: [l_int32 w]
		///  Msh: l_int32 | 1:Int |
		///  </remarks>
		public int w
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
					return Values.w;
				}
			}
		}

		///  <summary>
		///  height of pix
		///  </summary>
		///  <remarks>
		///  Loc: ./ccbord.h (110, 26)
		///  Org: [l_int32 h]
		///  Msh: l_int32 | 1:Int |
		///  </remarks>
		public int h
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
					return Values.h;
				}
			}
		}

		///  <summary>
		///  number of ccbord in ptr array
		///  </summary>
		///  <remarks>
		///  Loc: ./ccbord.h (111, 26)
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
		///  number of ccbord ptrs allocated
		///  </summary>
		///  <remarks>
		///  Loc: ./ccbord.h (112, 26)
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
		///  ccb ptr array
		///  </summary>
		///  <remarks>
		///  Loc: ./ccbord.h (113, 26)
		///  Org: [struct CCBord ** ccb]
		///  Msh: struct CCBord ** | 3:StructDeclaration |  ... Marshal List of Class to PTR | Typedef: CCBord = CCBord
		///  </remarks>
		public List<CCBord> ccb
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
					if (Values.ccb != IntPtr.Zero)
					{
						List<CCBord> LSTRET = new List<CCBord>();
						IntPtr[] LSTPTR = new IntPtr[Values.n];
						Marshal.Copy(Values.ccb, LSTPTR, 0, Values.n);

						foreach (IntPtr Entry in LSTPTR)
						{
							LSTRET.Add(new CCBord(Entry));
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
		private class Marshal_CCBorda
		{
			public IntPtr pix;
			public int w;
			public int h;
			public int n;
			public int nalloc;
			public IntPtr ccb;
		}
	}

}
