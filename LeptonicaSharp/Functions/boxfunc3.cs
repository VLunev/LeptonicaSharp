using System;
using System.IO;

using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LeptonicaSharp
{
	public partial class _All
	{

		// boxfunc3.c (94, 1)
		// pixMaskConnComp(pixs, connectivity, pboxa) as Pix
		// pixMaskConnComp(PIX *, l_int32, BOXA **) as PIX *
		///  <summary>
		/// (1) This generates a mask image with ON pixels over the
		/// b.b. of the c.c. in pixs.  If there are no ON pixels in pixs,
		/// pixd will also have no ON pixels.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixMaskConnComp/*"/>
		///  <param name="pixs">[in] - 1 bpp</param>
		///  <param name="connectivity">[in] - 4 or 8</param>
		///  <param name="pboxa">[out][optional] - bounding boxes of c.c.</param>
		///   <returns>pixd 1 bpp mask over the c.c., or NULL on error</returns>
		public static Pix pixMaskConnComp(
						Pix pixs,
						int connectivity,
						out Boxa pboxa)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if ((new List<int> {1}).Contains ((int)pixs.d) == false) { throw new ArgumentException ("1 bpp"); }
			IntPtr pboxaPtr = IntPtr.Zero;
			IntPtr _Result = Natives.pixMaskConnComp(pixs.Pointer,   connectivity, out pboxaPtr);
			if (pboxaPtr == IntPtr.Zero) {pboxa = null;} else { pboxa = new Boxa(pboxaPtr); };

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// boxfunc3.c (148, 1)
		// pixMaskBoxa(pixd, pixs, boxa, op) as Pix
		// pixMaskBoxa(PIX *, PIX *, BOXA *, l_int32) as PIX *
		///  <summary>
		/// (1) This can be used with:
		/// pixd = NULL  (makes a new pixd)
		/// pixd = pixs  (in-place)<para/>
		///
		/// (2) If pixd == NULL, this first makes a copy of pixs, and then
		/// bit-twiddles over the boxes.  Otherwise, it operates directly
		/// on pixs.<para/>
		///
		/// (3) This simple function is typically used with 1 bpp images.
		/// It uses the 1-image rasterop function, rasteropUniLow(),
		/// to set, clear or flip the pixels in pixd.<para/>
		///
		/// (4) If you want to generate a 1 bpp mask of ON pixels from the boxes
		/// in a Boxa, in a pix of size (w,h):
		/// pix = pixCreate(w, h, 1)
		/// pixMaskBoxa(pix, pix, boxa, L_SET_PIXELS)
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixMaskBoxa/*"/>
		///  <param name="pixd">[in][optional] - may be NULL</param>
		///  <param name="pixs">[in] - any depth not cmapped</param>
		///  <param name="boxa">[in] - of boxes, to paint</param>
		///  <param name="op">[in] - L_SET_PIXELS, L_CLEAR_PIXELS, L_FLIP_PIXELS</param>
		///   <returns>pixd with masking op over the boxes, or NULL on error</returns>
		public static Pix pixMaskBoxa(
						Pix pixd,
						Pix pixs,
						Boxa boxa,
						int op)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (boxa == null) {throw new ArgumentNullException  ("boxa cannot be Nothing");}

			IntPtr pixdPtr = IntPtr.Zero; 	if (pixd != null) {pixdPtr = pixd.Pointer;}
			IntPtr _Result = Natives.pixMaskBoxa(pixdPtr, pixs.Pointer, boxa.Pointer,   op);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// boxfunc3.c (217, 1)
		// pixPaintBoxa(pixs, boxa, val) as Pix
		// pixPaintBoxa(PIX *, BOXA *, l_uint32) as PIX *
		///  <summary>
		/// (1) If pixs is 1 bpp or is colormapped, it is converted to 8 bpp
		/// and the boxa is painted using a colormap otherwise,
		/// it is converted to 32 bpp rgb.<para/>
		///
		/// (2) There are several ways to display a box on an image:
		/// Paint it as a solid color
		/// Draw the outline
		/// Blend the outline or region with the existing image
		/// We provide painting and drawing here blending is in blend.c.
		/// When painting or drawing, the result can be either a
		/// cmapped image or an rgb image.  The dest will be cmapped
		/// if the src is either 1 bpp or has a cmap that is not full.
		/// To force RGB output, use pixConvertTo8(pixs, FALSE)
		/// before calling any of these paint and draw functions.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixPaintBoxa/*"/>
		///  <param name="pixs">[in] - any depth, can be cmapped</param>
		///  <param name="boxa">[in] - of boxes, to paint</param>
		///  <param name="val">[in] - rgba color to paint</param>
		///   <returns>pixd with painted boxes, or NULL on error</returns>
		public static Pix pixPaintBoxa(
						Pix pixs,
						Boxa boxa,
						uint val)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (boxa == null) {throw new ArgumentNullException  ("boxa cannot be Nothing");}

			IntPtr _Result = Natives.pixPaintBoxa(pixs.Pointer, boxa.Pointer,   val);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// boxfunc3.c (283, 1)
		// pixSetBlackOrWhiteBoxa(pixs, boxa, op) as Pix
		// pixSetBlackOrWhiteBoxa(PIX *, BOXA *, l_int32) as PIX *
		///  <summary>
		/// pixSetBlackOrWhiteBoxa()
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixSetBlackOrWhiteBoxa/*"/>
		///  <param name="pixs">[in] - any depth, can be cmapped</param>
		///  <param name="boxa">[in][optional] - of boxes, to clear or set</param>
		///  <param name="op">[in] - L_SET_BLACK, L_SET_WHITE</param>
		///   <returns>pixd with boxes filled with white or black, or NULL on error</returns>
		public static Pix pixSetBlackOrWhiteBoxa(
						Pix pixs,
						Boxa boxa,
						int op)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			IntPtr boxaPtr = IntPtr.Zero; 	if (boxa != null) {boxaPtr = boxa.Pointer;}
			IntPtr _Result = Natives.pixSetBlackOrWhiteBoxa(pixs.Pointer, boxaPtr,   op);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// boxfunc3.c (364, 1)
		// pixPaintBoxaRandom(pixs, boxa) as Pix
		// pixPaintBoxaRandom(PIX *, BOXA *) as PIX *
		///  <summary>
		/// (1) If pixs is 1 bpp, we paint the boxa using a colormap
		/// otherwise, we convert to 32 bpp.<para/>
		///
		/// (2) We use up to 254 different colors for painting the regions.<para/>
		///
		/// (3) If boxes overlap, the later ones paint over earlier ones.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixPaintBoxaRandom/*"/>
		///  <param name="pixs">[in] - any depth, can be cmapped</param>
		///  <param name="boxa">[in] - of boxes, to paint</param>
		///   <returns>pixd with painted boxes, or NULL on error</returns>
		public static Pix pixPaintBoxaRandom(
						Pix pixs,
						Boxa boxa)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (boxa == null) {throw new ArgumentNullException  ("boxa cannot be Nothing");}

			IntPtr _Result = Natives.pixPaintBoxaRandom(pixs.Pointer, boxa.Pointer);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// boxfunc3.c (435, 1)
		// pixBlendBoxaRandom(pixs, boxa, fract) as Pix
		// pixBlendBoxaRandom(PIX *, BOXA *, l_float32) as PIX *
		///  <summary>
		/// (1) pixs is converted to 32 bpp.<para/>
		///
		/// (2) This differs from pixPaintBoxaRandom(), in that the
		/// colors here are blended with the color of pixs.<para/>
		///
		/// (3) We use up to 254 different colors for painting the regions.<para/>
		///
		/// (4) If boxes overlap, the final color depends only on the last
		/// rect that is used.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixBlendBoxaRandom/*"/>
		///  <param name="pixs">[in] - any depth can be cmapped</param>
		///  <param name="boxa">[in] - of boxes, to blend/paint</param>
		///  <param name="fract">[in] - of box color to use</param>
		///   <returns>pixd 32 bpp, with blend/painted boxes, or NULL on error</returns>
		public static Pix pixBlendBoxaRandom(
						Pix pixs,
						Boxa boxa,
						Single fract)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (boxa == null) {throw new ArgumentNullException  ("boxa cannot be Nothing");}

			IntPtr _Result = Natives.pixBlendBoxaRandom(pixs.Pointer, boxa.Pointer,   fract);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// boxfunc3.c (496, 1)
		// pixDrawBoxa(pixs, boxa, width, val) as Pix
		// pixDrawBoxa(PIX *, BOXA *, l_int32, l_uint32) as PIX *
		///  <summary>
		/// (1) If pixs is 1 bpp or is colormapped, it is converted to 8 bpp
		/// and the boxa is drawn using a colormap otherwise,
		/// it is converted to 32 bpp rgb.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixDrawBoxa/*"/>
		///  <param name="pixs">[in] - any depth can be cmapped</param>
		///  <param name="boxa">[in] - of boxes, to draw</param>
		///  <param name="width">[in] - of lines</param>
		///  <param name="val">[in] - rgba color to draw</param>
		///   <returns>pixd with outlines of boxes added, or NULL on error</returns>
		public static Pix pixDrawBoxa(
						Pix pixs,
						Boxa boxa,
						int width,
						uint val)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (boxa == null) {throw new ArgumentNullException  ("boxa cannot be Nothing");}

			IntPtr _Result = Natives.pixDrawBoxa(pixs.Pointer, boxa.Pointer,   width,   val);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// boxfunc3.c (560, 1)
		// pixDrawBoxaRandom(pixs, boxa, width) as Pix
		// pixDrawBoxaRandom(PIX *, BOXA *, l_int32) as PIX *
		///  <summary>
		/// (1) If pixs is 1 bpp, we draw the boxa using a colormap
		/// otherwise, we convert to 32 bpp.<para/>
		///
		/// (2) We use up to 254 different colors for drawing the boxes.<para/>
		///
		/// (3) If boxes overlap, the later ones draw over earlier ones.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixDrawBoxaRandom/*"/>
		///  <param name="pixs">[in] - any depth, can be cmapped</param>
		///  <param name="boxa">[in] - of boxes, to draw</param>
		///  <param name="width">[in] - thickness of line</param>
		///   <returns>pixd with box outlines drawn, or NULL on error</returns>
		public static Pix pixDrawBoxaRandom(
						Pix pixs,
						Boxa boxa,
						int width)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (boxa == null) {throw new ArgumentNullException  ("boxa cannot be Nothing");}

			IntPtr _Result = Natives.pixDrawBoxaRandom(pixs.Pointer, boxa.Pointer,   width);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// boxfunc3.c (634, 1)
		// boxaaDisplay(pixs, baa, linewba, linewb, colorba, colorb, w, h) as Pix
		// boxaaDisplay(PIX *, BOXAA *, l_int32, l_int32, l_uint32, l_uint32, l_int32, l_int32) as PIX *
		///  <summary>
		/// (1) If %pixs exists, this renders the boxes over an 8 bpp version
		/// of it.  Otherwise, it renders the boxes over an empty image
		/// with a white background.<para/>
		///
		/// (2) If %pixs exists, the dimensions of %pixd are the same,
		/// and input values of %w and %h are ignored.
		/// If %pixs is NULL, the dimensions of %pixd are determined by
		/// - %w and %h if both are  is greater  0, or
		/// - the minimum size required using all boxes in %baa.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/boxaaDisplay/*"/>
		///  <param name="pixs">[in][optional] - 1 bpp</param>
		///  <param name="baa">[in] - boxaa, typically from a 2d sort</param>
		///  <param name="linewba">[in] - line width to display outline of each boxa</param>
		///  <param name="linewb">[in] - line width to display outline of each box</param>
		///  <param name="colorba">[in] - color to display boxa</param>
		///  <param name="colorb">[in] - color to display box</param>
		///  <param name="w">[in] - width of outupt pix use 0 if determined by %pixs or %baa</param>
		///  <param name="h">[in] - height of outupt pix use 0 if determined by %pixs or %baa</param>
		///   <returns>0 if OK, 1 on error</returns>
		public static Pix boxaaDisplay(
						Pix pixs,
						Boxaa baa,
						int linewba,
						int linewb,
						uint colorba,
						uint colorb,
						int w,
						int h)
		{
			if (baa == null) {throw new ArgumentNullException  ("baa cannot be Nothing");}

			if ((new List<int> {1}).Contains ((int)pixs.d) == false) { throw new ArgumentException ("1 bpp"); }
			IntPtr pixsPtr = IntPtr.Zero; 	if (pixs != null) {pixsPtr = pixs.Pointer;}
			IntPtr _Result = Natives.boxaaDisplay(pixsPtr, baa.Pointer,   linewba,   linewb,   colorba,   colorb,   w,   h);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}

		// boxfunc3.c (716, 1)
		// pixaDisplayBoxaa(pixas, baa, colorflag, width) as Pixa
		// pixaDisplayBoxaa(PIXA *, BOXAA *, l_int32, l_int32) as PIXA *
		///  <summary>
		/// (1) All pix in %pixas that are not rgb are converted to rgb.<para/>
		///
		/// (2) Each boxa in %baa contains boxes that will be drawn on
		/// the corresponding pix in %pixas.<para/>
		///
		/// (3) The color of the boxes drawn on each pix are selected with
		/// %colorflag:
		/// For red, green or blue: use L_DRAW_RED, etc.
		/// For sequential r, g, b: use L_DRAW_RGB
		/// For random colors: use L_DRAW_RANDOM
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixaDisplayBoxaa/*"/>
		///  <param name="pixas">[in] - any depth, can be cmapped</param>
		///  <param name="baa">[in] - boxes to draw on input pixa</param>
		///  <param name="colorflag">[in] - (L_DRAW_RED, L_DRAW_GREEN, etc)</param>
		///  <param name="width">[in] - thickness of lines</param>
		///   <returns>pixa with box outlines drawn on each pix, or NULL on error</returns>
		public static Pixa pixaDisplayBoxaa(
						Pixa pixas,
						Boxaa baa,
						int colorflag,
						int width)
		{
			if (pixas == null) {throw new ArgumentNullException  ("pixas cannot be Nothing");}

			if (baa == null) {throw new ArgumentNullException  ("baa cannot be Nothing");}

			IntPtr _Result = Natives.pixaDisplayBoxaa(pixas.Pointer, baa.Pointer,   colorflag,   width);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pixa(_Result);
		}

		// boxfunc3.c (835, 1)
		// pixSplitIntoBoxa(pixs, minsum, skipdist, delta, maxbg, maxcomps, remainder) as Boxa
		// pixSplitIntoBoxa(PIX *, l_int32, l_int32, l_int32, l_int32, l_int32, l_int32) as BOXA *
		///  <summary>
		/// (1) This generates a boxa of rectangles that covers
		/// the fg of a mask.  For each 8-connected component in pixs,
		/// it does a greedy partitioning, choosing the largest
		/// rectangle found from each of the four directions at each iter.
		/// See pixSplitComponentIntoBoxa() for details.<para/>
		///
		/// (2) The input parameters give some flexibility for boundary
		/// noise.  The resulting set of rectangles may cover some
		/// bg pixels.<para/>
		///
		/// (3) This should be used when there are a small number of
		/// mask components, each of which has sides that are close
		/// to horizontal and vertical.  The input parameters %delta
		/// and %maxbg determine whether or not holes in the mask are covered.<para/>
		///
		/// (4) The parameter %maxcomps gives the maximum number of allowed
		/// rectangles extracted from any single connected component.
		/// Use 0 if no limit is to be applied.<para/>
		///
		/// (5) The flag %remainder specifies whether we take a final bounding
		/// box for anything left after the maximum number of allowed
		/// rectangle is extracted.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixSplitIntoBoxa/*"/>
		///  <param name="pixs">[in] - 1 bpp</param>
		///  <param name="minsum">[in] - minimum pixels to trigger propagation</param>
		///  <param name="skipdist">[in] - distance before computing sum for propagation</param>
		///  <param name="delta">[in] - difference required to stop propagation</param>
		///  <param name="maxbg">[in] - maximum number of allowed bg pixels in ref scan</param>
		///  <param name="maxcomps">[in] - use 0 for unlimited number of subdivided components</param>
		///  <param name="remainder">[in] - set to 1 to get b.b. of remaining stuff</param>
		///   <returns>boxa of rectangles covering the fg of pixs, or NULL on error</returns>
		public static Boxa pixSplitIntoBoxa(
						Pix pixs,
						int minsum,
						int skipdist,
						int delta,
						int maxbg,
						int maxcomps,
						int remainder)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if ((new List<int> {1}).Contains ((int)pixs.d) == false) { throw new ArgumentException ("1 bpp"); }
			IntPtr _Result = Natives.pixSplitIntoBoxa(pixs.Pointer,   minsum,   skipdist,   delta,   maxbg,   maxcomps,   remainder);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Boxa(_Result);
		}

		// boxfunc3.c (944, 1)
		// pixSplitComponentIntoBoxa(pix, box, minsum, skipdist, delta, maxbg, maxcomps, remainder) as Boxa
		// pixSplitComponentIntoBoxa(PIX *, BOX *, l_int32, l_int32, l_int32, l_int32, l_int32, l_int32) as BOXA *
		///  <summary>
		/// (1) This generates a boxa of rectangles that covers
		/// the fg of a mask.  It does so by a greedy partitioning of
		/// the mask, choosing the largest rectangle found from
		/// each of the four directions at each step.<para/>
		///
		/// (2) The input parameters give some flexibility for boundary
		/// noise.  The resulting set of rectangles must cover all
		/// the fg pixels and, in addition, may cover some bg pixels.
		/// Using small input parameters on a noiseless mask (i.e., one
		/// that has only large vertical and horizontal edges) will
		/// result in a proper covering of only the fg pixels of the mask.<para/>
		///
		/// (3) The input is assumed to be a single connected component, that
		/// may have holes.  From each side, sweep inward, counting
		/// the pixels.  If the count becomes greater than %minsum,
		/// and we have moved forward a further amount %skipdist,
		/// record that count ('countref'), but don't accept if the scan
		/// contains more than %maxbg bg pixels.  Continue the scan
		/// until we reach a count that differs from countref by at
		/// least %delta, at which point the propagation stops.  The box
		/// swept out gets a score, which is the sum of fg pixels
		/// minus a penalty.  The penalty is the number of bg pixels
		/// in the box.  This is done from all four sides, and the
		/// side with the largest score is saved as a rectangle.
		/// The process repeats until there is either no rectangle
		/// left, or there is one that can't be captured from any
		/// direction.  For the latter case, we simply accept the
		/// last rectangle.<para/>
		///
		/// (4) The input box is only used to specify the location of
		/// the UL corner of pix, with respect to an origin that
		/// typically represents the UL corner of an underlying image,
		/// of which pix is one component.  If %box is null,
		/// the UL corner is taken to be (0, 0).<para/>
		///
		/// (5) The parameter %maxcomps gives the maximum number of allowed
		/// rectangles extracted from any single connected component.
		/// Use 0 if no limit is to be applied.<para/>
		///
		/// (6) The flag %remainder specifies whether we take a final bounding
		/// box for anything left after the maximum number of allowed
		/// rectangle is extracted.<para/>
		///
		/// (7) So if %maxcomps  is greater  0, it specifies that we want no more than
		/// the first %maxcomps rectangles that satisfy the input
		/// criteria.  After this, we can get a final rectangle that
		/// bounds everything left over by setting %remainder == 1.
		/// If %remainder == 0, we only get rectangles that satisfy
		/// the input criteria.<para/>
		///
		/// (8) It should be noted that the removal of rectangles can
		/// break the original c.c. into several c.c.<para/>
		///
		/// (9) Summing up:
		/// If %maxcomp == 0, the splitting proceeds as far as possible.
		/// If %maxcomp  is greater  0, the splitting stops when %maxcomps are
		/// found, or earlier if no more components can be selected.
		/// If %remainder == 1 and components remain that cannot be
		/// selected, they are returned as a single final rectangle
		/// otherwise, they are ignored.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixSplitComponentIntoBoxa/*"/>
		///  <param name="pix">[in] - 1 bpp</param>
		///  <param name="box">[in][optional] - location of pix w/rt an origin</param>
		///  <param name="minsum">[in] - minimum pixels to trigger propagation</param>
		///  <param name="skipdist">[in] - distance before computing sum for propagation</param>
		///  <param name="delta">[in] - difference required to stop propagation</param>
		///  <param name="maxbg">[in] - maximum number of allowed bg pixels in ref scan</param>
		///  <param name="maxcomps">[in] - use 0 for unlimited number of subdivided components</param>
		///  <param name="remainder">[in] - set to 1 to get b.b. of remaining stuff</param>
		///   <returns>boxa of rectangles covering the fg of pix, or NULL on error</returns>
		public static Boxa pixSplitComponentIntoBoxa(
						Pix pix,
						Box box,
						int minsum,
						int skipdist,
						int delta,
						int maxbg,
						int maxcomps,
						int remainder)
		{
			if (pix == null) {throw new ArgumentNullException  ("pix cannot be Nothing");}

			if ((new List<int> {1}).Contains ((int)pix.d) == false) { throw new ArgumentException ("1 bpp"); }
			IntPtr boxPtr = IntPtr.Zero; 	if (box != null) {boxPtr = box.Pointer;}
			IntPtr _Result = Natives.pixSplitComponentIntoBoxa(pix.Pointer, boxPtr,   minsum,   skipdist,   delta,   maxbg,   maxcomps,   remainder);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Boxa(_Result);
		}

		// boxfunc3.c (1308, 1)
		// makeMosaicStrips(w, h, direction, size) as Boxa
		// makeMosaicStrips(l_int32, l_int32, l_int32, l_int32) as BOXA *
		///  <summary>
		/// (1) For example, this can be used to generate a pixa of
		/// vertical strips of width 10 from an image, using:
		/// pixGetDimensions(pix, [and]w, [and]h, NULL)
		/// boxa = makeMosaicStrips(w, h, L_SCAN_HORIZONTAL, 10)
		/// pixa = pixClipRectangles(pix, boxa)
		/// All strips except the last will be the same width.  The
		/// last strip will have width w % 10.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/makeMosaicStrips/*"/>
		///  <param name="w">[in] - </param>
		///  <param name="h">[in] - </param>
		///  <param name="direction">[in] - L_SCAN_HORIZONTAL or L_SCAN_VERTICAL</param>
		///  <param name="size">[in] - of strips in the scan direction</param>
		///   <returns>boxa, or NULL on error</returns>
		public static Boxa makeMosaicStrips(
						int w,
						int h,
						int direction,
						int size)
		{
			IntPtr _Result = Natives.makeMosaicStrips(  w,   h,   direction,   size);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Boxa(_Result);
		}

		// boxfunc3.c (1388, 1)
		// boxaCompareRegions(boxa1, boxa2, areathresh, pnsame, pdiffarea, pdiffxor, ppixdb) as int
		// boxaCompareRegions(BOXA *, BOXA *, l_int32, l_int32 *, l_float32 *, l_float32 *, PIX **) as l_ok
		///  <summary>
		/// (1) This takes 2 boxa, removes all boxes smaller than a given area,
		/// and compares the remaining boxes between the boxa.<para/>
		///
		/// (2) The area threshold is introduced to help remove noise from
		/// small components.  Any box with a smaller value of w  h
		/// will be removed from consideration.<para/>
		///
		/// (3) The xor difference is the most stringent test, requiring alignment
		/// of the corresponding boxes.  It is also more computationally
		/// intensive and is optionally returned.  Alignment is to the
		/// UL corner of each region containing all boxes, as given by
		/// boxaGetExtent().<para/>
		///
		/// (4) Both fractional differences are with respect to the total
		/// area in the two boxa.  They range from 0.0 to 1.0.
		/// A perfect match has value 0.0.  If both boxa are empty,
		/// we return 0.0 if one is empty we return 1.0.<para/>
		///
		/// (5) An example input might be the rectangular regions of a
		/// segmentation mask for text or images from two pages.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/boxaCompareRegions/*"/>
		///  <param name="boxa1">[in] - </param>
		///  <param name="boxa2">[in] - </param>
		///  <param name="areathresh">[in] - minimum area of boxes to be considered</param>
		///  <param name="pnsame">[out] - true if same number of boxes</param>
		///  <param name="pdiffarea">[out] - fractional difference in total area</param>
		///  <param name="pdiffxor">[out][optional] - fractional difference in xor of regions</param>
		///  <param name="ppixdb">[out][optional] - debug pix showing two boxa</param>
		///   <returns>0 if OK, 1 on error</returns>
		public static int boxaCompareRegions(
						Boxa boxa1,
						Boxa boxa2,
						int areathresh,
						out int pnsame,
						out Single pdiffarea,
						out Single pdiffxor,
						out Pix ppixdb)
		{
			if (boxa1 == null) {throw new ArgumentNullException  ("boxa1 cannot be Nothing");}

			if (boxa2 == null) {throw new ArgumentNullException  ("boxa2 cannot be Nothing");}

			IntPtr ppixdbPtr = IntPtr.Zero;
			int _Result = Natives.boxaCompareRegions(boxa1.Pointer, boxa2.Pointer,   areathresh, out  pnsame, out  pdiffarea, out  pdiffxor, out ppixdbPtr);
			if (ppixdbPtr == IntPtr.Zero) {ppixdb = null;} else { ppixdb = new Pix(ppixdbPtr); };

			return _Result;
		}

		// boxfunc3.c (1532, 1)
		// pixSelectLargeULComp(pixs, areaslop, yslop, connectivity) as Box
		// pixSelectLargeULComp(PIX *, l_float32, l_int32, l_int32) as BOX *
		///  <summary>
		/// (1) This selects a box near the top (first) and left (second)
		/// of the image, from the set of all boxes that have
		/// area greater or equal %areaslop  (area of biggest box),
		/// where %areaslop is some fraction say ~ 0.9.<para/>
		///
		/// (2) For all boxes satisfying the above condition, select
		/// the left-most box that is within %yslop (say, 20) pixels
		/// of the box nearest the top.<para/>
		///
		/// (3) This can be used to reliably select a specific one of
		/// the largest regions in an image, for applications where
		/// there are expected to be small variations in region size
		/// and location.<para/>
		///
		/// (4) See boxSelectLargeULBox() for implementation details.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixSelectLargeULComp/*"/>
		///  <param name="pixs">[in] - 1 bpp</param>
		///  <param name="areaslop">[in] - fraction near but less than 1.0</param>
		///  <param name="yslop">[in] - number of pixels in y direction</param>
		///  <param name="connectivity">[in] - 4 or 8</param>
		///   <returns>box, or NULL on error</returns>
		public static Box pixSelectLargeULComp(
						Pix pixs,
						Single areaslop,
						int yslop,
						int connectivity)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if ((new List<int> {1}).Contains ((int)pixs.d) == false) { throw new ArgumentException ("1 bpp"); }
			IntPtr _Result = Natives.pixSelectLargeULComp(pixs.Pointer,   areaslop,   yslop,   connectivity);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Box(_Result);
		}

		// boxfunc3.c (1573, 1)
		// boxaSelectLargeULBox(boxas, areaslop, yslop) as Box
		// boxaSelectLargeULBox(BOXA *, l_float32, l_int32) as BOX *
		///  <summary>
		/// (1) See usage notes in pixSelectLargeULComp().
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/boxaSelectLargeULBox/*"/>
		///  <param name="boxas">[in] - 1 bpp</param>
		///  <param name="areaslop">[in] - fraction near but less than 1.0</param>
		///  <param name="yslop">[in] - number of pixels in y direction</param>
		///   <returns>box, or NULL on error</returns>
		public static Box boxaSelectLargeULBox(
						Boxa boxas,
						Single areaslop,
						int yslop)
		{
			if (boxas == null) {throw new ArgumentNullException  ("boxas cannot be Nothing");}

			IntPtr _Result = Natives.boxaSelectLargeULBox(boxas.Pointer,   areaslop,   yslop);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Box(_Result);
		}


	}
}
