using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System;

namespace LeptonicaSharp
{
	//  ./morph.h (62, 8)
	/// <summary>
	/// Selection
	/// </summary>
	/// <example>
	/// <code source="CSource\Struct_Sel.txt" language="C" title="./morph.h (62, 8)"/>
	/// </example>
	public partial class Sel : IDisposable
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] public IntPtr Pointer;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Marshal_Sel Values = new Marshal_Sel();
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Dictionary<String, Object> Caching = new Dictionary<String, Object>();

		public void Dispose()
		{
			Natives.selDestroy(ref Pointer);
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

		public Sel(IntPtr PTR)
		{
			if (PTR != IntPtr.Zero) { Pointer = PTR; }

			Pointer = PTR; Marshal.PtrToStructure(Pointer, Values);
		}

		public Sel(String Text, int w, int h, String name)
		{
			this.Pointer = LeptonicaSharp._All.selCreateFromString(Text, h, w, name).Pointer;
		}

		///  <summary>
		///  sel height
		///  </summary>
		///  <remarks>
		///  Loc: ./morph.h (64, 19)
		///  Org: [l_int32 sy]
		///  Msh: l_int32 | 1:Int |
		///  </remarks>
		public int sy
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
					return Values.sy;
				}
			}
		}

		///  <summary>
		///  sel width
		///  </summary>
		///  <remarks>
		///  Loc: ./morph.h (65, 19)
		///  Org: [l_int32 sx]
		///  Msh: l_int32 | 1:Int |
		///  </remarks>
		public int sx
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
					return Values.sx;
				}
			}
		}

		///  <summary>
		///  y location of sel origin
		///  </summary>
		///  <remarks>
		///  Loc: ./morph.h (66, 19)
		///  Org: [l_int32 cy]
		///  Msh: l_int32 | 1:Int |
		///  </remarks>
		public int cy
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
					return Values.cy;
				}
			}
		}

		///  <summary>
		///  x location of sel origin
		///  </summary>
		///  <remarks>
		///  Loc: ./morph.h (67, 19)
		///  Org: [l_int32 cx]
		///  Msh: l_int32 | 1:Int |
		///  </remarks>
		public int cx
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
					return Values.cx;
				}
			}
		}

		///  <summary>
		///  {0,1,2} data[i][j] in [row][col] order
		///  </summary>
		///  <remarks>
		///  Loc: ./morph.h (68, 19)
		///  Org: [l_int32 ** data]
		///  Msh: l_int32 ** | 3:Integer | List<int[]>
		///  </remarks>
		public List<int[]> data
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
					// Structs.cs : Struct 3 : Else
					int[] _Data1 = new int[sy];
					List<int[]> _DataFin = new List<int[]>();
					Marshal.Copy(Values.data, _Data1, 0, _Data1.Length);

					foreach (int eintrag in _Data1)
					{
						int[] _Data2 = new int[sx];
						Marshal.Copy((IntPtr)eintrag, _Data2, 0, _Data2.Length);
						_DataFin.Add(_Data2);
					}

					return _DataFin; ;
				}
			}
		}

		///  <summary>
		///  used to find sel by name
		///  </summary>
		///  <remarks>
		///  Loc: ./morph.h (69, 19)
		///  Org: [char * name]
		///  Msh: char * | 2:CharS |
		///  </remarks>
		public String name
		{
			get
			{
				if (Pointer == IntPtr.Zero)
				{
					return "";
				}
				else
				{
					Marshal.PtrToStructure(Pointer, Values);

					if (Values.name != IntPtr.Zero)
					{
						return Marshal.PtrToStringAnsi(Values.name);
					}
					else { return ""; };
				}
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		private class Marshal_Sel
		{
			public int sy;
			public int sx;
			public int cy;
			public int cx;
			public IntPtr data;
			public IntPtr name;
		}
	}

}
