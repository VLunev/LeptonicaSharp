using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System;

namespace LeptonicaSharp
{
	//  ./imageio.h (211, 8)
	/// <summary>
	/// Intermediate pdf generation data
	/// </summary>
	/// <example>
	/// <code source="CSource\Struct_L_Pdf_Data.txt" language="C" title="./imageio.h (211, 8)"/>
	/// </example>
	public partial class L_Pdf_Data : IDisposable
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] public IntPtr Pointer;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Marshal_L_Pdf_Data Values = new Marshal_L_Pdf_Data();
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

		public L_Pdf_Data(IntPtr PTR)
		{
			if (PTR != IntPtr.Zero) { Pointer = PTR; }

			Pointer = PTR; Marshal.PtrToStructure(Pointer, Values);
		}

		public L_Pdf_Data()
		{
			Values = new Marshal_L_Pdf_Data();
			Values.cida = new L_Ptra(10).Pointer;
			Values.objloc = new L_Dna(10).Pointer;
			Values.objsize = new L_Dna(10).Pointer;
			Values.sacmap = new Sarray(10).Pointer;
			Values.saprex = new Sarray(1).Pointer;
			Values.wh = new Pta(10).Pointer;
			Values.xy = new Pta(10).Pointer;
			Pointer = Marshal.AllocHGlobal(Marshal.SizeOf(Values));
			Marshal.StructureToPtr(Values, Pointer, true);
		}

		///  <summary>
		///  optional title for pdf
		///  </summary>
		///  <remarks>
		///  Loc: ./imageio.h (213, 24)
		///  Org: [char * title]
		///  Msh: char * | 2:CharS |
		///  </remarks>
		public String title
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

					if (Values.title != IntPtr.Zero)
					{
						return Marshal.PtrToStringAnsi(Values.title);
					}
					else { return ""; };
				}
			}
		}

		///  <summary>
		///  number of images
		///  </summary>
		///  <remarks>
		///  Loc: ./imageio.h (214, 24)
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
		///  number of colormaps
		///  </summary>
		///  <remarks>
		///  Loc: ./imageio.h (215, 24)
		///  Org: [l_int32 ncmap]
		///  Msh: l_int32 | 1:Int |
		///  </remarks>
		public int ncmap
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
					return Values.ncmap;
				}
			}
		}

		///  <summary>
		///  array of compressed image data
		///  </summary>
		///  <remarks>
		///  Loc: ./imageio.h (216, 24)
		///  Org: [struct L_Ptra * cida]
		///  Msh: struct L_Ptra * | 2:Struct |  | Typedef: L_Ptra = L_Ptra
		///  </remarks>
		public L_Ptra cida
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
					if (Values.cida != IntPtr.Zero)
					{
						return new L_Ptra(Values.cida);
					}
					else
					{
						return null;
					};
				}
			}
		}

		///  <summary>
		///  %PDF-1.2 id string
		///  </summary>
		///  <remarks>
		///  Loc: ./imageio.h (217, 24)
		///  Org: [char * id]
		///  Msh: char * | 2:CharS |
		///  </remarks>
		public String id
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

					if (Values.id != IntPtr.Zero)
					{
						return Marshal.PtrToStringAnsi(Values.id);
					}
					else { return ""; };
				}
			}
		}

		///  <summary>
		///  catalog string
		///  </summary>
		///  <remarks>
		///  Loc: ./imageio.h (218, 24)
		///  Org: [char * obj1]
		///  Msh: char * | 2:CharS |
		///  </remarks>
		public String obj1
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

					if (Values.obj1 != IntPtr.Zero)
					{
						return Marshal.PtrToStringAnsi(Values.obj1);
					}
					else { return ""; };
				}
			}
		}

		///  <summary>
		///  metadata string
		///  </summary>
		///  <remarks>
		///  Loc: ./imageio.h (219, 24)
		///  Org: [char * obj2]
		///  Msh: char * | 2:CharS |
		///  </remarks>
		public String obj2
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

					if (Values.obj2 != IntPtr.Zero)
					{
						return Marshal.PtrToStringAnsi(Values.obj2);
					}
					else { return ""; };
				}
			}
		}

		///  <summary>
		///  pages string
		///  </summary>
		///  <remarks>
		///  Loc: ./imageio.h (220, 24)
		///  Org: [char * obj3]
		///  Msh: char * | 2:CharS |
		///  </remarks>
		public String obj3
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

					if (Values.obj3 != IntPtr.Zero)
					{
						return Marshal.PtrToStringAnsi(Values.obj3);
					}
					else { return ""; };
				}
			}
		}

		///  <summary>
		///  page string (variable data)
		///  </summary>
		///  <remarks>
		///  Loc: ./imageio.h (221, 24)
		///  Org: [char * obj4]
		///  Msh: char * | 2:CharS |
		///  </remarks>
		public String obj4
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

					if (Values.obj4 != IntPtr.Zero)
					{
						return Marshal.PtrToStringAnsi(Values.obj4);
					}
					else { return ""; };
				}
			}
		}

		///  <summary>
		///  content string (variable data)
		///  </summary>
		///  <remarks>
		///  Loc: ./imageio.h (222, 24)
		///  Org: [char * obj5]
		///  Msh: char * | 2:CharS |
		///  </remarks>
		public String obj5
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

					if (Values.obj5 != IntPtr.Zero)
					{
						return Marshal.PtrToStringAnsi(Values.obj5);
					}
					else { return ""; };
				}
			}
		}

		///  <summary>
		///  post-binary-stream string
		///  </summary>
		///  <remarks>
		///  Loc: ./imageio.h (223, 24)
		///  Org: [char * poststream]
		///  Msh: char * | 2:CharS |
		///  </remarks>
		public String poststream
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

					if (Values.poststream != IntPtr.Zero)
					{
						return Marshal.PtrToStringAnsi(Values.poststream);
					}
					else { return ""; };
				}
			}
		}

		///  <summary>
		///  trailer string (variable data)
		///  </summary>
		///  <remarks>
		///  Loc: ./imageio.h (224, 24)
		///  Org: [char * trailer]
		///  Msh: char * | 2:CharS |
		///  </remarks>
		public String trailer
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

					if (Values.trailer != IntPtr.Zero)
					{
						return Marshal.PtrToStringAnsi(Values.trailer);
					}
					else { return ""; };
				}
			}
		}

		///  <summary>
		///  store (xpt, ypt) array
		///  </summary>
		///  <remarks>
		///  Loc: ./imageio.h (225, 24)
		///  Org: [struct Pta * xy]
		///  Msh: struct Pta * | 2:Struct |
		///  </remarks>
		public Pta xy
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
					if (Values.xy != IntPtr.Zero)
					{
						return new Pta(Values.xy);
					}
					else
					{
						return null;
					};
				}
			}
		}

		///  <summary>
		///  store (wpt, hpt) array
		///  </summary>
		///  <remarks>
		///  Loc: ./imageio.h (226, 24)
		///  Org: [struct Pta * wh]
		///  Msh: struct Pta * | 2:Struct |
		///  </remarks>
		public Pta wh
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
					if (Values.wh != IntPtr.Zero)
					{
						return new Pta(Values.wh);
					}
					else
					{
						return null;
					};
				}
			}
		}

		///  <summary>
		///  bounding region for all images
		///  </summary>
		///  <remarks>
		///  Loc: ./imageio.h (227, 24)
		///  Org: [struct Box * mediabox]
		///  Msh: struct Box * | 2:Struct |
		///  </remarks>
		public Box mediabox
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
					if (Values.mediabox != IntPtr.Zero)
					{
						return new Box(Values.mediabox);
					}
					else
					{
						return null;
					};
				}
			}
		}

		///  <summary>
		///  pre-binary-stream xobject strings
		///  </summary>
		///  <remarks>
		///  Loc: ./imageio.h (228, 24)
		///  Org: [struct Sarray * saprex]
		///  Msh: struct Sarray * | 2:Struct |  | Typedef: Sarray = Sarray
		///  </remarks>
		public Sarray saprex
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
					if (Values.saprex != IntPtr.Zero)
					{
						return new Sarray(Values.saprex);
					}
					else
					{
						return null;
					};
				}
			}
		}

		///  <summary>
		///  colormap pdf object strings
		///  </summary>
		///  <remarks>
		///  Loc: ./imageio.h (229, 24)
		///  Org: [struct Sarray * sacmap]
		///  Msh: struct Sarray * | 2:Struct |  | Typedef: Sarray = Sarray
		///  </remarks>
		public Sarray sacmap
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
					if (Values.sacmap != IntPtr.Zero)
					{
						return new Sarray(Values.sacmap);
					}
					else
					{
						return null;
					};
				}
			}
		}

		///  <summary>
		///  sizes of each pdf string object
		///  </summary>
		///  <remarks>
		///  Loc: ./imageio.h (230, 24)
		///  Org: [struct L_Dna * objsize]
		///  Msh: struct L_Dna * | 2:Struct |  | Typedef: L_Dna = L_Dna
		///  </remarks>
		public L_Dna objsize
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
					if (Values.objsize != IntPtr.Zero)
					{
						return new L_Dna(Values.objsize);
					}
					else
					{
						return null;
					};
				}
			}
		}

		///  <summary>
		///  location of each pdf string object
		///  </summary>
		///  <remarks>
		///  Loc: ./imageio.h (231, 24)
		///  Org: [struct L_Dna * objloc]
		///  Msh: struct L_Dna * | 2:Struct |  | Typedef: L_Dna = L_Dna
		///  </remarks>
		public L_Dna objloc
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
					if (Values.objloc != IntPtr.Zero)
					{
						return new L_Dna(Values.objloc);
					}
					else
					{
						return null;
					};
				}
			}
		}

		///  <summary>
		///  location of xref
		///  </summary>
		///  <remarks>
		///  Loc: ./imageio.h (232, 24)
		///  Org: [l_int32 xrefloc]
		///  Msh: l_int32 | 1:Int |
		///  </remarks>
		public int xrefloc
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
					return Values.xrefloc;
				}
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		private class Marshal_L_Pdf_Data
		{
			public IntPtr title;
			public int n;
			public int ncmap;
			public IntPtr cida;
			public IntPtr id;
			public IntPtr obj1;
			public IntPtr obj2;
			public IntPtr obj3;
			public IntPtr obj4;
			public IntPtr obj5;
			public IntPtr poststream;
			public IntPtr trailer;
			public IntPtr xy;
			public IntPtr wh;
			public IntPtr mediabox;
			public IntPtr saprex;
			public IntPtr sacmap;
			public IntPtr objsize;
			public IntPtr objloc;
			public int xrefloc;
		}
	}

}
