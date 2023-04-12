using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuna.Revit.Extension.Extensions
{
    /// <summary>
    /// Revit curve extension
    /// </summary>
    public static class CurveExtension
    {
        /// <summary>
        /// start point of curve
        /// </summary>
        /// <param name="curve"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static XYZ StartPoint(this Curve curve) 
        {
            if (curve == null) 
            {
                throw new ArgumentNullException(nameof(curve), "curve can not be null");
            }
            if (!curve.IsBound) 
            {
                throw new ArgumentException(nameof(curve), "curve is unbound and does not have start ");
            }
            return curve.GetEndPoint(0);
        }

        /// <summary>
        /// end point of curve
        /// </summary>
        /// <param name="curve"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        public static XYZ EndPoint(this Curve curve)
        {
            if (curve == null)
            {
                throw new ArgumentNullException(nameof(curve), "curve can not be null");
            }
            if (!curve.IsBound)
            {
                throw new ArgumentException(nameof(curve), "curve is unbound and does not have end ");
            }
            return curve.GetEndPoint(1);
        }

    }
}
