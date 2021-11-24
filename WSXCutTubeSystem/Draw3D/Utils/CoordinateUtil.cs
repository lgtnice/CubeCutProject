using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSX.CommomModel.DrawModel;
using WSX.Draw3D.MathTools;
using WSX.GlobalData.Model;

namespace WSX.Draw3D.Utils
{
    public class CoordinateUtil
    {
        /// <summary>
        /// 投影后的二维坐标(屏幕中心为原点)转换为屏幕二维坐标
        /// </summary>
        /// <param name="pointF"></param>
        /// <returns></returns>
        public static PointF CenterPointToScreenPoint(PointF pointF)
        {
            float centerX = GlobalModel.CanvasSize.Width / 2;
            float centerY = GlobalModel.CanvasSize.Height / 2;
            centerX = centerX + pointF.X;
            centerY = centerY + pointF.Y;
            return new PointF(centerX, centerY);
        }

        /// <summary>
        /// 三维坐标转换为二维坐标(投影后的以屏幕中心为原点的二维坐标)
        /// </summary>
        /// <param name="point3D"></param>
        /// <returns></returns>
        public static PointF Point3dToCenterPoint(Point3D point3D)
        {
            float[] vertexPoint = { point3D.X, point3D.Y, point3D.Z, 1 };
            float[] result = { 0, 0, 0, 0 };

            result = MatrixHelper.Multi4x4with4x1(GlobalModel.ModelMatrix2, vertexPoint);

            return new PointF(result[0], result[1]);
        }

        /// <summary>
        /// 三维坐标转换为屏幕(二维)坐标
        /// </summary>
        /// <param name="point3D"></param>
        /// <returns></returns>
        public static PointF Point3dToScreenPoint(Point3D point3D)
        {
            float[] vertexPoint = { point3D.X, point3D.Y, point3D.Z, 1 };
            float[] result = MatrixHelper.Multi4x4with4x1(GlobalModel.ModelMatrix2, vertexPoint);

            float centerX = GlobalModel.CanvasSize.Width / 2;
            float centerY = GlobalModel.CanvasSize.Height / 2;

            return new PointF(centerX + result[0], centerY + result[1]);
        }
    }
}
