using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System;

namespace LeptonicaSharp.Structures
{
	//  ./ccbord.h (91, 8)
	/// <summary>
	/// CCBord contains:
	/// </summary>
	/// <example>
	/// <code source="CSource\Struct_CCBord.txt" language="C" title="./ccbord.h (91, 8)"/>
	/// </example>
	public partial class CCBord : IDisposable
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] public IntPtr Pointer;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Marshal_CCBord Values = new Marshal_CCBord();
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

		public CCBord(IntPtr PTR)
		{
			if (PTR != IntPtr.Zero) { Pointer = PTR; }

			Pointer = PTR; Marshal.PtrToStructure(Pointer, Values);
		}
		///  <summary>
		///  component bitmap (min size)
		///  </summary>
		///  <remarks>
		///  Loc: ./ccbord.h (93, 26)
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
		///  regions of each closed curve
		///  </summary>
		///  <remarks>
		///  Loc: ./ccbord.h (94, 26)
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

		///  <summary>
		///  initial border pixel locations
		///  </summary>
		///  <remarks>
		///  Loc: ./ccbord.h (95, 26)
		///  Org: [struct Pta * start]
		///  Msh: struct Pta * | 2:Struct |
		///  </remarks>
		public Pta start
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
					if (Values.start != IntPtr.Zero)
					{
						return new Pta(Values.start);
					}
					else
					{
						return null;
					};
				}
			}
		}

		///  <summary>
		///  number of handles start at 1
		///  </summary>
		///  <remarks>
		///  Loc: ./ccbord.h (96, 26)
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
		///  ptaa of chain pixels (local)
		///  </summary>
		///  <remarks>
		///  Loc: ./ccbord.h (97, 26)
		///  Org: [struct Ptaa * local]
		///  Msh: struct Ptaa * | 2:Struct |
		///  </remarks>
		public Ptaa local
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
					if (Values.local != IntPtr.Zero)
					{
						return new Ptaa(Values.local);
					}
					else
					{
						return null;
					};
				}
			}
		}

		///  <summary>
		///  ptaa of chain pixels (global)
		///  </summary>
		///  <remarks>
		///  Loc: ./ccbord.h (98, 26)
		///  Org: [struct Ptaa * global]
		///  Msh: struct Ptaa * | 2:Struct |
		///  </remarks>
		public Ptaa _global_
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
					if (Values._global_ != IntPtr.Zero)
					{
						return new Ptaa(Values._global_);
					}
					else
					{
						return null;
					};
				}
			}
		}

		///  <summary>
		///  numaa of chain code (step dir)
		///  </summary>
		///  <remarks>
		///  Loc: ./ccbord.h (99, 26)
		///  Org: [struct Numaa * step]
		///  Msh: struct Numaa * | 2:Struct |  | Typedef: Numaa = Numaa
		///  </remarks>
		public Numaa _step_
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
					if (Values._step_ != IntPtr.Zero)
					{
						return new Numaa(Values._step_);
					}
					else
					{
						return null;
					};
				}
			}
		}

		///  <summary>
		///  pta of single chain (local)
		///  </summary>
		///  <remarks>
		///  Loc: ./ccbord.h (100, 26)
		///  Org: [struct Pta * splocal]
		///  Msh: struct Pta * | 2:Struct |
		///  </remarks>
		public Pta splocal
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
					if (Values.splocal != IntPtr.Zero)
					{
						return new Pta(Values.splocal);
					}
					else
					{
						return null;
					};
				}
			}
		}

		///  <summary>
		///  pta of single chain (global)
		///  </summary>
		///  <remarks>
		///  Loc: ./ccbord.h (101, 26)
		///  Org: [struct Pta * spglobal]
		///  Msh: struct Pta * | 2:Struct |
		///  </remarks>
		public Pta spglobal
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
					if (Values.spglobal != IntPtr.Zero)
					{
						return new Pta(Values.spglobal);
					}
					else
					{
						return null;
					};
				}
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		private class Marshal_CCBord
		{
			public IntPtr pix;
			public IntPtr boxa;
			public IntPtr start;
			public int refcount;
			public IntPtr local;
			public IntPtr _global_;
			public IntPtr _step_;
			public IntPtr splocal;
			public IntPtr spglobal;
		}
	}

}
