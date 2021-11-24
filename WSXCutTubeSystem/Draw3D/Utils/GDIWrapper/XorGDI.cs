using SharpGL;
using System;
using System.Drawing;
using System.IO;
using WSX.CommomModel.ParaModel;
using WSX.GlobalData.Model;

namespace WSX.Draw3D.Utils
{
    public static class XorGDI
    {
        //private static int NullBrush = 5;
        private static int Transparent = 1;

        public static void DrawLine(PenStyles penStyles, int penWidth, Color color, Graphics graphics, int x1, int y1, int x2, int y2)
        {
            IntPtr hdc = graphics.GetHdc();
            IntPtr pen = GDI.CreatePen(penStyles, penWidth, GDI.RGB(color.R, color.G, color.B));
            GDI.SetROP2(hdc, DrawingMode.R2_XORPEN);
            GDI.SetBkMode(hdc, Transparent);
            GDI.SetROP2(hdc, DrawingMode.R2_XORPEN);
            IntPtr oldPen = GDI.SelectObject(hdc, pen);
            GDI.MoveToEx(hdc, x1, y1, 0);
            GDI.LineTo(hdc, x2, y2);
            GDI.SelectObject(hdc, oldPen);
            GDI.DeleteObject(pen);
            graphics.Dispose();
        }

        public static void DrawRectangle(OpenGL gl, int x1, int y1, int x2, int y2)
        {
            gl.PushMatrix();
            gl.LoadIdentity();
            gl.Begin(OpenGL.GL_QUADS);
            gl.Color(0.0f, 0.0f, 0.7f);
            gl.Vertex(x1, y1,0);
            gl.Color(0.0f, 0.0f, 0.7f);
            gl.Vertex(x2, y1,0);
            gl.Color(0.0f, 0.0f, 0.7f);
            gl.Vertex(x2, y2,0);
            gl.Color(0.0f, 0.0f, 0.7f);
            gl.Vertex(x1, y2,0);
            gl.End();
            gl.Flush();
            gl.PopMatrix();
        }

        public static void DrawLine(OpenGL gl, float[] color, float x1,float y1, float x2, float y2)
        {
            gl.PushMatrix();
            gl.LoadIdentity();
            gl.Begin(OpenGL.GL_LINES);
            gl.Color(color);
            gl.Vertex(x1, y1, 0);
            gl.Vertex(x2, y2, 0);
            gl.End();
            gl.Flush();
            gl.PopMatrix();
        }

        #region 画文字(支持中文) 
        /// <summary>
        /// 画布右上角显示管信息(类型 宽高、长)
        /// </summary>
        /// <param name="tubeMode"></param>
        /// <param name="gl"></param>
        /// <param name="g_ctrl"></param>
        public static void DrawTubeMessage(StandardTubeMode tubeMode, OpenGL gl, Graphics g_ctrl)
        {
            if (tubeMode == null)
                return;
            String text = tubeMode.ToString();
            int x = (int)GlobalModel.CanvasSize.Width - (text.Length + 2) * 12;
            int y = (int)GlobalModel.CanvasSize.Height - 30;
            XorGDI.DrawCNText(x, y, text, new int[] { 255, 255, 255 }, "宋体", 14f, gl, g_ctrl);
        }

        public static void DrawTubeMessage1(StandardTubeMode tubeMode, OpenGL gl)
        {
            if (tubeMode == null)
                return;
            String text = tubeMode.ToString();
            int x = (int)GlobalModel.CanvasSize.Width - (text.Length + 2) * 12;
            int y = (int)GlobalModel.CanvasSize.Height - 30;
            XorGDI.DrawCNText1(x, y, text, new float[] { 1.0f, 1.0f, 1.0f }, "宋体", 20, gl);
        }

        /// <summary>
        /// 右下角显示切断断面的Z轴位置
        /// </summary>
        /// <param name="z"></param>
        /// <param name="gl"></param>
        public static void DrawCutOffLocation(float z, OpenGL gl)
        {
            String text = String.Format("Z: {0}",z.ToString("f3"));
            int x = (int)GlobalModel.CanvasSize.Width - (text.Length + 1) * 12;
            int y = 5;
            XorGDI.DrawCNText1(x, y, text, new float[] { 1.0f, 1.0f, 1.0f }, "宋体", 20, gl);
        }

