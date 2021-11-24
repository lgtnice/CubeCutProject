using System;
using System.Collections.Generic;

namespace WSX.Draw3D.DrawTools
{
    public class ArcHelper
    {
        public List<Point3D> GetPointOfCircel(float radius)
        {
            int n = 100;
            List<Point3D> circlePoint = new List<Point3D>();
            for (int i = 0; i < n; i++)
            {
                circlePoint.Add(new Point3D(0.0f, (float)(radius * Math.Cos(2 * Math.PI * i / n)), (float)(radius * Math.Sin(2 * Math.PI * i / n))));
            }
            return circlePoint;
        }

        public List<Point3D> GetPointOfArc(float radius,float startAngle,float endAngle)
        {
            int n = (int)radius*3;
            List<Point3D> arcPoint = new List<Point3D>();
            float diff, x, y;
            if (startAngle < endAngle)
            {
                diff = endAngle - startAngle;                
            }
            else
            {
                diff = endAngle - startAngle + 360.0f;               
            }
            for (int i = 0; i < n; i++)
            {
                x = (float)(radius * Math.Cos((startAngle + diff / n * i) / 360.0 * 2 * Math.PI));
                y = (float)(radius * Math.Sin((startAngle + diff / n * i) / 360.0 * 2 * Math.PI));
                arcPoint.Add(new Point3D(x, y, 0.0f));
            }
            x = (float)(radius * Math.Cos(endAngle / 360.0 * 2 * Math.PI));
            y = (float)(radius * Math.Sin(endAngle / 360.0 * 2 * Math.PI));
            arcPoint.Add(new Point3D(x, y, 0.0f));
            return arcPoint;
        }

    }
}
