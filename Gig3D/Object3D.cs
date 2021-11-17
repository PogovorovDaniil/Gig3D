using System;
using System.Collections.Generic;

namespace Gig3D
{
    public class Object3D
    {
        internal unsafe float* polygons;
        internal int polyCount;
        internal IntPtr array_view;
        private List<float> polyList;
        public float x;
        public float y;
        public float z;
        public float angleX;
        public float angleY;
        public float angleZ;
        public Object3D(float x, float y, float z, float angleX, float angleY, float angleZ)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.angleX = angleX;
            this.angleY = angleY;
            this.angleZ = angleZ;
        }
        public void StartDraw()
        {
            polyList = new List<float>();
        }
        public void AddPoly(float x1, float y1, float z1, float x2, float y2, float z2, float x3, float y3, float z3)
        {
            polyList.Add(x1);
            polyList.Add(y1);
            polyList.Add(z1);

            polyList.Add(x2);
            polyList.Add(y2);
            polyList.Add(z2);

            polyList.Add(x3);
            polyList.Add(y3);
            polyList.Add(z3);
        }
        public void StopDraw()
        {
            polyCount = polyList.Count / 9;
            unsafe
            {
                polygons = DllMethod.GetFloatMem(polyList.Count);
                for(int i = 0; i < polyList.Count; i++)
                {
                    polygons[i] = polyList[i];
                }
                array_view = DllMethod.GetArrayViewPolygons(polygons, polyCount);
            }
            polyList.Clear();
        }
    }
}