        /// <summary>
        /// 在显示区指定位置显示文本(屏幕坐标 左下角为0,0)
        /// </summary>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        /// <param name="m_text">显示文本</param>
        public static void DrawCNText1(float x, float y, string text, float[] color, String fontName, int fontSize, OpenGL gl)
        {
            if (string.IsNullOrEmpty(text))
                return;

            gl.Color(color);

            fontName = string.IsNullOrEmpty(fontName) ? "新宋体" : fontName;

            //  Create the font based on the face name.
            var hFont = Win32.CreateFont(fontSize, 0, 0, 0, Win32.FW_DONTCARE, 0, 0, 0, Win32.DEFAULT_CHARSET,
                Win32.OUT_OUTLINE_PRECIS, Win32.CLIP_DEFAULT_PRECIS, Win32.CLEARTYPE_QUALITY, Win32.CLEARTYPE_NATURAL_QUALITY, fontName);

            //  Select the font handle.
            var hOldObject = Win32.SelectObject(gl.RenderContextProvider.DeviceContextHandle, hFont);
            //  Create the list base.
            var list = gl.GenLists(1);

            gl.PushMatrix();
            gl.LoadIdentity();
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.PushMatrix();
            gl.LoadIdentity();
            gl.Ortho2D(0.0, (double)GlobalModel.CanvasSize.Width, 0.0, (double)GlobalModel.CanvasSize.Height);

            gl.RasterPos(x, y);

            // 逐个输出字符
            for (int i = 0; i < text.Length; ++i)
            {
                bool result = Win32.wglUseFontBitmapsW(gl.RenderContextProvider.DeviceContextHandle, text[i], 1, list);
                gl.CallList(list);
            }
            // 回收所有临时资源
            //free(wstring);
            gl.DeleteLists(list, 1);
            //  Reselect the old font.
            Win32.SelectObject(gl.RenderContextProvider.DeviceContextHandle, hOldObject);
            //  Free the font.
            Win32.DeleteObject(hFont);
            //glDeleteLists(list, 1);

            gl.PopMatrix();
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.PopMatrix();
            gl.End();
        }

        /// <summary>
        /// 将指定 字体格式 的文本输出为 实际显示的 像素 字节流
        /// </summary>
        /// <param name="text">显示文本</param>
        /// <param name="pixBuffer">输出的像素缓冲</param>
        /// <param name="font">字体格式</param>
        private static void BuildFont(string text, int[] color, Font font, Graphics g_ctrl, out byte[] pixBuffer, out SizeF m_size)
        {
            if (font == null)
                font = new Font("宋体", 9F);
            Color m_color = Color.FromArgb(color[0], color[1], color[2]);

            m_size = g_ctrl.MeasureString(text, font).ToSize() + new Size(1, 0);
            g_ctrl.Dispose();

            pixBuffer = new byte[(int)(m_size.Width * m_size.Height * 4)];
            Bitmap bitmap = new Bitmap((int)m_size.Width, (int)m_size.Height);
            Graphics g_bmp = Graphics.FromImage(bitmap);
            Brush brush = new SolidBrush(m_color);
            
            g_bmp.Clear(System.Drawing.Color.FromArgb(1, 1, 0, 0));
            g_bmp.DrawString(text, font, brush, new Rectangle(0, 0, (int)m_size.Width, (int)m_size.Height));
            System.IO.MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
            Array.Copy(stream.ToArray(), 54, pixBuffer, 0, pixBuffer.Length);

            stream.Dispose();
            brush.Dispose();
            g_bmp.Dispose();
            bitmap.Dispose();
        }

        /// <summary>
        /// 在显示区指定位置显示文本(屏幕坐标 左下角为0,0)
        /// </summary>
        /// <param name="x">横坐标</param>
        /// <param name="y">纵坐标</param>
        /// <param name="m_text">显示文本</param>
        public static void DrawCNText(int x, int y, string text, int[] color,String fontName, float fontSize, OpenGL gl, Graphics g_ctrl)
        {
            if (string.IsNullOrEmpty(text))
                return;
            Font font = new Font(fontName, fontSize);

            SizeF m_size;
            byte[] pixBuffer;

            XorGDI.BuildFont(text, color, font, g_ctrl, out pixBuffer, out m_size);

            gl.BindTexture(OpenGL.GL_TEXTURE_2D, 0);   // 取消材质绑定

            gl.PushMatrix();
            gl.LoadIdentity();

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.PushMatrix();
            gl.LoadIdentity();
            gl.Ortho2D(0.0, (double)GlobalModel.CanvasSize.Width, 0.0, (double)GlobalModel.CanvasSize.Height);

            gl.RasterPos(x, y);

            gl.Enable(OpenGL.GL_BLEND); // 让绘制的文字透明
            //GL.glBlendFunc(GL.GL_SRC_ALPHA, GL.GL_ONE_MINUS_SRC_ALPHA);
            gl.DrawPixels((int)m_size.Width, (int)m_size.Height, OpenGL.GL_RGBA,  pixBuffer);//OpenGL.GL_UNSIGNED_BYTE,

            gl.Disable(OpenGL.GL_BLEND);

            gl.PopMatrix();

            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.PopMatrix();
        }
        #endregion

        public static void DrawRectangle(OpenGL gl, PointF topLeft, PointF bottomRight)
        {
            Console.WriteLine($"P1:{topLeft.X},{topLeft.Y}");
            Console.WriteLine($"P1:{bottomRight.X},{bottomRight.Y}");
            Console.WriteLine();
            DrawRectangle(gl, (int)topLeft.X, (int)topLeft.Y, (int)bottomRight.X, (int)bottomRight.Y);
        }
    }
}