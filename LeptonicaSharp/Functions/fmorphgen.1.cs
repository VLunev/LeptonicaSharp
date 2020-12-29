using System;
using System.IO;

using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LeptonicaSharp
{
	public partial class _All
	{

		// fmorphgen.1.c (37, 6)
		// pixMorphDwa_1(pixd, pixs, operation, selname) as Pix
		// pixMorphDwa_1(PIX *, PIX *, l_int32, char *) as PIX *
		///  <summary>
		/// (1) This simply adds a border, calls the appropriate
		/// pixFMorphopGen_(), and removes the border.
		/// See the notes for that function.<para/>
		///
		/// (2) The size of the border depends on the operation
		/// and the boundary conditions.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixMorphDwa_1/*"/>
		///  <param name="pixd">[in] - usual 3 choices: null, == pixs, != pixs</param>
		///  <param name="pixs">[in] - 1 bpp</param>
		///  <param name="operation">[in] - L_MORPH_DILATE, L_MORPH_ERODE, L_MORPH_OPEN, L_MORPH_CLOSE</param>
		///   <returns>pixd</returns>
		public static Pix pixMorphDwa_1(
						Pix pixd,
						Pix pixs,
						int operation,
						String selname)
		{
			if (pixd == null) {throw new ArgumentNullException  ("pixd cannot be Nothing");}

			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (selname == null) {throw new ArgumentNullException  ("selname cannot be Nothing");}

			if ((new List<int> {1}).Contains ((int)pixs.d) == false) { throw new ArgumentException ("1 bpp"); }
			IntPtr _Result = Natives.pixMorphDwa_1(pixd.Pointer, pixs.Pointer,   operation,   selname);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// fmorphgen.1.c (38, 6)
		// pixFMorphopGen_1(pixd, pixs, operation, selname) as Pix
		// pixFMorphopGen_1(PIX *, PIX *, l_int32, char *) as PIX *
		///  <summary>
		/// (1) This is a dwa operation, and the Sels must be limited in
		/// size to not more than 31 pixels about the origin.<para/>
		///
		/// (2) A border of appropriate size (32 pixels, or 64 pixels
		/// for safe closing with asymmetric b.c.) must be added before
		/// this function is called.<para/>
		///
		/// (3) This handles all required setting of the border pixels
		/// before erosion and dilation.<para/>
		///
		/// (4) The closing operation is safe no pixels can be removed
		/// near the boundary.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixFMorphopGen_1/*"/>
		///  <param name="pixd">[in] - usual 3 choices: null, == pixs, != pixs</param>
		///  <param name="pixs">[in] - 1 bpp</param>
		///  <param name="operation">[in] - L_MORPH_DILATE, L_MORPH_ERODE, L_MORPH_OPEN, L_MORPH_CLOSE</param>
		///   <returns>pixd</returns>
		public static Pix pixFMorphopGen_1(
						Pix pixd,
						Pix pixs,
						int operation,
						String selname)
		{
			if (pixd == null) {throw new ArgumentNullException  ("pixd cannot be Nothing");}

			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (selname == null) {throw new ArgumentNullException  ("selname cannot be Nothing");}

			if ((new List<int> {1}).Contains ((int)pixs.d) == false) { throw new ArgumentException ("1 bpp"); }
			IntPtr _Result = Natives.pixFMorphopGen_1(pixd.Pointer, pixs.Pointer,   operation,   selname);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// fmorphgen.1.c (39, 9)
		// fmorphopgen_low_1(datad, w, h, wpld, datas, wpls, index) as int
		// fmorphopgen_low_1(l_uint32 *, l_int32, l_int32, l_int32, l_uint32 *, l_int32, l_int32) as l_int32
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/fmorphopgen_low_1/*"/>
		///   <returns></returns>
		public static int fmorphopgen_low_1(
						object datad,
						int w,
						int h,
						int wpld,
						object datas,
						int wpls,
						int index)
		{
			if (datad == null) {throw new ArgumentNullException  ("datad cannot be Nothing");}

			if (datas == null) {throw new ArgumentNullException  ("datas cannot be Nothing");}

			int _Result = Natives.fmorphopgen_low_1(  datad,   w,   h,   wpld,   datas,   wpls,   index);
			return _Result;
		}


	}
}
