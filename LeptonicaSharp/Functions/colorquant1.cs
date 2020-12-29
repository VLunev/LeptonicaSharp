using System;
using System.IO;

using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LeptonicaSharp
{
	public partial class _All
	{

		// colorquant1.c (535, 1)
		// pixOctreeColorQuant(pixs, colors, ditherflag) as Pix
		// pixOctreeColorQuant(PIX *, l_int32, l_int32) as PIX *
		///  <summary>
		/// I found one description in the literature of octree color
		/// quantization, using progressive truncation of the octree,
		/// by M. Gervautz and W. Purgathofer in Graphics Gems, pp.
		/// 287-293, ed. A. Glassner, Academic Press, 1990.
		/// Rather than setting up a fixed partitioning of the color
		/// space ab initio, as we do here, they allow the octree to be
		/// progressively truncated as new pixels are added.  They
		/// need to set up some data structures that are traversed
		/// with the addition of each 24 bit pixel, in order to decide
		/// either 1) in which cluster (sub-branch of the octree to put
		/// the pixel, or 2 whether to truncate the octree further
		/// to place the pixel in an existing cluster, or 3 which
		/// two existing clusters should be merged so that the pixel
		/// can be left to start a truncated leaf of the octree.  Such dynamic
		/// truncation is considerably more complicated, and Gervautz et
		/// al. did not explain how they did it in anywhere near the
		/// detail required to check their implementation.
		/// The simple method in pixFixedOctcubeQuant256 is very
		/// fast, and with dithering the results are good, but you
		/// can do better if the color clusters are selected adaptively
		/// from the image.  We want a method that makes much better
		/// use of color samples in regions of color space with high
		/// pixel density, while also fairly representing small numbers
		/// of color pixels in low density regions.  Such adaptation
		/// requires two passes through the image: the first for generating
		/// the pruned tree of color cubes and the second for computing the index
		/// into the color table for each pixel.
		/// A relatively simple adaptive method is pixOctreeQuantByPopulation.
		/// That function first determines if the image has very few colors,
		/// and, if so, quantizes to those colors.  If there are more than
		/// 256 colors, it generates a histogram of octcube leaf occupancy
		/// at level 4, chooses the 192 most populated such leaves as
		/// the first 192 colors, and sets the remaining 64 colors to the
		/// residual average pixel values in each of the 64 level 2 octcubes.
		/// This is a bit faster than pixOctreeColorQuant, and does very
		/// well without dithering, but for most images with dithering it
		/// is clearly inferior.
		/// We now describe pixOctreeColorQuant.  The first pass is done
		/// on a subsampled image, because we do not need to use all the
		/// pixels in the image to generate the tree.  Subsampling
		/// down to 0.25 1/16 of the pixels makes the program run
		/// about 1.3 times faster.
		/// Instead of dividing the color space into 256 equal-sized
		/// regions, we initially divide it into 2^12 or 2^15 or 2^18
		/// equal-sized octcubes.  Suppose we choose to use 2^18 octcubes.
		/// This gives us 6 octree levels.  We then prune back,
		/// starting from level 6.  For every cube at level 6, there
		/// are 8 cubes at level 5.  Call the operation of putting a
		/// cube aside as a color table entry CTE a "saving."
		/// We use a in general level-dependent threshold, and save
		/// those level 6 cubes that are above threshold.
		/// The rest are combined into the containing level 5 cube.
		/// If between 1 and 7 level 6 cubes within a level 5
		/// cube have been saved by thresholding, then the remaining
		/// level 6 cubes in that level 5 cube are automatically
		/// saved as well, without applying a threshold.  This greatly
		/// simplifies both the description of the CTEs and the later
		/// classification of each pixel as belonging to a CTE.
		/// This procedure is iterated through every cube, starting at
		/// level 5, and then 4, 3, and 2, successively.  The result is that
		/// each CTE contains the entirety of a set of from 1 to 7 cubes
		/// from a given level that all belong to a single cube at the
		/// level above. We classify the CTEs in terms of the
		/// condition in which they are made as either being "threshold"
		/// or "residual."  They are "threshold" CTEs if no subcubes
		/// are CTEs that is, they contain every pixel within the cube
		/// and the number of pixels exceeds the threshold for making
		/// a CTE.  They are "residual" CTEs if at least one but not more
		/// than 7 of the subcubes have already been determined to be CTEs
		/// this happens automatically -- no threshold is applied.
		/// If all 8 subcubes are determined to be CTEs, the cube is
		/// marked as having all pixels accounted for 'bleaf' = 1 but
		/// is not saved as a CTE.
		/// We stop the pruning at level 2, at which there are 64
		/// sub-cubes.  Any pixels not already claimed in a CTE are
		/// put in these cubes.
		/// As the cubes are saved as color samples in the color table,
		/// the number of remaining pixels P and the number of
		/// remaining colors in the color table N are recomputed,
		/// along with the average number of pixels P/N ppc to go in
		/// each of the remaining colors.  This running average number is
		/// used to set the threshold at the current level.
		/// Because we are going to very small cubes at levels 6 or 5,
		/// and will dither the colors for errors, it is not necessary
		/// to compute the color center of each cluster we can simply
		/// use the center of the cube.  This gives us a minimax error
		/// condition: the maximum error is half the width of the
		/// level 2 cubes -- 32 color values out of 256 -- for each color
		/// sample.  In practice, most of the pixels will be very much
		/// closer to the center of their cells.  And with dithering,
		/// the average pixel color in a small region will be closer still.
		/// Thus with the octree quantizer, we are able to capture
		/// regions of high color pdf probability density function in small
		/// but accurate CTEs, and to have only a small number of pixels
		/// that end up a significant distance with a guaranteed maximum
		/// from their true color.
		/// How should the threshold factor vary?  Threshold factors
		/// are required for levels 2, 3, 4 and 5 in the pruning stage.
		/// The threshold for level 5 is actually applied to cubes at
		/// level 6, etc.  From various experiments, it appears that
		/// the results do not vary appreciably for threshold values near 1.0.
		/// If you want more colors in smaller cubes, the threshold
		/// factors can be set lower than 1.0 for cubes at levels 4 and 5.
		/// However, if the factor is set much lower than 1.0 for
		/// levels 2 and 3, we can easily run out of colors.
		/// We put aside 64 colors in the calculation of the threshold
		/// values, because we must have 64 color centers at level 2,
		/// that will have very few pixels in most of them.
		/// If we reduce the factor for level 5 to 0.4, this will
		/// generate many level 6 CTEs, and consequently
		/// many residual cells will be formed up from those leaves,
		/// resulting in the possibility of running out of colors.
		/// Remember, the residual CTEs are mandatory, and are formed
		/// without using the threshold, regardless of the number of
		/// pixels that are absorbed.
		/// The implementation logically has four parts:
		/// 1 accumulation into small, fixed cells
		/// 2 pruning back into selected CTE cubes
		/// 3 organizing the CTEs for fast search to find
		/// the CTE to which any image pixel belongs
		/// 4 doing a second scan to code the image pixels by CTE
		/// Step 1 is straightforward we use 2^15 cells.
		/// We've already discussed how the pruning step 2 will be performed.
		/// Steps 3) and (4 are related, in that the organization
		/// used by step 3 determines how the search actually
		/// takes place for each pixel in step 4.
		/// There are many ways to do step 3.  Let's explore a few.
		/// a The simplest is to order the cubes from highest occupancy
		/// to lowest, and traverse the list looking for the deepest
		/// match.  To make this more efficient, so that we know when
		/// to stop looking, any cube that has separate CTE subcubes
		/// would be marked as such, so that we know when we hit a
		/// true leaf.
		/// b Alternatively, we can order the cubes by highest
		/// occupancy separately each level, and work upward,
		/// starting at level 5, so that when we find a match we
		/// know that it will be correct.
		/// c Another approach would be to order the cubes by
		/// "address" and use a hash table to find the cube
		/// corresponding to a pixel color.  I don't know how to
		/// do this with a variable length address, as each CTE
		/// will have 3n bits, where n is the level.
		/// d Another approach entirely is to put the CTE cubes into
		/// a tree, in such a way that starting from the root, and
		/// using 3 bits of address at a time, the correct branch of
		/// each octree can be taken until a leaf is found.  Because
		/// a given cube can be both a leaf and also have branches
		/// going to sub-cubes, the search stops only when no
		/// marked subcubes have addresses that match the given pixel.
		/// In the tree method, we can start with a dense infrastructure,
		/// and place the leaves corresponding to the N colors
		/// in the tree, or we can grow from the root only those
		/// branches that end directly on leaves.
		/// What we do here is to take approach d, and implement the tree
		/// "virtually", as a set of arrays, one array for each level
		/// of the tree. Initially we start at level 5, an array with
		/// 2^15 cubes, each with 8 subcubes.  We then build nodes at
		/// levels closer to the root at level 4 there are 2^12 nodes
		/// each with 8 subcubes etc.  Using these arrays has
		/// several advantages:
		/// ~  We don't need to keep track of links between cubes
		/// and subcubes, because we can use the canonical
		/// addressing on the cell arrays directly to determine
		/// which nodes are parent cubes and which are sub-cubes.
		/// ~  We can prune directly on this tree
		/// ~  We can navigate the pruned tree quickly to classify
		/// each pixel in the image.
		/// Canonical addressing guarantees that the i-th node at level k
		/// has 8 subnodes given by the 8i ... 8i+7 nodes at level k+1.
		/// The pruning step works as follows.  We go from the lowest
		/// level up.  At each level, the threshold is found from the
		/// product of a factor near 1.0 and the ratio of unmarked pixels
		/// to remaining colors minus the 64.  We march through
		/// the space, sequentially considering a cube and its 8 subcubes.
		/// We first check those subcubes that are not already
		/// marked as CTE to see if any are above threshold, and if so,
		/// generate a CTE and mark them as such.
		/// We then determine if any of the subcubes have been marked.
		/// If so, and there are subcubes that are not marked,
		/// we generate a CTE for the cube from the remaining unmarked
		/// subcubes this is mandatory and does not depend on how many
		/// pixels are in the set of subcubes.  If none of the subcubes
		/// are marked, we aggregate their pixels into the cube
		/// containing them, but do not mark it as a CTE that
		/// will be determined when iterating through the next level up.
		/// When all the pixels in a cube are accounted for in one or more
		/// colors, we set the boolean 'bleaf' to true.  This is the
		/// flag used to mark the cubes in the pruning step.  If a cube
		/// is marked, and all 8 subcubes are marked, then it is not
		/// itself given a CTE because all pixels have already been
		/// accounted for.
		/// Note that the pruning of the tree and labelling of the CTEs
		/// step 2 accomplishes step 3 implicitly, because the marked
		/// and pruned tree is ready for use in labelling each pixel
		/// in step 4.  We now, for every pixel in the image, traverse
		/// the tree from the root, looking for the lowest cube that is a leaf.
		/// At each level we have a cube and subcube.  If we reach a subcube
		/// leaf that is marked 0, we know that the color is stored in the
		/// cube above, and we've found the CTE.  Otherwise, the subcube
		/// leaf is marked 1.  If we're at the last level, we've reached
		/// the final leaf and must use it.  Otherwise, continue the
		/// process at the next level down.
		/// For robustness, efficiency and high quality output, we do the following:<para/>
		///
		/// (1) Measure the color content of the image.  If there is very little
		/// color, quantize in grayscale.<para/>
		///
		/// (2) For efficiency, build the octree with a subsampled image if the
		/// image is larger than some threshold size.<para/>
		///
		/// (3) Reserve an extra set of colors to prevent running out of colors
		/// when pruning the octree specifically, during the assignment
		/// of those level 2 cells out of the 64 that have unassigned
		/// pixels.  The problem of running out is more likely to happen
		/// with small images, because the estimation we use for the
		/// number of pixels available is not accurate.<para/>
		///
		/// (4) In the unlikely event that we run out of colors, the dithered
		/// image can be very poor.  As this would only happen with very
		/// small images, and dithering is not particularly noticeable with
		/// such images, turn it off.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixOctreeColorQuant/*"/>
		///  <param name="pixs">[in] - 32 bpp 24-bit color</param>
		///  <param name="colors">[in] - in colormap some number in range [128 ... 256] the actual number of colors used will be smaller</param>
		///  <param name="ditherflag">[in] - 1 to dither, 0 otherwise</param>
		///   <returns>pixd 8 bpp with colormap, or NULL on error</returns>
		public static Pix pixOctreeColorQuant(
						Pix pixs,
						int colors,
						int ditherflag)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			IntPtr _Result = Natives.pixOctreeColorQuant(pixs.Pointer,   colors,   ditherflag);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// colorquant1.c (601, 1)
		// pixOctreeColorQuantGeneral(pixs, colors, ditherflag, validthresh, colorthresh) as Pix
		// pixOctreeColorQuantGeneral(PIX *, l_int32, l_int32, l_float32, l_float32) as PIX *
		///  <summary>
		/// (1) The parameters %validthresh and %colorthresh are used to
		/// determine if color quantization should be used on an image,
		/// or whether, instead, it should be quantized in grayscale.
		/// If the image has very few non-white and non-black pixels, or
		/// if those pixels that are non-white and non-black are all
		/// very close to either white or black, it is usually better
		/// to treat the color as accidental and to quantize the image
		/// to gray only.  These parameters are useful if you know
		/// something a priori about the image.  Perhaps you know that
		/// there is only a very small fraction of color pixels, but they're
		/// important to preserve then you want to use a smaller value for
		/// these parameters.  To disable conversion to gray and force
		/// color quantization, use %validthresh = 0.0 and %colorthresh = 0.0.<para/>
		///
		/// (2) See pixOctreeColorQuant() for algorithmic and implementation
		/// details.  This function has a more general interface.<para/>
		///
		/// (3) See pixColorFraction() for computing the fraction of pixels
		/// that are neither white nor black, and the fraction of those
		/// pixels that have little color.  From the documentation there:
		/// If pixfract is very small, there are few pixels that are
		/// neither black nor white.  If colorfract is very small,
		/// the pixels that are neither black nor white have very
		/// little color content.  The product 'pixfract  colorfract'
		/// gives the fraction of pixels with significant color content.
		/// We test against the product %validthresh  %colorthresh
		/// to find color in images that have either very few
		/// intermediate gray pixels or that have many such gray pixels.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixOctreeColorQuantGeneral/*"/>
		///  <param name="pixs">[in] - 32 bpp 24-bit color</param>
		///  <param name="colors">[in] - in colormap some number in range [128 ... 240] the actual number of colors used will be smaller</param>
		///  <param name="ditherflag">[in] - 1 to dither, 0 otherwise</param>
		///  <param name="validthresh">[in] - minimum fraction of pixels neither near white nor black, required for color quantization typically ~0.01, but smaller for images that have color but are nearly all white</param>
		///  <param name="colorthresh">[in] - minimum fraction of pixels with color that are not near white or black, that are required for color quantization typ. ~0.01, but smaller for images that have color along with a significant fraction of gray</param>
		///   <returns>pixd 8 bit with colormap, or NULL on error</returns>
		public static Pix pixOctreeColorQuantGeneral(
						Pix pixs,
						int colors,
						int ditherflag,
						Single validthresh,
						Single colorthresh)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			IntPtr _Result = Natives.pixOctreeColorQuantGeneral(pixs.Pointer,   colors,   ditherflag,   validthresh,   colorthresh);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// colorquant1.c (1361, 1)
		// makeRGBToIndexTables(prtab, pgtab, pbtab, cqlevels) as int
		// makeRGBToIndexTables(l_uint32 **, l_uint32 **, l_uint32 **, l_int32) as l_ok
		///  <summary>
		/// Set up tables.  e.g., for cqlevels = 5, we need an integer 0  is smaller i  is smaller 2^15:
		/// rtab = 0  i7  0 0  i6  0 0  i5  0 0 i4  0 0 i3  0 0
		/// gtab = 0  0 i7  0 0  i6  0 0  i5  0 0 i4  0 0 i3  0
		/// btab = 0  0 0 i7  0  0 i6  0  0 i5  0 0 i4  0 0 i3
		/// The tables are then used to map from rbg to index as follows:
		/// index = 0  r7  g7  b7  r6  g6  b6  r5  g5  b5  r4  g4  b4  r3  g3  b3
		/// e.g., for cqlevels = 4, we map to
		/// index = 0  0 0 0 r7  g7  b7  r6  g6  b6  r5  g5  b5  r4  g4  b4
		/// This may look a bit strange.  The notation 'r7' means the MSBit of
		/// the r value which has 8 bits, going down from r7 to r0.
		/// Keep in mind that r7 is actually the r component bit for level 1 of
		/// the octtree.  Level 1 is composed of 8 octcubes, represented by
		/// the bits r7 g7 b7, which divide the entire color space into
		/// 8 cubes.  At level 2, each of these 8 octcubes is further divided into
		/// 8 cubes, each labeled by the second most significant bits r6 g6 b6
		/// of the rgb color.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/makeRGBToIndexTables/*"/>
		///  <param name="prtab">[out] - tables</param>
		///  <param name="pgtab">[out] - tables</param>
		///  <param name="pbtab">[out] - tables</param>
		///  <param name="cqlevels">[in] - can be 1, 2, 3, 4, 5 or 6</param>
		///   <returns>0 if OK 1 on error</returns>
		public static int makeRGBToIndexTables(
						out Byte[] prtab,
						out Byte[] pgtab,
						out Byte[] pbtab,
						int cqlevels)
		{
			IntPtr prtabPtr = IntPtr.Zero;
			IntPtr pgtabPtr = IntPtr.Zero;
			IntPtr pbtabPtr = IntPtr.Zero;
			int _Result = Natives.makeRGBToIndexTables(out  prtabPtr, out  pgtabPtr, out  pbtabPtr,   cqlevels);
			Byte[] prtabGen = new Byte[1];

			if (prtabPtr != IntPtr.Zero) {
				Marshal.Copy(prtabPtr, prtabGen, 0, prtabGen.Length);
			}

			prtab = prtabGen;
			Byte[] pgtabGen = new Byte[1];

			if (pgtabPtr != IntPtr.Zero) {
				Marshal.Copy(pgtabPtr, pgtabGen, 0, pgtabGen.Length);
			}

			pgtab = pgtabGen;
			Byte[] pbtabGen = new Byte[1];

			if (pbtabPtr != IntPtr.Zero) {
				Marshal.Copy(pbtabPtr, pbtabGen, 0, pbtabGen.Length);
			}

			pbtab = pbtabGen;
			return _Result;
		}

		// colorquant1.c (1470, 1)
		// getOctcubeIndexFromRGB(rval, gval, bval, rtab, gtab, btab, pindex) as Object
		// getOctcubeIndexFromRGB(l_int32, l_int32, l_int32, l_uint32 *, l_uint32 *, l_uint32 *, l_uint32 *) as void
		///  <summary>
		/// No error checking!
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/getOctcubeIndexFromRGB/*"/>
		///  <param name="rval">[in] - </param>
		///  <param name="gval">[in] - </param>
		///  <param name="bval">[in] - </param>
		///  <param name="rtab">[in] - generated with makeRGBToIndexTables()</param>
		///  <param name="gtab">[in] - generated with makeRGBToIndexTables()</param>
		///  <param name="btab">[in] - generated with makeRGBToIndexTables()</param>
		///  <param name="pindex">[out] - found index</param>
		public static void getOctcubeIndexFromRGB(
						int rval,
						int gval,
						int bval,
						Byte[] rtab,
						Byte[] gtab,
						Byte[] btab,
						out uint pindex)
		{
			if (rtab == null) {throw new ArgumentNullException  ("rtab cannot be Nothing");}

			if (gtab == null) {throw new ArgumentNullException  ("gtab cannot be Nothing");}

			if (btab == null) {throw new ArgumentNullException  ("btab cannot be Nothing");}

			IntPtr rtabPtr = 	Marshal.AllocHGlobal(rtab.Length);
			Marshal.Copy(rtab, 0, rtabPtr, rtab.Length);
			IntPtr gtabPtr = 	Marshal.AllocHGlobal(gtab.Length);
			Marshal.Copy(gtab, 0, gtabPtr, gtab.Length);
			IntPtr btabPtr = 	Marshal.AllocHGlobal(btab.Length);
			Marshal.Copy(btab, 0, btabPtr, btab.Length);
			Natives.getOctcubeIndexFromRGB(  rval,   gval,   bval,   rtabPtr,   gtabPtr,   btabPtr, out  pindex);
			Marshal.FreeHGlobal(rtabPtr);
			Marshal.FreeHGlobal(gtabPtr);
			Marshal.FreeHGlobal(btabPtr);
		}

		// colorquant1.c (1701, 1)
		// pixOctreeQuantByPopulation(pixs, level, ditherflag) as Pix
		// pixOctreeQuantByPopulation(PIX *, l_int32, l_int32) as PIX *
		///  <summary>
		/// (1) This color quantization method works very well without
		/// dithering, using octcubes at two different levels:
		/// (a) the input %level, which is either 3 or 4
		/// (b) level 2 (64 octcubes to cover the entire color space)<para/>
		///
		/// (2) For best results, using %level = 4 is recommended.
		/// Why do we provide an option for using level 3?  Because
		/// there are 512 octcubes at level 3, and for many images
		/// not more than 256 are filled.  As a result, on some images
		/// a very accurate quantized representation is possible using
		/// %level = 3.<para/>
		///
		/// (3) This first breaks up the color space into octcubes at the
		/// input %level, and computes, for each octcube, the average
		/// value of the pixels that are in it.<para/>
		///
		/// (4) Then there are two possible situations:
		/// (a) If there are not more than 256 populated octcubes,
		/// it returns a cmapped pix with those values assigned.
		/// (b) Otherwise, it selects 192 octcubes containing the largest
		/// number of pixels and quantizes pixels within those octcubes
		/// to their average.  Then, to handle the residual pixels
		/// that are not in those 192 octcubes, it generates a
		/// level 2 octree consisting of 64 octcubes, and within
		/// each octcube it quantizes the residual pixels to their
		/// average within each of those level 2 octcubes.<para/>
		///
		/// (5) Unpopulated level 2 octcubes are represented in the colormap
		/// by their centers.  This, of course, has no effect unless
		/// dithering is used for the output image.<para/>
		///
		/// (6) The depth of pixd is the minimum required to support the
		/// number of colors found at %level namely, 2, 4 or 8.<para/>
		///
		/// (7) This function works particularly well on images such as maps,
		/// where there are a relatively small number of well-populated
		/// colors, but due to antialiasing and compression artifacts
		/// there may be a large number of different colors.  This will
		/// pull out and represent accurately the highly populated colors,
		/// while still making a reasonable approximation for the others.<para/>
		///
		/// (8) The highest level of octcubes allowed is 4.  Use of higher
		/// levels typically results in having a small fraction of
		/// pixels in the most populated 192 octcubes.  As a result,
		/// most of the pixels are represented at level 2, which is
		/// not sufficiently accurate.<para/>
		///
		/// (9) Dithering shows artifacts on some images.  If you plan to
		/// dither, pixOctreeColorQuant() and pixFixedOctcubeQuant256()
		/// usually give better results.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixOctreeQuantByPopulation/*"/>
		///  <param name="pixs">[in] - 32 bpp rgb</param>
		///  <param name="level">[in] - significant bits for each of RGB valid for {3,4}, Use 0 for default (level 4 recommended</param>
		///  <param name="ditherflag">[in] - 1 to dither, 0 otherwise</param>
		///   <returns>pixd quantized to octcubes or NULL on error</returns>
		public static Pix pixOctreeQuantByPopulation(
						Pix pixs,
						int level,
						int ditherflag)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if ((new List<int> {32}).Contains ((int)pixs.d) == false) { throw new ArgumentException ("32 bpp rgb"); }
			IntPtr _Result = Natives.pixOctreeQuantByPopulation(pixs.Pointer,   level,   ditherflag);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// colorquant1.c (2263, 1)
		// pixOctreeQuantNumColors(pixs, maxcolors, subsample) as Pix
		// pixOctreeQuantNumColors(PIX *, l_int32, l_int32) as PIX *
		///  <summary>
		/// pixOctreeColorQuant is very flexible in terms of the relative
		/// depth of different cubes of the octree. By contrast, this function,
		/// pixOctreeQuantNumColors is also adaptive, but it supports octcube
		/// leaves at only two depths: a smaller depth that guarantees
		/// full coverage of the color space and octcubes at one level
		/// deeper for more accurate colors.  Its main virutes are simplicity
		/// and speed, which are both derived from the natural indexing of
		/// the octcubes from the RGB values.
		/// Before describing pixOctreeQuantNumColors, consider an even simpler
		/// approach for 4 bpp with either 8 or 16 colors.  With 8 colors,
		/// you simply go to level 1 octcubes and use the average color
		/// found in each cube.  For 16 colors, you find which of the three
		/// colors has the largest variance at the second level, and use two
		/// indices for that color.  The result is quite poor, because 1 some
		/// of the cubes are nearly empty and 2 you don't get much color
		/// differentiation for the extra 8 colors.  Trust me, this method may
		/// be simple, but it isn't worth anything.
		/// In pixOctreeQuantNumColors, we generate colormapped images at
		/// either 4 bpp or 8 bpp.  For 4 bpp, we have a minimum of 8 colors
		/// for the level 1 octcubes, plus up to 8 additional colors that
		/// are determined from the level 2 popularity.  If the number of colors
		/// is between 8 and 16, the output is a 4 bpp image.  If the number of
		/// colors is greater than 16, the output is a 8 bpp image.
		/// We use a priority queue, implemented with a heap, to select the
		/// requisite number of most populated octcubes at the deepest level
		/// level 2 for 64 or fewer colors level 3 for more than 64 colors.
		/// These are combined with one color for each octcube one level above,
		/// which is used to span the color space of octcubes that were not
		/// included at the deeper level.
		/// If the deepest level is 2, we combine the popular level 2 octcubes
		/// out of a total of 64 with the 8 level 1 octcubes.  If the deepest
		/// level is 3, we combine the popular level 3 octcubes out of a
		/// total 512 with the 64 level 2 octcubes that span the color space.
		/// In the latter case, we require a minimum of 64 colors for the level 2
		/// octcubes, plus up to 192 additional colors determined from level 3
		/// popularity.
		/// The parameter 'maxlevel' is the deepest octcube level that is used.
		/// The implementation also uses two LUTs, which are employed in
		/// two successive traversals of the dest image.  The first maps
		/// from the src octindex at 'maxlevel' to the color table index,
		/// which is the value that is stored in the 4 or 8 bpp dest pixel.
		/// The second LUT maps from that colormap value in the dest to a
		/// new colormap value for a minimum sized colormap, stored back in
		/// the dest.  It is used to remove any color map entries that
		/// correspond to color space regions that have no pixels in the
		/// source image.  These regions can be either from the higher level
		/// e.g., level 1 for 4 bpp, or from octcubes at 'maxlevel' that
		/// are unoccupied.  This remapping results in the minimum number
		/// of colors used according to the constraints induced by the
		/// input 'maxcolors'.  We also compute the average R, G and B color
		/// values in each region of the color space represented by a
		/// colormap entry, and store them in the colormap.
		/// The maximum number of colors is input, which determines the
		/// following properties of the dest image and octcube regions used:
		/// Number of colors  dest image depth  maxlevel
		/// ----------------  ----------------  --------
		/// 8 to 16  4 bpp   2
		/// 17 to 64   8 bpp   2
		/// 65 to 256  8 bpp   3
		/// It may turn out that the number of extra colors, beyond the
		/// minimum 8 and 64 for maxlevel 2 and 3, respectively, is larger
		/// than the actual number of occupied cubes at these levels
		/// In that case, all the pixels are contained in this
		/// subset of cubes at maxlevel, and no colormap colors are needed
		/// to represent the remainder pixels one level above.  Thus, for
		/// example, in use one often finds that the pixels in an image
		/// occupy less than 192 octcubes at level 3, so they can be represented
		/// by a colormap for octcubes at level 3 only.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixOctreeQuantNumColors/*"/>
		///  <param name="pixs">[in] - 32 bpp rgb</param>
		///  <param name="maxcolors">[in] - 8 to 256 the actual number of colors used may be less than this</param>
		///  <param name="subsample">[in] - factor for computing color distribution use 0 for default</param>
		///   <returns>pixd 4 or 8 bpp, colormapped, or NULL on error</returns>
		public static Pix pixOctreeQuantNumColors(
						Pix pixs,
						int maxcolors,
						int subsample)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if ((new List<int> {32}).Contains ((int)pixs.d) == false) { throw new ArgumentException ("32 bpp rgb"); }
			IntPtr _Result = Natives.pixOctreeQuantNumColors(pixs.Pointer,   maxcolors,   subsample);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// colorquant1.c (2587, 1)
		// pixOctcubeQuantMixedWithGray(pixs, depth, graylevels, delta) as Pix
		// pixOctcubeQuantMixedWithGray(PIX *, l_int32, l_int32, l_int32) as PIX *
		///  <summary>
		/// (1) Generates a colormapped image, where the colormap table values
		/// have two components: octcube values representing pixels with
		/// color content, and grayscale values for the rest.<para/>
		///
		/// (2) The threshold (delta) is the maximum allowable difference of
		/// the max abs value of | r - g |, | r - b | and | g - b |.<para/>
		///
		/// (3) The octcube values are the averages of all pixels that are
		/// found in the octcube, and that are far enough from gray to
		/// be considered color.  This can roughly be visualized as all
		/// the points in the rgb color cube that are not within a "cylinder"
		/// of diameter approximately 'delta' along the main diagonal.<para/>
		///
		/// (4) We want to guarantee full coverage of the rgb color space thus,
		/// if the output depth is 4, the octlevel is 1 (2 x 2 x 2 = 8 cubes)
		/// and if the output depth is 8, the octlevel is 2 (4 x 4 x 4
		/// = 64 cubes).<para/>
		///
		/// (5) Consequently, we have the following constraint on the number
		/// of allowed gray levels: for 4 bpp, 8 for 8 bpp, 192.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixOctcubeQuantMixedWithGray/*"/>
		///  <param name="pixs">[in] - 32 bpp rgb</param>
		///  <param name="depth">[in] - of output pix</param>
		///  <param name="graylevels">[in] - graylevels (must be  is greater  1)</param>
		///  <param name="delta">[in] - threshold for deciding if a pix is color or gray</param>
		///   <returns>pixd     quantized to octcube and gray levels or NULL on error</returns>
		public static Pix pixOctcubeQuantMixedWithGray(
						Pix pixs,
						int depth,
						int graylevels,
						int delta)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if ((new List<int> {32}).Contains ((int)pixs.d) == false) { throw new ArgumentException ("32 bpp rgb"); }
			IntPtr _Result = Natives.pixOctcubeQuantMixedWithGray(pixs.Pointer,   depth,   graylevels,   delta);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// colorquant1.c (2806, 1)
		// pixFixedOctcubeQuant256(pixs, ditherflag) as Pix
		// pixFixedOctcubeQuant256(PIX *, l_int32) as PIX *
		///  <summary>
		/// This simple 1-pass color quantization works by breaking the
		/// color space into 256 pieces, with 3 bits quantized for each of
		/// red and green, and 2 bits quantized for blue.  We shortchange
		/// blue because the eye is least sensitive to blue.  This
		/// division of the color space is into two levels of octrees,
		/// followed by a further division by 4 not 8, where both
		/// blue octrees have been combined in the third level.
		/// The color map is generated from the 256 color centers by
		/// taking the representative color to be the center of the
		/// cell volume.  This gives a maximum error in the red and
		/// green values of 16 levels, and a maximum error in the
		/// blue sample of 32 levels.
		/// Each pixel in the 24-bit color image is placed in its containing
		/// cell, given by the relevant MSbits of the red, green and blue
		/// samples.  An error-diffusion dithering is performed on each
		/// color sample to give the appearance of good average local color.
		/// Dithering is required without it, the contouring and visible
		/// color errors are very bad.
		/// I originally implemented this algorithm in two passes,
		/// where the first pass was used to compute the weighted average
		/// of each sample in each pre-allocated region of color space.
		/// The idea was to use these centroids in the dithering algorithm
		/// of the second pass, to reduce the average error that was
		/// being dithered.  However, with dithering, there is
		/// virtually no difference, so there is no reason to make the
		/// first pass.  Consequently, this 1-pass version just assigns
		/// the pixels to the centers of the pre-allocated cells.
		/// We use dithering to spread the difference between the sample
		/// value and the location of the center of the cell.  For speed
		/// and simplicity, we use integer dithering and propagate only
		/// to the right, down, and diagonally down-right, with ratios
		/// 3/8, 3/8 and 1/4, respectively.  The results should be nearly
		/// as good, and a bit faster, with propagation only to the right
		/// and down.
		/// The algorithm is very fast, because there is no search,
		/// only fast generation of the cell index for each pixel.
		/// We use a simple mapping from the three 8 bit rgb samples
		/// to the 8 bit cell index namely, r7 r6 r5 g7 g6 g5 b7 b6.
		/// This is not in an octcube format, but it doesn't matter.
		/// There are no storage requirements.  We could keep a
		/// running average of the center of each sample in each
		/// cluster, rather than using the center of the cell, but
		/// this is just extra work, esp. with dithering.
		/// This method gives surprisingly good results with dithering.
		/// However, without dithering, the loss of color accuracy is
		/// evident in regions that are very light or that have subtle
		/// blending of colors.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixFixedOctcubeQuant256/*"/>
		///  <param name="pixs">[in] - 32 bpp 24-bit color</param>
		///  <param name="ditherflag">[in] - 1 for dithering 0 for no dithering</param>
		///   <returns>pixd 8 bit with colormap, or NULL on error</returns>
		public static Pix pixFixedOctcubeQuant256(
						Pix pixs,
						int ditherflag)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			IntPtr _Result = Natives.pixFixedOctcubeQuant256(pixs.Pointer,   ditherflag);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// colorquant1.c (2940, 1)
		// pixFewColorsOctcubeQuant1(pixs, level) as Pix
		// pixFewColorsOctcubeQuant1(PIX *, l_int32) as PIX *
		///  <summary>
		/// (1) Generates a colormapped image, where the colormap table values
		/// are the averages of all pixels that are found in the octcube.<para/>
		///
		/// (2) This fails if there are more than 256 colors (i.e., more
		/// than 256 occupied octcubes).<para/>
		///
		/// (3) Often level 3 (512 octcubes) will succeed because not more
		/// than half of them are occupied with 1 or more pixels.<para/>
		///
		/// (4) The depth of the result, which is either 2, 4 or 8 bpp,
		/// is the minimum required to hold the number of colors that
		/// are found.<para/>
		///
		/// (5) This can be useful for quantizing orthographically generated
		/// images such as color maps, where there may be more than 256 colors
		/// because of aliasing or jpeg artifacts on text or lines, but
		/// there are a relatively small number of solid colors.  Then,
		/// use with level = 3 can often generate a compact and accurate
		/// representation of the original RGB image.  For this purpose,
		/// it is better than pixFewColorsOctcubeQuant2(), because it
		/// uses the average value of pixels in the octcube rather
		/// than the first found pixel.  It is also simpler to use,
		/// because it generates the histogram internally.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixFewColorsOctcubeQuant1/*"/>
		///  <param name="pixs">[in] - 32 bpp rgb</param>
		///  <param name="level">[in] - significant bits for each of RGB valid in [1...6]</param>
		///   <returns>pixd quantized to octcube or NULL on error</returns>
		public static Pix pixFewColorsOctcubeQuant1(
						Pix pixs,
						int level)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if ((new List<int> {32}).Contains ((int)pixs.d) == false) { throw new ArgumentException ("32 bpp rgb"); }
			IntPtr _Result = Natives.pixFewColorsOctcubeQuant1(pixs.Pointer,   level);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// colorquant1.c (3110, 1)
		// pixFewColorsOctcubeQuant2(pixs, level, na, ncolors, pnerrors) as Pix
		// pixFewColorsOctcubeQuant2(PIX *, l_int32, NUMA *, l_int32, l_int32 *) as PIX *
		///  <summary>
		/// (1) Generates a colormapped image, where the colormap table values
		/// are the averages of all pixels that are found in the octcube.<para/>
		///
		/// (2) This fails if there are more than 256 colors (i.e., more
		/// than 256 occupied octcubes).<para/>
		///
		/// (3) Often level 3 (512 octcubes) will succeed because not more
		/// than half of them are occupied with 1 or more pixels.<para/>
		///
		/// (4) For an image with not more than 256 colors, it is unlikely
		/// that two pixels of different color will fall in the same
		/// octcube at level = 4. However it is possible, and this
		/// function optionally returns %nerrors, the number of pixels
		/// where, because more than one color is in the same octcube,
		/// the pixel color is not exactly reproduced in the colormap.
		/// The colormap for an occupied leaf of the octree contains
		/// the color of the first pixel encountered in that octcube.<para/>
		///
		/// (5) This differs from pixFewColorsOctcubeQuant1(), which also
		/// requires not more than 256 occupied leaves, but represents
		/// the color of each leaf by an average over the pixels in
		/// that leaf.  This also requires precomputing the histogram
		/// of occupied octree leaves, which is generated using
		/// pixOctcubeHistogram().<para/>
		///
		/// (6) This is used in pixConvertRGBToColormap() for images that
		/// are determined, by their histogram, to have relatively few
		/// colors.  This typically happens with orthographically
		/// produced images (as oppopsed to natural images), where
		/// it is expected that most of the pixels within a leaf
		/// octcube have exactly the same color, and quantization to
		/// that color is lossless.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixFewColorsOctcubeQuant2/*"/>
		///  <param name="pixs">[in] - 32 bpp rgb</param>
		///  <param name="level">[in] - of octcube indexing, for histogram: 3, 4, 5, 6</param>
		///  <param name="na">[in] - histogram of pixel occupation in octree leaves at given level</param>
		///  <param name="ncolors">[in] - number of occupied octree leaves at given level</param>
		///  <param name="pnerrors">[out][optional] - num of pixels not exactly represented in the colormap</param>
		///   <returns>pixd 2, 4 or 8 bpp with colormap, or NULL on error</returns>
		public static Pix pixFewColorsOctcubeQuant2(
						Pix pixs,
						int level,
						Numa na,
						int ncolors,
						out int pnerrors)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (na == null) {throw new ArgumentNullException  ("na cannot be Nothing");}

			if ((new List<int> {32}).Contains ((int)pixs.d) == false) { throw new ArgumentException ("32 bpp rgb"); }
			IntPtr _Result = Natives.pixFewColorsOctcubeQuant2(pixs.Pointer,   level, na.Pointer,   ncolors, out  pnerrors);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// colorquant1.c (3299, 1)
		// pixFewColorsOctcubeQuantMixed(pixs, level, darkthresh, lightthresh, diffthresh, minfract, maxspan) as Pix
		// pixFewColorsOctcubeQuantMixed(PIX *, l_int32, l_int32, l_int32, l_int32, l_float32, l_int32) as PIX *
		///  <summary>
		/// (1) First runs pixFewColorsOctcubeQuant1().  If this succeeds,
		/// it separates the color from gray(ish) entries in the cmap,
		/// and re-quantizes the gray pixels.  The result has some pixels
		/// in color and others in gray.<para/>
		///
		/// (2) This fails if there are more than 256 colors (i.e., more
		/// than 256 occupied octcubes in the color quantization).<para/>
		///
		/// (3) Level 3 (512 octcubes) will usually succeed because not more
		/// than half of them are occupied with 1 or more pixels.<para/>
		///
		/// (4) This uses the criterion from pixColorFraction() for deciding
		/// if a colormap entry is color namely, if the color components
		/// are not too close to either black or white, and the maximum
		/// difference between component values equals or exceeds a threshold.<para/>
		///
		/// (5) For quantizing the gray pixels, it uses a histogram-based
		/// method where input parameters determining the buckets are
		/// the minimum population fraction and the maximum allowed size.<para/>
		///
		/// (6) Recommended input parameters are:
		/// %level:  3 or 4  (3 is default)
		/// %darkthresh:  20
		/// %lightthresh: 244
		/// %diffthresh: 20
		/// %minfract: 0.05
		/// %maxspan: 15
		/// These numbers are intended to be conservative (somewhat over-
		/// sensitive) in color detection,  It's usually better to pay
		/// extra with octcube quantization of a grayscale image than
		/// to use grayscale quantization on an image that has some
		/// actual color.  Input 0 on any of these to get the default.<para/>
		///
		/// (7) This can be useful for quantizing orthographically generated
		/// images such as color maps, where there may be more than 256 colors
		/// because of aliasing or jpeg artifacts on text or lines, but
		/// there are a relatively small number of solid colors.  It usually
		/// gives results that are better than pixOctcubeQuantMixedWithGray(),
		/// both in size and appearance.  But it is a bit slower.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixFewColorsOctcubeQuantMixed/*"/>
		///  <param name="pixs">[in] - 32 bpp rgb</param>
		///  <param name="level">[in] - significant octcube bits for each of RGB valid in [1...6] use 0 for default</param>
		///  <param name="darkthresh">[in] - threshold near black if the lightest component is below this, the pixel is not considered to be gray or color uses 0 for default</param>
		///  <param name="lightthresh">[in] - threshold near white if the darkest component is above this, the pixel is not considered to be gray or color use 0 for default</param>
		///  <param name="diffthresh">[in] - thresh for the max difference between component values for differences below this, the pixel is considered to be gray use 0 for default</param>
		///  <param name="minfract">[in] - min fraction of pixels for gray histo bin use 0.0 for default</param>
		///  <param name="maxspan">[in] - max size of gray histo bin use 0 for default</param>
		///   <returns>pixd 8 bpp, quantized to octcube for pixels that are not gray gray pixels are quantized separately over the full gray range, or NULL on error</returns>
		public static Pix pixFewColorsOctcubeQuantMixed(
						Pix pixs,
						int level,
						int darkthresh,
						int lightthresh,
						int diffthresh,
						Single minfract,
						int maxspan)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if ((new List<int> {32}).Contains ((int)pixs.d) == false) { throw new ArgumentException ("32 bpp rgb"); }
			IntPtr _Result = Natives.pixFewColorsOctcubeQuantMixed(pixs.Pointer,   level,   darkthresh,   lightthresh,   diffthresh,   minfract,   maxspan);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// colorquant1.c (3417, 1)
		// pixFixedOctcubeQuantGenRGB(pixs, level) as Pix
		// pixFixedOctcubeQuantGenRGB(PIX *, l_int32) as PIX *
		///  <summary>
		/// (1) Unlike the other color quantization functions, this one
		/// generates an rgb image.<para/>
		///
		/// (2) The pixel values are quantized to the center of each octcube
		/// (at the specified level) containing the pixel.  They are
		/// not quantized to the average of the pixels in that octcube.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixFixedOctcubeQuantGenRGB/*"/>
		///  <param name="pixs">[in] - 32 bpp rgb</param>
		///  <param name="level">[in] - significant bits for each of r,g,b</param>
		///   <returns>pixd rgb quantized to octcube centers, or NULL on error</returns>
		public static Pix pixFixedOctcubeQuantGenRGB(
						Pix pixs,
						int level)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if ((new List<int> {32}).Contains ((int)pixs.d) == false) { throw new ArgumentException ("32 bpp rgb"); }
			IntPtr _Result = Natives.pixFixedOctcubeQuantGenRGB(pixs.Pointer,   level);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// colorquant1.c (3490, 1)
		// pixQuantFromCmap(pixs, cmap, mindepth, level, metric) as Pix
		// pixQuantFromCmap(PIX *, PIXCMAP *, l_int32, l_int32, l_int32) as PIX *
		///  <summary>
		/// (1) This is a top-level wrapper for quantizing either grayscale
		/// or rgb images to a specified colormap.<para/>
		///
		/// (2) The actual output depth is constrained by %mindepth and
		/// by the number of colors in %cmap.<para/>
		///
		/// (3) For grayscale, %level and %metric are ignored.<para/>
		///
		/// (4) If the cmap has color and pixs is grayscale, the color is
		/// removed from the cmap before quantizing pixs.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixQuantFromCmap/*"/>
		///  <param name="pixs">[in] - 8 bpp grayscale without cmap, or 32 bpp rgb</param>
		///  <param name="cmap">[in] - to quantize to insert copy into dest pix</param>
		///  <param name="mindepth">[in] - minimum depth of pixd: can be 2, 4 or 8 bpp</param>
		///  <param name="level">[in] - of octcube used for finding nearest color in cmap</param>
		///  <param name="metric">[in] - L_MANHATTAN_DISTANCE, L_EUCLIDEAN_DISTANCE</param>
		///   <returns>pixd  2, 4 or 8 bpp, colormapped, or NULL on error</returns>
		public static Pix pixQuantFromCmap(
						Pix pixs,
						PixColormap cmap,
						int mindepth,
						int level,
						int metric)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (cmap == null) {throw new ArgumentNullException  ("cmap cannot be Nothing");}

			IntPtr _Result = Natives.pixQuantFromCmap(pixs.Pointer, cmap.Pointer,   mindepth,   level,   metric);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// colorquant1.c (3579, 1)
		// pixOctcubeQuantFromCmap(pixs, cmap, mindepth, level, metric) as Pix
		// pixOctcubeQuantFromCmap(PIX *, PIXCMAP *, l_int32, l_int32, l_int32) as PIX *
		///  <summary>
		/// (1) In typical use, we are doing an operation, such as
		/// interpolative scaling, on a colormapped pix, where it is
		/// necessary to remove the colormap before the operation.
		/// We then want to re-quantize the RGB result using the same
		/// colormap.<para/>
		///
		/// (2) The level is used to divide the color space into octcubes.
		/// Each input pixel is, in effect, placed at the center of an
		/// octcube at the given level, and it is mapped into the
		/// exact color (given in the colormap) that is the closest
		/// to that location.  We need to know that distance, for each color
		/// in the colormap.  The higher the level of the octtree, the smaller
		/// the octcubes in the color space, and hence the more accurately
		/// we can determine the closest color in the colormap however,
		/// the size of the LUT, which is the total number of octcubes,
		/// increases by a factor of 8 for each increase of 1 level.
		/// The time required to acquire a level 4 mapping table, which has
		/// about 4K entries, is less than 1 msec, so that is the
		/// recommended minimum size to be used.  At that size, the
		/// octcubes have their centers 16 units apart in each (r,g,b)
		/// direction.  If two colors are in the same octcube, the one
		/// closest to the center will always be chosen.  The maximum
		/// error for any component occurs when the correct color is
		/// at a cube corner and there is an incorrect color just inside
		/// the cube next to the opposite corner, giving an error of
		/// 14 units (out of 256) for each component. Using a level 5
		/// mapping table reduces the maximum error to 6 units.<para/>
		///
		/// (3) Typically you should use the Euclidean metric, because the
		/// resulting voronoi cells (which are generated using the actual
		/// colormap values as seeds) are convex for Euclidean distance
		/// but not for Manhattan distance.  In terms of the octcubes,
		/// convexity of the voronoi cells means that if the 8 corners
		/// of any cube (of which the octcubes are special cases)
		/// are all within a cell, then every point in the cube will
		/// lie within the cell.<para/>
		///
		/// (4) The depth of the output pixd is equal to the maximum of
		/// (a) %mindepth and (b) the minimum (2, 4 or 8 bpp) necessary
		/// to hold the indices in the colormap.<para/>
		///
		/// (5) We build a mapping table from octcube to colormap index so
		/// that this function can run in a time (otherwise) independent
		/// of the number of colors in the colormap.  This avoids a
		/// brute-force search for the closest colormap color to each
		/// pixel in the image.<para/>
		///
		/// (6) This is similar to the function pixAssignToNearestColor()
		/// used for color segmentation.<para/>
		///
		/// (7) Except for very small images or when using level  is greater  4,
		/// it takes very little time to generate the tables,
		/// compared to the generation of the colormapped dest pix,
		/// so one would not typically use the low-level version.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixOctcubeQuantFromCmap/*"/>
		///  <param name="pixs">[in] - 32 bpp rgb</param>
		///  <param name="cmap">[in] - to quantize to insert copy into dest pix</param>
		///  <param name="mindepth">[in] - minimum depth of pixd: can be 2, 4 or 8 bpp</param>
		///  <param name="level">[in] - of octcube used for finding nearest color in cmap</param>
		///  <param name="metric">[in] - L_MANHATTAN_DISTANCE, L_EUCLIDEAN_DISTANCE</param>
		///   <returns>pixd  2, 4 or 8 bpp, colormapped, or NULL on error</returns>
		public static Pix pixOctcubeQuantFromCmap(
						Pix pixs,
						PixColormap cmap,
						int mindepth,
						int level,
						int metric)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (cmap == null) {throw new ArgumentNullException  ("cmap cannot be Nothing");}

			if ((new List<int> {32}).Contains ((int)pixs.d) == false) { throw new ArgumentException ("32 bpp rgb"); }
			IntPtr _Result = Natives.pixOctcubeQuantFromCmap(pixs.Pointer, cmap.Pointer,   mindepth,   level,   metric);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// colorquant1.c (3729, 1)
		// pixOctcubeHistogram(pixs, level, pncolors) as Numa
		// pixOctcubeHistogram(PIX *, l_int32, l_int32 *) as NUMA *
		///  <summary>
		/// (1) Input NULL for [and]ncolors to prevent computation and return value.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixOctcubeHistogram/*"/>
		///  <param name="pixs">[in] - 32 bpp rgb</param>
		///  <param name="level">[in] - significant bits for each of RGB valid in [1...6]</param>
		///  <param name="pncolors">[out][optional] - number of occupied cubes</param>
		///   <returns>numa histogram of color pixels, or NULL on error</returns>
		public static Numa pixOctcubeHistogram(
						Pix pixs,
						int level,
						out int pncolors)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if ((new List<int> {32}).Contains ((int)pixs.d) == false) { throw new ArgumentException ("32 bpp rgb"); }
			IntPtr _Result = Natives.pixOctcubeHistogram(pixs.Pointer,   level, out  pncolors);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Numa(_Result);
		}

		// colorquant1.c (3852, 1)
		// pixcmapToOctcubeLUT(cmap, level, metric) as int[]
		// pixcmapToOctcubeLUT(PIXCMAP *, l_int32, l_int32) as l_int32 *
		///  <summary>
		/// (1) This function is used to quickly find the colormap color
		/// that is closest to any rgb color.  It is used to assign
		/// rgb colors to an existing colormap.  It can be very expensive
		/// to search through the entire colormap for the closest color
		/// to each pixel.  Instead, we first set up this table, which is
		/// populated by the colormap index nearest to each octcube
		/// color.  Then we go through the image for each pixel,
		/// do two table lookups: first to generate the octcube index
		/// from rgb and second to use this table to read out the
		/// colormap index.<para/>
		///
		/// (2) Do a slight modification for white and black.  For level = 4,
		/// each octcube size is 16.  The center of the whitest octcube
		/// is at (248, 248, 248), which is closer to 242 than 255.
		/// Consequently, any gray color between 242 and 254 will
		/// be selected, even if white (255, 255, 255) exists.  This is
		/// typically not optimal, because the original color was
		/// likely white.  Therefore, if white exists in the colormap,
		/// use it for any rgb color that falls into the most white octcube.
		/// Do the similar thing for black.<para/>
		///
		/// (3) Here are the actual function calls for quantizing to a
		/// specified colormap:
		/// ~ first make the tables that map from rgb to octcube index
		/// makeRGBToIndexTables()
		/// ~ then for each pixel:
		/// use the tables to get the octcube index
		/// getOctcubeIndexFromRGB()
		/// use this table to get the nearest color in the colormap
		/// cmap_index = tab[index]<para/>
		///
		/// (4) Distance can be either manhattan or euclidean.<para/>
		///
		/// (5) In typical use, level = 4 gives reasonable results, and
		/// level = 5 is slightly better.  When this function is used
		/// for color segmentation, there are typically a small number
		/// of colors and the number of levels can be small (e.g., level = 3).
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixcmapToOctcubeLUT/*"/>
		///  <param name="cmap">[in] - </param>
		///  <param name="level">[in] - significant bits for each of RGB valid in [1...6]</param>
		///  <param name="metric">[in] - L_MANHATTAN_DISTANCE, L_EUCLIDEAN_DISTANCE</param>
		///   <returns>tab[23  level]</returns>
		public static int[] pixcmapToOctcubeLUT(
						PixColormap cmap,
						int level,
						int metric)
		{
			if (cmap == null) {throw new ArgumentNullException  ("cmap cannot be Nothing");}

			int[] _Result = Natives.pixcmapToOctcubeLUT(cmap.Pointer,   level,   metric);
			return _Result;
		}

		// colorquant1.c (3938, 1)
		// pixRemoveUnusedColors(pixs) as int
		// pixRemoveUnusedColors(PIX *) as l_ok
		///  <summary>
		/// (1) This is an in-place operation.<para/>
		///
		/// (2) If the image doesn't have a colormap, returns without error.<para/>
		///
		/// (3) Unusued colors are removed from the colormap, and the
		/// image pixels are re-numbered.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixRemoveUnusedColors/*"/>
		///  <param name="pixs">[in] - colormapped</param>
		///   <returns>0 if OK, 1 on error</returns>
		public static int pixRemoveUnusedColors(
						Pix pixs)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			int _Result = Natives.pixRemoveUnusedColors(pixs.Pointer);
			return _Result;
		}

		// colorquant1.c (4084, 1)
		// pixNumberOccupiedOctcubes(pix, level, mincount, minfract, pncolors) as int
		// pixNumberOccupiedOctcubes(PIX *, l_int32, l_int32, l_float32, l_int32 *) as l_ok
		///  <summary>
		/// (1) Exactly one of (%mincount, %minfract) must be -1, so, e.g.,
		/// if %mincount == -1, then we use %minfract.<para/>
		///
		/// (2) If all occupied octcubes are to count, set %mincount == 1.
		/// Setting %minfract == 0.0 is taken to mean the same thing.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixNumberOccupiedOctcubes/*"/>
		///  <param name="pix">[in] - 32 bpp</param>
		///  <param name="level">[in] - of octcube</param>
		///  <param name="mincount">[in] - minimum num pixels in an octcube to be counted -1 to not use</param>
		///  <param name="minfract">[in] - minimum fract of pixels in an octcube to be counted -1 to not use</param>
		///  <param name="pncolors">[out] - number of occupied octcubes</param>
		///   <returns>0 if OK, 1 on error</returns>
		public static int pixNumberOccupiedOctcubes(
						Pix pix,
						int level,
						int mincount,
						Single minfract,
						out int pncolors)
		{
			if (pix == null) {throw new ArgumentNullException  ("pix cannot be Nothing");}

			int _Result = Natives.pixNumberOccupiedOctcubes(pix.Pointer,   level,   mincount,   minfract, out  pncolors);
			return _Result;
		}


	}
}
