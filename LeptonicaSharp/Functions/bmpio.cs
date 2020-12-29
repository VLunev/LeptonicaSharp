using System;
using System.IO;

using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LeptonicaSharp
{
	public partial class _All
	{

		// bmpio.c (89, 1)
		// pixReadStreamBmp(fp) as Pix
		// pixReadStreamBmp(FILE *) as PIX *
		///  <summary>
		/// (1) Here are references on the bmp file format:
		/// http://en.wikipedia.org/wiki/BMP_file_format
		/// http://www.fortunecity.com/skyscraper/windows/364/bmpffrmt.html
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixReadStreamBmp/*"/>
		///  <param name="fp">[in] - file stream opened for read</param>
		///   <returns>pix, or NULL on error</returns>
		public static Pix pixReadStreamBmp(
						FILE fp)
		{
			if (fp == null) {throw new ArgumentNullException  ("fp cannot be Nothing");}

			IntPtr _Result = Natives.pixReadStreamBmp(fp.Pointer);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// bmpio.c (119, 1)
		// pixReadMemBmp(cdata, size) as Pix
		// pixReadMemBmp(const l_uint8 *, size_t) as PIX *
		///  <summary>
		/// pixReadMemBmp()
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixReadMemBmp/*"/>
		///  <param name="cdata">[in] - bmp data</param>
		///  <param name="size">[in] - number of bytes of bmp-formatted data</param>
		///   <returns>pix, or NULL on error</returns>
		public static Pix pixReadMemBmp(
						Byte[] cdata,
						uint size)
		{
			if (cdata == null) {throw new ArgumentNullException  ("cdata cannot be Nothing");}

			IntPtr _Result = Natives.pixReadMemBmp(  cdata,   size);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// bmpio.c (351, 1)
		// pixWriteStreamBmp(fp, pix) as int
		// pixWriteStreamBmp(FILE *, PIX *) as l_ok
		///  <summary>
		/// pixWriteStreamBmp()
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixWriteStreamBmp/*"/>
		///  <param name="fp">[in] - file stream</param>
		///  <param name="pix">[in] - all depths</param>
		///   <returns>0 if OK, 1 on error</returns>
		public static int pixWriteStreamBmp(
						FILE fp,
						Pix pix)
		{
			if (fp == null) {throw new ArgumentNullException  ("fp cannot be Nothing");}

			if (pix == null) {throw new ArgumentNullException  ("pix cannot be Nothing");}

			int _Result = Natives.pixWriteStreamBmp(fp.Pointer, pix.Pointer);
			return _Result;
		}

		// bmpio.c (396, 1)
		// pixWriteMemBmp(pfdata, pfsize, pixs) as int
		// pixWriteMemBmp(l_uint8 **, size_t *, PIX *) as l_ok
		///  <summary>
		/// (1) 2 bpp bmp files are not valid in the spec, and are
		/// written as 8 bpp.<para/>
		///
		/// (2) pix with depth smaller or equal 8 bpp are written with a colormap.
		/// 16 bpp gray and 32 bpp rgb pix are written without a colormap.<para/>
		///
		/// (3) The transparency component in an rgb pix is ignored.
		/// All 32 bpp pix have the bmp alpha component set to 255 (opaque).<para/>
		///
		/// (4) The bmp colormap entries, RGBA_QUAD, are the same as
		/// the ones used for colormaps in leptonica.  This allows
		/// a simple memcpy for bmp output.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixWriteMemBmp/*"/>
		///  <param name="pfdata">[out] - data of bmp formatted image</param>
		///  <param name="pfsize">[out] - size of returned data</param>
		///  <param name="pixs">[in] - 1, 2, 4, 8, 16, 32 bpp</param>
		///   <returns>0 if OK, 1 on error</returns>
		public static int pixWriteMemBmp(
						out Byte[] pfdata,
						out uint pfsize,
						Pix pixs)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if ((new List<int> {1, 2, 4, 8, 16, 32}).Contains ((int)pixs.d) == false) { throw new ArgumentException ("1, 2, 4, 8, 16, 32 bpp"); }
			IntPtr pfdataPtr = IntPtr.Zero;
			int _Result = Natives.pixWriteMemBmp(out  pfdataPtr, out  pfsize, pixs.Pointer);
			Byte[] pfdataGen = new Byte[pfsize];

			if (pfdataPtr != IntPtr.Zero) {
				Marshal.Copy(pfdataPtr, pfdataGen, 0, pfdataGen.Length);
			}

			pfdata = pfdataGen;
			return _Result;
		}


	}
}
