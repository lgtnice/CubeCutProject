using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.DrawModel;

namespace WSX.Draw3D.DrawTools
{
    public class SquareCircle3D : Spline3D
    {
        /// <summary>
        /// 
        /// </summary>
        public Point3D CenterPoint { get; set; } = new Point3D();
        public float Radius { get; set; } = 0.0f;

        private SquareCircle3D() { }

        public SquareCircle3D(float radius, float angle, Point3D translateDistance, float rectangleWidth, float rectangleHeight, bool rightOrLeft,bool topOrBottom)
        {
            Type = FigureType.Spline;
            IsClosed = false;

            float K = 0.64884669265831163572049554337619f;
            float weight = 0.94925302f;

            Point3D unitVectorX = new Point3D(1, 0, 0);
            Point3D unitVectorY = new Point3D(0, 1, 0);
            Point3D unitVectorXY = new Point3D(-(float)Math.Sqrt(2) / 2 , (float)Math.Sqrt(2) / 2 , 0);
            Knots.AddRange(new float[] { 0, 0, 0, 0, 0.5f, 0.5f, 0.5f, 1, 1, 1, 1 });

            //y轴上的位移
            Point3D yMove = new Point3D();
            //x轴上的位移
            Point3D xMove = new Point3D();
            //y = -x方向上的位移
            Point3D xyMove = new Point3D();
            //管长平移量
            Point3D translateMove = translateDistance;
            //因为矩形而产生的位移
            Point3D recMove = unitVectorX * (rectangleWidth / 2.0f) + unitVectorY * (rectangleHeight / 2.0f);
            //临时存放
            Point3D temp = new Point3D();

            xMove = new Point3D();
            yMove = unitVectorY * radius;
            xyMove = new Point3D();
            temp = (xMove + yMove + xyMove + recMove) * (rightOrLeft ? new Point3D(1, 1, 1) : new Point3D(-1, 1, 1)) * (topOrBottom ? new Point3D(1, 1, 1) : new Point3D(1, -1, 1));
            ControlPoints.Add(temp + new Point3D(0,0,(float)Math.Tan(angle) * temp.X) + translateMove);

            xMove = unitVectorX * ((float)Math.Tan(Math.PI / 8) * radius * K);
            yMove = unitVectorY * radius;
            xyMove = new Point3D();
            temp = (xMove + yMove + xyMove + recMove) * (rightOrLeft ? new Point3D(1, 1, 1) : new Point3D(-1, 1, 1)) * (topOrBottom ? new Point3D(1, 1, 1) : new Point3D(1, -1, 1));
            ControlPoints.Add(temp + new Point3D(0, 0, (float)Math.Tan(angle) * temp.X) + translateMove);

            xMove = new Point3D((float)Math.Cos(Math.PI / 4) * radius, 0, 0);
            yMove = new Point3D(0, (float)Math.Cos(Math.PI / 4) * radius, 0);
            xyMove = unitVectorXY * ((float)Math.Tan(Math.PI / 8) * radius * K);
            temp = (xMove + yMove + xyMove + recMove) * (rightOrLeft ? new Point3D(1, 1, 1) : new Point3D(-1, 1, 1)) * (topOrBottom ? new Point3D(1, 1, 1) : new Point3D(1, -1, 1));
            ControlPoints.Add(temp + new Point3D(0, 0, (float)Math.Tan(angle) * temp.X) + translateMove);

            xMove = new Point3D((float)Math.Cos(Math.PI / 4) * radius, 0, 0);
            yMove = new Point3D(0, (float)Math.Cos(Math.PI / 4) * radius, 0);
            xyMove = new Point3D();
            temp = (xMove + yMove + xyMove + recMove) * (rightOrLeft ? new Point3D(1, 1, 1) : new Point3D(-1, 1, 1)) * (topOrBottom ? new Point3D(1, 1, 1) : new Point3D(1, -1, 1));
            ControlPoints.Add(temp + new Point3D(0, 0, (float)Math.Tan(angle) * temp.X) + translateMove);

            xMove = new Point3D((float)Math.Cos(Math.PI / 4) * radius, 0, 0);
            yMove = new Point3D(0, (float)Math.Cos(Math.PI / 4) * radius, 0);
            xyMove = unitVectorXY * ((float)Math.Tan(Math.PI / 8) * radius * K) * -1.0f;
            temp = (xMove + yMove + xyMove + recMove) * (rightOrLeft ? new Point3D(1, 1, 1) : new Point3D(-1, 1, 1)) * (topOrBottom ? new Point3D(1, 1, 1) : new Point3D(1, -1, 1));
            ControlPoints.Add(temp + new Point3D(0, 0, (float)Math.Tan(angle) * temp.X) + translateMove);

            xMove = unitVectorX * radius;
            yMove = unitVectorY * ((float)Math.Tan(Math.PI / 8) * radius * K);
            xyMove = new Point3D();
            temp = (xMove + yMove + xyMove + recMove) * (rightOrLeft ? new Point3D(1, 1, 1) : new Point3D(-1, 1, 1)) * (topOrBottom ? new Point3D(1, 1, 1) : new Point3D(1, -1, 1));
            ControlPoints.Add(temp + new Point3D(0, 0, (float)Math.Tan(angle) * temp.X) + translateMove);

            xMove = unitVectorX * radius;
            yMove = new Point3D();
            xyMove = new Point3D();
            temp = (xMove + yMove + xyMove + recMove) * (rightOrLeft ? new Point3D(1, 1, 1) : new Point3D(-1, 1, 1)) * (topOrBottom ? new Point3D(1, 1, 1) : new Point3D(1, -1, 1));
            ControlPoints.Add(temp + new Point3D(0, 0, (float)Math.Tan(angle) * temp.X) + translateMove);

            ControlPoints[1].Weight = weight;
            ControlPoints[2].Weight = weight;
            ControlPoints[4].Weight = weight;
            ControlPoints[5].Weight = weight;

            CenterPoint = translateDistance;
            Radius = radius;
        }
    }
}
