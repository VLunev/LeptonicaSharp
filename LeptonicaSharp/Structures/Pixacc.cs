using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System;

namespace LeptonicaSharp
{
	//  ./pix.h (546, 8)
	/// <summary>
	/// Pix accumulator container
	/// </summary>
	/// <example>
	/// <code source="CSource\Struct_Pixacc.txt" language="C" title="./pix.h (546, 8)"/>
	/// </example>
	public partial class Pixacc : IDisposable
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] public IntPtr Pointer;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Marshal_Pixacc Values = new Marshal_Pixacc();
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Dictionary<String, Object> Caching = new Dictionary<String, Object>();

		public void Dispose()
		{
			Natives.pixaccDestroy(ref Pointer);
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

		public Pixacc(IntPtr PTR)
		{
			if (PTR != IntPtr.Zero) { Pointer = PTR; }

			Pointer = PTR; Marshal.PtrToStructure(Pointer, Values);
		}
		///  <summary>
		///  array width
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (548, 25)
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
		///  array height
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (549, 25)
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
		///  used to allow negative
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (550, 25)
		///  Org: [l_int32 offset]
		///  Msh: l_int32 | 1:Int |
		///  </remarks>
		public int offset
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
					return Values.offset;
				}
			}
		}

		///  <summary>
		///  the 32 bit accumulator pix
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (552, 25)
		///  Org: [struct Pix * pix]
		///  Msh: struct Pix * | 2:Struct |  | Typedef: Pix = Pix
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

		[StructLayout(LayoutKind.Sequential)]
		private class Marshal_Pixacc
		{
			public int w;
			public int h;
			public int offset;
			public IntPtr pix;
		}
	}

}
