using System;
using System.IO;

using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LeptonicaSharp
{
	public partial class _All
	{

		// bytearray.c (93, 1)
		// l_byteaCreate(nbytes) as L_Bytea
		// l_byteaCreate(size_t) as L_BYTEA *
		///  <summary>
		/// (1) The allocated array is n + 1 bytes.  This allows room
		/// for null termination.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/l_byteaCreate/*"/>
		///  <param name="nbytes">[in] - determines initial size of data array</param>
		///   <returns>l_bytea, or NULL on error</returns>
		public static L_Bytea l_byteaCreate(
						uint nbytes)
		{
			IntPtr _Result = Natives.l_byteaCreate(  nbytes);

			if (_Result == IntPtr.Zero) {return null;}

			return  new L_Bytea(_Result);
		}

		// bytearray.c (122, 1)
		// l_byteaInitFromMem(data, size) as L_Bytea
		// l_byteaInitFromMem(const l_uint8 *, size_t) as L_BYTEA *
		///  <summary>
		/// l_byteaInitFromMem()
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/l_byteaInitFromMem/*"/>
		///  <param name="data">[in] - to be copied to the array</param>
		///  <param name="size">[in] - amount of data</param>
		///   <returns>l_bytea, or NULL on error</returns>
		public static L_Bytea l_byteaInitFromMem(
						Byte[] data,
						uint size)
		{
			if (data == null) {throw new ArgumentNullException  ("data cannot be Nothing");}

			IntPtr _Result = Natives.l_byteaInitFromMem(  data,   size);

			if (_Result == IntPtr.Zero) {return null;}

			return  new L_Bytea(_Result);
		}

		// bytearray.c (149, 1)
		// l_byteaInitFromFile(fname) as L_Bytea
		// l_byteaInitFromFile(const char *) as L_BYTEA *
		///  <summary>
		/// l_byteaInitFromFile()
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/l_byteaInitFromFile/*"/>
		///  <param name="fname">[in] - </param>
		///   <returns>l_bytea, or NULL on error</returns>
		public static L_Bytea l_byteaInitFromFile(
						String fname)
		{
			if (fname == null) {throw new ArgumentNullException  ("fname cannot be Nothing");}

			if (File.Exists (fname) == false) {   throw new ArgumentException ("File is missing");};

			IntPtr _Result = Natives.l_byteaInitFromFile(  fname);

			if (_Result == IntPtr.Zero) {return null;}

			return  new L_Bytea(_Result);
		}

		// bytearray.c (176, 1)
		// l_byteaInitFromStream(fp) as L_Bytea
		// l_byteaInitFromStream(FILE *) as L_BYTEA *
		///  <summary>
		/// l_byteaInitFromStream()
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/l_byteaInitFromStream/*"/>
		///  <param name="fp">[in] - file stream</param>
		///   <returns>l_bytea, or NULL on error</returns>
		public static L_Bytea l_byteaInitFromStream(
						FILE fp)
		{
			if (fp == null) {throw new ArgumentNullException  ("fp cannot be Nothing");}

			IntPtr _Result = Natives.l_byteaInitFromStream(fp.Pointer);

			if (_Result == IntPtr.Zero) {return null;}

			return  new L_Bytea(_Result);
		}

		// bytearray.c (213, 1)
		// l_byteaCopy(bas, copyflag) as L_Bytea
		// l_byteaCopy(L_BYTEA *, l_int32) as L_BYTEA *
		///  <summary>
		/// (1) If cloning, up the refcount and return a ptr to %bas.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/l_byteaCopy/*"/>
		///  <param name="bas">[in] - source lba</param>
		///  <param name="copyflag">[in] - L_COPY, L_CLONE</param>
		///   <returns>clone or copy of bas, or NULL on error</returns>
		public static L_Bytea l_byteaCopy(
						L_Bytea bas,
						int copyflag)
		{
			if (bas == null) {throw new ArgumentNullException  ("bas cannot be Nothing");}

			IntPtr _Result = Natives.l_byteaCopy(bas.Pointer,   copyflag);

			if (_Result == IntPtr.Zero) {return null;}

			return  new L_Bytea(_Result);
		}

		// bytearray.c (245, 1)
		// l_byteaDestroy(pba) as Object
		// l_byteaDestroy(L_BYTEA **) as void
		///  <summary>
		/// (1) Decrements the ref count and, if 0, destroys the lba.<para/>
		///
		/// (2) Always nulls the input ptr.<para/>
		///
		/// (3) If the data has been previously removed, the lba will
		/// have been nulled, so this will do nothing.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/l_byteaDestroy/*"/>
		///  <param name="pba">[in,out] - will be set to null before returning</param>
		public static void l_byteaDestroy(
						ref L_Bytea pba)
		{
			IntPtr pbaPtr = IntPtr.Zero; 	if (pba != null) {pbaPtr = pba.Pointer;}
			Natives.l_byteaDestroy(ref pbaPtr);
			if (pbaPtr == IntPtr.Zero) {pba = null;} else { pba = new L_Bytea(pbaPtr); };
		}

		// bytearray.c (281, 1)
		// l_byteaGetSize(ba) as uint
		// l_byteaGetSize(L_BYTEA *) as size_t
		///  <summary>
		/// l_byteaGetSize()
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/l_byteaGetSize/*"/>
		///  <param name="ba">[in] - </param>
		///   <returns>size of stored byte array, or 0 on error</returns>
		public static uint l_byteaGetSize(
						L_Bytea ba)
		{
			if (ba == null) {throw new ArgumentNullException  ("ba cannot be Nothing");}

			uint _Result = Natives.l_byteaGetSize(ba.Pointer);
			return _Result;
		}

		// bytearray.c (304, 1)
		// l_byteaGetData(ba, psize) as Byte[]
		// l_byteaGetData(L_BYTEA *, size_t *) as l_uint8 *
		///  <summary>
		/// (1) The returned ptr is owned by %ba.  Do not free it!
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/l_byteaGetData/*"/>
		///  <param name="ba">[in] - </param>
		///  <param name="psize">[out] - size of data in lba</param>
		///   <returns>ptr to existing data array, or NULL on error</returns>
		public static Byte[] l_byteaGetData(
						L_Bytea ba,
						out uint psize)
		{
			if (ba == null) {throw new ArgumentNullException  ("ba cannot be Nothing");}

			Byte[] _Result = Natives.l_byteaGetData(ba.Pointer, out  psize);
			return _Result;
		}

		// bytearray.c (333, 1)
		// l_byteaCopyData(ba, psize) as Byte[]
		// l_byteaCopyData(L_BYTEA *, size_t *) as l_uint8 *
		///  <summary>
		/// (1) The returned data is owned by the caller.  The input %ba
		/// still owns the original data array.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/l_byteaCopyData/*"/>
		///  <param name="ba">[in] - </param>
		///  <param name="psize">[out] - size of data in lba</param>
		///   <returns>copy of data in use in the data array, or NULL on error.</returns>
		public static Byte[] l_byteaCopyData(
						L_Bytea ba,
						out uint psize)
		{
			if (ba == null) {throw new ArgumentNullException  ("ba cannot be Nothing");}

			Byte[] _Result = Natives.l_byteaCopyData(ba.Pointer, out  psize);
			return _Result;
		}

		// bytearray.c (363, 1)
		// l_byteaAppendData(ba, newdata, newbytes) as int
		// l_byteaAppendData(L_BYTEA *, const l_uint8 *, size_t) as l_ok
		///  <summary>
		/// l_byteaAppendData()
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/l_byteaAppendData/*"/>
		///  <param name="ba">[in] - </param>
		///  <param name="newdata">[in] - byte array to be appended</param>
		///  <param name="newbytes">[in] - size of data array</param>
		///   <returns>0 if OK, 1 on error</returns>
		public static int l_byteaAppendData(
						L_Bytea ba,
						Byte[] newdata,
						uint newbytes)
		{
			if (ba == null) {throw new ArgumentNullException  ("ba cannot be Nothing");}

			if (newdata == null) {throw new ArgumentNullException  ("newdata cannot be Nothing");}

			int _Result = Natives.l_byteaAppendData(ba.Pointer,   newdata,   newbytes);
			return _Result;
		}

		// bytearray.c (396, 1)
		// l_byteaAppendString(ba, str) as int
		// l_byteaAppendString(L_BYTEA *, const char *) as l_ok
		///  <summary>
		/// l_byteaAppendString()
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/l_byteaAppendString/*"/>
		///  <param name="ba">[in] - </param>
		///  <param name="str">[in] - null-terminated string to be appended</param>
		///   <returns>0 if OK, 1 on error</returns>
		public static int l_byteaAppendString(
						L_Bytea ba,
						String str)
		{
			if (ba == null) {throw new ArgumentNullException  ("ba cannot be Nothing");}

			if (str == null) {throw new ArgumentNullException  ("str cannot be Nothing");}

			int _Result = Natives.l_byteaAppendString(ba.Pointer,   str);
			return _Result;
		}

		// bytearray.c (465, 1)
		// l_byteaJoin(ba1, pba2) as int
		// l_byteaJoin(L_BYTEA *, L_BYTEA **) as l_ok
		///  <summary>
		/// (1) It is a no-op, not an error, for %ba2 to be null.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/l_byteaJoin/*"/>
		///  <param name="ba1">[in] - </param>
		///  <param name="pba2">[in,out] - data array is added to the one in ba1, and then ba2 is destroyed</param>
		///   <returns>0 if OK, 1 on error</returns>
		public static int l_byteaJoin(
						L_Bytea ba1,
						ref L_Bytea pba2)
		{
			if (ba1 == null) {throw new ArgumentNullException  ("ba1 cannot be Nothing");}

			IntPtr pba2Ptr = IntPtr.Zero; 	if (pba2 != null) {pba2Ptr = pba2.Pointer;}
			int _Result = Natives.l_byteaJoin(ba1.Pointer, ref pba2Ptr);
			if (pba2Ptr == IntPtr.Zero) {pba2 = null;} else { pba2 = new L_Bytea(pba2Ptr); };

			return _Result;
		}

		// bytearray.c (497, 1)
		// l_byteaSplit(ba1, splitloc, pba2) as int
		// l_byteaSplit(L_BYTEA *, size_t, L_BYTEA **) as l_ok
		///  <summary>
		/// l_byteaSplit()
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/l_byteaSplit/*"/>
		///  <param name="ba1">[in] - lba to split array bytes nulled beyond the split loc</param>
		///  <param name="splitloc">[in] - location in ba1 to split ba2 begins there</param>
		///  <param name="pba2">[out] - with data starting at splitloc</param>
		///   <returns>0 if OK, 1 on error</returns>
		public static int l_byteaSplit(
						L_Bytea ba1,
						uint splitloc,
						out L_Bytea pba2)
		{
			if (ba1 == null) {throw new ArgumentNullException  ("ba1 cannot be Nothing");}

			IntPtr pba2Ptr = IntPtr.Zero;
			int _Result = Natives.l_byteaSplit(ba1.Pointer,   splitloc, out pba2Ptr);
			if (pba2Ptr == IntPtr.Zero) {pba2 = null;} else { pba2 = new L_Bytea(pba2Ptr); };

			return _Result;
		}

		// bytearray.c (540, 1)
		// l_byteaFindEachSequence(ba, sequence, seqlen, pda) as int
		// l_byteaFindEachSequence(L_BYTEA *, const l_uint8 *, size_t, L_DNA **) as l_ok
		///  <summary>
		/// l_byteaFindEachSequence()
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/l_byteaFindEachSequence/*"/>
		///  <param name="ba">[in] - </param>
		///  <param name="sequence">[in] - subarray of bytes to find in data</param>
		///  <param name="seqlen">[in] - length of sequence, in bytes</param>
		///  <param name="pda">[out] - byte positions of each occurrence of %sequence</param>
		///   <returns>0 if OK, 1 on error</returns>
		public static int l_byteaFindEachSequence(
						L_Bytea ba,
						Byte[] sequence,
						uint seqlen,
						out L_Dna pda)
		{
			if (ba == null) {throw new ArgumentNullException  ("ba cannot be Nothing");}

			if (sequence == null) {throw new ArgumentNullException  ("sequence cannot be Nothing");}

			IntPtr pdaPtr = IntPtr.Zero;
			int _Result = Natives.l_byteaFindEachSequence(ba.Pointer,   sequence,   seqlen, out pdaPtr);
			if (pdaPtr == IntPtr.Zero) {pda = null;} else { pda = new L_Dna(pdaPtr); };

			return _Result;
		}

		// bytearray.c (578, 1)
		// l_byteaWrite(fname, ba, startloc, endloc) as int
		// l_byteaWrite(const char *, L_BYTEA *, size_t, size_t) as l_ok
		///  <summary>
		/// l_byteaWrite()
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/l_byteaWrite/*"/>
		///  <param name="fname">[in] - output file</param>
		///  <param name="ba">[in] - </param>
		///  <param name="startloc">[in] - first byte to output</param>
		///  <param name="endloc">[in] - last byte to output use 0 to write to the end of the data array</param>
		///   <returns>0 if OK, 1 on error</returns>
		public static int l_byteaWrite(
						String fname,
						L_Bytea ba,
						uint startloc,
						uint endloc)
		{
			if (fname == null) {throw new ArgumentNullException  ("fname cannot be Nothing");}

			if (ba == null) {throw new ArgumentNullException  ("ba cannot be Nothing");}

			int _Result = Natives.l_byteaWrite(  fname, ba.Pointer,   startloc,   endloc);
			return _Result;
		}

		// bytearray.c (612, 1)
		// l_byteaWriteStream(fp, ba, startloc, endloc) as int
		// l_byteaWriteStream(FILE *, L_BYTEA *, size_t, size_t) as l_ok
		///  <summary>
		/// l_byteaWriteStream()
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/l_byteaWriteStream/*"/>
		///  <param name="fp">[in] - file stream opened for binary write</param>
		///  <param name="ba">[in] - </param>
		///  <param name="startloc">[in] - first byte to output</param>
		///  <param name="endloc">[in] - last byte to output use 0 to write to the end of the data array</param>
		///   <returns>0 if OK, 1 on error</returns>
		public static int l_byteaWriteStream(
						FILE fp,
						L_Bytea ba,
						uint startloc,
						uint endloc)
		{
			if (fp == null) {throw new ArgumentNullException  ("fp cannot be Nothing");}

			if (ba == null) {throw new ArgumentNullException  ("ba cannot be Nothing");}

			int _Result = Natives.l_byteaWriteStream(fp.Pointer, ba.Pointer,   startloc,   endloc);
			return _Result;
		}


	}
}
