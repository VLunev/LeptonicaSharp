using System;
using System.IO;

using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LeptonicaSharp
{
	public partial class _All
	{

		// baseline.c (113, 1)
		// pixFindBaselines(pixs, ppta, pixadb) as Numa
		// pixFindBaselines(PIX *, PTA **, PIXA *) as NUMA *
		///  <summary>
		/// (1) Input binary image must have text lines already aligned
		/// horizontally.  This can be done by either rotating the
		/// image with pixDeskew(), or, if a projective transform
		/// is required, by doing pixDeskewLocal() first.<para/>
		///
		/// (2) Input null for [and]pta if you don't want this returned.
		/// The pta will come in pairs of points (left and right end
		/// of each baseline).<para/>
		///
		/// (3) Caution: this will not work properly on text with multiple
		/// columns, where the lines are not aligned between columns.
		/// If there are multiple columns, they should be extracted
		/// separately before finding the baselines.<para/>
		///
		/// (4) This function constructs different types of output
		/// for baselines namely, a set of raster line values and
		/// a set of end points of each baseline.<para/>
		///
		/// (5) This function was designed to handle short and long text lines
		/// without using dangerous thresholds on the peak heights.  It does
		/// this by combining the differential signal with a morphological
		/// analysis of the locations of the text lines.  One can also
		/// combine this data to normalize the peak heights, by weighting
		/// the differential signal in the region of each baseline
		/// by the inverse of the width of the text line found there.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixFindBaselines/*"/>
		///  <param name="pixs">[in] - 1 bpp, 300 ppi</param>
		///  <param name="ppta">[out][optional] - pairs of pts corresponding to approx. ends of each text line</param>
		///  <param name="pixadb">[in] - for debug output use NULL to skip</param>
		///   <returns>na of baseline y values, or NULL on error</returns>
		public static Numa pixFindBaselines(
						Pix pixs,
						out Pta ppta,
						Pixa pixadb)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (pixadb == null) {throw new ArgumentNullException  ("pixadb cannot be Nothing");}

			IntPtr pptaPtr = IntPtr.Zero;
			IntPtr _Result = Natives.pixFindBaselines(pixs.Pointer, out pptaPtr, pixadb.Pointer);
			if (pptaPtr == IntPtr.Zero) {ppta = null;} else { ppta = new Pta(pptaPtr); };

			if (_Result == IntPtr.Zero) {return null;}

			return  new Numa(_Result);
		}

		// baseline.c (320, 1)
		// pixDeskewLocal(pixs, nslices, redsweep, redsearch, sweeprange, sweepdelta, minbsdelta) as Pix
		// pixDeskewLocal(PIX *, l_int32, l_int32, l_int32, l_float32, l_float32, l_float32) as PIX *
		///  <summary>
		/// (1) This function allows deskew of a page whose skew changes
		/// approximately linearly with vertical position.  It uses
		/// a projective transform that in effect does a differential
		/// shear about the LHS of the page, and makes all text lines
		/// horizontal.<para/>
		///
		/// (2) The origin of the keystoning can be either a cheap document
		/// feeder that rotates the page as it is passed through, or a
		/// camera image taken from either the left or right side
		/// of the vertical.<para/>
		///
		/// (3) The image transformation is a projective warping,
		/// not a rotation.  Apart from this function, the text lines
		/// must be properly aligned vertically with respect to each
		/// other.  This can be done by pre-processing the page e.g.,
		/// by rotating or horizontally shearing it.
		/// Typically, this can be achieved by vertically aligning
		/// the page edge.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixDeskewLocal/*"/>
		///  <param name="pixs">[in] - 1 bpp</param>
		///  <param name="nslices">[in] - the number of horizontal overlapping slices must be larger than 1 and not exceed 20 use 0 for default</param>
		///  <param name="redsweep">[in] - sweep reduction factor: 1, 2, 4 or 8 use 0 for default value</param>
		///  <param name="redsearch">[in] - search reduction factor: 1, 2, 4 or 8, and not larger than redsweep use 0 for default value</param>
		///  <param name="sweeprange">[in] - half the full range, assumed about 0 in degrees use 0.0 for default value</param>
		///  <param name="sweepdelta">[in] - angle increment of sweep in degrees use 0.0 for default value</param>
		///  <param name="minbsdelta">[in] - min binary search increment angle in degrees use 0.0 for default value</param>
		///   <returns>pixd, or NULL on error</returns>
		public static Pix pixDeskewLocal(
						Pix pixs,
						int nslices,
						int redsweep = 0,
						int redsearch = 0,
						Single sweeprange = 0,
						Single sweepdelta = 0,
						Single minbsdelta = 0)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if ((new List<int> {1}).Contains ((int)pixs.d) == false) { throw new ArgumentException ("1 bpp"); }

			if ((new List<int> {0, 1, 2, 4, 8}).Contains ((int)redsweep) == false) {
				throw new ArgumentException ("sweep reduction factor: 1, 2, 4 or 8 use 0 for default value");
			};

			if ((new List<int> {0, 1, 2, 4, 8}).Contains ((int)redsearch) == false) {
				throw new ArgumentException ("search reduction factor: 1, 2, 4 or 8, and not larger than redsweep use 0 for default value");
			};
			IntPtr _Result = Natives.pixDeskewLocal(pixs.Pointer,   nslices,   redsweep,   redsearch,   sweeprange,   sweepdelta,   minbsdelta);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// baseline.c (388, 1)
		// pixGetLocalSkewTransform(pixs, nslices, redsweep, redsearch, sweeprange, sweepdelta, minbsdelta, pptas, pptad) as int
		// pixGetLocalSkewTransform(PIX *, l_int32, l_int32, l_int32, l_float32, l_float32, l_float32, PTA **, PTA **) as l_ok
		///  <summary>
		/// (1) This generates two pairs of points in the src, each pair
		/// corresponding to a pair of points that would lie along
		/// the same raster line in a transformed (dewarped) image.<para/>
		///
		/// (2) The sets of 4 src and 4 dest points returned by this function
		/// can then be used, in a projective or bilinear transform,
		/// to remove keystoning in the src.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixGetLocalSkewTransform/*"/>
		///  <param name="pixs">[in] - </param>
		///  <param name="nslices">[in] - the number of horizontal overlapping slices must be larger than 1 and not exceed 20 use 0 for default</param>
		///  <param name="redsweep">[in] - sweep reduction factor: 1, 2, 4 or 8 use 0 for default value</param>
		///  <param name="redsearch">[in] - search reduction factor: 1, 2, 4 or 8, and not larger than redsweep use 0 for default value</param>
		///  <param name="sweeprange">[in] - half the full range, assumed about 0 in degrees use 0.0 for default value</param>
		///  <param name="sweepdelta">[in] - angle increment of sweep in degrees use 0.0 for default value</param>
		///  <param name="minbsdelta">[in] - min binary search increment angle in degrees use 0.0 for default value</param>
		///  <param name="pptas">[out] - 4 points in the source</param>
		///  <param name="pptad">[out] - the corresponding 4 pts in the dest</param>
		///   <returns>0 if OK, 1 on error</returns>
		public static int pixGetLocalSkewTransform(
						Pix pixs,
						int nslices,
						int redsweep,
						int redsearch,
						Single sweeprange,
						Single sweepdelta,
						Single minbsdelta,
						out Pta pptas,
						out Pta pptad)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if ((new List<int> {0, 1, 2, 4, 8}).Contains ((int)redsweep) == false) {
				throw new ArgumentException ("sweep reduction factor: 1, 2, 4 or 8 use 0 for default value");
			};

			if ((new List<int> {0, 1, 2, 4, 8}).Contains ((int)redsearch) == false) {
				throw new ArgumentException ("search reduction factor: 1, 2, 4 or 8, and not larger than redsweep use 0 for default value");
			};
			IntPtr pptasPtr = IntPtr.Zero;
			IntPtr pptadPtr = IntPtr.Zero;
			int _Result = Natives.pixGetLocalSkewTransform(pixs.Pointer,   nslices,   redsweep,   redsearch,   sweeprange,   sweepdelta,   minbsdelta, out pptasPtr, out pptadPtr);
			if (pptasPtr == IntPtr.Zero) {pptas = null;} else { pptas = new Pta(pptasPtr); };
			if (pptadPtr == IntPtr.Zero) {pptad = null;} else { pptad = new Pta(pptadPtr); };

			return _Result;
		}

		// baseline.c (506, 1)
		// pixGetLocalSkewAngles(pixs, nslices, redsweep, redsearch, sweeprange, sweepdelta, minbsdelta, pa, pb, debug) as Numa
		// pixGetLocalSkewAngles(PIX *, l_int32, l_int32, l_int32, l_float32, l_float32, l_float32, l_float32 *, l_float32 *, l_int32) as NUMA *
		///  <summary>
		/// (1) The local skew is measured in a set of overlapping strips.
		/// We then do a least square linear fit parameters to get
		/// the slope and intercept parameters a and b in
		/// skew-angle = a  y + b  (degrees)
		/// for the local skew as a function of raster line y.
		/// This is then used to make naskew, which can be interpreted
		/// as the computed skew angle (in degrees) at the left edge
		/// of each raster line.<para/>
		///
		/// (2) naskew can then be used to find the baselines of text, because
		/// each text line has a baseline that should intersect
		/// the left edge of the image with the angle given by this
		/// array, evaluated at the raster line of intersection.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixGetLocalSkewAngles/*"/>
		///  <param name="pixs">[in] - 1 bpp</param>
		///  <param name="nslices">[in] - the number of horizontal overlapping slices must be larger than 1 and not exceed 20 0 for default</param>
		///  <param name="redsweep">[in] - sweep reduction factor: 1, 2, 4 or 8 use 0 for default value</param>
		///  <param name="redsearch">[in] - search reduction factor: 1, 2, 4 or 8, and not larger than redsweep use 0 for default value</param>
		///  <param name="sweeprange">[in] - half the full range, assumed about 0 in degrees use 0.0 for default value</param>
		///  <param name="sweepdelta">[in] - angle increment of sweep in degrees use 0.0 for default value</param>
		///  <param name="minbsdelta">[in] - min binary search increment angle in degrees use 0.0 for default value</param>
		///  <param name="pa">[out][optional] - slope of skew as fctn of y</param>
		///  <param name="pb">[out][optional] - intercept at y=0 of skew as fctn of y</param>
		///  <param name="debug">[in] - 1 for generating plot of skew angle vs. y 0 otherwise</param>
		///   <returns>naskew, or NULL on error</returns>
		public static Numa pixGetLocalSkewAngles(
						Pix pixs,
						int nslices,
						int redsweep,
						int redsearch,
						Single sweeprange,
						Single sweepdelta,
						Single minbsdelta,
						out Single pa,
						out Single pb,
						DebugOnOff debug = DebugOnOff.DebugOn)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if ((new List<int> {1}).Contains ((int)pixs.d) == false) { throw new ArgumentException ("1 bpp"); }

			if ((new List<int> {0, 1, 2, 4, 8}).Contains ((int)redsweep) == false) {
				throw new ArgumentException ("sweep reduction factor: 1, 2, 4 or 8 use 0 for default value");
			};

			if ((new List<int> {0, 1, 2, 4, 8}).Contains ((int)redsearch) == false) {
				throw new ArgumentException ("search reduction factor: 1, 2, 4 or 8, and not larger than redsweep use 0 for default value");
			};
			IntPtr _Result = Natives.pixGetLocalSkewAngles(pixs.Pointer,   nslices,   redsweep,   redsearch,   sweeprange,   sweepdelta,   minbsdelta, out  pa, out  pb,  (int) debug);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Numa(_Result);
		}


	}
}
