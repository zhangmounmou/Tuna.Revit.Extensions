using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tuna.Revit.Extension.Extensions
{
    /// <summary>
    /// Revit XYZ extension
    /// </summary>
    public static class XYZExtension
    {
        /// <summary>
        /// point is equal
        /// </summary>
        /// <param name="host"></param>
        /// <param name="target"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool IsEqual(this XYZ host, XYZ target, double tolerance = 0.001)
        {
            return host.X.IsEqual(target.X, tolerance)
               && host.Y.IsEqual(target.Y, tolerance)
               && host.Z.IsEqual(target.Z, tolerance);
        }

        /// <summary>
        /// vector is Vertical
        /// </summary>
        /// <param name="host"></param>
        /// <param name="target"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool IsVertical(this XYZ host, XYZ target, double tolerance = 0.0)
        {
            return host.Normalize().DotProduct(target.Normalize()).IsEqual(0.0, tolerance);
        }

        /// <summary>
        /// vector is parallel
        /// </summary>
        /// <param name="host"></param>
        /// <param name="target"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool IsParallel(this XYZ host, XYZ target, double tolerance = 0.0)
        {
            return host.Normalize().DotProduct(target.Normalize()).IsEqual(1.0, tolerance);
        }
    }
}
