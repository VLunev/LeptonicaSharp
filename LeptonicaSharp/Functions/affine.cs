using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LeptonicaSharp
{
	public partial class _All
	{

		// affine.c (280, 1)
		// pixAffineSampledPta(pixs, ptad, ptas, incolor) as Pix
		// pixAffineSampledPta(PIX *, PTA *, PTA *, l_int32) as PIX *
		///  <summary>
		/// (1) Brings in either black or white pixels from the boundary.<para/>
		///
		/// (2) Retains colormap, which you can do for a sampled transform..<para/>
		///
		/// (3) The 3 points must not be collinear.<para/>
		///
		/// (4) The order of the 3 points is arbitrary however, to compare
		/// with the sequential transform they must be in these locations
		/// and in this order: origin, x-axis, y-axis.<para/>
		///
		/// (5) For 1 bpp images, this has much better quality results
		/// than pixAffineSequential(), particularly for text.
		/// It is about 3x slower, but does not require additional
		/// border pixels.  The poor quality of pixAffineSequential()
		/// is due to repeated quantized transforms.  It is strongly
		/// recommended that pixAffineSampled() be used for 1 bpp images.<para/>
		///
		/// (6) For 8 or 32 bpp, much better quality is obtained by the
		/// somewhat slower pixAffinePta().  See that function
		/// for relative timings between sampled and interpolated.<para/>
		///
		/// (7) To repeat, use of the sequential transform,
		/// pixAffineSequential(), for any images, is discouraged.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixAffineSampledPta/*"/>
		///  <param name="pixs">[in] - all depths</param>
		///  <param name="ptad">[in] - 3 pts of final coordinate space</param>
		///  <param name="ptas">[in] - 3 pts of initial coordinate space</param>
		///  <param name="incolor">[in] - L_BRING_IN_WHITE, L_BRING_IN_BLACK</param>
		///   <returns>pixd, or NULL on error</returns>
		public static Pix pixAffineSampledPta(
						Pix pixs,
						Pta ptad,
						Pta ptas,
						int incolor)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (ptad == null) {throw new ArgumentNullException  ("ptad cannot be Nothing");}

			if (ptas == null) {throw new ArgumentNullException  ("ptas cannot be Nothing");}

			IntPtr _Result = Natives.pixAffineSampledPta(pixs.Pointer, ptad.Pointer, ptas.Pointer,   incolor);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// affine.c (330, 1)
		// pixAffineSampled(pixs, vc, incolor) as Pix
		// pixAffineSampled(PIX *, l_float32 *, l_int32) as PIX *
		///  <summary>
		/// (1) Brings in either black or white pixels from the boundary.<para/>
		///
		/// (2) Retains colormap, which you can do for a sampled transform..<para/>
		///
		/// (3) For 8 or 32 bpp, much better quality is obtained by the
		/// somewhat slower pixAffine().  See that function
		/// for relative timings between sampled and interpolated.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixAffineSampled/*"/>
		///  <param name="pixs">[in] - all depths</param>
		///  <param name="vc">[in] - vector of 6 coefficients for affine transformation</param>
		///  <param name="incolor">[in] - L_BRING_IN_WHITE, L_BRING_IN_BLACK</param>
		///   <returns>pixd, or NULL on error</returns>
		public static Pix pixAffineSampled(
						Pix pixs,
						Single[] vc,
						int incolor)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (vc == null) {throw new ArgumentNullException  ("vc cannot be Nothing");}

			IntPtr _Result = Natives.pixAffineSampled(pixs.Pointer,   vc,   incolor);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// affine.c (423, 1)
		// pixAffinePta(pixs, ptad, ptas, incolor) as Pix
		// pixAffinePta(PIX *, PTA *, PTA *, l_int32) as PIX *
		///  <summary>
		/// (1) Brings in either black or white pixels from the boundary<para/>
		///
		/// (2) Removes any existing colormap, if necessary, before transforming
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixAffinePta/*"/>
		///  <param name="pixs">[in] - all depths colormap ok</param>
		///  <param name="ptad">[in] - 3 pts of final coordinate space</param>
		///  <param name="ptas">[in] - 3 pts of initial coordinate space</param>
		///  <param name="incolor">[in] - L_BRING_IN_WHITE, L_BRING_IN_BLACK</param>
		///   <returns>pixd, or NULL on error</returns>
		public static Pix pixAffinePta(
						Pix pixs,
						Pta ptad,
						Pta ptas,
						int incolor)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (ptad == null) {throw new ArgumentNullException  ("ptad cannot be Nothing");}

			if (ptas == null) {throw new ArgumentNullException  ("ptas cannot be Nothing");}

			IntPtr _Result = Natives.pixAffinePta(pixs.Pointer, ptad.Pointer, ptas.Pointer,   incolor);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// affine.c (493, 1)
		// pixAffine(pixs, vc, incolor) as Pix
		// pixAffine(PIX *, l_float32 *, l_int32) as PIX *
		///  <summary>
		/// (1) Brings in either black or white pixels from the boundary<para/>
		///
		/// (2) Removes any existing colormap, if necessary, before transforming
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixAffine/*"/>
		///  <param name="pixs">[in] - all depths colormap ok</param>
		///  <param name="vc">[in] - vector of 6 coefficients for affine transformation</param>
		///  <param name="incolor">[in] - L_BRING_IN_WHITE, L_BRING_IN_BLACK</param>
		///   <returns>pixd, or NULL on error</returns>
		public static Pix pixAffine(
						Pix pixs,
						Single[] vc,
						int incolor)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (vc == null) {throw new ArgumentNullException  ("vc cannot be Nothing");}

			IntPtr _Result = Natives.pixAffine(pixs.Pointer,   vc,   incolor);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// affine.c (549, 1)
		// pixAffinePtaColor(pixs, ptad, ptas, colorval) as Pix
		// pixAffinePtaColor(PIX *, PTA *, PTA *, l_uint32) as PIX *
		///  <summary>
		/// pixAffinePtaColor()
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixAffinePtaColor/*"/>
		///  <param name="pixs">[in] - 32 bpp</param>
		///  <param name="ptad">[in] - 3 pts of final coordinate space</param>
		///  <param name="ptas">[in] - 3 pts of initial coordinate space</param>
		///  <param name="colorval">[in] - e.g., 0 to bring in BLACK, 0xffffff00 for WHITE</param>
		///   <returns>pixd, or NULL on error</returns>
		public static Pix pixAffinePtaColor(
						Pix pixs,
						Pta ptad,
						Pta ptas,
						uint colorval)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (ptad == null) {throw new ArgumentNullException  ("ptad cannot be Nothing");}

			if (ptas == null) {throw new ArgumentNullException  ("ptas cannot be Nothing");}

			IntPtr _Result = Natives.pixAffinePtaColor(pixs.Pointer, ptad.Pointer, ptas.Pointer,   colorval);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// affine.c (590, 1)
		// pixAffineColor(pixs, vc, colorval) as Pix
		// pixAffineColor(PIX *, l_float32 *, l_uint32) as PIX *
		///  <summary>
		/// pixAffineColor()
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixAffineColor/*"/>
		///  <param name="pixs">[in] - 32 bpp</param>
		///  <param name="vc">[in] - vector of 6 coefficients for affine transformation</param>
		///  <param name="colorval">[in] - e.g., 0 to bring in BLACK, 0xffffff00 for WHITE</param>
		///   <returns>pixd, or NULL on error</returns>
		public static Pix pixAffineColor(
						Pix pixs,
						Single[] vc,
						uint colorval)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (vc == null) {throw new ArgumentNullException  ("vc cannot be Nothing");}

			IntPtr _Result = Natives.pixAffineColor(pixs.Pointer,   vc,   colorval);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// affine.c (652, 1)
		// pixAffinePtaGray(pixs, ptad, ptas, grayval) as Pix
		// pixAffinePtaGray(PIX *, PTA *, PTA *, l_uint8) as PIX *
		///  <summary>
		/// pixAffinePtaGray()
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixAffinePtaGray/*"/>
		///  <param name="pixs">[in] - 8 bpp</param>
		///  <param name="ptad">[in] - 3 pts of final coordinate space</param>
		///  <param name="ptas">[in] - 3 pts of initial coordinate space</param>
		///  <param name="grayval">[in] - 0 to bring in BLACK, 255 for WHITE</param>
		///   <returns>pixd, or NULL on error</returns>
		public static Pix pixAffinePtaGray(
						Pix pixs,
						Pta ptad,
						Pta ptas,
						byte grayval)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (ptad == null) {throw new ArgumentNullException  ("ptad cannot be Nothing");}

			if (ptas == null) {throw new ArgumentNullException  ("ptas cannot be Nothing");}

			if ((new List<int> {8}).Contains ((int)pixs.d) == false) { throw new ArgumentException ("8 bpp"); }
			IntPtr _Result = Natives.pixAffinePtaGray(pixs.Pointer, ptad.Pointer, ptas.Pointer,   grayval);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// affine.c (694, 1)
		// pixAffineGray(pixs, vc, grayval) as Pix
		// pixAffineGray(PIX *, l_float32 *, l_uint8) as PIX *
		///  <summary>
		/// pixAffineGray()
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixAffineGray/*"/>
		///  <param name="pixs">[in] - 8 bpp</param>
		///  <param name="vc">[in] - vector of 6 coefficients for affine transformation</param>
		///  <param name="grayval">[in] - 0 to bring in BLACK, 255 for WHITE</param>
		///   <returns>pixd, or NULL on error</returns>
		public static Pix pixAffineGray(
						Pix pixs,
						Single[] vc,
						byte grayval)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (vc == null) {throw new ArgumentNullException  ("vc cannot be Nothing");}

			if ((new List<int> {8}).Contains ((int)pixs.d) == false) { throw new ArgumentException ("8 bpp"); }
			IntPtr _Result = Natives.pixAffineGray(pixs.Pointer,   vc,   grayval);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// affine.c (782, 1)
		// pixAffinePtaWithAlpha(pixs, ptad, ptas, pixg, fract, border) as Pix
		// pixAffinePtaWithAlpha(PIX *, PTA *, PTA *, PIX *, l_float32, l_int32) as PIX *
		///  <summary>
		/// (1) The alpha channel is transformed separately from pixs,
		/// and aligns with it, being fully transparent outside the
		/// boundary of the transformed pixs.  For pixels that are fully
		/// transparent, a blending function like pixBlendWithGrayMask()
		/// will give zero weight to corresponding pixels in pixs.<para/>
		///
		/// (2) If pixg is NULL, it is generated as an alpha layer that is
		/// partially opaque, using %fract.  Otherwise, it is cropped
		/// to pixs if required and %fract is ignored.  The alpha channel
		/// in pixs is never used.<para/>
		///
		/// (3) Colormaps are removed.<para/>
		///
		/// (4) When pixs is transformed, it doesn't matter what color is brought
		/// in because the alpha channel will be transparent (0) there.<para/>
		///
		/// (5) To avoid losing source pixels in the destination, it may be
		/// necessary to add a border to the source pix before doing
		/// the affine transformation.  This can be any non-negative number.<para/>
		///
		/// (6) The input %ptad and %ptas are in a coordinate space before
		/// the border is added.  Internally, we compensate for this
		/// before doing the affine transform on the image after the border
		/// is added.<para/>
		///
		/// (7) The default setting for the border values in the alpha channel
		/// is 0 (transparent) for the outermost ring of pixels and
		/// (0.5  fract  255) for the second ring.  When blended over
		/// a second image, this
		/// (a) shrinks the visible image to make a clean overlap edge
		/// with an image below, and
		/// (b) softens the edges by weakening the aliasing there.
		/// Use l_setAlphaMaskBorder() to change these values.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixAffinePtaWithAlpha/*"/>
		///  <param name="pixs">[in] - 32 bpp rgb</param>
		///  <param name="ptad">[in] - 3 pts of final coordinate space</param>
		///  <param name="ptas">[in] - 3 pts of initial coordinate space</param>
		///  <param name="pixg">[in][optional] - 8 bpp, can be null</param>
		///  <param name="fract">[in] - between 0.0 and 1.0, with 0.0 fully transparent and 1.0 fully opaque</param>
		///  <param name="border">[in] - of pixels added to capture transformed source pixels</param>
		///   <returns>pixd, or NULL on error</returns>
		public static Pix pixAffinePtaWithAlpha(
						Pix pixs,
						Pta ptad,
						Pta ptas,
						Pix pixg,
						Single fract,
						int border)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (ptad == null) {throw new ArgumentNullException  ("ptad cannot be Nothing");}

			if (ptas == null) {throw new ArgumentNullException  ("ptas cannot be Nothing");}

			if ((new List<int> {32}).Contains ((int)pixs.d) == false) { throw new ArgumentException ("32 bpp rgb"); }
			IntPtr pixgPtr = IntPtr.Zero; 	if (pixg != null) {pixgPtr = pixg.Pointer;}
			IntPtr _Result = Natives.pixAffinePtaWithAlpha(pixs.Pointer, ptad.Pointer, ptas.Pointer, pixgPtr,   fract,   border);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// affine.c (931, 1)
		// getAffineXformCoeffs(ptas, ptad, pvc) as int
		// getAffineXformCoeffs(PTA *, PTA *, l_float32 **) as l_ok
		///  <summary>
		/// We have a set of six equations, describing the affine
		/// transformation that takes 3 points ptas into 3 other
		/// points ptad.  These equations are:
		/// x1' = c[0]x1 + c[1]y1 + c[2]
		/// y1' = c[3]x1 + c[4]y1 + c[5]
		/// x2' = c[0]x2 + c[1]y2 + c[2]
		/// y2' = c[3]x2 + c[4]y2 + c[5]
		/// x3' = c[0]x3 + c[1]y3 + c[2]
		/// y3' = c[3]x3 + c[4]y3 + c[5]
		/// This can be represented as
		/// AC = B
		/// where B and C are column vectors
		/// B = [ x1' y1' x2' y2' x3' y3' ]
		/// C = [ c[0] c[1] c[2] c[3] c[4] c[5] c[6] ]
		/// and A is the 6x6 matrix
		/// x1 y1 1 0  0  0
		/// 0  0 0 x1 y1 1
		/// x2 y2 1 0  0  0
		/// 0  0 0 x2 y2 1
		/// x3 y3 1 0  0  0
		/// 0  0 0 x3 y3 1
		/// These six equations are solved here for the coefficients C.
		/// These six coefficients can then be used to find the dest
		/// point x',y') corresponding to any src point (x,y, according
		/// to the equations
		/// x' = c[0]x + c[1]y + c[2]
		/// y' = c[3]x + c[4]y + c[5]
		/// that are implemented in affineXformPt.
		/// !!!!!!!!!!!!!!!!!! Very important !!!!!!!!!!!!!!!!!!!!!!
		/// When the affine transform is composed from a set of simple
		/// operations such as translation, scaling and rotation,
		/// it is built in a form to convert from the un-transformed src
		/// point to the transformed dest point.  However, when an
		/// affine transform is used on images, it is used in an inverted
		/// way: it converts from the transformed dest point to the
		/// un-transformed src point.  So, for example, if you transform
		/// a boxa using transform A, to transform an image in the same
		/// way you must use the inverse of A.
		/// For example, if you transform a boxa with a 3x3 affine matrix
		/// 'mat', the analogous image transformation must use 'matinv':
		/// \code
		/// boxad = boxaAffineTransform(boxas, mat)
		/// affineInvertXform(mat, [and]matinv)
		/// pixd = pixAffine(pixs, matinv, L_BRING_IN_WHITE)
		/// \endcode
		/// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/getAffineXformCoeffs/*"/>
		///  <param name="ptas">[in] - source 3 points unprimed</param>
		///  <param name="ptad">[in] - transformed 3 points primed</param>
		///  <param name="pvc">[out] - vector of coefficients of transform</param>
		///   <returns>0 if OK 1 on error</returns>
		public static int getAffineXformCoeffs(
						Pta ptas,
						Pta ptad,
						out List<Single[]> pvc)
		{
			if (ptas == null) {throw new ArgumentNullException  ("ptas cannot be Nothing");}

			if (ptad == null) {throw new ArgumentNullException  ("ptad cannot be Nothing");}

			IntPtr pvcPtr = IntPtr.Zero;
			int _Result = Natives.getAffineXformCoeffs(ptas.Pointer, ptad.Pointer, out  pvcPtr);
			if (pvcPtr == null) {pvc = null;} else { pvc = null; };

			return _Result;
		}

		// affine.c (1024, 1)
		// affineInvertXform(vc, pvci) as int
		// affineInvertXform(l_float32 *, l_float32 **) as l_ok
		///  <summary>
		/// (1) The 6 affine transform coefficients are the first
		/// two rows of a 3x3 matrix where the last row has
		/// only a 1 in the third column.  We invert this
		/// using gaussjordan(), and select the first 2 rows
		/// as the coefficients of the inverse affine transform.<para/>
		///
		/// (2) Alternatively, we can find the inverse transform
		/// coefficients by inverting the 2x2 submatrix,
		/// and treating the top 2 coefficients in the 3rd column as
		/// a RHS vector for that 2x2 submatrix.  Then the
		/// 6 inverted transform coefficients are composed of
		/// the inverted 2x2 submatrix and the negative of the
		/// transformed RHS vector.  Why is this so?  We have
		/// Y = AX + R  (2 equations in 6 unknowns)
		/// Then
		/// X = A'Y - A'R
		/// Gauss-jordan solves
		/// AF = R
		/// and puts the solution for F, which is A'R,
		/// into the input R vector.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/affineInvertXform/*"/>
		///  <param name="vc">[in] - vector of 6 coefficients</param>
		///  <param name="pvci">[out] - inverted transform</param>
		///   <returns>0 if OK 1 on error</returns>
		public static int affineInvertXform(
						Single[] vc,
						out List<Single[]> pvci)
		{
			if (vc == null) {throw new ArgumentNullException  ("vc cannot be Nothing");}

			IntPtr pvciPtr = IntPtr.Zero;
			int _Result = Natives.affineInvertXform(  vc, out  pvciPtr);
			if (pvciPtr == null) {pvci = null;} else { pvci = null; };

			return _Result;
		}

		// affine.c (1107, 1)
		// affineXformSampledPt(vc, x, y, pxp, pyp) as int
		// affineXformSampledPt(l_float32 *, l_int32, l_int32, l_int32 *, l_int32 *) as l_ok
		///  <summary>
		/// (1) This finds the nearest pixel coordinates of the transformed point.<para/>
		///
		/// (2) It does not check ptrs for returned data!
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/affineXformSampledPt/*"/>
		///  <param name="vc">[in] - vector of 6 coefficients</param>
		///  <param name="x">[in] - initial point</param>
		///  <param name="y">[in] - initial point</param>
		///  <param name="pxp">[out] - transformed point</param>
		///  <param name="pyp">[out] - transformed point</param>
		///   <returns>0 if OK 1 on error</returns>
		public static int affineXformSampledPt(
						Single[] vc,
						int x,
						int y,
						out int pxp,
						out int pyp)
		{
			if (vc == null) {throw new ArgumentNullException  ("vc cannot be Nothing");}

			int _Result = Natives.affineXformSampledPt(  vc,   x,   y, out  pxp, out  pyp);
			return _Result;
		}

		// affine.c (1139, 1)
		// affineXformPt(vc, x, y, pxp, pyp) as int
		// affineXformPt(l_float32 *, l_int32, l_int32, l_float32 *, l_float32 *) as l_ok
		///  <summary>
		/// (1) This computes the floating point location of the transformed point.<para/>
		///
		/// (2) It does not check ptrs for returned data!
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/affineXformPt/*"/>
		///  <param name="vc">[in] - vector of 6 coefficients</param>
		///  <param name="x">[in] - initial point</param>
		///  <param name="y">[in] - initial point</param>
		///  <param name="pxp">[out] - transformed point</param>
		///  <param name="pyp">[out] - transformed point</param>
		///   <returns>0 if OK 1 on error</returns>
		public static int affineXformPt(
						Single[] vc,
						int x,
						int y,
						out Single pxp,
						out Single pyp)
		{
			if (vc == null) {throw new ArgumentNullException  ("vc cannot be Nothing");}

			int _Result = Natives.affineXformPt(  vc,   x,   y, out  pxp, out  pyp);
			return _Result;
		}

		// affine.c (1180, 1)
		// linearInterpolatePixelColor(datas, wpls, w, h, x, y, colorval, pval) as int
		// linearInterpolatePixelColor(l_uint32 *, l_int32, l_int32, l_int32, l_float32, l_float32, l_uint32, l_uint32 *) as l_ok
		///  <summary>
		/// (1) This is a standard linear interpolation function.  It is
		/// equivalent to area weighting on each component, and
		/// avoids "jaggies" when rendering sharp edges.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/linearInterpolatePixelColor/*"/>
		///  <param name="datas">[in] - ptr to beginning of image data</param>
		///  <param name="wpls">[in] - 32-bit word/line for this data array</param>
		///  <param name="w">[in] - of image</param>
		///  <param name="h">[in] - of image</param>
		///  <param name="x">[in] - floating pt location for evaluation</param>
		///  <param name="y">[in] - floating pt location for evaluation</param>
		///  <param name="colorval">[in] - color brought in from the outside when the input x,y location is outside the image in 0xrrggbb00 format)</param>
		///  <param name="pval">[out] - interpolated color value</param>
		///   <returns>0 if OK, 1 on error</returns>
		public static int linearInterpolatePixelColor(
						Byte[] datas,
						int wpls,
						int w,
						int h,
						Single x,
						Single y,
						uint colorval,
						out uint pval)
		{
			if (datas == null) {throw new ArgumentNullException  ("datas cannot be Nothing");}

			IntPtr datasPtr = 	Marshal.AllocHGlobal(datas.Length);
			Marshal.Copy(datas, 0, datasPtr, datas.Length);
			int _Result = Natives.linearInterpolatePixelColor(  datasPtr,   wpls,   w,   h,   x,   y,   colorval, out  pval);
			Marshal.FreeHGlobal(datasPtr);
			return _Result;
		}

		// affine.c (1267, 1)
		// linearInterpolatePixelGray(datas, wpls, w, h, x, y, grayval, pval) as int
		// linearInterpolatePixelGray(l_uint32 *, l_int32, l_int32, l_int32, l_float32, l_float32, l_int32, l_int32 *) as l_ok
		///  <summary>
		/// (1) This is a standard linear interpolation function.  It is
		/// equivalent to area weighting on each component, and
		/// avoids "jaggies" when rendering sharp edges.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/linearInterpolatePixelGray/*"/>
		///  <param name="datas">[in] - ptr to beginning of image data</param>
		///  <param name="wpls">[in] - 32-bit word/line for this data array</param>
		///  <param name="w">[in] - of image</param>
		///  <param name="h">[in] - of image</param>
		///  <param name="x">[in] - floating pt location for evaluation</param>
		///  <param name="y">[in] - floating pt location for evaluation</param>
		///  <param name="grayval">[in] - color brought in from the outside when the input x,y location is outside the image</param>
		///  <param name="pval">[out] - interpolated gray value</param>
		///   <returns>0 if OK, 1 on error</returns>
		public static int linearInterpolatePixelGray(
						Byte[] datas,
						int wpls,
						int w,
						int h,
						Single x,
						Single y,
						int grayval,
						out int pval)
		{
			if (datas == null) {throw new ArgumentNullException  ("datas cannot be Nothing");}

			IntPtr datasPtr = 	Marshal.AllocHGlobal(datas.Length);
			Marshal.Copy(datas, 0, datasPtr, datas.Length);
			int _Result = Natives.linearInterpolatePixelGray(  datasPtr,   wpls,   w,   h,   x,   y,   grayval, out  pval);
			Marshal.FreeHGlobal(datasPtr);
			return _Result;
		}

		// affine.c (1346, 1)
		// gaussjordan(a, b, n) as int
		// gaussjordan(l_float32 **, l_float32 *, l_int32) as l_int32
		///  <summary>
		/// (1) There are two side-effects:
		/// The matrix a is transformed to its inverse A
		/// The rhs vector b is transformed to the solution x
		/// of the linear equation ax = b<para/>
		///
		/// (2) The inverse A can then be used to solve the same equation with
		/// different rhs vectors c by multiplication: x = Ac<para/>
		///
		/// (3) Adapted from "Numerical Recipes in C, Second Edition", 1992,
		/// pp. 36-41 (gauss-jordan elimination)
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/gaussjordan/*"/>
		///  <param name="a">[in] - n x n matrix</param>
		///  <param name="b">[in] - n x 1 right-hand side column vector</param>
		///  <param name="n">[in] - dimension</param>
		///   <returns>0 if ok, 1 on error</returns>
		public static int gaussjordan(
						List<Single[]> a,
						Single[] b,
						int n)
		{
			if (a == null) {throw new ArgumentNullException  ("a cannot be Nothing");}

			if (b == null) {throw new ArgumentNullException  ("b cannot be Nothing");}

			IntPtr aPtr = Marshal.AllocHGlobal(a.Count);
			int _Result = Natives.gaussjordan(  aPtr,   b,   n);
			return _Result;
		}

		// affine.c (1470, 1)
		// pixAffineSequential(pixs, ptad, ptas, bw, bh) as Pix
		// pixAffineSequential(PIX *, PTA *, PTA *, l_int32, l_int32) as PIX *
		///  <summary>
		/// (1) The 3 pts must not be collinear.<para/>
		///
		/// (2) The 3 pts must be given in this order:
		/// ~ origin
		/// ~ a location along the x-axis
		/// ~ a location along the y-axis.<para/>
		///
		/// (3) You must guess how much border must be added so that no
		/// pixels are lost in the transformations from src to
		/// dest coordinate space.  (This can be calculated but it
		/// is a lot of work!)  For coordinate spaces that are nearly
		/// at right angles, on a 300 ppi scanned page, the addition
		/// of 1000 pixels on each side is usually sufficient.<para/>
		///
		/// (4) This is here for pedagogical reasons.  It is about 3x faster
		/// on 1 bpp images than pixAffineSampled(), but the results
		/// on text are much inferior.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixAffineSequential/*"/>
		///  <param name="pixs">[in] - </param>
		///  <param name="ptad">[in] - 3 pts of final coordinate space</param>
		///  <param name="ptas">[in] - 3 pts of initial coordinate space</param>
		///  <param name="bw">[in] - pixels of additional border width during computation</param>
		///  <param name="bh">[in] - pixels of additional border height during computation</param>
		///   <returns>pixd, or NULL on error</returns>
		public static Pix pixAffineSequential(
						Pix pixs,
						Pta ptad,
						Pta ptas,
						int bw,
						int bh)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (ptad == null) {throw new ArgumentNullException  ("ptad cannot be Nothing");}

			if (ptas == null) {throw new ArgumentNullException  ("ptas cannot be Nothing");}

			IntPtr _Result = Natives.pixAffineSequential(pixs.Pointer, ptad.Pointer, ptas.Pointer,   bw,   bh);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}


	}
}
