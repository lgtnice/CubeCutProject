using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSX.CommomModel.Utilities
{
    public class MathUtils
    {
        public static double DegreeToRad(double degree)
        {
            return degree * Math.PI / 180.0;
        }
    }
}
