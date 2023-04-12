using Autodesk.Revit.Creation;
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
                return hostLine.Direction.IsParallel(targetLine.Direction, tolerance);
            }
            else
            {
                return hostLine.Direction.IsParallel(targetLine.Direction, tolerance)
                    || hostLine.Direction.IsParallel(-targetLine.Direction, tolerance);
            }
        }

        /// <summary>
        /// line is vertical
        /// </summary>
        /// <param name="hostLine">host line</param>
        /// <param name="targetLine"> target line</param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool IsVertical(this Line hostLine, Line targetLine, double tolerance = 0.01)
        {
            return hostLine.Direction.IsVertical(targetLine.Direction, tolerance);
        }

        /// <summary>
        /// Create a bound line
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static Line CreateBoundLine(this XYZ startPoint, XYZ endPoint)
        {
            if (startPoint.IsEqual(endPoint))
            {
                throw new ArgumentException("The distance is too close");
            }
            return Line.CreateBound(startPoint, endPoint);
        }

        /// <summary>
        /// create line by direction and distance
        /// </summary>
        /// <param name="start"></param>
        /// <param name="direction"></param>
        /// <param name="distance"></param>
        /// <param name="extendByMid"></param>
        /// <returns></returns>
        public static Line CreateBoundLine(this XYZ start, XYZ direction, double distance, bool extendByMid = false)
        {
            if (extendByMid)
            {
                return CreateBoundLine(start - direction.Normalize() * distance / 2.0, start + direction.Normalize() * distance / 2.0);
            }
            return CreateBoundLine(start, start + direction.Normalize() * distance);
        }

        /// <summary>
        /// Extend line by start point
        /// </summary>
        /// <param name="line"></param>
        /// <param name="extendLength"></param>
        /// <returns></returns>
        public static Line ExtendLengthByStart(this Line line, double extendLength)
        {
            XYZ direction = line.Direction;
            Line boundLine = line.ToBound();
            return CreateBoundLine(boundLine.StartPoint() - direction * extendLength, boundLine.EndPoint());
        }

        /// <summary>
        /// Extend line by end point
        /// </summary>
        /// <param name="line"></param>
        /// <param name="extendLength"></param>
        /// <returns></returns>
        public static Line ExtendLengthByEnd(this Line line, double extendLength)
        {
            XYZ direction = line.Direction;
            Line boundLine = line.ToBound();
            return CreateBoundLine(boundLine.StartPoint(), boundLine.EndPoint() + direction * extendLength);
        }

        /// <summary>
        /// Extend line by end point
        /// </summary>
        /// <param name="line"></param>
        /// <param name="extendLength"></param>
        /// <returns></returns>
        public static Line ExtendLengthByMiddle(this Line line, double extendLength)
        {
            XYZ direction = line.Direction;
            Line boundLine = line.ToBound();
            return CreateBoundLine(boundLine.StartPoint() - direction * extendLength / 2, boundLine.EndPoint() + direction * extendLength / 2);
        }

        /// <summary>
        /// transfer unbound line to bound
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static Line ToBound(this Line line)
        {
            if (line.IsBound)
            {
                return line;
            }
            return line.Origin.CreateBoundLine(line.Direction, 100000.0, true);
        }

    }
}
