using System;

namespace Gig3D
{
    public class Vector<T>
    {
        public T x;
        public T y;
        public Vector(T x, T y)
        {
            this.x = x;
            this.y = y;
        }
        public override string ToString()
        {
            return x.ToString() + " " + y.ToString();
        }
        public T Mod()
        {
            dynamic x = this.x, y = this.y;
            return Math.Sqrt(x * x + y * y);
        }

        public static Vector<T> operator -(Vector<T> v1, Vector<T> v2)
        {
            dynamic x1 = v1.x, x2 = v2.x, y1 = v1.y, y2 = v2.y;
            return new Vector<T>(x1 - x2, y1 - y2);
        }
        public static Vector<T> operator +(Vector<T> v1, Vector<T> v2)
        {
            dynamic x1 = v1.x, x2 = v2.x, y1 = v1.y, y2 = v2.y;
            return new Vector<T>(x1 + x2, y1 + y2);
        }
    }
}
