using System;
using System.IO;

using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LeptonicaSharp
{
	public partial class _All
	{

		// binreduce.c (71, 1)
		// pixReduceBinary2(pixs, intab) as Pix
		// pixReduceBinary2(PIX *, l_uint8 *) as PIX *
		///  <summary>
		/// (1) After folding, the data is in bytes 0 and 2 of the word,
		/// and the bits in each byte are in the following order
		/// (with 0 being the leftmost originating pair and 7 being
		/// the rightmost originating pair):
		/// 0 4 1 5 2 6 3 7
		/// These need to be permuted to
		/// 0 1 2 3 4 5 6 7
		/// which is done with an 8-bit table generated by makeSubsampleTab2x().
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixReduceBinary2/*"/>
		///  <param name="pixs">[in] - </param>
		///  <param name="intab">[in][optional] - if null, a table is made here and destroyed before exit</param>
		///   <returns>pixd 2x subsampled, or NULL on error</returns>
		public static Pix pixReduceBinary2(
						Pix pixs,
						Byte[] intab = null)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			IntPtr _Result = Natives.pixReduceBinary2(pixs.Pointer,   intab);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// binreduce.c (148, 1)
		// pixReduceRankBinaryCascade(pixs, level1, level2, level3, level4) as Pix
		// pixReduceRankBinaryCascade(PIX *, l_int32, l_int32, l_int32, l_int32) as PIX *
		///  <summary>
		/// (1) This performs up to four cascaded 2x rank reductions.<para/>
		///
		/// (2) Use level = 0 to truncate the cascade.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixReduceRankBinaryCascade/*"/>
		///  <param name="pixs">[in] - 1 bpp</param>
		///  <param name="level1">[in] - threshold, in the set {0, 1, 2, 3, 4}</param>
		///  <param name="level2">[in] - threshold, in the set {0, 1, 2, 3, 4}</param>
		///  <param name="level3">[in] - threshold, in the set {0, 1, 2, 3, 4}</param>
		///  <param name="level4">[in] - threshold, in the set {0, 1, 2, 3, 4}</param>
		///   <returns>pixd, or NULL on error</returns>
		public static Pix pixReduceRankBinaryCascade(
						Pix pixs,
						int level1,
						int level2,
						int level3,
						int level4)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if ((new List<int> {1}).Contains ((int)pixs.d) == false) { throw new ArgumentException ("1 bpp"); }
			IntPtr _Result = Natives.pixReduceRankBinaryCascade(pixs.Pointer,   level1,   level2,   level3,   level4);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// binreduce.c (223, 1)
		// pixReduceRankBinary2(pixs, level, intab) as Pix
		// pixReduceRankBinary2(PIX *, l_int32, l_uint8 *) as PIX *
		///  <summary>
		/// (1) pixd is downscaled by 2x from pixs.<para/>
		///
		/// (2) The rank threshold specifies the minimum number of ON
		/// pixels in each 2x2 region of pixs that are required to
		/// set the corresponding pixel ON in pixd.<para/>
		///
		/// (3) Rank filtering is done to the UL corner of each 2x2 pixel block,
		/// using only logical operations.  Then these pixels are chosen
		/// in the 2x subsampling process, subsampled, as described
		/// above in pixReduceBinary2().
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixReduceRankBinary2/*"/>
		///  <param name="pixs">[in] - 1 bpp</param>
		///  <param name="level">[in] - rank threshold: 1, 2, 3, 4</param>
		///  <param name="intab">[in][optional] - if null, a table is made here and destroyed before exit</param>
		///   <returns>pixd 1 bpp, 2x rank threshold reduced, or NULL on error</returns>
		public static Pix pixReduceRankBinary2(
						Pix pixs,
						int level,
						Byte[] intab = null)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if ((new List<int> {1}).Contains ((int)pixs.d) == false) { throw new ArgumentException ("1 bpp"); }
			IntPtr _Result = Natives.pixReduceRankBinary2(pixs.Pointer,   level,   intab);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// binreduce.c (384, 1)
		// makeSubsampleTab2x() as Byte[]
		// makeSubsampleTab2x() as l_uint8 *
		///  <summary>
		/// This table permutes the bits in a byte, from
		/// 0 4 1 5 2 6 3 7
		/// to
		/// 0 1 2 3 4 5 6 7
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/makeSubsampleTab2x/*"/>
		///   <returns>tab table of 256 permutations, or NULL on error</returns>
		public static Byte[] makeSubsampleTab2x()
		{
			Byte[] _Result = Natives.makeSubsampleTab2x();
			return _Result;
		}


	}
}
