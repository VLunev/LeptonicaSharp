using System;
using System.IO;

using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LeptonicaSharp
{
	public partial class _All
	{

		// stack.c (78, 1)
		// lstackCreate(nalloc) as L_Stack
		// lstackCreate(l_int32) as L_STACK *
		///  <summary>
		/// lstackCreate()
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/lstackCreate/*"/>
		///  <param name="nalloc">[in] - initial ptr array size use 0 for default</param>
		///   <returns>lstack, or NULL on error</returns>
		public static L_Stack lstackCreate(
						int nalloc)
		{
			IntPtr _Result = Natives.lstackCreate(  nalloc);

			if (_Result == IntPtr.Zero) {return null;}

			return  new L_Stack(_Result);
		}

		// stack.c (121, 1)
		// lstackDestroy(plstack, freeflag) as Object
		// lstackDestroy(L_STACK **, l_int32) as void
		///  <summary>
		/// (1) If freeflag is TRUE, frees each struct in the array.<para/>
		///
		/// (2) If freeflag is FALSE but there are elements on the array,
		/// gives a warning and destroys the array.  This will
		/// cause a memory leak of all the items that were on the lstack.
		/// So if the items require their own destroy function, they
		/// must be destroyed before the lstack.<para/>
		///
		/// (3) To destroy the lstack, we destroy the ptr array, then
		/// the lstack, and then null the contents of the input ptr.
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/lstackDestroy/*"/>
		///  <param name="plstack">[in,out] - to be nulled</param>
		///  <param name="freeflag">[in] - TRUE to free each remaining struct in the array</param>
		public static void lstackDestroy(
						ref L_Stack plstack,
						int freeflag)
		{
			IntPtr plstackPtr = IntPtr.Zero; 	if (plstack != null) {plstackPtr = plstack.Pointer;}
			Natives.lstackDestroy(ref plstackPtr,   freeflag);
			if (plstackPtr == IntPtr.Zero) {plstack = null;} else { plstack = new L_Stack(plstackPtr); };
		}

		// stack.c (167, 1)
		// lstackAdd(lstack, item) as int
		// lstackAdd(L_STACK *, void *) as l_ok
		///  <summary>
		/// lstackAdd()
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/lstackAdd/*"/>
		///  <param name="lstack">[in] - </param>
		///  <param name="item">[in] - to be added to the lstack</param>
		///   <returns>0 if OK 1 on error.</returns>
		public static int lstackAdd(
						L_Stack lstack,
						Object item)
		{
			if (lstack == null) {throw new ArgumentNullException  ("lstack cannot be Nothing");}

			if (item == null) {throw new ArgumentNullException  ("item cannot be Nothing");}

			IntPtr itemPtr = IntPtr.Zero;

			if (item.GetType() == typeof(IntPtr)) {
				itemPtr = (IntPtr)item;
			} else if (item.GetType() == typeof(Byte[])) {
				var cdata = (Byte[])item;
				itemPtr = Marshal.AllocHGlobal(cdata.Length);
				Marshal.Copy(cdata, 0, itemPtr, cdata.Length);
			} else if (item.GetType().GetProperty("item") != null) {
				var cdata = (Byte[])item.GetType().GetProperty("data").GetValue(item, null);
				itemPtr = Marshal.AllocHGlobal(cdata.Length);
				Marshal.Copy(cdata, 0, itemPtr, cdata.Length);
			}

			int _Result = Natives.lstackAdd(lstack.Pointer,   itemPtr);
			Marshal.FreeHGlobal(itemPtr);
			return _Result;
		}

		// stack.c (197, 1)
		// lstackRemove(lstack) as Object
		// lstackRemove(L_STACK *) as void *
		///  <summary>
		/// lstackRemove()
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/lstackRemove/*"/>
		///  <param name="lstack">[in] - </param>
		///   <returns>ptr to item popped from the top of the lstack, or NULL if the lstack is empty or on error</returns>
		public static Object lstackRemove(
						L_Stack lstack)
		{
			if (lstack == null) {throw new ArgumentNullException  ("lstack cannot be Nothing");}

			IntPtr _Result = Natives.lstackRemove(lstack.Pointer);
			Byte[] B = new Byte[1];
			Marshal.Copy(_Result, B, 0, B.Length);
			return B;
		}

		// stack.c (247, 1)
		// lstackGetCount(lstack) as int
		// lstackGetCount(L_STACK *) as l_int32
		///  <summary>
		/// lstackGetCount()
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/lstackGetCount/*"/>
		///  <param name="lstack">[in] - </param>
		///   <returns>count, or 0 on error</returns>
		public static int lstackGetCount(
						L_Stack lstack)
		{
			if (lstack == null) {throw new ArgumentNullException  ("lstack cannot be Nothing");}

			int _Result = Natives.lstackGetCount(lstack.Pointer);
			return _Result;
		}

		// stack.c (270, 1)
		// lstackPrint(fp, lstack) as int
		// lstackPrint(FILE *, L_STACK *) as l_ok
		///  <summary>
		/// lstackPrint()
		///  </summary>
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/lstackPrint/*"/>
		///  <param name="fp">[in] - file stream</param>
		///  <param name="lstack">[in] - </param>
		///   <returns>0 if OK 1 on error</returns>
		public static int lstackPrint(
						FILE fp,
						L_Stack lstack)
		{
			if (fp == null) {throw new ArgumentNullException  ("fp cannot be Nothing");}

			if (lstack == null) {throw new ArgumentNullException  ("lstack cannot be Nothing");}

			int _Result = Natives.lstackPrint(fp.Pointer, lstack.Pointer);
			return _Result;
		}


	}
}
