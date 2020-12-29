using System;
using System.IO;

using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LeptonicaSharp
{
	public partial class _All
	{

		// fliphmtgen.c (77, 1)
		// pixFlipFHMTGen(pixd, pixs, selname) as Pix
		// pixFlipFHMTGen(PIX *, PIX *, const char *) as PIX *
		///  <remarks>
		///  </remarks>
		///  <include file="..\CHM_Help\IncludeComments.xml" path="Comments/pixFlipFHMTGen/*"/>
		///   <returns></returns>
		public static Pix pixFlipFHMTGen(
						Pix pixd,
						Pix pixs,
						String selname)
		{
			if (pixd == null) {throw new ArgumentNullException  ("pixd cannot be Nothing");}

			if (pixs == null) {throw new ArgumentNullException  ("pixs cannot be Nothing");}

			if (selname == null) {throw new ArgumentNullException  ("selname cannot be Nothing");}

			IntPtr pixdPtr = IntPtr.Zero; if (pixd != null) {pixdPtr = pixd.Pointer;}
			IntPtr pixsPtr = IntPtr.Zero; if (pixs != null) {pixsPtr = pixs.Pointer;}
			IntPtr _Result = Natives.pixFlipFHMTGen(pixd.Pointer, pixs.Pointer,   selname);

			if (_Result == IntPtr.Zero) {return null;}

			return  new Pix(_Result);
		}


	}
}
