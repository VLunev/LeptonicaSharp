using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System;

namespace LeptonicaSharp
{
	//  ./pix.h (134, 8)
	/// <summary>
	/// Basic Pix
	/// </summary>
	/// <example>
	/// <code source="CSource\Struct_Pix.txt" language="C" title="./pix.h (134, 8)"/>
	/// </example>
	public partial class Pix : IDisposable
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] public IntPtr Pointer;
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Marshal_Pix Values = new Marshal_Pix();
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] private Dictionary<String, Object> Caching = new Dictionary<String, Object>();


		public Pix(IntPtr PTR)
		{
			if (PTR == IntPtr.Zero)
				throw new ArgumentNullException("Pix PTR is zero");

			Pointer = PTR; 
			Marshal.PtrToStructure(Pointer, Values);
		}

		public static Pix Create(System.Drawing.Bitmap Bitmap)
		{
			using (var stream = new System.IO.MemoryStream())
            {
				Bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
				return _All.pixReadMemBmp(stream.GetBuffer(), (uint)stream.Length);
            }
		}
		public static Pix Create(int w, int h)
		{
			return LeptonicaSharp._All.pixCreate(w, h, 32);
		}

		/*public Pix()
		{
		}*/

		public void Dispose()
		{
			Natives.pixDestroy(ref Pointer);
			Pointer = IntPtr.Zero;

			foreach (KeyValuePair<string, object> Entry in Caching)
			{
				var disp = Entry.Value as IDisposable;

				if (disp != null)
					disp.Dispose();
			}

			Caching.Clear();
			Caching = null;
			System.GC.SuppressFinalize(this);
		}

		///  <summary>
		///  width in pixels
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (136, 26)
		///  Org: [l_uint32 w]
		///  Msh: l_uint32 | 1:UInt |
		///  </remarks>
		public uint w
		{
			get
			{
				if (Pointer == IntPtr.Zero)
					return 0;

				Marshal.PtrToStructure(Pointer, Values);
				return Values.w;
			}
		}

		///  <summary>
		///  height in pixels
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (137, 26)
		///  Org: [l_uint32 h]
		///  Msh: l_uint32 | 1:UInt |
		///  </remarks>
		public uint h
		{
			get
			{
				if (Pointer == IntPtr.Zero)
					return 0;

				Marshal.PtrToStructure(Pointer, Values);
				return Values.h;
			}
		}

		///  <summary>
		///  depth in bits (bpp)
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (138, 26)
		///  Org: [l_uint32 d]
		///  Msh: l_uint32 | 1:UInt |
		///  </remarks>
		public uint d
		{
			get
			{
				if (Pointer == IntPtr.Zero)
					return 0;

				Marshal.PtrToStructure(Pointer, Values);
				return Values.d;
			}
		}

		///  <summary>
		///  number of samples per pixel
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (139, 26)
		///  Org: [l_uint32 spp]
		///  Msh: l_uint32 | 1:UInt |
		///  </remarks>
		public uint spp
		{
			get
			{
				if (Pointer == IntPtr.Zero)
					return 0;

				Marshal.PtrToStructure(Pointer, Values);
				return Values.spp;
			}
		}

		///  <summary>
		///  32-bit words/line
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (140, 26)
		///  Org: [l_uint32 wpl]
		///  Msh: l_uint32 | 1:UInt |
		///  </remarks>
		public uint wpl
		{
			get
			{
				if (Pointer == IntPtr.Zero)
					return 0;

				Marshal.PtrToStructure(Pointer, Values);
				return Values.wpl;
			}
		}

		///  <summary>
		///  reference count (1 if no clones)
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (141, 26)
		///  Org: [l_uint32 refcount]
		///  Msh: l_uint32 | 1:UInt |
		///  </remarks>
		public uint refcount
		{
			get
			{
				if (Pointer == IntPtr.Zero)
					return 0;

				Marshal.PtrToStructure(Pointer, Values);
				return Values.refcount;
			}
		}

		///  <summary>
		///  image res (ppi) in x direction
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (142, 26)
		///  Org: [l_int32 xres]
		///  Msh: l_int32 | 1:Int |
		///  </remarks>
		public int xres
		{
			get
			{
				if (Pointer == IntPtr.Zero)
					return 0;

				Marshal.PtrToStructure(Pointer, Values);
				return Values.xres;
			}
		}

		///  <summary>
		///  image res (ppi) in y direction
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (144, 26)
		///  Org: [l_int32 yres]
		///  Msh: l_int32 | 1:Int |
		///  </remarks>
		public int yres
		{
			get
			{
				if (Pointer == IntPtr.Zero)
					return 0;

				Marshal.PtrToStructure(Pointer, Values);
				return Values.yres;
			}
		}

		///  <summary>
		///  input file format, IFF_*
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (146, 26)
		///  Org: [l_int32 informat]
		///  Msh: l_int32 | 1:Int |
		///  </remarks>
		public IFF informat
		{
			get
			{
				if (Pointer == IntPtr.Zero)
					return 0;

				Marshal.PtrToStructure(Pointer, Values);
				return (IFF)Values.informat;
			}
		}

		///  <summary>
		///  special instructions for I/O, etc
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (147, 26)
		///  Org: [l_int32 special]
		///  Msh: l_int32 | 1:Int |
		///  </remarks>
		public int special
		{
			get
			{
				if (Pointer == IntPtr.Zero)
					return 0;

				Marshal.PtrToStructure(Pointer, Values);
				return Values.special;
			}
		}

		///  <summary>
		///  text string associated with pix
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (148, 26)
		///  Org: [char * text]
		///  Msh: char * | 2:CharS |
		///  </remarks>
		public String text
		{
			get
			{
				if (Pointer == IntPtr.Zero)
					return "";

				Marshal.PtrToStructure(Pointer, Values);

				if (Values.text != IntPtr.Zero)
					return Marshal.PtrToStringAnsi(Values.text);
				else 
					return "";

			}
		}

		///  <summary>
		///  colormap (may be null)
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (149, 26)
		///  Org: [struct PixColormap * colormap]
		///  Msh: struct PixColormap * | 2:Struct |
		///  </remarks>
		public PixColormap colormap
		{
			get
			{
				if (Pointer == IntPtr.Zero)
					return null;

				Marshal.PtrToStructure(Pointer, Values);

				// Depth:2 'StructureDeclaration'
				if (Values.colormap != IntPtr.Zero)
					return new PixColormap(Values.colormap);
				else
					return null;
			}
		}

		///  <summary>
		///  the image data
		///  </summary>
		///  <remarks>
		///  Loc: ./pix.h (150, 26)
		///  Org: [l_uint32 * data]
		///  Msh: l_uint32 * | 2:UInt |  ... UInt = 4 Byte * Len)
		///  </remarks>
		public Byte[] data
		{
			get
			{
				if (Pointer == IntPtr.Zero)
					return null;

				Marshal.PtrToStructure(Pointer, Values);

				if (Values.data != IntPtr.Zero)
				{
					Byte[] _data = new Byte[(h * (wpl * 4))];
					Marshal.Copy(Values.data, _data, 0, _data.Length);
					return _data;
				}
				else
					return null;

			}
		}

		[StructLayout(LayoutKind.Sequential)]
		private class Marshal_Pix
		{
			public uint w;
			public uint h;
			public uint d;
			public uint spp;
			public uint wpl;
			public uint refcount;
			public int xres;
			public int yres;
			public int informat;
			public int special;
			public IntPtr text;
			public IntPtr colormap;
			public IntPtr data;
		}
	}

}
