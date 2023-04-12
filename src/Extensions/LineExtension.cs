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
    /// Revit Line extension
    /// </summary>
    public static class LineExtension
    {
        /// <summary>
        /// line is equal
        /// </summary>
        /// <param name="hostLine">host line</param>
        /// <param name="targetLine"> target line</param>
        /// <param name="sameDirection">required same direction</param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool IsEqual(this Line hostLine, Line targetLine, bool sameDirection = true, double tolerance = 0.01)
        {
            bool result1 = hostLine.StartPoint().IsEqual(targetLine.StartPoint(), tolerance)
                     && hostLine.EndPoint().IsEqual(targetLine.EndPoint(), tolerance);
            if (sameDirection)
            {
                return result1;
            }
            else
            {
                bool result2 = hostLine.StartPoint().IsEqual(targetLine.EndPoint(), tolerance)
                         && hostLine.EndPoint().IsEqual(targetLine.StartPoint(), tolerance);
                return result2 || result1;
            }
        }

        /// <summary>
        /// line is parallel
        /// </summary>
        /// <param name="hostLine">host line</param>
        /// <param name="targetLine"> target line</param>
        /// <param name="sameDirection">required same direction</param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool IsParallel(this Line hostLine, Line targetLine, bool sameDirection = true, double tolerance = 0.01)
        {
            if (sameDirection)
            {
                return hostLine.Direction.IsEqual(targetLine.Direction, tolerance);
            }
            else
            {
                return hostLine.Direction.IsEqual(targetLine.Direction, tolerance)
                    || hostLine.Direction.IsEqual(-targetLine.Direction, tolerance);
            }
        }

    }
}
