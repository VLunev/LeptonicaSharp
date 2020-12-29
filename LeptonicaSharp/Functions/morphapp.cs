using System;
using System.IO;

using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LeptonicaSharp
{
	public partial class _All
	{

		// morphapp.c (108, 1)
		// pixExtractBoundary(pixs, type) as Pix
		// pixExtractBoundary(PIX *, l_int32) as PIX *
		///  <summary>
		/// (1) Extracts the fg or bg boundary pixels for each component.
		/// Components are assumed to end at the boundary of pixs.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixExtractBoundary/*"/>
		///  <param name="pixs">[in] - 1 bpp</param>
		///  <param name="type">[in] - 0 for background pixels 1 for foreground pixels</param>
		///   <returns>pixd, or NULL on error</returns>
		public static Pix pixExtractBoundary(
						Pix pixs,
						int type)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if ((new List<int> {1}).Contains ((int)pixs.d) == false) { throw new ArgumentException ("1 bpp"); }
			IntPtr _Result = Natives.pixExtractBoundary(pixs.Pointer,   type);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// morphapp.c (148, 1)
		// pixMorphSequenceMasked(pixs, pixm, sequence, dispsep) as Pix
		// pixMorphSequenceMasked(PIX *, PIX *, const char *, l_int32) as PIX *
		///  <summary>
		/// (1) This applies the morph sequence to the image, but only allows
		/// changes in pixs for pixels under the background of pixm.<para/>
		///
		/// (5) If pixm is NULL, this is just pixMorphSequence().
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixMorphSequenceMasked/*"/>
		///  <param name="pixs">[in] - 1 bpp</param>
		///  <param name="pixm">[in][optional] - 1 bpp mask</param>
		///  <param name="sequence">[in] - string specifying sequence of operations</param>
		///  <param name="dispsep">[in] - horizontal separation in pixels between successive displays use zero to suppress display</param>
		///   <returns>pixd, or NULL on error</returns>
		public static Pix pixMorphSequenceMasked(
						Pix pixs,
						Pix pixm,
						String sequence,
						int dispsep)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (sequence == null) {throw new ArgumentNullException  ("sequence cannot be Nothing");}

			if ((new List<int> {1}).Contains ((int)pixs.d) == false) { throw new ArgumentException ("1 bpp"); }
			IntPtr pixmPtr = IntPtr.Zero; 	if (pixm != null) {pixmPtr = pixm.Pointer;}
			IntPtr _Result = Natives.pixMorphSequenceMasked(pixs.Pointer, pixmPtr,   sequence,   dispsep);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// morphapp.c (195, 1)
		// pixMorphSequenceByComponent(pixs, sequence, connectivity, minw, minh, pboxa) as Pix
		// pixMorphSequenceByComponent(PIX *, const char *, l_int32, l_int32, l_int32, BOXA **) as PIX *
		///  <summary>
		/// (1) See pixMorphSequence() for composing operation sequences.<para/>
		///
		/// (2) This operates separately on each c.c. in the input pix.<para/>
		///
		/// (3) The dilation does NOT increase the c.c. size it is clipped
		/// to the size of the original c.c. This is necessary to
		/// keep the c.c. independent after the operation.<para/>
		///
		/// (4) You can specify that the width and/or height must equal
		/// or exceed a minimum size for the operation to take place.<para/>
		///
		/// (5) Use NULL for boxa to avoid returning the boxa.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixMorphSequenceByComponent/*"/>
		///  <param name="pixs">[in] - 1 bpp</param>
		///  <param name="sequence">[in] - string specifying sequence</param>
		///  <param name="connectivity">[in] - 4 or 8</param>
		///  <param name="minw">[in] - minimum width to consider use 0 or 1 for any width</param>
		///  <param name="minh">[in] - minimum height to consider use 0 or 1 for any height</param>
		///  <param name="pboxa">[out][optional] - return boxa of c.c. in pixs</param>
		///   <returns>pixd, or NULL on error</returns>
		public static Pix pixMorphSequenceByComponent(
						Pix pixs,
						String sequence,
						int connectivity,
						int minw,
						int minh,
						out Boxa pboxa)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (sequence == null) {throw new ArgumentNullException  ("sequence cannot be Nothing");}

			if ((new List<int> {1}).Contains ((int)pixs.d) == false) { throw new ArgumentException ("1 bpp"); }
			IntPtr pboxaPtr = IntPtr.Zero;
			IntPtr _Result = Natives.pixMorphSequenceByComponent(pixs.Pointer,   sequence,   connectivity,   minw,   minh, out pboxaPtr);
			if (pboxaPtr == IntPtr.Zero) {pboxa = null;} else { pboxa = new Boxa(pboxaPtr); };

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// morphapp.c (265, 1)
		// pixaMorphSequenceByComponent(pixas, sequence, minw, minh) as Pixa
		// pixaMorphSequenceByComponent(PIXA *, const char *, l_int32, l_int32) as PIXA *
		///  <summary>
		/// (1) See pixMorphSequence() for composing operation sequences.<para/>
		///
		/// (2) This operates separately on each c.c. in the input pixa.<para/>
		///
		/// (3) You can specify that the width and/or height must equal
		/// or exceed a minimum size for the operation to take place.<para/>
		///
		/// (4) The input pixa should have a boxa giving the locations
		/// of the pix components.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixaMorphSequenceByComponent/*"/>
		///  <param name="pixas">[in] - of 1 bpp pix</param>
		///  <param name="sequence">[in] - string specifying sequence</param>
		///  <param name="minw">[in] - minimum width to consider use 0 or 1 for any width</param>
		///  <param name="minh">[in] - minimum height to consider use 0 or 1 for any height</param>
		///   <returns>pixad, or NULL on error</returns>
		public static Pixa pixaMorphSequenceByComponent(
						Pixa pixas,
						String sequence,
						int minw,
						int minh)
		{
			if (pixas == null) {throw new ArgumentNullException  ("pixas cannot be Nothing");}

			if (sequence == null) {throw new ArgumentNullException  ("sequence cannot be Nothing");}

			IntPtr _Result = Natives.pixaMorphSequenceByComponent(pixas.Pointer,   sequence,   minw,   minh);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pixa(_Result);
		}

		// morphapp.c (348, 1)
		// pixMorphSequenceByRegion(pixs, pixm, sequence, connectivity, minw, minh, pboxa) as Pix
		// pixMorphSequenceByRegion(PIX *, PIX *, const char *, l_int32, l_int32, l_int32, BOXA **) as PIX *
		///  <summary>
		/// (1) See pixMorphCompSequence() for composing operation sequences.<para/>
		///
		/// (2) This operates separately on the region in pixs corresponding
		/// to each c.c. in the mask pixm.  It differs from
		/// pixMorphSequenceByComponent() in that the latter does not have
		/// a pixm (mask), but instead operates independently on each
		/// component in pixs.<para/>
		///
		/// (3) Dilation will NOT increase the region size the result
		/// is clipped to the size of the mask region.  This is necessary
		/// to make regions independent after the operation.<para/>
		///
		/// (4) You can specify that the width and/or height of a region must
		/// equal or exceed a minimum size for the operation to take place.<para/>
		///
		/// (5) Use NULL for %pboxa to avoid returning the boxa.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixMorphSequenceByRegion/*"/>
		///  <param name="pixs">[in] - 1 bpp</param>
		///  <param name="pixm">[in] - mask specifying regions</param>
		///  <param name="sequence">[in] - string specifying sequence</param>
		///  <param name="connectivity">[in] - 4 or 8, used on mask</param>
		///  <param name="minw">[in] - minimum width to consider use 0 or 1 for any width</param>
		///  <param name="minh">[in] - minimum height to consider use 0 or 1 for any height</param>
		///  <param name="pboxa">[out][optional] - return boxa of c.c. in pixm</param>
		///   <returns>pixd, or NULL on error</returns>
		public static Pix pixMorphSequenceByRegion(
						Pix pixs,
						Pix pixm,
						String sequence,
						int connectivity,
						int minw,
						int minh,
						out Boxa pboxa)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (pixm == null) {throw new ArgumentNullException  ("pixm cannot be Nothing");}

			if (sequence == null) {throw new ArgumentNullException  ("sequence cannot be Nothing");}

			if ((new List<int> {1}).Contains ((int)pixs.d) == false) { throw new ArgumentException ("1 bpp"); }
			IntPtr pboxaPtr = IntPtr.Zero;
			IntPtr _Result = Natives.pixMorphSequenceByRegion(pixs.Pointer, pixm.Pointer,   sequence,   connectivity,   minw,   minh, out pboxaPtr);
			if (pboxaPtr == IntPtr.Zero) {pboxa = null;} else { pboxa = new Boxa(pboxaPtr); };

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// morphapp.c (427, 1)
		// pixaMorphSequenceByRegion(pixs, pixam, sequence, minw, minh) as Pixa
		// pixaMorphSequenceByRegion(PIX *, PIXA *, const char *, l_int32, l_int32) as PIXA *
		///  <summary>
		/// (1) See pixMorphSequence() for composing operation sequences.<para/>
		///
		/// (2) This operates separately on each region in the input pixs
		/// defined by the components in pixam.<para/>
		///
		/// (3) You can specify that the width and/or height of a mask
		/// component must equal or exceed a minimum size for the
		/// operation to take place.<para/>
		///
		/// (4) The input pixam should have a boxa giving the locations
		/// of the regions in pixs.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixaMorphSequenceByRegion/*"/>
		///  <param name="pixs">[in] - 1 bpp</param>
		///  <param name="pixam">[in] - of 1 bpp mask elements</param>
		///  <param name="sequence">[in] - string specifying sequence</param>
		///  <param name="minw">[in] - minimum width to consider use 0 or 1 for any width</param>
		///  <param name="minh">[in] - minimum height to consider use 0 or 1 for any height</param>
		///   <returns>pixad, or NULL on error</returns>
		public static Pixa pixaMorphSequenceByRegion(
						Pix pixs,
						Pixa pixam,
						String sequence,
						int minw,
						int minh)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (pixam == null) {throw new ArgumentNullException  ("pixam cannot be Nothing");}

			if (sequence == null) {throw new ArgumentNullException  ("sequence cannot be Nothing");}

			if ((new List<int> {1}).Contains ((int)pixs.d) == false) { throw new ArgumentException ("1 bpp"); }
			IntPtr _Result = Natives.pixaMorphSequenceByRegion(pixs.Pointer, pixam.Pointer,   sequence,   minw,   minh);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pixa(_Result);
		}

		// morphapp.c (502, 1)
		// pixUnionOfMorphOps(pixs, sela, type) as Pix
		// pixUnionOfMorphOps(PIX *, SELA *, l_int32) as PIX *
		///  <summary>
		/// pixUnionOfMorphOps()
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixUnionOfMorphOps/*"/>
		///  <param name="pixs">[in] - binary</param>
		///  <param name="sela">[in] - </param>
		///  <param name="type">[in] - L_MORPH_DILATE, etc.</param>
		///   <returns>pixd union of the specified morphological operation on pixs for each Sel in the Sela, or NULL on error</returns>
		public static Pix pixUnionOfMorphOps(
						Pix pixs,
						Sela sela,
						int type)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (sela == null) {throw new ArgumentNullException  ("sela cannot be Nothing");}

			IntPtr _Result = Natives.pixUnionOfMorphOps(pixs.Pointer, sela.Pointer,   type);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// morphapp.c (555, 1)
		// pixIntersectionOfMorphOps(pixs, sela, type) as Pix
		// pixIntersectionOfMorphOps(PIX *, SELA *, l_int32) as PIX *
		///  <summary>
		/// pixIntersectionOfMorphOps()
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixIntersectionOfMorphOps/*"/>
		///  <param name="pixs">[in] - binary</param>
		///  <param name="sela">[in] - </param>
		///  <param name="type">[in] - L_MORPH_DILATE, etc.</param>
		///   <returns>pixd intersection of the specified morphological operation on pixs for each Sel in the Sela, or NULL on error</returns>
		public static Pix pixIntersectionOfMorphOps(
						Pix pixs,
						Sela sela,
						int type)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (sela == null) {throw new ArgumentNullException  ("sela cannot be Nothing");}

			IntPtr _Result = Natives.pixIntersectionOfMorphOps(pixs.Pointer, sela.Pointer,   type);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// morphapp.c (613, 1)
		// pixSelectiveConnCompFill(pixs, connectivity, minw, minh) as Pix
		// pixSelectiveConnCompFill(PIX *, l_int32, l_int32, l_int32) as PIX *
		///  <summary>
		/// pixSelectiveConnCompFill()
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixSelectiveConnCompFill/*"/>
		///  <param name="pixs">[in] - binary</param>
		///  <param name="connectivity">[in] - 4 or 8</param>
		///  <param name="minw">[in] - minimum width to consider use 0 or 1 for any width</param>
		///  <param name="minh">[in] - minimum height to consider use 0 or 1 for any height</param>
		///   <returns>pix with holes filled in selected c.c., or NULL on error</returns>
		public static Pix pixSelectiveConnCompFill(
						Pix pixs,
						int connectivity,
						int minw,
						int minh)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			IntPtr _Result = Natives.pixSelectiveConnCompFill(pixs.Pointer,   connectivity,   minw,   minh);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// morphapp.c (684, 1)
		// pixRemoveMatchedPattern(pixs, pixp, pixe, x0, y0, dsize) as int
		// pixRemoveMatchedPattern(PIX *, PIX *, PIX *, l_int32, l_int32, l_int32) as l_ok
		///  <summary>
		/// (1) This is in-place.<para/>
		///
		/// (2) You can use various functions in selgen to create a Sel
		/// that is used to generate pixe from pixs.<para/>
		///
		/// (3) This function is applied after pixe has been computed.
		/// It finds the centroid of each c.c., and subtracts
		/// (the appropriately dilated version of) pixp, with the center
		/// of the Sel used to align pixp with pixs.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixRemoveMatchedPattern/*"/>
		///  <param name="pixs">[in] - input image, 1 bpp</param>
		///  <param name="pixp">[in] - pattern to be removed from image, 1 bpp</param>
		///  <param name="pixe">[in] - image after erosion by Sel that approximates pixp, 1 bpp</param>
		///  <param name="x0">[in] - center of Sel</param>
		///  <param name="y0">[in] - center of Sel</param>
		///  <param name="dsize">[in] - number of pixels on each side by which pixp is dilated before being subtracted from pixs valid values are {0, 1, 2, 3, 4}</param>
		///   <returns>0 if OK, 1 on error</returns>
		public static int pixRemoveMatchedPattern(
						Pix pixs,
						Pix pixp,
						Pix pixe,
						int x0,
						int y0,
						int dsize)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (pixp == null) {throw new ArgumentNullException  ("pixp cannot be Nothing");}

			if (pixe == null) {throw new ArgumentNullException  ("pixe cannot be Nothing");}

			int _Result = Natives.pixRemoveMatchedPattern(pixs.Pointer, pixp.Pointer, pixe.Pointer,   x0,   y0,   dsize);
			return _Result;
		}

		// morphapp.c (789, 1)
		// pixDisplayMatchedPattern(pixs, pixp, pixe, x0, y0, color, scale, nlevels) as Pix
		// pixDisplayMatchedPattern(PIX *, PIX *, PIX *, l_int32, l_int32, l_uint32, l_float32, l_int32) as PIX *
		///  <summary>
		/// (1) A 4 bpp colormapped image is generated.<para/>
		///
		/// (2) If scale smaller or equal 1.0, do scale to gray for the output, and threshold
		/// to nlevels of gray.<para/>
		///
		/// (3) You can use various functions in selgen to create a Sel
		/// that will generate pixe from pixs.<para/>
		///
		/// (4) This function is applied after pixe has been computed.
		/// It finds the centroid of each c.c., and colors the output
		/// pixels using pixp (appropriately aligned) as a stencil.
		/// Alignment is done using the origin of the Sel and the
		/// centroid of the eroded image to place the stencil pixp.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixDisplayMatchedPattern/*"/>
		///  <param name="pixs">[in] - input image, 1 bpp</param>
		///  <param name="pixp">[in] - pattern to be removed from image, 1 bpp</param>
		///  <param name="pixe">[in] - image after erosion by Sel that approximates pixp, 1 bpp</param>
		///  <param name="x0">[in] - center of Sel</param>
		///  <param name="y0">[in] - center of Sel</param>
		///  <param name="color">[in] - to paint the matched patterns 0xrrggbb00</param>
		///  <param name="scale">[in] - reduction factor for output pixd</param>
		///  <param name="nlevels">[in] - if scale  is smaller 1.0, threshold to this number of levels</param>
		///   <returns>pixd 8 bpp, colormapped, or NULL on error</returns>
		public static Pix pixDisplayMatchedPattern(
						Pix pixs,
						Pix pixp,
						Pix pixe,
						int x0,
						int y0,
						uint color,
						Single scale,
						int nlevels)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (pixp == null) {throw new ArgumentNullException  ("pixp cannot be Nothing");}

			if (pixe == null) {throw new ArgumentNullException  ("pixe cannot be Nothing");}

			IntPtr _Result = Natives.pixDisplayMatchedPattern(pixs.Pointer, pixp.Pointer, pixe.Pointer,   x0,   y0,   color,   scale,   nlevels);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// morphapp.c (901, 1)
		// pixaExtendByMorph(pixas, type, niters, sel, include) as Pixa
		// pixaExtendByMorph(PIXA *, l_int32, l_int32, SEL *, l_int32) as PIXA *
		///  <summary>
		/// (1) This dilates or erodes every pix in %pixas, iteratively,
		/// using the input Sel (or, if null, a 2x2 Sel by default),
		/// and puts the results in %pixad.<para/>
		///
		/// (2) If %niters smaller or equal 0, this is a no-op it returns a clone of pixas.<para/>
		///
		/// (3) If %include == 1, the output %pixad contains all the pix
		/// in %pixas.  Otherwise, it doesn't, but pixaJoin() can be
		/// used later to join pixas with pixad.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixaExtendByMorph/*"/>
		///  <param name="pixas">[in] - </param>
		///  <param name="type">[in] - L_MORPH_DILATE, L_MORPH_ERODE</param>
		///  <param name="niters">[in] - </param>
		///  <param name="sel">[in] - used for dilation, erosion uses 2x2 if null</param>
		///  <param name="include">[in] - 1 to include a copy of the input pixas in pixad 0 to omit</param>
		///   <returns>pixad   with derived pix, using all iterations, or NULL on error</returns>
		public static Pixa pixaExtendByMorph(
						Pixa pixas,
						int type,
						int niters,
						Sel sel,
						int include)
		{
			if (pixas == null) {throw new ArgumentNullException  ("pixas cannot be Nothing");}

			if (sel == null) {throw new ArgumentNullException  ("sel cannot be Nothing");}

			IntPtr _Result = Natives.pixaExtendByMorph(pixas.Pointer,   type,   niters, sel.Pointer,   include);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pixa(_Result);
		}

		// morphapp.c (973, 1)
		// pixaExtendByScaling(pixas, nasc, type, include) as Pixa
		// pixaExtendByScaling(PIXA *, NUMA *, l_int32, l_int32) as PIXA *
		///  <summary>
		/// (1) This scales every pix in %pixas by each factor in %nasc.
		/// and puts the results in %pixad.<para/>
		///
		/// (2) If %include == 1, the output %pixad contains all the pix
		/// in %pixas.  Otherwise, it doesn't, but pixaJoin() can be
		/// used later to join pixas with pixad.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixaExtendByScaling/*"/>
		///  <param name="pixas">[in] - </param>
		///  <param name="nasc">[in] - numa of scaling factors</param>
		///  <param name="type">[in] - L_HORIZ, L_VERT, L_BOTH_DIRECTIONS</param>
		///  <param name="include">[in] - 1 to include a copy of the input pixas in pixad 0 to omit</param>
		///   <returns>pixad   with derived pix, using all scalings, or NULL on error</returns>
		public static Pixa pixaExtendByScaling(
						Pixa pixas,
						Numa nasc,
						int type,
						int include)
		{
			if (pixas == null) {throw new ArgumentNullException  ("pixas cannot be Nothing");}

			if (nasc == null) {throw new ArgumentNullException  ("nasc cannot be Nothing");}

			IntPtr _Result = Natives.pixaExtendByScaling(pixas.Pointer, nasc.Pointer,   type,   include);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pixa(_Result);
		}

		// morphapp.c (1041, 1)
		// pixSeedfillMorph(pixs, pixm, maxiters, connectivity) as Pix
		// pixSeedfillMorph(PIX *, PIX *, l_int32, l_int32) as PIX *
		///  <summary>
		/// (1) This is in general a very inefficient method for filling
		/// from a seed into a mask.  Use it for a small number of iterations,
		/// but if you expect more than a few iterations, use
		/// pixSeedfillBinary().<para/>
		///
		/// (2) We use a 3x3 brick SEL for 8-cc filling and a 3x3 plus SEL for 4-cc.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixSeedfillMorph/*"/>
		///  <param name="pixs">[in] - seed</param>
		///  <param name="pixm">[in] - mask</param>
		///  <param name="maxiters">[in] - use 0 to go to completion</param>
		///  <param name="connectivity">[in] - 4 or 8</param>
		///   <returns>pixd after filling into the mask or NULL on error</returns>
		public static Pix pixSeedfillMorph(
						Pix pixs,
						Pix pixm,
						int maxiters,
						int connectivity)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (pixm == null) {throw new ArgumentNullException  ("pixm cannot be Nothing");}

			IntPtr _Result = Natives.pixSeedfillMorph(pixs.Pointer, pixm.Pointer,   maxiters,   connectivity);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// morphapp.c (1103, 1)
		// pixRunHistogramMorph(pixs, runtype, direction, maxsize) as Numa
		// pixRunHistogramMorph(PIX *, l_int32, l_int32, l_int32) as NUMA *
		///  <summary>
		/// pixRunHistogramMorph()
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixRunHistogramMorph/*"/>
		///  <param name="pixs">[in] - </param>
		///  <param name="runtype">[in] - L_RUN_OFF, L_RUN_ON</param>
		///  <param name="direction">[in] - L_HORIZ, L_VERT</param>
		///  <param name="maxsize">[in] - size of largest runlength counted</param>
		///   <returns>numa of run-lengths</returns>
		public static Numa pixRunHistogramMorph(
						Pix pixs,
						int runtype,
						int direction,
						int maxsize)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			IntPtr _Result = Natives.pixRunHistogramMorph(pixs.Pointer,   runtype,   direction,   maxsize);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Numa(_Result);
		}

		// morphapp.c (1203, 1)
		// pixTophat(pixs, hsize, vsize, type) as Pix
		// pixTophat(PIX *, l_int32, l_int32, l_int32) as PIX *
		///  <summary>
		/// (1) Sel is a brick with all elements being hits<para/>
		///
		/// (2) If hsize = vsize = 1, returns an image with all 0 data.<para/>
		///
		/// (3) The L_TOPHAT_WHITE flag emphasizes small bright regions,
		/// whereas the L_TOPHAT_BLACK flag emphasizes small dark regions.
		/// The L_TOPHAT_WHITE tophat can be accomplished by doing a
		/// L_TOPHAT_BLACK tophat on the inverse, or v.v.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixTophat/*"/>
		///  <param name="pixs">[in] - </param>
		///  <param name="hsize">[in] - of Sel must be odd origin implicitly in center</param>
		///  <param name="vsize">[in] - ditto</param>
		///  <param name="type">[in] - L_TOPHAT_WHITE: image - opening L_TOPHAT_BLACK: closing - image</param>
		///   <returns>pixd, or NULL on error</returns>
		public static Pix pixTophat(
						Pix pixs,
						int hsize,
						int vsize,
						int type)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			IntPtr _Result = Natives.pixTophat(pixs.Pointer,   hsize,   vsize,   type);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// morphapp.c (1303, 1)
		// pixHDome(pixs, height, connectivity) as Pix
		// pixHDome(PIX *, l_int32, l_int32) as PIX *
		///  <summary>
		/// (1) It is more efficient to use a connectivity of 4 for the fill.<para/>
		///
		/// (2) This fills bumps to some level, and extracts the unfilled
		/// part of the bump.  To extract the troughs of basins, first
		/// invert pixs and then apply pixHDome().<para/>
		///
		/// (3) It is useful to compare the HDome operation with the TopHat.
		/// The latter extracts peaks or valleys that have a width
		/// not exceeding the size of the structuring element used
		/// in the opening or closing, rsp.  The height of the peak is
		/// irrelevant.  By contrast, for the HDome, the gray seedfill
		/// is used to extract all peaks that have a height not exceeding
		/// a given value, regardless of their width!<para/>
		///
		/// (4) Slightly more precisely, suppose you set 'height' = 40.
		/// Then all bumps in pixs with a height greater than or equal
		/// to 40 become, in pixd, bumps with a max value of exactly 40.
		/// All shorter bumps have a max value in pixd equal to the height
		/// of the bump.<para/>
		///
		/// (5) The method: the filling mask, pixs, is the image whose peaks
		/// are to be extracted.  The height of a peak is the distance
		/// between the top of the peak and the highest "leak" to the
		/// outside -- think of a sombrero, where the leak occurs
		/// at the highest point on the rim.
		/// (a) Generate a seed, pixd, by subtracting some value, p, from
		/// each pixel in the filling mask, pixs.  The value p is
		/// the 'height' input to this function.
		/// (b) Fill in pixd starting with this seed, clipping by pixs,
		/// in the way described in seedfillGrayLow().  The filling
		/// stops before the peaks in pixs are filled.
		/// For peaks that have a height  is greater  p, pixd is filled to
		/// the level equal to the (top-of-the-peak - p).
		/// For peaks of height  is smaller p, the peak is left unfilled
		/// from its highest saddle point (the leak to the outside).
		/// (c) Subtract the filled seed (pixd) from the filling mask (pixs).
		/// Note that in this procedure, everything is done starting
		/// with the filling mask, pixs.<para/>
		///
		/// (6) For segmentation, the resulting image, pixd, can be thresholded
		/// and used as a seed for another filling operation.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixHDome/*"/>
		///  <param name="pixs">[in] - 8 bpp, filling mask</param>
		///  <param name="height">[in] - of seed below the filling maskhdome must be greater or equal 0</param>
		///  <param name="connectivity">[in] - 4 or 8</param>
		///   <returns>pixd 8 bpp, or NULL on error</returns>
		public static Pix pixHDome(
						Pix pixs,
						int height,
						int connectivity)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			IntPtr _Result = Natives.pixHDome(pixs.Pointer,   height,   connectivity);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// morphapp.c (1359, 1)
		// pixFastTophat(pixs, xsize, ysize, type) as Pix
		// pixFastTophat(PIX *, l_int32, l_int32, l_int32) as PIX *
		///  <summary>
		/// (1) Don't be fooled. This is NOT a tophat.  It is a tophat-like
		/// operation, where the result is similar to what you'd get
		/// if you used an erosion instead of an opening, or a dilation
		/// instead of a closing.<para/>
		///
		/// (2) Instead of opening or closing at full resolution, it does
		/// a fast downscale/minmax operation, then a quick small smoothing
		/// at low res, a replicative expansion of the "background"
		/// to full res, and finally a removal of the background level
		/// from the input image.  The smoothing step may not be important.<para/>
		///
		/// (3) It does not remove noise as well as a tophat, but it is
		/// 5 to 10 times faster.
		/// If you need the preciseness of the tophat, don't use this.<para/>
		///
		/// (4) The L_TOPHAT_WHITE flag emphasizes small bright regions,
		/// whereas the L_TOPHAT_BLACK flag emphasizes small dark regions.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixFastTophat/*"/>
		///  <param name="pixs">[in] - </param>
		///  <param name="xsize">[in] - width of max/min op, smoothing any integer greater or equal 1</param>
		///  <param name="ysize">[in] - height of max/min op, smoothing any integer greater or equal 1</param>
		///  <param name="type">[in] - L_TOPHAT_WHITE: image - min L_TOPHAT_BLACK: max - image</param>
		///   <returns>pixd, or NULL on error</returns>
		public static Pix pixFastTophat(
						Pix pixs,
						int xsize,
						int ysize,
						int type)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			IntPtr _Result = Natives.pixFastTophat(pixs.Pointer,   xsize,   ysize,   type);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// morphapp.c (1421, 1)
		// pixMorphGradient(pixs, hsize, vsize, smoothing) as Pix
		// pixMorphGradient(PIX *, l_int32, l_int32, l_int32) as PIX *
		///  <summary>
		/// pixMorphGradient()
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixMorphGradient/*"/>
		///  <param name="pixs">[in] - </param>
		///  <param name="hsize">[in] - of Sel must be odd origin implicitly in center</param>
		///  <param name="vsize">[in] - ditto</param>
		///  <param name="smoothing">[in] - half-width of convolution smoothing filter. The width is (2  smoothing + 1, so 0 is no-op.</param>
		///   <returns>pixd, or NULL on error</returns>
		public static Pix pixMorphGradient(
						Pix pixs,
						int hsize,
						int vsize,
						int smoothing)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			IntPtr _Result = Natives.pixMorphGradient(pixs.Pointer,   hsize,   vsize,   smoothing);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// morphapp.c (1475, 1)
		// pixaCentroids(pixa) as Pta
		// pixaCentroids(PIXA *) as PTA *
		///  <summary>
		/// (1) An error message is returned if any pix has something other
		/// than 1 bpp or 8 bpp depth, and the centroid from that pix
		/// is saved as (0, 0).
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixaCentroids/*"/>
		///  <param name="pixa">[in] - of components 1 or 8 bpp</param>
		///   <returns>pta of centroids relative to the UL corner of each pix, or NULL on error</returns>
		public static Pta pixaCentroids(
						Pixa pixa)
		{
			if (pixa == null) {throw new ArgumentNullException  ("pixa cannot be Nothing");}

			IntPtr _Result = Natives.pixaCentroids(pixa.Pointer);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pta(_Result);
		}

		// morphapp.c (1527, 1)
		// pixCentroid(pix, centtab, sumtab, pxave, pyave) as int
		// pixCentroid(PIX *, l_int32 *, l_int32 *, l_float32 *, l_float32 *) as l_ok
		///  <summary>
		/// (1) Any table not passed in will be made internally and destroyed
		/// after use.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixCentroid/*"/>
		///  <param name="pix">[in] - 1 or 8 bpp</param>
		///  <param name="centtab">[in][optional] - table for finding centroids can be null</param>
		///  <param name="sumtab">[in][optional] - table for finding pixel sums can be null</param>
		///  <param name="pxave">[out] - coordinates of centroid, relative to the UL corner of the pix</param>
		///  <param name="pyave">[out] - coordinates of centroid, relative to the UL corner of the pix</param>
		///   <returns>0 if OK, 1 on error</returns>
		public static int pixCentroid(
						Pix pix,
						int[] centtab,
						int[] sumtab,
						out Single pxave,
						out Single pyave)
		{
			if (pix == null) {throw new ArgumentNullException  ("pix cannot be Nothing");}

			int _Result = Natives.pixCentroid(pix.Pointer,   centtab,   sumtab, out  pxave, out  pyave);
			return _Result;
		}


	}
}
