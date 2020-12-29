using System;
using System.IO;

using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LeptonicaSharp
{
	public partial class _All
	{

		// correlscore.c (125, 1)
		// pixCorrelationScore(pix1, pix2, area1, area2, delx, dely, maxdiffw, maxdiffh, tab, pscore) as int
		// pixCorrelationScore(PIX *, PIX *, l_int32, l_int32, l_float32, l_float32, l_int32, l_int32, l_int32 *, l_float32 *) as l_ok
		///  <summary>
		/// We check first that the two pix are roughly the same size.
		/// For jbclass (jbig2) applications at roughly 300 ppi, maxdiffw and
		/// maxdiffh should be at least 2.
		/// Only if they meet that criterion do we compare the bitmaps.
		/// The centroid difference is used to align the two images to the
		/// nearest integer for the correlation.
		/// The correlation score is the ratio of the square of the number of
		/// pixels in the AND of the two bitmaps to the product of the number
		/// of ON pixels in each.  Denote the number of ON pixels in pix1
		/// by |1|, the number in pix2 by |2|, and the number in the AND
		/// of pix1 and pix2 by |1 [and] 2|.  The correlation score is then
		/// (|1 [and] 2|)2 / (|1||2|).
		/// This score is compared with an input threshold, which can
		/// be modified depending on the weight of the template.
		/// The modified threshold is
		/// thresh + (1.0 - thresh)  weight  R
		/// where
		/// weight is a fixed input factor between 0.0 and 1.0
		/// R = |2| / area(2)
		/// and area(2) is the total number of pixels in 2 (i.e., width x height).
		/// To understand why a weight factor is useful, consider what happens
		/// with thick, sans-serif characters that look similar and have a value
		/// of R near 1.  Different characters can have a high correlation value,
		/// and the classifier will make incorrect substitutions.  The weight
		/// factor raises the threshold for these characters.
		/// Yet another approach to reduce such substitutions is to run the classifier
		/// in a non-greedy way, matching to the template with the highest
		/// score, not the first template with a score satisfying the matching
		/// constraint.  However, this is not particularly effective.
		/// The implementation here gives the same result as in
		/// pixCorrelationScoreSimple(), where a temporary Pix is made to hold
		/// the AND and implementation uses rasterop:
		/// pixt = pixCreateTemplate(pix1)
		/// pixRasterop(pixt, idelx, idely, wt, ht, PIX_SRC, pix2, 0, 0)
		/// pixRasterop(pixt, 0, 0, wi, hi, PIX_SRC [and] PIX_DST, pix1, 0, 0)
		/// pixCountPixels(pixt, [and]count, tab)
		/// pixDestroy([and]pixt)
		/// However, here it is done in a streaming fashion, counting as it goes,
		/// and touching memory exactly once, giving a 3-4x speedup over the
		/// simple implementation.  This very fast correlation matcher was
		/// contributed by William Rucklidge.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixCorrelationScore/*"/>
		///  <param name="pix1">[in] - test pix, 1 bpp</param>
		///  <param name="pix2">[in] - exemplar pix, 1 bpp</param>
		///  <param name="area1">[in] - number of on pixels in pix1</param>
		///  <param name="area2">[in] - number of on pixels in pix2</param>
		///  <param name="delx">[in] - x comp of centroid difference</param>
		///  <param name="dely">[in] - y comp of centroid difference</param>
		///  <param name="maxdiffw">[in] - max width difference of pix1 and pix2</param>
		///  <param name="maxdiffh">[in] - max height difference of pix1 and pix2</param>
		///  <param name="tab">[in] - sum tab for byte</param>
		///  <param name="pscore">[out] - correlation score</param>
		///   <returns>0 if OK, 1 on error</returns>
		public static int pixCorrelationScore(
						Pix pix1,
						Pix pix2,
						int area1,
						int area2,
						Single delx,
						Single dely,
						int maxdiffw,
						int maxdiffh,
						int[] tab,
						out Single pscore)
		{
			if (pix1 == null) {throw new ArgumentNullException  ("pix1 cannot be Nothing");}

			if (pix2 == null) {throw new ArgumentNullException  ("pix2 cannot be Nothing");}

			if (tab == null) {throw new ArgumentNullException  ("tab cannot be Nothing");}

			int _Result = Natives.pixCorrelationScore(pix1.Pointer, pix2.Pointer,   area1,   area2,   delx,   dely,   maxdiffw,   maxdiffh,   tab, out  pscore);
			return _Result;
		}

		// correlscore.c (423, 1)
		// pixCorrelationScoreThresholded(pix1, pix2, area1, area2, delx, dely, maxdiffw, maxdiffh, tab, downcount, score_threshold) as int
		// pixCorrelationScoreThresholded(PIX *, PIX *, l_int32, l_int32, l_float32, l_float32, l_int32, l_int32, l_int32 *, l_int32 *, l_float32) as l_int32
		///  <summary>
		/// We check first that the two pix are roughly the same size.
		/// Only if they meet that criterion do we compare the bitmaps.
		/// The centroid difference is used to align the two images to the
		/// nearest integer for the correlation.
		/// The correlation score is the ratio of the square of the number of
		/// pixels in the AND of the two bitmaps to the product of the number
		/// of ON pixels in each.  Denote the number of ON pixels in pix1
		/// by |1|, the number in pix2 by |2|, and the number in the AND
		/// of pix1 and pix2 by |1 [and] 2|.  The correlation score is then
		/// (|1 [and] 2|)2 / (|1||2|).
		/// This score is compared with an input threshold, which can
		/// be modified depending on the weight of the template.
		/// The modified threshold is
		/// thresh + (1.0 - thresh)  weight  R
		/// where
		/// weight is a fixed input factor between 0.0 and 1.0
		/// R = |2| / area(2)
		/// and area(2) is the total number of pixels in 2 (i.e., width x height).
		/// To understand why a weight factor is useful, consider what happens
		/// with thick, sans-serif characters that look similar and have a value
		/// of R near 1.  Different characters can have a high correlation value,
		/// and the classifier will make incorrect substitutions.  The weight
		/// factor raises the threshold for these characters.
		/// Yet another approach to reduce such substitutions is to run the classifier
		/// in a non-greedy way, matching to the template with the highest
		/// score, not the first template with a score satisfying the matching
		/// constraint.  However, this is not particularly effective.
		/// This very fast correlation matcher was contributed by William Rucklidge.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixCorrelationScoreThresholded/*"/>
		///  <param name="pix1">[in] - test pix, 1 bpp</param>
		///  <param name="pix2">[in] - exemplar pix, 1 bpp</param>
		///  <param name="area1">[in] - number of on pixels in pix1</param>
		///  <param name="area2">[in] - number of on pixels in pix2</param>
		///  <param name="delx">[in] - x comp of centroid difference</param>
		///  <param name="dely">[in] - y comp of centroid difference</param>
		///  <param name="maxdiffw">[in] - max width difference of pix1 and pix2</param>
		///  <param name="maxdiffh">[in] - max height difference of pix1 and pix2</param>
		///  <param name="tab">[in] - sum tab for byte</param>
		///  <param name="downcount">[in] - count of 1 pixels below each row of pix1</param>
		///  <param name="score_threshold">[in] - </param>
		///   <returns>whether the correlation score is greater or equal score_threshold</returns>
		public static int pixCorrelationScoreThresholded(
						Pix pix1,
						Pix pix2,
						int area1,
						int area2,
						Single delx,
						Single dely,
						int maxdiffw,
						int maxdiffh,
						int[] tab,
						int[] downcount,
						Single score_threshold)
		{
			if (pix1 == null) {throw new ArgumentNullException  ("pix1 cannot be Nothing");}

			if (pix2 == null) {throw new ArgumentNullException  ("pix2 cannot be Nothing");}

			if (tab == null) {throw new ArgumentNullException  ("tab cannot be Nothing");}

			if (downcount == null) {throw new ArgumentNullException  ("downcount cannot be Nothing");}

			int _Result = Natives.pixCorrelationScoreThresholded(pix1.Pointer, pix2.Pointer,   area1,   area2,   delx,   dely,   maxdiffw,   maxdiffh,   tab,   downcount,   score_threshold);
			return _Result;
		}

		// correlscore.c (732, 1)
		// pixCorrelationScoreSimple(pix1, pix2, area1, area2, delx, dely, maxdiffw, maxdiffh, tab, pscore) as int
		// pixCorrelationScoreSimple(PIX *, PIX *, l_int32, l_int32, l_float32, l_float32, l_int32, l_int32, l_int32 *, l_float32 *) as l_ok
		///  <summary>
		/// (1) This calculates exactly the same value as pixCorrelationScore().
		/// It is 2-3x slower, but much simpler to understand.<para/>
		///
		/// (2) The returned correlation score is 0.0 if the width or height
		/// exceed %maxdiffw or %maxdiffh.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixCorrelationScoreSimple/*"/>
		///  <param name="pix1">[in] - test pix, 1 bpp</param>
		///  <param name="pix2">[in] - exemplar pix, 1 bpp</param>
		///  <param name="area1">[in] - number of on pixels in pix1</param>
		///  <param name="area2">[in] - number of on pixels in pix2</param>
		///  <param name="delx">[in] - x comp of centroid difference</param>
		///  <param name="dely">[in] - y comp of centroid difference</param>
		///  <param name="maxdiffw">[in] - max width difference of pix1 and pix2</param>
		///  <param name="maxdiffh">[in] - max height difference of pix1 and pix2</param>
		///  <param name="tab">[in] - sum tab for byte</param>
		///  <param name="pscore">[out] - correlation score, in range [0.0 ... 1.0]</param>
		///   <returns>0 if OK, 1 on error</returns>
		public static int pixCorrelationScoreSimple(
						Pix pix1,
						Pix pix2,
						int area1,
						int area2,
						Single delx,
						Single dely,
						int maxdiffw,
						int maxdiffh,
						int[] tab,
						out Single pscore)
		{
			if (pix1 == null) {throw new ArgumentNullException  ("pix1 cannot be Nothing");}

			if (pix2 == null) {throw new ArgumentNullException  ("pix2 cannot be Nothing");}

			if (tab == null) {throw new ArgumentNullException  ("tab cannot be Nothing");}

			int _Result = Natives.pixCorrelationScoreSimple(pix1.Pointer, pix2.Pointer,   area1,   area2,   delx,   dely,   maxdiffw,   maxdiffh,   tab, out  pscore);
			return _Result;
		}

		// correlscore.c (835, 1)
		// pixCorrelationScoreShifted(pix1, pix2, area1, area2, delx, dely, tab, pscore) as int
		// pixCorrelationScoreShifted(PIX *, PIX *, l_int32, l_int32, l_int32, l_int32, l_int32 *, l_float32 *) as l_ok
		///  <summary>
		/// (1) This finds the correlation between two 1 bpp images,
		/// when pix2 is shifted by (delx, dely) with respect
		/// to each other.<para/>
		///
		/// (2) This is implemented by starting with a copy of pix1 and
		/// ANDing its pixels with those of a shifted pix2.<para/>
		///
		/// (3) Get the pixel counts for area1 and area2 using piCountPixels().<para/>
		///
		/// (4) A good estimate for a shift that would maximize the correlation
		/// is to align the centroids (cx1, cy1 cx2, cy2), giving the
		/// relative translations etransx and etransy:
		/// etransx = cx1 - cx2
		/// etransy = cy1 - cy2
		/// Typically delx is chosen to be near etransx ditto for dely.
		/// This function is used in pixBestCorrelation(), where the
		/// translations delx and dely are varied to find the best alignment.<para/>
		///
		/// (5) We do not check the sizes of pix1 and pix2, because they should
		/// be comparable.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixCorrelationScoreShifted/*"/>
		///  <param name="pix1">[in] - 1 bpp</param>
		///  <param name="pix2">[in] - 1 bpp</param>
		///  <param name="area1">[in] - number of on pixels in pix1</param>
		///  <param name="area2">[in] - number of on pixels in pix2</param>
		///  <param name="delx">[in] - x translation of pix2 relative to pix1</param>
		///  <param name="dely">[in] - y translation of pix2 relative to pix1</param>
		///  <param name="tab">[in] - sum tab for byte</param>
		///  <param name="pscore">[out] - correlation score</param>
		///   <returns>0 if OK, 1 on error</returns>
		public static int pixCorrelationScoreShifted(
						Pix pix1,
						Pix pix2,
						int area1,
						int area2,
						int delx,
						int dely,
						int[] tab,
						out Single pscore)
		{
			if (pix1 == null) {throw new ArgumentNullException  ("pix1 cannot be Nothing");}

			if (pix2 == null) {throw new ArgumentNullException  ("pix2 cannot be Nothing");}

			if (tab == null) {throw new ArgumentNullException  ("tab cannot be Nothing");}

			if ((new List<int> {1}).Contains ((int)pix1.d) == false) { throw new ArgumentException ("1 bpp"); }

			if ((new List<int> {1}).Contains ((int)pix2.d) == false) { throw new ArgumentException ("1 bpp"); }
			int _Result = Natives.pixCorrelationScoreShifted(pix1.Pointer, pix2.Pointer,   area1,   area2,   delx,   dely,   tab, out  pscore);
			return _Result;
		}


	}
}
