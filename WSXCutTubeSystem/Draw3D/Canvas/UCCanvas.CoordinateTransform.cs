using System.Drawing;
using System.Windows.Forms;
using WSX.Draw3D.MathTools;

namespace WSX.Draw3D
{
    /// <summary>
    /// 坐标转换
    /// </summary>
    public partial class UCCanvas
    {
        private PointF mouseDownPoint;
        
        private PointF GetMousePoint()
        {
            Point point = this.PointToClient(Control.MousePosition);
            return point;
        }

        private PointF ScreenPointToOpenGLPoint(PointF screenPoint)
        {
            PointF result = new PointF();
            result.X = (screenPoint.X- this.Width / 2);
            result.Y = (-screenPoint.Y + this.Height / 2);
            return result;
        }

        private PointF OpenGLToScreen(PointF point)
        {
            float[] result = MatrixHelper.Multi4x4with4x1(this.modelMatrix2, new float[] { point.X, point.Y, 0, 1 });
            result[0] += this.Width / 2;
            result[1] = this.Height / 2 - result[1];
            return new PointF(result[0], result[1]);
        }

    }
}
