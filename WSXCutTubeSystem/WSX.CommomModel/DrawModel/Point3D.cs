using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WSX.CommomModel.DrawModel
{
    [Serializable]
    public class Point3D
    {
        [XmlAttribute("X")]
        public float X { get; set; }
        [XmlAttribute("Y")]
        public float Y { get; set; }
        [XmlAttribute("Z")]
        public float Z { get; set; }
        [XmlAttribute("Wt")]
        public float Weight { get; set; } = 1.0f;

        [XmlIgnore]
        public bool HasMicroConn { get; set; }

        public Point3D() : this(0, 0, 0)
        { }
        public Point3D(float x = 0, float y = 0, float z = 0)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        public Point3D(Point3D p) : this(p.X, p.Y, p.Z)
        { }
        public Point3D(double x , double y, double z):this((float)x,(float)y,(float)z)
        { }
        /// <summary>
        /// 平移
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Translate(float x, float y, float z)
        {
            X += x;
            Y += y;
            Z += z;
        }
        /// <summary>
        /// 旋转，参考https://www.cnblogs.com/esCharacter/p/8820610.html
        /// </summary>
        /// <param name="anglex">x轴旋转弧度</param>
        /// <param name="angley">y轴旋转弧度</param>
        /// <param name="anglez">z轴旋转弧度</param>
        public void Rotate(float anglex, float angley, float anglez)
        {
            float x, y, z;
            if (anglex != 0.00f)
            {
                y = (float)(Y * Math.Cos(anglex) - Z * Math.Sin(anglex));
                z = (float)(Y * Math.Sin(anglex) + Z * Math.Cos(anglex));
                Y = y;
                Z = z;
            }
            if (angley != 0.00f)
            {
                x = (float)(X * Math.Cos(angley) - Z * Math.Sin(angley));
                z = (float)(X * Math.Sin(angley) + Z * Math.Cos(angley));
                X = x;
                Z = z;
            }
            if (anglez != 0.00f)
            {
                x = (float)(X * Math.Cos(anglez) - Y * Math.Sin(anglez));
                y = (float)(X * Math.Sin(anglez) + Y * Math.Cos(anglez));
                X = x;
                Y = y;
            }
        }

        public PointF Point
        {
            get
            {
                return new PointF(this.X, this.Y);
            }
        }
        public static Point3D Zero
        {
            get { return new Point3D(0, 0, 0); }
        }
        //public static Point3D operator *(Point3D point, float a)
        //{
        //    Point3D result = new Point3D(point.X * a, point.Y * a, point.Z * a);
        //    return result;
        //}
        //public static Point3D operator +(Point3D p1, Point3D p2)
        //{
        //    Point3D result = new Point3D(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
        //    return result;
        //}
        #region overloaded operators
        public override bool Equals(object other)
        {
            if (other is Point3D)
                return this.Equals((Point3D)other);
            return false;
        }

        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode();
        }

        public static bool operator ==(Point3D u, Point3D v)
        {
            return Equals(u, v);
        }

        public static bool operator !=(Point3D u, Point3D v)
        {
            return !Equals(u, v);
        }

        public static Point3D operator +(Point3D u, Point3D v)
        {
            return new Point3D(u.X + v.X, u.Y + v.Y, u.Z + v.Z);
        }

        public static Point3D Add(Point3D u, Point3D v)
        {
            return new Point3D(u.X + v.X, u.Y + v.Y, u.Z + v.Z);
        }

        public static Point3D operator -(Point3D u, Point3D v)
        {
            return new Point3D(u.X - v.X, u.Y - v.Y, u.Z - v.Z);
        }

        public static Point3D Subtract(Point3D u, Point3D v)
        {
            return new Point3D(u.X - v.X, u.Y - v.Y, u.Z - v.Z);
        }

        public static Point3D operator -(Point3D u)
        {
            return new Point3D(-u.X, -u.Y, -u.Z) ;
        }

        public static Point3D Negate(Point3D u)
        {
            return new Point3D(-u.X, -u.Y, -u.Z);
        }

        public static Point3D operator *(Point3D u, double a)
        {
            return new Point3D(u.X * a, u.Y * a, u.Z * a);
        }

        public static Point3D Multiply(Point3D u, double a)
        {
            return new Point3D(u.X * a, u.Y * a, u.Z * a);
        }

        public static Point3D operator *(double a, Point3D u)
        {
            return new Point3D(u.X * a, u.Y * a, u.Z * a);
        }

        public static Point3D Multiply(double a, Point3D u)
        {
            return new Point3D(u.X * a, u.Y * a, u.Z * a);
        }

        public static Point3D operator *(Point3D u, Point3D v)
        {
            return new Point3D(u.X * v.X, u.Y * v.Y, u.Z * v.Z);
        }

        public static Point3D Multiply(Point3D u, Point3D v)
        {
            return new Point3D(u.X * v.X, u.Y * v.Y, u.Z * v.Z);
        }

        public static Point3D operator /(double a, Point3D u)
        {
            return new Point3D(a * u.X, a * u.Y, a * u.Z);
        }

        public static Point3D Divide(double a, Point3D u)
        {
            return new Point3D(a * u.X, a * u.Y, a * u.Z);
        }

        public static Point3D operator /(Point3D u, double a)
        {
            double invEscalar = 1 / a;
            return new Point3D(u.X * invEscalar, u.Y * invEscalar, u.Z * invEscalar);
        }

        public static Point3D Divide(Point3D u, double a)
        {
            double invEscalar = 1 / a;
            return new Point3D(u.X * invEscalar, u.Y * invEscalar, u.Z * invEscalar);
        }

        public static Point3D operator /(Point3D u, Point3D v)
        {
            return new Point3D(u.X / v.X, u.Y / v.Y, u.Z / v.Z);
        }

        public static Point3D Divide(Point3D u, Point3D v)
        {
            return new Point3D(u.X / v.X, u.Y / v.Y, u.Z / v.Z);
        }

        #endregion
    }
}
