using System;
using System.IO;

using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LeptonicaSharp
{
	public partial class _All
	{

		// pdfio2.c (182, 1)
		// pixConvertToPdfData(pix, type, quality, pdata, pnbytes, x, y, res, title, plpd, position) as int
		// pixConvertToPdfData(PIX *, l_int32, l_int32, l_uint8 **, size_t *, l_int32, l_int32, l_int32, const char *, L_PDF_DATA **, l_int32) as l_ok
		///  <summary>
		/// (1) If %res == 0 and the input resolution field is 0,
		/// this will use DEFAULT_INPUT_RES.<para/>
		///
		/// (2) This only writes %data if it is the last image to be
		/// written on the page.<para/>
		///
		/// (3) See comments in convertToPdf().
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixConvertToPdfData/*"/>
		///  <param name="pix">[in] - all depths cmap OK</param>
		///  <param name="type">[in] - L_G4_ENCODE, L_JPEG_ENCODE, L_FLATE_ENCODE</param>
		///  <param name="quality">[in] - used for JPEG only 0 for default (75)</param>
		///  <param name="pdata">[out] - pdf array</param>
		///  <param name="pnbytes">[out] - number of bytes in pdf array</param>
		///  <param name="x">[in] - location of lower-left corner of image, in pixels, relative to the PostScript origin (0,0) at the lower-left corner of the page)</param>
		///  <param name="y">[in] - location of lower-left corner of image, in pixels, relative to the PostScript origin (0,0) at the lower-left corner of the page)</param>
		///  <param name="res">[in] - override the resolution of the input image, in ppi use 0 to respect the resolution embedded in the input</param>
		///  <param name="title">[in][optional] - pdf title</param>
		///  <param name="plpd">[in,out] - ptr to lpd, which is created on the first invocation and returned until last image is processed</param>
		///  <param name="position">[in] - in image sequence: L_FIRST_IMAGE, L_NEXT_IMAGE, L_LAST_IMAGE</param>
		///   <returns>0 if OK, 1 on error</returns>
		public static int pixConvertToPdfData(
						Pix pix,
						int type,
						int quality,
						out Byte[] pdata,
						out uint pnbytes,
						int x,
						int y,
						int res,
						String title,
						ref L_Pdf_Data plpd,
						int position)
		{
			if (pix == null) {throw new ArgumentNullException  ("pix cannot be Nothing");}

			IntPtr pdataPtr = IntPtr.Zero;
			IntPtr plpdPtr = IntPtr.Zero; 	if (plpd != null) {plpdPtr = plpd.Pointer;}
			int _Result = Natives.pixConvertToPdfData(pix.Pointer,   type,   quality, out  pdataPtr, out  pnbytes,   x,   y,   res,   title, ref plpdPtr,   position);
			Byte[] pdataGen = new Byte[pnbytes];

			if (pdataPtr != IntPtr.Zero) {
				Marshal.Copy(pdataPtr, pdataGen, 0, pdataGen.Length);
			}

			pdata = pdataGen;
			if (plpdPtr == IntPtr.Zero) {plpd = null;} else { plpd = new L_Pdf_Data(plpdPtr); };

			return _Result;
		}

		// pdfio2.c (307, 1)
		// ptraConcatenatePdfToData(pa_data, sa, pdata, pnbytes) as int
		// ptraConcatenatePdfToData(L_PTRA *, SARRAY *, l_uint8 **, size_t *) as l_ok
		///  <summary>
		/// (1) This only works with leptonica-formatted single-page pdf files.
		/// pdf files generated by other programs will have unpredictable
		/// (and usually bad) results.  The requirements for each pdf file:
		/// (a) The Catalog and Info objects are the first two.
		/// (b) Object 3 is Pages
		/// (c) Object 4 is Page
		/// (d) The remaining objects are Contents, XObjects, and ColorSpace<para/>
		///
		/// (2) We remove trailers from each page, and append the full trailer
		/// for all pages at the end.<para/>
		///
		/// (3) For all but the first file, remove the ID and the first 3
		/// objects (catalog, info, pages), so that each subsequent
		/// file has only objects of these classes:
		/// Page, Contents, XObject, ColorSpace (Indexed RGB).
		/// For those objects, we substitute these refs to objects
		/// in the local file:
		/// Page:  Parent(object 3), Contents, XObject(typically multiple)
		/// XObject:  [ColorSpace if indexed]
		/// The Pages object on the first page (object 3) has a Kids array
		/// of references to all the Page objects, with a Count equal
		/// to the number of pages.  Each Page object refers back to
		/// this parent.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/ptraConcatenatePdfToData/*"/>
		///  <param name="pa_data">[in] - ptra array of pdf strings, each for a single-page pdf file</param>
		///  <param name="sa">[in] - string array [optional] of pathnames for input pdf files</param>
		///  <param name="pdata">[out] - concatenated pdf data in memory</param>
		///  <param name="pnbytes">[out] - number of bytes in pdf data</param>
		///   <returns>0 if OK, 1 on error</returns>
		public static int ptraConcatenatePdfToData(
						L_Ptra pa_data,
						Sarray sa,
						out Byte[] pdata,
						out uint pnbytes)
		{
			if (pa_data == null) {throw new ArgumentNullException  ("pa_data cannot be Nothing");}

			IntPtr saPtr = IntPtr.Zero; 	if (sa != null) {saPtr = sa.Pointer;}
			IntPtr pdataPtr = IntPtr.Zero;
			int _Result = Natives.ptraConcatenatePdfToData(pa_data.Pointer, saPtr, out  pdataPtr, out  pnbytes);
			Byte[] pdataGen = new Byte[pnbytes];

			if (pdataPtr != IntPtr.Zero) {
				Marshal.Copy(pdataPtr, pdataGen, 0, pdataGen.Length);
			}

			pdata = pdataGen;
			return _Result;
		}

		// pdfio2.c (471, 1)
		// convertTiffMultipageToPdf(filein, fileout) as int
		// convertTiffMultipageToPdf(const char *, const char *) as l_ok
		///  <summary>
		/// (1) A multipage tiff file can also be converted to PS, using
		/// convertTiffMultipageToPS()
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/convertTiffMultipageToPdf/*"/>
		///  <param name="filein">[in] - (tiff)</param>
		///  <param name="fileout">[in] - (pdf)</param>
		///   <returns>0 if OK, 1 on error</returns>
		public static int convertTiffMultipageToPdf(
						String filein,
						String fileout)
		{
			if (filein == null) {throw new ArgumentNullException  ("filein cannot be Nothing");}

			if (fileout == null) {throw new ArgumentNullException  ("fileout cannot be Nothing");}

			int _Result = Natives.convertTiffMultipageToPdf(  filein,   fileout);
			return _Result;
		}

		// pdfio2.c (520, 1)
		// l_generateCIDataForPdf(fname, pix, quality, pcid) as int
		// l_generateCIDataForPdf(const char *, PIX *, l_int32, L_COMP_DATA **) as l_ok
		///  <summary>
		/// (1) You must set either filename or pix.<para/>
		///
		/// (2) Given an image file and optionally a pix raster of that data,
		/// this provides a CID that is compatible with PDF, preferably
		/// without transcoding.<para/>
		///
		/// (3) The pix is included for efficiency, in case transcoding
		/// is required and the pix is available to the caller.<para/>
		///
		/// (4) We don't try to open files named "stdin" or "-" for Tesseract
		/// compatibility reasons. We may remove this restriction
		/// in the future.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/l_generateCIDataForPdf/*"/>
		///  <param name="fname">[in][optional] - can be null</param>
		///  <param name="pix">[in][optional] - can be null</param>
		///  <param name="quality">[in] - for jpeg if transcoded 75 is standard</param>
		///  <param name="pcid">[out] - compressed data</param>
		///   <returns>0 if OK, 1 on error</returns>
		public static int l_generateCIDataForPdf(
						String fname,
						Pix pix,
						int quality,
						out L_Compressed_Data pcid)
		{
			IntPtr pixPtr = IntPtr.Zero; 	if (pix != null) {pixPtr = pix.Pointer;}
			IntPtr pcidPtr = IntPtr.Zero;
			int _Result = Natives.l_generateCIDataForPdf(  fname, pixPtr,   quality, out pcidPtr);
			if (pcidPtr == IntPtr.Zero) {pcid = null;} else { pcid = new L_Compressed_Data(pcidPtr); };

			return _Result;
		}

		// pdfio2.c (598, 1)
		// l_generateFlateDataPdf(fname, pixs) as L_Compressed_Data
		// l_generateFlateDataPdf(const char *, PIX *) as L_COMP_DATA *
		///  <summary>
		/// (1) If you hand this a png file, you are going to get
		/// png predictors embedded in the flate data. So it has
		/// come to this. http://xkcd.com/1022/<para/>
		///
		/// (2) Exception: if the png is interlaced or if it is RGBA,
		/// it will be transcoded.<para/>
		///
		/// (3) If transcoding is required, this will not have to read from
		/// file if you also input a pix.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/l_generateFlateDataPdf/*"/>
		///  <param name="fname">[in] - preferably png</param>
		///  <param name="pixs">[in][optional] - can be null</param>
		///   <returns>cid containing png data, or NULL on error</returns>
		public static L_Compressed_Data l_generateFlateDataPdf(
						String fname,
						Pix pixs = null)
		{
			if (fname == null) {throw new ArgumentNullException  ("fname cannot be Nothing");}

			IntPtr pixsPtr = IntPtr.Zero; 	if (pixs != null) {pixsPtr = pixs.Pointer;}
			IntPtr _Result = Natives.l_generateFlateDataPdf(  fname, pixsPtr);

			if (_Result == IntPtr.Zero) {return null;}

			return  new L_Compressed_Data(_Result);
		}

		// pdfio2.c (795, 1)
		// l_generateJpegData(fname, ascii85flag) as L_Compressed_Data
		// l_generateJpegData(const char *, l_int32) as L_COMP_DATA *
		///  <summary>
		/// (1) Set ascii85flag:
		/// ~ 0 for binary data (not permitted in PostScript)
		/// ~ 1 for ascii85 (5 for 4) encoded binary data
		/// (not permitted in pdf)<para/>
		///
		/// (2) Do not free the data.  l_generateJpegDataMem() will free
		/// the data if it does not use ascii encoding.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/l_generateJpegData/*"/>
		///  <param name="fname">[in] - of jpeg file</param>
		///  <param name="ascii85flag">[in] - 0 for jpeg 1 for ascii85-encoded jpeg</param>
		///   <returns>cid containing jpeg data, or NULL on error</returns>
		public static L_Compressed_Data l_generateJpegData(
						String fname,
						int ascii85flag)
		{
			if (fname == null) {throw new ArgumentNullException  ("fname cannot be Nothing");}

			IntPtr _Result = Natives.l_generateJpegData(  fname,   ascii85flag);

			if (_Result == IntPtr.Zero) {return null;}

			return  new L_Compressed_Data(_Result);
		}

		// pdfio2.c (829, 1)
		// l_generateJpegDataMem(data, nbytes, ascii85flag) as L_Compressed_Data
		// l_generateJpegDataMem(l_uint8 *, size_t, l_int32) as L_COMP_DATA *
		///  <summary>
		/// (1) See l_generateJpegData().
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/l_generateJpegDataMem/*"/>
		///  <param name="data">[in] - of jpeg file</param>
		///  <param name="nbytes">[in] - </param>
		///  <param name="ascii85flag">[in] - 0 for jpeg 1 for ascii85-encoded jpeg</param>
		///   <returns>cid containing jpeg data, or NULL on error</returns>
		public static L_Compressed_Data l_generateJpegDataMem(
						Byte[] data,
						uint nbytes,
						int ascii85flag)
		{
			if (data == null) {throw new ArgumentNullException  ("data cannot be Nothing");}

			IntPtr _Result = Natives.l_generateJpegDataMem(  data,   nbytes,   ascii85flag);

			if (_Result == IntPtr.Zero) {return null;}

			return  new L_Compressed_Data(_Result);
		}

		// pdfio2.c (943, 1)
		// l_generateCIData(fname, type, quality, ascii85, pcid) as int
		// l_generateCIData(const char *, l_int32, l_int32, l_int32, L_COMP_DATA **) as l_ok
		///  <summary>
		/// (1) This can be used for both PostScript and pdf.<para/>
		///
		/// (1) Set ascii85:
		/// ~ 0 for binary data (not permitted in PostScript)
		/// ~ 1 for ascii85 (5 for 4) encoded binary data<para/>
		///
		/// (2) This attempts to compress according to the requested type.
		/// If this can't be done, it falls back to ordinary flate encoding.<para/>
		///
		/// (3) This differs from l_generateCIDataPdf(), which determines
		/// the format and attempts to generate the CID without transcoding.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/l_generateCIData/*"/>
		///  <param name="fname">[in] - </param>
		///  <param name="type">[in] - L_G4_ENCODE, L_JPEG_ENCODE, L_FLATE_ENCODE, L_JP2K_ENCODE</param>
		///  <param name="quality">[in] - used for jpeg only 0 for default (75)</param>
		///  <param name="ascii85">[in] - 0 for binary 1 for ascii85-encoded</param>
		///  <param name="pcid">[out] - compressed data</param>
		///   <returns>0 if OK, 1 on error</returns>
		public static int l_generateCIData(
						String fname,
						int type,
						int quality,
						int ascii85,
						out L_Compressed_Data pcid)
		{
			if (fname == null) {throw new ArgumentNullException  ("fname cannot be Nothing");}

			IntPtr pcidPtr = IntPtr.Zero;
			int _Result = Natives.l_generateCIData(  fname,   type,   quality,   ascii85, out pcidPtr);
			if (pcidPtr == IntPtr.Zero) {pcid = null;} else { pcid = new L_Compressed_Data(pcidPtr); };

			return _Result;
		}

		// pdfio2.c (1039, 1)
		// pixGenerateCIData(pixs, type, quality, ascii85, pcid) as int
		// pixGenerateCIData(PIX *, l_int32, l_int32, l_int32, L_COMP_DATA **) as l_ok
		///  <summary>
		/// (1) Set ascii85:
		/// ~ 0 for binary data (not permitted in PostScript)
		/// ~ 1 for ascii85 (5 for 4) encoded binary data
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixGenerateCIData/*"/>
		///  <param name="pixs">[in] - 8 or 32 bpp, no colormap</param>
		///  <param name="type">[in] - L_G4_ENCODE, L_JPEG_ENCODE, L_FLATE_ENCODE</param>
		///  <param name="quality">[in] - used for jpeg only 0 for default (75)</param>
		///  <param name="ascii85">[in] - 0 for binary 1 for ascii85-encoded</param>
		///  <param name="pcid">[out] - compressed data</param>
		///   <returns>0 if OK, 1 on error</returns>
		public static int pixGenerateCIData(
						Pix pixs,
						int type,
						int quality,
						int ascii85,
						out L_Compressed_Data pcid)
		{
			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			IntPtr pcidPtr = IntPtr.Zero;
			int _Result = Natives.pixGenerateCIData(pixs.Pointer,   type,   quality,   ascii85, out pcidPtr);
			if (pcidPtr == IntPtr.Zero) {pcid = null;} else { pcid = new L_Compressed_Data(pcidPtr); };

			return _Result;
		}

		// pdfio2.c (1112, 1)
		// l_generateFlateData(fname, ascii85flag) as L_Compressed_Data
		// l_generateFlateData(const char *, l_int32) as L_COMP_DATA *
		///  <summary>
		/// (1) The input image is converted to one of these 4 types:
		/// ~ 1 bpp
		/// ~ 8 bpp, no colormap
		/// ~ 8 bpp, colormap
		/// ~ 32 bpp rgb<para/>
		///
		/// (2) Set ascii85flag:
		/// ~ 0 for binary data (not permitted in PostScript)
		/// ~ 1 for ascii85 (5 for 4) encoded binary data
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/l_generateFlateData/*"/>
		///  <param name="fname">[in] - </param>
		///  <param name="ascii85flag">[in] - 0 for gzipped 1 for ascii85-encoded gzipped</param>
		///   <returns>cid flate compressed image data, or NULL on error</returns>
		public static L_Compressed_Data l_generateFlateData(
						String fname,
						int ascii85flag)
		{
			if (fname == null) {throw new ArgumentNullException  ("fname cannot be Nothing");}

			IntPtr _Result = Natives.l_generateFlateData(  fname,   ascii85flag);

			if (_Result == IntPtr.Zero) {return null;}

			return  new L_Compressed_Data(_Result);
		}

		// pdfio2.c (1350, 1)
		// l_generateG4Data(fname, ascii85flag) as L_Compressed_Data
		// l_generateG4Data(const char *, l_int32) as L_COMP_DATA *
		///  <summary>
		/// (1) Set ascii85flag:
		/// ~ 0 for binary data (not permitted in PostScript)
		/// ~ 1 for ascii85 (5 for 4) encoded binary data
		/// (not permitted in pdf)
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/l_generateG4Data/*"/>
		///  <param name="fname">[in] - of g4 compressed file</param>
		///  <param name="ascii85flag">[in] - 0 for g4 compressed 1 for ascii85-encoded g4</param>
		///   <returns>cid g4 compressed image data, or NULL on error</returns>
		public static L_Compressed_Data l_generateG4Data(
						String fname,
						int ascii85flag)
		{
			if (fname == null) {throw new ArgumentNullException  ("fname cannot be Nothing");}

			IntPtr _Result = Natives.l_generateG4Data(  fname,   ascii85flag);

			if (_Result == IntPtr.Zero) {return null;}

			return  new L_Compressed_Data(_Result);
		}

		// pdfio2.c (1427, 1)
		// cidConvertToPdfData(cid, title, pdata, pnbytes) as int
		// cidConvertToPdfData(L_COMP_DATA *, const char *, l_uint8 **, size_t *) as l_ok
		///  <summary>
		/// (1) Caller must not destroy the cid.  It is absorbed in the
		/// lpd and destroyed by this function.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/cidConvertToPdfData/*"/>
		///  <param name="cid">[in] - compressed image data -- of jp2k image</param>
		///  <param name="title">[in][optional] - pdf title can be NULL</param>
		///  <param name="pdata">[out] - output pdf data for image</param>
		///  <param name="pnbytes">[out] - size of output pdf data</param>
		///   <returns>0 if OK, 1 on error</returns>
		public static int cidConvertToPdfData(
						L_Compressed_Data cid,
						String title,
						out Byte[] pdata,
						out uint pnbytes)
		{
			if (cid == null) {throw new ArgumentNullException  ("cid cannot be Nothing");}

			IntPtr pdataPtr = IntPtr.Zero;
			int _Result = Natives.cidConvertToPdfData(cid.Pointer,   title, out  pdataPtr, out  pnbytes);
			Byte[] pdataGen = new Byte[pnbytes];

			if (pdataPtr != IntPtr.Zero) {
				Marshal.Copy(pdataPtr, pdataGen, 0, pdataGen.Length);
			}

			pdata = pdataGen;
			return _Result;
		}

		// pdfio2.c (1476, 1)
		// l_CIDataDestroy(pcid) as Object
		// l_CIDataDestroy(L_COMP_DATA **) as void
		///  <summary>
		/// l_CIDataDestroy()
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/l_CIDataDestroy/*"/>
		///  <param name="pcid">[in,out] - will be set to null before returning</param>
		public static void l_CIDataDestroy(
						ref L_Compressed_Data pcid)
		{
			IntPtr pcidPtr = IntPtr.Zero; 	if (pcid != null) {pcidPtr = pcid.Pointer;}
			Natives.l_CIDataDestroy(ref pcidPtr);
			if (pcidPtr == IntPtr.Zero) {pcid = null;} else { pcid = new L_Compressed_Data(pcidPtr); };
		}

		// pdfio2.c (2438, 1)
		// l_pdfSetG4ImageMask(flag) as Object
		// l_pdfSetG4ImageMask(l_int32) as void
		///  <summary>
		/// (1) The default is for writing only the fg (through the mask).
		/// That way when you write a 1 bpp image, the bg is transparent,
		/// so any previously written image remains visible behind it.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/l_pdfSetG4ImageMask/*"/>
		///  <param name="flag">[in] - 1 for writing g4 data as fg only through a mask 0 for writing fg and bg</param>
		public static void l_pdfSetG4ImageMask(
						int flag)
		{
			Natives.l_pdfSetG4ImageMask(  flag);
		}

		// pdfio2.c (2458, 1)
		// l_pdfSetDateAndVersion(flag) as Object
		// l_pdfSetDateAndVersion(l_int32) as void
		///  <summary>
		/// (1) The default is for writing this data.  For regression tests
		/// that compare output against golden files, it is useful to omit.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/l_pdfSetDateAndVersion/*"/>
		///  <param name="flag">[in] - 1 for writing date/time and leptonica version 0 for omitting this from the metadata</param>
		public static void l_pdfSetDateAndVersion(
						int flag)
		{
			Natives.l_pdfSetDateAndVersion(  flag);
		}


	}
}
