using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Dynamo_Desktop
{
    public static class ImportHelper
    {
        public struct Native
        {
            /// <summary>
            /// Initializes the X threading system
            /// </summary>
            /// <remarks>Linux X11 only</remarks>
            /// <returns>non-zero on success, zero on failure</returns>
            [DllImport("libX11", CallingConvention = CallingConvention.Cdecl)]
            public static extern int XInitThreads();

        }
    }
}
