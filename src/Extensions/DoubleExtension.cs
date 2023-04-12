using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tuna.Revit.Extension.Extensions
{
    /// <summary>
    /// Dotnet double extension
    /// </summary>
    public static class DoubleExtension
    {
        /// <summary>
        /// value is equal
        /// </summary>
        /// <param name="firstValue"></param>
        /// <param name="secondValue"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool IsEqual(this double firstValue,double secondValue,double tolerance = 0) 
        {
            return Math.Abs(firstValue - secondValue) <= tolerance;
        }


    }
}
